
using Mopups.Services;
using System.Collections.ObjectModel;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.TaxpayerProfile
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxpayerProfilePageView : ContentPage
    {
        NewTaxpayerProfileViewModel viewModel;
        ObservableCollection<InternationalMobileData> mobileData = null;

        public TaxpayerProfilePageView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            viewModel = App.Locator.TaxpayerProfilePageView;
            BindingContext = viewModel;

            TpProfileTaxpaayertypeRefresh();
            try
            {
                viewModel.ResidenceText = App.TP.TpType;
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
                TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileDataAPICall(App.TP.Tin);

                if (TPProfile != null)
                {
                    if (App.TP != null)
                    {
                        App.TP.TpType = TPProfile.TpType;
                        viewModel.ResidenceText = App.TP.TpType;
                    }

                }
                viewModel.IsLoading = false;
            }
            catch
            {
                viewModel.IsLoading = false;
            }
        }

        private void OnMobileEditTapped(object sender, EventArgs e)
        {
            viewModel.IsLoading = true;

            try
            {
                if (mobileData == null)
                    mobileData = WebServiceManager.GAZTGetMobileRegionDropdown();

                viewModel.IsLoading = false;
                MopupService.Instance.PushAsync(new UpdateMobilePopUp(mobileData));
            }
            catch (Exception)
            {



                viewModel.IsLoading = false;
            }
        }

        private void OnEmailEditTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new UpdateEmailPopUp());
        }

        private void OnPasswordEditTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new UpdatePasswordPopUp());
        }
        private void OnManagerDetailsTapped(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new UpdateManagerDetailsPopUp());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {


                if (App.TP != null)
                {
                    //viewModel.TPProfileNameLbl = App.TP.Name;

                    if (App.TP.TypeChk == "X")
                    {
                        viewModel.TPProfileNameLbl = App.TP.NameFirst + " " + App.TP.NameLast;
                    }
                    else
                    {
                        viewModel.TPProfileNameLbl = App.TP.NameOrg1;
                    }

                    viewModel.TINLabel = App.TP.Tin;
                    try
                    {
                        if (!string.IsNullOrEmpty(App.TP.Mobile))
                        {
                            if (App.TP.Mobile.Length < 12)
                                viewModel.MobileNumber = "+966" + App.TP.Mobile.Remove(0, 2);
                            else
                                viewModel.MobileNumber = "+" + App.TP.Mobile.Remove(0, 2);

                        }
                    }
                    catch (Exception)
                    {


                    }


                    viewModel.EmailEntry = App.TP.Email;
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