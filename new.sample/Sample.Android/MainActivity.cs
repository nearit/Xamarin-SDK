using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using IT.Near.Sdk;
using IT.Near.Sdk.Recipes.Inbox.Model;
using System.Collections.Generic;
using Android.Util;
using Android.Content;
using XamarinBridge.Droid;

namespace Sample.Droid
{
    [Activity(Label = "Sample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IT.Near.Sdk.Recipes.Inbox.NotificationHistoryManager.IOnNotificationHistoryListener
    {
        public void OnError(string p0)
        {
            Log.Error("inbox", p0);
        }

        public void OnNotifications(IList<HistoryItem> items)
        {
            foreach(HistoryItem item in items) {
                Log.Error("item", item.ToString());
            }
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            OnNewIntent(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            NearBridgeDroid.ParseIntent(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            string a = "d";
            if (CheckSelfPermission(Android.Manifest.Permission.AccessFineLocation) == Permission.Granted) {
                NearItManager.Instance.StartRadar();
            }

            NearItManager.Instance.GetHistory(this);

        }
    }
}