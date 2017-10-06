# Handle In-app content (iOS)

If you followed the instruction inside [Notification Setup](setup-notifications.md) closely, every tap on notification should be leading to a "**handleNearContent**" method.<br>
You should implement your handleNearContent method, inside it, you would typically check the content type and handle presentation.

<br>In the next chapter you will find a reference for any NearIT content.


## Recipe and content objects

When `eventWithContent` gets called or inside `processRecipe` callback you will obtain the content and the tracking info as arguments. 

Content can have several class types:

- `NITSimpleNotification` instance representing the simple notification
- `NITContent` instance representing the rich content if any
- `NITCustomJSON` instance representing the custom object if any
- `NITCoupon` instance representig the coupon if any
- `NITFeedback` instance representing the feedback request if any

## Content classes

- `NITSimpleNotification` for the simple notification, with the following attributes:
    - `message` returns the notification message

- `NITContent` for the notification with content, with the following attributes:
    - `content` returns the text content, without processing the html
    - `videoLink` returns the video link
    - `images` returns a list of *Image* object containing the source links for the images
    - `upload` returns an *Upload* object containing a link to a file uploaded on NearIT if any
    - `audio` returns an *Audio* object containing a link to an audio file uploaded on NearIT if any
    
- `NITFeedback` with the following getters:
    - `question` returns the feedback request string
To give a feedback call this method:

<div class="code-swift">
// rating must be an integer between 0 and 5, and you can set a comment string.
manager.sendEvent(feedbackEvent, completionHandler: { (error) in
    ...
})
</div>
<div class="code-objc">
// rating must be an integer between 0 and 5, and you can set a comment string.
[manager sendEventWithEvent:event completionHandler:^(NSError * _Nullable error) {
    ...
}];
</div>

    
- `NITCoupon` with the following getters:
    - `name` returns the coupon name
    - `couponDescription` returns the description
    - `value` returns the value string
    - `expires` returns the expiring date
    - `redeemable` returns the redeemable date, it's a start date of when you can reedem the coupon
    - `icon` returns an *Image* object containing the source links for the icon
    - `claims` returns a list of *NITClaim* which are the actual instances for the current profile
    - a `NITClaim` is composed by:
        - `serialNumber` the unique number assigned to the coupon instance
        - `claimed` a date representing when the coupon has been claimed
        - `redeemed` a date representing when the coupon has ben used

    
- `NITCustomJSON` with the following getters:
    - `content` returns the json content as a *[String: AnyObject]* (*[NSString**, id] in Objective-C)

## Fetch current user coupon

We handle the complete emission and redemption coupon cycle in our platform, and we deliver a coupon content only when a coupon is emitted (you will not be notified of recipes when a profile has already received the coupon, even if the coupon is still valid).
You can ask the library to fetch the list of all the user current coupons with the method:

<div class="code-swift">
NITManager.DefaultManager.CouponsWithCompletionHandler((coupons, error) => {
    ...      
});
</div>
<div class="code-objc">
[manager couponsWithCompletionHandler:^(NSArray<NITCoupon *> *coupons, NSError *error) {
    //Put your code here
}];
</div>



The method will also return already redeemed coupons so you get to decide to filter them if necessary.
