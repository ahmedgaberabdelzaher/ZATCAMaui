
using Mopups.Services;
using System.Collections.ObjectModel;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerProfile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerProfilePageView : ContentPage
    {
        NewTaxpayerProfileViewModel viewModel;
        

        public TaxpayerProfilePageView()
        {
            InitializeComponent();

            viewModel = App.Locator.TaxpayerProfilePageView;
            BindingContext = viewModel;

            TpProfileTaxpaayertypeRefresh();
            try
            {
                viewModel.ResidenceText = App.TP.taxpayerType;
            }
            catch (Exception)
            {


            }
        }



        public async void TpProfileTaxpaayertypeRefresh()
        {
            try
            {

                viewModel.IsLoading = true;
                TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileAndUpdatePasswordAPICall(App.TP.TIN);

                if (TPProfile != null)
                {
                    
                    if (App.TP != null)
                    {
                        App.TP.taxpayerType = TPProfile.taxpayerType;
                        viewModel.ResidenceText = App.TP.taxpayerType;
                    }

                }
                viewModel.IsLoading = false;
            }
            catch
            {
                viewModel.IsLoading = false;
            }
        }

      
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {


                if (App.TP != null)
                {

                    if (App.TP.typeCheck == "X")
                    {
                        viewModel.TPProfileNameLbl = App.TP.TpTitle + " " + App.TP.firstName + " " + App.TP.lastName;
                    }
                    else
                    {
                        viewModel.TPProfileNameLbl = App.TP.organizationName;
                    }

                    viewModel.TINLabel = App.TP.TIN;
                    if (!string.IsNullOrEmpty(App.TP.mobile))
                    {
                        if (App.TP.mobile.Length < 12)
                            viewModel.MobileNumber = "+966" + App.TP.mobile.Remove(0, 2);
                        else
                            viewModel.MobileNumber = "+" + App.TP.mobile.Remove(0, 2);

                    }


                    viewModel.EmailEntry = App.TP.email;
                    viewModel.PasswordEntry = "********";

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel.GetTinStatusDATA();
                    });


                }
            }
            catch (Exception)
            {
            }

        }
    }
}