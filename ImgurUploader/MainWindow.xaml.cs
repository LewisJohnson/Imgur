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

        public void Authorise_OnMouseDown(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Authorise());
        }

        public void Account_OnMouseDown(object sender, RoutedEventArgs e)
        {


        }

        public void Upload_OnMouseDown(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Upload());
        }

    }
}



