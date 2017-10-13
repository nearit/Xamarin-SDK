# Setup (Android)#

**NOTE** Make sure that your are using Visual Studio 7.2+

To install NearIT for your Android component, add all the *.dll* files from the `dlls` folder on our <a href="https://github.com/nearit/Xamarin-SDK/" target="_blank">GitHub repository</a>. To do that, right-click on “**References>Edit References**”. From the window, select the tab “**References**” and then “**Browse**” to select the *.dll* files.

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

Click on **Project>Add NuGet Packages** and find and add the following NuGet package:
```
- Xamarin.Android.Support.Compat v7 (min version: 26.0.2-rc1)

- Xamarin.Firebase.Messaging (min version: 57.1104.0-beta1)

- Xamarin.GooglePlayServices.Location (min version: 57.1104.0-beta1)
```

In your code you can access the `NearItManager` instance with:
```csharp
NearItManager.Instance
```
