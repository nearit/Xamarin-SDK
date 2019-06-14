using System;
using Foundation;
using UIKit;
using NearIT;
using XamarinBridge.iOS.Parser;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL;
using Xamarin.Forms;
using XamarinBridge.iOS.Adapter;
using System.Collections.Generic;
using UserNotifications;

[assembly: Dependency(typeof(XamarinBridge.iOS.NearBridgeiOS))]
namespace XamarinBridge.iOS
{
    public class NearBridgeiOS : INearFunc
    {
        static NearBridgeiOS() {}

        private static NITContentDelegate _contentsListener = new EventContent();

        public static void ParseContent(NSObject Content, NITTrackingInfo TrackingInfo)
        {
            //HandleNearContent.HandleContent(content, _contentsListener);      this is mine handlecontent --> implement in the internal class EVENTCONTENT : IContentsListener

            NITManager.DefaultManager.ParseContent(Content, TrackingInfo, _contentsListener);
        }

        public static void Init(string key) {
            NITManager.SetupWithApiKey(key);
            NITManager.SetFrameworkName("xamarin");
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

        public static void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler) 
        {
            NITManager.DefaultManager.Application(application, completionHandler);
        }

        public void SendTrack(XCTrackingInfo trackingInfo, string value)
        {
            NITTrackingInfo track = new NITTrackingInfo();
            track.RecipeId = trackingInfo.RecipeId;
            foreach (var item in track.ExtrasDictionary())
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
                NITManager.DefaultManager.SendEventWithEvent(nativeFeedbackEvent, (error) =>
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

        public void GetCouponsFromPCL(Action<IList<XCCouponNotification>> OnCouponsDownloaded, Action<string> OnCouponDownloadError)
        {
            GetCoupon((coupons) => {
                IList<XCCouponNotification> list = new List<XCCouponNotification>();
                foreach (NITCoupon coupon in coupons)
                {
                    list.Add(AdapterCoupon.GetCommonType(coupon));
                }
                OnCouponsDownloaded.Invoke(list);
            }, (error) => {
                OnCouponDownloadError.Invoke(error.ToString());
            });
        }

        public static void GetNotificationHistory(Action<NSArray<NITHistoryItem>> OnSuccess, Action<NSError> OnFailure)
        {
            NITManager.DefaultManager.HistoryWithCompletion((NSArray<NITHistoryItem> notificationHistory, NSError error) =>
            {
                if (error != null) {
                    OnFailure.Invoke(error);
                } else {
                    OnSuccess.Invoke(notificationHistory);
                }
            });
        }

        

        public void GetNotificationHistoryFromPCL(Action<IList<XCHistoryItem>> OnNotificationHistory, Action<string> OnNotificationHistoryError)
        {
            GetNotificationHistory( (notificationHistory) => {
                IList<XCHistoryItem> list = new List<XCHistoryItem>();
                foreach(NITHistoryItem item in notificationHistory)
                {
                    list.Add(HistoryAdapter.GetCommonType(item));
                }
                OnNotificationHistory.Invoke(list);
            }, (error) => {
                OnNotificationHistoryError.Invoke(error.ToString());
            });
        }

        public void SetUserData(string key, string value)
        {
            NITManager.DefaultManager.SetUserDataWithKey(key, value);
        }

        public void GetUserData(Action<IDictionary<string, object>> OnUserData, Action<string> OnUserDataError)
        {
            GetUserDataNative((nativeMap) => {
                IDictionary<string, object> userDataMap = new Dictionary<string, object>();
                foreach (NSString key in nativeMap.Keys)
                {
                    userDataMap.Add(key, nativeMap.ObjectForKey(key));
                }
                OnUserData.Invoke(userDataMap);
            }, (error) => {
                OnUserDataError.Invoke(error.ToString());
            });

        }

        public static void GetUserDataNative(Action<NSDictionary<NSString, NSObject>> OnSuccess, Action<NSError> OnFailure)
        {
            NITManager.DefaultManager.GetUserDataWithCompletionHandler((NSDictionary<NSString, NSObject> userData, NSError error) =>
            {
                if (error != null)
                {
                    OnFailure.Invoke(error);
                }
                else
                {
                    OnSuccess.Invoke(userData);
                }
            });
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
            NITManager.DefaultManager.ProfileId = profileId;
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

        public void TriggerInAppEvent(string key)
        {
            NITManager.DefaultManager.TriggerInAppEventWithKey(key);
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

        [Obsolete("Please use ProcessRecipeWithResponse instead.")]
        public static void ProcessRecipeWithUserInfo(UNNotificationResponse response, Action<NITReactionBundle, NITTrackingInfo> OnSuccess)
        {
            ProcessRecipeWithResponse(response, OnSuccess);
        }

        public static void ProcessRecipeWithResponse(UNNotificationResponse response, Action<NITReactionBundle, NITTrackingInfo> OnSuccess)
        {
            NITManager.DefaultManager.ProcessRecipeWithResponse(response, (content, trackingInfo, error) =>
            {
                if (content != null && content is NITReactionBundle)
                {
                    OnSuccess.Invoke(content, trackingInfo);
                }
            });
        }

        public static void ParseContentFromResponse(UNNotificationResponse response)
        {
            ProcessRecipeWithResponse(response, (NITReactionBundle content, NITTrackingInfo trackingInfo) =>
            {
                NearBridgeiOS.ParseContent(content, trackingInfo);
            });
        }

        public void SetUserData(string key, Dictionary<string, bool> values)
        {
            if (values == null)
            {
                NITManager.DefaultManager.SetUserDataWithKey(key, null as NSDictionary<NSString, NSNumber>);
            }
            else
            {
                NSMutableDictionary<NSString, NSNumber> multi = new NSMutableDictionary<NSString, NSNumber>();
                foreach (KeyValuePair<string, bool> entry in values)
                {
                    NSString nativeKey = new NSString(entry.Key);
                    NSNumber nativeBool = new NSNumber(entry.Value);
                    multi.Add(nativeKey, nativeBool);
                }
                NITManager.DefaultManager.SetUserDataWithKey(key, multi.Copy() as NSDictionary<NSString, NSNumber>);
            }
        }

        public static void Start() {
            NITManager.DefaultManager.Start();
        }

        public static void Stop() {
            NITManager.DefaultManager.Stop();
        }

        public static void SetDeviceToken(NSData token) {
            NITManager.DefaultManager.SetDeviceTokenWithData(token);
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

