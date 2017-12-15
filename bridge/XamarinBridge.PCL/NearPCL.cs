using System;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using Xamarin.Forms;

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

        public static void RefreshConfiguration()
        {
            DependencyService.Get<INearFunc>().RefreshConfiguration();
        }

        public static void SendTracking(XCTrackingInfo trackingInfo, string value)
        {
            DependencyService.Get<INearFunc>().SendTrack(trackingInfo, value);
        }

        public static void SendEvent(XCEvent ev)
        {
            DependencyService.Get<INearFunc>().SendEvent(ev);
        }

        public static void GetCoupon()
        {
            DependencyService.Get<INearFunc>().GetCoupon();
        }

        public static void SetUserData(string key, string value)
        {
            DependencyService.Get<INearFunc>().SetUserData(key, value);
        }

        public static void GetProfileId()
        {
            DependencyService.Get<INearFunc>().GetProfileId();
        }

        public static void SetProfileId(string profile)
        {
            DependencyService.Get<INearFunc>().SetProfileId(profile);
        }

        public static void ResetProfileId()
        {
            DependencyService.Get<INearFunc>().ResetProfileId();
        }

        public static void OptOut()
        {
            DependencyService.Get<INearFunc>().OptOut();
        }

        public static void ProcessCustomTrigger(string key)
        {
            DependencyService.Get<INearFunc>().ProcessCustomTrigger(key);
        }

    }
}
