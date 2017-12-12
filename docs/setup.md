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

## Portable Class Library

Implement `IContentManager` interface that allows you to manage the notification using a common type.

**Remember** to call the method `NearPCL.SetContentManager(this);` in the constructor of the class where you implement this interface.
