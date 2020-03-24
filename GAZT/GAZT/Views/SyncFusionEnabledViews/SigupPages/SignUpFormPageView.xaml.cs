using GAZT.CustomControl;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpFormPageView : ContentPage
    {
        SignUpFormPageViewModel viewModel;
        public SignUpFormPageView()
        {
            try
            {
                viewModel = App.Locator.SignUpFormPageView;

                InitializeComponent();
                this.BindingContext = viewModel;
                ClearFields();
                viewModel.OnPageLoad();
                DDlIDType.SelectedIndex = 0;
                // UsingDDl.SelectedIndex = 1;

                SetLTR();
            }
            catch (Exception ex)
            {

            }


        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // viewModel.PkrDBO = null;
            // DpDbo.NullableDate = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ClearFields();
            viewModel.OnPageLoad();

        }
        public void ClearFields()
        {
            viewModel.IDTypeIndex = 0;
            viewModel.SelectedLOrC = 1;
            viewModel.IsLoading = false;
            viewModel.SelectedSignUpUsing = null;
            viewModel.SignUpUsingList = null;
            viewModel.SelectLCType = null;
            viewModel.LcTypeList = null;
            viewModel.SelectCityList = null;
            viewModel.CityList = null;
            viewModel.IsCRVisible = true;
            viewModel.IsLicenseVisible = false;
            viewModel.IsTIN = false;
            viewModel.IsTINVisible = false;
            viewModel.SelectedIssuedBy = null;
            viewModel.IssuedByList = null;
            viewModel.TxtTIN = string.Empty;
            viewModel.TxtIDNumber = string.Empty;
            viewModel.TxtName = string.Empty;
            viewModel.TxtCRNumber = string.Empty;
            viewModel.TxtLicenseNumber = string.Empty;
            viewModel.TxtEmailAddress = string.Empty;
            viewModel.TxtMobileNumber = string.Empty;
            viewModel.TxtPhoneNumber = string.Empty;
            //viewModel.EnteredCaptchaValue = string.Empty;
            //viewModel.Captcha = string.Empty;
            // DpDbo.Date = NullableDateProperty;

            //  viewModel.PkrDBO = null;
            viewModel.IDTypeModelRootObject = null;
            viewModel.SignUpFirstSubmitModel = null;
            viewModel.MaximumxD = DateTime.Now;


            // DpDbo.Format = "        ";
        }
        private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void btnSubmitNext_Clicked(object sender, EventArgs e)
        {
            bool IsNextValid = true;
            if (viewModel.IsTIN == true)
            {
                if (string.IsNullOrEmpty(viewModel.TxtTIN) || FrmTIN.HasError==true)
                {
                    FrmTIN.HasError = true;
                    IsNextValid = false;
                }
                else
                {
                    FrmTIN.HasError = false;
                }
            }
            if ((viewModel.SelectedSignUpUsing == null )|| (FrmIDType.HasError == true))
            {
                FrmIDType.HasError = true;
                IsNextValid = false;
            }
            else
            {
                FrmIDType.HasError = false;
            }
            if (string.IsNullOrEmpty(viewModel.TxtIDNumber) || (FrmIDNumber.HasError == true))
            {
                FrmIDNumber.HasError = true;
                IsNextValid = false;
            }
            else
            {
                FrmIDNumber.HasError = false;
            }
            if (string.IsNullOrEmpty(viewModel.PkrDBO) || (FrmDBO.HasError == true ))
            {
               // FrmDBO.HasError = true;
                IsNextValid = false;
            }
            else
            {
                FrmDBO.HasError = false;
            }

            if (string.IsNullOrEmpty(viewModel.TxtName) || FrmName.HasError == true)
            {
                FrmName.HasError = true;
                IsNextValid = false;
            }
            else
            {
                FrmName.HasError = false;
            }

            if (viewModel.IsCRChecked == false)
            {
                if (string.IsNullOrEmpty(viewModel.TxtLicenseNumber) || FrmLicenseNumber.HasError == true)
                {
                    FrmLicenseNumber.HasError = true;
                    IsNextValid = false;
                }
                else
                {
                    FrmLicenseNumber.HasError = false;
                }
                if (viewModel.SelectedIssuedBy == null || FrmLicenseIssuedBy.HasError == true)
                {
                    FrmLicenseIssuedBy.HasError = true;
                    IsNextValid = false;
                }
                else
                {
                    FrmLicenseIssuedBy.HasError = false;

                }
                if (viewModel.SelectCityList == null || FrmLicenseIssuedCity.HasError == true)
                {
                    FrmLicenseIssuedCity.HasError = true;
                    IsNextValid = false;
                }
                else
                {
                    FrmLicenseIssuedCity.HasError = false;

                }
            }
            else
            {
                if (string.IsNullOrEmpty(viewModel.TxtCRNumber) || FrmCR.HasError == true)
                {
                    FrmCR.HasError = true;
                    IsNextValid = false;
                }
                else
                {
                    FrmCR.HasError = false;
                }

            }

            if (string.IsNullOrEmpty(viewModel.TxtEmailAddress)|| FrmEmailAddress.HasError == true)
            {
                FrmEmailAddress.HasError = true;
                IsNextValid = false;
            }
            else
            {
                FrmEmailAddress.HasError = false;

            }
            if (string.IsNullOrEmpty(viewModel.TxtMobileNumber)|| FrmMobileNumber.HasError == true)
            {
                FrmMobileNumber.HasError = true;
                IsNextValid = false;
            }
            else
            {
                FrmMobileNumber.HasError = false;

            }
            //if (string.IsNullOrEmpty(viewModel.EnteredCaptchaValue))
            //{
            //    FrmEnteredCaptcha.HasError = true;
            //    IsNextValid = false;
            //}
            //else
            //{
            //    FrmEnteredCaptcha.HasError = false;


            //    bool IsCapValid = viewModel.ValidateCaptcha();
            //    if (IsCapValid == false)
            //    {
            //        FrmEnteredCaptcha.HasError = true;

            //        IsNextValid = false;
            //    }
            //    else
            //    {
            //        FrmEnteredCaptcha.HasError = false;

            //    }
            //}



            if (IsNextValid == false)
            {
                viewModel._dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
            }
            else
            {
                if (viewModel.SelectedSignUpUsing.ID == 1)
                {
                    try
                    {
                        DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", string.Empty, string.Empty);
                        if (ResultDuplicate.d.Flag == "X")
                        {
                            if (viewModel.IsCRChecked == true)
                            {
                                try
                                {
                                    DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", "BUP002", "SA");
                                    if (ResultDuplicateCR.d.Flag == "X")
                                    {
                                        CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();

                                        SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                        if (App.IsArabic)
                                        {
                                            SiguupModel.ALang = "A";
                                        }
                                        else
                                        {
                                            SiguupModel.ALang = "E";
                                        }
                                        SiguupModel.AType = "1";
                                        SiguupModel.AFirstname = viewModel.TxtName;
                                        SiguupModel.ALastname = ".";
                                        if (viewModel.IsTIN)
                                        {
                                            SiguupModel.ATin = viewModel.TxtTIN;
                                            SiguupModel.ATinExist = "X";
                                        }
                                        else
                                        {
                                            SiguupModel.ATin = "";
                                            SiguupModel.ATinExist = "";
                                        }
                                        SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                        if (viewModel.IsCRChecked == true)
                                        {
                                            SiguupModel.ACommId = viewModel.TxtCRNumber;
                                            SiguupModel.ALicenceNo = "";
                                        }
                                        else
                                        {
                                            SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                            SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                            SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                            SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                            SiguupModel.ACommId = "";
                                        }
                                        SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                        SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                        SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                        //SiguupModel.ABirthdt = viewModel.PkrDBO;

                                        SiguupModel.ACity = "";
                                        if (viewModel.SelectedSignUpUsing.ID == 1)
                                        {
                                            SiguupModel.AIdtype = "ZS0001";
                                        }
                                        else if (viewModel.SelectedSignUpUsing.ID == 2)
                                        {
                                            SiguupModel.AIdtype = "ZS0002";
                                        }
                                        else if (viewModel.SelectedSignUpUsing.ID == 3)
                                        {
                                            SiguupModel.AIdtype = "ZS0003";
                                        }
                                        SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;

                                        string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                        SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                        viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                        if (ResultFirstSubmitModel.d == null)
                                        {
                                            SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                            viewModel._dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);

                                        }
                                        else
                                        {
                                            viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                        }

                                    }
                                    else
                                    {
                                        viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                                    }
                                }
                                catch (InternetException ex)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                    });
                                }
                            }
                            else
                            {
                                try
                                {
                                    CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();

                                    SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                    if (App.IsArabic)
                                    {
                                        SiguupModel.ALang = "A";
                                    }
                                    else
                                    {
                                        SiguupModel.ALang = "E";
                                    }
                                    SiguupModel.AType = "1";
                                    SiguupModel.AFirstname = viewModel.TxtName;
                                    SiguupModel.ALastname = ".";
                                    if (viewModel.IsTIN)
                                    {
                                        SiguupModel.ATin = viewModel.TxtTIN;
                                        SiguupModel.ATinExist = "X";
                                    }
                                    else
                                    {
                                        SiguupModel.ATin = "";
                                        SiguupModel.ATinExist = "";
                                    }
                                    SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                    if (viewModel.IsCRChecked == true)
                                    {
                                        SiguupModel.ACommId = viewModel.TxtCRNumber;
                                        SiguupModel.ALicenceNo = "";
                                    }
                                    else
                                    {
                                        SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                        SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                        SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                        SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        SiguupModel.ACommId = "";
                                    }
                                    SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                    SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                    SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                   // SiguupModel.ABirthdt = viewModel.PkrDBO;


                                    if (viewModel.SelectedSignUpUsing.ID == 1)
                                    {
                                        SiguupModel.AIdtype = "ZS0001";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 2)
                                    {
                                        SiguupModel.AIdtype = "ZS0002";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                                    {
                                        SiguupModel.AIdtype = "ZS0003";
                                    }
                                    SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                    string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                    viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                    if (ResultFirstSubmitModel.d == null)
                                    {
                                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                        viewModel._dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);

                                    }
                                    else
                                    {
                                        viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                    }
                                }
                                catch (InternetException ex)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                    });
                                }
                            }
                        }
                        else
                        {
                            viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                        }
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }

                }
                if (viewModel.SelectedSignUpUsing.ID == 2)
                {
                    DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0002", string.Empty, string.Empty);
                    if (ResultDuplicate.d.Flag == "X")
                    {
                        if (viewModel.IsCRChecked == true)
                        {
                            try
                            {
                                DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", "BUP002", "SA");
                                if (ResultDuplicateCR.d.Flag == "X")
                                {
                                    CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();

                                    SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                    if (App.IsArabic)
                                    {
                                        SiguupModel.ALang = "A";
                                    }
                                    else
                                    {
                                        SiguupModel.ALang = "E";
                                    }
                                    SiguupModel.AType = "1";
                                    SiguupModel.AFirstname = viewModel.TxtName;
                                    SiguupModel.ALastname = ".";
                                    if (viewModel.IsTIN)
                                    {
                                        SiguupModel.ATin = viewModel.TxtTIN;
                                        SiguupModel.ATinExist = "X";
                                    }
                                    else
                                    {
                                        SiguupModel.ATin = "";
                                        SiguupModel.ATinExist = "";
                                    }
                                    SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                    if (viewModel.IsCRChecked == true)
                                    {
                                        SiguupModel.ACommId = viewModel.TxtCRNumber;
                                        SiguupModel.ALicenceNo = "";
                                    }
                                    else
                                    {
                                        SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                        SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                        SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                        SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        SiguupModel.ACommId = "";
                                    }
                                    SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                    SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                    SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                    //SiguupModel.ABirthdt = viewModel.PkrDBO;


                                    if (viewModel.SelectedSignUpUsing.ID == 1)
                                    {
                                        SiguupModel.AIdtype = "ZS0001";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 2)
                                    {
                                        SiguupModel.AIdtype = "ZS0002";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                                    {
                                        SiguupModel.AIdtype = "ZS0003";
                                    }
                                    SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                    string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                    viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                    if (ResultFirstSubmitModel.d == null)
                                    {
                                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                        viewModel._dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);

                                    }
                                    else
                                    {
                                        viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                    }
                                }
                                else
                                {
                                    viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                                }
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                        }
                        else
                        {
                            try
                            {
                                CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();

                                SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                if (App.IsArabic)
                                {
                                    SiguupModel.ALang = "A";
                                }
                                else
                                {
                                    SiguupModel.ALang = "E";
                                }
                                SiguupModel.AType = "1";
                                SiguupModel.AFirstname = viewModel.TxtName;
                                SiguupModel.ALastname = ".";
                                if (viewModel.IsTIN)
                                {
                                    SiguupModel.ATin = viewModel.TxtTIN;
                                    SiguupModel.ATinExist = "X";
                                }
                                else
                                {
                                    SiguupModel.ATin = "";
                                    SiguupModel.ATinExist = "";
                                }
                                SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                if (viewModel.IsCRChecked == true)
                                {
                                    SiguupModel.ACommId = viewModel.TxtCRNumber;
                                    SiguupModel.ALicenceNo = "";
                                }
                                else
                                {
                                    SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                    SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                    SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                    SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                    SiguupModel.ACommId = "";
                                }
                                SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                //SiguupModel.ABirthdt = viewModel.PkrDBO;


                                if (viewModel.SelectedSignUpUsing.ID == 1)
                                {
                                    SiguupModel.AIdtype = "ZS0001";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 2)
                                {
                                    SiguupModel.AIdtype = "ZS0002";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 3)
                                {
                                    SiguupModel.AIdtype = "ZS0003";
                                }
                                SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                if (ResultFirstSubmitModel.d == null)
                                {
                                    SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                    viewModel._dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);

                                }
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                        }

                    }
                    else
                    {
                        viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                    }
                }
                if (viewModel.SelectedSignUpUsing.ID == 3)
                {
                    try
                    {
                        DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0003", string.Empty, string.Empty);


                        if (viewModel.IsCRChecked==true)
                        {
                            try
                            {
                                DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", "BUP002", "SA");
                                if (ResultDuplicateCR.d.Flag == "X")
                                {
                                    CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();

                                    SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                    if (App.IsArabic)
                                    {
                                        SiguupModel.ALang = "A";
                                    }
                                    else
                                    {
                                        SiguupModel.ALang = "E";
                                    }
                                    SiguupModel.AType = "1";
                                    SiguupModel.AFirstname = viewModel.TxtName;
                                    SiguupModel.ALastname = ".";
                                    if (viewModel.IsTIN)
                                    {
                                        SiguupModel.ATin = viewModel.TxtTIN;
                                        SiguupModel.ATinExist = "X";
                                    }
                                    else
                                    {
                                        SiguupModel.ATin = "";
                                        SiguupModel.ATinExist = "";
                                    }
                                    SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                    if (viewModel.IsCRChecked == true)
                                    {
                                        SiguupModel.ACommId = viewModel.TxtCRNumber;
                                        SiguupModel.ALicenceNo = "";
                                    }
                                    else
                                    {
                                        SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                        SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                        SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                        SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                        SiguupModel.ACommId = "";
                                    }
                                    SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                    SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                    SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                    //SiguupModel.ABirthdt = viewModel.PkrDBO;


                                    if (viewModel.SelectedSignUpUsing.ID == 1)
                                    {
                                        SiguupModel.AIdtype = "ZS0001";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 2)
                                    {
                                        SiguupModel.AIdtype = "ZS0002";
                                    }
                                    else if (viewModel.SelectedSignUpUsing.ID == 3)
                                    {
                                        SiguupModel.AIdtype = "ZS0003";
                                    }
                                    SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                    string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                    viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                    if (ResultFirstSubmitModel.d == null)
                                    {
                                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                        viewModel._dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);

                                    }
                                    else
                                    {
                                        viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                    }
                                }
                                else
                                {
                                    viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                                }
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                        }
                        else
                        {
                            try
                            {
                                CaseGuidModelRootObject ResutGuid = WebServiceManager.GAZTGetSignupGuid();

                                SignUpNextBodyModel SiguupModel = new SignUpNextBodyModel();
                                if (App.IsArabic)
                                {
                                    SiguupModel.ALang = "A";
                                }
                                else
                                {
                                    SiguupModel.ALang = "E";
                                }
                                SiguupModel.AType = "1";
                                SiguupModel.AFirstname = viewModel.TxtName;
                                SiguupModel.ALastname = ".";
                                if (viewModel.IsTIN)
                                {
                                    SiguupModel.ATin = viewModel.TxtTIN;
                                    SiguupModel.ATinExist = "X";
                                }
                                else
                                {
                                    SiguupModel.ATin = "";
                                    SiguupModel.ATinExist = "";
                                }
                                SiguupModel.AIdnumber = viewModel.TxtIDNumber;
                                if (viewModel.IsCRChecked == true)
                                {
                                    SiguupModel.ACommId = viewModel.TxtCRNumber;
                                    SiguupModel.ALicenceNo = "";
                                }
                                else
                                {
                                    SiguupModel.ALicenceNo = viewModel.TxtLicenseNumber;
                                    SiguupModel.AIssuedBy = viewModel.SelectedIssuedBy.elementCode;
                                    SiguupModel.ACity = viewModel.SelectCityList.CityName;
                                    SiguupModel.ACityCode = viewModel.SelectCityList.CityCode;
                                    SiguupModel.ACommId = "";
                                }
                                SiguupModel.AEmail = viewModel.TxtEmailAddress;
                                SiguupModel.APhone = "00966" + viewModel.TxtPhoneNumber;
                                SiguupModel.AMobile = "00966" + viewModel.TxtMobileNumber;
                                //SiguupModel.ABirthdt = viewModel.PkrDBO;


                                if (viewModel.SelectedSignUpUsing.ID == 1)
                                {
                                    SiguupModel.AIdtype = "ZS0001";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 2)
                                {
                                    SiguupModel.AIdtype = "ZS0002";
                                }
                                else if (viewModel.SelectedSignUpUsing.ID == 3)
                                {
                                    SiguupModel.AIdtype = "ZS0003";
                                }
                                SiguupModel.CaseGuid = ResutGuid.d.results[0].CaseGuid;
                                string ResultFirstSubmit = WebServiceManager.GAZTSignUpFirstSubmit(SiguupModel);
                                SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                if (ResultFirstSubmitModel.d == null)
                                {
                                    SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                                    viewModel._dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);

                                }
                                else
                                {
                                    viewModel._navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
                                }
                            }
                            catch (InternetException ex)
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                        }
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                }
            }

        }

        private void GAZTBorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                try
                {
                    EntryIDNumber.IsEnabled = true;
                    var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;
                    if (selectedItem != null && selectedItem[0] != null)
                    {
                        string month = selectedItem[0].ToString();
                        string day = selectedItem[1].ToString();
                        string year = selectedItem[2].ToString();

                        string DBO = year + month + day;
                        //string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));

                        if (!string.IsNullOrEmpty(EntryIDNumber.Text))
                        {
                            if (viewModel.SelectedSignUpUsing.ID == 1)
                            {
                                if (EntryIDNumber.Text.Substring(0, 1) != "1")
                                {
                                    FrmIDNumber.HasError = true;
                                }
                                else
                                {
                                    FrmIDNumber.HasError = false;
                                    if (EntryIDNumber.Text.Length == 10)
                                    {
                                        try
                                        {
                                            string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                            IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);

                                            if (SignupIsIDTypeValid.d == null)
                                            {

                                                IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                                                if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                                {
                                                    FrmIDNumber.HasError = true;
                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                    //  viewModel.TxtIDNumber = string.Empty;
                                                }
                                                else
                                                {
                                                    FrmIDNumber.HasError = false;
                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                }
                                            }
                                            else
                                            {
                                                viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                                                EntryIDNumber.IsEnabled = false;
                                            }
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                                IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                                                if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                                {
                                                    FrmIDNumber.HasError = true;
                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                    // viewModel.TxtIDNumber = string.Empty;
                                                }
                                                else
                                                {
                                                    FrmIDNumber.HasError = false;
                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                }
                                            }
                                            catch (InternetException ex)
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                                });
                                            }
                                        }
                                    }
                                }

                            }
                            else if (viewModel.SelectedSignUpUsing.ID == 2)
                            {
                                if (EntryIDNumber.Text.Substring(0, 1) != "2")
                                {
                                    FrmIDNumber.HasError = true;
                                }
                                else
                                {
                                    FrmIDNumber.HasError = false;
                                    if (EntryIDNumber.Text.Length == 10)
                                    {
                                        try
                                        {
                                            string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                                            IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                                            if (SignupIsIDTypeValid.d == null)
                                            {

                                                IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                                                if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                                {
                                                    FrmIDNumber.HasError = true;

                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                    //  viewModel.TxtIDNumber = string.Empty;
                                                }
                                                else
                                                {
                                                    FrmIDNumber.HasError = false;
                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                                }
                                            }
                                            else
                                            {
                                                viewModel.TxtName = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                                                EntryIDNumber.IsEnabled = false;
                                            }

                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                                                IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                                                if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                                {
                                                    FrmIDNumber.HasError = true;
                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                    // viewModel.TxtIDNumber = string.Empty;
                                                }
                                                else
                                                {
                                                    FrmIDNumber.HasError = false;
                                                    viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                                }
                                            }
                                            catch (InternetException ex)
                                            {
                                                Device.BeginInvokeOnMainThread(async () =>
                                                {
                                                    viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                            }
                        }
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }
            }
        }

        private void EntryMobileNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryMobileNumber.Text))
            {
                if (EntryMobileNumber.Text.Substring(0, 1) != "5")
                {

                    FrmMobileNumber.HasError = true;

                }
                else
                {
                    FrmMobileNumber.HasError = false;

                }
            }
        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                if (EntryPhoneNumber.Text.Substring(0, 1) != "1")
                {

                    FrmPhoneNumber.HasError = true;

                }
                else
                {
                    FrmPhoneNumber.HasError = false;
                }
            }
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            string day = selectedItem[1].ToString();
            string year = selectedItem[2].ToString();

            string DBO = year + month + day;
            //string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));

            EntryIDNumber.IsEnabled = true;
            if (viewModel.SelectedSignUpUsing.ID == 1)
            {
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                        IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (SignupIsIDTypeValid.d == null)
                        {

                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);

                            }
                        }
                        else
                        {
                            viewModel.TxtIDNumber = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                            EntryIDNumber.IsEnabled = false;
                        }

                    }
                    catch
                    {
                        try
                        {
                            string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            });
                        }

                    }
                }
            }
            if (viewModel.SelectedSignUpUsing.ID == 2)
            {
                EntryIDNumber.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {
                        string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                        IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (SignupIsIDTypeValid.d == null)
                        {

                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.TxtIDNumber = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                            EntryIDNumber.IsEnabled = false;
                        }

                    }
                    catch
                    {
                        try
                        {
                            string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            });
                        }
                    }
                }
            }
        }

        private void EntryCRNumber_Unfocused(object sender, FocusEventArgs e)
        {

            if (!string.IsNullOrEmpty(EntryCRNumber.Text))
            {
                if (EntryCRNumber.Text.Length == 10)
                {
                    FrmCR.HasError = false;
                    CRValidationModelRootObject Result = WebServiceManager.GAZTValidateCRNumber(EntryCRNumber.Text);
                    if (Result != null)
                    {
                        if (Result.d != null)
                        {
                            if (Result.d.NotFound == "X")
                            {
                                FrmCR.HasError = true;
                                viewModel._dialogService.ShowMessage(AppResources.ZZPleaseentervalidCRnumber, AppResources.Information);
                            }
                            else
                            {
                                FrmCR.HasError = false;
                            }
                        }
                    }
                }
                else
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZCommercialReiterationNumbershouddbe10digits;


                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmCR.HasError = true;
                    EntryCRNumber.Text = string.Empty;
                    EntryCRNumber.Focus();
                }

            }
        }

        private void EntryTIN_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    FrmTIN.HasError = true;
                }
                else
                {
                    FrmTIN.HasError = false;
                }
            }
        }

        private void EntryTIN_Unfocused(object sender, FocusEventArgs e)
        {
            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder();
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {

                    Messages.Append(AppResources.ZZTINnumberhastostartwithnumber3);
                    FrmTIN.HasError = true;
                    EntryTIN.Text = string.Empty;
                    EntryTIN.Focus();

                }
                if (EntryTIN.Text.Length != 10)
                {
                    if (Messages.Length > 0)
                    {
                        Messages.Append(Environment.NewLine);

                    }
                    Messages.Append(AppResources.ZZTINnumberlengthcannotbelessthan10digits);

                }
                if (Messages.Length > 0)
                {
                    popUp.Message = Messages.ToString();
                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmTIN.HasError = true;
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    FrmTIN.HasError = false;
                }

            }
        }

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryEmail.Text))
            {
                bool flag = IsValid(EntryEmail.Text);
                if (!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;//ZZPleaseenteravalidEmailAddress//ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;//ZZZInvalidEmailAddressMessage


                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmEmailAddress.HasError = true;
                    EntryEmail.Text = string.Empty;
                }
                else
                {
                    FrmEmailAddress.HasError = false;
                }
            }

        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void EntryMobileNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryMobileNumber.Text))
            {
                StringBuilder Message = new StringBuilder();
                PopUp popUp = new PopUp();
                if (EntryMobileNumber.Text.Substring(0, 1) != "5")
                {


                    Message.Append(AppResources.ZZMobilenumberhastostartwithnumber5);



                }
                if (EntryMobileNumber.Text.Length != 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);

                    }
                    Message.Append(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);



                }
                if (Message.Length > 0)
                {
                    popUp.Message = Message.ToString();
                    popUp.IsLinkAvailable = false;

                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    FrmMobileNumber.HasError = true;
                    EntryMobileNumber.Text = string.Empty;
                }
                else
                {
                    FrmMobileNumber.HasError = false;
                }
            }
        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                PopUp popUp = new PopUp();
                StringBuilder Message = new StringBuilder();
                if (EntryPhoneNumber.Text.Substring(0, 1) != "1")
                {


                    Message.Append(AppResources.ZZPhonenumberhastostartwithnumber1);


                }
                if (EntryPhoneNumber.Text.Length != 9)
                {
                    if (Message.Length > 0)
                    {
                        Message.Append(Environment.NewLine);

                    }
                    Message.Append(AppResources.ZZPhonenumberlengthcannotbelessthan9digits);

                    if (Message.Length > 0)
                    {
                        popUp.Message = Message.ToString();
                        popUp.IsLinkAvailable = false;

                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        FrmPhoneNumber.HasError = true;
                        EntryPhoneNumber.Text = string.Empty;
                        EntryPhoneNumber.Focus();
                    }
                    else
                    {
                        FrmPhoneNumber.HasError = false;
                    }
                }
            }
        }

        private void DDlIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.TxtIDNumber = string.Empty;
            EntryIDNumber.IsEnabled = true;
        }

        private void EntryIDNumber_Unfocused(object sender, FocusEventArgs e)
        {

            PopUp popUp = new PopUp();
            StringBuilder Messages = new StringBuilder(); ;
            if (!string.IsNullOrEmpty(EntryIDNumber.Text))
            {
                if (viewModel.SelectedSignUpUsing.ID == 1)
                {
                    if (EntryIDNumber.Text.Substring(0, 1) != "1")
                    {

                        popUp.Message = AppResources.ZZNationalIDstartswith1;


                        popUp.IsLinkAvailable = false;

                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        FrmIDNumber.HasError = true;
                        EntryIDNumber.Text = string.Empty;

                    }
                }
                else if (viewModel.SelectedSignUpUsing.ID == 2)
                {
                    if (EntryIDNumber.Text.Substring(0, 1) != "2")
                    {
                        
                        popUp.Message = AppResources.ZZIqamaIDstartswith2;


                        popUp.IsLinkAvailable = false;

                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        FrmIDNumber.HasError = true;
                        EntryIDNumber.Text = string.Empty;
                    }
                    if (EntryIDNumber.Text.Length != 10)
                    {
                       
                        if (Messages.Length > 0)
                        {
                            Messages.Append(Environment.NewLine);

                        }
                        Messages.Append(AppResources.ZZPleaseEnterValidId);

                    }
                    if (Messages.Length > 0)
                    {
                        popUp.Message = Messages.ToString();
                        popUp.IsLinkAvailable = false;

                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        FrmTIN.HasError = true;
                        EntryTIN.Text = string.Empty;
                    }
                    else
                    {
                        FrmTIN.HasError = false;
                    }


                }
                else if (viewModel.SelectedSignUpUsing.ID == 3)
                {
                    if(EntryIDNumber.Text.Substring(0, 1) == "0")
                    {
                        //Have to change to neww error message
                        popUp.Message = AppResources.ZZGCCIDdonotstartwith0;


                        popUp.IsLinkAvailable = false;


                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        FrmIDNumber.HasError = true;
                        EntryIDNumber.Text = string.Empty;

                        }
                    else if (!(EntryIDNumber.Text.Length <=15 && EntryIDNumber.Text.Length >= 7))
                    {
                        popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;


                        popUp.IsLinkAvailable = false;


                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));


                        FrmIDNumber.HasError = true;
                        EntryIDNumber.Text = string.Empty;//ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit


                    }
                    
                }


                else
                {
                    FrmIDNumber.HasError = false;
                }
            }
        }

        private void btn1_Clicked(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
        }

        private void DOBpicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;

            //string month = selectedItem[0].ToString();
            //string day = selectedItem[1].ToString();
            //string year = selectedItem[2].ToString();

            //           viewModel.PkrDBO = year + "/" + month + "/" + day;
            var selectedItem = DpDbo.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            string day = selectedItem[1].ToString();
            string year = selectedItem[2].ToString();
            viewModel.PkrDBO = year + "/" + month + "/" + day;
            string DBO = year + month + day;
            //string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));

            EntryIDNumber.IsEnabled = true;
            if (viewModel.SelectedSignUpUsing.ID == 1)
            {
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {

                        string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                        IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (SignupIsIDTypeValid.d == null)
                        {

                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);

                            }
                        }
                        else
                        {
                            viewModel.TxtIDNumber = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                            EntryIDNumber.IsEnabled = false;
                        }

                    }
                    catch
                    {
                        try
                        {
                            string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            });
                        }

                    }
                }
            }
            if (viewModel.SelectedSignUpUsing.ID == 2)
            {
                EntryIDNumber.IsEnabled = true;
                if (!string.IsNullOrEmpty(viewModel.TxtIDNumber))
                {
                    try
                    {
                        string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                        IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
                        if (SignupIsIDTypeValid.d == null)
                        {

                            IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        else
                        {
                            viewModel.TxtIDNumber = SignupIsIDTypeValid.d.Name1 + " " + SignupIsIDTypeValid.d.Name2;
                            EntryIDNumber.IsEnabled = false;
                        }

                    }
                    catch
                    {
                        try
                        {
                            string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                            IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                            if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                            {
                                FrmIDNumber.HasError = true;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.HasError = false;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                viewModel._dialogService.ShowMessage(ex.Message, AppResources.Information);
                            });
                        }
                    }
                }
            }




        }

        private void btnDate_Clicked(object sender, EventArgs e)
        {
            DpDbo.IsOpen = true;
        }

        //private void LOrCSelect_Clicked(object sender, EventArgs e)
        //{
        //    UsingDDl.IsOpen = true;
        //}

        private void LIssuedBy_Clicked(object sender, EventArgs e)
        {
            ddlLIssuedBy.IsOpen = true;
        }

        private void LIssuedByCity_Clicked(object sender, EventArgs e)
        {
            ddlLIssuedByCity.IsOpen = true;
        }

        private void DDlIDType_SelectedIndexChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void EntryEmail_TextChanged(object sender, FocusEventArgs e)
        {



        }
    }
}
