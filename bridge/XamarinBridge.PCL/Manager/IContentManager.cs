using System;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.PCL.Manager
{
    public interface IContentManager
    {
        void GotXContentNotification(XCContentNotification notification);

        void GotXCouponNotification(XCCouponNotification notification);

        void GotXCustomJSONNotification(XCCustomJSONNotification notification);

        void GotXSimpleNotification(XCSimpleNotification notification);

        void GotXFeedbackNotification(XCFeedbackNotification notification); 
    }
}
