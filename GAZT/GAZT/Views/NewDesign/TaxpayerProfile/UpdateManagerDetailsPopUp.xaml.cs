using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.TaxpayerProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class UpdateManagerDetailsPopUp : PopupPage

    {
        UpdateManagerViewModel viewModel;
        public UpdateManagerDetailsPopUp()
        {
            InitializeComponent();
            viewModel = App.Locator.UpdateManagerPopUp;
            this.BindingContext = viewModel;

            //SetLTR();
            this.FlowDirection = UtilityManager.SetLTRAndRTL();

            viewModel.LoadManagerDetails();
        }
        async void OnUpdateBtnClicked(System.Object sender, System.EventArgs e)
        {
            try
            {
               await viewModel.PrepareDataForSubmit();
            }
            catch (Exception)
            {
                viewModel.IsLoading = false;
            }
        }
        async void OnBackArrowTapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception)
            {
                viewModel.IsLoading = false;
            }
        }
        
        void SfButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void OnDiscardBtnClicked(System.Object sender, System.EventArgs e)
        {
            await viewModel.LoadManagerDetails();
        }
    }
}

