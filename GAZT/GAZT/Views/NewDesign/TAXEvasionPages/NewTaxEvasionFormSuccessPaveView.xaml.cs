using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [Preserve(AllMembers = true)]
    public partial class NewTaxEvasionFormSuccessPaveView : ContentPage
    {
        NewTaxEvasionFormPageViewModel viewModel;
        public NewTaxEvasionFormSuccessPaveView()
        {
            InitializeComponent();
            viewModel = App.Locator.NewTaxEvasionFormSuccessPaveView;
            this.BindingContext = viewModel;
            SetLTR();
            
            if (App.TP != null)
            {
                if (!string.IsNullOrEmpty(App.TP.Tin))
                {
                    btnDashboard.Text = AppResources.ZZZZGotoDashboard;
                }
                else
                {
                    btnDashboard.Text = AppResources.NDBacktoLoginnew;
                }
            }
            else
                btnDashboard.Text = AppResources.NDBacktoLoginnew;
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
      

        protected override bool OnBackButtonPressed() => true;
       
        
        private void btnDashboard_Clicked(object sender, EventArgs e)
        {
            if (App.TP != null)
            {
                if (!string.IsNullOrEmpty(App.TP.Tin))
                {
                    viewModel._navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                }
                else
                {
                    viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                }
            }
            else
                viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
        }

    private void OnGotoReportPageClicked(object sender, EventArgs e)
    {
            try
            {
                if (Navigation.NavigationStack.Count > 0)
                {
                    Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(pg);
                }
                viewModel._navigationService.GoBack();
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
           
        }

    
}
}
