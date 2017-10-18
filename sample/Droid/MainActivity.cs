using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using IT.Near.Sdk;
using IT.Near.Sdk.Geopolis.Beacons.Ranging;
using IT.Near.Sdk.Utils;
using IT.Near.Sdk.Reactions.Simplenotificationplugin.Model;
using IT.Near.Sdk.Trackings;
using IT.Near.Sdk.Operation;
using IT.Near.Sdk.Reactions.Contentplugin.Model;
using IT.Near.Sdk.Reactions.Couponplugin.Model;
using IT.Near.Sdk.Reactions.Customjsonplugin.Model;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;
using Android;
using Android.Gms.Common;
using System.Collections.Generic;
using IT.Near.Sdk.Recipes;

namespace XamarinSample.Droid
{
    [Activity(Label = "XamarinSample.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        ProximityListener _proximityListener;
        readonly string[] PermissionsLocation = { Manifest.Permission.AccessFineLocation };
        const int RequestLocationId = 0;
        TextView msgText;
        UserDataListener _successListener;


        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            _proximityListener = new ProximityListener();

            NearItManager.Instance.AddProximityListener(_proximityListener);

            var userData = new Dictionary<string, string> {
                { "name", "John" } , {"age", "23"} , { "saw_tutorial" , "true" }
            };
            NearItManager.Instance.SetBatchUserData(userData, new UserDataListener());

            GetLocationPermission();
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
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

        public void RefreshConfig()
        {
            RecipeRefreshListener recipeRefreshListener = new RecipeRefreshListener(this);
            NearItManager.Instance.RefreshConfigs(recipeRefreshListener);
        }

    }

    internal class RecipeRefreshListener : Java.Lang.Object, IRecipeRefreshListener
    {

        Context context;

        public RecipeRefreshListener(Context context)
        {
            this.context = context;
        }

        public void OnRecipesRefresh()
        {
            Console.WriteLine("Recipe refreshed");
            //Toast.MakeText(context, "Recipe refreshed", ToastLength.Short).Show();
        }

        public void OnRecipesRefreshFail()
        {

        }
    }


    internal class ProximityListener : Java.Lang.Object, IProximityListener
    {
        private ICoreContentsListener _coreContentListener;

        public void ForegroundEvent(IParcelable p0, TrackingInfo p1)
        {
            Console.WriteLine("Ranging Beacon");
        }
    }




    internal class EventContent : Java.Lang.Object, ICoreContentsListener
    {
        public void GotContentNotification(Content p0, TrackingInfo p1)
        {
            Console.WriteLine("GotContentNotification ", p0.NotificationMessage);
        }

        public void GotCouponNotification(Coupon p0, TrackingInfo p1)
        {
            Console.WriteLine("GotCouponNotification ", p0.NotificationMessage);
        }

        public void GotCustomJSONNotification(CustomJSON p0, TrackingInfo p1)
        {
            Console.WriteLine("GotCustomJSONNotification ", p0.NotificationMessage);
        }

        public void GotFeedbackNotification(Feedback p0, TrackingInfo p1)
        {
            Console.WriteLine("GotFeedbackNotification ", p0.NotificationMessage);
        }

        public void GotSimpleNotification(SimpleNotification p0, TrackingInfo p1)
        {
            Console.WriteLine("Simple Notification: ", p0.NotificationMessage);
        }
    }




    internal class UserDataListener : Java.Lang.Object, IUserDataNotifier
    {
        public void OnDataCreated()
        {
            Console.WriteLine("on data created");
        }

        public void OnDataNotSetError(string p0)
        {
            Console.WriteLine("on data not set error");
        }
    }



}