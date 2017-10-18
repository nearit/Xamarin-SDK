using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinSample
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            if (e.Value == true)
            {
                XamarinSample.ItemsViewModel ivm = new ItemsViewModel();
                ivm.ForegroundNotification(true);
            }
            else
            {
                XamarinSample.ItemsViewModel ivm = new ItemsViewModel();
                ivm.ForegroundNotification(false);
            }
        }
    }
}
