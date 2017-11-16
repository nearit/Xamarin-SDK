# Setup (iOS) #

**NOTE** Make sure that your are using Visual Studio 7.2+

To install NearIT for your iOS component, click on “**Project>Add NuGet Packages**”, find and add the following NuGet package:
```
- Xamarin.NearIT.iOS
```

In the `FinishedLaunching` method of your **AppDelegate** class, set the API token to the SDK

```csharp
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    NITManager.SetupWithApiKey("<your API token here>");
    ...
}
```

You can find the API key on <a href="https://go.nearit.com/" target="_blank">**NearIT web interface**</a>, under the "**Settings>SDK Integration**" section.


## Manual Configuration Refresh ##

The SDK **initialization is done automatically** and handles the task of syncing the recipes with our servers when your app starts up.
<br>However, if you need to sync the recipes configuration more often, you can call this method:

```
NITManager.DefaultManager.RefreshConfigWithCompletionHandler((error) => {
...
});
```

If the method has succeeded, *error* is **null**.

