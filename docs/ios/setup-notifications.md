## Notification Setup (iOS)

This setup allows NearIT to show notifications automatically.<br>

### iOS10+
If your app is closed or in background, a system notification will be added to the Notification Center.
If your app is in foreground, the notification will be shown inside the app.

```csharp
// AppDelegate
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {

    });
    UNUserNotificationCenter.Current.Delegate = new UserNotificationDelegate();
 
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
```
When user taps on notification, you have two ways to manage this event:
#### Without the use of our bridge
```
var userInfo = response.Notification.Request.Content.UserInfo;

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
    NITManager.DefaultManager.ProcessRecipeWithUserInfo(notif, (content, trackingInfo, error) =>
    {
        if (content != null && content is NITReactionBundle)
        {
            //call the ParseContent to manage your notification
            NITManager.DefaultManager.ParseContent(content, trackingInfo, <NITContentDelegateListener>);
        }
    });
}
```
#### Using our bridge
```
NearBridge.NearBridgeiOS.ProcessRecipeWithUserInfo(response, (NITReactionBundle content, NITTrackingInfo trackingInfo) =>
{
    //call the ParseContent to manage your notification
    NITManager.DefaultManager.ParseContent(content, trackingInfo, <NITContentDelegateListener>);
});

```
<br><br>
The code below is called when a notification arrived.

```csharp
{
    ...
    manager.delegate = <NITManagerDelegate>;
    ...
}

public class NearSDKManager : NITManagerDelegate {
    public override void EventFailureWithError(NITManager manager, NSError error)
    {
        ...
    }

    public override void EventWithContent(NITManager manager, NSObject content, NITTrackingInfo trackingInfo)
    {
        ...
    }
}
```

<br>

### iOS9
If your app is closed or in background, a system notification will be added to the Notification Center.
If your app is in foreground, an alert will be shown.

```csharp
//App Delegate
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
}  

```

```
// Manage tap on remote notifications
public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
{
    NITManager.DefaultManager.ProcessRecipeWithUserInfo((Foundation.NSDictionary<Foundation.NSString, Foundation.NSObject>)userInfo, (content, trackingInfo, error) => {
        if (content != null && content is NITReactionBundle)
        {
            NITManager.DefaultManager.ParseContent(content, trackingInfo, <NITContentDelegateListener>);
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
            NITManager.DefaultManager.ParseContent(content, trackingInfo, <NITContentDelegateListener>);
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

## Trackings
NearIT allows to track user engagement events on recipes. Any recipe has at least two default events:

  - **Notified**: the user *received* a notification
  - **Engaged**: the user *tapped* on the notification
  
Usually the SDK tracks those events automatically, but if you write custom code to show notification or content please make sure that at least the "**notified**" event is tracked.
<br>**Warning:** Failing in tracking this event cause some NearIT features to not work.


You can track **default or custom events** using the "**sendTracking**" method:

```
// notified - notification received
NITManager.DefaultManager.SendTrackingWithTrackingInfo(trackingInfo, "notified");

// engaged - notification tapped
NITManager.DefaultManager.SendTrackingWithTrackingInfo(trackingInfo, "engaged");

// custom recipe event
NITManager.DefaultManager.SendTrackingWithTrackingInfo(trackingInfo, "my awesome custom event");
```
