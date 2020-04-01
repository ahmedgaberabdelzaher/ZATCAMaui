using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatBillDetailsPageView : ContentPage
    {
        ZakatBillDetailsPageViewModel viewModel;
        public ZakatBillDetailsPageView()
        {
            try
            {
                InitializeComponent();
            
                viewModel = App.Locator.ZakatBillDetailsPageView;
                this.BindingContext = viewModel;
                NavigationPage.SetBackButtonTitle(this, "");
                ChangeAeroIcon();
            }
            catch(Exception ex)
            {

            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
    }
}