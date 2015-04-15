namespace ImgurLibrary
{
    using System.Diagnostics;

    public class GetToken
    {
        public static void ImgurToken(string state = "")
        {
            var Endpoint = "https://api.imgur.com/oauth2/authorize?client_id={0}&response_type={1}&state={2}";
            var url = string.Format(Endpoint, GetClientInfo.ClientId(), "token", state);
            Process.Start(url);
        }
    }
}
