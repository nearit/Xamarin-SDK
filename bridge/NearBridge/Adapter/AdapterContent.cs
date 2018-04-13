using System;
using NearIT;
using XamarinBridge.PCL.Types;

namespace NearBridge.Adapter
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
                XContent.ImageLink = new XCImageSet
                {
                    FullSize = ContentNotification.Image.Url.AbsoluteString,
                    SmallSize = ContentNotification.Image.SmallSizeURL.AbsoluteString
                };
            }

            if (ContentNotification.Link != null)
            {
                XContent.Cta = new XCContentLink
                {
                    Label = ContentNotification.Link.Label,
                    Url = ContentNotification.Link.Url.AbsoluteString
                };
            }
            XContent.Id = ContentNotification.ID;

            return XContent;
        }

    }
}
