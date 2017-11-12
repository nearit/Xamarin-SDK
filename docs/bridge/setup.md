# Setup (Bridge) #

This Bridge allows you to manage significant SDK's method directly in the common fragment of your project.

To install NearIT Bridge, click on “**Project>Add NuGet Packages**”, find and add the following NuGet package in your shared fragment:
```
- Xamarin.NearIT.Bridge
```
Then implement `INearFunc` and  `IContentManager` interfaces.


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
