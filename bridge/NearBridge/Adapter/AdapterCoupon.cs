using System;
using NearIT;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterCoupon
    {
        public static XCCouponNotification GetCommonType(NITCoupon CouponNotification)
        {
            XCCouponNotification XCoupon = new XCCouponNotification();

            XCoupon.NotificationMessage = CouponNotification.NotificationMessage;
            XCoupon.Description = CouponNotification.Description;
            XCoupon.Value = CouponNotification.Value;
            XCoupon.ExpiresAt = CouponNotification.ExpiresAt;
            XCoupon.ReedemableFrom = CouponNotification.RedeemableFrom;
            if (CouponNotification.Icon != null)
            {
                XCoupon.IconSet = new XCImageSet();
                XCoupon.IconSet.FullSize = CouponNotification.Icon.Url.AbsoluteString;
                XCoupon.IconSet.SmallSize = CouponNotification.Icon.SmallSizeURL.AbsoluteString;
            }
            XCoupon.Id = CouponNotification.ID;

            return XCoupon;
        }
    }
}
