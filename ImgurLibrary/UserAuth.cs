namespace ImgurLibrary
{
    public class UserAuth
    {
        public static string ImgurToken(string state = "")
        {
            string responseType = "token";
            const string Url = "https://api.imgur.com/oauth2/authorize?";
            const string DataTemplate = "client_id={0}&response_type={1}&state={2}";
            var data = string.Format(DataTemplate, ClientId.ReturnClientId(), responseType, state);
            
            var oauth = new OAuth.Manager();
            // the URL to obtain a temporary "request token"
            var rtUrl = "https://api.imgur.com/oauth2/authorize?";
            oauth["consumer_key"] = ClientId.ReturnClientId();
            oauth["consumer_secret"] = "908ff345a4b6aacc8b70f97d245b16239d5012d2";
            oauth.AcquireRequestToken(rtUrl, "POST");
            return "d";

            //using (WebClient client = new WebClient())
            //{
            //    var responsePayload = client.UploadString(Url, data);
            //
            //    // Use some random JSON Parser, you´ll get access_token and refresh_token
            //    //var Deserializer = new JavaScriptSerializer();
            //    //
            //    //var Response = Deserializer.DeserializeObject(ApiResponse) as Dictionary<string, object>;
            //    //return new ImgurToken(){
            //    //                           AccessToken = Convert.ToString(Response["access_token"]),
            //    //                           RefreshToken = Convert.ToString(Response["refresh_token"])
            //    //                       };
            //    return responsePayload;
            //}

        }
    }
}
