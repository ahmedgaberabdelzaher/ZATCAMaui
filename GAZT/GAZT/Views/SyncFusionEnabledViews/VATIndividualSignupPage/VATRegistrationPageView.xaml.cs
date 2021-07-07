using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
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
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATRegistrationPageView : ContentPage
    {
        VATRegistrationPageViewModel viewModel;
        public VATRegistrationPageView()
        {
            try
            {
                InitializeComponent();
                Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];

                viewModel = App.Locator.VATRegistrationPageView;
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                this.BindingContext = viewModel;
                ChangeAeroIcon();
                clearDATA();
                viewModel.SetVisibility();
                viewModel.IsInstrunctionVisible = true;
                viewModel.CurrentStep = AppResources.VATRStep2;
                SetfirstBoxColor();
                viewModel.IsNewAccountClicked = false;
                viewModel.IsInstrunctionChecked = false;
                viewModel.NewAccountText = AppResources.ZTERNewAccount;
               // viewModel.VatEligibleStartDate =string.Empty;
                // App.IsArabic = false;
                // App.IsArabic = false;
                viewModel.SetDefaultDate();
                SetLTR();

               

                SetPickerFont();
            }
            catch (Exception)
            {

            }

        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            DDlIDType.HeaderFontFamily = "SSTArabic-Medium";
                            DDlIDType.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DDlIDType.SelectedItemFontFamily = "SSTArabic-Medium";
                            DDlIDType.UnSelectedItemFontFamily = "SSTArabic-Medium";//ddlLIssuedBy


                            DDlContactIDType.HeaderFontFamily = "SSTArabic-Medium";
                            DDlContactIDType.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DDlContactIDType.SelectedItemFontFamily = "SSTArabic-Medium";
                            DDlContactIDType.UnSelectedItemFontFamily = "SSTArabic-Medium";//dd  

                            DpEStartDate.HeaderFontFamily = "SSTArabic-Medium";
                            DpEStartDate.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            DpEStartDate.SelectedItemFontFamily = "SSTArabic-Medium";
                            DpEStartDate.UnSelectedItemFontFamily = "SSTArabic-Medium";//dd      

                            SignUpDOB.HeaderFontFamily = "SSTArabic-Medium";
                            SignUpDOB.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            SignUpDOB.SelectedItemFontFamily = "SSTArabic-Medium";
                            SignUpDOB.UnSelectedItemFontFamily = "SSTArabic-Medium";//dd   

                            ContactDOBPicker.HeaderFontFamily = "SSTArabic-Medium";
                            ContactDOBPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                            ContactDOBPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                            ContactDOBPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//dd  
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        DDlIDType.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlIDType.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlIDType.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlIDType.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy 

                        DDlContactIDType.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlContactIDType.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlContactIDType.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlContactIDType.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy  

                        DpEStartDate.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DpEStartDate.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DpEStartDate.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DpEStartDate.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy  

                        SignUpDOB.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SignUpDOB.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SignUpDOB.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        SignUpDOB.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy  

                        ContactDOBPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        ContactDOBPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        ContactDOBPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        ContactDOBPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//ddlLIssuedBy  

                        break;
                }
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

        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        private async void DpEStartDate_Closed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = DpEStartDate.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                //viewModel.VatEligibleStartDate = day + "/" + month + "/" + year;
                //          string DOB = year + month + day;
              
                await viewModel.getVatEligibleDate(year+"-"+ month+"-"+ day);

            }
            catch (Exception)
            {

            }
        }

        private void DpEStartDate_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = DpEStartDate.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.VatEligibleStartDate = day + "/" + month + "/" + year;
                //          string DOB = year + month + day;

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
            DpEStartDate.IsOpen = true;
        }

        private void btnImporter_Clicked(object sender, EventArgs e)
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            VATRegistrationDetails vatReg = null;
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void btnExporter_Clicked(object sender, EventArgs e)
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
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
                                viewModel.NewAccountText = AppResources.VATREditAccount;
                            }
                            results1D.Add(result);
                        }

                        if (results1D != null && results1D.Count != 0)
                        {
                            if (viewModel.IbanList != null)
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
                            viewModel.NewAccountText = AppResources.VATREditAccount;
                        }
                    }
                    //                viewModel.IsNewAccountClicked = false;
                }
            }
            catch (Exception)
            {

            }
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
                {//viewModel.IsSalesVisible = true;
                 //SetthirdBoxColor();
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
                            //viewModel.IsFinancialVisible = true;
                            viewModel.IsFinancialVisible = true;
                            //viewModel.CurrentIndex = 4;
                            SetfourthBoxColor();
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

                        //viewModel.IsFinancialVisible = true;
                        viewModel.IsFinancialVisible = true;
                        //viewModel.CurrentIndex = 4;
                        //SetfifthBoxColor();
                        SetfourthBoxColor();
                    }
                }
                else if (viewModel.CurrentStep == AppResources.VATRStep5)
                {

                    //viewModel.IsDeclarationChecked = false;
                    //viewModel.CurrentStep = "Submit";
                    viewModel.CurrentStep = AppResources.ZTEReportCategorySubmitBtn;
                    viewModel.SetVisibility();
                    //viewModel.IsSummaryVisible = true;
                    viewModel.IsSummaryVisible = true;
                    //viewModel.CurrentIndex = 5;
                    SetfifthBoxColor();

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
        public async void step5Validation()
        {
            if (viewModel.IsDeclarationChecked == true)
            {
                bool flag = true;
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
                //if (viewModel.SelectedIdTypeSR.Name != AppResources.ZZGCCID)
                //{
                //    if (string.IsNullOrEmpty(viewModel.ContactDOB))
                //    {
                //        flag = false;
                //        viewModel.FrameContactDOBError = true;

                //    }
                //}
                if (flag)
                {

                    viewModel.VATRegistrationDetailsData.d.Operationz = "01";

                    VATRegistrationDetails response = await viewModel.SubmitClicked();
                    if (response != null)
                    {
                        if (response.d.Operationz.Equals("25"))
                        {
                            if (Navigation.NavigationStack.Count > 0)
                            {
                                Xamarin.Forms.Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                                Navigation.RemovePage(pg1);
                                this.Navigation.PopAsync();
                            }
                            //viewModel._navigationService.GoBack();
                        }
                        else
                        {
                            viewModel._navigationService.NavigateTo(App.VATRegistrationSuccessfullPageView, response);
                        }
                    }
                }
                else
                {
                    //viewModel._dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
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
                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATRAcceptDeclarationToSubmit));
                chkDeclaration.Focus();
                //viewModel.IsContinueButtonEnable = false;
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
                //await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                    //viewModel.IsExpensesVisible = true;
                    viewModel.IsSalesVisible = true;
                    //SetfourthBoxColor();
                    SetthirdBoxColor();

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
                VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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

                        // eligibilityText = "Not Eligible";
                    }
                    else if (code.Equals("M"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
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
                VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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

                        // eligibilityText = "Not Eligible";
                    }
                    else if (code.Equals("M"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
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

                    //double   value = 2;
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
                    VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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

                            // eligibilityText = "Not Eligible";
                        }
                        else if (code.Equals("M"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                            // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    }
                    //  viewModel.SliderLable1EligibilityText = eligibilityText;
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
                    //string AnswerID = viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "001" && x.QoptAns == "1").Select(x => x.QoptNo).FirstOrDefault();
                    ////int a = UtilityManager.FindTheAnswerIndexBasedOntheAnswerId("001", AnswerID, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
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
                    VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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
                    //  viewModel.SliderCurrentValue2 = value; ;
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
                    VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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

                            // eligibilityText = "Not Eligible";
                        }
                        else if (code.Equals("M"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                            // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
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
                    VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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

                            // eligibilityText = "Not Eligible";
                        }
                        else if (code.Equals("M"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                            // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
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
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceived");
            MessagingCenter.Unsubscribe<object, ATTDETSet>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, ATTDETSet>(this, "EligibilitySetAttachmentReceived");

        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;

                Task.Run(async () =>
                {
                    viewModel.IsLoading = true;
                    await GetVatRegistrationData();
                });

                if (Device.RuntimePlatform == Device.Android)
                {
                    DDlIDType.BackgroundColor = Color.FromHex("#f7f7f7");
                    DDlContactIDType.BackgroundColor = Color.FromHex("#f7f7f7");
                }
                else
                {
                    DDlIDType.BackgroundColor = Color.FromHex("#FFFFFF");
                    DDlContactIDType.BackgroundColor = Color.FromHex("#FFFFFF");
                }

                MessagingCenter.Subscribe<VATRegistrationPageViewModel, bool>(this, "IsInstrunctionChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                string message = string.Empty;
                Xamarin.Forms.MessagingCenter.Subscribe<object, string>(this, "IbanReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        message = arg;
                        // firebasemessage = JsonConvert.DeserializeObject<PushnotificationMessage>(arg);
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
                                    viewModel.IbanList = new ObservableCollection<Result2>(viewModel.VATRegistrationDetailsData.d.IBANSet.results);
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
                        //  FrmNewAttachment.HasError = false;
                    }
                });

                //await GetVatRegistrationData();
            }
            catch (Exception)
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
                    setIban();
                });
                //await Task.Run(() =>
                //{
                //    viewModel.IsLoading = false;
                //});
            }
            catch (Exception)
            {

            }
        }
        public void setIban()
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
            try
            {
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
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                viewModel.FrameIDError = true;
                                viewModel.IdnumberFR = string.Empty;
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.DOB))
                                {
                                    ValidateIDNumber();
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
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
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
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
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
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                            viewModel.FrameIDError = true;
                            viewModel.IdnumberFR = string.Empty;
                        }


                    }
                }
                else
                {
                    // FrmIDNumber.HasError = false;
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

                EntryIDNo.IsEnabled = false;
                viewModel.IsFDNameMobEmailEnable = false;
            }
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
            try
            {
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
                    viewModel.TxtIDTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].Name;
                    viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR];
                    EntryTINNumber.Text = string.Empty;
                    EntryIDNo.Text = string.Empty;
                    EntryTINNumber.IsEnabled = false;

                    viewModel.IDNumberNonMandatoryVisibility = false;
                    viewModel.IDNumberMandatoryVisibility = true;


                    // For GCC ID DOB is not mandatory
                    if (viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].ID.Equals("ZS0003"))
                    {
                        viewModel.DOBNonMandatoryVisibility = true;
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
                //viewModel.CurrentIndex = 1;
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
                //viewModel.CurrentIndex = 2;
                viewModel.CurrentStep = AppResources.VATRStep3;
                SetsecondBoxColor();
                setAttachmentImporterExporterVisibility();
            }
            else if (viewModel.IsFinancialVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsSalesVisible = true;
                //viewModel.CurrentIndex = 3;
                viewModel.CurrentStep = AppResources.VATRStep4;
                SetthirdBoxColor();
            }
            else if (viewModel.IsSummaryVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsFinancialVisible = true;
                //viewModel.CurrentIndex = 4;
                viewModel.CurrentStep = AppResources.VATRStep5;
                SetfourthBoxColor();
            }
        }



        private void btnAttachmentDocuments_Clicked(object sender, EventArgs e)
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            VATRegistrationDetails vATRegistrationDetails = null;
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

                    Device.OpenUri(new Uri("https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/default.aspx"));
                }
                else
                {
                    Device.OpenUri(new Uri("https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/default.aspx"));

                }

            }
            catch
            {

            }
        }

        private async void NewAttachment_Clicked(object sender, EventArgs e)
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
            try
            {
                DataToPassTofinancialDetailAttachmentPopup sendtoPopup = new DataToPassTofinancialDetailAttachmentPopup();
                sendtoPopup.VATRegistrationDetailsDatatoPopup = viewModel.VATRegistrationDetailsData;
                sendtoPopup.vatRegOthrDetailtoPopup = viewModel.VATRegistrationOtherDetails;
                await PopupNavigation.Instance.PushAsync(new FinancialDetailAttachmentPopupPageView(sendtoPopup));
            }
            catch (Exception)
            {

            }
        }

        private void TappendOnImporter(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.ImporterImageSource == "selected171x136.png")
                {
                    viewModel.ImporterImageSource = "unselected171x136.png";
                    viewModel.ImporterTextColor = Color.Black;
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                }
                else
                {
                    viewModel.ImporterImageSource = "selected171x136.png";
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
                if (viewModel.ExporterImageSource == "selected171x136.png")
                {
                    viewModel.ExporterImageSource = "unselected171x136.png";
                    viewModel.ExporterTextColor = Color.Black;
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                else
                {
                    viewModel.ExporterImageSource = "selected171x136.png";
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
                    String action = await DisplayActionSheet("", AppResources.ZZCancel, null, viewModel.ListOfActionButtonsApplicableForRegistration.ToArray());


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
                }
            }
            catch (Exception)
            {
            }

            viewModel.VATRegistrationDetailsData.d.Operationz = OperationCode;
            if (!string.IsNullOrEmpty(OperationCode))
            {
                if (OperationCode == "04")
                {
                    if (App.IsArabic)
                    {
                        var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRVoidConfirmationMessage, AppResources.ZNo, AppResources.ZYes);
                        if (!result)
                        {
                            await viewModel.SubmitClicked();
                        }
                    }
                    else
                    {
                        var result = await this.DisplayAlert(AppResources.ZZZConfirmationMsg, AppResources.VATRVoidConfirmationMessage, AppResources.ZYes, AppResources.ZNo);
                        if (result)
                        {
                            await viewModel.SubmitClicked();
                        }
                    }
                }
                else
                {
                    await viewModel.SubmitClicked();
                }
            }
        }
        private void DDlIDTypeSR_OkayButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                if (viewModel.IdTypeListFR[viewModel.IDTypeIndexSR].ID.Equals("00000"))
                {
                    viewModel.IDTypeIndexFR = 0;
                    viewModel.TxtIDTypeSR = string.Empty;

                    viewModel.IDNumberNonMandatoryVisibility = true;
                    viewModel.IDNumberMandatoryVisibility = false;

                    viewModel.DOBNonMandatoryVisibility = true;
                    viewModel.DOBMandatoryVisibilitySM = false;
                    viewModel.DOBMandatoryVisibility = false;

                }
                else
                {
                    viewModel.TxtIDTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR].Name;
                    viewModel.SelectedIdTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR];
                    EntryContactIDNumber.Text = string.Empty;

                    viewModel.IDNumberNonMandatoryVisibility = false;
                    viewModel.IDNumberMandatoryVisibility = true;


                    // For GCC ID DOB is not mandatory
                    if (viewModel.IdTypeListSR[viewModel.IDTypeIndexSR].ID.Equals("ZS0003"))
                    {
                        viewModel.DOBNonMandatoryVisibility = true;
                        viewModel.DOBMandatoryVisibilitySM = false;
                        viewModel.DOBMandatoryVisibility = false;

                    }
                    else
                    {
                        viewModel.DOBNonMandatoryVisibility = false;
                        viewModel.DOBMandatoryVisibilitySM = true;
                        viewModel.DOBMandatoryVisibility = false;

                    }


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
                    viewModel.TxtIDTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].Name;
                    viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR];
                    EntryTINNumber.Text = string.Empty;
                    EntryIDNo.Text = string.Empty;
                    EntryTINNumber.IsEnabled = false;

                    viewModel.IDNumberNonMandatoryVisibility = false;
                    viewModel.IDNumberMandatoryVisibility = true;


                    // For GCC ID DOB is not mandatory
                    if (viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].ID.Equals("ZS0003"))
                    {
                        viewModel.DOBNonMandatoryVisibility = true;
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
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Count > 0) return;
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
            if (viewModel.SelectedIdTypeFR.ID == "ZS0001")
            {
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdnumberFR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstnmFR = vATSignUpData.d.Name1;
                            viewModel.LastnmFR = vATSignUpData.d.Name2;
                            viewModel.FirstnmFR = vATSignUpData.d.Name1;
                            viewModel.LastnmFR = vATSignUpData.d.Name2;
                            viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                            viewModel.SmtpAddrFR = vATSignUpData.d.Email;
                            viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(x => x.ID == vATSignUpData.d.Idtype).FirstOrDefault();
                            if (vATSignUpData.d.Mobile != null && !string.IsNullOrEmpty(vATSignUpData.d.Mobile))
                            {
                                viewModel.MobNumberFR = vATSignUpData.d.Mobile.Substring(5);
                            }
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
            if (viewModel.SelectedIdTypeFR.ID == "ZS0002")
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
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {

                            viewModel.FirstnmFR = vATSignUpData.d.Name1;
                            viewModel.LastnmFR = vATSignUpData.d.Name2;
                            viewModel.FirstnmFR = vATSignUpData.d.Name1;
                            viewModel.LastnmFR = vATSignUpData.d.Name2;
                            viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                            viewModel.SmtpAddrFR = vATSignUpData.d.Email;
                            viewModel.FrameIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameIDError = true;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
            if (string.IsNullOrEmpty(EntryTINNumber.Text))
            {
                SignUpDOB.IsOpen = true;
            }

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

                ValidateIDNumber();
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
                                ValidateIDNumberContact();
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
                                ValidateIDNumber();
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
                var selectedItem = ContactDOBPicker.SelectedItem as ObservableCollection<object>;
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameContactIDError = false;

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
                    viewModel.DOB = vATSignUpData.d.Birthdt10;
                    viewModel.FirstnmFR = vATSignUpData.d.Name1;
                    viewModel.LastnmFR = vATSignUpData.d.Name2;
                    viewModel.MobNumberFR = vATSignUpData.d.Mobile.Substring(5);
                    viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                    viewModel.SmtpAddrFR = vATSignUpData.d.Email;
                    FrmTINNumber.HasError = false;
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

                        //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        viewModel._navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
                VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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
                VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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
            VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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
            viewModel.quesTion3answerSelected = viewModel.TextQuestion3Second;
            viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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
            VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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
            VATRegistrationDetails registrationDetails = await viewModel.SubmitClicked();
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

                    // eligibilityText = "Not Eligible";
                }
                else if (code.Equals("M"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
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

            

            if (e != null && !string.IsNullOrEmpty(e.OldTextValue) && !string.IsNullOrEmpty(e.NewTextValue))
            {

                var keyword = e.NewTextValue;
                if (keyword.Length >= 1)
                {

                    if (viewModel.SelectedIdTypeSR != null || !viewModel.SelectedIdTypeSR.ID.Equals("00000"))
                    {

                        if (viewModel.SelectedIdTypeSR.ID.Equals("ZS0003"))
                        {

                            viewModel.DOBNonMandatoryVisibility = true;
                            viewModel.DOBMandatoryVisibility = false;
                        }
                        else
                        {
                            viewModel.DOBNonMandatoryVisibility = false;
                            viewModel.DOBMandatoryVisibility = true;
                        }


                    }

                }
            }

        }

        private void EntryPhoneNumber_Unfocused_1(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                StringBuilder Message = new StringBuilder();
                PopUp popUp = new PopUp();
                if (EntryPhoneNumber.Text.Substring(0, 1) != "5")
                {
                    Message.Append(AppResources.ZZMobilenumberhastostartwithnumber5);
                }
                if (EntryPhoneNumber.Text.Length != 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);
                    }
                    Message.AppendLine(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                }
                if (Message.Length > 0)
                {
                    popUp.Message = Message.ToString();
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
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                    //FrmMobileNumber.HasError = true;

                    EntryPhoneNumber.Text = string.Empty;
                }
                else
                {
                    //FrmMobileNumber.HasError = false;

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

        private void SignUpDOB_Closed(object sender, EventArgs e)
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
            catch (Exception)
            {

            }
        }

        private void ContactDOBPicker_Closed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = ContactDOBPicker.SelectedItem as ObservableCollection<object>;
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

        private void OnBackTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.GoBack();
        }

        private void IBANAccManagementTapped(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.GAZTBankAccountManagementPageView);

        }
    }
}