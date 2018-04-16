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

            CLLocationManager LocationManager = new CLLocationManager();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            NearBridgeiOS.SetApiKey();


            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {});
            UNUserNotificationCenter.Current.Delegate = new UserNotificationDelegate();

            LocationManager.AuthorizationChanged += (s, e) =>
            {
                if (e.Status == CLAuthorizationStatus.AuthorizedAlways || e.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
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
                NearBridge.NearBridgeiOS.ProcessRecipeWithUserInfo(response, (NITReactionBundle content, NITTrackingInfo trackingInfo) =>
                {
                    //call the ParseContent to manage your notification
                    NearBridge.NearBridgeiOS.ParseContent(content, trackingInfo);
                });
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
                NearBridgeiOS.ParseContent(content, trackingInfo);
            }
        }

    }
}
