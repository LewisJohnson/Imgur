namespace ImgurUploader
{
    using System.Windows;

    using MahApps.Metro;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {

            var globaltheme = ThemeManager.DetectAppStyle(Current);

            ThemeManager.ChangeAppStyle(
                Current,
                ThemeManager.GetAccent("Cyan"),
                ThemeManager.GetAppTheme("BaseDark"));

            base.OnStartup(e);
        }
    }
}
