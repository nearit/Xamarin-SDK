using System;
using XamarinBridge.PCL.Types;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;
using IT.Near.Sdk.Trackings;
using System.Collections.Generic;
using System.Collections;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterFeedback
    {
        public static Feedback GetNative(XCFeedbackNotification xfeed)
        {
            Feedback native = new Feedback();
            TrackingInfo nativeTrack = new TrackingInfo();

            nativeTrack.RecipeId = xfeed.TrackingInfo.RecipeId;
            nativeTrack.Metadata = xfeed.TrackingInfo.extras;

            native.NotificationMessage = xfeed.NotificationMessage;
            native.Question = xfeed.Question;
            native.RecipeId = xfeed.RecipeId;
            native.TrackingInfo = nativeTrack;
            native.Id = xfeed.Id;

            return native;
        }

        public static XCFeedbackNotification GetCommonType(Feedback native)
        {
            XCFeedbackNotification xfeed = new XCFeedbackNotification();
            xfeed.TrackingInfo = new XCTrackingInfo();

            xfeed.NotificationMessage = native.NotificationMessage;
            xfeed.Question = native.Question;
            xfeed.TrackingInfo.RecipeId = native.TrackingInfo.RecipeId;
            xfeed.TrackingInfo.extras = AdapterUtils.From(native.TrackingInfo.Metadata);
            xfeed.Id = native.Id;

            return xfeed;
        }


    }
}
