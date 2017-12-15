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

        public static void SetApiKey()
        {
            Console.WriteLine("Your ApiKey: ");
            Console.WriteLine(loadApiKey());
            NITManager.SetupWithApiKey(loadApiKey());
        }

        private static string loadApiKey()
        {
            NSDictionary settings = NSDictionary.FromFile("Keys.plist");

            if (settings != null)
            {
                var value = settings.ValueForKey(new NSString("apiKey"));
                if (value != null) return value.ToString();
            }

            return "";
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

                nativeFeedback = AdapterFeedback.GetNative(feedback);

                NITFeedbackEvent nativeFeedbackEvent = new NITFeedbackEvent(nativeFeedback, feedbackEvent.rating, feedbackEvent.comment);
                NITManager.DefaultManager.SendEventWithEvent(nativeFeedbackEvent,
                                                             (error) => {
                                                                 if (error != null) Console.WriteLine("SendEventWithEvent error ios" + error);
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
            NITManager.DefaultManager.SetUserData(key, value);
        }

        public void GetProfileId()
        {
            NITManager.DefaultManager.ProfileIdWithCompletionHandler(
                (arg1, arg2) =>
                {
                    if (arg2 != null) Console.WriteLine("GetProfileId error ios");
                    else Console.WriteLine("GetProfileId ios");
                });
        }

        public void SetProfileId(string profile)
        {
            NITManager.DefaultManager.ProfileIdWithCompletionHandler((arg1, arg2) =>
            {
                if (arg2 != null) Console.WriteLine("SetProfileId error ios");
                else Console.WriteLine("SetProfileId ios");
            });
        }

        public void ResetProfileId()
        {
            NITManager.DefaultManager.ResetProfileWithCompletionHandler((arg1, arg2) => {
                if(arg2 != null) Console.WriteLine("ResetProfileId error ios");
                else Console.WriteLine("ResetProfileId ios");
            });
        }

        public void OptOut()
        {
            NITManager.DefaultManager.OptOutWithCompletionHandler(
                (error) => {
                if(error)Console.WriteLine("OptOut error ios");
                else Console.WriteLine("OptOut ios");
            });
        }

        public void ProcessCustomTrigger(string key)
        {
            NITManager.DefaultManager.ProcessCustomTriggerWithKey(key);
        }

        internal class EventContent : IContentsListener
        {
            public void GotContentNotification(NITContent content)
            {
                XCContentNotification XContent = AdapterContent.GetCommonType(content);

                if (NearPCL.GetContentManager() != null)
                {
                    Console.WriteLine("prima di gotxcontent");
                    NearPCL.GetContentManager().GotXContentNotification(XContent);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public void GotCouponNotification(NITCoupon content)
            {
                XCCouponNotification XCoupon = AdapterCoupon.GetCommonType(content);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXCouponNotification(XCoupon);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public void GotCustomJSONNotification(NITCustomJSON content)
            {
                XCCustomJSONNotification XCustomJSON = AdapterCustom.GetCommonType(content);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXCustomJSONNotification(XCustomJSON);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public void GotFeedbackNotification(NITFeedback content)
            {
                XCFeedbackNotification XFeedback = AdapterFeedback.GetCommonType(content);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXFeedbackNotification(XFeedback);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }


            public void GotSimpleNotification(NITSimpleNotification content)
            {
                XCSimpleNotification XSimple = AdapterSimple.GetCommonType(content);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXSimpleNotification(XSimple);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }
        }
    }
}

