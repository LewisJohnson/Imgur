using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace ImgurLibrary
//{
//    using System.Net;
//
//    class GetToken
//    {
//
//
//        public static ImgurToken ImgurToken(string clientId, string clientSecret, string pin)
//        {
//            string Url = "https://api.imgur.com/oauth2/token/";
//            string DataTemplate = "client_id={0}&client_secret={1}&grant_type=pin&pin={2}";
//            string Data = String.Format(DataTemplate, clientId, clientSecret, pin);
//
//            using (WebClient Client = new WebClient())
//            {
//                var ApiResponse = Client.UploadString(Url, Data);
//
//                // Use some random JSON Parser, you´ll get access_token and refresh_token
//                var Deserializer = new JavaScriptSerializer();
//                var Response = Deserializer.DeserializeObject(ApiResponse) as Dictionary<string, object>;
//
//                return new ImgurToken()
//                {
//                    AccessToken = Convert.ToString(Response["access_token"]),
//                    RefreshToken = Convert.ToString(Response["refresh_token"])
//                };
//            }
//        }
//    }
//}
