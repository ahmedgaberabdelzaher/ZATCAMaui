using System;
using System.Collections.Generic;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentAmendUpdatePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablishmentAmendUpdateSuccessfulPage : ContentPage
    {
        private EstablishmentAmendUpdateSuccessfulPageViewModel viewModel;
        public EstablishmentAmendUpdateSuccessfulPage(TaxPayerDetails taxpayerProfile)
        {
            InitializeComponent(); SetLTR();
            viewModel = App.Locator.EstablishmentAmendUpdateSuccessfulPage;
            viewModel.taxPayerDetails = taxpayerProfile;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.OnAppearing();
        }


        private void GoToDashBoardButtonClick(object sender, EventArgs e)
        {
            try
            {
                //if (Navigation.NavigationStack.Count > 0)
                //{
                //    int PageCount = Navigation.NavigationStack.Count;
                //    int indexToRemoveThePage = PageCount - 2;
                //    if (!string.IsNullOrEmpty(viewModel.taxPayerDetails?.Fbsta) && viewModel.taxPayerDetails?.Fbsta != "IP011")
                //    {
                //        try
                //        {
                //            Xamarin.Forms.Page pg = Navigation.NavigationStack[indexToRemoveThePage];
                //            Navigation.RemovePage(pg);
                //            indexToRemoveThePage = Navigation.NavigationStack.Count - 1;
                //            Xamarin.Forms.Page pg1 = Navigation.NavigationStack[indexToRemoveThePage];
                //            Navigation.RemovePage(pg1);
                //        }
                //        catch (Exception ex)
                //        {

                //        }

                //        // viewModel._navigationService.GoBack();

                //    }
                //    else
                //    {
                //        Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                //        Navigation.RemovePage(pg);
                //        viewModel._navigationService.GoBack();

                //        //Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                //        //Navigation.RemovePage(pg1);
                //    }

                //}
                //if (Navigation.NavigationStack.Count > 0)
                //{
                //    foreach (var item in Navigation.NavigationStack)
                //    {
                //        if (Navigation.NavigationStack.Count > 1)
                //        {
                //            Navigation.RemovePage(item);
                //        }
                //    }
                //}

                try
                {
                    var firstPageToRemove = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                    Navigation.RemovePage(firstPageToRemove);

                    viewModel._navigationService.GoBack();
                }
                catch (Exception ex)
                {

                }


            }
            catch (Exception ex)
            {

            }

        }

        //private void GoToDashBoardButtonClick(object sender, EventArgs e)
        // {
        //     if (Navigation.NavigationStack.Count > 0)
        //     {
        //         Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
        //         Navigation.RemovePage(pg);
        //         Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
        //         Navigation.RemovePage(pg1);
        //     }
        //     viewModel._navigationService.GoBack();
        // }

        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
    }
}
