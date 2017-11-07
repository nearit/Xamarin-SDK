using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinSample
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ICommand RefreshRecipes { get; private set; }
        public bool enabled;

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            RefreshRecipes = new Command(Refresh);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void Refresh()
        {
#if __IOS__
            XamarinSample.iOS.AppDelegate appd = new iOS.AppDelegate();
            appd.RefreshConfig();
#else
#if __ANDROID__
            XamarinSample.Droid.MainActivity ma = new Droid.MainActivity();
                    ma.RefreshConfig();
#endif
#endif

        }

        public void ForegroundNotification(bool val)
        {
#if __IOS__
            XamarinSample.iOS.AppDelegate appd = new iOS.AppDelegate();
            appd.EnableForegroundNotification(val);
#endif
        }
    }
}
