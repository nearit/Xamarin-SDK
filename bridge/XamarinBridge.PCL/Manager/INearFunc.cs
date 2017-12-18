using System;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.PCL.Manager
{
    public interface INearFunc
    {
        void RefreshConfiguration();
        void SendTrack(XCTrackingInfo trackingInfo, string value);
        void SendEvent(XCEvent ev);
        void GetCoupon();
        void SetUserData(string key, string value);
        void GetProfileId();
        void SetProfileId(string profile);
        void ResetProfileId();
        void OptOut();
        void ProcessCustomTrigger(string key);
    }
}
