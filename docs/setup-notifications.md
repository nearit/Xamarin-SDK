## Notification Setup

This setup allows NearIT to show notifications automatically.<br>
When an user taps on a notification, an "**handleNearContent**" method will be called to let you manage in-app content presentation.

### iOS10+
If your app is closed or in background, a system notification will be added to the Notification Center.
If your app is in foreground, the notification will be shown inside the app.

```swift
// AppDelegate
func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplicationLaunchOptionsKey: Any]?) -> Bool {
    ...
    UNUserNotificationCenter.current().delegate = self
}

...
@available(iOS 10.0, *)
extension AppDelegate : UNUserNotificationCenterDelegate {
    
    func userNotificationCenter(_ center: UNUserNotificationCenter, willPresent notification: UNNotification, withCompletionHandler completionHandler: @escaping (UNNotificationPresentationOptions) -> Void) {
        completionHandler(.alert)
    }
    
    func userNotificationCenter(_ center: UNUserNotificationCenter, didReceive response: UNNotificationResponse, withCompletionHandler completionHandler: @escaping () -> Void) {
        let userInfo = response.notification.request.content.userInfo
        let isNearNotification = NearManager.shared.processRecipe(userInfo) { (content, trackingInfo, error) in
            if let content = content, let trackingInfo = trackingInfo {
                self.handleNearContent(content: content)
            }
        }
        completionHandler()
    }
}
```

<br>

### iOS9
If your app is closed or in background, a system notification will be added to the Notification Center.
If your app is in foreground, an alert will be shown.

```swift
// Manage tap on remote notifications
func application(_ application: UIApplication, didReceiveRemoteNotification userInfo: [AnyHashable : Any]) {
    let isNearNotification = NearManager.shared.processRecipe(userInfo, completion: { (content, trackingInfo, error) in
        if let content = content {
            self.handleNearContent(content: content)
        }
    })
}

// Manage tap on local notifications
func application(_ application: UIApplication, didReceive notification: UILocalNotification) {
    if let userInfo = notification.userInfo {
        let isNearNotification = NearManager.shared.processRecipe(userInfo, completion: { (content, trackingInfo, error) in
            if let content = content {
                self.handleNearContent(content: content)
            }
        })
    }
}

// Manage tap on iOS9 in-app alert
extension AppDelegate: NearManagerDelegate {

    func manager(_ manager: NearManager, alertWantsToShowContent content: Any) {
        ...
        handleNearContent(content: content)
    }

}
```

<br>If you want to customize your notifications, see this [section](customize-notifications.md).

<br>
## Trackings
NearIT allows to track user engagement events on recipes. Any recipe has at least two default events:

  - **Notified**: the user *received* a notification
  - **Engaged**: the user *tapped* on the notification
  
Usually the SDK tracks those events automatically, but if you write custom code to show notification or content please make sure that at least the "**notified**" event is tracked.
<br>**Warning:** Failing in tracking this event cause some NearIT features to not work.


You can track **default or custom events** using the "**sendTracking**" method:
 
<div class="code-swift">
// notified - notification received
manager.sendTracking(trackingInfo, event: NearRecipeTracking.notified.rawValue)

// engaged - notification tapped
manager.sendTracking(trackingInfo, event: NearRecipeTracking.engaged.rawValue)

// custom recipe event
manager.sendTracking(trackingInfo, event: "my awesome custom event")
</div>
<div class="code-objc">
// notified - notified received
[manager sendTrackingWithTrackingInfo:trackingInfo event:NITRecipeNotified];

// engaged - notification tapped
[manager sendTrackingWithTrackingInfo:trackingInfo event:NITRecipeEngaged];

// custom recipe event
[manager sendTrackingWithTrackingInfo:trackingInfo event:@"my awesome custom event"];
</div>
