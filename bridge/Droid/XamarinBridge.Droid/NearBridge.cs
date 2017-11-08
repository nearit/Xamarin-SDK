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
using IT.Near.Sdk.Operation;
using XamarinBridge.PCL.Types;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL;
using XamarinBridge.Droid.Adapter;
using System.Collections.Generic;

[assembly: Dependency(typeof(XamarinBridge.Droid.NearBridge))]
namespace XamarinBridge.Droid
{
    public class NearBridge : INearFunc
    {
        int count = 1;

        private static ICoreContentsListener _coreContentListener = new EventContent();
        private static IRecipeRefreshListener _refreshListener = new RefreshListener();
        private static ICouponListener _couponListener = new CouponListener();
        private static IUserDataNotifier _userDataListener = new UserDataListener();
        private static NearItUserProfile.IProfileFetchListener _profileListener = new ProfileListener();

        public static void ParseIntent(Intent intent)
        {
            NearUtils.ParseCoreContents(intent, _coreContentListener);
        }

        public void RefreshConfiguration()
        {
            NearItManager.Instance.RefreshConfigs(_refreshListener);
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
            if(ev.GetPluginName() == XCFeedbackEvent.PluginName)
            {
                XCFeedbackEvent feedbackEvent = (XamarinBridge.PCL.Types.XCFeedbackEvent)ev;

                XCFeedbackNotification feedback = feedbackEvent.FeedbackNotification;
                Feedback nativeFeedback = new Feedback();

                nativeFeedback = AdapterFeedback.GetNativeFeeback(nativeFeedback, feedback);

                FeedbackEvent nativeFeedbackEvent = new FeedbackEvent(nativeFeedback, feedbackEvent.rating, feedbackEvent.comment);
                NearItManager.Instance.SendEvent(nativeFeedbackEvent);
            }
            else
            {
                Console.WriteLine("Error SendEvent android");
            }
        }

        public void GetCoupon()
        {
            NearItManager.Instance.GetCoupons(_couponListener);
        }

        public void SetUserData(string key, string value)
        {
            NearItManager.Instance.SetUserData(key,value, _userDataListener);
        }

        public void GetProfileId()
        {
            NearItManager.Instance.GetProfileId(_profileListener);
        }

        public void SetProfileId(string profile)
        {
            NearItManager.Instance.ProfileId = profile;
        }

        public void ResetProfileId()
        {
            NearItManager.Instance.ResetProfileId(_profileListener);
        }





        internal class ProfileListener : Java.Lang.Object, NearItUserProfile.IProfileFetchListener
        {
            public void OnError(string p0)
            {
                Console.WriteLine("Error Profile android");
            }

            public void OnProfileId(string p0)
            {
                Console.WriteLine("Profile success android");
            }
        }

        internal class UserDataListener : Java.Lang.Object, IUserDataNotifier
        {
            public void OnDataCreated()
            {
                Console.WriteLine("UserData created android");
            }

            public void OnDataNotSetError(string p0)
            {
                Console.WriteLine("Error UserData android");
            }
        }

        internal class CouponListener : Java.Lang.Object, ICouponListener
        {
            public void OnCouponDownloadError(string p0)
            {
                Console.WriteLine("GetCoupon android");
            }

            public void OnCouponsDownloaded(IList<Coupon> p0)
            {
                Console.WriteLine("Error GetCoupon android");
            }
        }

        internal class RefreshListener : Java.Lang.Object, IRecipeRefreshListener
        {
            public void OnRecipesRefresh()
            {
                Console.WriteLine("Refresh android");
            }

            public void OnRecipesRefreshFail()
            {
                Console.WriteLine("Error Refresh android");
            }
        }

        internal class EventContent : Java.Lang.Object, ICoreContentsListener
        {
            public void GotContentNotification(Content p0, TrackingInfo p1)
            {
                Content ContentNotification = p0;
                XCContentNotification XContent = new XCContentNotification();

                XContent.NotificationMessage = ContentNotification.NotificationMessage;
                XContent.Title = ContentNotification.Title;
                XContent.Content = ContentNotification.ContentString;
                XContent.ImageLink.FullSize = ContentNotification.ImageLink.FullSize;
                XContent.ImageLink.SmallSize = ContentNotification.ImageLink.SmallSize;
                XContent.Cta.Label = ContentNotification.Cta.Label;
                XContent.Cta.Url = ContentNotification.Cta.Url;

                // TODO null check
                NearPCL.GetContentManager().GotXContentNotification(XContent);
            }

            public void GotCouponNotification(Coupon p0, TrackingInfo p1)
            {
                Coupon CouponNotification = p0;
                XCCouponNotification XCoupon = new XCCouponNotification();

                XCoupon.NotificationMessage = CouponNotification.NotificationMessage;
                XCoupon.Description = CouponNotification.Description;
                XCoupon.Value = CouponNotification.Value;
                XCoupon.ExpiresAt = CouponNotification.ExpiresAt;
                XCoupon.ReedemableFrom = CouponNotification.RedeemableFrom;
                XCoupon.IconSet.FullSize = CouponNotification.IconSet.FullSize;
                XCoupon.IconSet.SmallSize = CouponNotification.IconSet.SmallSize;

                NearPCL.GetContentManager().GotXCouponNotification(XCoupon);
            }

            public void GotCustomJSONNotification(CustomJSON p0, TrackingInfo p1)
            {
                CustomJSON CustomJSONNotification = p0;
                XCCustomJSONNotification XCustomJSON = new XCCustomJSONNotification();

                XCustomJSON.NotificationMessage = CustomJSONNotification.NotificationMessage;
                XCustomJSON.Content = (System.Collections.Generic.Dictionary<string, object>)CustomJSONNotification.Content;

                NearPCL.GetContentManager().GotXCustomJSONNotification(XCustomJSON);
            }

            public void GotFeedbackNotification(Feedback p0, TrackingInfo p1)
            {
                Feedback FeedbackNotification = p0;
                XCFeedbackNotification XFeedback = new XCFeedbackNotification();

                XFeedback.NotificationMessage = FeedbackNotification.NotificationMessage;
                XFeedback.Question = FeedbackNotification.Question;
                XFeedback.RecipeId = p1.RecipeId;

                NearPCL.GetContentManager().GotXFeedbackNotification(XFeedback);
            }

            public void GotSimpleNotification(SimpleNotification p0, TrackingInfo p1)
            {
                SimpleNotification SimpleNotification = p0;
                XCSimpleNotification XSimple = new XCSimpleNotification();

                XSimple.NotificationMessage = SimpleNotification.NotificationMessage;

                NearPCL.GetContentManager().GotXSimpleNotification(XSimple);
            }
        }

    }
}

