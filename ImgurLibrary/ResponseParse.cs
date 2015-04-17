using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ImgurLibrary
{
    public static class ResponseParse
    {
        public static Dictionary<string, string> Image(string response)
        {

            var responseReader = new XmlTextReader(new StringReader(response));
            var data           = XElement.Load(responseReader);

            var success = data.Attribute("success");
            var status  = data.Attribute("status");

            var id          = data.Element("id");
            var title       = data.Element("title");
            var description = data.Element("description");
            var datetime    = data.Element("datetime");
            var deletehash  = data.Element("deletehash");
            var link        = data.Element("link");
            var accountUrl  = data.Element("account_url");

            var returnDic      = new Dictionary<string, string>{
                {"success"     , success.Value },
                {"status"      , status.Value },
                { "Id"         , id?.Value },
                { "Title"      , title?.Value },
                { "Description", description?.Value },
                { "DateTime"   , datetime?.Value },
                { "Deletehash" , deletehash?.Value },
                { "Link"       , link?.Value },
                { "Account"    , accountUrl?.Value }
            };
            
            foreach (var item in returnDic)
            {
                Debug.WriteLine(item);
            }

            return returnDic;
        }


        public static Dictionary<string, string> Account(string response)
        {

            var responseReader = new XmlTextReader(new StringReader(response));
            var data = XElement.Load(responseReader);


            var accessToken = data.Element("access_token");
            var expireTime = data.Element("expires_in");
            var tokenType = data.Element("token_type");
            var refreshToken = data.Element("refresh_token");
            var accountUsername = data.Element("account_username");
            var accountId = data.Element("account_id");

            var returnDic = new Dictionary<string, string>{
                { "Access Token"    , accessToken?.Value },
                { "Expire Time"     , expireTime?.Value },
                { "Token Type"      , tokenType?.Value },
                { "Refresh Token"   , refreshToken?.Value },
                { "Username"        , accountUsername?.Value },
                { "Account ID"      , accountId?.Value },
            };

            foreach (var item in returnDic)
            {
                Debug.WriteLine(item);
            }

            return returnDic;
        }
    }
}