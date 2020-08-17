using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    
    public partial class NewTaxEvasionFormSuccessPaveView : ContentPage
    {
        NewTaxEvasionFormPageViewModel viewModel;
        public NewTaxEvasionFormSuccessPaveView()
        {
            InitializeComponent();
            viewModel = App.Locator.NewTaxEvasionFormSuccessPaveView;
            this.BindingContext = viewModel;
            SetLTR();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //try
            //{
            //    var existingPages = Navigation.NavigationStack.ToList();

            //    foreach (var page in existingPages)
            //    {
            //        if (page.GetType().Name != App.NewTaxEvasionFormPageView)
            //        {
            //            Navigation.RemovePage(page);

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {                try
                {

                var _navigation = Application.Current.MainPage.Navigation;
                var _lastPage = _navigation.NavigationStack.LastOrDefault();
                //Remove last page
                _navigation.RemovePage(_lastPage);
                //Go back 
                _navigation.PopAsync();
            }
            catch (Exception ex)
                { 
                
                }
         

        }


        protected override bool OnBackButtonPressed() => true;

        private void btnDashboard_Clicked(object sender, EventArgs e)
        {

          viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
        }
    }
}
