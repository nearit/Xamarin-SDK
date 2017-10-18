using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace XamarinSample
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://www.nearit.com/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}
