using System;
using XamarinBridge.PCL.Types;
using NearIT;
using Foundation;
using System.Collections.Generic;
using System.Linq;

namespace NearBridge.Adapter
{
    public class AdapterFeedback
    {
        public static NITFeedback GetNative(XCFeedbackNotification xfeed)
        {
            NITFeedback native = new NITFeedback();
            native.TrackingInfo = new NITTrackingInfo();

            native.NotificationMessage = xfeed.NotificationMessage;
            native.Question = xfeed.Question;
            native.RecipeId = xfeed.RecipeId;
            native.TrackingInfo.extras = From(xfeed.TrackingInfo.extras);
            native.TrackingInfo.RecipeId = xfeed.TrackingInfo.RecipeId;
            native.ID = xfeed.Id;

            return native;
        }

        public static XCFeedbackNotification GetCommonType(NITFeedback native)
        {
            XCFeedbackNotification xfeed = new XCFeedbackNotification();
            xfeed.TrackingInfo = new XCTrackingInfo();

            xfeed.NotificationMessage = native.NotificationMessage;
            xfeed.Question = native.Question;
            xfeed.RecipeId = native.RecipeId;
            xfeed.TrackingInfo.extras = FromNS(native.TrackingInfo.extras);
            xfeed.TrackingInfo.RecipeId = native.TrackingInfo.RecipeId;
            xfeed.Id = native.ID;

            return xfeed;
        }

        private static NSDictionary<NSString, NSObject> From(Dictionary<string, object> Dic)
        {
            var XKeys = new NSString[Dic.Count()];
            var XObj = new NSObject[Dic.Count()];
            int i = 0;

            foreach(string key in Dic.Keys)
            {
                XKeys[i] = (Foundation.NSString)key;
                XObj[i] = NSObject.FromObject(Dic[key]);
                i++;
            }

            NSDictionary<NSString, NSObject> NSDic = new NSDictionary<NSString, NSObject>(XKeys, XObj);

            return NSDic;
        }

        private static Dictionary<string, object> FromNS(NSDictionary<NSString, NSObject> NSDic)
        {
            Dictionary<string, object> Dic = new Dictionary<string, object>();

            foreach(NSString key in NSDic.Keys)
            {
                NSObject value = NSDic.ObjectForKey(key);
                object val = ToObject(value);
                Dic.Add(key, val);
            }
            return Dic;
        }

        public static Object ToObject(NSObject nsO)
        {
            if (nsO is NSString)
            {
                return nsO.ToString();
            }

            //array di stringhe

            return nsO;
        }
    }
}
