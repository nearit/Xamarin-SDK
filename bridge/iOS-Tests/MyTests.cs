using NearBridge.Adapter;
using System;
using NUnit.Framework;
using XamarinBridge.PCL.Types;
using NearIT;
using System.Collections.Generic;
using Foundation;

namespace iOSTests
{
    [TestFixture]
    public class MyTests
    {
        [Test]
        public void FromBridgeToNative()
        {
            XCTrackingInfo xtrack = new XCTrackingInfo();
            xtrack.RecipeId = "recid";
            xtrack.extras = new Dictionary<string, object>();
            xtrack.extras.Add("key", "value");

            XCFeedbackNotification XFeed = new XCFeedbackNotification();
            XFeed.Question = "Question?";
            XFeed.NotificationMessage = "ciao";
            XFeed.RecipeId = "rec-id";
            XFeed.TrackingInfo = xtrack;
            NITFeedback nativeFeed = AdapterFeedback.GetNative(XFeed);

            NSObject val = nativeFeed.TrackingInfo.extras["key"];
            Assert.True(nativeFeed.Question.Equals(XFeed.Question));

        }

        [Test]
        public void FromNativeToBridge()
        {
            var XKeys = new NSString[]{(NSString)"key"};
            var XObj = new NSObject[]{(NSString)"value"};

            NITTrackingInfo NTrack = new NITTrackingInfo();
            NTrack.RecipeId = "recid";
            NTrack.extras = new NSDictionary<NSString, NSObject>(XKeys, XObj);

            NITFeedback NFeed = new NITFeedback();
            NFeed.Question = "Question?";
            NFeed.NotificationMessage = "ciao";
            NFeed.RecipeId = "rec-id";
            NFeed.TrackingInfo = NTrack;
            XCFeedbackNotification XFeed = AdapterFeedback.GetCommonType(NFeed);

            Assert.True(XFeed.Question.Equals(NFeed.Question));
            Assert.NotNull(XFeed.TrackingInfo.extras);
            object value = XFeed.TrackingInfo.extras["key"];
            Assert.NotNull(value);
            Assert.True(value is string);
            Assert.True(XFeed.TrackingInfo.extras["key"].Equals("value"));
        }
    }
}
