## Notification Setup (iOS)

This setup allows NearIT to show notifications automatically.<br>
When an user taps on a notification, an "**handleNearContent**" method will be called to let you manage in-app content presentation.

### Check iOS Version
```csharp
// AppDelegate
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
    {
        UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {

           });
        UNUserNotificationCenter.Current.Delegate = nsdelegate;
        UIApplication.SharedApplication.RegisterForRemoteNotifications();
    }
    else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
    {
        var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,new NSSet());

        UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
        UIApplication.SharedApplication.RegisterForRemoteNotifications();
    }
    else
    {
        UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
        UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
    }
}

```
### iOS10+
If your app is closed or in background, a system notification will be added to the Notification Center.
If your app is in foreground, the notification will be shown inside the app.

```csharp
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
        NSDictionary userInfo = response.Notification.Request.Content.UserInfo;
        NITManager.DefaultManager.ProcessRecipeWithUserInfo((Foundation.NSDictionary<Foundation.NSString, Foundation.NSObject>)userInfo, (content, trackingInfo, error) => {
            if (content != null && content is NITReactionBundle) {
                Console.WriteLine("Near notification tap: " + content.NotificationMessage);
            }
        });
    }
}
```

<br>

### iOS9
If your app is closed or in background, a system notification will be added to the Notification Center.
If your app is in foreground, an alert will be shown.

```csharp
// Manage tap on remote notifications
public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
{
    NITManager.DefaultManager.ProcessRecipeWithUserInfo((Foundation.NSDictionary<Foundation.NSString, Foundation.NSObject>)userInfo, (content, trackingInfo, error) => {
        if (content != null && content is NITReactionBundle)
        {
            HandleNearContent(content);
        }
    });
}

// Manage tap on local notifications
public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
{
    NSDictionary userInfo = notification.UserInfo;
    NITManager.DefaultManager.ProcessRecipeWithUserInfo((Foundation.NSDictionary<Foundation.NSString, Foundation.NSObject>)userInfo, (content, trackingInfo, error) => {
        if (content != null && content is NITReactionBundle)
        {
            HandleNearContent(content);
        }
    });
}

// Manage tap on iOS9 in-app alert
public class NearDelegate : NITManagerDelegate
{

    public override void AlertWantsToShowContent(NITManager manager, NSObject content)
    {
        HandleNearContent(content);
    }
}
```

<br>If you want to customize your notifications, see this [section](../customize-notifications.md).

<br>
## Trackings
NearIT allows to track user engagement events on recipes. Any recipe has at least two default events:

  - **Notified**: the user *received* a notification
  - **Engaged**: the user *tapped* on the notification
  
Usually the SDK tracks those events automatically, but if you write custom code to show notification or content please make sure that at least the "**notified**" event is tracked.
<br>**Warning:** Failing in tracking this event cause some NearIT features to not work.


You can track **default or custom events** using the "**sendTracking**" method:
 
```csharp
// notified - notification received
NITManager.DefaultManager.SendTrackingWithTrackingInfo(trackingInfo, "notified");

// engaged - notification tapped
NITManager.DefaultManager.SendTrackingWithTrackingInfo(trackingInfo, "engaged");

// custom recipe event
NITManager.DefaultManager.SendTrackingWithTrackingInfo(trackingInfo, "my awesome custom event");
```
