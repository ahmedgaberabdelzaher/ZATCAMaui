using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.VATAmendReactivationPageViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATAmendReactivationPages
{
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
                viewModel = App.Locator.VATAmendReactivationPageView;
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
                // App.IsArabic = false;
                // App.IsArabic = false;
                viewModel.SetDefaultDate();
                SetLTR();

                Task.Run(async () =>
                {
                    viewModel.IsLoading = true;
                    await GetVatRegistrationData();
                });
                if (App.VATType == Enums.PageExecutionType.Amend)
                    viewModel.PageTitle = AppResources.ZZZZVatRegistrationAmendmentTile;
                else if (App.VATType == Enums.PageExecutionType.Reactivation)
                    viewModel.PageTitle = AppResources.ZZZZVatRegistrationReactivationTile;
                else if (App.VATType == Enums.PageExecutionType.Register)
                    viewModel.PageTitle = AppResources.ZZZZVatRegistrationTile;
                //FrmContactDBO.IsVisible = false;
                //lblDOB.IsVisible = false;

            }
            catch (Exception ex)
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

        private void DpEStartDate_Closed(object sender, EventArgs e)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {

            }
        }


        private async void btnContinue_Clicked(object sender, EventArgs e)
        {
            //  IsSubmitClicked = false;
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
                    //if (viewModel.CurrentIndex == 2)
                    //    viewModel.CurrentIndex++;

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
                            //SetfifthBoxColor();
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

                        //viewModel.IsFinancialVisible = true;
                        viewModel.IsFinancialVisible = true;
                        //SetfifthBoxColor();
                        SetfourthBoxColor();
                        if (viewModel.CurrentIndex == 3)
                            viewModel.CurrentIndex++;
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
                    //SetfifthBoxColor();
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
                        //  IsSubmitClicked = true;
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
            catch (Exception ex)
            {

            }
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
                    //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                    //else
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
                    viewModel.VATRegistrationDetailsData.d.Operationz = "01";

                    Models.VATRegistrationDetails response = await viewModel.SubmitClicked();
                    if (response != null)
                    {
                        viewModel._navigationService.NavigateTo(App.VATAmendReactivationSuccessfulPageView, response);
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
                //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                //else
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
                //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                //else
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
                    //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                    //else
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
                catch (Exception ex)
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
                    //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                    //else
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
                catch (Exception ex)
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
                    //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                    //else
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
                catch (Exception ex)
                {

                }

            }
            else
            {
                try
                {
                    double value = 0;
                    //string AnswerID = viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.results.Where(x => x.QueNo == "002" && x.QoptAns == "1").Select(x => x.QoptNo).FirstOrDefault();
                    //int a = UtilityManager.FindTheAnswerIndexBasedOntheAnswerId("002", AnswerID, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    Slider_Answer2.Value = value;

                    //  viewModel.SliderCurrentValue2 = value; ;
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
                    //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                    //else
                    //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                    //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
                catch (Exception ex)
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
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");

        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

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

                string message = string.Empty;
                MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem", (sender, arg) =>
                {
                    viewModel.VatEligibleStartDate = DateTime.Parse(arg.SelectedValue).Date.ToString("dd/MM/yyyy").Replace('-', '/');
                });
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
                    catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            //var selectedItem = DpEStartDate.SelectedItem as ObservableCollection<object>;
            //string month = selectedItem[1].ToString();
            //string day = selectedItem[0].ToString();
            //string year = selectedItem[2].ToString();
            //viewModel.VatEligibleStartDate = day + "/" + month + "/" + year;
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
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
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
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                            //FrmIDNumber.HasError = true;
                            viewModel.FrameIDError = true;
                            viewModel.IdnumberFR = string.Empty;
                            // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                        }
                        else
                        {
                            ValidateIDNumber();
                        }


                    }
                }
                else
                {
                    // FrmIDNumber.HasError = false;
                    viewModel.FrameIDError = false;
                }


            }
            catch (Exception ex)
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
            // Code commented to implement as per web
            //if (string.IsNullOrEmpty(EntryTINNumber.Text))
            //{
            //    DDlIDType.IsOpen = true;
            //}
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
                    FrmIDNo.IsEnabled = false;
                    EntryTINNumber.IsEnabled = true;
                    viewModel.IDTypeIndexFR = 0;
                    viewModel.TxtIDTypeFR = string.Empty;

                    viewModel.IDNumberNonMandatoryVisibility = true;
                    viewModel.IDNumberMandatoryVisibility = true;

                    viewModel.DOBNonMandatoryVisibility = true;
                    viewModel.DOBMandatoryVisibility = true;
                }
                else
                {
                    viewModel.TxtIDTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR].Name;
                    viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR[viewModel.IDTypeIndexFR];
                    EntryTINNumber.Text = string.Empty;
                    EntryIDNo.Text = string.Empty;
                    EntryTINNumber.IsEnabled = false;
                    FrmIDNo.IsEnabled = true;
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
            catch (Exception ex)
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
            //BoxOne.BackgroundColor = Color.DarkGreen;
            //BoxTwo.BackgroundColor = Color.LightGray;
            //BoxThree.BackgroundColor = Color.LightGray;
            //BoxFour.BackgroundColor = Color.LightGray;
            //BoxFive.BackgroundColor = Color.LightGray;

        }
        public void SetsecondBoxColor()
        {
            //BoxOne.BackgroundColor = Color.LightGray;
            //BoxTwo.BackgroundColor = Color.DarkGreen;
            //BoxThree.BackgroundColor = Color.LightGray;
            //BoxFour.BackgroundColor = Color.LightGray;
            //BoxFive.BackgroundColor = Color.LightGray;
        }
        public void SetthirdBoxColor()
        {
            //BoxOne.BackgroundColor = Color.LightGray;
            //BoxTwo.BackgroundColor = Color.LightGray;
            //BoxThree.BackgroundColor = Color.DarkGreen;
            //BoxFour.BackgroundColor = Color.LightGray;
            //BoxFive.BackgroundColor = Color.LightGray;
        }
        public void SetfourthBoxColor()
        {
            //BoxOne.BackgroundColor = Color.LightGray;
            //BoxTwo.BackgroundColor = Color.LightGray;
            //BoxThree.BackgroundColor = Color.LightGray;
            //BoxFour.BackgroundColor = Color.DarkGreen;
            //BoxFive.BackgroundColor = Color.LightGray;
        }
        public void SetfifthBoxColor()
        {
            //BoxOne.BackgroundColor = Color.LightGray;
            //BoxTwo.BackgroundColor = Color.LightGray;
            //BoxThree.BackgroundColor = Color.LightGray;
            //BoxFour.BackgroundColor = Color.LightGray;
            //BoxFive.BackgroundColor = Color.DarkGreen;
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

            //PopUp popUp = new PopUp();
            //popUp.HeaderText = AppResources.ZZZInformationNew;
            //popUp.IsLinkAvailable = true;
            //popUp.LinkMessage = AppResources.ZVatClickFaqInstructions;
            //if (App.IsArabic)
            //{
            //    popUp.Link = "https://www.vat.gov.sa/ar/vat-rate";
            //}
            //else
            //{
            //    popUp.Link = "https://www.vat.gov.sa/en/vat-rate";
            //}

            //if (App.IsArabic)
            //{
            //    popUp.FlowDirections = "RightToLeft";
            //}
            //else
            //{
            //    popUp.FlowDirections = "LeftToRight";
            //}
            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));

        }

        private async void NewAttachment_Clicked(object sender, EventArgs e)
        {
            try
            {
                DataToPassTofinancialDetailAttachmentPopup sendtoPopup = new DataToPassTofinancialDetailAttachmentPopup();
                sendtoPopup.VATRegistrationDetailsDatatoPopup = viewModel.VATRegistrationDetailsData;
                sendtoPopup.vatRegOthrDetailtoPopup = viewModel.VATRegistrationOtherDetails;
                await PopupNavigation.Instance.PushAsync(new FinancialDetailAttachmentPopupPageView(sendtoPopup));
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
                                //VATRegistrationPageViewModel.IsComeFromForAttachment = IsComeFromForAttachment.General;
                                //await PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(viewModel.VATRegistrationDetailsData));
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
                                //VATRegistrationPageViewModel.IsComeFromForAttachment = IsComeFromForAttachment.General;
                                //await PopupNavigation.Instance.PushAsync(new FileAttachmentPopUpPageView(viewModel.VATRegistrationDetailsData));
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
            catch (Exception ex)
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
                viewModel.TxtIDTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR].Name;
                viewModel.SelectedIdTypeSR = viewModel.IdTypeListSR[viewModel.IDTypeIndexSR];
                if (!viewModel.TxtIDTypeSR.Trim().ToLower().Equals("gcc id"))
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
            catch (Exception ex)
            {


            }
        }

        private void DDlIDTypeFR_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                // if (App.VATType != Enums.PageExecutionType.Amend && App.VATType != Enums.PageExecutionType.Reactivation)
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
            catch (Exception ex)
            {

            }
        }



        private void OnPageSelectedForIban(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ((Xamarin.Forms.CollectionView)sender).SelectedItem = null;
            }
            catch (Exception ex)
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

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdnumberFR, dob);
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
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //FrmIDNumber.HasError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            //  viewModel.FirstnmFR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            // viewModel.DOB = vATSignUpData.d.Birthdt10;
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

                            //  EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
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
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeFR.ID == "ZS0002")
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.IdnumberFR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
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
                            //  EntryName.IsEnabled = false;
                            //FrmIDNumber.HasError = false;
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
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeFR.ID == "ZS0003")
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
                {
                    try
                    {
                        string Result = string.Empty;
                        await Task.Run(async () =>
                        {
                            Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0003", viewModel.IdnumberFR, dob);
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
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {

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
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdnumberFR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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
            // EntryName.IsEnabled = true;
            if (viewModel.SelectedIdTypeSR.ID == "ZS0001")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdNumberSR, dob);
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
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                //FrmIDNumber.HasError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                    FrmContactName.IsEnabled = false;
                }
            }
            if (viewModel.SelectedIdTypeSR.ID == "ZS0002")
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                    FrmContactName.IsEnabled = false;
                }
            }
            if (viewModel.SelectedIdTypeSR.ID == "ZS0003")
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {
                        string Result = string.Empty;
                        await Task.Run(async () =>
                        {
                            Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0003", viewModel.IdNumberSR, dob);
                        });


                        VATSignUp vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {

                            viewModel.FirstNameSR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
                            FrmContactName.IsEnabled = false;

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
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameIDError = true;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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
        //public async void ValidateIDNumber()
        //{

        //        await Task.Run(() =>
        //        {
        //            viewModel.IsLoading = true;
        //        });

        //    string dob = viewModel.DOB.Replace("/", "");
        //    // EntryName.IsEnabled = true;
        //    if (viewModel.SelectedIdTypeFR.ID == "ZS0001")
        //    {
        //        if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
        //        {
        //            try
        //            {

        //                string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdnumberFR, dob);
        //                VATSignUp vATSignUpData = new VATSignUp();
        //                vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
        //                //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
        //                if (vATSignUpData.d == null)
        //                {
        //                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
        //                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
        //                    {
        //                        //FrmIDNumber.HasError = true;
        //                        viewModel.FrameIDError = true;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                    else
        //                    {
        //                        viewModel.FrameIDError = false;
        //                        //FrmIDNumber.HasError = false;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                }
        //                else
        //                {
        //                    //  viewModel.FirstnmFR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
        //                    // viewModel.DOB = vATSignUpData.d.Birthdt10;
        //                    viewModel.FirstnmFR = vATSignUpData.d.Name1;
        //                    viewModel.LastnmFR = vATSignUpData.d.Name2;
        //                    viewModel.MobNumberFR = vATSignUpData.d.Mobile.Substring(5);
        //                    viewModel.IdnumberFR = vATSignUpData.d.Idnum;
        //                    viewModel.SmtpAddrFR = vATSignUpData.d.Email;
        //                    //  EntryName.IsEnabled = false;
        //                    //FrmIDNumber.HasError = false;
        //                    viewModel.FrameIDError = false;
        //                }
        //            }
        //            catch
        //            {
        //                try
        //                {
        //                    string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.IdnumberFR, dob);
        //                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
        //                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
        //                    {
        //                        //FrmIDNumber.HasError = true;
        //                        viewModel.FrameIDError = true;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                    else
        //                    {
        //                        //FrmIDNumber.HasError = false;
        //                        viewModel.FrameIDError = false;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                }
        //                catch (GAZTException gex)
        //                {
        //                    // Handle the GAZT custom exception.
        //                    string MessageForTheUser = gex.Message;
        //                    if (gex is GAZTInvalidDataException)
        //                    {
        //                        MessageForTheUser = AppResources.ZZSomethingwentwrong;
        //                    }
        //                    if (gex is GAZTNetworkConnectivityIssueException)
        //                    {
        //                        MessageForTheUser = AppResources.NetworkConnectivityIssue;
        //                    }
        //                    else if (gex is GAZTInternetException)
        //                    {
        //                        MessageForTheUser = AppResources.ZZInternetConnectionMessage;
        //                    }
        //                    else if (gex is GAZTSessionExpiredException)
        //                    {
        //                        MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
        //                    }

        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        viewModel.IsLoading = false;

        //                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
        //                        viewModel._navigationService.GoBack();
        //                    });
        //                }
        //                catch (InternetException ex)
        //                {
        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
        //                        await Task.Run(() =>
        //                        {
        //                            viewModel.IsLoading = false;
        //                        });
        //                    });
        //                }
        //                catch (HttpRequestException ex)
        //                {
        //                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        // IsLoading = false;

        //                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
        //                        //_navigationService.GoBack();
        //                    });
        //                }
        //                catch (Exception ex)
        //                {

        //                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        // IsLoading = false;

        //                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
        //                        //_navigationService.GoBack();
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    if (viewModel.SelectedIdTypeFR.ID == "ZS0002")
        //    {
        //        //  EntryName.IsEnabled = true;
        //        if (!string.IsNullOrEmpty(viewModel.IdnumberFR))
        //        {
        //            try
        //            {

        //                string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.IdnumberFR, dob);
        //                VATSignUp vATSignUpData = new VATSignUp();
        //                vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
        //                if (vATSignUpData.d == null)
        //                {
        //                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
        //                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
        //                    {
        //                        // FrmIDNumber.HasError = true;
        //                        viewModel.FrameIDError = true;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                    else
        //                    {
        //                        //FrmIDNumber.HasError = false;
        //                        viewModel.FrameIDError = false;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                }
        //                else
        //                {
        //                    viewModel.FirstnmFR = vATSignUpData.d.Name1 + " " + vATSignUpData.d.Name2;
        //                    //  EntryName.IsEnabled = false;
        //                    //FrmIDNumber.HasError = false;
        //                    viewModel.FrameIDError = false;
        //                }
        //            }
        //            catch
        //            {
        //                try
        //                {
        //                    string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdnumberFR, dob);
        //                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
        //                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
        //                    {
        //                        //FrmIDNumber.HasError = true;
        //                        viewModel.FrameIDError = true;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                    else
        //                    {
        //                        //FrmIDNumber.HasError = false;
        //                        viewModel.FrameIDError = false;
        //                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
        //                    }
        //                }
        //                catch (GAZTException gex)
        //                {
        //                    // Handle the GAZT custom exception.
        //                    string MessageForTheUser = gex.Message;
        //                    if (gex is GAZTInvalidDataException)
        //                    {
        //                        MessageForTheUser = AppResources.ZZSomethingwentwrong;
        //                    }
        //                    if (gex is GAZTNetworkConnectivityIssueException)
        //                    {
        //                        MessageForTheUser = AppResources.NetworkConnectivityIssue;
        //                    }
        //                    else if (gex is GAZTInternetException)
        //                    {
        //                        MessageForTheUser = AppResources.ZZInternetConnectionMessage;
        //                    }
        //                    else if (gex is GAZTSessionExpiredException)
        //                    {
        //                        MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
        //                    }

        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        viewModel.IsLoading = false;

        //                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
        //                        viewModel._navigationService.GoBack();
        //                    });
        //                }
        //                catch (InternetException ex)
        //                {
        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
        //                        await Task.Run(() =>
        //                        {
        //                            viewModel.IsLoading = false;
        //                        });
        //                    });
        //                }
        //                catch (HttpRequestException ex)
        //                {
        //                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        // IsLoading = false;

        //                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
        //                        //_navigationService.GoBack();
        //                    });
        //                }
        //                catch (Exception ex)
        //                {

        //                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
        //                    Device.BeginInvokeOnMainThread(async () =>
        //                    {
        //                        // IsLoading = false;

        //                        await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
        //                        //_navigationService.GoBack();
        //                    });
        //                }
        //            }
        //        }
        //    }

        //        await Task.Run(() =>
        //        {
        //            viewModel.IsLoading = false;
        //        });

        //}

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
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
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
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
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
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                            //FrmIDNumber.HasError = true;
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
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                            //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                            //FrmIDNumber.HasError = true;
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
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
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
                        //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                        //FrmIDNumber.HasError = true;
                        viewModel.FrameContactIDError = true;
                        viewModel.IdNumberSR = string.Empty;
                        // EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit
                    }
                    else
                    {
                        ValidateIDNumberSR();
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
            FrmContactName.IsEnabled = false;
            string dob = viewModel.ContactDOB.Replace("/", "");
            // EntryName.IsEnabled = true;
            if (viewModel.SelectedIdTypeSR.ID == "ZS0001")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0001", viewModel.IdNumberSR, dob);
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
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                //FrmIDNumber.HasError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                    }
                }
            }
            if (viewModel.SelectedIdTypeSR.ID == "ZS0002")
            {
                //  EntryName.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0002", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                // FrmIDNumber.HasError = true;
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameContactIDError = false;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
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
                            string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                //FrmIDNumber.HasError = true;
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
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

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                viewModel._navigationService.GoBack();
                            });
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                //_navigationService.GoBack();
                            });
                        }
                        catch (Exception ex)
                        {

                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                // IsLoading = false;

                                //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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

        public async void ValidateTinNumber(string TinNumber)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });

                string Result = await WebServiceManager.GAZTVATSignUpValidateTinNumberStringResp(TinNumber);
                VATSignUp vATSignUpData = new VATSignUp();
                vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                if (vATSignUpData.d == null)
                {
                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                    {
                        //FrmIDNumber.HasError = true;
                        FrmTINNumber.HasError = true;
                        //viewModel.FrameIDError = true;
                        //await viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                    else
                    {
                        FrmTINNumber.HasError = false;
                        //viewModel.FrameIDError = false;
                        //FrmIDNumber.HasError = false;
                        //await   viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
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
                    //viewModel.TypeFR = viewModel.SelectedIdTypeFR.Name
                    // vATSignUpData.d.Idtype
                    FrmTINNumber.HasError = false;
                }
            }
            catch
            {
                try
                {
                    string Result = await WebServiceManager.GAZTVATSignUpValidateTinNumberStringResp(TinNumber);
                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                    {
                        //FrmIDNumber.HasError = true;
                        // FrmTINNumber.HasError = true;
                        //await  viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                    else
                    {
                        //FrmIDNumber.HasError = false;
                        //   FrmTINNumber.HasError = false;
                        //await      viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
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
                        //                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
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
                        // IsLoading = false;

                        //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        //_navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {

                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // IsLoading = false;

                        //await viewModel._dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        //_navigationService.GoBack();
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
                //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                //else
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
            catch (Exception ex)
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
                //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
                //else
                //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
                //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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
            catch (Exception ex)
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
            //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
            //else
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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

                    // eligibilityText = "Not Eligible";
                }
                else if (code.Equals("M"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                    // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
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
            //viewModel.quesTion3answerSelected = viewModel.TextQuestion3Second;
            //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
            //else
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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

                    // eligibilityText = "Not Eligible";
                }
                else if (code.Equals("M"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                    // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
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
            //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
            //else
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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

                    // eligibilityText = "Not Eligible";
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
            //if (App.VATType == Enums.PageExecutionType.Reactivation || App.VATType == Enums.PageExecutionType.Amend || App.VATType == Enums.PageExecutionType.Register)
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "05";
            //else
            //    viewModel.VATRegistrationDetailsData.d.Operationz = "16";
            //viewModel.VATRegistrationDetailsData.d.Operationz = IsSubmitClicked ? "01" : viewModel.VATRegistrationDetailsData.d.Operationz;
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

            // bool has = list.Any(cus => cus.FirstName == "John" && );

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
            catch (Exception ex)
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
            catch (Exception ex)
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
                //viewModel.DOBPrev = viewModel.DOB;


                ValidateIDNumberContact();
            }
            catch (Exception ex)
            {

            }

        }

        private async void AddNewRepresentative_Tapped(object sender, CheckedChangedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked)
            {
                var result = await DisplayAlert("", AppResources.VATAmendAddNewFinancialRepresentativeWarning, AppResources.ZYes, AppResources.ZNo);
                if (result)
                {
                    viewModel.IsNewFinancialRepVisible = ((CheckBox)sender).IsChecked;
                }
                else
                {
                    viewModel.IsNewFinancialRepVisible = !((CheckBox)sender).IsChecked;
                    ((CheckBox)sender).IsChecked = false;
                }

            }
            else
            {
                viewModel.IsNewFinancialRepVisible = ((CheckBox)sender).IsChecked;
            }
        }

        private void AddAdditionalInfo_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
                viewModel.IsTaxPayerIBANEnabled = ((CheckBox)sender).IsChecked;
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

            GenericDatePickerModel genericDatePickerModel = new GenericDatePickerModel();
            genericDatePickerModel.DatePickerTitle = "";
            genericDatePickerModel.PickerId = "EndDateTypePicker";
            try
            {
                var ssd = App.Locator.CalendarPickerPageView.SelectedDate;
                await PopupNavigation.Instance.PushAsync(new CalendarPickerPageView(genericDatePickerModel, true));
            }
            catch (GAZTUnlockAccountException ex)
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
}