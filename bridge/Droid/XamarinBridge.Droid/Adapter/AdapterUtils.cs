using System;
using System.Collections;
using System.Collections.Generic;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterUtils
    {
        public static Dictionary<string, object> From(IDictionary dic)
        {
            Dictionary<string, object> NewDic = new Dictionary<string, object>();

            IDictionaryEnumerator enumerator = dic.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Key is string)
                {
                    NewDic.Add((string)enumerator.Key, enumerator.Value);
                }
            }
            return NewDic;
        }
    }
}
