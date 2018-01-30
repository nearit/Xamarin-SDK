using System;
using NearIT;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.iOS.Adapter
{
    public class AdapterContent
    {
        public static XCContentNotification GetCommonType(NITContent ContentNotification)
        {
            XCContentNotification XContent = new XCContentNotification();

            XContent.NotificationMessage = ContentNotification.NotificationMessage;
            XContent.Title = ContentNotification.Title;
            XContent.Content = ContentNotification.Content;
            if (ContentNotification.Image != null)
            {
                XContent.ImageLink.FullSize = ContentNotification.Image.Url.AbsoluteString;
                XContent.ImageLink.SmallSize = ContentNotification.Image.SmallSizeURL.AbsoluteString;
            }

            if (ContentNotification.Link != null)
            {
                XContent.Cta.Label = ContentNotification.Link.Label;
                XContent.Cta.Url = ContentNotification.Link.Url.AbsoluteString;
            }
            XContent.Id = ContentNotification.ID;

            return XContent;
        }

    }
}
