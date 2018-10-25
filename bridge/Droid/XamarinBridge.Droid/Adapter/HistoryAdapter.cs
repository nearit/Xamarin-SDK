using System;
using IT.Near.Sdk.Reactions.Contentplugin.Model;
using IT.Near.Sdk.Reactions.Couponplugin.Model;
using IT.Near.Sdk.Reactions.Customjsonplugin.Model;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;
using IT.Near.Sdk.Reactions.Simplenotificationplugin.Model;
using IT.Near.Sdk.Recipes.Inbox.Model;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.Droid.Adapter
{
    public class HistoryAdapter
    {
        public static XCHistoryItem GetCommonType(HistoryItem nativeItem) {
            XCHistoryItem item = new XCHistoryItem();
            XCTrackingInfo trackingInfo = new XCTrackingInfo();

            trackingInfo.RecipeId = nativeItem.TrackingInfo.RecipeId;
            trackingInfo.extras = AdapterUtils.From(nativeItem.TrackingInfo.Metadata);

            item.read = nativeItem.Read;
            item.timestamp = nativeItem.Timestamp;

            item.trackingInfo = trackingInfo;

            if (nativeItem.Reaction is SimpleNotification simple)
            {
                item.reaction = AdapterSimple.GetCommonType(simple);
            }
            else if (nativeItem.Reaction is Content content)
            {
                item.reaction = AdapterContent.GetCommonType(content);
            }
            else if (nativeItem.Reaction is Feedback feedback)
            {
                item.reaction = AdapterFeedback.GetCommonType(feedback);
            }
            else if (nativeItem.Reaction is Coupon coupon)
            {
                item.reaction = AdapterCoupon.GetCommonType(coupon);
            }
            else if (nativeItem.Reaction is CustomJSON customJSON)
            {
                item.reaction = AdapterCustom.GetCommonType(customJSON);
            }

            return item;
        }
    }
}
