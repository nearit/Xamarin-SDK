using Xamarin.Forms;
using XamarinBridge.PCL;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL.Types;

namespace NearForms
{
    public partial class App : Application, IContentManager
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NearFormsPage();
            NearPCL.SetContentManager(this);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void GotXContentNotification(XCContentNotification notification)
        {
        }

        public void GotXCouponNotification(XCCouponNotification notification)
        {
        }

        public void GotXCustomJSONNotification(XCCustomJSONNotification notification)
        {
        }

        public void GotXSimpleNotification(XCSimpleNotification notification)
        {
        }

        public void GotXFeedbackNotification(XCFeedbackNotification notification)
        {
            System.Diagnostics.Debug.WriteLine("Noti message: ");
            System.Diagnostics.Debug.WriteLine(notification.NotificationMessage);
            System.Diagnostics.Debug.WriteLine("Question: ");
            System.Diagnostics.Debug.WriteLine(notification.Question);
        }
    }
}
