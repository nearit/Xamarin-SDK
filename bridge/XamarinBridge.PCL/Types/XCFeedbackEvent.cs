using System;
namespace XamarinBridge.PCL.Types
{
    public class XCFeedbackEvent : XCEvent
    {
        public static string PluginName = "FeedbackEvent";
        public XCFeedbackNotification FeedbackNotification;
        public string comment;
        public int rating;

        public XCFeedbackEvent(XCFeedbackNotification feedbackNotification, string comment, int rating)
        {
            FeedbackNotification = feedbackNotification;
            this.comment = comment;
            this.rating = rating;
        }

        public override string GetPluginName()
        {
            return PluginName;
        }
    }
}
