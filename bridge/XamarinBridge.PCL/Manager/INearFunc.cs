using System;
using System.Collections.Generic;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.PCL.Manager
{
    public interface INearFunc
    {
        void SendTrack(XCTrackingInfo trackingInfo, string value);
        void SendEvent(XCEvent ev);
        void GetCouponsFromPCL(Action<IList<XCCouponNotification>> OnCouponsDownloaded, Action<String> OnCouponDownloadError);
        void SetUserData(string key, string value);
        void GetProfileIdFromPCL(Action<String> OnProfile, Action<String> OnError);
        void SetProfileId(string profile);
        void ResetProfileIdFromPCL(Action<String> OnProfile, Action<String> OnError);
        void OptOutFromPCL(Action<int> OnSuccess, Action<String> OnFailure);
        void ProcessCustomTrigger(string key);
    }
}
