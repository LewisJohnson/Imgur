using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ImgurUploader
{
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            var list = new ObservableCollection<string>
            {
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test",
                "Test"
            };

            ListBox.ItemsSource = list;
            MainFrame.NavigationService.Navigate(new Pages.AnonUpload());
        }


    }
}



