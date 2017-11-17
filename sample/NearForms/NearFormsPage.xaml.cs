using Xamarin.Forms;
using XamarinBridge.PCL;
using XamarinBridge.PCL.Manager;
using XamarinBridge.PCL.Types;

namespace NearForms
{
    public partial class NearFormsPage : ContentPage
    {

        public NearFormsPage()
        {
            InitializeComponent();
        }

        public void OptOut(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("qwerty");
            NearPCL.OptOut();
        }




    }
}
