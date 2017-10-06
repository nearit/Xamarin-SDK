# Handle In-app Content (Android)

NearIT takes care of delivering content at the right time, you will just need to handle content presentation.

## Foreground vs Background

Recipes either deliver content in background or in foreground but not both. Check this table to see how you will be notified.

| Type of trigger                  | Delivery           |
|----------------------------------|--------------------|
| Push (immediate or scheduled)    | Background intent  |
| Enter and Exit on geofences      | Background intent  |
| Enter and Exit on beacon regions | Background intent  |
| Enter in a specific beacon range | Proximity listener (foreground) |

## Foreground Content

To receive foreground content (e.g. ranging recipes) set a proximity listener with the method
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

## Background Content

Any background working trigger will deliver the actual content through an intent that will call your app launcher activity and carry some extras.
To extract the content from an intent use the utility method:
```csharp
NearUtils.ParseCoreContents(intent, _coreContentListener);
```
If you want to just check if the intent carries NearIT content, without having to eventually handle the actual content, use this method
```csharp
bool hasNearContent = NearUtils.CarriesNearItContent(intent);
```

If you want to customize the behavior of background notification see [this page](custom-bkg-notification.md)

## Trackings

NearIT analytics on recipes are built from trackings describing the status of user engagement with a recipe. The two recipe states are "Notified" and "Engaged" to represent a recipe delivered to the user and a recipe that the user responded to.
Built-in background recipes track themselves as notified and engaged.

Foreground recipes don't have automatic tracking. You need to track both the "Notified" and the "Engaged" statuses when it's the best appropriate for you scenario.
```java
NearItManager.Instance.SendTracking(trackinginfo, Recipe.NotifiedStatus);
// and
NearItManager.Instance.SendTracking(trackinginfo, Recipe.EngagedStatus);
// or
NearItManager.Instance.SendTracking(trackinginfo, "custome")
```
The recipe cooldown feature uses tracking calls to hook its functionality, so failing to properly track user interactions will result in the cooldown not being applied.

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
    - `Name` returns the coupon name
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

## Fetch current user coupon

We handle the complete emission and redemption coupon cycle in our platform, and we deliver a coupon content only when a coupon is emitted (you will not be notified of recipes when a profile has already received the coupon, even if the coupon is still valid).
You can ask the library to fetch the list of all the user current coupons with the method:
```java
NearItManager.Instance.GetCoupons(_couponlistener);
```
The method will also return already redeemed coupons so you get to decide to filter them if necessary.
