using System;
using XamarinBridge.PCL.Types;
using IT.Near.Sdk.Reactions.Feedbackplugin;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterFeedback
    {
        public static Feedback GetNativeFeeback(Feedback native, XCFeedbackNotification xfeed)
        {
            native.NotificationMessage = xfeed.NotificationMessage;
            native.Question = xfeed.Question;
            native.RecipeId = xfeed.RecipeId;

            return native;   
        }

        public static XCFeedbackNotification GetXCFeedback(Feedback native, XCFeedbackNotification xfeed)
        {
            xfeed.NotificationMessage = native.NotificationMessage;
            xfeed.Question = native.Question;
            xfeed.RecipeId = native.RecipeId;

            return xfeed;
        }
    }
}
