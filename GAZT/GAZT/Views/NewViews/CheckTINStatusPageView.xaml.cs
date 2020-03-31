using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckTINStatusPageView : ContentPage
    {
        ChecKTINStatusViewModel viewModel;
        public CheckTINStatusPageView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.CheckTINStatusPageView;
            this.BindingContext = viewModel;

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.OnPageLoad();
            SetLTR();
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void ListTINStatus_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            return;
        }
    }
}