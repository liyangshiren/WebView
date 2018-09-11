using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Webkit;
using System;
using Android.Views;

namespace MesWebView
{
    [Activity(Label = "江阴振宏锻造MES系统WebView", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        WebView webview;
        DateTime? lastBackKeyDownTime;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //soure code
            webview = FindViewById<WebView>(Resource.Id.webView);
            webview.SetWebViewClient(new ExtendWebViewClient());
            webview.LoadUrl("https://www.nopcommerce.com");//就这里需要改改根据具体的实际情况
            WebSettings webSettings = webview.Settings;
            webSettings.JavaScriptEnabled = true;
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (!lastBackKeyDownTime.HasValue || DateTime.Now - lastBackKeyDownTime.Value > new TimeSpan(0, 0, 2))
                {
                    Toast.MakeText(this.ApplicationContext, "再按一次退出程序", ToastLength.Short).Show();
                    lastBackKeyDownTime = DateTime.Now;
                    webview.GoBack();
                }
                else
                {
                    Finish();
                }
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

    }
    internal class ExtendWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}