using System;
using XamarinBridge.PCL.Types;
using IT.Near.Sdk.Reactions.Customjsonplugin.Model;
using System.Collections.Generic;
using Android.Runtime;
using System.Collections;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterCustom
    {
        public static XCCustomJSONNotification GetCommonType(CustomJSON CustomJSONNotification)
        {
            XCCustomJSONNotification XCustomJSON = new XCCustomJSONNotification();

            XCustomJSON.NotificationMessage = CustomJSONNotification.NotificationMessage;

            XCustomJSON.Content = From((Android.Runtime.JavaDictionary)CustomJSONNotification.Content);

            XCustomJSON.Id = CustomJSONNotification.Id;

            return XCustomJSON;
        }

        private static IDictionary<string, object> From(JavaDictionary dic) {
            IDictionary<string, object> output = new Dictionary<string, object>();
            foreach(string key in dic.Keys) {
                object value = dic[key];
                output.Add(key, NormalizeDicValue(value));
            }
            return output;
        }

        private static object NormalizeDicValue(object value) {
            if (value is JavaDictionary) {
                return From(value as JavaDictionary);
            } else if (value is Java.Util.ArrayList) {
                List<object> list = new List<object>();
                Java.Util.ArrayList nativeList = (Java.Util.ArrayList)value;
                for (int i = 0; i < nativeList.Size(); i++) {
                    list.Add(NormalizeDicValue(nativeList.Get(i)));
                }
                return list;
            } else if (value is Java.Lang.Double){
                return (value as Java.Lang.Double).DoubleValue();
            } else {
                return value;
            }
        }
    }
}
