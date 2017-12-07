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
            Console.WriteLine("prima di content");
            XContent.Content = ContentNotification.Content;
            if (ContentNotification.Image != null)
            {
                Console.WriteLine("prima di fullsize");
                XContent.ImageLink.FullSize = ContentNotification.Image.Url.AbsoluteString;
                Console.WriteLine("prima di smallsize");
                XContent.ImageLink.SmallSize = ContentNotification.Image.SmallSizeURL.AbsoluteString;
            }

            if (ContentNotification.Link != null)
            {
                Console.WriteLine("prima di cta label");
                XContent.Cta.Label = ContentNotification.Link.Label;
                Console.WriteLine("prima di cta url");
                XContent.Cta.Url = ContentNotification.Link.Url.AbsoluteString;
            }
            Console.WriteLine("prima di ID");
            XContent.Id = ContentNotification.ID;
            Console.WriteLine("fine cast");

            return XContent;
        }

    }
}
