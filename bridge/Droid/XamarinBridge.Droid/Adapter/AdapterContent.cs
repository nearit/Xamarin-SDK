using System;
using IT.Near.Sdk.Reactions.Contentplugin.Model;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterContent
    {
        public static XCContentNotification GetCommonType(Content ContentNotification)
        {
            XCContentNotification XContent = new XCContentNotification();

            XContent.NotificationMessage = ContentNotification.NotificationMessage;
            XContent.Title = ContentNotification.Title;
            XContent.Content = ContentNotification.ContentString;

            if (ContentNotification.ImageLink != null)
            {
                XContent.ImageLink = new XCImageSet();
                XContent.ImageLink.FullSize = ContentNotification.ImageLink.FullSize;
                XContent.ImageLink.SmallSize = ContentNotification.ImageLink.SmallSize;
            }

            if (ContentNotification.Cta != null)
            {
                XContent.Cta = new XCContentLink();
                XContent.Cta.Label = ContentNotification.Cta.Label;
                XContent.Cta.Url = ContentNotification.Cta.Url;
            }
            XContent.Id = ContentNotification.Id;

            return XContent;
        }
    }
}
