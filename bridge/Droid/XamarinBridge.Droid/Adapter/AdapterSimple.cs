using System;
using XamarinBridge.PCL.Types;
using IT.Near.Sdk.Reactions.Simplenotificationplugin.Model;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterSimple
    {
        public static XCSimpleNotification GetCommonType(SimpleNotification SimpleNotification)
        {
            XCSimpleNotification XSimple = new XCSimpleNotification();

            XSimple.NotificationMessage = SimpleNotification.NotificationMessage;
            XSimple.Id = SimpleNotification.Id;

            return XSimple;
        }
    }
}
