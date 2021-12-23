using Android.Annotation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Webkit;
using WebViewDemo;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyWebView), typeof(WebViewRenderer))]
namespace WebViewDemo.Droid
{
    public class MyWebViewRenderer : WebViewRenderer
    {
        Activity mContext;
        public MyWebViewRenderer(Context context) : base(context)
        {
            this.mContext = context as Activity;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.DomStorageEnabled = true;
            Control.Settings.SetPluginState(WebSettings.PluginState.On);

            Control.ClearCache(true);

            MyWebClient cwc = new MyWebClient(MainActivity.Instance);
            Control.SetWebChromeClient(cwc);
        }

        public class MyWebClient : WebChromeClient
        {
            Activity mContext;
            public MyWebClient(Activity context)
            {
                this.mContext = context;
            }
            [TargetApi(Value = 21)]
            public override void OnPermissionRequest(PermissionRequest request)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    mContext.RunOnUiThread(() =>
                    {
                        request.Grant(request.GetResources());

                    });
                }
            }
        }
    }
}