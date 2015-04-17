using System;
using System.Diagnostics;

namespace ImgurUploader.Pages
{

    public partial class Authorise
    {
        private const int PinLength = 10;
        public Authorise()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            WebBrowser.Source = new Uri(ImgurLibrary.Authorise.ImgurPin());

        }


        private void WebBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (WebBrowser.Source.ToString().Contains("pin="))
            {
                Debug.Write("It's here.");
            }
        }

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var res = ImgurLibrary.Authorise.ImgurToken(PinBox.ToString());
            var account = ImgurLibrary.ResponseParse.Account(res);

        }
    }


}
