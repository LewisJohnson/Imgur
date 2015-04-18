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
            WebBrowser.Source = new Uri(ImgurLibrary.Authorization.Authorise.ImgurPin());

        }


        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var res = ImgurLibrary.Authorization.Authorise.ImgurToken(PinBox.Text);
            var account = ImgurLibrary.ResponseParse.Account(res);

        }
    }


}
