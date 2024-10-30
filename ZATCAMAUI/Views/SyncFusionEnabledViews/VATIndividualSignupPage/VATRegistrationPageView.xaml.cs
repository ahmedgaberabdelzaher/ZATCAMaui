using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;
using Application = Microsoft.Maui.Controls.Application;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Mangers;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Core.Exceptions;
using Slider = Microsoft.Maui.Controls.Slider;
using ZATCAMAUI.Core.CustomControls;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
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
                this.BindingContext = viewModel;
                clearDATA();
                viewModel.SetVisibility();
                viewModel.IsInstrunctionVisible = true;
                viewModel.CurrentStep = AppResources.VATRStep2;
                viewModel.IsNewAccountClicked = false;
                viewModel.IsInstrunctionChecked = false;
                viewModel.ShouldLoad = true;
                viewModel.NewAccountText = AppResources.ZTERNewAccount;
                viewModel.SetDefaultDate();

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

                await viewModel.getVatEligibleDate(year + "-" + month + "-" + day);
            }
            catch (Exception)
            {


            }
        }

        private void DpEStartDate_OkButtonClicked(object sender, EventArgs e)
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

        private void btn1_Clicked(object sender, EventArgs e)
        {
            DpEStartDate.IsOpen = true;
        }

        private void btnImporter_Clicked(object sender, EventArgs e)
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            VATRegistrationDetails vatReg = null;
            MopupService.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void btnExporter_Clicked(object sender, EventArgs e)
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            VATRegistrationDetails vatReg = null;
            MopupService.Instance.PushAsync(new FileAttachmentPopUpPageView(vatReg));
        }

        private void NewAccount_Clicked(object sender, EventArgs e)
        {
            // viewModel.IsNewAccountClicked = true;
            MopupService.Instance.PushAsync(new NewAccountPopUpPageView(viewModel.VATRegistrationDetailsData.d.OptIban));
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
                }
            }
            catch (Exception)
            {


            }
        }
        public bool step4Validation()
        {   // condition when only one Question is visible
            if (!viewModel.IsResident)
            {
                Question1_Error.IsVisible = false;
                Question3_Error.IsVisible = false;
                Question4_Error.IsVisible = false;
            }

            // Condition when all four questions are visible
            if (Question1_Error.IsVisible || Question2_Error.IsVisible || Question3_Error.IsVisible || Question4_Error.IsVisible)
            {
                return false;
            }
            return true;

        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {


            if (viewModel.IsContinueButtonEnable)
            {
                if (viewModel.CurrentStep == AppResources.VATRStep1)
                {

                    viewModel.CurrentStep = AppResources.VATRStep2;
                    viewModel.SetVisibility();
                    viewModel.IsTaxPayersVisible = true;


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
                    if (step4Validation())
                    {
                        if (viewModel.RegTypeCode == "N")
                        {
                            if (viewModel.VATRegistrationDetailsData.d.ATTDETSet.Count > 0)
                            {
                                viewModel.CurrentStep = AppResources.VATRStep5;

                                viewModel.SetVisibility();
                                viewModel.IsFinancialVisible = true;
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
                        }
                    }
                    else
                    {
                        viewModel.CurrentStep = AppResources.VATRStep4;
                    }
                }
                else if (viewModel.CurrentStep == AppResources.VATRStep5)
                {
                    viewModel.CurrentStep = AppResources.ZTEReportCategorySubmitBtn;
                    viewModel.SetVisibility();
                    viewModel.IsSummaryVisible = true;

                    if (viewModel.VatDeregDeclaration != null && viewModel.VatDeregDeclaration.D != null && string.IsNullOrEmpty(viewModel.VatDeregDeclaration.D.Zterms))
                    {
                        viewModel.IsDeclarationViewEnabled = true;
                        viewModel.Zterms = "";
                    }
                    else
                    {
                        viewModel.IsDeclarationViewEnabledNew = true;
                        viewModel.Zterms = viewModel.VatDeregDeclaration.D.Zterms;
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
                    viewModel.ImporterTextColor = (Color)App.Current.Resources["Primary"];
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";

                }
                else
                {
                    if (viewModel.VATRegistrationDetailsData.d.ImFg.Equals("0"))
                    {
                        viewModel.ImporterImageSource = "vat_tile_IbanCard_background_white.png";
                        viewModel.ImporterTextColor = (Color)App.Current.Resources["Primary"];
                        viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                    }
                    else if (viewModel.VATRegistrationDetailsData.d.ImFg.Equals("1"))
                    {
                        viewModel.ImporterImageSource = "vat_tile_IbanCard_background.png";
                        viewModel.ImporterTextColor = Colors.White;
                        viewModel.VATRegistrationDetailsData.d.ImFg = "1";
                    }

                }

                if (string.IsNullOrEmpty(viewModel.VATRegistrationDetailsData.d.ExFg))
                {
                    viewModel.ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                    viewModel.ExporterTextColor = (Color)App.Current.Resources["Primary"];
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                else
                {
                    if (viewModel.VATRegistrationDetailsData.d.ExFg.Equals("0"))
                    {
                        viewModel.ExporterImageSource = "vat_tile_IbanCard_background_white.png";
                        viewModel.ExporterTextColor = (Color)App.Current.Resources["Primary"];
                        viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                    }
                    else if (viewModel.VATRegistrationDetailsData.d.ExFg.Equals("1"))
                    {
                        viewModel.ExporterImageSource = "vat_tile_IbanCard_background.png";
                        viewModel.ExporterTextColor = Colors.White;
                        viewModel.VATRegistrationDetailsData.d.ExFg = "1";
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        public async Task step5Validation()
        {
            try
            {
                if (viewModel.IsDeclarationChecked == true)
                {
                    bool flag = true;
                    if (!viewModel.IsDeclarationViewEnabledNew) //When Zterms is empty then only do the validation of ID Details.
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
                    }
                    if (flag)
                    {

                        viewModel.VATRegistrationDetailsData.d.Operationz = "01";
                        VATRegistrationDetails response = new VATRegistrationDetails();
                        response.d = await viewModel.SubmitClicked();
                        if (response != null)
                        {
                            if (response.d.Operationz.Equals("25"))
                            {
                                if (Navigation.NavigationStack.Count > 0)
                                {
                                    Page pg1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                                    Navigation.RemovePage(pg1);
                                    await this.Navigation.PopAsync();
                                }
                            }
                            else
                            {
                                await viewModel._navigationService.NavigateTo(App.VATRegistrationSuccessfullPageView, response);
                            }
                            App.HasToRefreshLoaderOnDashboard = true;
                            App.LoginDataRetrieved.VtReg = "X";
                        }
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
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
                    viewModel.IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.VATRAcceptDeclarationToSubmit));
                    chkDeclaration.Focus();
                }
            }
            catch (Exception)
            {
            }



        }

        public async Task step2Validation()
        {
            if (viewModel.IsInstrunctionChecked == true)
            {

                viewModel.CurrentStep = AppResources.VATRStep3;
                viewModel.SetVisibility();
                viewModel.IsTaxPayersVisible = true;

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
                viewModel.IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselecttermsandconditions));
                chkDeclaration.Focus();
            }


        }

        public async Task step3Validation()
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

                    viewModel.IsFDNameMobEmailEnable = false;

                    viewModel.Attachments = AppResources.Attachments;




                    if (viewModel.VATRegistrationDetailsData.d.ResidencyTy == "Resident")
                    {
                        viewModel.IsResident = true;
                        await setAnsWerOneSlider();
                        await setDefaultAnswerThree();
                        await setDefaultansForAnswer4();
                        await setAnsWertwoSlider();


                    }
                    else
                    {
                        viewModel.IsResident = false;
                        await setAnsWertwoSlider();
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

        public async Task setDefaultAnswerThree()
        {
            if (viewModel.answer3selectedcount == 0)
            {
                foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
                registrationDetails.d = await viewModel.SubmitClicked();
                if (registrationDetails != null & registrationDetails.d != null)
                {
                    viewModel.RegTypeCode = registrationDetails.d.RegTy;
                    string code = registrationDetails.d.RegTy;
                    string eligibilityText = string.Empty;
                    viewModel.Attachments = AppResources.Attachments;

                    if (code.Equals("Mandatory Registration large taxpayer group"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    }
                    else if (code.Equals("Mandatory Registration small taxpayer group"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    }
                    else if (code.Equals("Voluntary Registration"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    }
                    else if (code.Equals("Not Eligible"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                        viewModel.Attachments = AppResources.Attachments + "*";

                        // eligibilityText = "Not Eligible";
                    }
                    else if (code.Equals("Mandatory Registration medium taxpayer group"))
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

        public async Task setDefaultansForAnswer4()
        {
            try
            {
                if (viewModel.answer4selectedcount == 0)
                {
                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                    VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
                    registrationDetails.d = await viewModel.SubmitClicked();

                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("Mandatory Registration large taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("Mandatory Registration small taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("Voluntary Registration"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("Not Eligible"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";

                            // eligibilityText = "Not Eligible";
                        }
                        else if (code.Equals("Mandatory Registration medium taxpayer group"))
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
            catch (Exception)
            {
            }
        }

        public async Task setAnsWerOneSlider()
        {
            if (viewModel.answer1selectedcount != 0)
            {
                try
                {

                    //double   value = 2;
                    string AnswerID = viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.Where(x => x.QueNo == "001" && x.QoptAns == "1").Select(x => x.QoptNo).FirstOrDefault();
                    int a = UtilityManager.FindTheAnswerIndexBasedOntheAnswerId("001", AnswerID, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    Slider_Answer1.Value = Convert.ToDouble(a);
                    QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("001", Convert.ToDouble(a), viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    viewModel.SliderLable1 = obj.QoptTxt;
                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                    Models.VATRegistrationDetails registrationDetails = new Models.VATRegistrationDetails();
                    registrationDetails.d = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("Mandatory Registration large taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("Mandatory Registration small taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("Voluntary Registration"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("Not Eligible"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";

                            // eligibilityText = "Not Eligible";
                        }
                        else if (code.Equals("Mandatory Registration medium taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                            // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        viewModel.IsLoading = false;
                    }
                }
                catch (Exception)
                {
                    viewModel.IsLoading = false;
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
                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                    VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
                    registrationDetails.d = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("Mandatory Registration large taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("Mandatory Registration small taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("Voluntary Registration"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("Not Eligible"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";
                        }
                        else if (code.Equals("Mandatory Registration medium taxpayer group"))
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
                    viewModel.IsLoading = false;

                }
            }
        }

        public async Task setAnsWertwoSlider()
        {
            if (viewModel.answer2selectedcount != 0)
            {

                try
                {

                    string AnswerID = viewModel.VATRegistrationDetailsData.d.QUESTIONSSet.Where(x => x.QueNo == "002" && x.QoptAns == "1").Select(x => x.QoptNo).FirstOrDefault();
                    int a = UtilityManager.FindTheAnswerIndexBasedOntheAnswerId("002", AnswerID, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    Slider_Answer2.Value = Convert.ToDouble(a);
                    //  viewModel.SliderCurrentValue2 = value; ;
                    QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("002", Convert.ToDouble(a), viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                    viewModel.SliderLable2 = obj.QoptTxt;

                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                    VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
                    registrationDetails.d = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("Mandatory Registration large taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("Mandatory Registration small taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("Voluntary Registration"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("Not Eligible"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";

                            // eligibilityText = "Not Eligible";
                        }
                        else if (code.Equals("Mandatory Registration medium taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                            // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        viewModel.IsLoading = false;
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

                    foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                    VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
                    registrationDetails.d = await viewModel.SubmitClicked();
                    if (registrationDetails != null & registrationDetails.d != null)
                    {
                        viewModel.RegTypeCode = registrationDetails.d.RegTy;
                        string code = registrationDetails.d.RegTy;
                        string eligibilityText = string.Empty;
                        viewModel.Attachments = AppResources.Attachments;

                        if (code.Equals("Mandatory Registration large taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        }
                        else if (code.Equals("Mandatory Registration small taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        }
                        else if (code.Equals("Voluntary Registration"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        }
                        else if (code.Equals("Not Eligible"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                            viewModel.Attachments = AppResources.Attachments + "*";

                            // eligibilityText = "Not Eligible";
                        }
                        else if (code.Equals("Mandatory Registration medium taxpayer group"))
                        {
                            eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                            // eligibilityText = "Mandatory Registration - Small / Medium Taxpayer Group";
                        }
                        viewModel.SliderLable1EligibilityText = eligibilityText;
                        viewModel.IsLoading = false;
                    }
                    else
                    {
                        viewModel.Attachments = AppResources.Attachments;
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
            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "IbanReceived");
            MessagingCenter.Unsubscribe<object, ATTDETSet>(this, "AttachmentReceived");
            MessagingCenter.Unsubscribe<object, ATTDETSet>(this, "EligibilitySetAttachmentReceived");

        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();



                if (viewModel.ShouldLoad)
                {
                    viewModel.ShouldLoad = false;
                    viewModel.IsLoading = true;
                    await GetVatRegistrationData();
                }

                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    DDlIDType.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
                    DDlContactIDType.BackgroundColor = (Color)Application.Current.Resources["PickerBgGray"];
                }
                else
                {
                    DDlIDType.BackgroundColor = (Color)Application.Current.Resources["White"];
                    DDlContactIDType.BackgroundColor = (Color)Application.Current.Resources["White"];
                }

                MessagingCenter.Subscribe<VATRegistrationPageViewModel, bool>(this, "IsInstrunctionChecked", (obj, res) =>
                {
                    if (res)
                        Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxSelectedFontStyle"];
                    else
                        Resources["IsInstrunctionCheckedStyle"] = App.Current.Resources["CheckboxUnselectedFontStyle"];
                });
                string message = string.Empty;
                MessagingCenter.Subscribe<object, string>(this, "IbanReceived", (sender, arg) =>
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
                                    viewModel.IbanList = new ObservableCollection<Result2>(viewModel.VATRegistrationDetailsData.d.IBANSet);
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

                MessagingCenter.Subscribe<object, List<Attachment>>(this, "AttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.VATRegistrationDetailsData.d.ATTDETSet = arg;
                        FrmNewAttachment.HasError = false;
                        viewModel.ATTDETSetObject = viewModel.VATRegistrationDetailsData.d.ATTDETSet;
                    }
                });
                MessagingCenter.Subscribe<object, List<ResultsItemForDOCSetforsubmit>>(this, "EligibilitySetAttachmentReceived", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        viewModel.VATRegistrationDetailsData.d.ELGBL_DOCSet = arg;
                        //  FrmNewAttachment.HasError = false;
                    }
                });


                getYesCommand();
                getNoCommand();
                MessagingCenter.Subscribe<object, string>(this, "SaveCommandReceived", async (sender, arg) =>
                {
                    String OperationCode = String.Empty;
                    await MopupService.Instance.PopAsync();
                    if (arg != null)
                    {
                        string message1 = arg;
                        if (App.IsArabic)
                        {
                            ArButtons buttonId = ArButtons.None;
                            if (!string.IsNullOrEmpty(message1))
                            {
                                message1 = message1.Replace(" ", "");
                            }
                            Enum.TryParse(message1, out buttonId);
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
                            if (!string.IsNullOrEmpty(message1))
                            {
                                message1 = message1.Replace(" ", "");
                            }
                            Enum.TryParse(message1, out buttonId);
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


                                viewModel.VoidMsg();


                            }
                            else
                            {
                                await viewModel.SubmitClicked();
                            }
                        }
                    }
                });

            }
            catch (Exception)
            {
            }
        }

        public void getYesCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await MopupService.Instance.PopAsync();
                            //viewModel.VATSetReturnVoidAsync();

                            viewModel.VATRegistrationDetailsData.d.Operationz = "04";
                            await viewModel.SubmitClicked();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                    }
                });
            }
            catch (Exception)
            {


            }
        }

        public void getNoCommand()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "NoReceived", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        if (arg == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                        else if (arg == AppResources.ZZZRefundEnableMessage)
                        {
                            await MopupService.Instance.PopAsync();
                        }
                    }
                });
            }
            catch (Exception)
            {


            }
        }

        public async Task GetVatRegistrationData()
        {
            try
            {
                viewModel.IsLoading = true;
                await viewModel.onPageLoad();
                viewModel.IsLoading = false;
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

        private async void EntryIDNo_Unfocused(object sender, FocusEventArgs e)
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
                           await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
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
                              await  MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
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
                          await  MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                              await  MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                viewModel.FrameIDError = true;
                                viewModel.IdnumberFR = string.Empty;
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameIDError = false;
                                if (!string.IsNullOrEmpty(viewModel.DOB))
                                {
                                  await  ValidateIDNumber();
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
                           await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
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
                           await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                            viewModel.FrameIDError = true;
                            viewModel.IdnumberFR = string.Empty;
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

                EntryIDNo.IsEnabled = false;
                viewModel.IsFDNameMobEmailEnable = false;
            }
        }

        private void btnID_Clicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
        }


        private void DDlIDType_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {

                ClearFinancialRepresentativeData();
                CustomSfPicker item = sender as CustomSfPicker;
                viewModel.IDTypeIndexFR = item.Columns[0].SelectedIndex;
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



        private void btnContactID_Clicked(object sender, EventArgs e)
        {
            DDlContactIDType.IsOpen = true;
        }


        private void TappedOnBackButton(object sender, EventArgs e)
        {
            if (viewModel.IsTaxPayersVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsInstrunctionVisible = true;
                viewModel.CurrentStep = AppResources.VATRStep2;

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
                setAttachmentImporterExporterVisibility();
            }
            else if (viewModel.IsFinancialVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsSalesVisible = true;
                //viewModel.CurrentIndex = 3;
                viewModel.CurrentStep = AppResources.VATRStep4;
            }
            else if (viewModel.IsSummaryVisible)
            {
                viewModel.SetVisibility();
                viewModel.IsFinancialVisible = true;
                //viewModel.CurrentIndex = 4;
                viewModel.CurrentStep = AppResources.VATRStep5;
            }
        }



        private void btnAttachmentDocuments_Clicked(object sender, EventArgs e)
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            VATRegistrationDetails vATRegistrationDetails = null;
            MopupService.Instance.PushAsync(new FileAttachmentPopUpPageView(vATRegistrationDetails));
        }

        private void TappedOnMenu(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new VATRegistrationMenuPopUp());
        }

        private void VATFaqTapped(object sender, EventArgs e)
        {
            try
            {
                if (App.IsArabic)
                {

                    Browser.Default.OpenAsync(new Uri("https://gazt.gov.sa/ar/HelpCenter/FAQs/Pages/default.aspx"));
                }
                else
                {
                    Browser.Default.OpenAsync(new Uri("https://gazt.gov.sa/en/HelpCenter/FAQs/Pages/default.aspx"));

                }

            }
            catch
            {

            }
        }

        private async void NewAttachment_Clicked(object sender, EventArgs e)
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            try
            {
                DataToPassTofinancialDetailAttachmentPopup sendtoPopup = new DataToPassTofinancialDetailAttachmentPopup();
                sendtoPopup.VATRegistrationDetailsDatatoPopup = viewModel.VATRegistrationDetailsData;
                sendtoPopup.vatRegOthrDetailtoPopup = viewModel.VATRegistrationOtherDetails;
                await MopupService.Instance.PushAsync(new FinancialDetailAttachmentPopupPageView(sendtoPopup));
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
                    viewModel.ImporterTextColor = (Color)App.Current.Resources["Primary"];
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                }
                else
                {
                    viewModel.ImporterImageSource = "selected171x136.png";
                    viewModel.ImporterTextColor = Colors.White;
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
                    viewModel.ExporterTextColor = (Color)App.Current.Resources["Primary"];
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                else
                {
                    viewModel.ExporterImageSource = "selected171x136.png";
                    viewModel.ExporterTextColor = Colors.White;
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
            await MopupService.Instance.PushAsync(new MoreMenuPopUpPageViewRTwo(viewModel.ListOfActionButtonsApplicable));

        }

        private void DDlIDTypeSR_OkayButtonClicked(object sender, EventArgs e)
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

        private void DDlIDTypeFR_OkButtonClicked(object sender, EventArgs e)
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

        private async void ImporterExporterAttachment(object sender, EventArgs e)
        {
            if (MopupService.Instance.PopupStack.Count > 0) return;
            try
            {
                VATRegistrationPageViewModel.IsComeFromForAttachment = IsComeFromForAttachment.Import;
                if (viewModel.ImporterImageSource == "selected171x136.png")
                {
                    viewModel.VATRegistrationDetailsData.d.ImFg = "1";
                }
                else
                {
                    viewModel.VATRegistrationDetailsData.d.ImFg = "0";
                }
                if (viewModel.ExporterImageSource == "selected171x136.png")
                {
                    viewModel.VATRegistrationDetailsData.d.ExFg = "1";
                }
                else
                {
                    viewModel.VATRegistrationDetailsData.d.ExFg = "0";
                }
                await MopupService.Instance.PushAsync(new FileAttachmentPopUpPageView(viewModel.VATRegistrationDetailsData));
            }
            catch (Exception)
            {


            }
        }


        private void OnPageSelectedForIban(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ((CollectionView)sender).SelectedItem = null;
            }
            catch (Exception)
            {


            }


        }

        public async Task ValidateIDNumber()
        {
            viewModel.IsLoading = true;
            string dob = viewModel.DOB.Replace("/", "-");
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            //viewModel.Title = vATSignUpData.d.taxpayerTitle;
                            viewModel.FirstnmFR = vATSignUpData.d.name1;
                            viewModel.LastnmFR = vATSignUpData.d.name2;
                            viewModel.FirstnmFR = vATSignUpData.d.name1;
                            viewModel.LastnmFR = vATSignUpData.d.name2;
                            viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                            viewModel.SmtpAddrFR = vATSignUpData.d.email;
                            viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(x => x.ID == vATSignUpData.d.Idtype).FirstOrDefault();
                            if (vATSignUpData.d.mobile != null && !string.IsNullOrEmpty(vATSignUpData.d.mobile))
                            {
                                viewModel.MobNumberFR = vATSignUpData.d.mobile;
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            viewModel.IsLoading = false;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            viewModel._navigationService.GoBack();
                        }
                        catch (InternetException ex)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            viewModel.IsLoading = false;
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        }
                        catch (Exception)
                        {


                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {

                            viewModel.FirstnmFR = vATSignUpData.d.name1;
                            viewModel.LastnmFR = vATSignUpData.d.name2;
                            viewModel.FirstnmFR = vATSignUpData.d.name1;
                            viewModel.LastnmFR = vATSignUpData.d.name2;
                            viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                            viewModel.SmtpAddrFR = vATSignUpData.d.email;
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            viewModel.IsLoading = false;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            viewModel._navigationService.GoBack();
                        }
                        catch (InternetException ex)
                        {
                            viewModel.IsLoading = false;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        }
                        catch (Exception)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        }
                    }
                }
            }
            viewModel.IsLoading = false;
        }

        private void btnDate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntryTINNumber.Text))
            {
                SignUpDOB.IsOpen = true;
            }

        }

        private async void DOB_OkButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = SignUpDOB.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.DOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;

                await ValidateIDNumber();
            }
            catch (Exception)
            {


            }
        }


        private void btn4_Clicked(object sender, EventArgs e)
        {
            ContactDOBPicker.IsOpen = true;
        }

        private async void EntryTINNumber_Unfocused(object sender, FocusEventArgs e)
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
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
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
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));

                                EntryTINNumber.Text = string.Empty;
                            }
                        }
                        else
                        {

                            FrmTINNumber.HasError = false;
                           await ValidateTinNumber(viewModel.GpartFR);
                            FrmTINNumber.HasError = false;

                        }
                    }
                    else
                    {

                        FrmTINNumber.HasError = false;
                       await ValidateTinNumber(viewModel.GpartFR);
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
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));
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
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
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
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
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
                            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
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
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
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
                        MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
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

        private async void ContactDOBPicker_OkButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = ContactDOBPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.ContactDOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;
                await ValidateIDNumberContact();
            }
            catch (Exception)
            {


            }
        }

        public async Task ValidateIDNumberContact()
        {
            viewModel.IsLoading = true;
            string dob = viewModel.ContactDOB.Replace("/", "-");
            if (viewModel.SelectedIdTypeSR.ID == "ZS0001")
            {
                if (!string.IsNullOrEmpty(viewModel.IdNumberSR))
                {
                    try
                    {

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDDeclaration("ZS0001", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                viewModel.FrameContactIDError = true;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.name1 + " " + vATSignUpData.d.name2;
                            viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypesDelecration("ZS0001", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                //FrmIDNumber.HasError = false;
                                viewModel.FrameContactIDError = false;

                                //viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                             {
                                 viewModel.IsLoading = false;

                                 await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                 viewModel._navigationService.GoBack();
                             });
                        }
                        catch (InternetException ex)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            viewModel.IsLoading = false;
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        }
                        catch (Exception ex)
                        {


                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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

                        string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDDeclaration("ZS0002", viewModel.IdNumberSR, dob);
                        VATSignUp vATSignUpData = new VATSignUp();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                        if (vATSignUpData.d == null)
                        {
                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                            }
                        }
                        else
                        {
                            viewModel.FirstNameSR = vATSignUpData.d.name1 + " " + vATSignUpData.d.name2;
                            viewModel.FrameContactIDError = false;
                        }
                    }
                    catch
                    {
                        try
                        {
                            string Result = await WebServiceManager.GAZTValidateIDTypesDelecration("ZS0002", viewModel.IdNumberSR, dob);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                viewModel.FrameContactIDError = true;
                                viewModel.IdNumberSR = string.Empty;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                            }
                            else
                            {
                                viewModel.FrameContactIDError = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                            MainThread.BeginInvokeOnMainThread(async () =>
                             {
                                 viewModel.IsLoading = false;
                                 await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                 viewModel._navigationService.GoBack();
                             });
                        }
                        catch (InternetException ex)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            viewModel.IsLoading = false;
                        }
                        catch (HttpRequestException)
                        {
                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        }
                        catch (Exception)
                        {


                            string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        }
                    }
                }
            }
            viewModel.IsLoading = false;
        }

        public async Task ValidateTinNumber(string TinNumber)
        {
            try
            {
                viewModel.IsLoading = false;

                string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateTinNumberStringResp(TinNumber);
                VATSignUp vATSignUpData = new VATSignUp();
                vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                if (vATSignUpData.d == null)
                {
                    IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                    if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                    {
                        FrmTINNumber.HasError = true;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                    else
                    {
                        FrmTINNumber.HasError = false;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                }
                else
                {
                    viewModel.SelectedIdTypeFR = viewModel.IdTypeListFR.Where(obj => obj.ID == vATSignUpData.d.Idtype).FirstOrDefault();
                    viewModel.DOB = vATSignUpData.d.birthDate10;
                    viewModel.FirstnmFR = vATSignUpData.d.name1;
                    viewModel.LastnmFR = vATSignUpData.d.name2;
                    viewModel.MobNumberFR = vATSignUpData.d.mobile;
                    viewModel.IdnumberFR = vATSignUpData.d.Idnum;
                    viewModel.SmtpAddrFR = vATSignUpData.d.email;
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
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                        EntryTINNumber.Text = string.Empty;
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel.IsLoading = false;

                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        viewModel._navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                        await Task.Run(() =>
                        {
                            viewModel.IsLoading = false;
                        });
                    });
                }
                catch (HttpRequestException ex)
                {
                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    });
                }
                catch (Exception ex)
                {


                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    });
                }
            }
        }


        private void slider1_completed(object sender, EventArgs e)
        {
            double value = ((Slider)sender).Value;
        }

        private async void Slider_DragCompleted(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
                double value = ((Slider)sender).Value;
                viewModel.SliderCurrentValue1 = value;
                Question1_Error.IsVisible = false;
                QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("001", value, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                viewModel.SliderLable1 = obj.QoptTxt;

                foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
                registrationDetails.d = await viewModel.SubmitClicked();
                if (registrationDetails != null & registrationDetails.d != null)
                {
                    viewModel.RegTypeCode = registrationDetails.d.RegTy;
                    string code = registrationDetails.d.RegTy;
                    string eligibilityText = string.Empty;
                    viewModel.Attachments = AppResources.Attachments;

                    if (code.Equals("Mandatory Registration large taxpayer group"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("Mandatory Registration small taxpayer group"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("Voluntary Registration"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("Not Eligible"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                        viewModel.Attachments = AppResources.Attachments + "*";
                    }
                    else if (code.Equals("Mandatory Registration medium taxpayer group"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        FrmNewAttachment.HasError = false;
                    }
                    viewModel.SliderLable1EligibilityText = eligibilityText;
                }
            }
            catch (Exception ex)
            {
                viewModel.IsLoading = false;


            }
            viewModel.IsLoading = false;

        }

        private async void Slider2Dragged(object sender, EventArgs e)
        {
            try
            {
                double value = ((Slider)sender).Value;
                viewModel.SliderCurrentValue2 = value;
                //await DisplayAlert("ok", viewModel.SliderCurrentValue2.ToString(), "ok");
                QuestionsetWithMinMax obj = UtilityManager.FindTheAnswerApplicableBasedOntheValue("002", value, viewModel.VATRegistrationDetailsData.d.QUESCONFIG_MSet);
                viewModel.SliderLable2 = obj.QoptTxt;
                Question2_Error.IsVisible = false;
                foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
                VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
                registrationDetails.d = await viewModel.SubmitClicked();
                if (registrationDetails != null & registrationDetails.d != null)
                {
                    viewModel.RegTypeCode = registrationDetails.d.RegTy;
                    string code = registrationDetails.d.RegTy;
                    string eligibilityText = string.Empty;
                    viewModel.Attachments = AppResources.Attachments;

                    if (code.Equals("Mandatory Registration large taxpayer group"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("Mandatory Registration small taxpayer group"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("Voluntary Registration"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                        FrmNewAttachment.HasError = false;
                    }
                    else if (code.Equals("Not Eligible"))
                    {
                        eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                        viewModel.Attachments = AppResources.Attachments + "*";
                    }
                    else if (code.Equals("Mandatory Registration medium taxpayer group"))
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
            Question3_Error.IsVisible = false;
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
            VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
            registrationDetails.d = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("Mandatory Registration large taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Mandatory Registration small taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Voluntary Registration"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Not Eligible"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";
                }
                else if (code.Equals("Mandatory Registration medium taxpayer group"))
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
            Question3_Error.IsVisible = false;
            //await DisplayAlert("ok", viewModel.answer3selectedcount.ToString()+viewModel.quesTion3answerSelected, "ok");
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
            VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
            registrationDetails.d = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("Mandatory Registration large taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Mandatory Registration small taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Voluntary Registration"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Not Eligible"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";
                }
                else if (code.Equals("Mandatory Registration medium taxpayer group"))
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
            Question4_Error.IsVisible = false;
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
            VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
            registrationDetails.d = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("Mandatory Registration large taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Mandatory Registration small taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Voluntary Registration"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Not Eligible"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";
                }
                else if (code.Equals("Mandatory Registration medium taxpayer group"))
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
            Question4_Error.IsVisible = false;
            foreach (var item in viewModel.VATRegistrationDetailsData.d.QUESTIONSSet)
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
            VATRegistrationDetails registrationDetails = new VATRegistrationDetails();
            registrationDetails.d = await viewModel.SubmitClicked();
            if (registrationDetails != null & registrationDetails.d != null)
            {
                viewModel.RegTypeCode = registrationDetails.d.RegTy;
                string code = registrationDetails.d.RegTy;
                string eligibilityText = string.Empty;
                viewModel.Attachments = AppResources.Attachments;

                if (code.Equals("Mandatory Registration large taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrl;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Mandatory Registration small taxpayer group"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestmrs;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Voluntary Registration"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestvr;
                    FrmNewAttachment.HasError = false;
                }
                else if (code.Equals("Not Eligible"))
                {
                    eligibilityText = AppResources.ZZZZEligibilitylableTestne;
                    viewModel.Attachments = AppResources.Attachments + "*";

                    // eligibilityText = "Not Eligible";
                }
                else if (code.Equals("Mandatory Registration medium taxpayer group"))
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
                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));

                    EntryPhoneNumber.Text = string.Empty;
                }
            }
        }

        private void DDlContactIDType_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
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
                //viewModel.DOBPrev = viewModel.DOB;


                await ValidateIDNumber();
            }
            catch (Exception ex)
            {


            }
        }

        private async void ContactDOBPicker_Closed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = ContactDOBPicker.SelectedItem as ObservableCollection<object>;
                string month = selectedItem[1].ToString();
                string day = selectedItem[0].ToString();
                string year = selectedItem[2].ToString();
                viewModel.ContactDOB = year + "/" + month + "/" + day;
                string DOB = year + month + day;

                await ValidateIDNumberContact();
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