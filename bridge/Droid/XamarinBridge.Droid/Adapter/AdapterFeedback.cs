using System;
using XamarinBridge.PCL.Types;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterFeedback
    {
        public static Feedback GetNative(XCFeedbackNotification xfeed)
        {
            Feedback native = new Feedback();

            native.NotificationMessage = xfeed.NotificationMessage;
            native.Question = xfeed.Question;
            native.RecipeId = xfeed.RecipeId;
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
