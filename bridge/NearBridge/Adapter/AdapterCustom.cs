using System;
using XamarinBridge.PCL.Types;
using NearIT;
using Foundation;
using System.Collections.Generic;
using System.Collections;

namespace XamarinBridge.Droid.Adapter
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

        public static IDictionary From(NSDictionary NSDic) {
            IDictionary output = new Dictionary<NSString, NSObject>();
            foreach (NSString key in NSDic.Keys)
            {
                NSObject value = NSDic.ObjectForKey(key);
                if (value is NSDictionary)
                {
                    value = (Foundation.NSObject)From((NSDictionary)value);
                }
                output.Add(key, value);
            }
            return output;
        }
    }
}
