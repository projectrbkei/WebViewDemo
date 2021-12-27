using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace WebViewDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //var browser = new WebView();
            //browser.Source = "https://remo.co/mic-cam-test/";
            //Content = browser;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RunTimePermission();
        }

        public async void RunTimePermission()
        {
            var status = PermissionStatus.Unknown;

            status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (status != PermissionStatus.Granted)
            {

                status = await Utils.CheckPermissions(Permission.Camera);
            }

        }
    }
}
