using System;
using XamarinBridge.PCL.Types;
using IT.Near.Sdk.Reactions.Customjsonplugin.Model;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterCustom
    {
        public static XCCustomJSONNotification GetCommonType(CustomJSON CustomJSONNotification)
        {
            XCCustomJSONNotification XCustomJSON = new XCCustomJSONNotification();

            XCustomJSON.NotificationMessage = CustomJSONNotification.NotificationMessage;

            XCustomJSON.Content = CustomJSONNotification.Content;

            XCustomJSON.Id = CustomJSONNotification.Id;

            return XCustomJSON;
        }
    }
}
