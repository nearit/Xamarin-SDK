using System;
using XamarinBridge.PCL.Types;
using NearIT;

namespace XamarinBridge.iOS.Adapter
{
    public class AdapterSimple
    {
        public static XCSimpleNotification GetCommonType(NITSimpleNotification SimpleNotification)
        {
            XCSimpleNotification XSimple = new XCSimpleNotification();

            XSimple.NotificationMessage = SimpleNotification.NotificationMessage;
            XSimple.Id = SimpleNotification.ID;

            return XSimple;
        }
    }
}
