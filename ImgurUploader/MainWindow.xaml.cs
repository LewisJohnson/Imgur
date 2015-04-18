using System.Windows;
using ImgurUploader.Pages;

namespace ImgurUploader
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Authorise_OnMouseDown(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Authorise());
        }

        private void Account_OnMouseDown(object sender, RoutedEventArgs e)
        {


        }

        private void Upload_OnMouseDown(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Upload());
        }
    }
}



