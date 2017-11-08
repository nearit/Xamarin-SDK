using System;
using Xamarin.Forms;
using XamarinBridge.PCL;
using XamarinBridge.PCL.Manager;

namespace NearForms
{
    public partial class NearFormsPage : ContentPage, INearFunc
    {
        public NearFormsPage()
        {
            InitializeComponent();
        }

        public void Refresh(object sender, EventArgs args)
        {
            RefreshConfiguration();
        }

        public void RefreshConfiguration()
        {
            NearPCL.RefreshConfiguration();
        }
    }
}
