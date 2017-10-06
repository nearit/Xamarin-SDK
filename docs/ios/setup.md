# Setup (iOS) #

In the `FinishedLaunching(UIApplication application, NSDictionary launchOptions)` method of your AppDelegate class, set the API token to the SDK a String


<div class="code-swift">
public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
    ...
    NITManager.SetupWithApiKey("&lt;your API token here&gt;");
    ...
}
</div>
<div class="code-objc">
#import &lt;NearITSDK/NearITSDK.h&gt;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    ...
    [NITManager setupWithApiKey:@"&lt;your API token here&gt;"];
    NITManager *manager = [NITManager defaultManager];
    ...
}
</div>

You can find the API key on <a href="https://go.nearit.com/" target="_blank">**NearIT web interface**</a>, under the "**Settings> SDK Integration**" section.

<br>
##Manual Configuration Refresh##

The SDK **initialization is done automatically** and handles the task of syncing the recipes with our servers when your app starts up.
<br>However, if you need to sync the recipes configuration more often, you can call this method:

<div class="code-swift">
NITManager.DefaultManager.RefreshConfigWithCompletionHandler((error) => {
    ...                
});
</div>
<div class="code-objc">
NITManager.DefaultManager.RefreshConfigWithCompletionHandler((error) => {
    ...                
});
</div>

If the refreshConfig has succeeded, 'error' is nil.
