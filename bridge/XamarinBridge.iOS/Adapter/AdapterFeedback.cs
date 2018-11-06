using System;
using XamarinBridge.PCL.Types;
using NearIT;
using Foundation;
using System.Collections.Generic;
using System.Linq;

namespace XamarinBridge.iOS.Adapter
{
    public class AdapterFeedback
    {
        public static NITFeedback GetNative(XCFeedbackNotification xfeed)
        {
            NITFeedback native = new NITFeedback();
            native.TrackingInfo = NITTrackingInfo.TrackingInfoFromRecipeId(xfeed.TrackingInfo.RecipeId,
                                                                           AdapterUtils.From(xfeed.TrackingInfo.extras));

            native.NotificationMessage = xfeed.NotificationMessage;
            native.Question = xfeed.Question;
            native.ID = xfeed.Id;

            return native;
        }

        public static XCFeedbackNotification GetCommonType(NITFeedback native)
        {
            XCFeedbackNotification xfeed = new XCFeedbackNotification();
            xfeed.TrackingInfo = new XCTrackingInfo();

            xfeed.NotificationMessage = native.NotificationMessage;
            xfeed.Question = native.Question;
            // xfeed.RecipeId = native.RecipeId;
            xfeed.TrackingInfo.extras = AdapterUtils.FromNS(native.TrackingInfo.ExtrasDictionary());
            xfeed.TrackingInfo.RecipeId = native.TrackingInfo.RecipeId;
            xfeed.Id = native.ID;

            return xfeed;
        }


    }
}
