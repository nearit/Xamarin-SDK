using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinBridge.PCL;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL.Types;

namespace Sample
{
    public partial class MainPage : ContentPage, IContentManager
    {
        public MainPage()
        {
            InitializeComponent();

            NearPCL.SetContentManager(this);
        }

        public void GotXContentNotification(XCContentNotification notification)
        {
            throw new NotImplementedException();
        }

        public void GotXCouponNotification(XCCouponNotification notification)
        {
            throw new NotImplementedException();
        }

        public void GotXCustomJSONNotification(XCCustomJSONNotification notification)
        {
            throw new NotImplementedException();
        }

        public void GotXFeedbackNotification(XCFeedbackNotification notification)
        {
            throw new NotImplementedException();
        }

        public void GotXSimpleNotification(XCSimpleNotification notification)
        {
            throw new NotImplementedException();
        }
    }
}
