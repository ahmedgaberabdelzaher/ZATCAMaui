using GAZT;
using GAZT.Models;
using GAZTeServicesApp.ViewModels.LandingPage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZTeServicesApp.Views.LandingPage
{
    /// <summary>
    /// Page to show the article tile
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SFAnonymousLandingPageView : ContentPage
    {
        SFAnonymousLandingPageViewModel viewModel;
        //  Calendar appointments;
        /// <summary>
        /// Initializes a new instance of the <see cref="SFAnonymousLandingPageView" /> class.
        /// </summary>
        public SFAnonymousLandingPageView()
        {
            try
            {
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel = App.Locator.SFAnonymousLandingPageView;
                this.BindingContext = viewModel;
                LoadData();
                SetLTR();
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadData()
        {
            viewModel.PopulateeServicesApplicableToTheTaxPayer();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                SetLTR();
            }
            catch (Exception)
            {

            }
        }


        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }

        }

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SFLoginPageView,App.SFLandingPageView);
        }
      
        private void OnTappedeService(object sender, EventArgs e)
        {
            string controltype = sender.GetType().ToString();

            if (controltype == "Xamarin.Forms.Image")
            {
                Image arrowImage = sender as Image;
                eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                if (BModel.eServiceName == AppResources.VATDeclaration)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView,App.ICRListPageView);
                }

                if (BModel.eServiceName == AppResources.EstimateZakat)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.ZakatReturnListPageView);
                }
                
                if (BModel.eServiceName == AppResources.ZZFormBundleStatus)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.FormBundleStatusPageView);
                }

                if (BModel.eServiceName == AppResources.MyCertificate)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyCertificate);
                }

                if (BModel.eServiceName == AppResources.ZTINStatus)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CheckTINStatusPageView);
                }

                if (BModel.eServiceName == AppResources.ZZCorrespondence)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CorrespondancePageView);
                }

                if (BModel.eServiceName == AppResources.MyBills)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyBillsView);
                }

                if (BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                {
                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportTypePageView);
                }

                if (BModel.eServiceName == AppResources.VATLookup)
                {
                    viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                }
            }
            if (controltype == "Xamarin.Forms.Label")
            {
                Label arrowImage = sender as Label;
                eServiceInfo BModel = (eServiceInfo)arrowImage.BindingContext;
                if (BModel.eServiceName == AppResources.VATDeclaration)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.ICRListPageView);
                }

                if (BModel.eServiceName == AppResources.EstimateZakat)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.ZakatReturnListPageView);
                }

                if (BModel.eServiceName == AppResources.ZZFormBundleStatus)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.FormBundleStatusPageView);
                }

                if (BModel.eServiceName == AppResources.MyCertificate)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyCertificate);
                }

                if (BModel.eServiceName == AppResources.ZTINStatus)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CheckTINStatusPageView);
                }

                if (BModel.eServiceName == AppResources.ZZCorrespondence)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.CorrespondancePageView);
                }

                if (BModel.eServiceName == AppResources.MyBills)
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.MyBillsView);
                }

                if (BModel.eServiceName == AppResources.ZTEReportReportScreenTitle)
                {
                    viewModel._navigationService.NavigateTo(App.TaxEvasionReportTypePageView);
                }

                if (BModel.eServiceName == AppResources.VATLookup)
                {
                    viewModel._navigationService.NavigateTo(App.VATLookupPageView);
                }
            }
        }

        private void OptionMenuClicked(object sender, EventArgs e)
        {
            ComingToOptionScreenFrom comingToOptionScreenFrom = ComingToOptionScreenFrom.IsAnonymousPage;
            viewModel._navigationService.NavigateTo(App.SFOptionsPageView, comingToOptionScreenFrom);
        }
    }
}