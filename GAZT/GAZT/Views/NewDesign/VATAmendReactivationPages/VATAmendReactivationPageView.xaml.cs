using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Models.Template;
using EGAZT.ViewModel.NewDesignViewModel.VATAmendReactivationPageViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using GAZT;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATAmendReactivationPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATAmendReactivationPageView : ContentPage
    {
        VATAmendReactivationPageViewModel viewModel;
        public bool IsSubmitClicked { get; set; }
        public VATAmendReactivationPageView()
        {
            try
            {
                InitializeComponent();
                Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                Resources["IsDeclarationCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                Resources["IsAddAdditionalInfoCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                Resources["IsFDChangeSectionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                Resources["IsChangeEmailCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                Resources["IsAddNewRepresentativeCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                viewModel = App.Locator.VATAmendReactivationPageView;
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                this.BindingContext = viewModel;
                if (App.IsArabic)
                {
                    label1.HorizontalTextAlignment = TextAlignment.End;
                    label2.HorizontalTextAlignment = TextAlignment.End;
                    label3.HorizontalTextAlignment = TextAlignment.End;
                    label4.HorizontalTextAlignment = TextAlignment.End;
                }
                else
                {
                    label1.HorizontalTextAlignment = TextAlignment.Start;
                    label2.HorizontalTextAlignment = TextAlignment.Start;
                    label3.HorizontalTextAlignment = TextAlignment.Start;
                    label4.HorizontalTextAlignment = TextAlignment.Start;

                }

                ChangeAeroIcon();
                clearDATA();
                viewModel.SetVisibility();
                viewModel.IsInstrunctionVisible = true;
                viewModel.CurrentStep = AppResources.VATRStep2;
                SetfirstBoxColor();
                viewModel.IsNewAccountClicked = false;
                viewModel.IsInstrunctionChecked = false;
                viewModel.NewAccountText = AppResources.ZTERNewAccount;
                viewModel.SetDefaultDate();
                SetLTR();
                Task.Run(async () =>
                {
                    try
                    {
                        viewModel.IsLoading = true;
                        await GetVatRegistrationData();
                        NewFRDOBField.IsVisible = false;
                        viewModel.SetUIAvailability();
                        if (!viewModel.IsChangeEmailChecked)
                        {
                            viewModel.IsFDNameMobEmailEnable = false;
                        }
                        if (!viewModel.IsAddAdditionalInfoChecked)
                        {
                            viewModel.IsTaxPayerIBANEnabled = false;
                        }

                        if (App.VATType == Enums.PageExecutionType.Amend)
                        {
                            viewModel.Declaration.IDTypeOrNoEntry = false;
                            viewModel.Declaration.ContactNameEntry = false;

                        }
                    }
                    catch (Exception)
                    {

                    }
                });
            }
            catch (Exception)
            {

            }
        }
        public void clearDATA()
        {
            viewModel.StartdateToshow = string.Empty;
            viewModel.AddressLineOne = string.Empty;
            viewModel.AddressLineTwo = string.Empty;
            viewModel.SliderLable1EligibilityText = string.Empty;

        }

        public void ClearFinancialRepresentativeData()
        {
            EntryTINNumber.Text = string.Empty;
            DateEntryFR.Text = string.Empty;
            EntryFirstName.Text = string.Empty;
            EntryLastName.Text = string.Empty;
            EntryEmail.Text = string.Empty;
            EntryPhoneNumber.Text = string.Empty;
            EntryIDNo.Text = string.Empty;
        }

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
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        private void DpEStartDate_Closed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = DpEStartDate.SelectedItem as List<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.VatEligibleStartDate = day + "/" + month + "/" + year;
            }
            catch (Exception)
            {

            }
        }

        private void DpEStartDate_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = DpEStartDate.SelectedItem as List<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.VatEligibleStartDate = day + "/" + month + "/" + year;
            }
            catch (Exception)
            {

            }
        }

        private void DpEStartDate_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            if (App.VATType == Enums.PageExecutionType.Reactivation)
            {
                DpEStartDate.IsOpen = true;
            }
        }

        private void btnImporter_Clicked(object sender, EventArgs e)
        {
            Models.VATRegistrationDetails vatReg = null;
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void btnExporter_Clicked(object sender, EventArgs e)
        {
            Models.VATRegistrationDetails vatReg = null;
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void NewAccount_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new NewAccountPopUpPageView(string.Empty, IsComingFromScreen.VATAmendReactivation));
        }

        public void triggerIban(string messagestring)
        {
            try
            {
                string message = messagestring;
                if (!string.IsNullOrEmpty(message))
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        viewModel.VATRegistrationDetailsData.d.OptIban = message;
                        viewModel.NewAccountText = AppResources.VATREditAccount;


                        Result2 result2 = new Result2();
                        result2.Iban = message;


                        bool checkDuplicate = false;
                        foreach (var item in viewModel.IbanList)
                        {
                            checkDuplicate = JsonCompare(item, result2);
                            if (checkDuplicate)
                                break;
                        }
                        if (!checkDuplicate)
                            viewModel.IbanList.Add(result2);
                        checkDuplicate = false;
                        viewModel.NewAccountText = AppResources.VATREditAccount;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private bool JsonCompare(object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);

            return objJson == anotherJson;
        }

        private async void btnContinue_Clicked(object sender, EventArgs e)
        {
            if (viewModel.IsContinueButtonEnable)
            {
                if (viewModel.CurrentStep == AppResources.VATRStep1)
                {
                    viewModel.CurrentStep = AppResources.VATRStep2;
                    viewModel.SetVisibility();
                    viewModel.IsTaxPayersVisible = true;
                    SetsecondBoxColor();
                }
                else if (viewModel.CurrentStep == AppResources.VATRStep2)
                {
                    step2Validation();
                    setAttachmentImporterExporterVisibility();
                }
                else if (viewModel.CurrentStep == AppResources.VATRStep3)
                {
                    step3Validation();
                }
                else if (viewModel.CurrentStep == AppResources.VATRStep4)
                {
                    if (viewModel.RegTypeCode == "N")
                    {
                        if (viewModel.VATRegistrationDetailsData.d.ATTDETSet.results.Count > 0)
                        {
                            viewModel.CurrentStep = AppResources.VATRStep5;
                            viewModel.SetVisibility();
                            viewModel.IsFinancialVisible = true;
                            SetfourthBoxColor();
                            if (viewModel.CurrentIndex == 3)
                                viewModel.CurrentIndex++;
                        }
                        else
                        {
                            FrmNewAttachment.HasError = true;
                        }
                    }
                    else
                    {
                        viewModel.CurrentStep = AppResources.VATRStep5;
                        viewModel.SetVisibility();
                        viewModel.IsFinancialVisible = true;
                        SetfourthBoxColor();
                        if (viewModel.CurrentIndex == 3)
                            viewModel.CurrentIndex++;
                    }
                }
                else if (viewModel.CurrentStep == AppResources.VATRStep5)
                {
                    if (viewModel.IsNewFinancialRepVisible)
                    {
                        bool validFlag = await step4Validation();
                        if (!validFlag)
                        {
                            return;
                        }
                    }
                    else
                    {

                        viewModel.GpartSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Gpart;
                        viewModel.IdnumberSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Idnumber;
                        viewModel.FirstnmSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Firstnm;
                        viewModel.LastnmSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Lastnm;
                        viewModel.MobNumberSum = viewModel.ListFinanceRepresenatives[0].MobNumberFR;
                        viewModel.SmtpAddrSum = viewModel.ListFinanceRepresenatives[0].SmtpAddrFR;
                        viewModel.TxtIDTypeSum = viewModel.IdTypeListFR.Where(x => x.ID == viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Type).FirstOrDefault()?.Name;

                    }
                    viewModel.CurrentStep = AppResources.ZTEReportCategorySubmitBtn;
                    viewModel.SetVisibility();
                    viewModel.IsSummaryVisible = true;
                    SetfifthBoxColor();
                    if (viewModel.CurrentIndex == 4)
                        viewModel.CurrentIndex++;
                    if (viewModel.IsDeclarationChecked)
                    {
                        viewModel.IsContinueButtonEnable = true;
                    }
                    else
                    {
                        viewModel.IsContinueButtonEnable = false;
                    }
                }
                else if (viewModel.CurrentStep == AppResources.ZTEReportCategorySubmitBtn)
                {
                    if (viewModel.IsContinueButtonEnable)
                    {
                        step5Validation();
                    }
                }
            }
        }
        public void setdefaultvalueforTPDetailscreen()
        {
            try
            {
                if (string.IsNullOrEmpty(viewModel.VATRegistrationDetailsData.d.ImFg))
                {
                    viewModel.ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                    viewModel.ImporterTextColor = Color.Black;
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                }
                else
                {
                    if (viewModel.VATRegistrationDetailsData.d.ImFg.Equals("0"))
                    {
                        viewModel.ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                        viewModel.ImporterTextColor = Color.Black;
                        viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                    }
                    else if (viewModel.VATRegistrationDetailsData.d.ImFg.Equals("1"))
                    {
                        viewModel.ImporterImageSource = "vat_tile_IbanCard_background.png";
                        viewModel.ImporterTextColor = Color.White;
                        viewModel.VATRegistrationDetailsData.d.ImFg = "1";
                    }
                }

                if (string.IsNullOrEmpty(viewModel.VATRegistrationDetailsData.d.ExFg))
                {
                    viewModel.ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                    viewModel.ExporterTextColor = Color.Black;
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                else
                {
                    if (viewModel.VATRegistrationDetailsData.d.ExFg.Equals("0"))
                    {
                        viewModel.ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                        viewModel.ExporterTextColor = Color.Black;
                        viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                    }
                    else if (viewModel.VATRegistrationDetailsData.d.ExFg.Equals("1"))
                    {
                        viewModel.ExporterImageSource = "vat_tile_IbanCard_background.png";
                        viewModel.ExporterTextColor = Color.White;
                        viewModel.VATRegistrationDetailsData.d.ExFg = "1";
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public async Task<bool> step4Validation()
        {
            bool flag = true;

            if (viewModel.IsDeclarationChecked == true)
            {
                if (viewModel.SelectedIdTypeFR == null || FrmIDType.HasError)
                {
                    flag = false;
                    FrmIDType.HasError = true;
                }
                if (string.IsNullOrEmpty(viewModel.IdnumberFR) || FrmIDNo.HasError)
                {
                    flag = false;
                    viewModel.FrameIDError = true;
                }
                if (NewFRDOBField.IsVisible && (viewModel.FrameDOBError || string.IsNullOrEmpty(viewModel.DOB)))
                {
                    flag = false;
                    viewModel.FrameDOBError = true;
                }
                if (FrmFirstName.IsEnabled && (FrmFirstName.HasError || string.IsNullOrEmpty(viewModel.FirstnmFR)))
                {
                    flag = false;
                    FrmFirstName.HasError = true;

                }
                if (FrmLastName.IsEnabled && (FrmLastName.HasError || string.IsNullOrEmpty(viewModel.LastnmFR)))
                {
                    flag = false;
                    FrmLastName.HasError = true;

                }
                if (FrmEmailAddress.IsEnabled && (FrmEmailAddress.HasError || string.IsNullOrEmpty(viewModel.SmtpAddrFR)))
                {
                    flag = false;
                    FrmEmailAddress.HasError = true;

                }
                if (FrmPhoneNumber.IsEnabled && (FrmPhoneNumber.HasError || string.IsNullOrEmpty(viewModel.MobNumberFR)))
                {
                    flag = false;
                    FrmPhoneNumber.HasError = true;
                }
                if (!flag)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
                }
            }
            else
            {
                PopUp popUp = new PopUp();
                popUp.Message = AppResources.VATRAcceptDeclarationToSubmit;
                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATRAcceptDeclarationToSubmit));
                chkDeclaration.Focus();
            }

            return await Task.FromResult(flag);
        }
        public async void step5Validation()
        {
            if (viewModel.IsDeclarationChecked == true)
            {
                bool flag = true;
                if (App.VATType == Enums.PageExecutionType.Reactivation)
                {
                    if (viewModel.SelectedIdTypeSR == null)
                    {
                        flag = false;
                    }
                    if (string.IsNullOrEmpty(viewModel.IdNumberSR))
                    {
                        flag = false;
                        viewModel.FrameContactIDError = true;
                    }
                    if (string.IsNullOrEmpty(viewModel.FirstNameSR))
                    {
                        flag = false;
                        FrmContactName.HasError = true;

                    }
                    if (btnSR.IsVisible && string.IsNullOrEmpty(viewModel.ContactDOB))
                    {
                        flag = false;
                        viewModel.FrameContactDOBError = true;

                    }
                }
                if (flag)
                {
                    if (App.VATType == Enums.PageExecutionType.Amend)
                    {
                        if (!viewModel.IsAddAdditionalInfoChecked && !viewModel.IsFDChangeSectionEnabled && !viewModel.IsAddNewRepresentativeChecked && !viewModel.IsChangeEmailChecked)
                        {
                            await PopupNavigation.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZZZOkayText, AppResources.ZZVATAmendNoChangesMadeSubmitMessage, string.Empty));
                            return;
                        }
                    }
                    viewModel.VATRegistrationDetailsData.d.Operationz = "01";

                    Models.VATRegistrationDetails response = await viewModel.SubmitClicked();
                    if (response != null)
                    {
                        viewModel._navigationService.NavigateTo(App.VATAmendReactivationSuccessfulPageView, response);
                    }

                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
                }

            }
            else
            {
                PopUp popUp = new PopUp();
                popUp.Message = AppResources.VATRAcceptDeclarationToSubmit;
                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATRAcceptDeclarationToSubmit));
                chkDeclaration.Focus();
            }
        }
        public async void step2Validation()
        {
            if (viewModel.IsInstrunctionChecked == true)
            {

                viewModel.CurrentStep = AppResources.VATRStep3;
                viewModel.SetVisibility();
                viewModel.IsTaxPayersVisible = true;
                SetsecondBoxColor();
                if (viewModel.CurrentIndex == 1)
                    viewModel.CurrentIndex++;
                setdefaultvalueforTPDetailscreen();
                if (string.IsNullOrEmpty(viewModel.VatEligibleStartDate))
                {
                    viewModel.IsContinueButtonEnable = false;
                }
                else
                {
                    string[] year = viewModel.VatEligibleStartDate.Split('/');
                    int yearnumber = Int32.Parse(year[2]);
                    if (yearnumber >= 2018)
                    {
                        viewModel.IsContinueButtonEnable = true;
                    }
                    else
                    {
                        viewModel.IsContinueButtonEnable = false;
                    }
                }
            }
            else
            {
                PopUp popUp = new PopUp();
                popUp.Message = AppResources.ZZPleaseselecttermsandconditions;
                if (App.IsArabic)
                {
                    popUp.FlowDirections = "RightToLeft";
                    popUp.isFontSet = true;
                }
                else
                {
                    popUp.FlowDirections = "LeftToRight";
                }
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselecttermsandconditions));
                chkDeclaration.Focus();
            }
        }
        public void step3Validation()
        {

            if (!string.IsNullOrEmpty(DateEntry.Text))
            {

                string[] year = DateEntry.Text.Split('/');
                int yearnumber = Int32.Parse(year[2]);
                if (yearnumber >= 2018)
                {
                    viewModel.CurrentStep = AppResources.VATRStep4;
                    viewModel.SetVisibility();
                    viewModel.IsSalesVisible = true;
                    SetthirdBoxColor();
                    if (viewModel.CurrentIndex == 2)
                        viewModel.CurrentIndex++;
                    viewModel.IsFDNameMobEmailEnable = false;
                    viewModel.Attachments = AppResources.Attachments;
                    if (viewModel.VATRegistrationDetailsData.d.ResidencyTy == "R")
                    {
                        viewModel.IsResident = true;
                        setAnsWerOneSlider();
                        setDefaultAnswerThree();
                        setDefaultansForAnswer4();
                        setAnsWertwoSlider();
                    }
                    else
                    {
                        viewModel.IsResident = false;
                        setAnsWertwoSlider();
                    }
                }
                else
                {
                    FrmEStartDate.Focus();
                    FrmEStartDate.HasError = true;
                }
            }
            else
            {
                FrmEStartDate.Focus();
                FrmEStartDate.HasError = true;
            }

        }
        public async void setDefaultAnswerThree()
        {
            if (viewModel.answer3selectedcount == 0)
            {
                foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                {
                    if (item.QueNo == "003" && item.QoptNo == "031")
                    {
                        item.QoptAns = "1";
                    }
                    if (item.QueNo == "003" && item.QoptNo == "032")
                    {
                        item.QoptAns = "0";
                    }
                }
                viewModel.quesTion3answerSelected = viewModel.TextQuestion3First;
                viewModel.setQuestionImage();
                viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();

                if (registrationDetails != null & registrationDetails.d != null)
                {
                    viewModel.RegTypeCode = registrationDetails.d.RegTy;
                    string code = registrationDetails.d.RegTy;
                    string eligibilityText = string.Empty;
                    viewModel.Attachments = AppResources.Attachments;

                    if (code.Equals("L"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    }
                    else if (code.Equals("S"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    }
                    else if (code.Equals("V"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    }
                    else if (code.Equals("N"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                        viewModel.Attachments = AppResources.Attachments + "*";
                    }
                    else if (code.Equals("M"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    }
                    viewModel.SliderLable1EligibilityText = eligibilityText;
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });
                }
            }
        }

        public async void setDefaultansForAnswer4()
        {
            if (viewModel.answer4selectedcount == 0)
            {
                foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                {
                    if (item.QueNo == "004" && item.QoptNo == "041")
                    {
                        item.QoptAns = "1";
                    }
                    if (item.QueNo == "004" && item.QoptNo == "042")
                    {
                        item.QoptAns = "0";
                    }
                }
                viewModel.quesTion4answerSelected = viewModel.TextQuestion4First;
                viewModel.setQuestionImage();
                viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
                if (registrationDetails != null & registrationDetails.d != null)
                {
                    viewModel.RegTypeCode = registrationDetails.d.RegTy;
                    string code = registrationDetails.d.RegTy;
                    string eligibilityText = string.Empty;
                    viewModel.Attachments = AppResources.Attachments;

                    if (code.Equals("L"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    }
                    else if (code.Equals("S"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    }
                    else if (code.Equals("V"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    }
                    else if (code.Equals("N"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                        viewModel.Attachments = AppResources.Attachments + "*";
                    }
                    else if (code.Equals("M"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    }
                    viewModel.SliderLable1EligibilityText = eligibilityText;
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });
                }
            }
        }
        public async void setAnsWerOneSlider()
        {
            if (viewModel.answer1selectedcount != 0)
            {
                try
                {
                    string AnswerID = viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "001" && x.QoptAns == "1").Select(x => x.QoptNo).FirstOrDefault();
                    int a = UtilityManager.FindTheAnswerIndexBasedOntheAnswerId("001", AnswerID, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    Slider_Answer1.Value = Convert.ToDouble(a);
                    QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("001", Convert.ToDouble(a), viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    viewModel.SliderLable1 = obj.QoptTxt;
                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                    {
                        if (item.QueNo == "001")
                        {
                            if (item.QoptNo == obj.QoptNo)
                            {
                                item.QoptAns = "1";
                            }
                            else
                            {
                                item.QoptAns = "0";
                            }
                        }
                    }
                    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("L"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("S"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("V"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("N"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";
                        }
                        else if (code.Equals("M"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    }
                }
                catch (Exception)
                {
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });
                }
            }
            else
            {
                try
                {
                    double value = 0;
                    Slider_Answer1.Value = value;
                    QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("001", value, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    viewModel.SliderLable1 = obj.QoptTxt;
                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                    {
                        if (item.QueNo == "001")
                        {
                            if (item.QoptNo == obj.QoptNo)
                            {

                                item.QoptAns = "1";
                            }
                            else
                            {
                                item.QoptAns = "0";
                            }
                        }
                    }
                    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("L"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("S"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("V"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("N"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";
                        }
                        else if (code.Equals("M"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    }
                }
                catch (Exception)
                {
                    await Task.Run(() =>
                    {
                        viewModel.IsLoading = false;
                    });

                }
            }
        }
        public async void setAnsWertwoSlider()
        {
            if (viewModel.answer2selectedcount != 0)
            {

                try
                {

                    string AnswerID = viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "002" && x.QoptAns == "1").Select(x => x.QoptNo).FirstOrDefault();
                    int a = UtilityManager.FindTheAnswerIndexBasedOntheAnswerId("002", AnswerID, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    Slider_Answer2.Value = Convert.ToDouble(a);
                    QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("002", Convert.ToDouble(a), viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    viewModel.SliderLable2 = obj.QoptTxt;
                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                    {
                        if (item.QueNo == "002")
                        {
                            if (item.QoptNo == obj.QoptNo)
                            {
                                item.QoptAns = "1";
                            }
                            else
                            {
                                item.QoptAns = "0";
                            }
                        }
                    }
                    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("L"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("S"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("V"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("N"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";
                        }
                        else if (code.Equals("M"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                try
                {
                    double value = 0;
                    Slider_Answer2.Value = value;
                    QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("002", value, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    viewModel.SliderLable2 = obj.QoptTxt;
                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                    {
                        if (item.QueNo == "002")
                        {
                            if (item.QoptNo == obj.QoptNo)
                            {
                                item.QoptAns = "1";
                            }
                            else
                            {
                                item.QoptAns = "0";
                            }
                        }
                    }
                    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("L"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("S"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("V"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("N"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";
                        }
                        else if (code.Equals("M"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceived");
            MessagingCenter.Unsubscribe<object, ATTDETSet>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, ATTDETSet>(this, "EligibilitySetAttachmentReceived");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse");
            MessagingCenter.Unsubscribe<VATAmendReactivationPageViewModel, bool>(this, "IsInstrunctionChecked");
            MessagingCenter.Unsubscribe<VATAmendReactivationPageViewModel, bool>(this, "IsDeclarationChecked");
            MessagingCenter.Unsubscribe<VATAmendReactivationPageViewModel, bool>(this, "IsAddAdditionalInfoChecked");
            MessagingCenter.Unsubscribe<VATAmendReactivationPageViewModel, bool>(this, "IsFDChangeSectionChecked");
            MessagingCenter.Unsubscribe<VATAmendReactivationPageViewModel, bool>(this, "IsChangeEmailChecked");
            MessagingCenter.Unsubscribe<VATAmendReactivationPageViewModel, bool>(this, "IsAddNewRepresentativeChecked");
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;

                if (Device.RuntimePlatform == Device.Android)
                {
                    DDlIDType.BackgroundColor = Color.FromHex("#f7f7f7");
                    DDlContactIDType.BackgroundColor = Color.FromHex("#f7f7f7");
                }
                else
                {
                    Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
                    //Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
                    DDlIDType.BackgroundColor = Color.FromHex("#FFFFFF");
                    DDlContactIDType.BackgroundColor = Color.FromHex("#FFFFFF");
                }
                string message = string.Empty;
                MessagingCenter.Subscribe<VATAmendReactivationPageViewModel, bool>(this, "IsInstrunctionChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                MessagingCenter.Subscribe<VATAmendReactivationPageViewModel, bool>(this, "IsDeclarationChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsDeclarationCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsDeclarationCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                MessagingCenter.Subscribe<VATAmendReactivationPageViewModel, bool>(this, "IsAddAdditionalInfoChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsAddAdditionalInfoCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsAddAdditionalInfoCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                MessagingCenter.Subscribe<VATAmendReactivationPageViewModel, bool>(this, "IsFDChangeSectionChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsFDChangeSectionCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsFDChangeSectionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                MessagingCenter.Subscribe<VATAmendReactivationPageViewModel, bool>(this, "IsChangeEmailChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsChangeEmailCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsChangeEmailCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                MessagingCenter.Subscribe<VATAmendReactivationPageViewModel, bool>(this, "IsAddNewRepresentativeChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsAddNewRepresentativeCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsAddNewRepresentativeCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", (obj, res) => { PopupNavigation.Instance.PopAsync(); });
                MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
                {
                    viewModel.VatEligibleStartDate = DateTime.Parse(arg.SelectedValue).Date.ToString("dd/MM/yyyy").Replace('-', '/');
                });
                Xamarin.Forms.MessagingCenter.Subscribe<object, string>(this, "IbanReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        message = arg;
                        try
                        {
                            if (message == "SA")
                            {
                                if (viewModel.IbanList != null)
                                {
                                    viewModel.IbanList.Clear();
                                }
                                viewModel.IbanList = null;
                                if (viewModel.VATRegistrationDetailsData != null && viewModel.VATRegistrationDetailsData.d != null && viewModel.VATRegistrationDetailsData.d.IBANSet != null)
                                {
                                    viewModel.IbanList.Clear();
                                    foreach (var item in viewModel.VATRegistrationDetailsData.d.IBANSet.results)
                                    {
                                        if (!string.IsNullOrEmpty(item.Bkvid))
                                        {
                                            viewModel.IbanList.Add(item);
                                        }
                                    }
                                }
                                viewModel.VATRegistrationDetailsData.d.OptIban = String.Empty;
                                viewModel.NewAccountText = AppResources.ZTERNewAccount;
                            }
                            else
                            {
                                triggerIban(message);
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                });

                Xamarin.Forms.MessagingCenter.Subscribe<object, ATTDETSet>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.VATRegistrationDetailsData.d.ATTDETSet = arg;
                        FrmNewAttachment.HasError = false;
                        viewModel.ATTDETSetObject = viewModel.VATRegistrationDetailsData.d.ATTDETSet;
                    }
                });
                Xamarin.Forms.MessagingCenter.Subscribe<object, ELGBL_DOCSetforsubmit>(this, "EligibilitySetAttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.VATRegistrationDetailsData.d.ELGBL_DOCSet = arg;
                    }
                });
                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                {
                    if (arg != null && !string.IsNullOrEmpty(arg.SelectedValue))
                    {
                        if (arg.PickerId == "SummaryIdTypePicker")
                        {
                            viewModel.TxtIDTypeSR = arg.SelectedValue;
                            viewModel.SelectedIdTypeSR = viewModel.IdTypeListSR.Where(p => p.Name.Equals(arg.SelectedValue)).FirstOrDefault();
                            if (!viewModel.TxtIDTypeSR.Equals(AppResources.ZZGCCID))
                            {
                                if (string.IsNullOrEmpty(arg.SelectedValue) || string.IsNullOrWhiteSpace(arg.SelectedValue))
                                {
                                    FrmContactDBO.IsVisible = false;
                                    btnSR.IsVisible = false;
                                    lblDOB.IsVisible = false;
                                }
                                else
                                {
                                    FrmContactDBO.IsVisible = true;
                                    btnSR.IsVisible = true;
                                    lblDOB.IsVisible = true;
                                }
                            }
                            else
                            {
                                FrmContactDBO.IsVisible = false;
                                btnSR.IsVisible = false;
                                lblDOB.IsVisible = false;
                            }
                        }
                        else if (arg.PickerId == "FinancialIdTypePicker")
                        {
                            FrmFirstName.IsEnabled = false;
                            FrmLastName.IsEnabled = false;
                            FrmEmailAddress.IsEnabled = false;
                            FrmPhoneNumber.IsEnabled = false;
                            ClearFinancialRepresentativeData();
                            viewModel.TxtIDTypeFR = viewModel.IdTypeListFR.Where(p => p.Name.Equals(arg.SelectedValue)).FirstOrDefault().Name;
                            viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(p => p.Name.Equals(arg.SelectedValue)).FirstOrDefault();
                            var val = viewModel.IdTypeListFR.Where(p => p.Name.Equals(arg.SelectedValue)).FirstOrDefault();
                            if (val != null && val.ID.ToString() == "00000")
                            {
                                FrmIDNo.IsEnabled = false;
                                EntryTINNumber.IsEnabled = true;
                                viewModel.IDTypeIndexFR = 0;
                                viewModel.TxtIDTypeFR = string.Empty;

                                viewModel.IDNumberNonMandatoryVisibility = true;
                                viewModel.IDNumberMandatoryVisibility = true;

                                viewModel.DOBNonMandatoryVisibility = true;
                                viewModel.DOBMandatoryVisibility = true;
                                NewFRDOBField.IsVisible = false;
                            }
                            else
                            {
                                EntryTINNumber.Text = string.Empty;
                                EntryIDNo.Text = string.Empty;
                                EntryTINNumber.IsEnabled = true;
                                FrmIDNo.IsEnabled = true;
                                viewModel.IDNumberNonMandatoryVisibility = false;
                                viewModel.IDNumberMandatoryVisibility = true;

                                // For GCC ID DOB is not mandatory
                                var val2 = viewModel.IdTypeListFR.Where(p => p.Name.Equals(arg.SelectedValue)).FirstOrDefault();
                                if (val2 != null && val2.ID.Equals("ZS0003"))
                                {
                                    viewModel.DOBNonMandatoryVisibility = true;
                                    viewModel.DOBMandatoryVisibility = false;
                                    NewFRDOBField.IsVisible = false;
                                }
                                else
                                {
                                    viewModel.DOBNonMandatoryVisibility = false;
                                    viewModel.DOBMandatoryVisibility = true;
                                    NewFRDOBField.IsVisible = true;
                                }
                            }
                        }
                    }
                });
                viewModel.IsNewFinancialRepVisible = viewModel.IsAddNewRepresentativeChecked;
            }
            catch (Exception)
            {

            }
        }
        public async Task GetVatRegistrationData()
        {
            try
            {
                await viewModel.onPageLoad();
                setIban();
            }
            catch (Exception)
            {

            }
        }
        public void setIban()
        {
            if (viewModel.VATRegistrationDetailsData != null)
            {
                if (string.IsNullOrEmpty(viewModel.VATRegistrationDetailsData.d.OptIban))
                {
                    viewModel.NewAccountText = AppResources.ZTERNewAccount;
                }
                else
                {
                    viewModel.NewAccountText = AppResources.VATREditAccount;
                    triggerIban(viewModel.VATRegistrationDetailsData.d.OptIban);
                }
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

        private async void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                FrmIDNo.HasError = false;
                if (string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    FrmIDNo.HasError = true;
                }
                PopUp popUp = new PopUp();
                StringBuilder Messages = new StringBuilder();
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    if (viewModel.SelectedIdTypeFR.ID == "ZS0001")
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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                viewModel.FrameIDError = true;
                                viewModel.IdnumberFR = string.Empty;
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.DOB))
                                {
                                    await ValidateIDNumber();
                                }
                            }
                        }
                    }
                    if (viewModel.SelectedIdTypeFR.ID == "ZS0002")
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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                viewModel.FrameIDError = true;
                                viewModel.IdnumberFR = string.Empty;
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.DOB))
                                {
                                    await ValidateIDNumber();
                                }
                            }
                        }
                    }
                    if (viewModel.SelectedIdTypeFR.ID == "ZS0003")
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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                            viewModel.FrameIDError = true;
                            viewModel.IdnumberFR = string.Empty;
                        }
                        else
                        {
                            await ValidateIDNumber();
                        }
                    }
                }
                else
                {
                    viewModel.FrameIDError = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void EntryTINNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntryTINNumber.Text))
            {
                viewModel.IsFDNameMobEmailEnable = false;
                FrmTINNumber.HasError = false;
                EntryIDNo.IsEnabled = true;
            }
            else
            {

                if (EntryTINNumber.Text.Substring(0, 1) != "3")
                {
                    if (EntryTINNumber.Text.Length != 10)
                    {
                        FrmTINNumber.HasError = true;
                    }
                }
                viewModel.IsFDNameMobEmailEnable = false;
            }
        }

        private void btnID_Clicked(object sender, EventArgs e)
        {
            //DDlIDType.IsOpen = true;
            GenericPickerModel genericPickerModel = new GenericPickerModel();
            genericPickerModel.PickerData = new List<string>();
            foreach (var item in viewModel.IdTypeListFR)
            {
                genericPickerModel.PickerData.Add(item.Name);
            }
            //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
            genericPickerModel.PickerId = "FinancialIdTypePicker";
            PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
        }

        private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }
        private void DDlIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ClearFinancialRepresentativeData();
                FrmIDType.HasError = false;
                if (viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].ID.Equals("00000"))
                {
                    FrmIDNo.IsEnabled = false;
                    EntryTINNumber.IsEnabled = true;
                    viewModel.IDTypeIndexFR = 0;
                    viewModel.TxtIDTypeFR = string.Empty;

                    viewModel.IDNumberNonMandatoryVisibility = true;
                    viewModel.IDNumberMandatoryVisibility = true;

                    viewModel.DOBNonMandatoryVisibility = true;
                    viewModel.DOBMandatoryVisibility = true;
                    NewFRDOBField.IsVisible = false;
                }
                else
                {
                    viewModel.TxtIDTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].Name;
                    viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR];
                    EntryTINNumber.Text = string.Empty;
                    EntryIDNo.Text = string.Empty;
                    EntryTINNumber.IsEnabled = true;
                    FrmIDNo.IsEnabled = true;
                    viewModel.IDNumberNonMandatoryVisibility = false;
                    viewModel.IDNumberMandatoryVisibility = true;

                    // For GCC ID DOB is not mandatory
                    if (viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].ID.Equals("ZS0003"))
                    {
                        viewModel.DOBNonMandatoryVisibility = true;
                        viewModel.DOBMandatoryVisibility = false;
                        NewFRDOBField.IsVisible = false;
                    }
                    else
                    {
                        viewModel.DOBNonMandatoryVisibility = false;
                        viewModel.DOBMandatoryVisibility = true;
                        NewFRDOBField.IsVisible = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void EntryIDNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
            {
                viewModel.FrameIDError = false;
            }
        }

        private void EntryFirstName_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void EntryLastName_Unfocused(object sender, FocusEventArgs e)
        {

        }
        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnContactID_Clicked(object sender, EventArgs e)
        {
            if (App.VATType == Enums.PageExecutionType.Reactivation)
            {
                // DDlContactIDType.IsOpen = true;
                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = new List<string>();
                foreach (var item in viewModel.IdTypeListSR)
                {
                    genericPickerModel.PickerData.Add(item.Name);
                }
                //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                genericPickerModel.PickerId = "SummaryIdTypePicker";
                PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
            }
        }
        #region SetColor
        public void SetfirstBoxColor()
        {
        }
        public void SetsecondBoxColor()
        {
        }
        public void SetthirdBoxColor()
        {
        }
        public void SetfourthBoxColor()
        {
        }
        public void SetfifthBoxColor()
        {
        }

        #endregion

        private void TappedOnBackButton(object sender, EventArgs e)
        {
            if (viewModel.IsTaxPayersVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsInstrunctionVisible = true;
                viewModel.CurrentIndex = 1;
                viewModel.CurrentStep = AppResources.VATRStep2;
                SetfirstBoxColor();
                if (viewModel.IsInstrunctionChecked)
                {
                    viewModel.IsContinueButtonEnable = true;
                }
            }
            else if (viewModel.IsSalesVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsTaxPayersVisible = true;
                viewModel.CurrentIndex = 2;
                viewModel.CurrentStep = AppResources.VATRStep3;
                SetsecondBoxColor();
                setAttachmentImporterExporterVisibility();
            }
            else if (viewModel.IsFinancialVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsSalesVisible = true;
                viewModel.CurrentIndex = 3;
                viewModel.CurrentStep = AppResources.VATRStep4;
                SetthirdBoxColor();
            }
            else if (viewModel.IsSummaryVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsFinancialVisible = true;
                viewModel.CurrentIndex = 4;
                viewModel.CurrentStep = AppResources.VATRStep5;
                SetfourthBoxColor();
            }
        }

        private void btnAttachmentDocuments_Clicked(object sender, EventArgs e)
        {
            Models.VATRegistrationDetails vATRegistrationDetails = null;
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vATRegistrationDetails));
        }

        private void TappedOnMenu(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new VATRegistrationMenuPopUp());
        }

        private void VATFaqTapped(object sender, EventArgs e)
        {
            try
            {
                if (App.IsArabic)
                {

                    Launcher.OpenAsync(new Uri("https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/default.aspx"));
                }
                else
                {
                    Launcher.OpenAsync(new Uri("https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/default.aspx"));

                }
            }
            catch
            {
            }
        }

        private async void NewAttachment_Clicked(object sender, EventArgs e)
        {
            try
            {
                DataToPassTofinancialDetailAttachmentPopup sendtoPopup = new DataToPassTofinancialDetailAttachmentPopup();
                sendtoPopup.VATRegistrationDetailsDatatoPopup = viewModel.VATRegistrationDetailsData;
                sendtoPopup.vatRegOthrDetailtoPopup = viewModel.VATRegistrationOtherDetails;
                await PopupNavigation.Instance.PushAsync(new FinancialDetailAttachmentPopupPageView(sendtoPopup, Models.ZakatInstalationModels.WhichAttachment.VATAmendRegistration));
            }
            catch (Exception)
            {
            }
        }

        private void TappendOnImporter(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ImporterImageSource == "vat_tile_IbanCard_background.png")
                {
                    viewModel.ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                    viewModel.ImporterTextColor = Color.Black;
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                }
                else
                {
                    viewModel.ImporterImageSource = "vat_tile_IbanCard_background.png";
                    viewModel.ImporterTextColor = Color.White;
                    viewModel.VATRegistrationDetailsData.d.ImFg = "1";
                }
                setAttachmentImporterExporterVisibility();
            }
            catch (Exception)
            {

            }
        }
        public void setAttachmentImporterExporterVisibility()
        {
            if (viewModel.VATRegistrationDetailsData.d.ExFg == "0" && viewModel.VATRegistrationDetailsData.d.ImFg == "0")
            {

                viewModel.isAttachmentImporterExporterVisible = false;
            }
            else
            {
                viewModel.isAttachmentImporterExporterVisible = true;
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
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                else
                {
                    viewModel.ExporterImageSource = "vat_tile_IbanCard_background.png";
                    viewModel.ExporterTextColor = Color.White;
                    viewModel.VATRegistrationDetailsData.d.ExFg = "1";
                }
                setAttachmentImporterExporterVisibility();
            }
            catch (Exception)
            {

            }
        }
        private async void onMoreOptionClicked(object sender, EventArgs e)
        {
            String OperationCode = String.Empty;
            try
            {
                if (viewModel.ListOfActionButtonsApplicableForRegistration != null && viewModel.ListOfActionButtonsApplicableForRegistration.Count() != 0)
                {
                    String action = string.Empty;
                    VATAmendReactivationMoreOptionPopUp popUp = new VATAmendReactivationMoreOptionPopUp(viewModel.ListOfActionButtonsApplicableForRegistration);
                    popUp.OnItemSelect = async (args) =>
                    {
                        action = args;

                        if (App.IsArabic)
                        {
                            ArButtons buttonId = ArButtons.None;
                            if (!string.IsNullOrEmpty(action))
                            {
                                action = action.Replace(" ", "");
                            }
                            Enum.TryParse(action, out buttonId);
                            switch (buttonId)
                            {
                                case ArButtons.إضافةملاحظات:
                                    break;
                                case ArButtons.عرضملاحظات:
                                    break;
                                case ArButtons.المرفقات:
                                    break;
                                case ArButtons.إلغاء:
                                    OperationCode = "04";
                                    break;
                                case ArButtons.حفظكمسودة:
                                    OperationCode = "05";
                                    break;
                                case ArButtons.تقديم:
                                    OperationCode = "01";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Buttons buttonId = Buttons.None;
                            buttonId = Buttons.Attachments;
                            if (!string.IsNullOrEmpty(action))
                            {
                                action = action.Replace(" ", "");
                            }
                            Enum.TryParse(action, out buttonId);
                            switch (buttonId)
                            {
                                case Buttons.CreateNotes:
                                    break;
                                case Buttons.DisplayNotes:
                                    break;
                                case Buttons.Attachments:
                                    break;
                                case Buttons.Void:
                                    OperationCode = "04";
                                    break;
                                case Buttons.SaveasDraft:
                                    OperationCode = "05";
                                    break;
                                case Buttons.Submit:
                                    OperationCode = "01";
                                    break;

                                default:
                                    break;
                            }
                        }


                        viewModel.VATRegistrationDetailsData.d.Operationz = OperationCode;
                        if (!string.IsNullOrEmpty(OperationCode))
                        {
                            if (OperationCode == "04")
                            {
                                if (App.IsArabic)
                                {
                                    var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.VATRVoidConfirmationMessage);
                                    confirmPopup.OnSelect = async (result) =>
                                    {
                                        if (result == "Yes")
                                        {
                                            await viewModel.SubmitClicked();
                                        }
                                    };
                                    await PopupNavigation.Instance.PushAsync(confirmPopup);
                                }
                                else
                                {
                                    var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.VATRVoidConfirmationMessage);
                                    confirmPopup.OnSelect = async (result) =>
                                    {
                                        if (result == "Yes")
                                        {
                                            await viewModel.SubmitClicked();
                                        }
                                    };
                                    await PopupNavigation.Instance.PushAsync(confirmPopup);
                                }
                            }
                            else
                            {
                                if (viewModel.IsAddAdditionalInfoChecked)
                                {
                                    viewModel.AddAdditionalInfoCheckBoxEnabled = false;
                                }
                                else
                                {
                                    viewModel.AddAdditionalInfoCheckBoxEnabled = true;
                                }
                                if (viewModel.IsFDChangeSectionChecked)
                                {
                                    viewModel.IsFinancialDChangeSectionEnabled = false;
                                }
                                else
                                {
                                    viewModel.IsFinancialDChangeSectionEnabled = true;
                                }
                                if (App.VATType == Enums.PageExecutionType.Amend)
                                {
                                    if (viewModel.CurrentStep == "Step 3")
                                    {
                                        if (!viewModel.IsAddAdditionalInfoChecked)
                                        {
                                            string message = AppResources.ZZVATAmendNoChangesMadeMessage;
                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));
                                        }
                                        else
                                        {
                                            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.VATRSaveasdraftMessage);
                                            confirmPopup.OnSelect = async (result) =>
                                            {
                                                if (result == "Yes")
                                                {
                                                    await viewModel.SubmitClicked();
                                                }
                                            };
                                            await PopupNavigation.Instance.PushAsync(confirmPopup);
                                        }
                                    }
                                    else if (viewModel.CurrentStep == "Step 4")
                                    {
                                        if (!viewModel.IsAddNewRepresentativeChecked || !viewModel.IsAddAdditionalInfoChecked)
                                        {
                                            string message = AppResources.ZZVATAmendNoChangesMadeMessage;
                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));
                                        }
                                        else
                                        {
                                            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.VATRSaveasdraftMessage);
                                            confirmPopup.OnSelect = async (result) =>
                                            {
                                                if (result == "Yes")
                                                {
                                                    await viewModel.SubmitClicked();
                                                }
                                            };
                                            await PopupNavigation.Instance.PushAsync(confirmPopup);
                                        }
                                    }
                                    else
                                    {
                                        if (viewModel.IsFDChangeSectionChecked || viewModel.IsAddAdditionalInfoChecked || viewModel.IsAddNewRepresentativeChecked || viewModel.IsChangeEmailChecked)
                                        {
                                            await viewModel.SubmitClicked();
                                        }
                                        else
                                        {
                                            string message = AppResources.ZZVATAmendNoChangesMadeMessage;
                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));
                                        }
                                    }
                                }
                                else
                                {
                                    await viewModel.SubmitClicked();
                                }
                            }
                        }
                    };
                    await PopupNavigation.Instance.PushAsync(popUp);


                    //  String action = await DisplayActionSheet("", AppResources.VATAmendRegistrationCancel, null, viewModel.ListOfActionButtonsApplicableForRegistration.ToArray());

                }
            }
            catch (Exception)
            {
            }

        }
        private void DDlIDTypeSR_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.TxtIDTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR].Name;
                viewModel.SelectedIdTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR];
                if (!viewModel.TxtIDTypeSR.Equals(AppResources.ZZGCCID))
                {
                    if (string.IsNullOrEmpty(((SignUpIdType)e.NewValue).Name) || string.IsNullOrWhiteSpace(((SignUpIdType)e.NewValue).Name))
                    {
                        FrmContactDBO.IsVisible = false;
                        btnSR.IsVisible = false;
                        lblDOB.IsVisible = false;
                    }
                    else
                    {
                        FrmContactDBO.IsVisible = true;
                        btnSR.IsVisible = true;
                        lblDOB.IsVisible = true;
                    }
                }
                else
                {
                    FrmContactDBO.IsVisible = false;
                    btnSR.IsVisible = false;
                    lblDOB.IsVisible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void DDlIDTypeFR_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                FrmFirstName.IsEnabled = false;
                FrmLastName.IsEnabled = false;
                FrmEmailAddress.IsEnabled = false;
                FrmPhoneNumber.IsEnabled = false;
                ClearFinancialRepresentativeData();
                if (viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].ID.Equals("00000"))
                {
                    EntryTINNumber.IsEnabled = true;
                    viewModel.IDTypeIndexFR = 0;
                    viewModel.TxtIDTypeFR = string.Empty;

                    viewModel.IDNumberNonMandatoryVisibility = true;
                    viewModel.IDNumberMandatoryVisibility = false;

                    viewModel.DOBNonMandatoryVisibility = true;
                    viewModel.DOBMandatoryVisibility = false;
                }
                else
                {
                    viewModel.IDNumberNonMandatoryVisibility = false;
                    viewModel.IDNumberMandatoryVisibility = true;

                    // For GCC ID DOB is not mandatory
                    if (viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].ID.Equals("ZS0003"))
                    {
                        viewModel.DOBNonMandatoryVisibility = true; viewModel.TxtIDTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].Name;
                        viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR];
                        EntryTINNumber.Text = string.Empty;
                        EntryIDNo.Text = string.Empty;
                        EntryTINNumber.IsEnabled = true;
                        viewModel.DOBMandatoryVisibility = false;
                    }
                    else
                    {
                        viewModel.DOBNonMandatoryVisibility = false;
                        viewModel.DOBMandatoryVisibility = true;
                    }
                }
            }
            catch (Exception)
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
                bool isImporter = false;
                VATRegistrationPageViewModel.IsComeFromForAttachment = IsComeFromForAttachment.Import;
                if (viewModel.ImporterImageSource == "vat_tile_IbanCard_background.png")
                {
                    isImporter = true;
                    viewModel.VATRegistrationDetailsData.d.ImFg = "1";
                }
                else
                {
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                }
                if (viewModel.ExporterImageSource == "vat_tile_IbanCard_background.png")
                {
                    isImporter = false;

                    viewModel.VATRegistrationDetailsData.d.ExFg = "1";
                }
                else
                {
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                await PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(viewModel.VATRegistrationDetailsData, Models.ZakatInstalationModels.WhichAttachment.VATAmendRegistration, isImporter));
            }
            catch (Exception)
            {
            }
        }



        private void OnPageSelectedForIban(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ((Xamarin.Forms.CollectionView)sender).SelectedItem = null;
            }
            catch (Exception)
            {
            }
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

        public async Task<bool> ValidateIDNumber()
        {
            bool result = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string dob = viewModel.DOB.Replace("/", "");
            if (viewModel.SelectedIdTypeFR?.ID == "ZS0001")
            {
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdnumberFR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            result = false;
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            result = true;
                            viewModel.GpartFR = vATSignUpData.d.Tin;
                            viewModel.FirstnmFR = vATSignUpData.d.Name1;
                            viewModel.LastnmFR = vATSignUpData.d.Name2;

                            viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                            viewModel.SmtpAddrFR = vATSignUpData.d.Email;
                            viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(x => x.ID == vATSignUpData.d.Idtype).FirstOrDefault();
                            if (vATSignUpData.d.Mobile != null && !string.IsNullOrEmpty(vATSignUpData.d.Mobile))
                            {
                                viewModel.MobNumberFR = vATSignUpData.d.Mobile.Substring(5);
                            }
                            FrmFirstName.IsEnabled = false;
                            FrmLastName.IsEnabled = false;
                            FrmEmailAddress.IsEnabled = false;
                            FrmPhoneNumber.IsEnabled = false;
                            viewModel.FrameIDError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            result = false;
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeFR?.ID == "ZS0002")
            {
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {
                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.IdnumberFR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            result = false;
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            result = true;
                            viewModel.GpartFR = vATSignUpData.d.Tin;
                            viewModel.FirstnmFR = vATSignUpData.d.Name1;
                            viewModel.LastnmFR = vATSignUpData.d.Name2;

                            viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                            viewModel.SmtpAddrFR = vATSignUpData.d.Email;
                            FrmFirstName.IsEnabled = false;
                            FrmLastName.IsEnabled = false;
                            FrmEmailAddress.IsEnabled = false;
                            FrmPhoneNumber.IsEnabled = false;
                            viewModel.FrameIDError = false;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            result = false;
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeFR?.ID == "ZS0003")
            {
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {
                        string Result = string.Empty;
                        await Task.Run(async () =>
                        {
                            Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0003", viewModel.IdnumberFR, dob);
                        });
                        VATSignUp vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (string.IsNullOrEmpty(Result) || vATSignUpData == null)
                        {
                            FrmFirstName.IsEnabled = true;
                            FrmLastName.IsEnabled = true;
                            FrmEmailAddress.IsEnabled = true;
                            FrmPhoneNumber.IsEnabled = true;
                        }
                        else if (vATSignUpData.d == null)
                        {
                            result = false;
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            result = true;
                            viewModel.GpartFR = vATSignUpData.d.Tin;
                            viewModel.FirstnmFR = vATSignUpData.d.Name1;
                            viewModel.LastnmFR = vATSignUpData.d.Name2;
                            viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                            viewModel.SmtpAddrFR = vATSignUpData.d.Email;
                            viewModel.MobNumberFR = vATSignUpData.d.Mobile;
                            if (string.IsNullOrEmpty(viewModel.FirstnmFR) && string.IsNullOrEmpty(viewModel.LastnmFR) && string.IsNullOrEmpty(viewModel.MobNumberFR) && string.IsNullOrEmpty(viewModel.SmtpAddrFR))
                            {
                                FrmFirstName.IsEnabled = true;
                                FrmLastName.IsEnabled = true;
                                FrmEmailAddress.IsEnabled = true;
                                FrmPhoneNumber.IsEnabled = true;
                                viewModel.FrameIDError = true;
                            }
                            else
                            {
                                FrmFirstName.IsEnabled = false;
                                FrmLastName.IsEnabled = false;
                                FrmEmailAddress.IsEnabled = false;
                                FrmPhoneNumber.IsEnabled = false;
                                viewModel.FrameIDError = false;
                            }
                        }
                    }
                    catch
                    {
                        try
                        {
                            result = false;
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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
            return await Task.FromResult(result);
        }
        public async void ValidateIDNumberSR()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
            });
            string dob = viewModel.ContactDOB.Replace("/", "");
            if (viewModel.SelectedIdTypeSR.ID == "ZS0001")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {
                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                    }
                    FrmContactName.IsEnabled = false;
                }
            }
            if (viewModel.SelectedIdTypeSR.ID == "ZS0002")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {
                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                    }
                    FrmContactName.IsEnabled = false;
                }
            }
            if (viewModel.SelectedIdTypeSR.ID == "ZS0003")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {
                        string Result = string.Empty;
                        await Task.Run(async () =>
                        {
                            Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0003", viewModel.IdNumberSR, dob);
                        });

                        VATSignUp vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            if (App.VATType == Enums.PageExecutionType.Reactivation)
                            {
                                if (viewModel.FirstNameSR.Contains(string.Empty))
                                {
                                    FrmContactName.IsEnabled = true;
                                }
                                else
                                {
                                    FrmContactName.IsEnabled = false;
                                }
                            }
                            else
                            {
                                FrmContactName.IsEnabled = false;
                            }
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0003", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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

        private async void DOB_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = SignUpDOB.SelectedItem as List<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.DOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                if (string.IsNullOrEmpty(viewModel.DOB))
                {
                    viewModel.FrameDOBError = true;
                }
                else
                {
                    viewModel.FrameDOBError = false;
                }
                await ValidateIDNumber();
            }
            catch (Exception)
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
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryTINNumber.Text))
            {
                if (EntryTINNumber.Text.Substring(0, 1) != "3")
                {
                    Messages.AppendLine(AppResources.ZZTINnumberhastostartwithnumber3);
                    EntryTINNumber.Focus();
                }
                if (EntryTINNumber.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);
                    }
                    Messages.AppendLine(AppResources.ZZTINnumberlengthcannotbelessthan10digits);
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
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                    EntryTINNumber.Text = string.Empty;
                }
                else
                {
                    if (viewModel.VATRegistrationDetailsData != null && viewModel.VATRegistrationDetailsData.d != null)
                    {
                        if (viewModel.VATRegistrationDetailsData.d.Gpartz.Equals(EntryTINNumber.Text))
                        {
                            /// have to change the message
                            Messages.AppendLine(AppResources.SameTincantbeaddedasfinancialrepresentative);

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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                EntryTINNumber.Text = string.Empty;
                            }
                        }
                        else
                        {
                            FrmTINNumber.HasError = false;
                            ValidateTinNumber(viewModel.GpartFR);
                            FrmTINNumber.HasError = false;
                        }
                    }
                    else
                    {
                        FrmTINNumber.HasError = false;
                        ValidateTinNumber(viewModel.GpartFR);
                    }
                }
            }
        }

        private void EntryContactIDNumber_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
            {
                if (viewModel.SelectedIdTypeSR.ID == "ZS0001")
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
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
                        viewModel.FrameContactIDError = true;
                        viewModel.IdNumberSR = string.Empty;
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
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                            viewModel.FrameContactIDError = true;
                            EntryContactIDNumber.Text = string.Empty;
                        }
                        else
                        {
                            viewModel.FrameContactIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.ContactDOB))
                            {
                                ValidateIDNumberSR();
                            }
                        }
                    }
                }
                if (viewModel.SelectedIdTypeSR.ID == "ZS0002")
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
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                            Messages.AppendLine(AppResources.ZZIqamaIDlengthis10digit);
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
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                            viewModel.FrameContactIDError = true;
                            viewModel.IdNumberSR = string.Empty;
                        }
                        else
                        {
                            viewModel.FrameContactIDError = false;
                            if (!string.IsNullOrEmpty(viewModel.ContactDOB))
                            {
                                ValidateIDNumberSR();
                            }
                        }
                    }
                }
                if (viewModel.SelectedIdTypeSR.ID == "ZS0003")
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
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
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
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                        viewModel.FrameContactIDError = true;
                        viewModel.IdNumberSR = string.Empty;
                    }
                    else
                    {
                        ValidateIDNumberSR();
                    }
                }
            }
            else
            {
                viewModel.FrameContactIDError = false;
            }
        }

        private void ContactDOBPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = ContactDOBPicker.SelectedItem as List<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.ContactDOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                ValidateIDNumberContact();
            }
            catch (Exception)
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
            FrmContactName.IsEnabled = false;
            string dob = viewModel.ContactDOB.Replace("/", "");
            if (viewModel.SelectedIdTypeSR.ID == "ZS0001")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeSR.ID == "ZS0002")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {
                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                await Task.Run(() =>
                                {
                                    viewModel.IsLoading = false;
                                });
                            });
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            });
                        }
                        catch (Exception)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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

        public async void ValidateTinNumber(string TinNumber)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });

                string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateTinNumberStringResp(TinNumber);
                VATSignUp vATSignUpData = new VATSignUp();
                vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                if (vATSignUpData.d == null)
                {
                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                    {
                        FrmTINNumber.HasError = true;
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                    else
                    {
                        FrmTINNumber.HasError = false;
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                }
                else
                {
                    viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(obj => obj.ID == vATSignUpData.d.Idtype).FirstOrDefault();
                    if (viewModel.SelectedIdTypeFR.ID == "ZS0003")
                    {
                        NewFRDOBField.IsVisible = false;
                        viewModel.FrameDOBError = false;
                    }
                    viewModel.DOB = vATSignUpData.d.Birthdt10;
                    viewModel.FirstnmFR = vATSignUpData.d.Name1;
                    viewModel.LastnmFR = vATSignUpData.d.Name2;
                    viewModel.MobNumberFR = vATSignUpData.d.Mobile.Substring(5);
                    viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                    viewModel.SmtpAddrFR = vATSignUpData.d.Email;
                    FrmTINNumber.HasError = false;
                    FrmFirstName.IsEnabled = false;
                    FrmLastName.IsEnabled = false;
                    FrmEmailAddress.IsEnabled = false;
                    FrmPhoneNumber.IsEnabled = false;
                    viewModel.FrameIDError = false;
                    viewModel.FrameIDError = false;
                }
            }
            catch
            {
                try
                {
                    string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateTinNumberStringResp(TinNumber);
                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;

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
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        viewModel._navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    });
                }
                catch (HttpRequestException)
                {
                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    });
                }
                catch (Exception)
                {

                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    });
                }
            }
        }


        private void Slider_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void Slider1_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void slider1_completed(object sender, EventArgs e)
        {
            double value = ((Xamarin.Forms.Slider)sender).Value;
        }

        private async void Slider_DragCompleted(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                double value = ((Xamarin.Forms.Slider)sender).Value;
                viewModel.SliderCurrentValue1 = value;

                QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("001", value, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                viewModel.SliderLable1 = obj.QoptTxt;
                foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                {
                    if (item.QueNo == "001")
                    {
                        if (item.QoptNo == obj.QoptNo)
                        {

                            item.QoptAns = "1";
                        }
                        else
                        {
                            item.QoptAns = "0";
                        }
                    }
                }
                viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
                if (registrationDetails != null & registrationDetails.d != null)
                {
                    viewModel.RegTypeCode = registrationDetails.d.RegTy;
                    string code = registrationDetails.d.RegTy;
                    string eligibilityText = string.Empty;
                    viewModel.Attachments = AppResources.Attachments;

                    if (code.Equals("L"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("S"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("V"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("N"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                        viewModel.Attachments = AppResources.Attachments + "*";
                    }
                    else if (code.Equals("M"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        FrmNewAttachment.HasError = false;
                    }
                    viewModel.SliderLable1EligibilityText = eligibilityText;
                }
            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });

            }
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });

        }

        private async void Slider2Dragged(object sender, EventArgs e)
        {
            try
            {
                double value = ((Xamarin.Forms.Slider)sender).Value;
                viewModel.SliderCurrentValue2 = value;

                QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("002", value, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                viewModel.SliderLable2 = obj.QoptTxt;

                foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
                {
                    if (item.QueNo == "002")
                    {
                        if (item.QoptNo == obj.QoptNo)
                        {
                            item.QoptAns = "1";
                        }
                        else
                        {
                            item.QoptAns = "0";
                        }
                    }
                }
                viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
                if (registrationDetails != null & registrationDetails.d != null)
                {
                    viewModel.RegTypeCode = registrationDetails.d.RegTy;
                    string code = registrationDetails.d.RegTy;
                    string eligibilityText = string.Empty;
                    viewModel.Attachments = AppResources.Attachments;

                    if (code.Equals("L"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("S"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("V"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("N"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                        viewModel.Attachments = AppResources.Attachments + "*";
                    }
                    else if (code.Equals("M"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        FrmNewAttachment.HasError = false;
                    }
                    viewModel.SliderLable1EligibilityText = eligibilityText;
                }
            }
            catch (Exception)
            {

            }
        }

        private async void TapppedOnQuestion3First(object sender, EventArgs e)
        {
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
            {
                if (item.QueNo == "003" && item.QoptNo == "031")
                {
                    item.QoptAns = "1";
                }
                if (item.QueNo == "003" && item.QoptNo == "032")
                {
                    item.QoptAns = "0";
                }
            }
            viewModel.quesTion3answerSelected = viewModel.TextQuestion3First;
            viewModel.setQuestionImage();
            viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("L"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("S"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("V"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("N"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";
                }
                else if (code.Equals("M"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                viewModel.SliderLable1EligibilityText = eligibilityText;
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }

        private async void TapppedOnQuestion3Second(object sender, EventArgs e)
        {
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
            {
                if (item.QueNo == "003" && item.QoptNo == "031")
                {
                    item.QoptAns = "0";
                }
                if (item.QueNo == "003" && item.QoptNo == "032")
                {
                    item.QoptAns = "1";
                }
            }
            viewModel.setQuestionImage();
            viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("L"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("S"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("V"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("N"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";
                }
                else if (code.Equals("M"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                viewModel.SliderLable1EligibilityText = eligibilityText;
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }

        private async void TapppedOnQuestion4First(object sender, EventArgs e)
        {
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
            {
                if (item.QueNo == "004" && item.QoptNo == "041")
                {
                    item.QoptAns = "1";
                }
                if (item.QueNo == "004" && item.QoptNo == "042")
                {
                    item.QoptAns = "0";
                }
            }
            viewModel.quesTion4answerSelected = viewModel.TextQuestion4First;
            viewModel.setQuestionImage();
            viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("L"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("S"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("V"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("N"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";
                }
                else if (code.Equals("M"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                viewModel.SliderLable1EligibilityText = eligibilityText;
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }

        private async void TapppedOnQuestion4Second(object sender, EventArgs e)
        {
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results)
            {
                if (item.QueNo == "004" && item.QoptNo == "041")
                {
                    item.QoptAns = "0";
                }
                if (item.QueNo == "004" && item.QoptNo == "042")
                {
                    item.QoptAns = "1";
                }
            }
            viewModel.setQuestionImage();
            viewModel.quesTion4answerSelected = viewModel.TextQuestion4Second;
            viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            Models.VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("L"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("S"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("V"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("N"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";
                }
                else if (code.Equals("M"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                viewModel.SliderLable1EligibilityText = eligibilityText;
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }

        private void DateEntry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(DateEntry.Text))
                {
                    string[] year = viewModel.VatEligibleStartDate.Split('/');
                    int yearnumber = Int32.Parse(year[2]);
                    if (yearnumber >= 2018)
                    {
                        FrmEStartDate.HasError = false;
                        viewModel.IsContinueButtonEnable = true;
                    }
                    else
                    {
                        FrmEStartDate.HasError = true; ;
                        viewModel.IsContinueButtonEnable = false;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void validDateAnswer1present()
        {
        }

        private void ContactDateEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.ContactDOB))
            {
                viewModel.FrameContactDOBError = false;
            }
        }

        private void EntryContactName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.FirstNameSR))
            {
                FrmContactName.HasError = false;
            }
        }

        private void DateEntry_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void EntryContactIDNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.FrameContactIDError = false;
            FrmContactIDNumber.HasError = false;
        }

        private void EntryPhoneNumber_Unfocused_1(object sender, FocusEventArgs e)
        {
            string mobileNUmber = ((BorderlessEntry)sender).Text;

         //  SfTextInputLayout str = (SfTextInputLayout)((BorderlessEntry)sender).Parent;

            if (string.IsNullOrEmpty(mobileNUmber))
            {

                ///str.HasError = true;
                return;
            }
            StringBuilder Messages = new StringBuilder();
            string message = string.Empty;
            if (!string.IsNullOrEmpty(mobileNUmber))
            {
                if (mobileNUmber.Length < 9)
                {
                    message = AppResources.ZZMobilenumberlengthcannotbelessthan9digits;
                    ShowValidationPopup(message);

                   // str.HasError = true;
                    return;
                }
                else if (mobileNUmber.Substring(0, 6) != "009665")
                {
                    message = AppResources.VATAmendMobileNumberValidation;
                    ShowValidationPopup(message);
                   // str.HasError = true;
                    return;
                }
                else
                {
                   // if (mobileNUmber.Length != 15)
                    //{
                        if (mobileNUmber.Length < 15)
                        {
                            message = AppResources.ZZMobilenumberlengthcannotbelessthan9digits;
                        ShowValidationPopup(message);

                    }
                    if (Messages.Length > 0)
                        {
                            ShowValidationPopup(message);
                           // str.HasError = true;
                        }
                      //  else
                            //str.HasError = false;
                    //}
                }
            }
        }

        private void DDlContactIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.TxtIDTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR].Name;
                viewModel.SelectedIdTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR];
            }
            catch (Exception)
            {
            }
        }

        private async void SignUpDOB_Closed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.DOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                if (!string.IsNullOrEmpty(viewModel.DOB))
                {
                    viewModel.FrameDOBError = false;
                }
                else
                {
                    viewModel.FrameDOBError = true;
                }
                await ValidateIDNumber();
            }
            catch (Exception)
            {

            }
        }

        private void ContactDOBPicker_Closed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = ContactDOBPicker.SelectedItem as List<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.ContactDOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                ValidateIDNumberContact();
            }
            catch (Exception)
            {

            }

        }

        private void FDChangeSection_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            viewModel.IsFDChangeSectionEnabled = ((CheckBox)sender).IsChecked;
        }

        private void OnBackTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        private async void VATEligibleDateClicked(object sender, EventArgs e)
        {
            if (App.VATType == Enums.PageExecutionType.Reactivation)
            {
                GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
                genericDatePickerModel.DatePickerTitle = "";
                genericDatePickerModel.PickerId = "EndDateTypePicker";
                try
                {
                    var ssd = App.Locator.CalendarPickerPageView.SelectedDate;
                    await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
                }
                catch (GAZTUnlockAccountException)
                {

                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        viewModel._navigationService.GoBack();
                    });
                }
            }
        }
        public bool IsValid(string emailaddress)
        {
            bool isEmail = Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                FrmEmailAddress.HasError = false;

                return true;
            }
            else
            {
                FrmEmailAddress.HasError = true;
                return false;
            }
        }
        private void EntryEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (viewModel.VATRegistrationDetailsData.d.CONTACTDTSet.results != null)
            {
                //bool flag1 = IsValid(viewModel.ListFinanceRepresenatives[0].SmtpAddrFR);
                bool flag = IsValid(viewModel.SmtpAddrFR);
                if (viewModel.IsAddNewRepresentativeChecked && !flag)
                {
                    ShowValidationPopup(AppResources.ZZPleaseenteravalidEmailAddress);
                    FrmEmailAddress.HasError = true;
                    return;
                }
                else
                    FrmEmailAddress.HasError = false;

            }
        }
        public void ShowValidationPopup(string sourceString)
        {
            PopUp popUp = new PopUp();
            popUp.Message = sourceString;
            popUp.IsLinkAvailable = false;
            if (App.IsArabic)
                popUp.FlowDirections = "RightToLeft";
            else
                popUp.FlowDirections = "LeftToRight";
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(sourceString));
        }
        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {
            FrmPhoneNumber.HasError = false;
            if (string.IsNullOrEmpty(viewModel.MobNumberFR))
            {
                FrmPhoneNumber.HasError = true;
                return;
            }
            StringBuilder Messages = new StringBuilder();
            string message = string.Empty;
            if (!string.IsNullOrEmpty(viewModel.MobNumberFR))
            {
                if (viewModel.MobNumberFR.Length < 9)
                {
                    message = AppResources.ZZMobilenumberlengthcannotbelessthan9digits;
                    ShowValidationPopup(message);
                    FrmPhoneNumber.HasError = true;
                    return;
                }
                else if (viewModel.MobNumberFR.Substring(0, 6) != "009665")
                {
                    message = AppResources.VATAmendMobileNumberValidation;
                    ShowValidationPopup(message);
                    FrmPhoneNumber.HasError = true;
                    return;
                }
                else
                {
                    //if (viewModel.MobNumberFR.Length != 15)
                    //{
                        if (viewModel.MobNumberFR.Length < 15)
                        {
                            message = AppResources.ZZMobilenumberlengthcannotbelessthan9digits;
                        ShowValidationPopup(message);
                        FrmPhoneNumber.HasError = true;
                        return;

                    }
                    if (Messages.Length > 0)
                        {
                            ShowValidationPopup(message);
                            FrmPhoneNumber.HasError = true;
                        }
                        else
                            FrmPhoneNumber.HasError = false;
                   // }
                }
            }
        }

        private void EntryLastName_Unfocused_1(object sender, FocusEventArgs e)
        {
            FrmLastName.HasError = false;
            if (string.IsNullOrEmpty(viewModel.LastnmFR))
            {
                FrmLastName.HasError = true;
            }
        }

        private void EntryFirstName_Unfocused_1(object sender, FocusEventArgs e)
        {
            FrmFirstName.HasError = false;
            if (string.IsNullOrEmpty(viewModel.FirstnmFR))
            {
                FrmFirstName.HasError = true;
            }
        }

        private async void AddNewRepresentative_Tapped(object sender, EventArgs e)
        {
            if (!viewModel.IsAddNewRepresentativeChecked && !viewModel.IsChangeEmailChecked)
            {
                var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.VATAmendAddNewFinancialRepresentativeWarning);
                confirmPopup.OnSelect = (result) =>
                {
                    if (result == "Yes")
                    {
                        viewModel.IsNewFinancialRepVisible = true;
                        viewModel.IsAddNewRepresentativeChecked = true;

                        viewModel.GpartSum =
                        viewModel.IdnumberSum =
                        viewModel.FirstnmSum =
                        viewModel.LastnmSum =
                        viewModel.MobNumberSum =
                        viewModel.SmtpAddrSum = string.Empty;
                        viewModel.TxtIDTypeSum = viewModel.IdTypeListFR.FirstOrDefault()?.Name;
                        Resources["IsAddNewRepresentativeCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    }
                    else
                    {
                        viewModel.IsNewFinancialRepVisible = false;
                        viewModel.IsAddNewRepresentativeChecked = false;
                    }
                };
                await PopupNavigation.Instance.PushAsync(confirmPopup);
            }
            else
            {
                var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.VATAmendReactivationFRUncheckWarning);
                confirmPopup.OnSelect = (result) =>
                {
                    if (result == "Yes")
                    {
                        viewModel.IsNewFinancialRepVisible = false;
                        viewModel.IsAddNewRepresentativeChecked = false;

                        viewModel.GpartSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Gpart;
                        viewModel.IdnumberSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Idnumber;
                        viewModel.FirstnmSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Firstnm;
                        viewModel.LastnmSum = viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Lastnm;
                        viewModel.MobNumberSum = viewModel.VATRegistrationData.d.CONTACTDTSet.results[0].MobNumber;
                        viewModel.SmtpAddrSum = viewModel.VATRegistrationData.d.CONTACTDTSet.results[0].SmtpAddr;
                        viewModel.TxtIDTypeSum = viewModel.IdTypeListFR.Where(x => x.ID == viewModel.VATRegistrationData.d.CONTACT_PERSONSet.results[0].Type).FirstOrDefault()?.Name;
                        Resources["IsAddNewRepresentativeCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                    }
                };
                await PopupNavigation.Instance.PushAsync(confirmPopup);
            }
        }

        private void ChangeMobileNumber_Tapped(object sender, EventArgs e)
        {
            viewModel.IsFDNameMobEmailEnable = true;
            if (viewModel.IsChangeEmailChecked)
            {
                cbAddRepresentative.IsEnabled = false;
            }
            else
            {
                cbAddRepresentative.IsEnabled = true;
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void GoBackToTaxPayerDetails(object sender, EventArgs e)
        {
            viewModel.CurrentIndex = 2;
            viewModel.CurrentStep = AppResources.VATRStep2;
            viewModel.SetVisibility();
            viewModel.IsTaxPayersVisible = true;
            SetsecondBoxColor();
        }

        private void GoBackToSalesDetails(object sender, EventArgs e)
        {
            viewModel.CurrentIndex = 3;
            step3Validation();
        }

        private void GoBackToVATExpenseDetails(object sender, EventArgs e)
        {
            viewModel.CurrentIndex = 3;
            step3Validation();
        }
        private void GoBackToFinacialRepresentativeDetails(object sender, EventArgs e)
        {
            viewModel.CurrentIndex = 4;
            if (viewModel.RegTypeCode == "N")
            {
                if (viewModel.VATRegistrationDetailsData.d.ATTDETSet.results.Count > 0)
                {
                    viewModel.CurrentStep = AppResources.VATRStep5;

                    viewModel.SetVisibility();
                    viewModel.IsFinancialVisible = true;
                    SetfourthBoxColor();
                    if (viewModel.CurrentIndex == 3)
                        viewModel.CurrentIndex++;
                }
                else
                {
                    FrmNewAttachment.HasError = true;
                }
            }
            else
            {
                viewModel.CurrentStep = AppResources.VATRStep5;
                viewModel.SetVisibility();
                viewModel.IsFinancialVisible = true;
                SetfourthBoxColor();
                if (viewModel.CurrentIndex == 3)
                    viewModel.CurrentIndex++;
            }
        }

        private void importerExporterLV_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            var selectedItem = e.AddedItems[0] as ListViewCardTemplateModel;
            if (selectedItem.SelectedCardIcon == "vat_tile_IbanCard_background" && selectedItem.CardLabel == AppResources.VATRImporter)
            {
                viewModel.VATRegistrationDetailsData.d.ImFg = "1";
            }
            else if (selectedItem.SelectedCardIcon == "vat_tile_IbanCard_background" && selectedItem.CardLabel == AppResources.VATRExporter)
            {
                viewModel.VATRegistrationDetailsData.d.ExFg = "1";
            }
            else if (selectedItem.SelectedCardIcon == "vat_tile_IbanCard_background_white" && selectedItem.CardLabel == AppResources.VATRImporter)
            {
                viewModel.VATRegistrationDetailsData.d.ImFg = "0";
            }
            else if (selectedItem.SelectedCardIcon == "vat_tile_IbanCard_background_white" && selectedItem.CardLabel == AppResources.VATRExporter)
            {
                viewModel.VATRegistrationDetailsData.d.ExFg = "0";
            }
        }

        private void SfCheckBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            viewModel.IsFDChangeSectionEnabled = e.IsChecked == true ? true : false;
        }

        private void AddAdditionalInfo_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            viewModel.IsTaxPayerIBANEnabled = e.IsChecked == true ? true : false;
            viewModel.IsTaxPayerEligDateEnabled = e.IsChecked == true ? true : false;
        }

        private void AddAdditionalInfo_CheckedChanged(object sender, bool e)
        {
            viewModel.IsTaxPayerIBANEnabled = e;
            viewModel.IsTaxPayerEligDateEnabled = e;
        }

        private void SfCheckBox_StateChanged(object sender, bool e)
        {

        }
        private void FD_CheckedCanged(object sender, bool e)
        {
            viewModel.IsFDChangeSectionEnabled = e;
        }
    }
}