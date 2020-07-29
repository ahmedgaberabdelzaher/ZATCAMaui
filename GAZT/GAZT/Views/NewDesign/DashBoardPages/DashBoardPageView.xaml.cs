using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignDashBoardPageView : ContentPage
    {

        #region Variable
        GAZTNewDesignDashBoardPageViewModel viewModel;
        #endregion

        public GAZTNewDesignDashBoardPageView()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                
                viewModel = App.Locator.GAZTNewDesignDashBoardPageView;
                this.BindingContext = viewModel;


                if (App.TP != null)
                    viewModel.TaxPayerProfile = App.TP;

                MenuView.IsVisible = false;
                HomeView.IsVisible = true;
            }
            catch(Exception ex)
            {

            }
        }

        #region Method

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

            SetLTR();
            App.IsComingFromSleepMode = false;

            MenuView.IsVisible = false;
            HomeView.IsVisible = true;            

            Task.Run(async () =>
            {
                await LoadData();
                viewModel.IsLoading = false;
            });
        }
        private async Task LoadData()
        {
            try
            {
                await viewModel.LoadDashboardData();

                viewModel.PopulateBillsInformation();
                viewModel.PopulateReturnsInformation();
                viewModel.PopualateCommittmentsInformation();
               // viewModel.PopulateeServicesApplicableToTheTaxPayer();
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