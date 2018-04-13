using System;
using XamarinBridge.PCL.Types;
using NearIT;
using Foundation;
using System.Collections.Generic;
using System.Collections;

namespace XamarinBridge.iOS.Adapter
{
    public class AdapterCustom
    {
        public static XCCustomJSONNotification GetCommonType(NITCustomJSON CustomJSONNotification)
        {
            XCCustomJSONNotification XCustomJSON = new XCCustomJSONNotification();

            XCustomJSON.NotificationMessage = CustomJSONNotification.NotificationMessage;
            XCustomJSON.Id = CustomJSONNotification.ID;
            XCustomJSON.Content = From(CustomJSONNotification.Content);
            
            return XCustomJSON;
        }

        private static IDictionary<string, object> From(NSDictionary NSDic)
        {
            IDictionary<string, object> output = new Dictionary<string, object>();
            foreach (NSString key in NSDic.Keys)
            {
                NSObject value = NSDic.ObjectForKey(key);
                output.Add(key.ToString(), NormalizeDicValue(value));
            }
            return output;
        }

        private static object NormalizeDicValue(NSObject value) {
            if (value == null) {
                return null;
            } else if (value is NSDictionary) {
                return From((NSDictionary)value);
            } else if (value is NSString) {
                return ((NSString)value).ToString();
            } else if (value is NSNumber) {
                return ((NSNumber)value).DoubleValue;
            } else if (value is NSArray) {
                List<object> list = new List<object>();
                NSArray nativeList = (NSArray)value;
                for (nuint i = 0; i < nativeList.Count; i++) {
                    NSObject listObject = nativeList.GetItem<NSObject>(i);
                    list.Add(NormalizeDicValue(listObject));
                }
                return list;
            }
            return null;
        }
    }
}
