using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ImgurLibrary;
using Microsoft.Win32;

namespace ImgurUploader.Pages
{

    public partial class Upload
    {
        public Upload()
        {

            InitializeComponent();
            loading.Visibility = Visibility.Hidden;
        }

        private string _imagestring = "";
        private string _holder;
        private string _type;

        private void ImageSelector_OnClick(object sender, RoutedEventArgs e)
        {
            loading.Visibility = Visibility.Visible;

            var dlg = new OpenFileDialog
            {
                DefaultExt = Properties.Resources.Default_File_Extension,
                Filter = Properties.Resources.File_Types
            };

            var result = dlg.ShowDialog();

            if (result == true)
            {
                _imagestring = dlg.FileName;
                _holder = dlg.FileName;
                _type = "base64";

                var uriSource = new Uri(_imagestring);
                imageHolder.Source = new BitmapImage(uriSource);

                UrlTextBox.Text = null;

                loading.Visibility = Visibility.Hidden;

                Debug.WriteLine("Type is:" + _type + " at " + _imagestring);

            }
            else
            {

                Debug.WriteLine(Properties.Resources.Something_Went_Wrong);

            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _type = "URL";
            _imagestring = UrlTextBox.Text;
            _holder = UrlTextBox.Text;

        }

        private void UrLlostfocus(object sender, RoutedEventArgs routedEventArgs)
        {
            loading.Visibility = Visibility.Visible;

            try
            {
                var uriSource = new Uri(_imagestring);
                imageHolder.Source = new BitmapImage(uriSource);
            }
            catch (Exception ex)
            {
                UrlTextBox.Text = null;
                Debug.WriteLine("EXECPTION: " + ex);
            }


            loading.Visibility = Visibility.Hidden;

            Debug.WriteLine("Type is:" + _type + " at " + _imagestring);
        }

        private void Upload_OnClick(object sender, RoutedEventArgs e)
        {
            if (UrlTextBox == null)
            {
                _type = "base64";
            }
            if (SupportedFileType.CheckAgainstFormat(_imagestring))
            {
                var response = ImgurLibrary.Api_Endpoints.Image.Upload.UploadImage(_imagestring, TitleTextBox.Text, DescTextBox.Text, _type);
                var responseDictionary = ResponseParse.Image(response);
                var holder = responseDictionary.Aggregate("", (current, item) => current + (item + "\n"));
                DescTextBox.Text = holder;
            }
            else
            {
                UrlTextBox.Text = "ヽ༼ ಠ益ಠ ༽ﾉ File isn't supported by imgur.";
            }


        }


    }
}
