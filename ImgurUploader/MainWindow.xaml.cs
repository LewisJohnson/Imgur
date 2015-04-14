namespace ImgurUploader
{
    using System;
    using System.Windows;
    using Microsoft.Win32;
    using ImgurLibrary;

    public partial class MainWindow
    {
        private string imagestring = "";

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void ImageSelector_OnClick(object sender, RoutedEventArgs routedEventArgs)
        {

            var dlg = new OpenFileDialog
            {
                DefaultExt = Properties.Resources.Default_File_Extension,
                Filter = Properties.Resources.FileTypes
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                this.imagestring = dlg.FileName;
            }
            else
            {
                Console.WriteLine(Properties.Resources.Something_went_wrong);
            }
        }

        private void Upload_OnClick(object sender, RoutedEventArgs e)
        {
           // var linkString = AnonymousUpload.UploadImage(this.imagestring);
           // this.Link.Text = linkString;

            var l = UserAuth.ImgurToken();
            Console.WriteLine(l);
        }
    }
}

