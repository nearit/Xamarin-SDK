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
            System.Diagnostics.Debug.WriteLine("App.xaml");
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
            System.Diagnostics.Debug.WriteLine("Content");
        }

        public void GotXCouponNotification(XCCouponNotification notification)
        {
            throw new System.NotImplementedException();
        }

        public void GotXCustomJSONNotification(XCCustomJSONNotification notification)
        {
            System.Diagnostics.Debug.WriteLine("Custom");
        }

        public void GotXSimpleNotification(XCSimpleNotification notification)
        {
            throw new System.NotImplementedException();
        }

        public void GotXFeedbackNotification(XCFeedbackNotification notification)
        {
            System.Diagnostics.Debug.WriteLine("feedback");
        }
    }
}
