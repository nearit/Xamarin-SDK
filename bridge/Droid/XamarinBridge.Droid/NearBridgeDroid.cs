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

[assembly: Dependency(typeof(XamarinBridge.Droid.NearBridgeDroid))]
namespace XamarinBridge.Droid
{
    [MetaData("near_api_key", Value = "@string/nearit_api_key")]
    public class NearBridgeDroid : INearFunc
    {
        int count = 1;

        private static ICoreContentsListener _coreContentListener = new EventContent();
        private static IRecipeRefreshListener _refreshListener = new RefreshListener();
        private static ICouponListener _couponListener = new CouponListener();
        private static NearItUserProfile.IProfileFetchListener _profileListener = new ProfileListener();
        private static IOptOutNotifier _optOutListener = new OptOutListener();

        public static void ParseIntent(Intent intent)
        {
            NearUtils.ParseCoreContents(intent, _coreContentListener);
        }

        public static void ParseForegroundEvent(IParcelable parcelable, TrackingInfo track)
        {
            NearUtils.ParseCoreContents(parcelable, track, _coreContentListener);
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

        public void GetCoupon()
        {
            NearItManager.Instance.GetCoupons(_couponListener);
        }

        public void SetUserData(string key, string value)
        {
            NearItManager.Instance.SetUserData(key, value);
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

        public void OptOut()
        {
            NearItManager.Instance.InvokeOptOut(_optOutListener);
        }

        internal class OptOutListener : Java.Lang.Object, IOptOutNotifier
        {
            public void OnFailure(string p0)
            {
                Console.WriteLine("Error OptOut android");
            }

            public void OnSuccess()
            {
                Console.WriteLine("OptOut success android");
            }
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

