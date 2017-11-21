using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Foundation;
using NearBridge;
using NearIT;
using UIKit;
using UserNotifications;

namespace NearForms.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            NearBridgeiOS.SetApiKey();

            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {});
            UNUserNotificationCenter.Current.Delegate = new UserNotificationDelegate();

            CLLocationManager LocationManager = new CLLocationManager();
            LocationManager.AuthorizationChanged += (s, e) =>
            {
                if (e.Status == CLAuthorizationStatus.AuthorizedAlways)
                    NITManager.DefaultManager.Start();
                else
                    NITManager.DefaultManager.Stop();
            };

            LocationManager.RequestAlwaysAuthorization();


            return base.FinishedLaunching(app, options);
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

        internal class UserNotificationDelegate : UNUserNotificationCenterDelegate
        {
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
                            NearBridgeiOS.ParseContent(content);
                       }
                    });
                }
            }
        }

        internal class EventContent : NITManagerDelegate
        {
            public override void EventFailureWithError(NITManager manager, NSError error)
            {
                Console.WriteLine("error");
            }

            public override void EventWithContent(NITManager manager, NSObject content, NITTrackingInfo trackingInfo)
            {
                Console.WriteLine("EventWithContent");
                NearBridgeiOS.ParseContent(content);
            }
        }

    }
}
