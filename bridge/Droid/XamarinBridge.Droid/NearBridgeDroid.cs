using System;
using Android.App;
using Android.Widget;
using Android.OS;
using IT.Near.Sdk;
using IT.Near.Sdk.Utils;
using Android.Content;
using Android.Runtime;
using Xamarin.Forms;
using IT.Near.Sdk.Reactions.Contentplugin.Model;
using IT.Near.Sdk.Reactions.Couponplugin.Model;
using IT.Near.Sdk.Reactions.Customjsonplugin.Model;
using IT.Near.Sdk.Reactions.Feedbackplugin.Model;
using IT.Near.Sdk.Reactions.Simplenotificationplugin.Model;
using IT.Near.Sdk.Trackings;
using IT.Near.Sdk.Recipes;
using IT.Near.Sdk.Reactions.Feedbackplugin;
using IT.Near.Sdk.Reactions.Couponplugin;
using IT.Near.Sdk.Reactions;
using IT.Near.Sdk.Communication;
using IT.Near.Sdk.Operation;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL;
using XamarinBridge.Droid.Adapter;
using System.Collections.Generic;
using IT.Near.Sdk.Operation.Values;
using IT.Near.Sdk.Recipes.Inbox.Model;
using IT.Near.Sdk.Recipes.Inbox;

[assembly: Dependency(typeof(XamarinBridge.Droid.NearBridgeDroid))]
namespace XamarinBridge.Droid
{
    public class NearBridgeDroid : INearFunc
    {
        private static IContentsListener _contentListener = new EventContent();

        public static void ParseIntent(Intent intent)
        {
            NearUtils.ParseContents(intent, _contentListener);
        }

        public static void ParseForegroundEvent(IParcelable parcelable, TrackingInfo track)
        {
            NearUtils.ParseContents(parcelable, track, _contentListener);
        }

        public void SendTrack(XCTrackingInfo trackingInfo, string value)
        {
            TrackingInfo track = new TrackingInfo();
            track.RecipeId = trackingInfo.RecipeId;
            track.Metadata = trackingInfo.extras;

            NearItManager.Instance.SendTracking(track, value);
        }

        public void SendEvent(XCEvent ev)
        {
            if (ev.GetPluginName() == XCFeedbackEvent.PluginName)
            {
                XCFeedbackEvent feedbackEvent = (XamarinBridge.PCL.Types.XCFeedbackEvent)ev;

                XCFeedbackNotification feedback = feedbackEvent.FeedbackNotification;

                Feedback nativeFeedback = AdapterFeedback.GetNative(feedback);

                FeedbackEvent nativeFeedbackEvent = new FeedbackEvent(nativeFeedback, feedbackEvent.rating, feedbackEvent.comment);
                NearItManager.Instance.SendEvent(nativeFeedbackEvent);
            }
            else
            {
                Console.WriteLine("Error SendEvent android");
            }
        }

        public void GetCouponsFromPCL(Action<IList<XCCouponNotification>> OnCouponsDownloaded, Action<String> OnCouponDownloadError)
        {
            NearItManager.Instance.GetCoupons(new CouponDelegate((couponList) => {
                IList<XCCouponNotification> XCcouponList = new List<XCCouponNotification>();

                foreach(Coupon item in couponList)
                {
                    XCcouponList.Add(AdapterCoupon.GetCommonType(item));
                }

                OnCouponsDownloaded.Invoke(XCcouponList);
            }, OnCouponDownloadError));
        }

        public static void GetCoupons(Action<IList<Coupon>> OnCouponsDownloaded, Action<String> OnCouponDownloadError)
        {
            NearItManager.Instance.GetCoupons(new CouponDelegate(OnCouponsDownloaded, OnCouponDownloadError));
        }

        public void GetNotificationHistoryFromPCL(Action<IList<XCHistoryItem>> OnNotificationHistory, Action<string> OnNotificationHistoryError)
        {
            NearItManager.Instance.GetHistory(new HistoryDelegate( (notificationHistory) => {
                IList<XCHistoryItem> XCHistoryItems = new List<XCHistoryItem>();
                foreach(HistoryItem item in notificationHistory)
                {
                    XCHistoryItems.Add(HistoryAdapter.GetCommonType(item));
                }
                OnNotificationHistory.Invoke(XCHistoryItems);
            }, OnNotificationHistoryError));
        }

        public static void GetNotificationHistory(Action<IList<HistoryItem>> OnNotificationHistory, Action<string> OnError) {
            NearItManager.Instance.GetHistory(new HistoryDelegate(OnNotificationHistory, OnError));
        }

        public void SetUserData(string key, string value)
        {
            NearItManager.Instance.SetUserData(key, value);
        }

        public void SetUserData(string key, Dictionary<string, bool> value)
        {
            if (value == null)
            {
                NearItManager.Instance.SetUserData(key, null as NearMultipleChoiceDataPoint);
            }
            else
            {
                NearMultipleChoiceDataPoint multi = new NearMultipleChoiceDataPoint();
                foreach (KeyValuePair<string, bool> entry in value)
                {
                    multi.Put(entry.Key, entry.Value);
                }
                NearItManager.Instance.SetUserData(key, multi);
            }
        }

