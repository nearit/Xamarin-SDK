using System;
using System.Collections.Generic;
using System.Linq;

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
            LoadApplication(new App());

            // Xamarin.Forms.DependencyService.Register<XamarinBridge.PCL.Manager.INearFunc, XamarinBridge.iOS.NearBridgeiOS>();
            NearBridgeiOS.SetApiKey();
            // NITManager.SetupWithApiKey("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiI5MWMxYzViYzUxODU0ZjE0OGYzYWNiNmQ4YmE4NzU0ZiIsImlhdCI6MTUwNjQxODE2MCwiZXhwIjoxNjMyNzAwNzk5LCJkYXRhIjp7ImFjY291bnQiOnsiaWQiOiIxN2YxMjJiNi1iZjUwLTQ4ZGQtOWZiYi00OTVjMjc4OTZmMzkiLCJyb2xlX2tleSI6ImFwcCJ9fX0.b6AUrbcuwiPJNpY2f7gGH3Qi6s3ZTfCMALqTPwyjJxA");
            // NITManager.SetFrameworkName("xamarin");
            //NITManager.DefaultManager.TriggerInAppEventWithKey("test_trigger");

            //NITManager f = NITManager.DefaultManager;

            //f.HistoryWithCompletion((items, error) => {
            //    foreach(NITHistoryItem item in items) {
            //        string t = item.ToString();
            //    }
            //});

            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {

            });
            UNUserNotificationCenter.Current.Delegate = new UserNotificationDelegate();

            return base.FinishedLaunching(app, options);
        }
    }
}

// Create a delegate class
public class UserNotificationDelegate : UNUserNotificationCenterDelegate
{
    public UserNotificationDelegate()
    {
    }

    public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
    {
        completionHandler(UNNotificationPresentationOptions.Alert);
    }

    public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
    {
        // see code below
    }
}