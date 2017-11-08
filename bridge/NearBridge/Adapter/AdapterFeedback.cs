using System;
using XamarinBridge.PCL.Types;
using NearIT;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterFeedback
    {
        public static NITFeedback GetNativeFeeback(NITFeedback native, XCFeedbackNotification xfeed)
        {
            native.NotificationMessage = xfeed.NotificationMessage;
            native.Question = xfeed.Question;
            native.RecipeId = xfeed.RecipeId;

            return native;   
        }

        public static XCFeedbackNotification GetXCFeedback(NITFeedback native, XCFeedbackNotification xfeed)
        {
            xfeed.NotificationMessage = native.NotificationMessage;
            xfeed.Question = native.Question;
            xfeed.RecipeId = native.RecipeId;

            return xfeed;
        }
    }
}
