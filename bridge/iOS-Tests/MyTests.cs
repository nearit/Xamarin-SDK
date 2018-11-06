using System;
using NUnit.Framework;
using XamarinBridge.PCL.Types;
using System.Collections.Generic;
using Foundation;
using NearIT;
using XamarinBridge.iOS.Adapter;

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

            NSObject val = nativeFeed.TrackingInfo.ExtrasDictionary()["key"];
            Assert.True(nativeFeed.Question.Equals(XFeed.Question));

        }

        [Test]
        public void FromNativeToBridge()
        {
            var XKeys = new NSString[]{(NSString)"key"};
            var XObj = new NSObject[]{(NSString)"value"};

            NITTrackingInfo NTrack = 
                NITTrackingInfo.TrackingInfoFromRecipeId("recid", 
                                                         NSDictionary<NSString, NSObject>.FromObjectsAndKeys(XObj, XKeys));

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

        [Test]
        public void FromCustomJsonNativeToCommon() {
          
            NSMutableDictionary<NSString, NSObject> nativeDic = new NSMutableDictionary<NSString, NSObject>();
            nativeDic.Add(new NSString("nome"), new NSString("stefano"));
            nativeDic.Add(new NSString("numero"), new NSNumber(4.6));

            NSNumber number3 = new NSNumber(3.0);
            NSNumber number4 = new NSNumber(4.0);
            NSArray array = NSArray.FromObjects(number3, number4);
            nativeDic.Add(new NSString("lista"), array);

            NSMutableDictionary<NSString, NSObject> innerNativeDic = new NSMutableDictionary<NSString, NSObject>();
            innerNativeDic.Add(new NSString("nome"), new NSString("martin"));
            innerNativeDic.Add(new NSString("cognome"), new NSString("scorsese"));
            NSDictionary<NSString, NSObject> innerDic = new NSDictionary<NSString, NSObject>(innerNativeDic.Keys, innerNativeDic.Values);
            nativeDic.Add(new NSString("inner_object"), innerDic);

            NSDictionary<NSString, NSObject> dic = new NSDictionary<NSString, NSObject>(nativeDic.Keys, nativeDic.Values);

            NITCustomJSON nativeCUstomJson = new NITCustomJSON();
            nativeCUstomJson.Content = dic;
            nativeCUstomJson.NotificationMessage = "messaggio";
            XCCustomJSONNotification jSONNotification = AdapterCustom.GetCommonType(nativeCUstomJson);

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
