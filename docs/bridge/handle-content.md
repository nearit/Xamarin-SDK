# Handle In-app Content (Bridge)

Every tap on notification should be leading to the implementation of the "**IContentManager**" interface.<br>

<br>In the next chapter you will find a reference for any NearITBridge content.


## Recipe and content objects

When `eventWithContent` gets called or inside `processRecipe` callback you will obtain the content and the tracking info as arguments. 

In your native fragment, content can have several class types:

- `XCSimpleNotification` instance representing the simple notification
- `XCContentNotification` instance representing the rich content if any
- `XCCustomJSONNotification` instance representing the custom object if any
- `XCCouponNotification` instance representig the coupon if any
- `XCFeedbackNotification` instance representing the feedback request if any

   
## Bridge content classes
- `XCSimpleNotification` for the simple notification, with the following attributes:
    - `NotificationMessage` returns the notification message

- `XCContentNotification` for the notification with content, with the following attributes:
   - `Title` returns the notification title
    - `Content` returns the text content, without processing the html
    - `ImageLink` returns the link of the image object
    - `Cta` returns a `ContentLink` with a label and url fields

- `XCFeedbackNotification` with the following getters:
    - `Question` returns the feedback request string
    - `RecipeId` returns the recipe id
To give a feedback call this method:

```csharp
// rating must be an integer between 0 and 5, and you can set a comment string.
NearPCL.SendEvent(ev);
```


- `XCCouponNotification` with the following getters:
    - `Description` returns the description
    - `Value` returns the value string
    - `ExpiresAt` returns the expiring date
    - `RedeemableFrom` returns the redeemable date, it's a start date of when you can reedem the coupon
    - `IconSet` returns an *Image* object containing the source links for the icon

- `XCCustomJSONNotification` with the following getters:
    - `Content` returns the json content

