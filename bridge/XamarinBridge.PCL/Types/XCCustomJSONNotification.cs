using System;
using System.Collections;
using System.Collections.Generic;

namespace XamarinBridge.PCL.Types
{
    public class XCCustomJSONNotification
    {
        public string NotificationMessage;
        public IDictionary<string, object> Content;
        public string Id;

        public XCCustomJSONNotification()
        {
        }
    }
}
