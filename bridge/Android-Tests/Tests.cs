using System;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;
using NUnit.Framework;
using XamarinBridge.PCL.Types;
using XamarinBridge.Droid.Adapter;
using System.Collections.Generic;
using IT.Near.Sdk.Trackings;
using System.Collections;
using Java.Util;

namespace AndroidTests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

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

            Feedback NFeed = AdapterFeedback.GetNative(XFeed);

            Assert.True(NFeed.Question.Equals(XFeed.Question));
        }

        [Test]
        public void FromNativeToBridge()
        {
            TrackingInfo NTrack = new TrackingInfo();
            NTrack.RecipeId = "recid";
            NTrack.Metadata = new Dictionary<Java.Lang.String, Java.Lang.Object>();
            NTrack.Metadata.Add("key", "value");

            Feedback NFeed = new Feedback();
            NFeed.Question = "Question?";
            NFeed.NotificationMessage = "ciao";
            NFeed.RecipeId = "rec-id";
            NFeed.TrackingInfo = NTrack;

            XCFeedbackNotification XFeed = AdapterFeedback.GetCommonType(NFeed);

            Assert.True(XFeed.Question.Equals(NFeed.Question));
            Assert.True(XFeed.TrackingInfo.extras["key"].Equals("value"));
        }

    }
}