        public void GetProfileIdFromPCL(Action<String> OnProfile, Action<String> OnError)
        {
            NearBridgeDroid.GetProfileId(OnProfile, OnError);
        }

        public static void GetProfileId(Action<String> OnProfile, Action<String> OnError)
        {
            NearItManager.Instance.GetProfileId(new ProfileDelegate(OnProfile, OnError));
        }

        public void SetProfileId(string profile)
        {
            NearItManager.Instance.ProfileId = profile;
        }



        public void ResetProfileIdFromPCL(Action<String> OnProfile, Action<String> OnError) {
            NearBridgeDroid.ResetProfileId(OnProfile, OnError);
        }

        public static void ResetProfileId(Action<String> OnProfile, Action<String> OnError)
        {
            NearItManager.Instance.ResetProfileId(new ProfileDelegate(OnProfile, OnError));
        }


        public void OptOutFromPCL(Action<int> OnSuccess, Action<String> OnFailure)
        {
            NearBridgeDroid.OptOut(OnSuccess, OnFailure);
        }

        public static void OptOut(Action<int> OnSuccess, Action<String> OnFailure)
        {
            NearItManager.Instance.InvokeOptOut(new OptOutDelegate(OnSuccess, OnFailure));
        }


        public void TriggerInAppEvent(string key)
        {
            NearItManager.Instance.TriggerInAppEvent(key);
        }

        internal class OptOutDelegate : Java.Lang.Object, IOptOutNotifier
        {
            Action<int> success;
            Action<String> failure;

            public OptOutDelegate(Action<int> OnSuccess, Action<String> OnFailure)
            {
                this.success = OnSuccess;
                this.failure = OnFailure;
            }
 
            public void OnFailure(string p0)
            {
                failure.Invoke(p0);
            }

            public void OnSuccess()
            {
                success.Invoke(0);
            }
        }

        internal class ProfileDelegate : Java.Lang.Object, NearItUserProfile.IProfileFetchListener
        {
            Action<String> success;
            Action<String> failure;

            public ProfileDelegate(Action<String> OnProfile, Action<String> OnError)
            {
                this.success = OnProfile;
                this.failure = OnError;
            }
            public void OnError(string p0)
            {
                failure.Invoke(p0);
            }

            public void OnProfileId(string p0)
            {
                success.Invoke(p0);
            }
        }

        internal class CouponDelegate : Java.Lang.Object, ICouponListener
        {
            Action<IList<Coupon>> success;
            Action<String> failure;

            public CouponDelegate(Action<IList<Coupon>> OnCouponsDownloaded,Action<String> OnCouponDownloadError)
            {
                success = OnCouponsDownloaded;
                failure = OnCouponDownloadError;
            }

            public void OnCouponDownloadError(string p0)
            {
                failure.Invoke(p0);
            }

            public void OnCouponsDownloaded(IList<Coupon> p0)
            {
                success.Invoke(p0);
            }
        }

        internal class HistoryDelegate : Java.Lang.Object, NotificationHistoryManager.IOnNotificationHistoryListener
        {
            Action<IList<HistoryItem>> success;
            Action<String> failure;

            public HistoryDelegate(Action<IList<HistoryItem>> OnNotificationHistory, Action<String> OnError)
            {
                success = OnNotificationHistory;
                failure = OnError;
            }

            public void OnError(string p0)
            {
                failure.Invoke(p0);
            }

            public void OnNotifications(IList<HistoryItem> p0)
            {
                success.Invoke(p0);
            }
        }

        internal class EventContent : Java.Lang.Object, IContentsListener
        {
            public void GotContentNotification(Content p0, TrackingInfo p1)
            {
                XCContentNotification XContent = AdapterContent.GetCommonType(p0);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXContentNotification(XContent);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public void GotCouponNotification(Coupon p0, TrackingInfo p1)
            {
                XCCouponNotification XCoupon = AdapterCoupon.GetCommonType(p0);
                    
                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXCouponNotification(XCoupon);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public void GotCustomJSONNotification(CustomJSON p0, TrackingInfo p1)
            {
                XCCustomJSONNotification XCustomJSON = AdapterCustom.GetCommonType(p0);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXCustomJSONNotification(XCustomJSON);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public void GotFeedbackNotification(Feedback p0, TrackingInfo p1)
            {
                XCFeedbackNotification XFeedback = AdapterFeedback.GetCommonType(p0);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXFeedbackNotification(XFeedback);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }

            public void GotSimpleNotification(SimpleNotification p0, TrackingInfo p1)
            {
                XCSimpleNotification XSimple = AdapterSimple.GetCommonType(p0);

                if (NearPCL.GetContentManager() != null)
                {
                    NearPCL.GetContentManager().GotXSimpleNotification(XSimple);
                }
                else Console.WriteLine("You receive a content but you haven't registered a content manager");
            }
        }

    }
}

