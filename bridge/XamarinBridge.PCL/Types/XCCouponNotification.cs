﻿using System;
namespace XamarinBridge.PCL.Types
{
    public class XCCouponNotification
    {
        public string NotificationMessage;
        public string Description;
        public string Value;
        public string ExpiresAt;
        public string RedeemableFrom;
        public XCImageSet IconSet;
        public string Id;
        public string Serial;
        public string ClaimedAt;
        public string RedeemedAt;

        public XCCouponNotification()
        {
        }
    }
}
