using System;
using Foundation;
using UIKit;
using NearIT;
using NearBridge.Parser;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL;
using Xamarin.Forms;
using NearBridge.Adapter;
using System.Collections.Generic;
using UserNotifications;

[assembly: Dependency(typeof(NearBridge.NearBridgeiOS))]
namespace NearBridge
{
    public class NearBridgeiOS : INearFunc
    {
        private static NITContentDelegate _contentsListener = new EventContent();

        public static void ParseContent(NSObject Content, NITTrackingInfo TrackingInfo)
        {
            //HandleNearContent.HandleContent(content, _contentsListener);      this is mine handlecontent --> implement in the internal class EVENTCONTENT : IContentsListener

            NITManager.DefaultManager.ParseContent(Content, TrackingInfo, _contentsListener);
        }

        public static void SetApiKey()
        {
            Console.WriteLine("Your ApiKey: " + loadApiKey());
            NITManager.SetupWithApiKey(loadApiKey());
            SetFrameworkName();
        }

        public static void SetFrameworkName()
        {
            NITManager.SetFrameworkName("xamarin");
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
                                                             (error) =>
                                                             {
                                                                 if (error != null) Console.WriteLine("SendEventWithEvent error ios" + error);
                                                                 else Console.WriteLine("SendEventWithEvent ios");
                                                             });
            }

        }

        public static void GetCoupon(Action<NSArray<NITCoupon>> OnSuccess, Action<NSError> OnFailure)
        {
            NITManager.DefaultManager.CouponsWithCompletionHandler((NSArray<NITCoupon> coupons, NSError error) => 
            {
                if (error != null) {
                    OnFailure.Invoke(error);
                } else {
                    OnSuccess.Invoke(coupons);
                }
            });
        }

        public void SetUserData(string key, string value)
        {
            NITManager.DefaultManager.SetUserData(key, value);
        }

        public static void GetProfileId(Action<NSString> OnSuccess, Action<NSError> OnError)
        {
            NITManager.DefaultManager.ProfileIdWithCompletionHandler((NSString profileId, NSError error) =>
            {
                if (error != null)
                {
                    OnError.Invoke(error);
                }
                else
                {
                    OnSuccess.Invoke(profileId);
                }
            });
        }

        public void SetProfileId(string profileId)
        {
            NITManager.DefaultManager.SetProfileId(profileId);
        }

        public static void ResetProfileId(Action<NSString> OnSuccess, Action<NSError> OnError)
        {
            NITManager.DefaultManager.ResetProfileWithCompletionHandler((NSString profileId, NSError error) =>
            {
                if (error != null)
                {
                    OnError.Invoke(error);
                }
                else
                {
                    OnSuccess.Invoke(profileId);
                }
            });
        }

        public static void OptOut(Action<int> OnSuccess, Action<int> OnError) {
            NITManager.DefaultManager.OptOutWithCompletionHandler(
                (error) =>
                    {
                        if (error) OnError.Invoke(1);
                        else OnSuccess.Invoke(0);
                 });
        }

        public void ProcessCustomTrigger(string key)
        {
            NITManager.DefaultManager.ProcessCustomTriggerWithKey(key);
        }

        public void GetCouponsFromPCL(Action<IList<XCCouponNotification>> OnCouponsDownloaded, Action<string> OnCouponDownloadError)
        {
            GetCoupon((coupons) => {
                IList<XCCouponNotification> list = new List<XCCouponNotification>();
                foreach(NITCoupon coupon in coupons) {
                    list.Add(AdapterCoupon.GetCommonType(coupon));
                }
                OnCouponsDownloaded.Invoke(list);
            },(error) => {
                OnCouponDownloadError.Invoke(error.ToString());
            });
        }

        public void GetProfileIdFromPCL(Action<string> OnProfile, Action<string> OnError)
        {
            GetProfileId((profileId) => {
                OnProfile.Invoke(profileId);
            }, (error) => {
                OnError.Invoke(error.ToString());
            });
        }

        public void ResetProfileIdFromPCL(Action<string> OnProfile, Action<string> OnError)
        {
            ResetProfileId((profileId) =>
            {
                OnProfile.Invoke(profileId);
            }, (error) =>
            {
                OnError.Invoke(error.ToString());
            });
        }

        public void OptOutFromPCL(Action<int> OnSuccess, Action<string> OnFailure)
        {
            OptOut(OnSuccess, (error) =>
            {
                OnFailure.Invoke("Error");
            });
        }

        public static void ProcessRecipeWithUserInfo(UNNotificationResponse response, Action<NITReactionBundle, NITTrackingInfo> OnSuccess)
        {
            var userInfo = response.Notification.Request.Content.UserInfo;

            NSString[] keys = new NSString[userInfo.Keys.Length];
            int i;
            for (i = 0; i < userInfo.Keys.Length; i++)
            {
                if (userInfo.Keys[i] is NSString)
                    keys[i] = userInfo.Keys[i] as NSString;
                else
                    i = int.MaxValue;
            }
            if (i != int.MaxValue)
            {
                NSDictionary<NSString, NSObject> notif = new NSDictionary<NSString, NSObject>(keys, userInfo.Values);
                NITManager.DefaultManager.ProcessRecipeWithUserInfo(notif, (content, trackingInfo, error) =>
                {
                    if (content != null && content is NITReactionBundle)
                    {
                        OnSuccess.Invoke(content, trackingInfo);
                    }
                });
            }
        }

        internal class EventContent : NITContentDelegate
        {
            public override void GotContent(NITContent Content, NITTrackingInfo TrackingInfo)
            {
                XCContentNotification XContent = AdapterContent.GetCommonType(Content);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXContentNotification(XContent);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public override void GotCoupon(NITCoupon Coupon, NITTrackingInfo TrackingInfo)
            {
                XCCouponNotification XCoupon = AdapterCoupon.GetCommonType(Coupon);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXCouponNotification(XCoupon);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public override void GotCustomJSON(NITCustomJSON CustomJSON, NITTrackingInfo TrackingInfo)
            {
                XCCustomJSONNotification XCustomJSON = AdapterCustom.GetCommonType(CustomJSON);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXCustomJSONNotification(XCustomJSON);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public override void GotFeedback(NITFeedback Feedback, NITTrackingInfo TrackingInfo)
            {
                XCFeedbackNotification XFeedback = AdapterFeedback.GetCommonType(Feedback);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXFeedbackNotification(XFeedback);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public override void GotSimpleNotification(NITSimpleNotification Notification, NITTrackingInfo TrackingInfo)
            {
                XCSimpleNotification XSimple = AdapterSimple.GetCommonType(Notification);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXSimpleNotification(XSimple);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }
        }
    }
}

