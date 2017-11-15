using System;
using NearIT;

namespace NearBridge.Parser
{
    public class HandleNearContent
    {
        public static void HandleContent(Object content, IContentsListener listener)
        {
            if (content.GetType() == typeof(NITContent))
            {
                NITContent cont = (NITContent)content;
                listener.GotContentNotification(cont);
            }
            else if (content.GetType() == typeof(NITSimpleNotification))
            {
                NITSimpleNotification cont = (NITSimpleNotification)content;
                listener.GotSimpleNotification(cont);
            }
            else if (content.GetType() == typeof(NITCoupon))
            {
                NITCoupon cont = (NITCoupon)content;
                listener.GotCouponNotification(cont);
            }
            else if (content.GetType() == typeof(NITCustomJSON))
            {
                NITCustomJSON cont = (NITCustomJSON)content;
                listener.GotCustomJSONNotification(cont);
            }
            else if (content.GetType() == typeof(NITFeedback))
            {
                NITFeedback cont = (NITFeedback)content;
                listener.GotFeedbackNotification(cont);
            }
        }
    }
}
