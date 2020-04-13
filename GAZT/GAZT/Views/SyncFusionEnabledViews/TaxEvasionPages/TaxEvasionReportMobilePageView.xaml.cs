using GAZT.Helper;
using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using Foundation;

namespace GAZT.Views.SyncFusionEnabledViews.TaxEvasionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class TaxEvasionReportMobilePageView : ContentPage
    {
        
            TaxEvasionReportMobilePageViewModel viewModel;

        public TaxEvasionReportMobilePageView()
        {
            InitializeComponent();
            //viewModel = App.Locator.TaxEvasionReportPhonePageView;
         this.BindingContext = viewModel;
            viewModel = App.Locator.TaxEvasionReportPhonePageView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            //  On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);


            if (App.IsArabic)
            {

                this.FlowDirection = FlowDirection.RightToLeft;


            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }

            DependencyService.Get<IStatusBar>().HideStatusBar();


            this.BindingContext = viewModel;
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

        private void Mobile_Entry_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(Mobile_Entry.Text))

            {
                if (Mobile_Entry.Text.Length != 8)
                {
                    EmailInputLayout.HasError = true;
                    viewModel.IsVerifyEnable = false;

                }
                else
                {
                    viewModel.IsVerifyEnable = true;
                    EmailInputLayout.HasError = false;
                }
            }
            

        }
    }
    

}