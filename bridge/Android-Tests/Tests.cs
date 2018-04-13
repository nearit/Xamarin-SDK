using System;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;
using IT.Near.Sdk.Reactions.Customjsonplugin.Model;
using NUnit.Framework;
using XamarinBridge.PCL.Types;
using XamarinBridge.Droid.Adapter;
using System.Collections.Generic;
using IT.Near.Sdk.Trackings;
using System.Collections;
using Java.Util;
using Android.Runtime;

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

        [Test]
        public void FromNativeToBridgeCustomJSON() {
            CustomJSON customJSON = new CustomJSON();
            JavaDictionary jsonMap = new JavaDictionary();
            jsonMap.Add("nome", "stefano");
            jsonMap.Add("numero", 4.6);
            Java.Util.ArrayList lista = new Java.Util.ArrayList();
            lista.Add(3.0);
            lista.Add(4.0);
            jsonMap.Add("lista", lista);

            JavaDictionary innerDict = new JavaDictionary();
            innerDict.Add("nome", "martin");
            innerDict.Add("cognome", "scorsese");
            jsonMap.Add("inner_object", innerDict);

            customJSON.Content = jsonMap;
            customJSON.NotificationMessage = "messaggio";

            XCCustomJSONNotification jSONNotification = AdapterCustom.GetCommonType(customJSON);

            Assert.True(jSONNotification.Content is Dictionary<string, object>);

            jSONNotification.Content.TryGetValue("nome", out object stringValue);
            Assert.True(stringValue is string);
            Assert.True(stringValue.Equals("stefano"));

            jSONNotification.Content.TryGetValue("numero", out object doubleValue);
            Assert.True(doubleValue is double);
            Assert.True(doubleValue.Equals(4.6));

            jSONNotification.Content.TryGetValue("lista", out object listValue);
            Assert.True(listValue is List<object>);
            Assert.True((listValue as List<object>)[0] is double);
            Assert.True((double)(listValue as List<object>)[0] == 3.0);
            Assert.True((listValue as List<object>)[1] is double);
            Assert.True((double)(listValue as List<object>)[1] == 4.0);

            jSONNotification.Content.TryGetValue("inner_object", out object innerObj);
            Assert.True(innerObj is Dictionary<string, object>);
            Dictionary<string, object> castedDic = innerObj as Dictionary<string, object>;
            castedDic.TryGetValue("nome", out object name);
            Assert.True(name is string);
            Assert.True(((string)name).Equals("martin"));
            castedDic.TryGetValue("cognome", out object cognome);
            Assert.True(cognome is string);
            Assert.True(((string)cognome).Equals("scorsese"));
        }

    }
}
