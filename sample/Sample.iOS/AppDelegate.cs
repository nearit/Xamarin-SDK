using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Foundation;
using NearIT;
using UIKit;
using UserNotifications;
using XamarinBridge.iOS;

namespace Sample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            NearBridgeiOS.Init("YOUR_API_KEY");

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(7200);

            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {

            });
            // set a notification delegate
            UNUserNotificationCenter.Current.Delegate = new UserNotificationDelegate();

            CLLocationManager LocationManager = new CLLocationManager();

            LocationManager.AuthorizationChanged += (s, e) =>
            {
                if (e.Status == CLAuthorizationStatus.AuthorizedAlways)
                    NearBridgeiOS.Start();
            };

            LocationManager.RequestAlwaysAuthorization();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            NearBridgeiOS.PerformFetch(application, completionHandler);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return NITManager.DefaultManager.Application(app, url, options as NSDictionary<NSString, NSObject>);
        }
    }
}

public class UserNotificationDelegate : UNUserNotificationCenterDelegate
{
    public UserNotificationDelegate() { }

    public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
    {
        completionHandler(UNNotificationPresentationOptions.Alert);
    }

    public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
    {
        NearBridgeiOS.ParseContentFromResponse(response);
        // you will receive the Near content in your common Bridge implementation
    }
}
