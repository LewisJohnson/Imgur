namespace ImgurLibrary
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;

    public static class AnonymousUpload
    {
        public static string UploadImage(string image)
        {
            var base64 = "";
            try
            {
                 base64 = Convert.ToBase64String(File.ReadAllBytes(image));
            }
            catch (Exception exception)
            {
                Console.WriteLine("Image was shit. Failed to convert. " + exception);
            }

            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Client-ID " + ClientId.ReturnClientId());
                    var data = new NameValueCollection{ ["image"] = base64, ["type"] = "base64" };
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



