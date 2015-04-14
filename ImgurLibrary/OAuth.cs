namespace ImgurLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    public class OAuth
    {
        public class Manager
        {

            public Manager()
            {
                this.random = new Random();
                this._params = new Dictionary<string, string>{
                        ["callback"] = "oob",
                        ["consumer_key"] = "",
                        ["consumer_secret"] = "",
                        ["timestamp"] = GenerateTimeStamp(),
                        ["nonce"] = this.GenerateNonce(),
                        ["signature_method"] = "HMAC-SHA1",
                        ["signature"] = "",
                        ["token"] = "",
                        ["token_secret"] = "",
                        ["version"] = "1.0"
                };
            }

            public Manager(string consumerKey, string consumerSecret, string token, string tokenSecret)
                : this()

            {
                this._params["consumer_key"] = consumerKey;
                this._params["consumer_secret"] = consumerSecret;
                this._params["token"] = token;
                this._params["token_secret"] = tokenSecret;
            }

            public string this[string ix]
            {
                private get
                {
                    if (this._params.ContainsKey(ix)) return this._params[ix];
                    throw new ArgumentException(ix);
                }
                set
                {
                    if (!this._params.ContainsKey(ix)) throw new ArgumentException(ix);
                    this._params[ix] = value;
                }
            }


            private static string GenerateTimeStamp()
            {
                var ts = DateTime.UtcNow - Epoch;
                return Convert.ToInt64(ts.TotalSeconds).ToString();
            }

            private void NewRequest()
            {
                this._params["nonce"] = this.GenerateNonce();
                this._params["timestamp"] = GenerateTimeStamp();
            }

            private string GenerateNonce()
            {
                var sb = new StringBuilder();
                for (var i = 0; i < 8; i++)
                {
                    var g = this.random.Next(3);
                    switch (g)
                    {
                        case 0:
                            // lowercase alpha
                            sb.Append((char)(this.random.Next(26) + 97), 1);
                            break;
                        default:
                            // numeric digits
                            sb.Append((char)(this.random.Next(10) + 48), 1);
                            break;
                    }
                }
                return sb.ToString();
            }



            private Dictionary<string, string> ExtractQueryParameters(string queryString)
            {
                if (queryString.StartsWith("?")) queryString = queryString.Remove(0, 1);

                var result = new Dictionary<string, string>();

                if (string.IsNullOrEmpty(queryString)) return result;

                foreach (var s in queryString.Split('&').Where(s => !string.IsNullOrEmpty(s) && !s.StartsWith("oauth_"))
                    )
                {
                    if (s.IndexOf('=') > -1)
                    {
                        var temp = s.Split('=');
                        result.Add(temp[0], temp[1]);
                    }
                    else result.Add(s, string.Empty);
                }

                return result;
            }



            public static string UrlEncode(string value)
            {
                var result = new StringBuilder();
                foreach (var symbol in value)
                {
                    if (UnreservedChars.IndexOf(symbol) != -1) result.Append(symbol);
                    else result.Append('%' + $"{(int)symbol :X2}");
                }
                return result.ToString();
            }

            private static readonly string UnreservedChars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";



            private static string EncodeRequestParameters(ICollection<KeyValuePair<string, string>> p)
            {
                var sb = new StringBuilder();
                foreach (
                    var item in
                        p.OrderBy(x => x.Key)
                            .Where(item => !string.IsNullOrEmpty(item.Value) && !item.Key.EndsWith("secret")))
                {
                    sb.AppendFormat("oauth_{0}=\"{1}\", ", item.Key, UrlEncode(item.Value));
                }

                return sb.ToString().TrimEnd(' ').TrimEnd(',');
            }

            public OAuthResponse AcquireRequestToken(string uri, string method)
            {
                this.NewRequest();
                var authzHeader = this.GetAuthorizationHeader(uri, method);

                // prepare the token request
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("Authorization", authzHeader);
                request.Method = method;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var r = new OAuthResponse(reader.ReadToEnd());
                        this["token"] = r["oauth_token"];

                        // Sometimes the request_token URL gives us an access token,
                        // with no user interaction required. Eg, when prior approval
                        // has already been granted.
                        try
                        {
                            if (r["oauth_token_secret"] != null) this["token_secret"] = r["oauth_token_secret"];
                        }
                        catch {}
                        return r;
                    }
                }
            }


            public OAuthResponse AcquireAccessToken(string uri, string method, string pin)
            {
                this.NewRequest();
                this._params["verifier"] = pin;

                var authzHeader = this.GetAuthorizationHeader(uri, method);

                // prepare the token request
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("Authorization", authzHeader);
                request.Method = method;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var r = new OAuthResponse(reader.ReadToEnd());
                        this["token"] = r["oauth_token"];
                        this["token_secret"] = r["oauth_token_secret"];
                        return r;
                    }
                }
            }


            public string GenerateCredsHeader(string uri, string method, string realm)
            {
                this.NewRequest();
                var authzHeader = this.GetAuthorizationHeader(uri, method, realm);
                return authzHeader;
            }


            public string GenerateAuthzHeader(string uri, string method)
            {
                this.NewRequest();
                var authzHeader = this.GetAuthorizationHeader(uri, method, null);
                return authzHeader;
            }

            private string GetAuthorizationHeader(string uri, string method)
            {
                return this.GetAuthorizationHeader(uri, method, null);
            }

            private string GetAuthorizationHeader(string uri, string method, string realm)
            {
                if (string.IsNullOrEmpty(this._params["consumer_key"])) throw new ArgumentNullException("consumer_key");

                if (string.IsNullOrEmpty(this._params["signature_method"])) throw new ArgumentNullException("signature_method");

                this.Sign(uri, method);

                var erp = EncodeRequestParameters(this._params);
                return (string.IsNullOrEmpty(realm)) ? "OAuth " + erp : $"OAuth realm=\"{realm}\", " + erp;
            }


            private void Sign(string uri, string method)
            {
                var signatureBase = this.GetSignatureBase(uri, method);
                var hash = this.GetHash();

                var dataBuffer = Encoding.ASCII.GetBytes(signatureBase);
                var hashBytes = hash.ComputeHash(dataBuffer);

                this["signature"] = Convert.ToBase64String(hashBytes);
            }

            private string GetSignatureBase(string url, string method)
            {
                // normalize the URI
                var uri = new Uri(url);
                var normUrl = $"{uri.Scheme}://{uri.Host}";
                if (!((uri.Scheme == "http" && uri.Port == 80) || (uri.Scheme == "https" && uri.Port == 443))) normUrl += ":" + uri.Port;

                normUrl += uri.AbsolutePath;

                // the sigbase starts with the method and the encoded URI
                var sb = new StringBuilder();
                sb.Append(method).Append('&').Append(UrlEncode(normUrl)).Append('&');

                // the parameters follow - all oauth params plus any params on
                // the uri
                // each uri may have a distinct set of query params
                var p = this.ExtractQueryParameters(uri.Query);
                // add all non-empty params to the "current" params
                foreach (
                    var p1 in
                        this._params.Where(
                            p1 =>
                            !string.IsNullOrEmpty(this._params[p1.Key]) && !p1.Key.EndsWith("_secret")
                            && !p1.Key.EndsWith("signature")))
                {
                    p.Add("oauth_" + p1.Key, p1.Value);
                }

                // concat+format all those params
                var sb1 = new StringBuilder();
                foreach (var item in p.OrderBy(x => x.Key))
                {
                    // even "empty" params need to be encoded this way.
                    sb1.AppendFormat("{0}={1}&", item.Key, item.Value);
                }

                // append the UrlEncoded version of that string to the sigbase
                sb.Append(UrlEncode(sb1.ToString().TrimEnd('&')));
                var result = sb.ToString();
                return result;
            }



            private HashAlgorithm GetHash()
            {
                if (this["signature_method"] != "HMAC-SHA1") throw new NotImplementedException();

                string keystring = $"{UrlEncode(this["consumer_secret"])}&{UrlEncode(this["token_secret"])}";
                var hmacsha1 = new HMACSHA1{ Key = Encoding.ASCII.GetBytes(keystring) };
                return hmacsha1;
            }


            private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            private readonly Dictionary<string, string> _params;

            private readonly Random random;
        }



        public class OAuthResponse
        {
            private string AllText { get; set; }

            private readonly Dictionary<string, string> _params;

            public string this[string ix] => this._params[ix];

            public OAuthResponse(string alltext)
            {
                this.AllText = alltext;
                this._params = new Dictionary<string, string>();
                var kvpairs = alltext.Split('&');
                foreach (var kv in kvpairs.Select(pair => pair.Split('=')))
                {
                    this._params.Add(kv[0], kv[1]);
                }
                // expected keys:
                //   oauth_token, oauth_token_secret, user_id, screen_name, etc
            }
        }
    }
}


