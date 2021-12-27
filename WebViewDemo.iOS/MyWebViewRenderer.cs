using System;
using WebViewDemo;
using WebViewDemo.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyWebView), typeof(MyWebViewRenderer))]
namespace WebViewDemo.iOS
{
    public class MyWebViewRenderer: WkWebViewRenderer
    {
        public MyWebViewRenderer()
        {
        }
    }
}
