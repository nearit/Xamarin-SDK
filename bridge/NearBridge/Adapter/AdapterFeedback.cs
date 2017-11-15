using System;
using XamarinBridge.PCL.Types;
using NearIT;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterFeedback
    {
        public static NITFeedback GetNative(XCFeedbackNotification xfeed)
        {
            NITFeedback native = new NITFeedback();

            native.NotificationMessage = xfeed.NotificationMessage;
            native.Question = xfeed.Question;
            native.RecipeId = xfeed.RecipeId;
            native.ID = xfeed.Id;

            return native;   
        }

        public static XCFeedbackNotification GetCommonType(NITFeedback native)
        {
            XCFeedbackNotification xfeed = new XCFeedbackNotification();

            xfeed.NotificationMessage = native.NotificationMessage;
            xfeed.Question = native.Question;
            xfeed.RecipeId = native.RecipeId;
            xfeed.Id = native.ID;

            return xfeed;
        }
    }
}
