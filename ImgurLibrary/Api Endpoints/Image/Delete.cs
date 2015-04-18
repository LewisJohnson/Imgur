using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace ImgurLibrary.Api_Endpoints.Image
{
    internal class Delete
    {
        public static string DeleteImage(string deletehash, string format = "xml")
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Client-ID " + GetClientInfo.ClientId);
                    var data = new NameValueCollection
                    {
                        ["id"] = deletehash,
                        ["_format"] = format
                    };


                    var responsePayload = client.UploadValues("https://api.imgur.com/3/image/", "POST", data);
                    var response = Encoding.ASCII.GetString(responsePayload);


                    if (response.Contains("true"))
                    {
                        Debug.WriteLine("Imgur has accepted request. Response: {0}", response);
                    }

                    return response;
                }
            }

            catch (Exception ಠ_ಠimgur)
            {
                Debug.WriteLine("Something went wrong: " + ಠ_ಠimgur.Message);
                return "Failed to delete image. Server response: " + ಠ_ಠimgur.Message;
            }
        }
    }
}
