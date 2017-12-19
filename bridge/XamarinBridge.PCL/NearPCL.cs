using System;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using Xamarin.Forms;
using System.Collections.Generic;

namespace XamarinBridge.PCL
{
    public class NearPCL
    {
        private static IContentManager contentManager;

        public static void SetContentManager(IContentManager cont)
        {
            contentManager = cont;
        }

        public static IContentManager GetContentManager()
        {
            return contentManager;
        }

        public static void SendTracking(XCTrackingInfo trackingInfo, string value)
        {
            DependencyService.Get<INearFunc>().SendTrack(trackingInfo, value);
        }

        public static void SendEvent(XCEvent ev)
        {
            DependencyService.Get<INearFunc>().SendEvent(ev);
        }

        public static void GetCoupons(Action<IList<XCCouponNotification>> OnCouponsDownloaded, Action<String> OnCouponDownloadError)
        {
            DependencyService.Get<INearFunc>().GetCouponsFromPCL(OnCouponsDownloaded, OnCouponDownloadError);
        }

        public static void SetUserData(string key, string value)
        {
            DependencyService.Get<INearFunc>().SetUserData(key, value);
        }

        public static void GetProfileId(Action<String> OnProfile, Action<String> OnError)
        {
            DependencyService.Get<INearFunc>().GetProfileIdFromPCL(OnProfile, OnError);
        }

        public static void SetProfileId(string profile)
        {
            DependencyService.Get<INearFunc>().SetProfileId(profile);
        }

        public static void ResetProfileId(Action<String> OnProfile, Action<String> OnError)
        {
            DependencyService.Get<INearFunc>().ResetProfileIdFromPCL(OnProfile, OnError);
        }

        public static void OptOut(Action<int> OnSuccess, Action<String> OnFailure)
        {
            DependencyService.Get<INearFunc>().OptOutFromPCL(OnSuccess, OnFailure);
        }

        public static void ProcessCustomTrigger(string key)
        {
            DependencyService.Get<INearFunc>().ProcessCustomTrigger(key);
        }

    }
}
