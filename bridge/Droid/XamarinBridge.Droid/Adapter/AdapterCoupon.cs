using System;
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
            if (CouponNotification.IconSet != null)
            {
                XCoupon.IconSet = new XCImageSet();
                XCoupon.IconSet.FullSize = CouponNotification.IconSet.FullSize;
                XCoupon.IconSet.SmallSize = CouponNotification.IconSet.SmallSize;
            }
            XCoupon.Id = CouponNotification.Id;
            XCoupon.Serial = CouponNotification.Serial;
            XCoupon.ClaimedAt = CouponNotification.ClaimedAt;
            XCoupon.ReedemedAt = CouponNotification.RedeemedAt;

            return XCoupon;
        }
    }
}
