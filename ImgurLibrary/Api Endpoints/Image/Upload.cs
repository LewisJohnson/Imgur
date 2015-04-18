using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace ImgurLibrary.Api_Endpoints.Image
{
    public static class Upload
    {
        public static string UploadImage(string image, string title = "", string desc = "", string type = "base64", string format = "xml")
        {
            var imageBase64 = "";
            if (type == "base64")
            {
                try
                {
                    imageBase64 = Convert.ToBase64String(File.ReadAllBytes(image));
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Failed to convert image." + exception);
                }
            }
            else
            {
                imageBase64 = image;
            }

            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Client-ID " + GetClientInfo.ClientId);
                    var data = new NameValueCollection
                    {
                        ["image"] = imageBase64,
                        ["title"] = title,
                        ["description"] = desc,
                        ["type"] = type,
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
                return "Failed to upload image. Server response: " + ಠ_ಠimgur.Message;
            }
        }
    }
}