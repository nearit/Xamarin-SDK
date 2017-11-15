﻿using System;
using IT.Near.Sdk.Reactions.Couponplugin.Model;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterCoupon
    {
        public static XCCouponNotification GetCommonType(Coupon CouponNotification)
        {
            XCCouponNotification XCoupon = new XCCouponNotification();

            XCoupon.NotificationMessage = CouponNotification.NotificationMessage;
            XCoupon.Description = CouponNotification.Description;
            XCoupon.Value = CouponNotification.Value;
            XCoupon.ExpiresAt = CouponNotification.ExpiresAt;
            XCoupon.ReedemableFrom = CouponNotification.RedeemableFrom;
            XCoupon.IconSet.FullSize = CouponNotification.IconSet.FullSize;
            XCoupon.IconSet.SmallSize = CouponNotification.IconSet.SmallSize;
            XCoupon.Id = CouponNotification.Id;

            return XCoupon;
        }
    }
}
