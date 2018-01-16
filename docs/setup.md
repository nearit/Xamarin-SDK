# Setup #

To install NearIT, click on “**Project>Add NuGet Packages**”, make sure you have "**Show pre-release packages**" option checked, find and add the following NuGet packages:

```
- Xamarin.NearIT.PCL        (in your common fragment)
- Xamarin.NearIT.iOS        (in your native iOS fragment)
- Xamarin.NearIT.Android    (in your native Android fragment)
```
**NOTE** Make sure that your Android **target framework** is set to 8.0
<br>
## Android

In your *AndroidManifest.xml*, under the *Source* tab, add the following permission:
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
<receiver android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver" android:exported="true" android:permission="com.google.android.c2dm.permission.SEND">
    <intent-filter>
        <action android:name="com.google.android.c2dm.intent.RECEIVE" />
        <action android:name="com.google.android.c2dm.intent.REGISTRATION" />
        <category android:name="${applicationId}" />
    </intent-filter>
</receiver>
```

**WARNING**
In both **Debug** and **Release** mode, make sure you have **Multi-Dex** enabled in the “**Options>Android Build**” settings.

In your code you can access the `NearItManager` instance with:
```csharp
NearItManager.Instance
```
<br>

## iOS

In the `FinishedLaunching` method of your **AppDelegate** class, set the API token to the SDK and also the framework name.

```csharp
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    NITManager.SetupWithApiKey("<your API token here>");
    NITManager.SetFrameworkName("xamarin");
    ...
}
```
**Instead** if you have created a *Keys.plist* file where you define a variable **apiKey** and after set **Build Action>BundleResource**, you can call inside `FinishedLaunching` method of your **AppDelegate** class this method:
```
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    NearBridgeiOS.SetApiKey();
    ...
}
```
This method automatically set the framework name.
<br>

To keep your app up to date even if a user is not using the app, you have to call the sdk method developed to support the iOS feature called **background fetch**.
```
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(MINIMUM_BACKGROUND_FETCH_INTERVAL);    //MINIMUM_BACKGROUND_FETCH_INTERVAL usually set to 900
    ...
}

public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
{
    NITManager.DefaultManager.PerformFetchWithCompletionHandler(application, (UIBackgroundFetchResult obj) =>
    {
        completionHandler(obj);
    });
}
```

<br><br>
You can find the API key on <a href="https://go.nearit.com/" target="_blank">**NearIT web interface**</a>, under the "**Settings>SDK Integration**" section.
<br><br>

## Portable Class Library

Implement `IContentManager` interface that allows you to manage the notification using a common type.

**Remember** to call the method `NearPCL.SetContentManager(this);` in the constructor of the class where you implement this interface.
