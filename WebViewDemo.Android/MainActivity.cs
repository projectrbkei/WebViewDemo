using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Webkit;
using Java.IO;
using Android.Content;
using Android.Provider;

namespace WebViewDemo.Droid
{
    [Activity(Label = "WebViewDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static IValueCallback mUploadCallbackAboveL;
        public static Android.Net.Uri imageUri;
        public static MainActivity Instance;
        public static int PHOTO_REQUEST = 10023;
        public static IValueCallback mUploadMessage;
        public static int FILECHOOSER_RESULTCODE = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            if (requestCode == PHOTO_REQUEST)
            {
                if (resultCode == Result.Ok)
                {
                    Android.Net.Uri result = intent.Data ?? imageUri;
                    if (mUploadCallbackAboveL != null)
                    {
                        mUploadCallbackAboveL.OnReceiveValue(new Android.Net.Uri[] { result });
                    }
                }
                else
                {
                    mUploadCallbackAboveL.OnReceiveValue(null);
                }
                mUploadCallbackAboveL = null;
            }
            else
            {
                base.OnActivityResult(requestCode, resultCode, intent);
            }
        }
    }

    public class CustomWebChromeClient : WebChromeClient
    {
        Activity mActivity = null;
        public CustomWebChromeClient(Activity activity)
        {
            mActivity = activity;
        }
        public override bool OnShowFileChooser(Android.Webkit.WebView webView, IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {
            MainActivity.mUploadCallbackAboveL = filePathCallback;
            //TakePhoto();
            File imageStorageDir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "MyAppFolder");
            if (!imageStorageDir.Exists())
            {
                imageStorageDir.Mkdirs();
            }
            // Create camera captured image file path and name, add ticks to make it unique 
            var file = new File(imageStorageDir + File.Separator + "IMG_" + DateTime.Now.Ticks + ".jpg");
            MainActivity.imageUri = Android.Net.Uri.FromFile(file);
            //Create camera capture image intent and add it to the chooser
            var captureIntent = new Intent(MediaStore.ActionImageCapture);
            captureIntent.PutExtra(MediaStore.ExtraOutput, MainActivity.imageUri);
            var i = new Intent(Intent.ActionGetContent);
            i.AddCategory(Intent.CategoryOpenable);
            i.SetType("image/*");
            var chooserIntent = Intent.CreateChooser(i, "Choose image");
            chooserIntent.PutExtra(Intent.ExtraInitialIntents, new Intent[] { captureIntent });
            MainActivity.Instance.StartActivityForResult(chooserIntent, MainActivity.PHOTO_REQUEST);
            return true;
        }
    }
}