namespace ImgurLibrary.AnonActions
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;

    public static class Upload
    {
        public static string UploadImage(string image, string title = "", string desc = "", string type = "base64")
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
                    Console.WriteLine("Image was shit. Failed to convert. " + exception);
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
                    client.Headers.Add("Authorization", "Client-ID " + GetClientInfo.ClientId());
                    var data = new NameValueCollection{
                                                          ["image"] = imageBase64,
                                                          ["title"] = title,
                                                          ["description"] = desc,
                                                          ["type"] = type,
                                                      };
                    var responsePayload = client.UploadValues("https://api.imgur.com/3/image/", "POST", data);
                    var response = Encoding.ASCII.GetString(responsePayload);
                    if (response.Contains("true"))
                    {
                        Console.WriteLine("Imgur has accepted request. Response: {0}", response);
                    }
                    return response;

                }
            }

                // ReSharper disable once InconsistentNaming
            catch (Exception ಠ_ಠimgur)
            {

                Console.WriteLine("Something went wrong. " + ಠ_ಠimgur.Message);
                return "Failed!" + ಠ_ಠimgur.Message;
            }
        }
    }
}



