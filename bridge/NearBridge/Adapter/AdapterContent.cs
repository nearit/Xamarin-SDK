using System;
using NearIT;
using XamarinBridge.PCL.Types;

namespace XamarinBridge.Droid.Adapter
{
    public class AdapterContent
    {
        public static XCContentNotification GetCommonType(NITContent ContentNotification)
        {
            XCContentNotification XContent = new XCContentNotification();

            XContent.NotificationMessage = ContentNotification.NotificationMessage;
            XContent.Title = ContentNotification.Title;
            XContent.Content = ContentNotification.Content;
            XContent.ImageLink.FullSize = ContentNotification.Image.Url.AbsoluteString;
            XContent.ImageLink.SmallSize = ContentNotification.Image.SmallSizeURL.AbsoluteString;
            XContent.Cta.Label = ContentNotification.Link.Label;
            XContent.Cta.Url = ContentNotification.Link.Url.AbsoluteString;
            XContent.Id = ContentNotification.ID;

            return XContent;
        }
    }
}
