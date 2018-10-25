using System;
using Foundation;
using NearIT;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.iOS.Adapter
{
    public class HistoryAdapter
    {
        public static XCHistoryItem GetCommonType(NITHistoryItem nativeItem) {
            XCHistoryItem item = new XCHistoryItem();
            item.read = nativeItem.Read;
            item.timestamp = nativeItem.Timestamp;
            XCTrackingInfo trackingInfo = new XCTrackingInfo();
            trackingInfo.RecipeId = nativeItem.TrackingInfo.RecipeId;
            trackingInfo.extras = AdapterUtils.FromNS(
                nativeItem.TrackingInfo.ExtrasDictionary() as NSDictionary<NSString, NSObject>
            );
            item.trackingInfo = trackingInfo;

            if (nativeItem.ReactionBundle is NITSimpleNotification simple) {
                item.reaction = AdapterSimple.GetCommonType(simple);
            } else if (nativeItem.ReactionBundle is NITContent content) {
                item.reaction = AdapterContent.GetCommonType(content);
            } else if (nativeItem.ReactionBundle is NITFeedback feedback) {
                item.reaction = AdapterFeedback.GetCommonType(feedback);
            } else if (nativeItem.ReactionBundle is NITCoupon coupon) {
                item.reaction = AdapterCoupon.GetCommonType(coupon);
            } else if (nativeItem.ReactionBundle is NITCustomJSON customJSON) {
                item.reaction = AdapterCustom.GetCommonType(customJSON);
            }

            return item;
        }
    }
}
