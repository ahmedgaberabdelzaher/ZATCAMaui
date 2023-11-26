using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentSignUP
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreatedSuccessfullyPageView : ContentPage
    {
        AccountCreatedSuccessfullyPageViewModel viewModel;
        public AccountCreatedSuccessfullyPageView(string Tin)
        {
            InitializeComponent();
            viewModel = App.Locator.AccountCreatedSuccessfullyPageView;
            BindingContext = App.Locator.AccountCreatedSuccessfullyPageView;
            viewModel.TINnumber = Tin;
        }
        private async void Image_Copy_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(Label_Tin.Text);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                var displayText = AppResources.TINS + " " + text;
                // viewModel._dialogService.ShowMessage(displayText, AppResources.Copied);
                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();
                headerAmountInfo.IsLinkAvailable = false;
                headerAmountInfo.Message = displayText;
                if (App.IsArabic)
                {
                    headerAmountInfo.FlowDirections = "RightToLeft";
                }
                else
                {
                    headerAmountInfo.FlowDirections = "LeftToRight";
                }
                headerWithInfos.Add(headerAmountInfo);
                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.Copied;
                await PopupNavigation.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        private void OnGoToLoginClicked(object sender, EventArgs e)
        {
            try { 
            if (Navigation.NavigationStack.Count > 0)
            {
                Xamarin.Forms.Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
                Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg1);
            }
            viewModel._navigationService.GoBack();
            }
            catch (Exception)
            {
                
            }
        }
    }
}