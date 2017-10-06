# Push Notifications (Android)

To enable push notification you will need to integrate **Google Firebase** in your app:
<br><br>
**1.** If you don't already have a **Firebase project**, create one at [Google Firebase Console](https://console.firebase.google.com/).<br>
Inside the project, select **"Add Firebase to your Android app"** (make sure to enter the right package name of your app).
<br><br>
**2.** Download `google-services.json` file to your computer and add the file in your Android component.
![google-services.json](push_help/google_services_json.png "")
<br><br>
Select properties on that file and specify the following Build Action: GoogleServicesJson. If that Build Action is not available right after adding the file is added, close and re-open the solution.
**3.** Copy your project ***FCM Cloud Messaging Server Key*** from [Google Firebase Console](https://console.firebase.google.com/)
(See the screenshot below and make sure to use the right api key)
![fcmkey](push_help/fcmkeylocation.png "")

<br>
**4.** Add this Nuget package:

- Xamarin.Firebase.Messaging (min version: 57.1104.0-beta1)
<br>

**5.** Open [NearIT](https://go.nearit.com), select your app and navigate to **“Settings > Push Settings”**.
Paste your project FCM Key under the **“Setup Android push notifications”** block.
![nearitsettings](push_help/fcm_upload.gif "")
<br><br>
___
**WARNING**: Do not follow any further FCM-specific instructions: we automatically handle all the other part of the process inside the SDK code.
___


<br>
The SDK creates a system notification for every push recipe it receives.
On the notification tap, your launcher activity will start.
To learn how to deal with in-app content once the user taps on the notification, see this [section](in-app-content.md).

If you want to customize your notifications, see this [section](custom-bkg-notification.md).



___
**WARNING**: If you experience build or runtime problems with google play services components, make sure you are not including multiple versions of the google play services.
NearIT includes the 11.4.0 version. Conflicting play services version may result in compile-time and run-time errors.
