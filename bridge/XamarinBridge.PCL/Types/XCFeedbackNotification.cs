using System;
namespace XamarinBridge.PCL.Types
{
    public class XCFeedbackNotification
    {
        public string Id;
        public string NotificationMessage;
        public string Question;

        [ObsoleteAttribute("Use TrackingInfo instead.")]
        public string RecipeId;

        public XCTrackingInfo TrackingInfo;

        public XCFeedbackNotification()
        {
        }
    }
}
