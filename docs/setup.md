# Setup #

To install NearIT, click on “**Project>Add NuGet Packages**”, make sure you have "**Show pre-release packages**" option checked, find and add the following NuGet packages:

```
- Xamarin.NearIT.PCL        (in your common fragment)
- Xamarin.NearIT.iOS        (in your native iOS fragment)
- Xamarin.NearIT.Android    (in your native Android fragment)
```
<br>
## Android

**NOTE** Make sure that your **target framework** is set to 8.0

In your *AndroidManifest.xml*, under the section *Source*, add the following permissions:
```xml
<uses-permission android:name="android.permission.INTERNET" />
```
Also, add your NearIT API-Key in the `application` element.
```xml
<meta-data android:name="near_api_key" android:value="REPLACE-WITH-YOUR-KEY" />
```
and the following code:
```xml
<receiver android:name="com.google.firebase.iid.FirebaseInstanceIdInternalReceiver" android:exported="false" />
<receiver android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver" android:exported="true"                             android:permission="com.google.android.c2dm.permission.SEND">
    <intent-filter>
        <action android:name="com.google.android.c2dm.intent.RECEIVE" />
        <action android:name="com.google.android.c2dm.intent.REGISTRATION" />
        <category android:name="${applicationId}" />
    </intent-filter>
</receiver>
```

**WARNING**
In either mode, **Debug** and **Release**, make sure you have enabled **Multi-Dex** mode in “**Options>Android Build**”.

In your code you can access the `NearItManager` instance with:
```csharp
NearItManager.Instance
```
<br>
## iOS

To install NearIT for your iOS component, click on “**Project>Add NuGet Packages**”, make sure you have "**Show pre-release packages**" option checked, find and add the following NuGet package:

In the `FinishedLaunching` method of your **AppDelegate** class, set the API token to the SDK

```csharp
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    NITManager.SetupWithApiKey("<your API token here>");
    ...
}
```
**Instead** if you have created a *Keys.plist* file where you define a variable **apiKey**, you can call inside `FinishedLaunching` method of your **AppDelegate** class this method:
```
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    NearBridgeiOS.SetApiKey();
    ...
}
```

You can find the API key on <a href="https://go.nearit.com/" target="_blank">**NearIT web interface**</a>, under the "**Settings>SDK Integration**" section.
<br><br>

## Manual Configuration Refresh ##

The SDK **initialization is done automatically** and handles the task of syncing the recipes with our servers when your app starts up.
<br>However, if you need to sync the recipes configuration more often, you can call this method:

### iOS
```
NITManager.DefaultManager.RefreshConfigWithCompletionHandler((error) => {
    ...
});
```

If the method has succeeded, *error* is **null**.

### Android
```
NearItManager.Instance.RefreshConfigs(<IRecipeRefreshListener>);
```
<br>
## Portable Class Library

Implement `INearFunc` and  `IContentManager` interfaces.


### INearFunc Interface

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

### IContentManager Interface

This interface allows to manage the notification using a common type:

`void GotXContentNotification(XCContentNotification notification);`

`void GotXCouponNotification(XCCouponNotification notification);`

`void GotXCustomJSONNotification(XCCustomJSONNotification notification);`

`void GotXFeedbackNotification(XCFeedbackNotification notification);`

`void GotXSimpleNotification(XCSimpleNotification notification);`

**Remember** to call the method `NearPCL.SetContentManager(this);` in the constructor of the class where you implement this interface. 
<br>
**NOTE** The methods will be explain during the docs.
