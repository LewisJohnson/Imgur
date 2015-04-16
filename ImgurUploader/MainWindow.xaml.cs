using System;
using System.Windows;
using ImgurLibrary;
using ImgurUploader.Pages;
using MahApps.Metro.Controls;

namespace ImgurUploader
{
    public partial class MainWindow : MetroWindow
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
            MainFrame.Source = new Uri(GetToken.ImgurToken());
        }

        private void Upload_OnMouseDown(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Upload());
        }
    }
}



