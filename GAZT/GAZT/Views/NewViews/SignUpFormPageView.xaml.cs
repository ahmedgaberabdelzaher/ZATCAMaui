using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
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
            viewModel = App.Locator.SignUpFormPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
            viewModel.OnPageLoad();
            DDlIDType.SelectedIndex = 0;
            UsingDDl.SelectedIndex = 1;
            
            SetLTR();

           
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
                if (string.IsNullOrEmpty(viewModel.TxtTIN))
                {
                    FrmTIN.BorderColor = Color.Red;
                    IsNextValid = false;
                }
                else
                {
                    FrmTIN.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
            if (viewModel.SelectedSignUpUsing == null)
            {
                FrmIDType.BorderColor = Color.Red;
                IsNextValid = false;
            }
            else
            {
                FrmIDType.BorderColor = Color.FromHex("#B1B1B1");
            }
            if (string.IsNullOrEmpty(viewModel.TxtIDNumber))
            {
                FrmIDNumber.BorderColor = Color.Red;
                IsNextValid = false;
            }
            else
            {
                FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
            }
            if (viewModel.PkrDBO==null)
            {
                FrmDBO.BorderColor = Color.Red;
                IsNextValid = false;
            }
            else
            {
                FrmDBO.BorderColor = Color.FromHex("#B1B1B1");
            }

            if (string.IsNullOrEmpty(viewModel.TxtName))
            {
                FrmName.BorderColor = Color.Red;
                IsNextValid = false;
            }
            else
            {
                FrmName.BorderColor = Color.FromHex("#B1B1B1");
            }
            if (viewModel.SelectLCType != null)
            {
                if (viewModel.SelectLCType.ID == 1)
                {
                    if (string.IsNullOrEmpty(viewModel.TxtLicenseNumber))
                    {
                        FrmLicenseNumber.BorderColor = Color.Red;
                        IsNextValid = false;
                    }
                    else
                    {
                        FrmLicenseNumber.BorderColor = Color.FromHex("#B1B1B1");
                    }
                    if (viewModel.SelectedIssuedBy == null)
                    {
                        FrmLicenseIssuedBy.BorderColor = Color.Red;
                        IsNextValid = false;
                    }
                    else
                    {
                        FrmLicenseIssuedBy.BorderColor = Color.FromHex("#B1B1B1");
                    }
                    if (viewModel.SelectCityList == null)
                    {
                        FrmLicenseIssuedCity.BorderColor = Color.Red;
                        IsNextValid = false;
                    }
                    else
                    {
                        FrmLicenseIssuedCity.BorderColor = Color.FromHex("#B1B1B1");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(viewModel.TxtCRNumber))
                    {
                        FrmCR.BorderColor = Color.Red;
                        IsNextValid = false;
                    }
                    else
                    {
                        FrmCR.BorderColor = Color.FromHex("#B1B1B1");
                    }

                }
            }
            if (string.IsNullOrEmpty(viewModel.TxtEmailAddress))
            {
                FrmEmailAddress.BorderColor = Color.Red;
                IsNextValid = false;
            }
            else
            {
                FrmEmailAddress.BorderColor = Color.FromHex("#B1B1B1");
            }
            if (string.IsNullOrEmpty(viewModel.TxtMobileNumber))
            {
                FrmMobileNumber.BorderColor = Color.Red;
                IsNextValid = false;
            }
            else
            {
                FrmMobileNumber.BorderColor = Color.FromHex("#B1B1B1");
            }
            if (string.IsNullOrEmpty(viewModel.EnteredCaptchaValue))
            {
                FrmEnteredCaptcha.BorderColor = Color.Red;
                IsNextValid = false;
            }
            else
            {
                FrmEnteredCaptcha.BorderColor = Color.FromHex("#B1B1B1");

                bool IsCapValid = viewModel.ValidateCaptcha();
                if(IsCapValid==false)
                {
                    FrmEnteredCaptcha.BorderColor = Color.Red;
                    IsNextValid = false;
                }
                else
                {
                    FrmEnteredCaptcha.BorderColor = Color.FromHex("#B1B1B1");
                }
            }



            if (IsNextValid == false)
            {
                viewModel._dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
            }
            else
            {
                if (viewModel.SelectedSignUpUsing.ID == 1)
                {
                    DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber,"ZS0001",string.Empty,string.Empty);
                    if (ResultDuplicate.d.Flag == "X")
                    {
                        if (viewModel.SelectLCType.ID == 2)
                        {
                            DuplicateSignUpModelRootObject ResultDuplicateCR = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0001", "BUP002", "SA");
                            if(ResultDuplicateCR.d.Flag=="X")
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
                                if(viewModel.SelectLCType.ID==2)
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
                                SiguupModel.APhone ="00966"+ viewModel.TxtPhoneNumber;
                                SiguupModel.AMobile ="00966"+ viewModel.TxtMobileNumber;
                               
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
                                SignUpModelRootObject ResultFirstSubmitModel= JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                                viewModel.SignUpFirstSubmitModel = ResultFirstSubmitModel;
                                if (ResultFirstSubmitModel.d==null)
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
                        else
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
                            if (viewModel.SelectLCType.ID == 2)
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
                    }
                    else
                    {
                        viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                    }

                }
                if (viewModel.SelectedSignUpUsing.ID == 2)
                {
                    DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0002", string.Empty, string.Empty);
                    if (ResultDuplicate.d.Flag == "X")
                    {
                        if (viewModel.SelectLCType.ID == 2)
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
                                if (viewModel.SelectLCType.ID == 2)
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
                        else
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
                            if (viewModel.SelectLCType.ID == 2)
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
                    }
                    else
                    {
                        viewModel._dialogService.ShowMessage(AppResources.ZZYoushouldsignupasnewuser, AppResources.Information);
                    }
                }
                if (viewModel.SelectedSignUpUsing.ID == 3)
                {
                    DuplicateSignUpModelRootObject ResultDuplicate = WebServiceManager.GAZTValidateDuplicate(viewModel.TxtIDNumber, "ZS0003", string.Empty, string.Empty);


                    if (viewModel.SelectLCType.ID == 2)
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
                            if (viewModel.SelectLCType.ID == 2)
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
                    else
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
                        if (viewModel.SelectLCType.ID == 2)
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
                   
                }
            }

        }

        private void GAZTBorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));
                if (!string.IsNullOrEmpty(EntryIDNumber.Text))
                {
                    if (viewModel.SelectedSignUpUsing.ID == 1)
                    {
                        if (EntryIDNumber.Text.Substring(0, 1) != "1")
                        {
                            PopUp popUp = new PopUp();
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
                            FrmIDNumber.BorderColor = Color.Red;
                            EntryIDNumber.Text = string.Empty;
                        }
                        else
                        {
                            FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                            if (EntryIDNumber.Text.Length == 10)
                            {
                                try
                                {
                                    string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                    IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);

                                    if(SignupIsIDTypeValid.d==null)
                                    {
                                      
                                        IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                                        if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                        {
                                            FrmIDNumber.BorderColor = Color.Red;
                                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                            viewModel.TxtIDNumber = string.Empty;
                                        }
                                        else
                                        {
                                            FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                        }
                                    }
                                  
                                }
                                catch
                                {
                                    string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                    {
                                        FrmIDNumber.BorderColor = Color.Red;
                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                        viewModel.TxtIDNumber = string.Empty;
                                    }
                                    else
                                    {
                                        FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                    }
                                }
                            }
                        }

                    }
                    else if(viewModel.SelectedSignUpUsing.ID == 2)
                    {
                        if (EntryIDNumber.Text.Substring(0, 1) != "2")
                        {
                            PopUp popUp = new PopUp();
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
                            FrmIDNumber.BorderColor = Color.Red;
                            EntryIDNumber.Text = string.Empty;
                        }
                        else
                        {
                            FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
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
                                            FrmIDNumber.BorderColor = Color.Red;
                                      
                                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                            viewModel.TxtIDNumber = string.Empty;
                                        }
                                        else
                                        {
                                            FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                                        }
                                    }

                                }
                                catch
                                {
                                    string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                                    IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                                    if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                    {
                                        FrmIDNumber.BorderColor = Color.Red;
                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                        viewModel.TxtIDNumber = string.Empty;
                                    }
                                    else
                                    {
                                        FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                                        viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void EntryMobileNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryMobileNumber.Text))
            {
                if (EntryMobileNumber.Text.Substring(0, 1) != "5")
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZMobilenumberhastostartwithnumber5;


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
                    FrmMobileNumber.BorderColor = Color.Red;
                    EntryMobileNumber.Text = string.Empty;
                    EntryMobileNumber.Focus();
                }
                else
                {
                    FrmMobileNumber.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
        }

        private void EntryPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                if (EntryPhoneNumber.Text.Substring(0, 1) != "1")
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPhonenumberhastostartwithnumber1;


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
                    FrmPhoneNumber.BorderColor = Color.Red;
                    EntryPhoneNumber.Text = string.Empty;
                    EntryPhoneNumber.Focus();
                }
                else
                {
                    FrmPhoneNumber.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
        }

        private void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            string DBO = Convert.ToDateTime(DpDbo.Date.ToString().Split(' ')[0]).ToString("yyyyMMdd", new CultureInfo("en-US"));
            if (viewModel.SelectedSignUpUsing.ID == 1)
            {
                if (string.IsNullOrEmpty(viewModel.TxtIDNumber))
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
                                FrmIDNumber.BorderColor = Color.Red;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }

                    }
                    catch
                    {
                        string Result = WebServiceManager.GAZTValidateIDTypes("ZS0001", viewModel.TxtIDNumber, DBO);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            FrmIDNumber.BorderColor = Color.Red;
                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                    }
                }
            }
            if (viewModel.SelectedSignUpUsing.ID == 2)
            {
                if (string.IsNullOrEmpty(viewModel.TxtIDNumber))
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
                                FrmIDNumber.BorderColor = Color.Red;
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                            else
                            {
                                FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                                viewModel._dialogService.ShowMessage(SignupIsIDTypeValidError.error.innererror.errordetails[0].message, AppResources.Information);
                            }
                        }

                    }
                    catch
                    {
                        string Result = WebServiceManager.GAZTValidateIDTypes("ZS0002", viewModel.TxtIDNumber, DBO);
                        IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);

                        if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                        {
                            FrmIDNumber.BorderColor = Color.Red;
                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                        else
                        {
                            FrmIDNumber.BorderColor = Color.FromHex("#B1B1B1");
                            viewModel._dialogService.ShowMessage(SignupIsIDTypeValid.error.innererror.errordetails[0].message, AppResources.Information);
                        }
                    }
                }
            }
        }

        private void EntryCRNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if(!string.IsNullOrEmpty(EntryCRNumber.Text))
            {
                CRValidationModelRootObject Result = WebServiceManager.GAZTValidateCRNumber(EntryCRNumber.Text);
               
                if(Result.d != null)
                {
                    if (Result.d.NotFound == "X")
                    {
                        FrmCR.BorderColor = Color.Red;
                        viewModel._dialogService.ShowMessage(AppResources.ZZPleaseentervalidCRnumber, AppResources.Information);
                    }
                    else
                    {
                        FrmCR.BorderColor = Color.FromHex("#B1B1B1");
                    }
                }                
               
            }
        }

        private void EntryTIN_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Substring(0, 1) != "3")
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZTINnumberhastostartwithnumber3;


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
                    FrmTIN.BorderColor = Color.Red;
                    EntryTIN.Text = string.Empty;
                    EntryTIN.Focus();
                }
                else
                {
                    FrmTIN.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
        }

        private void EntryTIN_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryTIN.Text))
            {
                if (EntryTIN.Text.Length != 10)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZTINnumberlengthcannotbelessthan10digits;


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
                    FrmTIN.BorderColor = Color.Red;
                    EntryTIN.Text = string.Empty;
                }
                else
                {
                    FrmTIN.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
        }

        private void EntryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryEmail.Text))
            {
                bool flag = IsValid(EntryEmail.Text);
                if(!flag)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZEmailAddressdoesnotmatchwithvalueinMinistryofCommerce;


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
                    FrmEmailAddress.BorderColor = Color.Red;
                    EntryEmail.Text = string.Empty;
                }
                else
                {
                    FrmEmailAddress.BorderColor = Color.FromHex("#B1B1B1");
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
            if(!string.IsNullOrEmpty(EntryMobileNumber.Text))
            {
                if (EntryMobileNumber.Text.Length != 9)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZMobilenumberlengthcannotbelessthan9digits;


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
                    FrmMobileNumber.BorderColor = Color.Red;
                    EntryMobileNumber.Text = string.Empty;
                }
                else
                {
                    FrmMobileNumber.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
        }

        private void EntryPhoneNumber_Unfocused(object sender, FocusEventArgs e)
        {
            if (!string.IsNullOrEmpty(EntryPhoneNumber.Text))
            {
                if (EntryPhoneNumber.Text.Length != 9)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = AppResources.ZZPhonenumberlengthcannotbelessthan9digits;


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
                    FrmPhoneNumber.BorderColor = Color.Red;
                    EntryPhoneNumber.Text = string.Empty;
                }
                else
                {
                    FrmPhoneNumber.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
        }

        private void DDlIDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.TxtIDNumber = string.Empty;
        }
    }
}