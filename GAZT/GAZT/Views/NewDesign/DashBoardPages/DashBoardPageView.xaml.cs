using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoardPageView : ContentPage
    {

        #region Variable
        DashBoardPageViewModel viewModel;
        #endregion

        public DashBoardPageView()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                viewModel = App.Locator.DashBoardPageView;
                this.BindingContext = viewModel;
                MenuView.IsVisible = false;
                HomeView.IsVisible = true;
            }
            catch(Exception ex)
            {

            }
        }

        #region Method

        public async Task LoadDuesData()
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await viewModel.DuesData();
                    if (viewModel.listofPaymentReturn != null && viewModel.listofPaymentReturn.Count != 0)
                    {
                        List<OverduePaymentsAndUnSubmittedReturn> sortedList = new List<OverduePaymentsAndUnSubmittedReturn>();
                        viewModel.listofPaymentReturn = viewModel.listofPaymentReturn.OrderBy(icr => DateTime.Parse(icr.DueDate)).ToList();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            viewModel.CommitmentReturnsList = viewModel.listofPaymentReturn;
                            //  ReturnsList.ItemsSource = viewModel.listofPaymentReturn;
                        });
                        await Task.Delay(3000);
                        viewModel.IsListviewVisible = true;
                        viewModel.IsNoDuesLabelVisible = false;
                    }
                    else
                    {
                        viewModel.IsListviewVisible = false;
                        viewModel.IsNoDuesLabelVisible = true;
                    }
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception ex)
            {
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            HomeView.IsVisible = false;
            MenuView.IsVisible = true;
            HomeIndicator.BackgroundColor = Color.White;
            MenuIndicator.BackgroundColor = Color.DarkGreen;
            Tabbar.BorderColor = Color.Transparent;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MenuView.IsVisible = false;
            HomeView.IsVisible = true;

            App.IsComingFromSleepMode = false;
            SetLTR();
            viewModel.TaxPayerProfile = App.TP;
            LoadDuesData();
            LoadData();
        }
        private async Task LoadData()
        {
            try
            {
                await viewModel.LoadDashboardData();
                viewModel.PopulateReturnsInformation();
                viewModel.PopulateBillsInformation();
                // viewModel.PopulateBillsAndReturnsSchedule();
                viewModel.PopulateeServicesApplicableToTheTaxPayer();
            }
            catch (Exception ex)
            {
            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        

        #endregion


    }
}