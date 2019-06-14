using System;
using System.Collections.Generic;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.PCL.Manager
{
    public interface INearFunc
    {
        void SendTrack(XCTrackingInfo trackingInfo, string value);
        void SendEvent(XCEvent ev);
        void GetCouponsFromPCL(Action<IList<XCCouponNotification>> OnCouponsDownloaded, Action<string> OnCouponDownloadError);
        void GetNotificationHistoryFromPCL(Action<IList<XCHistoryItem>> OnNotificationHistory, Action<string> OnNotificationHistoryError);
        void SetUserData(string key, string value);
        void SetUserData(string key, Dictionary<string, bool> values);
        void GetUserData(Action<IDictionary<string, object>> OnUserData, Action<string> OnUserDataError);
        void GetProfileIdFromPCL(Action<string> OnProfile, Action<string> OnError);
        void SetProfileId(string profile);
        void ResetProfileIdFromPCL(Action<string> OnProfile, Action<string> OnError);
        void OptOutFromPCL(Action<int> OnSuccess, Action<string> OnFailure);
        void TriggerInAppEvent(string key);
    }
}
