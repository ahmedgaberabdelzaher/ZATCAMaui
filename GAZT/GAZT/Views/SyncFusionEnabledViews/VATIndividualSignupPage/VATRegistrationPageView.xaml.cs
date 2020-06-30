using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATRegistrationPageView : ContentPage
    {
        VATRegistrationPageViewModel viewModel;
        public VATRegistrationPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationPageView;
            this.BindingContext = viewModel;
            viewModel.IsInstrunctionVisible = true;
            viewModel.CurrentStep = "Step2";
            SetfirstBoxColor();
            App.IsArabic = false;
            SetLTR();

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void DpEStartDate_Closed(object sender, EventArgs e)
        {

        }

        private void DpEStartDate_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DpEStartDate_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            DpEStartDate.IsOpen = true;
        }

        private void btnImporter_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView());
        }

        private void btnExporter_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView());
        }

        private void NewAccount_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new NewAccountPopUpPageView());
        }



        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            if(viewModel.CurrentStep == "Step1")
            {
                viewModel.CurrentStep = "Step2";
                viewModel.SetVisibility();
                viewModel.IsTaxPayersVisible = true;
                SetsecondBoxColor();
            }
            else if(viewModel.CurrentStep == "Step2")
            {
                viewModel.CurrentStep = "Step3";
                viewModel.SetVisibility();
                viewModel.IsTaxPayersVisible = true;
                //viewModel.IsSalesVisible = true;
                SetsecondBoxColor();
                //SetthirdBoxColor();
            }
            else if (viewModel.CurrentStep == "Step3")
            {
                viewModel.CurrentStep = "Step4";
                viewModel.SetVisibility();
                //viewModel.IsExpensesVisible = true;
                viewModel.IsSalesVisible = true;
                //SetfourthBoxColor();
                SetthirdBoxColor();
            }
            else if (viewModel.CurrentStep == "Step4")
            {
                viewModel.CurrentStep = "Step5";
                viewModel.SetVisibility();
                //viewModel.IsFinancialVisible = true;
                viewModel.IsFinancialVisible = true;
                //SetfifthBoxColor();
                SetfourthBoxColor();
            }
            else if (viewModel.CurrentStep == "Step5")
            {
                viewModel.CurrentStep = "Submit";
                viewModel.SetVisibility();
                //viewModel.IsSummaryVisible = true;
                viewModel.IsSummaryVisible = true;
                //SetfifthBoxColor();
                SetfifthBoxColor();
            }
            else if (viewModel.CurrentStep == "Submit")
            {
                viewModel._navigationService.NavigateTo(App.VATRegistrationSuccessfullPageView);
            }

            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetVatRegistrationData();

        }
        public async Task GetVatRegistrationData()
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {
                await viewModel.onPageLoad();
            });
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private void DateEntry_Focused(object sender, FocusEventArgs e)
        {

        }

        private void DateEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void DpEStartDate_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void EntryTINNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryTINNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnID_Clicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
        }

        private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void EntryIDNo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryFirstName_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryLastName_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnContactID_Clicked(object sender, EventArgs e)
        {
            DDlContactIDType.IsOpen = true;
        }
        #region SetColor
        public void SetfirstBoxColor()
        {
            BoxOne.BackgroundColor = Color.DarkGreen;
            BoxTwo.BackgroundColor = Color.LightGray;
            BoxThree.BackgroundColor = Color.LightGray;
            BoxFour.BackgroundColor = Color.LightGray;
            BoxFive.BackgroundColor = Color.LightGray;

        }
        public void SetsecondBoxColor()
        {
            BoxOne.BackgroundColor = Color.LightGray;
            BoxTwo.BackgroundColor = Color.DarkGreen;
            BoxThree.BackgroundColor = Color.LightGray;
            BoxFour.BackgroundColor = Color.LightGray;
            BoxFive.BackgroundColor = Color.LightGray;
        }
        public void SetthirdBoxColor()
        {
            BoxOne.BackgroundColor = Color.LightGray;
            BoxTwo.BackgroundColor = Color.LightGray;
            BoxThree.BackgroundColor = Color.DarkGreen;
            BoxFour.BackgroundColor = Color.LightGray;
            BoxFive.BackgroundColor = Color.LightGray;
        }
        public void SetfourthBoxColor()
        {
            BoxOne.BackgroundColor = Color.LightGray;
            BoxTwo.BackgroundColor = Color.LightGray;
            BoxThree.BackgroundColor = Color.LightGray;
            BoxFour.BackgroundColor = Color.DarkGreen;
            BoxFive.BackgroundColor = Color.LightGray;
        }
        public void SetfifthBoxColor()
        {
            BoxOne.BackgroundColor = Color.LightGray;
            BoxTwo.BackgroundColor = Color.LightGray;
            BoxThree.BackgroundColor = Color.LightGray;
            BoxFour.BackgroundColor = Color.LightGray;
            BoxFive.BackgroundColor = Color.DarkGreen;
        }

        #endregion

        private void TappedOnBackButton(object sender, EventArgs e)
        {
            if(viewModel.IsTaxPayersVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsInstrunctionVisible = true;
                viewModel.CurrentStep = "Step2";
                SetfirstBoxColor();
            }
            else if(viewModel.IsSalesVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsTaxPayersVisible = true;
                viewModel.CurrentStep = "Step3";
                SetsecondBoxColor();
            }
            else if (viewModel.IsFinancialVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsSalesVisible = true;
                viewModel.CurrentStep = "Step4";
                SetthirdBoxColor();
            }
            else if (viewModel.IsSummaryVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsFinancialVisible = true;
                viewModel.CurrentStep = "Step5";
                SetfourthBoxColor();
            }
            //else if (viewModel.CurrentStep == "Submit")
            //{
            //    viewModel.SetVisibility();
            //   // viewModel.IsSummaryVisible = true;
            //    viewModel.IsFinancialVisible = true;
            //    viewModel.CurrentStep = "Step5";
            //    SetfifthBoxColor();
            //}
        }

        private void btnAttachmentDocuments_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView());
        }

        private void TappedOnMenu(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new VATRegistrationMenuPopUp());
        }

        private void VATFaqTapped(object sender, EventArgs e)
        {

        }
    }
}