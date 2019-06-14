using System;
using System.Collections.Generic;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.PCL.Manager
{
    public interface INotificationHistoryUpdateListener
    {
        void OnNotificationHistoryUpdate(IList<XCHistoryItem> history);
    }
}
