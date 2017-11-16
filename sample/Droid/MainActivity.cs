using System;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using IT.Near.Sdk.Utils;
using XamarinBridge.Droid;
using IT.Near.Sdk;
using Android.Gms.Common;

namespace NearForms.Droid
{
    [Activity(Label = "NearForms.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] PermissionsLocation = { Manifest.Permission.AccessFineLocation };
        const int RequestLocationId = 0;
        TextView msgText;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            GetLocationPermission();
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            OnNewIntent((Intent)Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            if (intent != null && NearUtils.CarriesNearItContent(intent))
            {
                NearBridgeDroid.ParseIntent(intent);
            }
        }

        public void GetLocationPermission()
        {
            const string permission = Manifest.Permission.AccessFineLocation;
            if (CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                Console.WriteLine("Radar Start!!");
                NearItManager.Instance.StartRadar();
                return;
            }

            RequestPermissions(PermissionsLocation, RequestLocationId);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted
                            NearItManager.Instance.StartRadar();
                        }
                        else
                        {
                            //Permission Denied :disappointed:
                            //Disabling location functionality
                            NearItManager.Instance.StopRadar();
                        }
                    }
                    break;
            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }
    }
}
