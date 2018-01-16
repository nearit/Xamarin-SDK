# Notification Setup (Bridge) #

This setup allows you receive location based notification and beacon notification.

First of all add the permission to the native fragment you want to use and start the radar, [Notification Setup (iOS)](../ios/setup-notifications.md) or [Setup (Android)](../android/location-based-notifications.md)<br>

## iOS ##

When an user taps on a notification, call "**ParseContent**" method which will manage your in-app content and send it automatically in the common fragment.
```
if (content != null && content is NITReactionBundle)
{
    NearBridgeiOS.ParseContent(content);
}
```

## Android ##

When an user taps on a notification, call "**ParseIntent**" method inside your "*OnNewIntent*" which will manage your in-app content and send it automatically in the common fragment.
```
protected override void OnNewIntent(Intent intent)
{
    NearBridgeDroid.ParseIntent(intent);
}
```

To manage the [Beacon Interaction Content (Android)](../android/in-app-content.md), call "**ParseForegroundEvent**" method,
```
NearBridgeDroid.ParseForegroundEvent(IParcelable parcelable, TrackingInfo track);
```
## Push Notification ##

To enable push configuration [Push Notifications (iOS)](../ios/push-notifications.md), [Push Notifications(Android)](../android/push-notifications.md).

## Trackings ##
NearIT allows to track user engagement events on recipes. Any recipe has at least two default events:

- **Notified**: the user *received* a notification
- **Engaged**: the user *tapped* on the notification

Usually the SDK tracks those events automatically, but if you write custom code to show notification or content please make sure that at least the "**notified**" event is tracked.
<br>**Warning:** Failing in tracking this event cause some NearIT features to not work.


You can track **default or custom events** using the "**SendTracking**" method:
```
NearPCL.SendTracking(trackingInfo, <value>);
```
## Manual Configuration Refresh ##

The SDK **initialization is done automatically** and handles the task of syncing the recipes with our servers when your app starts up.
<br>However, if you need to sync the recipes configuration more often, you can call this method:

```
NearPCL.RefreshConfiguration();
```
