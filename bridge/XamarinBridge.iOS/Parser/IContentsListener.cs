﻿using System;
using NearIT;

namespace XamarinBridge.iOS.Parser
{
    public interface IContentsListener
    {
        void GotContentNotification(NITContent content);

        void GotCouponNotification(NITCoupon content);

        void GotCustomJSONNotification(NITCustomJSON content);

        void GotSimpleNotification(NITSimpleNotification content);

        void GotFeedbackNotification(NITFeedback content); 
    }
}
