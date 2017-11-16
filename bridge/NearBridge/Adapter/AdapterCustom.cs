using System;
using XamarinBridge.PCL.Types;
using NearIT;
using Foundation;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterCustom
    {
        public static XCCustomJSONNotification GetCommonType(NITCustomJSON CustomJSONNotification)
        {
            XCCustomJSONNotification XCustomJSON = new XCCustomJSONNotification();

            XCustomJSON.NotificationMessage = CustomJSONNotification.NotificationMessage;
            XCustomJSON.Id = CustomJSONNotification.ID;
            foreach (var item in CustomJSONNotification.Content)
            {
                XCustomJSON.Content.Add((NSString)item.Key, item.Value);
            }

            return XCustomJSON;
        }
    }
}
