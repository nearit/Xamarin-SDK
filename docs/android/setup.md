# Setup (Android) #

**NOTE** Make sure that your are using Visual Studio 7.2+ and **target framework** 8.0

To install NearIT for your Android component, click on “**Project>Add NuGet Packages**”, make sure you have "**Show pre-release packages**" option checked, find and add the following NuGet package:
```
- Xamarin.NearIT.Android
```

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
<receiver android:name="com.google.firebase.iid.FirebaseInstanceIdReceiver" android:exported="true" 							android:permission="com.google.android.c2dm.permission.SEND">
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
