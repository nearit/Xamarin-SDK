using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Foundation;
using UIKit;
using NearIT;
using UserNotifications;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XamarinSample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        CLLocationManager LocationManager = new CLLocationManager();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            var apiKey = loadApiKey();

            NITManager.SetupWithApiKey(apiKey);
            NITManager.DefaultManager.SetDeferredUserDataWithKey("os", "iOS");

            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {

            });
            UNUserNotificationCenter.Current.Delegate = new UserNotificationDelegate();

            EventContent ec = new EventContent();
            ItemsViewModel ivm = new ItemsViewModel();

            LocationPermission();

            app.RegisterForRemoteNotifications();

            NITManager.DefaultManager.SetDeferredUserDataWithKey("age", "24");
            NITManager.DefaultManager.SetDeferredUserDataWithKey("name", "John");

            return base.FinishedLaunching(app, options);
        }

        public string loadApiKey()
        {
            NSDictionary settings = NSDictionary.FromFile("Keys.plist");

            if (settings != null)
            {
                var value = settings.ValueForKey(new NSString("apiKey"));
                if (value != null) return value.ToString();
            }

            return "";
        }

        public void LocationPermission()
        {
            LocationManager.AuthorizationChanged += (s, e) =>
            {
                if (e.Status == CLAuthorizationStatus.AuthorizedAlways)
                    NITManager.DefaultManager.Start();
                else
                    NITManager.DefaultManager.Stop();
            };

            LocationManager.RequestAlwaysAuthorization();
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Console.WriteLine("RegisteredForRemoteNotifications");
            NITManager.DefaultManager.SetDeviceTokenWithData(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Console.WriteLine("ERRORRegisteredForRemoteNotifications");
        }

        public void HandleNearContent(NSObject content)
        {
            if (content is NITSimpleNotification)
            {
                Console.WriteLine("Simple Notification");

            }

            if (content is NITCoupon)
            {
                Console.WriteLine("Coupon");
            }
        }

        public void RefreshConfig()
        {
            NITManager.DefaultManager.RefreshConfigWithCompletionHandler((error) => {
                if (error == null)
                {
                    Console.WriteLine("Refresh iOS");
                }
                else
                {
                    Console.WriteLine("NOT REFRESHED");
                }
            });
        }

        public void EnableForegroundNotification(bool val)
        {
            if (val is true)
            {
                NITManager.DefaultManager.ShowForegroundNotification = true;
            }
            else if (val is false)
            {
                NITManager.DefaultManager.ShowForegroundNotification = false;
            }

        }

    }



    public class UserNotificationDelegate : UNUserNotificationCenterDelegate
    {
        AppDelegate ad = new AppDelegate();

        public UserNotificationDelegate()
        {
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userInfo = response.Notification.Request.Content.UserInfo;

            ReceiveResponse(userInfo);
        }

        public void ReceiveResponse(NSDictionary userInfo)
        {
            NSString[] keys = new NSString[userInfo.Keys.Length];
            int i;
            for (i = 0; i < userInfo.Keys.Length; i++)
            {
                if (userInfo.Keys[i] is NSString)
                    keys[i] = userInfo.Keys[i] as NSString;
                else
                    i = int.MaxValue;
            }
            if (i != int.MaxValue)
            {
                NSDictionary<NSString, NSObject> notif = new NSDictionary<NSString, NSObject>(keys, userInfo.Values);
                NITManager.DefaultManager.ProcessRecipeWithUserInfo(notif, (content, recipe, error) =>
                {
                    if (content != null && content is NITReactionBundle)
                    {
                        Console.WriteLine("Near notification tap: " + content.NotificationMessage);
                        ad.HandleNearContent(content);
                    }
                });
            }
        }


    }



    public class EventContent : NITManagerDelegate
    {
        AppDelegate ad = new AppDelegate();
        public override void EventFailureWithError(NITManager manager, NSError error)
        {
            Console.WriteLine("error");
        }

        public override void EventWithContent(NITManager manager, NSObject content, NITTrackingInfo trackingInfo)
        {
            ad.HandleNearContent(content);
        }
    }


}