using System;
using Android.Annotation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Webkit;
using Java.IO;
using WebViewDemo;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyWebView), typeof(WebViewRenderer))]
namespace WebViewDemo.Droid
{
    public class MyWebViewRenderer : WebViewRenderer
    {
        //Activity mContext;
        public MyWebViewRenderer(Context context) : base(context)
        {
            //this.mContext = context as Activity;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.DomStorageEnabled = true;
            Control.Settings.SetPluginState(WebSettings.PluginState.On);

            Control.ClearCache(true);

            CustomWebChromeClient cwc = new CustomWebChromeClient(MainActivity.Instance);
            Control.SetWebChromeClient(cwc);
        }
    }
}