using System;
using Foundation;
using UIKit;
using NearIT;
using NearBridge.Parser;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL;
using Xamarin.Forms;
using XamarinBridge.Droid.Adapter;

[assembly: Dependency(typeof(NearBridge.NearBridgeiOS))]
namespace NearBridge
{
    public class NearBridgeiOS : INearFunc
    {
        private static IContentsListener _contentsListener = new EventContent();

        public static void ParseContent(object content)
        {
            HandleNearContent.HandleContent(content, _contentsListener);
        }

        public void SetApiKey(string apiKey)
        {
            NITManager.SetupWithApiKey(apiKey);
        }

        public void RefreshConfiguration()
        {
            NITManager.DefaultManager.RefreshConfigWithCompletionHandler(
                (error) => {
                    if (error != null) Console.WriteLine("Refresh error ios");
                    else Console.WriteLine("Refresh ios");
                }
            );
        }

        public void SendTrack(XCTrackingInfo trackingInfo, string value)
        {
            NITTrackingInfo track = new NITTrackingInfo();
            track.RecipeId = trackingInfo.RecipeId;
            foreach (var item in track.extras)
            {
                trackingInfo.extras.Add((NSString)item.Key, item.Value);
            }

            NITManager.DefaultManager.SendTrackingWithTrackingInfo(track, value);
        }

        public void SendEvent(XCEvent ev)
        {
            if (ev.GetPluginName() == XCFeedbackEvent.PluginName)
            {
                XCFeedbackEvent feedbackEvent = (XamarinBridge.PCL.Types.XCFeedbackEvent)ev;

                XCFeedbackNotification feedback = feedbackEvent.FeedbackNotification;
                NITFeedback nativeFeedback = new NITFeedback();

                nativeFeedback = AdapterFeedback.GetNativeFeeback(nativeFeedback, feedback);

                NITFeedbackEvent naviveFeedbackEvent = new NITFeedbackEvent(nativeFeedback, feedbackEvent.rating, feedbackEvent.comment);
                NITManager.DefaultManager.SendEventWithEvent(naviveFeedbackEvent,
                                                             (error) => {
                                                                 if (error != null) Console.WriteLine("SendEventWithEvent error ios");
                                                                 else Console.WriteLine("SendEventWithEvent ios");
                                                             });
            }

        }

        public void GetCoupon()
        {
            NITManager.DefaultManager.CouponsWithCompletionHandler(
                (arg1, arg2) => {
                    if(arg2 != null) Console.WriteLine("GetCoupon error ios");
                    else Console.WriteLine("GetCoupon ios");
            });
        }

        public void SetUserData(string key, string value)
        {
            NITManager.DefaultManager.SetDeferredUserDataWithKey(key,value);
        }

        public void GetProfileId()
        {
            NITManager.DefaultManager.ProfileIdWithCompletionHandler(
                (arg1, arg2) => {
                if (arg2 != null) Console.WriteLine("GetProfileId error ios");
                else Console.WriteLine("GetProfileId ios");
                });
        }

        public void SetProfileId(string profile)
        {
            NITManager.DefaultManager.ProfileId = profile;
        }

        public void ResetProfileId()
        {
            NITManager.DefaultManager.ResetProfile();
        }

        public void OptOut()
        {
            NITManager.DefaultManager.OptOutWithCompletionHandler(
                (error) => {
                if(error)Console.WriteLine("OptOut error ios");
                else Console.WriteLine("OptOut ios");
            });
        }





        internal class EventContent : IContentsListener
        {
            public void GotContentNotification(NITContent content)
            {
                NITContent ContentNotification = content;
                XCContentNotification XContent = new XCContentNotification();

                XContent.NotificationMessage = ContentNotification.NotificationMessage;
                XContent.Title = ContentNotification.Title;
                XContent.Content = ContentNotification.Content;
                XContent.ImageLink.FullSize = ContentNotification.Image.Url.AbsoluteString;
                XContent.ImageLink.SmallSize = ContentNotification.Image.SmallSizeURL.AbsoluteString;
                XContent.Cta.Label = ContentNotification.Link.Label;
                XContent.Cta.Url = ContentNotification.Link.Url.AbsoluteString;

                // TODO null check
                NearPCL.GetContentManager().GotXContentNotification(XContent);
            }

            public void GotCouponNotification(NITCoupon content)
            {
                NITCoupon CouponNotification = content;
                XCCouponNotification XCoupon = new XCCouponNotification();

                XCoupon.NotificationMessage = CouponNotification.NotificationMessage;
                XCoupon.Description = CouponNotification.Description;
                XCoupon.Value = CouponNotification.Value;
                XCoupon.ExpiresAt = CouponNotification.ExpiresAt;
                XCoupon.ReedemableFrom = CouponNotification.RedeemableFrom;
                XCoupon.IconSet.FullSize = CouponNotification.Icon.Url.AbsoluteString;
                XCoupon.IconSet.SmallSize = CouponNotification.Icon.SmallSizeURL.AbsoluteString;

                NearPCL.GetContentManager().GotXCouponNotification(XCoupon);
            }

            public void GotCustomJSONNotification(NITCustomJSON content)
            {
                NITCustomJSON CustomJSONNotification = content;
                XCCustomJSONNotification XCustomJSON = new XCCustomJSONNotification();

                XCustomJSON.NotificationMessage = CustomJSONNotification.NotificationMessage;
                foreach (var item in CustomJSONNotification.Content)
                {
                    XCustomJSON.Content.Add((NSString)item.Key, item.Value);
                }

                NearPCL.GetContentManager().GotXCustomJSONNotification(XCustomJSON);
            }

            public void GotFeedbackNotification(NITFeedback content)
            {
                NITFeedback FeedbackNotification = content;
                XCFeedbackNotification XFeedback = new XCFeedbackNotification();

                XFeedback.NotificationMessage = FeedbackNotification.NotificationMessage;
                XFeedback.Question = FeedbackNotification.Question;
                XFeedback.RecipeId = FeedbackNotification.RecipeId;

                NearPCL.GetContentManager().GotXFeedbackNotification(XFeedback);
            }

            public void GotSimpleNotification(NITSimpleNotification content)
            {
                NITSimpleNotification SimpleNotification = content;
                XCSimpleNotification XSimple = new XCSimpleNotification();

                XSimple.NotificationMessage = SimpleNotification.NotificationMessage;

                NearPCL.GetContentManager().GotXSimpleNotification(XSimple);
            }
        }
    }
}

