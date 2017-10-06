# Location Based Notifications (Android)

When you want to start the radar for **geofences and beacons**, you call this method:

```csharp
// call this when you are given the proper permission for scanning (ACCESS_FINE_LOCATION)
NearItManager.Instance.StartRadar();
// to stop the radar call the method nearItManager.stopRadar()
```

**VERY IMPORTANT** Only call the method, when you are given the ACCESS_FINE_LOCATION permission.

Also make sure to add those permissions in your `AndroidManifest.xml`:
```csharp
<uses-permission
	android:name="android.permission.ACCESS_COARSE_LOCATION"
	android:maxSdkVersion="22" />
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
```
And add the following Nuget package:

- Xamarin.GooglePlayServices.Location (min version: 57.1104.0-beta1)

The SDK creates a system notification for every background recipe. On the notification tap, your launcher activity will start.
To learn how to deal with in-app content once the user taps on the notification, see this [section](in-app-content.md).

If you want to customize your notifications, see this [section](custom-bkg-notification.md).

___
**WARNING**: If you experience build or runtime problems with google play services components, make sure you are not including multiple versions of the google play services.
NearIT includes the 11.4.0 version. Conflicting play services version may result in compile-time and run-time errors.
