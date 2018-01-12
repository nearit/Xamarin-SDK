using System;
using XamarinBridge.PCL.Types;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;
using IT.Near.Sdk.Trackings;

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
            native.t
            native.Id = xfeed.Id;

            return native;
        }

        public static XCFeedbackNotification GetCommonType(Feedback native)
        {
            XCFeedbackNotification xfeed = new XCFeedbackNotification();

            xfeed.NotificationMessage = native.NotificationMessage;
            xfeed.Question = native.Question;
            xfeed.RecipeId = native.RecipeId;
            xfeed.Id = native.Id;

            return xfeed;
        }
    }
}
