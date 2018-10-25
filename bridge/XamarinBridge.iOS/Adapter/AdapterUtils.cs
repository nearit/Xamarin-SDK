using System;
using System.Collections.Generic;
using Foundation;

namespace XamarinBridge.iOS.Adapter
{
    public class AdapterUtils
    {
        public static NSDictionary<NSString, NSObject> From(Dictionary<string, object> Dic)
        {
            var XKeys = new NSString[Dic.Count];
            var XObj = new NSObject[Dic.Count];
            int i = 0;

            foreach (string key in Dic.Keys)
            {
                XKeys[i] = (Foundation.NSString)key;
                XObj[i] = NSObject.FromObject(Dic[key]);
                i++;
            }

            NSDictionary<NSString, NSObject> NSDic = new NSDictionary<NSString, NSObject>(XKeys, XObj);

            return NSDic;
        }

        public static Dictionary<string, object> FromNS(NSDictionary<NSString, NSObject> NSDic)
        {
            Dictionary<string, object> Dic = new Dictionary<string, object>();

            foreach (NSString key in NSDic.Keys)
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
