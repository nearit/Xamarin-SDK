# Handle In-app Content (Android)

After an user **taps on a notification**, you will receive content through an intent to your app launcher activity. If you want to just check if the intent carries NearIT content use this method:
```csharp
bool hasNearContent = NearUtils.CarriesNearItContent(intent);
```
To extract the content from an intent use the utility method:
```csharp
NearUtils.ParseCoreContents(intent, _coreContentListener);
```

## Beacon Interaction Content
Beacon interaction (beacon ranging) is a peculiar trigger that works only when your app is in the foreground.
<br>
NearIT Android SDK will automatically show heads-up notifications.

<br>
If you need to disable the default behaviour, call this method in the **onCreate** method of your application:

```
NearItManager.Instance.DisableDefaultRangingNotifications();
```

And if you want to receive ranging contents and handle them manually, set a **proximity listener** with the method:

```csharp

ProximityListener _proximityListener;

protected override void OnCreate(Bundle savedInstanceState)
{
    ...
    NearItManager.Instance.AddProximityListener(_proximityListener);
    // remember to remove the listener when the object is being destroyed with
    // NearItManager.Instance.RemoveProximityListener(_proximityListener);
    ...
}

internal class ProximityListener : Java.Lang.Object, IProximityListener
    {
        public void ForegroundEvent(IParcelable content, TrackingInfo trackinginfo)
        {
            // you can parse your content with
            NearUtils.ParseCoreContents(content, _coreContentListener);
        }
    }
```

## Trackings

NearIT allows to track user engagement events on recipes. Any recipe has at least two default events:

  - **Notified**: the user *received* a notification
  - **Engaged**: the user *tapped* on the notification

Usually the SDK tracks those events automatically, but if you write custom code to show notification or content (i.e. to receive Beacon interaction content) please make sure that at least the "**notified**" event is tracked.
<br>**WARNING** Failing in tracking this event cause some NearIT features to not work.


You can track **default or custom events** using the "**sendTracking**" method:
```
// notified - notification received
NearItManager.Instance.SendTracking(trackinginfo, Recipe.NotifiedStatus);
// engaged - notification tapped
NearItManager.Instance.SendTracking(trackinginfo, Recipe.EngagedStatus);
// custom recipe event
NearItManager.Instance.SendTracking(trackinginfo, "my awesome custom event")
```

## Content Objects

For each callback method of the *ICoreContentsListener* you will receive a different content object.
Every object has a `NotificationMessage` and a `Id` public getters.
Here are the public getters for every other one:

- `SimpleNotification` with the following fields:
    - `Message` returns the notification message (it is the same as `NotificationMessage`)

- `Content` for the notification with content, with the following getters and fields:
    - `Title` returns the content title
    - `ContentString` returns the text content
    - `Cta` returns a `ContentLink`  with a label and url fields
    - `ImageLink` returns an `ImageSet` object containing the links for the image

- `Feedback` with the following getters and fields:
    - `Question` returns the feedback request string
    - `RecipeId` returns the recipeId associated with the feedback (you'll need it for answer it)
To give a feedback call this method:
```csharp
// rating must be an integer between 1 and 5, and you can set a comment string.
NearItManager.Instance.SendEvent(new FeedbackEvent(feedback, 5, "awesome"));
// the sendEvent method is available in 2 variants: with or without explicit callback handler. Example:
NearItManager.Instance.SendEvent(new FeedbackEvent(...), _callbackHandler);
```

- `Coupon` with the following getters and fields:
    - `Title` returns the coupon title
    - `Description` returns the coupon description
    - `Value` returns the value string
    - `ExpiresAt` returns the expiring date (as a string), might be null
    - `ExpiresAtDate` returns a the expiring Date object. Since coupon validity period is timezone related, consider showing the time of day.
    - `RedeemableFrom` returns the validity start date (as a string), might be null
    - `RedeemableFromDate` returns the validity start Date object. Since coupon validity period is timezone related, consider showing the time of day.
    - `IconSet` returns an *ImageSet* object containing the source links for the icon
    - `Serial` returns the serial code of the single coupon as a string
    - `ClaimedAt` returns the claimed date (when the coupon was earned) of the coupon as a string
    - `ClaimedAtDate` returns the claimed Date object.
    - `RedeemedAt` returns the redeemed date (when the coupon was used) of the coupon as a string
    - `RedeemedAtDate` returns the redeemed Date object.

- `CustomJSON` with the following fields:
    - `Content` returns the json content as an *IDictionary*
    
**NOTE** Click here the [Handle In-app Content (Bridge)](../bridge/handle-content.md) of the Bridge.

## Fetch current user coupon

We handle the complete emission and redemption coupon cycle in our platform, and we deliver a coupon content only when a coupon is emitted (you will not be notified of recipes when a profile has already received the coupon, even if the coupon is still valid).
You can ask the library to fetch the list of all the user current coupons with the method:
```
NearBridgeDroid.GetCoupons(
                (couponList) => {
                // handle the List of coupons
            }, (error) => {
                // handle the error
            });
```

The method will also return already redeemed coupons so you get to decide to filter them if necessary.
