using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatObjection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionPageView : ContentPage
    {
        private ZakatObjectionViewModel viewModel;
        public ZakatObjectionPageView()
        {
            InitializeComponent();
            
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel = App.Locator.ZakatObjectionView;
            this.BindingContext = viewModel;

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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
        
        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void OnIDNumberFocusChanged(object sender, FocusEventArgs e)
        {
            
        }

        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            
        }
    }
}