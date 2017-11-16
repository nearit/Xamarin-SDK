# Setup (Bridge) #

This Bridge allows you to manage significant SDK's method directly in the common fragment of your project.

To install NearIT Bridge, click on “**Project>Add NuGet Packages**”, make sure you have "**Show pre-release packages**" option checked, find and add the following NuGet package in your shared fragment:
```
- Xamarin.NearIT.PCL
```

Then move to the native fragments, install its NuGet (you can find it in [Setup (iOS)](../ios/setup.md) or [Setup (Android)](../android/setup.md)) and set the Api Key.<br>

To set the Api Key of **iOS** you can follow the docs or (if you have created a *Keys.plist* file where you define a variable **apiKey**),  call inside `FinishedLaunching` method of your **AppDelegate** class this method:
```
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    NearBridgeiOS.SetApiKey();
    ...
}
```
<br>
Now you can implement `INearFunc` and  `IContentManager` interfaces.


##INearFunc Interface##

This interface allows to use the main methods:

`void RefreshConfiguration();`

`void SendTrack(XCTrackingInfo trackingInfo, string value);`

`void SendEvent(XCEvent ev);`

`void GetCoupon();`

`void SetUserData(string key, string value);`

`void GetProfileId();`

`void SetProfileId(string profile);`

`void ResetProfileId();`

`void OptOut();`

##IContentManager Interface##

This interface allows to manage the notification using a common type:

`void GotXContentNotification(XCContentNotification notification);`

`void GotXCouponNotification(XCCouponNotification notification);`

`void GotXCustomJSONNotification(XCCustomJSONNotification notification);`

`void GotXFeedbackNotification(XCFeedbackNotification notification);`

`void GotXSimpleNotification(XCSimpleNotification notification);`

<br>
**NOTE** The methods will be explain during the docs.
