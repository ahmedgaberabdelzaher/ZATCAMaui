using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
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
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel.SetVisibility();
            viewModel.IsInstrunctionVisible = true;
            viewModel.CurrentStep = "Step2";
            SetfirstBoxColor();
            viewModel.IsNewAccountClicked = false;
            viewModel.NewAccountText = AppResources.ZTERNewAccount;
            // App.IsArabic = false;
            // App.IsArabic = false;
            viewModel.SetDefaultDate();
            SetLTR();

        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }

        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        private void DpEStartDate_Closed(object sender, EventArgs e)
        {

        }

        private void DpEStartDate_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = DpEStartDate.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.VatEligibleStartDate = day+"/"+ month+"/"+ year;
      //          string DOB = year + month + day;
     
            }
            catch (Exception ex)
            {

            }
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
            VATRegistrationDetails vatReg = null;
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void btnExporter_Clicked(object sender, EventArgs e)
        {
            VATRegistrationDetails vatReg = null;
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void NewAccount_Clicked(object sender, EventArgs e)
        {
           // viewModel.IsNewAccountClicked = true;
            PopupNavigation.Instance.PushAsync(new NewAccountPopUpPageView(viewModel.VATRegistrationDetailsData.d.OptIban));
        }

        public void triggerIban(string messagestring)
        {
            try
            {
                string message = messagestring;
                if (!string.IsNullOrEmpty(message))
                {
                    bool isExist = false;
                    if (!string.IsNullOrEmpty(message))
                    {
                        List<Result2> results1D = new List<Result2>();
                        foreach (var item in viewModel.IbanList)
                        {
                            Result2 result = new Result2();
                            result = item;
                            
                            if (string.IsNullOrEmpty(item.Bkvid))
                            {
                                isExist = true;
                                result.Iban = message;
                                //item.Iban = message;
                                viewModel.VATRegistrationDetailsData.d.OptIban = message;
                                viewModel.NewAccountText = "Edit Account";
                            }
                            results1D.Add(result);
                        }

                        if (results1D != null && results1D.Count != 0)
                        {
                            if(viewModel.IbanList!=null)
                            {
                                viewModel.IbanList.Clear();
                            }
                            viewModel.IbanList = null;
                            viewModel.IbanList = new ObservableCollection<Result2>(results1D);
                        }


                        if (!isExist)
                        {
                            viewModel.VATRegistrationDetailsData.d.OptIban = message;
                            Result2 result2 = new Result2();
                            result2.Iban = message;
                            List<Result2> results = new List<Result2>();
                            results.Add(result2);
                            viewModel.IbanList = new ObservableCollection<Result2>(results);
                            viewModel.NewAccountText = "Edit Account";
                        }
                    }
                    //                viewModel.IsNewAccountClicked = false;
                }
            }
            catch(Exception ex)
            {

            }
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceived");
            MessagingCenter.Unsubscribe<object, ATTDETSet>(this, "AttachmentReceived");
        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                string message = string.Empty;
                Xamarin.Forms.MessagingCenter.Subscribe<object, string>(this, "IbanReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        message = arg;
                    // firebasemessage = JsonConvert.DeserializeObject<PushnotificationMessage>(arg);
                    if (message == "SA")
                        {
                            if (viewModel.IbanList != null)
                            {
                                viewModel.IbanList.Clear();
                            }
                            viewModel.IbanList = null;
                            viewModel.IbanList = new ObservableCollection<Result2>(viewModel.VATRegistrationDetailsData.d.IBANSet.results);
                            viewModel.VATRegistrationDetailsData.d.OptIban = String.Empty;
                            viewModel.NewAccountText = "New Account";
                        }
                        else
                        {
                            triggerIban(message);
                        }
                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, ATTDETSet>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.VATRegistrationDetailsData.d.ATTDETSet = arg;
                    }
                });

                await GetVatRegistrationData();
            }
           catch(Exception ex)
            {

            }
        }
        public async Task GetVatRegistrationData()
        {
            try
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
            catch(Exception ex)
            {

            }
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

        private void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
            {
                if (viewModel.SelectedIdTypeFR.ID == "ZS0015")
                {
                    if (viewModel.IdnumberFR.Substring(0, 1) != "1")
                    {
                        popUp.Message = AppResources.ZZNationalIDstartswith1;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        viewModel.IdnumberFR = string.Empty;
                        //ZZPleaseenteravalidNationalID
                    }
                    else
                    {
                        if (viewModel.IdnumberFR.Length != 10)
                        {
                            if (Messages.Length > 0)
                            {
                                Messages.Append(Environment.NewLine);
                            }
                            Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                        }
                        if (Messages.Length > 0)
                        {
                            popUp.Message = Messages.ToString();
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.IdnumberFR= string.Empty;
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.DOB))
                            {
                                ValidateIDNumber();
                            }


                        }
                    }


                }
                if (viewModel.SelectedIdTypeFR.ID == "ZS0017")
                {
                    if (viewModel.IdnumberFR.Substring(0, 1) != "2")
                    {
                        popUp.Message = AppResources.ZZIqamaIDstartswith2;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        viewModel.IdnumberFR = string.Empty;
                    }
                    else
                    {
                        if (viewModel.IdnumberFR.Length != 10)
                        {
                            if (Messages.Length > 0)
                            {
                                Messages.Append(Environment.NewLine);
                            }
                            Messages.Append(AppResources.ZZIqamaIDlengthis10digit);
                        }
                        if (Messages.Length > 0)
                        {
                            popUp.Message = Messages.ToString();
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.IdnumberFR = string.Empty;
                        }
                        else
                        {
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.DOB))
                            {
                                ValidateIDNumber();
                            }
                        }
                    }


                }
                if (viewModel.SelectedIdTypeFR.ID == "ZS0018")
                {
                    if (viewModel.IdnumberFR.Substring(0, 1) == "0")
                    {
                        //Have to change to neww error message
                        popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        viewModel.IdnumberFR = string.Empty;
                    }
                    else if (!(viewModel.IdnumberFR.Length <= 15 && viewModel.IdnumberFR.Length >= 7))
                    {
                        popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameIDError = true;
                        viewModel.IdnumberFR = string.Empty;
                        // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                    }


                }
            }
            else
            {
                // FrmIDNumber.HasError = false;
                viewModel.FrameIDError = false;
            }

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
            VATRegistrationDetails vATRegistrationDetails = null;
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vATRegistrationDetails));
        }

        private void TappedOnMenu(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new VATRegistrationMenuPopUp());
        }

        private void VATFaqTapped(object sender, EventArgs e)
        {

        }

        private void NewAttachment_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FinancialDetailAttachmentPopupPageView());
        }

        private void TappendOnImporter(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ImporterImageSource == "vat_tile_IbanCard_background.png")
                {
                    viewModel.ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                    viewModel.ImporterTextColor = Color.Black;
                }
                else
                {
                    viewModel.ImporterImageSource = "vat_tile_IbanCard_background.png";
                    viewModel.ImporterTextColor = Color.White;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void TappendOnExporter(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ExporterImageSource == "vat_tile_IbanCard_background.png")
                {
                    viewModel.ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                    viewModel.ExporterTextColor = Color.Black;
                }
                else
                {
                    viewModel.ExporterImageSource = "vat_tile_IbanCard_background.png";
                    viewModel.ExporterTextColor = Color.White;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void DDlIDTypeSR_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.TxtIDTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR].Name;
                viewModel.SelectedIdTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR];
                


            }
            catch (Exception ex)
            {


            }
        }

        private void DDlIDTypeFR_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.TxtIDTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].Name;
                viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR];



            }
            catch (Exception ex)
            {


            }
        }

        private void DDlIDTypeFR_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void IDTypeSR_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private async void ImporterExporterAttachment(object sender, EventArgs e)
        {
            try
            {
                VATRegistrationPageViewModel.IsComeFromForAttachment = IsComeFromForAttachment.Import;
                if (viewModel.ImporterImageSource == "vat_tile_IbanCard_background.png")
                {
                    viewModel.VATRegistrationDetailsData.d.ImFg = "1";
                }
                else
                {
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                }
                if (viewModel.ExporterImageSource == "vat_tile_IbanCard_background.png")
                {
                    viewModel.VATRegistrationDetailsData.d.ExFg = "1";
                }
                else
                {
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(viewModel.VATRegistrationDetailsData));
            }
            catch(Exception ex)
            {

            }
        }

       

        private void OnPageSelectedForIban(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DateEntry_Focused_1(object sender, FocusEventArgs e)
        {

        }

        private void DateEntry_Unfocused_1(object sender, FocusEventArgs e)
        {

        }

        private void DateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        public async void ValidateIDNumber()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string dob = viewModel.DOB.Replace("/", "");
           // EntryName.IsEnabled = true;
            if (viewModel.SelectedIdTypeFR.ID == "ZS0015")
            {
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0015", viewModel.IdnumberFR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.FirstnmFR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                          //  EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0015", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeFR.ID == "ZS0017")
            {
              //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0017", viewModel.IdnumberFR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.FirstnmFR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                          //  EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0017", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }

        private void btnDate_Clicked(object sender, EventArgs e)
        {
            SignUpDOB.IsOpen = true;
        }

        private void DOB_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.DOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                //viewModel.DOBPrev = viewModel.DOB;


                ValidateIDNumber();
            }
            catch (Exception ex)
            {

            }
        }

        private void DOB_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void btn4_Clicked(object sender, EventArgs e)
        {
            ContactDOBPicker.IsOpen = true;
        }

        private void EntryTINNumber_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryContactIDNumber_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
            {
                if (viewModel.SelectedIdTypeSR.ID == "ZS0015")
                {
                    if (viewModel.IdNumberSR.Substring(0, 1) != "1")
                    {
                        popUp.Message = AppResources.ZZNationalIDstartswith1;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameContactIDError = true;
                        viewModel.IdNumberSR = string.Empty;
                        //ZZPleaseenteravalidNationalID
                    }
                    else
                    {
                        if (EntryContactIDNumber.Text.Length != 10)
                        {
                            if (Messages.Length > 0)
                            {
                                Messages.Append(Environment.NewLine);
                            }
                            Messages.Append(AppResources.ZZNationalIDlengthis10digit);
                        }
                        if (Messages.Length > 0)
                        {
                            popUp.Message = Messages.ToString();
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameContactIDError = true;
                            EntryContactIDNumber.Text = string.Empty;
                        }
                        else
                        {
                            
                            viewModel.FrameContactIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.ContactDOB))
                            {
                                ValidateIDNumber();
                            }


                        }
                    }


                }
                if (viewModel.SelectedIdTypeSR.ID == "ZS0017")
                {
                    if (viewModel.IdNumberSR.Substring(0, 1) != "2")
                    {
                        popUp.Message = AppResources.ZZIqamaIDstartswith2;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameContactIDError = true;
                        viewModel.IdNumberSR = string.Empty;
                    }
                    else
                    {
                        if (viewModel.IdNumberSR.Length != 10)
                        {
                            if (Messages.Length > 0)
                            {
                                Messages.Append(Environment.NewLine);
                            }
                            Messages.Append(AppResources.ZZIqamaIDlengthis10digit);
                        }
                        if (Messages.Length > 0)
                        {
                            popUp.Message = Messages.ToString();
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                                popUp.isFontSet = true;
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameContactIDError = true;
                            viewModel.IdNumberSR = string.Empty;
                        }
                        else
                        {
                            
                            viewModel.FrameContactIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.ContactDOB))
                            {
                               ValidateIDNumber();
                            }
                        }
                    }


                }
                if (viewModel.SelectedIdTypeSR.ID == "ZS0018")
                {
                    if (viewModel.IdNumberSR.Substring(0, 1) == "0")
                    {
                        //Have to change to neww error message
                        popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameContactIDError = true;
                        viewModel.IdNumberSR = string.Empty;
                    }
                    else if (!(viewModel.IdNumberSR.Length <= 15 && viewModel.IdNumberSR.Length >= 7))
                    {
                        popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
                        popUp.IsLinkAvailable = false;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameContactIDError = true;
                        viewModel.IdNumberSR = string.Empty;
                        // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                    }


                }
            }
            else
            {
                // FrmIDNumber.HasError = false;
                viewModel.FrameContactIDError = false;
            }



        }

        private void ContactDOBPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = ContactDOBPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.ContactDOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                //viewModel.DOBPrev = viewModel.DOB;


                ValidateIDNumberContact();
            }
            catch (Exception ex)
            {

            }
        }

        private void ContactDOBPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        public async void ValidateIDNumberContact()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string dob = viewModel.ContactDOB.Replace("/", "");
            // EntryName.IsEnabled = true;
            if (viewModel.SelectedIdTypeSR.ID == "ZS0015")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0015", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                
                                viewModel.FrameContactIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                //FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            //  EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0015", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameContactIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameContactIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeSR.ID == "ZS0017")
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0017", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameContactIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameContactIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            //  EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
                            viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0017", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameContactIDError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameContactIDError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (GAZTException gex)
                        {
                            // Handle the GAZT custom exception.
                            string MessageForTheUser = gex.Message;
                            if (gex is GAZTInvalidDataException)
                            {
                                MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            }
                            if (gex is GAZTNetworkConnectivityIssueException)
                            {
                                MessageForTheUser = AppResources.NetworkConnectivityIssue;
                            }
                            else if (gex is GAZTInternetException)
                            {
                                MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                            }
                            else if (gex is GAZTSessionExpiredException)
                            {
                                MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                            }

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel.IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException ex)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            });
        }
    }
}