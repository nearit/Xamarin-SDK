using System;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using Xamarin.Forms;
using System.Collections.Generic;

namespace XamarinBridge.PCL
{
    public static class Recipe
    {
        public static readonly string RECEIVED = "notified";
        public static readonly string OPENED = "engaged";
    }

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

        public static void GetNotificationHistory(Action<IList<XCHistoryItem>> OnNotificationHistory, Action<String> OnNotificationHistoryError)
        {
            DependencyService.Get<INearFunc>().GetNotificationHistoryFromPCL(OnNotificationHistory, OnNotificationHistoryError);
        }

        public static void SetNotificationHistoryUpdateListener(INotificationHistoryUpdateListener listener)
        {
            DependencyService.Get<INearFunc>().SetNotificationUpdateListener(listener);
        }

        public static void RemoveNotificationUpdateListener(INotificationHistoryUpdateListener listener)
        {
            DependencyService.Get<INearFunc>().RemoveNotificationUpdateListener(listener);
        }

        public static void SetUserData(string key, string value)
        {
            DependencyService.Get<INearFunc>().SetUserData(key, value);
        }

        public static void SetUserData(string key, Dictionary<string, bool> values)
        {
            DependencyService.Get<INearFunc>().SetUserData(key, values);
        }

        public static void GetUserData(Action<IDictionary<string, object>> OnUserData, Action<string> OnUserDataError)
        {
            DependencyService.Get<INearFunc>().GetUserData(OnUserData, OnUserDataError);
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

        [Obsolete("Please use TriggerInAppEvent instead.")]
        public static void ProcessCustomTrigger(string key)
        {
            TriggerInAppEvent(key);
        }

        public static void TriggerInAppEvent(string key)
        {
            DependencyService.Get<INearFunc>().TriggerInAppEvent(key);
        }

    }
}
