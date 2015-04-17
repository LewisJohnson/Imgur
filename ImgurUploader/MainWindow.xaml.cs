using System;
using System.Linq;
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
            GetToken.ImgurPin();

            Console.Write("s");
            var pin = "c6e9e36cc8";
            var response = GetToken.ImgurToken(pin);
            var responseDictionary = ResponseParse.Account(response);
            var holder = responseDictionary.Aggregate("", (current, item) => current + (item + "\n"));



        }

        private void Upload_OnMouseDown(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Upload());
        }
    }
}



