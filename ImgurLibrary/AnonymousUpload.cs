namespace ImgurLibrary
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;

    public class AnonymousUpload
    {
        public string UploadImage(string image)
        {

            var base64 = Convert.ToBase64String(File.ReadAllBytes(image));

            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization", "Client-ID " + ClientId.ReturnClientId());
                    var data = new NameValueCollection{ ["image"] = base64, ["type"] = "base64" };
                    var responsePayload = client.UploadValues("https://api.imgur.com/3/image/", "POST", data);
                    return Encoding.ASCII.GetString(responsePayload);

                }
            }
            catch (Exception ಠ_ಠimgur)
            {

                Console.WriteLine("Something went wrong. " + ಠ_ಠimgur.Message);
                return "Failed!" + ಠ_ಠimgur.Message;
            }
        }
    }
}



