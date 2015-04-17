using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using static System.String;

namespace ImgurLibrary
{
    public class GetToken
    {

        public static string ImgurToken(string pin, string format = "xml")
        {

            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Bearer" + GetClientInfo.ClientId);
                    var data = new NameValueCollection
                    {
                        ["client_id"] = GetClientInfo.ClientId,
                        ["client_secret"] = GetClientInfo.Secret,
                        ["grant_type"] = "pin",
                        ["pin"] = pin,
                        ["_format"] = format
                    };
                    byte[] responsePayload = client.UploadValues("https://api.imgur.com/oauth2/token", "POST", data);
                    var response = Encoding.ASCII.GetString(responsePayload);
                    Debug.WriteLine(response);

                    return response;
                }
            }

            catch (Exception ಠ_ಠimgur)
            {
                Debug.WriteLine("Something went wrong. " + ಠ_ಠimgur.Message);
                return "Failed! " + ಠ_ಠimgur.Message;
            }
        }



        public static void ImgurPin()
        {
            const string url = "https://api.imgur.com/oauth2/authorize?client_id={0}&response_type={1}";
            var finalurl = Format(url, GetClientInfo.ClientId, "pin");
            Process.Start(finalurl);
        }
    }
}