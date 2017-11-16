# Location Based Notifications (iOS)

Before you start:

* You must add the `NSLocationAlwaysUsageDescription` and  `NSLocationWhenInUseUsageDescription` in the project *Info.plist* under the section `Source`. If your device has iOS 11, you must also add the `NSLocationAlwaysAndWhenInUseUsageDescription`.
* You will need to get `Always authorization`  from an instance of  `CLLocationManager`
___
When you want to start the radar for **geofences and beacons**, call the ```start``` method.
<br>Typically, you would start the radar right after you get authorization:

```csharp
LocationManager = new CLLocationManager();

LocationManager.AuthorizationChanged += (s, e) =>
{
    if (e.Status == CLAuthorizationStatus.AuthorizedAlways)
        NITManager.DefaultManager.Start();
    else
        NITManager.DefaultManager.Stop();
};

LocationManager.RequestAlwaysAuthorization();
```

To learn how to deal with in-app content see this [section](handle-content.md).
