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
                DefaultExt = "*.*",
                Filter = "JPEG Files (*.jpeg) |*.jpeg|" +
                         "PNG Files  (*.png)  |*.png|" +
                         "JPG Files  (*.jpg)  |*.jpg|" +
                         "GIF Files  (*.gif)  |*.gif|" +
                         "All files  (*.*)    |*.*"
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
            var uploader = new AnonymousUpload();
            var linkString = uploader.UploadImage(this.imagestring);
            this.Link.Content = linkString;
        }
    }
}

