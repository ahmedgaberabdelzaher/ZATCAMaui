using CommonServiceLocator;
using System.Globalization;
using System.Diagnostics;
using Newtonsoft.Json;
using Environment = System.Environment;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.AppConfigurations;
using Mopups.Services;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ActivityIndicator;
using ZATCAMAUI.Views.NewDesign.OnboardingPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;
using ZATCAMAUI.Core.Helper;
using GalaSoft.MvvmLight.Views;
using DialogService = ZATCAMAUI.Core.Helper.DialogService;
using NavigationService = ZATCAMAUI.Core.Helper.NavigationService;
using ZATCAMAUI.Views.NewDesign.DashBoardPages;
using ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages;
using AppDynamics.Agent;
using ZATCAMAUI.Views.NewDesign.ForgotPasswordPages;
using ZATCAMAUI.Views.NewDesign.HomePages;

namespace ZATCAMAUI
{
    public partial class App : Application
    {

        // public static string CustomBaseUrl = "http://10.112.34.26:8024/";
        // public static string CustomBaseUrl = "http://10.112.34.38:8024/";
        //public static string VatCustom = "http://172.50.15.39:8443/api/";
        //public static string CustomBaseUrl = "https://stzgw-apic-gov.gazt.gov.sa/gazt-integration/test-third-party/v1/api/customs/";
        // public static string CustomBaseUrl = "https://gw-apic-gov.gazt.gov.sa/gazt-integration/third-party/v1/api/customs/";
        // public static string VatBaseUrl = "https://vatmobile.zatca.gov.sa/api";
        //public static string VatBaseUrl = "http://172.50.15.39:80/api";
        public static string CustomBaseUrl;
        public static string VatCustom;
        public static string VatBaseUrl;
        #region new design views

        public static string GUIDFrSSO { get; set; }
        public static bool successMsg { get; set; }
        public static bool isAndroidUrlloaded = false;
        public static bool isVatEffectDateNav = false;
        public static Stopwatch stopWatch = new Stopwatch();
        public const int defaultTimespan = 30;
        public const int defaultTimespanForLogin = 6;

        public static string GAZTNewDesignVATReturnUpdatedUIPageView = "GAZTNewDesignVATReturnUpdatedUIPageView";
        public static string GAZTNewDesignDashBoardPageView = "GAZTNewDesignDashBoardPageView";
        public static string GAZTNewDesignMyBillsPageView = "GAZTNewDesignMyBillsPageView";
        public static string MyBillsSuccessPageView = "MyBillsSuccessPageView";
        public static string MyBillsSadadDetailsPageView = "MyBillsSadadDetailsPageView";
        public static string GAZTNewDesignMyReturnsNewPageView = "GAZTNewDesignMyReturnsNewPageView";
        public static string GAZTNewDesignOnBoardingAnimationPageView = "GAZTNewDesignOnBoardingAnimationPageView";
        public static string GAZTNewDesignStyleTestUIPageView = "GAZTNewDesignStyleTestUIPageView";
        public static string GAZTNewDesignForgotPasswordPageView = "GAZTNewDesignForgotPasswordPageView";
        public static string VATLookUpNewPageView = "VATLookUpNewPageView";
        public static string ZAKATReturnDetailsView = "ZAKATReturnDetailsView";
        public static string TaxEvasionVerifyMobileNumberPage = "TaxEvasionVerifyMobileNumberPage";
        public static string TaxEvasionPageWebView = "TaxEvasionPageWebView";
        public static string NewTaxEvasionFormSuccessPaveView = "NewTaxEvasionFormSuccessPaveView";
        public static string TaxpayerSubsidyRequest = "TaxpayerSubsidyRequest";


        public static string MyReturnsNewPageView = "MyReturnsNewPageView";
        public static string ZakatDeregistrationPageView = "ZakatDeregistrationPageView";
        public static string TINDeregistrationPageView = "TINDeregistrationPageView";
        public static string VATDeregistrationDetailsPage = "VATDeregistrationDetailsPage";
        public static string VATDeregistrationInstructionsPage = "VATDeregistrationInstructionsPage";
        public static string VATDeregistrationSuccessPage = "VATDeregistrationSuccessPage";
        public static string CalendarPickerPageView = "CalendarPickerPageView";
        public static string PickerPageView = "PickerPageView";
        public static string ZakatRegistrationDetailsListPageView = "ZakatRegistrationDetailsListPageView";
        public static string GeneralServicesListPageView = "GeneralServicesListPageView";
        public static string RefundRequestMenuListPageView = "RefundRequestMenuListPageView";
        public static string FillingFreuencyMenuListPageView = "FillingFreuencyMenuListPageView";
        public static string TINDeregistrationCloseIndividualOutletsPageView = "TINDeregistrationCloseIndividualOutletsPageView";
        public static string ZakatRegistrationOutletsDetails = "ZakatRegistrationOutletsDetails";
        public static string ZakatRegistrationFinancialDetails = "ZakatRegistrationFinancialDetails";
        public static string ZakatRegistrationTaxPayerDetails = "ZakatRegistrationTaxPayerDetails";

        public static string TaxpayersCertificatesPageView = "TaxpayersCertificatesPageView";
        public static string GAZTNewDesignRecoverUsername = nameof(GAZTNewDesignRecoverUsername);
        public static string GAZTNewDesignRecoverPasswordPageView = nameof(GAZTNewDesignRecoverPasswordPageView);
        public static string GAZTForm5PageView = "GAZTFORM5PageView";
        public static string TaxpayerCorrespondancePageView = "TaxpayerCorrespondancePageView";
        public static string TaxpayerCorrespondanceDetailPageView = "TaxpayerCorrespondanceDetailPageView";
        public static string NewZakatObjectionPageView = "NewZakatObjectionPageView";
        public static string DashboardAnonymousMenuPageView = "DashboardAnonymousMenuPageView";
        //*EST
        public static string ActivityItemPage = nameof(ActivityItemPage);
        public static string ActivityItemAmendUpdatePage = nameof(ActivityItemAmendUpdatePage);
        public static string EstablishmentRegistrationPage = nameof(EstablishmentRegistrationPage);
        public static string EstablishmentAmendUpdatePage = nameof(EstablishmentAmendUpdatePageView);
        public static string OutletDetailsPageView = nameof(OutletDetailsPageView);
        public static string OutletDetailsAmendUpdatePageView = nameof(OutletDetailsAmendUpdatePageView);
        public static string RegistrationSuccessfulPage = nameof(RegistrationSuccessfulPage);
        public static string EstablishmentAmendUpdateSuccessfulPage = nameof(EstablishmentAmendUpdateSuccessfulPage);
        //*End EST

        //test
        public static string AttachmentPopupPageView = "AttachmentPopupPageView";
        public static string VATReturnSuccessfullPageView = "VATReturnSuccessfullPageView";
        public static string VatReturnNewSuccessPageView = "VatReturnNewSuccessPageView";
        public static string ZakatReturnDetailsSuccessfullPageView = "ZakatReturnDetailsSuccessfullPageView";
        public static string ZakatReturnNewSuccessPageView = "ZakatReturnNewSuccessPageView";
        public static string NewTaxEvasionFormPageView = "NewTaxEvasionFormPageView";
        public static string AttachmentPopUp = "AttachmentPopUp";
        public static string ZakatObjectionSuccessfullPageView = "ZakatObjectionSuccessfullPageView";
        public static string RefundAccountPopupPageView = "RefundAccountPopupPageView";
        public static string NewAccountPopPageView = "NewAccountPopPageView";
        public static string VATCreditCarriedForwardPopUpPageView = "VATCreditCarriedForwardPopUpPageView";

        // * Taxpayer Profile
        public static string TaxpayerProfilePageView = "TaxpayerProfilePageView";
        public static string UpdateMobilePopUp = "UpdateMobilePopUp";
        public static string UpdateEmailPopUp = "UpdateEmailPopUp";
        public static string UpdatePasswordPopUp = "UpdatePasswordPopUp";
        public static string VerificationPageView = "VerificationPageView";
        public static string TaxpayerProfileSuccessPage = "TaxpayerProfileSuccessPage";
        public static string EstablishmentSignUPPageView = "EstablishmentSignUPPageView";
        public static string SignUpForEstablishmentPageView = "SignUpForEstablishmentPageView";
        public static string SupportPageView = "SupportPageView";
        public static string ZatcaInfoMenuPageView = "ZatcaInfoMenuPageView";

        public static string NotesDescriptionPopUpPageView = "NotesDescriptionPopUpPageView";
        public static string NotesPopUpPageView = "NotesPopUpPageView";
        public static string TaxManagementPageView = "TaxManagementPageView";
        // * End
        // * Bank Managment
        public static string GAZTBankAccountManagementPageView = "BankAccountManagementPageView";
        public static string GAZTBankAccountAddOrUpdatePageView = "BankAccountAddorUpdateIBANPageView";
        // * End
        #endregion

        #region new design views Release2

        public static string MyBillsMultiplePayableList = "MyBillsMultiplePayableList";
        public static string InstalmentPlanPageView = "InstalmentPlanPageView";
        public static string VatInstalmentPlanSuccessPage = "VatInstalmentPlanSuccessPage";
        public static string ZakatInstalmentPlanPageView = "ZakatInstalmentPlanPageView";
        public static string VatInstalmentPlanPageView = "VatInstalmentPlanPageView";
        public static string VatInstalmentPlanListPageView = "VatInstalmentPlanListPageView";
        public static string ZakatInstalmentPlanListPageView = "ZakatInstalmentPlanListPageView";
        public static string TaxEvasionMyReportsListPageView = "TaxEvasionMyReportsListPageView";
        public static string TaxEvasionReportDetailPageView = "TaxEvasionReportDetailPageView";
        public static string ZakatAcknowledgmentPageView = "ZakatAcknowledgmentPageView";
        public static string ChangeFillingPeriodPageView = "ChangeFillingPeriodPageView";
        public static string ContractReleasePageView = "ContractReleasePageView";
        public static string ContractReleaseSuccessPageView = "ContractReleaseSuccessPageView";
        public static string ChangeFillingPeriodListPageView = "ChangeFillingPeriodListPageView";
        public static string ChangeFillingPeriodSuccessPage = "ChangeFillingPeriodSuccessPage";
        public static string ContractReleaseListPageView = "ContractReleaseListPageView";
        public static string ZakatInstalmentPlanSuccessPage = "ZakatInstalmentPlanSuccessPage";
        public static string VatReviewPageView = "VatReviewPageView";
        public static string VatReviewListPageView = "VatReviewListPageView";
        public static string VatReviewSuccessPageView = "VatReviewSuccessPageView";
        public static string VatReviewViewApplicationPageView = "VatReviewViewApplicationPageView";
        public static string ObjectionsSelectionPageView = "ObjectionsSelectionPageView";
        public static string ZakatObjectionsListPageView = "ZakatObjectionsListPageView";
        public static string ZakatObjectionPageView = "ZakatObjectionPageView";
        public static string ZakatObjectionListPageView = "ZakatObjectionListPageView";
        public static string ZakatObjectionSuccessPageView = "ZakatObjectionSuccessPageView";
        public static string VATDeclarationAttachmentPageView = "VATDeclarationAttachmentPageView";
        public static string VATRegistrationDisplayDetails = "VATRegistrationDisplayDetails";
        public static string MoreMenuPopUpPageViewRTwo = "MoreMenuPopUpPageViewRTwo";
        public static string VRSuspensionViewAppPageView = "VRSuspensionViewAppPageView";
        public static string VRVatRegViewPageView = "VRVatRegViewPageView";
        public static string VRVatGroupPageView = "VRVatGroupPageView";
        public static string VATServicesPageView = "VATServicesPageView";
        public static string OldZakatInstalmentPlanPageView = "OldZakatInstalmentPlanPageView";
        public static string OldZakatInstalmentPlanListPageView = "OldZakatInstalmentPlanListPageView";
        public static string OldZakatInstalmentPlanSuccessPage = "OldZakatInstalmentPlanSuccessPage";
        public static string AddNotesPopupPageView = "AddNotesPopupPageView";

        #endregion

        #region Payment Implementation
        public static string PaymentProcessWebview = "PaymentProcessWebview";

        #endregion

        #region old view strings
        public static string SFLandingPageView = "SFLandingPageView";
        public static string SFOptionsPageView = "SFOptionsPageView";
        public static string SFLoginPageView = "SFLoginPageView";
        public static string SFAnonymousLandingPageView = "SFAnonymousLandingPageView";
        public static string StyleTestUIPageView = "StyleTestUIPageView";
        public static string MyCertificate = "MyCertificate";
        public static string PdfView = "PdfView";
        public static string UpdateEmailAddress = "UpdateEmailAddress";
        public static string ForgotUsernamePassword = "ForgotUsernamePassword";
        public static string MyBillsView = "MyBillsView";
        public static string TaxPayerProfilePageView = "TaxPayerProfilePageView";
        public static string ChangeMobileNumberPageView = "ChangeMobileNumberPageView";
        public static string ChangeEmailPageView = "ChangeEmailPageView";
        public static string UpdateEmailVerificationPage = "UpdateEmailVerificationPage";
        public static string ChangePasswordPageView = "ChangePasswordPageView";
        public static string OTPPageView = "OTPPageView";
        public static string ForgotUsernamePasswordPageView = "ForgotUsernamePasswordPageView";
        public static string VATLookupPageView = "VATLookupPageView";
        public static string ZakatReturnListPageView = "ZakatReturnListPageView";
        public static string ZakatReturnDetailsPageView = "ZakatReturnDetailsPageView";
        public static string BillDetailsPageView = "BillDetailsPageView";
        public static string SalesDetailsPageView = "SalesDetailsPageView";
        public static string AmendSalesDetailsPageView = "AmendSalesDetailsPageView";
        public static string ICRListPageView = "ICRListPageView";
        public static string CheckTINStatusPageView = "CheckTINStatusPageView";
        public static string VATReturnsPageView = "VATReturnsPageView";
        public static string VATReturnsPageViewEX = "VATReturnsPageViewEX";
        public static string AAcknowledgementView = "AAcknowledgementView";
        public static string AcknowledgementDetailsPageView = "AcknowledgementDetailsPageView";
        public static string DisplayNotesPageView = "DisplayNotesPageView";
        public static string AttachmentPageView = "AttachmentPageView";
        public static string AddNotePageView = "AddNotePageView";
        public static string AddPopPageView = "AddPopPageView";
        public static string CreditCarriedPageView = "CreditCarriedPageView";
        public static string CorrespondancePageView = "CorrespondancePageView";
        public static string CorrespondenceDetailsPageView = "CorrespondenceDetailsPageView";
        public static string FormBundleStatusPageView = "FormBundleStatusPageView";
        public static string SignUpTAndCViewPage = "SignUpTAndCViewPage";
        public static string SignUpFormPageView = "SignUpFormPageView";
        public static string CreateGaztAccountPageView = "CreateGaztAccountPageView";
        public static string TaxEvasionRegistrationPageView = "TaxEvasionRegistrationPageView";
        public static string TaxEvasionReportTypePageView = "TaxEvasionReportTypePageView";
        public static string TaxEvasionReportMobilePageView = "TaxEvasionReportMobilePageView";
        public static string TaxEvasionReportFormPageView = "TaxEvasionReportFormPageView";
        public static string TaxEvasionAttachmentPageView = "TaxEvasionAttachmentPageView";
        public static string TaxEvasionFormPage = "TaxEvasionFormPage";
        public static string AccountCreatedPageView = "AccountCreatedPageView";
        public static string AccountCreatedSuccessfullyPageView = "AccountCreatedSuccessfullyPageView";
        public static string TaxEvasionReportListPageView = "TaxEvasionReportListPageView";
        public static string ReturnsPageView = "ReturnsPageView";
        public static string FAQPageView = "FAQPageView";
        public static string AboutUsPageView = "AboutUsPageView";
        public static string PrivacyAndPolicyPageView = "PrivacyAndPolicyPageView";
        public static string MyReturnsPageView = "MyReturnsPageView";
        public static string MyCommitmentsPageView = "MyCommitmentsPageView";
        public static string ContactUsPageView = "ContactUsPageView";
        public static string VATIndividualSignupPageView = "VATIndividualSignupPageView";
        public static string IndividualRegistrationPageView = "IndividualRegistrationPageView";
        public static string RegistrationSuccessfulPageView = "RegistrationSuccessfulPageView";
        public static string VATRealEstateServicesPageView = "VATRealEstateServicesPageView";
        public static string PropertyRegistrationPage = "PropertyRegistrationPage";
        public static string VATRegistrationPageView = "VATRegistrationPageView";
        public static string VATAmendReactivationPageView = "VATAmendReactivationPageView";
        public static string VATRegistrationSuccessfullPageView = "VATRegistrationSuccessfullPageView";
        public static string VATAmendReactivationSuccessfulPageView = nameof(VATAmendReactivationSuccessfulPageView);
        public static string VATIndividualSignupTnCPageView = "VATIndividualSignupTnCPageView";
        public static string FinancialDetailAttachmentPopupPageView = "FinancialDetailAttachmentPopupPageView";

        public static string InternationalMobileNumberCodePages = "InternationalMobileNumberCodePages";
        public static string InternationalCodeSearchPage = "InternationalCodeSearchPage";

        public static string FileAttachmentPopUpPageView = "FileAttachmentPopUpPageView";
        public static string NewAccountPopUpPageView = "NewAccountPopUpPageView";

        public static string UnlockAccountTINPageView = "UnlockAccountTINPageView";
        public static string UnlockAccountChangePasswordPageView = "UnlockAccountChangePasswordPageView";
        public static string UnlockAccountSuccessPageView = "UnlockAccountSuccessPageView";
        public static string TINDeregestrationSuccessPageView = "TINDeregestrationSuccessPageView";

        public static string VATRefundsSuccessPageView = "VATRefundsSuccessPageView";
        public static string VATRefundsListPageView = "VATRefundsListPageView";
        public static string VATRefundDetailsPageView = "VATRefundDetailsPageView";
        public static string VATRefundsNewRequestPageView = "VATRefundsNewRequestPageView";
        public static string GAZTNewDesignShowVatInformationPopUpPageView = "GAZTNewDesignShowVatInformationPopUpPageView";
        public static string VRInputTDViewAppPageViewApp = "VRInputTDViewAppPageViewApp";
        public static string VRVatDeRegViewAppPageView = "VRVatDeRegViewAppPageView";


        public static string VATRefundsInstructionsPageView = "VATRefundsInstructionsPageView";
        public static string MorePopUpPageView = "MorePopUpPageView";
        public static string ShowVatInformationConfirmationPageView = "ShowVatInformationConfirmationPageView";
        public static string InfoPopUpPage = "InfoPopUpPage";
        public static string QuickActionPopUpPageView = "QuickActionPopUpPageView";

        public static string AccountStatementsPageView = "AccountStatementsPageView";
        public static string AccountStatementBillsPageView = "AccountStatementBillsPageView";
        public static string AccountStatementsFiltersPageView = "AccountStatementsFiltersPageView";
        public static string AccountStatementsNewFilterPageView = "AccountStatementsNewFilterPageView";
        public static string AccountStatementsDownloadPageView = "AccountStatementsDownloadPageView";

        public static string NewYesorNoPageView = "NewYesorNoPageView";//cr6264

        //CR6003
        public static string ChangeMobileRequestPageView = "ChangeMobileRequestPageView";
        public static string ChangeMobNafathLoginPage = "ChangeMobNafathLoginPage";
        public static string UpdateManagerDetailsPopUp = "UpdateManagerDetailsPopUp";


        //cr6094
        public static string NafathPopUpPage = "NafathPopUpPage";
        public static string NafathLoginPageView = "NafathLoginPageView";

        //cr6003
        public static string GUIDFrChangeMob { get; set; }

        //AccountStatementsFiltersPageViewModel
        //AccountStatementsPageView

        //VATRefundsListPageView
        #endregion

        #region CustomsView
        public static string InquiryAboutCustomsDeclarationView = "InquiryAboutCustomsDeclarationView";
        public static string TraifSectionsView = "TraifSections";
        public static string LaboratoryPaymentOfInsuranceFees = "LaboratoryPaymentOfInsuranceFees";
        #endregion
        public static PageExecutionType VATType { get; set; }
        public static PageExecutionType ZAKATType { get; set; }
        public static string fontFamilyBold = null;
        public static string fontFamilyMedium = null;
        public static string fontFamilyLight = null;
        public static string fontFamilyRoman = null;
        public static TIN CurrentDropdownTIN;
        public static bool IsJailBrokenDevice = false;
        public static string CalType = "G";
        public static string ACCalType = "G";
        public static bool ZakatReturnBilldetails = false;

        // public static bool IsArabic = false;
        public static bool PreviousIsArabic = true;//true
        public static bool IsArabic = false;//true
        public static bool IsOTPiew = false;
        public static string ICRStatus = String.Empty;
        public static string EUser = String.Empty;
        public static string Fbguid = String.Empty;
        public static string VATDeclrationFbguid = String.Empty;
        public static TaxPayerProfile TP = null;
        public static string Token = String.Empty;
        public static string Otp = String.Empty;
        public static bool IsSessionExpired = false;
        public static bool IsLogOut = false;
        public static bool HasToRefreshLoaderOnDashboard = true;
        public static string AppVersion { get; set; }
        public static double NavigationBarHeightt = 0;
        public static CultureInfo ci;
        public static App appObj;
        public static DateTime TimeAtSleep { get; set; }
        public static DateTime TimeAtResume { get; set; }
        public static double TimeDifference { get; set; }
        public static int CurrentTimeDifference { get; set; }
        public static bool IsComingFromSleepMode { get; set; } = false;
        public static bool IsComingFromDashboardToLogOff = false;
        public static bool IsZakatLoadingFromMyReturns = false;
        public static bool IsLoginCalled = false;
        public static bool IsSamlApiCalledAndroid = false;
        public static bool ArePreLoginLangCookiesSet = false;
        public static List<CookieModel> LoginCookiesRetrieved { get; set; }
        public static LoginModel LoginDataRetrieved { get; set; }
        public static bool IsSAMLLoginEnabled = true;
        public static bool IsUserLoggedIn = false;
        //HttpClientHandlerForSSL Certificate Issue
        public static string IncomingChannel = string.Empty;
        public static bool DoesLoginNeedToBeRefreshed;
        public static string PaymentGuid = string.Empty;
        public static bool isFromDashboard = false;
        public static string selectedForm12Fbguid = string.Empty;
        public static bool isMybillsRefresh = false;
        //Cr6264

        public static bool IsVAtProfitForGoods { get; set; }
        //in Seconds
        public static int IdleTimeToLogout = 100;
        //Bank Managment
        public static string SelectedIBAN = string.Empty;
        public static string SelectedFbGuid = string.Empty;
        public static string IBanValidatedResponse = string.Empty;

        public static bool IsLoginPageRefreshed;
        private INavigationService _navigationService;
        private IDialogService _dialogService;

        #region Tax Evasion
        public static string TaxEvasionToken = string.Empty;
        public static TaxEvasionUserRegistrationResponseData TaxEvasionUserData;
        #endregion

        #region Zakat Instalment
        public static string selectedZakatItem = "";
        public static string selectedVATItem = "";
        public static string selectedVatFillingItem = "";
        public static string selectedVATItemFbust = "";
        public static double idleTimeSpan = 0;
        public static bool IsAppRunningInBackground = false;

        public static bool isAndroidRefresh = false;//#CR2068

        #endregion

        public string acntStatementsSelectedTaxTypeFilterId = string.Empty;
        public string acntStatementsStatementFilterId = string.Empty;

        public static ActivityIndicatorPageView ActivityIndicatorView;
        public static HttpClientHandler httpClientHandler = null;

        public App()
        {
            try
            {


                IsAppRunningInBackground = false;
                Preferences.Default.Set("timeOut", DateTime.Now);
                PageSettings.CheckTarget_Environment("STG");
                PageSettings.GetBaseURL("STG");

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXhccnRTRGJfVkd3X0M="); //MAUI 26.*

                AppResources.Culture = CultureInfo.CurrentUICulture;
                bool hasLanguageKey = Preferences.ContainsKey("Preferences_DefaultLanguage");

                if (hasLanguageKey)
                {
                    var LanguageKey = Preferences.Get("Preferences_DefaultLanguage", "");
                    {
                        if (LanguageKey != null)
                        {
                            if (LanguageKey.Equals("Ar"))
                            {
                                PreviousIsArabic = true;
                            }
                            if (LanguageKey.Equals("En"))
                            {
                                PreviousIsArabic = false;
                            }
                        }

                    }
                }
                else
                {
                    PreviousIsArabic = true;
                }

                if (PreviousIsArabic)
                {
                    String langName = "ar-AE";//"en-US";// "ar-AE";
                    ci = new CultureInfo(langName);
                    AppResources.Culture = ci;
                }

                InitializeComponent();
                App.appObj = this;
                onFontFamilyChanged();
                if (PreviousIsArabic)
                {
                    IsArabic = true;
                }
                else
                {
                    IsArabic = false;
                }
                CreateClientHandler();
                ResetAndContinueSession();

                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        {
                            IncomingChannel = "241";
                        }
                        break;
                    case Device.iOS:
                        {
                            IncomingChannel = "242";
                        }
                        break;
                }

                ActivityIndicatorView = new ActivityIndicatorPageView();

                CustomNavigation navigationPage;
                bool hasKey = Preferences.ContainsKey("first_TimeLoging_key");
                //NEw
                if (!hasKey)
                {
                    navigationPage = new CustomNavigation(new GAZTNewDesignOnBoardingAnimationPageView()) { BarTextColor = Colors.White };

                   // navigationPage = new CustomNavigation(new SFLoginPageView(App.GAZTNewDesignDashBoardPageView)) { BarTextColor = Colors.White };
                }
                else
                {
                    navigationPage = new CustomNavigation(new SFLoginPageView(App.GAZTNewDesignDashBoardPageView)) { BarTextColor = Colors.White };


                }
                var navigationService = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
                navigationService.Initialize(navigationPage);
                _navigationService = navigationService;
                var dialogService = (DialogService)ServiceLocator.Current.GetInstance<IDialogService>();
                dialogService.Initialize(navigationPage);
                _dialogService = dialogService;
                MainPage = navigationPage;

                MessagingCenter.Subscribe<object, string>(this, "LogoutUserFromApp", (sender, arg) =>
                {
                    if (App.DoesLoginNeedToBeRefreshed == true)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            try
                            {
                                Preferences.Default.Set("IsSessionExpired", true);
                                App.DoesLoginNeedToBeRefreshed = false;
                                navigationPage = new CustomNavigation(new SFLoginPageView(App.GAZTNewDesignDashBoardPageView)) { BarTextColor = Colors.White };
                                var navigationService1 = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
                                navigationService1.Initialize(navigationPage);
                                _navigationService = navigationService1;
                                var dialogService1 = (DialogService)ServiceLocator.Current.GetInstance<IDialogService>();
                                dialogService1.Initialize(navigationPage);
                                _dialogService = dialogService1;

                                MainPage = navigationPage;
                                _ = Task.Run(() => WebServiceManager.GAZTLogOff());

                            }
                            catch (Exception)
                            {


                            }
                        });
                    }
                });

                //InitializeAppDynamics();
            }
            catch (Exception ex)
            {

            }
        }

        public static void CreateClientHandler()
        {
            httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            httpClientHandler.CookieContainer = new System.Net.CookieContainer();
        }

        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return _locator ?? (_locator = new ViewModelLocator());
            }
        }

        private static App _Instance;
        public static App GetInstance()
        {
            if (_Instance == null)
                _Instance = new App();
            return _Instance;
        }

        public static void changeFontFamily(App app)
        {
            PreviousIsArabic = App.IsArabic;
            app.onFontFamilyChanged();
            App.IsArabic = PreviousIsArabic;
            try
            {
                if (App.IsArabic)
                {
                    Preferences.Set("Preferences_DefaultLanguage", "Ar");
                }
                else
                {
                    Preferences.Set("Preferences_DefaultLanguage", "En");
                }
            }
            catch (Exception)
            {
                Preferences.Set("Preferences_DefaultLanguage", "Ar");
            }

        }

        public void onFontFamilyChanged()
        {
            try
            {


                if (PreviousIsArabic)
                {
                    String langName = "ar-AE";//"en-US";// "ar-AE";
                    ci = new CultureInfo(langName);
                    AppResources.Culture = ci;
                }
                else
                {
                    String langName = "en-US";//"en-US";// "ar-AE";
                    ci = new CultureInfo(langName);
                    AppResources.Culture = ci;
                }
                if (PreviousIsArabic)
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            fontFamilyBold = "Somar-Bold";
                            fontFamilyMedium = "Somar-SemiBold";
                            fontFamilyLight = "Somar-Light";
                            fontFamilyRoman = "Somar-Regular";
                            break;
                        case Device.Android:
                            fontFamilyBold = "Somar-Bold.otf#Somar-Bold";
                            fontFamilyMedium = "Somar-SemiBold.otf#Somar-SemiBold";//GE_SS_Two_Medium
                            fontFamilyLight = "Somar-Light.otf#Somar-Light";
                            fontFamilyRoman = "Somar-Regular.otf#Somar-Regular";
                            break;
                    }
                }
                else
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            fontFamilyBold = "Somar-Bold";
                            fontFamilyMedium = "Somar-SemiBold";
                            fontFamilyLight = "Somar-Light";
                            fontFamilyRoman = "Somar-Regular";
                            break;
                        case Device.Android:
                            fontFamilyBold = "Somar-Bold.otf#Somar-Bold";
                            fontFamilyMedium = "Somar-SemiBold.otf#SomarSemiBold";
                            fontFamilyLight = "Somar-Light.otf#Somar-Light";
                            fontFamilyRoman = "Somar-Regular.otf#Somar-Regular";
                            break;
                    }
                }

                GAZTTextBoxStyleForEntry.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = "Somar-Bold" });
                GAZTSmallGreenLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
                MiniGoldLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                ForgotPasswordTextColor.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                InformationRedColorLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                MandatoryRedColorLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                InformationGrayColorLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                SmallWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                SmallMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                MyBillsSmallMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                MyBillsMediumMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                MiniGrayLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
                MiniBlackLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                PickerStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTSmallGoldLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTGreenLabelStyleForDashboardIcon.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
                GAZTCaptionGreenLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
                GAZTVerifyButton.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
                GAZTGreenLabelStyleForEservicesIcon.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
                GAZTGreenLabelStyleForMicro.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTGoldLabelStyleForSmallFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTGoldLabelStyleForCaptionFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTGrayLabelStyleForSmallFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTGrayLabelStyleForCaptionFont.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTDropdownStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                GAZTSmallGreenLabelStyleForSteps.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                GAZTGreyLabelStyleForOptionMenu.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                TabbedPageSmallMiniWhiteLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                TabbedPageMediumMiniGoldLabelStyle.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyMedium });
                forBoldLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyBold });
                forLightLabel.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyLight });
                MicroGrayLabelStyleNew.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyRoman });
                MicroGrayLabelStyleNewEn.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = fontFamilyRoman });
                //SYNCFUSION INTEGRATION
                if (App.IsArabic)
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            Current.Resources.TryGetValue("GAZT_Arabic_FONT_BOLD", out var GAZT_Arabic_FONT_BOLD);
                            var afb = (OnPlatform<string>)GAZT_Arabic_FONT_BOLD;
                            Current.Resources["GAZT_FONT_BOLD"] = afb.Platforms[0].Value;

                            Current.Resources.TryGetValue("GAZT_Arabic_FONT_MEDIUM", out var GAZT_Arabic_FONT_MEDIUM);
                            var afm = (OnPlatform<string>)GAZT_Arabic_FONT_BOLD;
                            Current.Resources["GAZT_FONT_MEDIUM"] = afm.Platforms[0].Value;

                            Current.Resources.TryGetValue("GAZT_Arabic_FONT_REGULAR", out var GAZT_Arabic_FONT_REGULAR);
                            var afr = (OnPlatform<string>)GAZT_Arabic_FONT_REGULAR;
                            Current.Resources["GAZT_FONT_REGULAR"] = afr.Platforms[0].Value;

                            break;

                        case Device.iOS:
                            Current.Resources.TryGetValue("GAZT_Arabic_FONT_BOLD", out var GAZT_Arabic_FONTBOLD);
                            var iafb = (OnPlatform<string>)GAZT_Arabic_FONTBOLD;
                            Current.Resources["GAZT_FONT_BOLD"] = iafb.Platforms[1].Value;

                            Current.Resources.TryGetValue("GAZT_Arabic_FONT_MEDIUM", out var GAZT_Arabic_FONTMEDIUM);
                            var iafm = (OnPlatform<string>)GAZT_Arabic_FONTMEDIUM;
                            Current.Resources["GAZT_FONT_MEDIUM"] = iafm.Platforms[1].Value;

                            Current.Resources.TryGetValue("GAZT_Arabic_FONT_REGULAR", out var GAZT_Arabic_FONTREGULAR);
                            var iafr = (OnPlatform<string>)GAZT_Arabic_FONTREGULAR;
                            Current.Resources["GAZT_FONT_REGULAR"] = iafr.Platforms[1].Value;
                            break;
                    }
                }
                else
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:

                            Current.Resources.TryGetValue("GAZT_English_FONT_REGULAR", out var GAZT_English_FONT_REGULAR_test);
                            var afrt = (OnPlatform<string>)GAZT_English_FONT_REGULAR_test;
                            Current.Resources["LargeLabelTest"] = afrt.Platforms[0].Value;

                            Current.Resources.TryGetValue("GAZT_English_FONT_BOLD", out var GAZT_English_FONT_BOLD);
                            var afb = (OnPlatform<string>)GAZT_English_FONT_BOLD;
                            Current.Resources["GAZT_FONT_BOLD"] = afb.Platforms[0].Value;

                            Current.Resources.TryGetValue("GAZT_English_FONT_MEDIUM", out var GAZT_English_FONT_MEDIUM);
                            var afm = (OnPlatform<string>)GAZT_English_FONT_MEDIUM;
                            Current.Resources["GAZT_FONT_MEDIUM"] = afm.Platforms[0].Value;

                            Current.Resources.TryGetValue("GAZT_English_FONT_REGULAR", out var GAZT_English_FONT_REGULAR);
                            var afr = (OnPlatform<string>)GAZT_English_FONT_REGULAR;
                            Current.Resources["GAZT_FONT_REGULAR"] = afr.Platforms[0].Value;

                            break;

                        case Device.iOS:
                            Current.Resources.TryGetValue("GAZT_English_FONT_REGULAR", out var GAZT_English_FONT_REGULARtest);
                            var iafrt = (OnPlatform<string>)GAZT_English_FONT_REGULARtest;
                            Current.Resources["LargeLabelTest"] = iafrt.Platforms[1].Value;

                            Current.Resources.TryGetValue("GAZT_English_FONT_BOLD", out var GAZT_English_FONTBOLD);
                            var iafb = (OnPlatform<string>)GAZT_English_FONTBOLD;
                            Current.Resources["GAZT_FONT_BOLD"] = iafb.Platforms[1].Value;

                            Current.Resources.TryGetValue("GAZT_English_FONT_MEDIUM", out var GAZT_English_FONTMEDIUM);
                            var iafm = (OnPlatform<string>)GAZT_English_FONTMEDIUM;
                            Current.Resources["GAZT_FONT_MEDIUM"] = iafm.Platforms[1].Value;

                            Current.Resources.TryGetValue("GAZT_English_FONT_REGULAR", out var GAZT_English_FONTREGULAR);
                            var iafr = (OnPlatform<string>)GAZT_English_FONTREGULAR;
                            Current.Resources["GAZT_FONT_REGULAR"] = iafr.Platforms[1].Value;

                            break;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStart()
        {
            IsJailBrokenDevice = false;
            try
            {
                IsJailBrokenDevice = DependencyService.Get<ZATCAMAUI.Core.Interfaces.IDeviceInfo>().IsJailBreakDetected();
            }
            catch (Exception)
            {

            }

        }

        public static Task ResetAndContinueSession()
        {
            Device.StartTimer(new TimeSpan(0, 0, 2), () =>
            {
                // Logic for logging out if the device is inactive for a period of time.
                int timeSpan = defaultTimespan;
                if (IsLoginPageVisible() == true)
                {
                    timeSpan = defaultTimespanForLogin;
                }

                var idleTime = Preferences.Default.Get("timeOut", DateTime.Now);
                idleTimeSpan = DateTime.Now.Subtract(idleTime).TotalMinutes;

                if (idleTimeSpan >= timeSpan)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Preferences.Default.Set("timeOut", DateTime.Now);
                        HandleSessionTimeout();
                    });

                    return false;
                }

                return true;
                // Always return true as to keep our device timer running.
            });
            return null;
        }

        protected override void OnSleep()
        {
            TimeAtSleep = DateTime.Now;
        }

        protected override void OnResume()
        {
            TimeAtResume = DateTime.Now;
            TimeDifference = (TimeAtResume - TimeAtSleep).TotalSeconds;
            IsComingFromSleepMode = true;

        }

        public static void InitializeAppDynamics()
        {
            var config = AgentConfiguration.Create("EUM-AAB-AUM");
            config.LoggingLevel = LoggingLevel.Debug;

            //Instrumentation.enableAggregateExceptionReporting = true;

            config.EnableAggregateExceptionReporting = true;
            config.CollectorURL = "https://eum.gazt.gov.sa";
            Instrumentation.InitWithConfiguration(config);
        }

        public static void DisplayProgressView()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
               {
                   MopupService.Instance.PushAsync(ActivityIndicatorView, true);
               });
            }
            catch (Exception)
            {



            }
        }

        public static bool IsLoginPageVisible()
        {
            var _navigation = Application.Current.MainPage.Navigation;
            Page topPage = _navigation.NavigationStack.ToList().LastOrDefault();

            if (topPage.GetType().Name == App.SFLoginPageView)
            {
                return true;
            }

            return false;
        }

        public static bool IsOnboardingPageVisible()
        {
            var _navigation = Application.Current.MainPage.Navigation;
            Page topPage = _navigation.NavigationStack.ToList().LastOrDefault();

            if (topPage.GetType().Name == App.GAZTNewDesignOnBoardingAnimationPageView)
            {
                return true;
            }

            return false;
        }

        public static bool isTimerOn = false;

        public static void StartTimer(int h, int m, int sec)
        {
            int hour = h;
            int mins = m;
            int counter = sec;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    counter = counter - 1;
                    if (counter < 0)
                    {
                        counter = 59;
                        mins = mins - 1;
                        if (mins < 0)
                        {
                            mins = 59;
                            hour = hour - 1;
                            if (hour < 0)
                            {
                                hour = 0;
                                mins = 0;
                                counter = 0;
                            }
                        }
                    }
                });
                if (hour == 0 && mins == 0 && counter == 0)
                {
                    isTimerOn = false;
                    App.HasToRefreshLoaderOnDashboard = true;
                    MessagingCenter.Send<GAZTNewDesignDashBoardPageView, string>(new GAZTNewDesignDashBoardPageView(), "StartTimerForDashboard", "StartTimerForDashboard");
                    return false;
                }
                else
                {
                    isTimerOn = true;
                    return true;
                }
            });
        }

        public static bool ShouldStopTimer = false;

        public static void StartTimerForBackground(int h, int m, int sec)
        {
            int hour = h;
            int mins = m;
            int counter = sec;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    counter = counter - 1;
                    if (counter < 0)
                    {
                        counter = 59;
                        mins = mins - 1;
                        if (mins < 0)
                        {
                            mins = 59;
                            hour = hour - 1;
                            if (hour < 0)
                            {
                                hour = 0;
                                mins = 0;
                                counter = 0;
                            }
                        }
                    }


                    // LblCountDownTimer = string.Format("{0:00}:{1:00}", mins, counter);
                });

                if (ShouldStopTimer == true)
                {
                    return false;
                }

                if (hour == 0 && mins == 0 && counter == 0)
                {
                    App.DoesLoginNeedToBeRefreshed = true;
                    MessagingCenter.Send<Object, string>(Application.Current, "LogoutUserFromApp", "LogoutUserFromApp");
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }

        public static bool ShouldStopLoginRefreshTimer = false;

        public static void StartTimerForLoginRefresh(int h, int m, int sec)
        {
            int hour = h;
            int mins = m;
            int counter = sec;

            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    counter = counter - 1;
                    if (counter < 0)
                    {
                        counter = 59;
                        mins = mins - 1;
                        if (mins < 0)
                        {
                            mins = 59;
                            hour = hour - 1;
                            if (hour < 0)
                            {
                                hour = 0;
                                mins = 0;
                                counter = 0;
                            }
                        }
                    }


                });

                if (ShouldStopLoginRefreshTimer == true)
                {
                    return false;
                }


                if (hour == 0 && mins == 0 && counter == 0)
                {
                    App.IsLoginPageRefreshed = true;
                    MessagingCenter.Send<Object, string>(Application.Current, "RefreshLoginPage", "RefreshLoginPage");
                    mins = m;
                    return true;
                }
                else
                {
                    return true;
                }
            });
        }

        public static void HideProgressView()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (MopupService.Instance.PopupStack.Count > 0)
                    MopupService.Instance.PopAsync(true);
            });
        }

        //rohith-login
        public static void HandleSessionTimeout()
        {
            if (App.IsOnboardingPageVisible() == false)
            {
                if (App.IsLoginPageVisible() == true)
                {
                    App.IsLoginPageRefreshed = true;
                    Preferences.Set("SessionAction", "RefreshLoginPage");
                    MessagingCenter.Send<Object, string>(Application.Current, "RefreshLoginPage", "RefreshLoginPage");
                    App.ResetAndContinueSession();
                }
                else if (IsUserLoggedIn)
                {
                    Preferences.Set("SessionAction", "LogoutUserFromApp");
                    App.DoesLoginNeedToBeRefreshed = true;
                    MessagingCenter.Send<Object, string>(Application.Current, "LogoutUserFromApp", "LogoutUserFromApp");
                }
            }
        }

        public static void OnBackPressed()
        {
            MessagingCenter.Send<Application>(Application.Current, "BackButtonPressed");
        }

        private static void DisplayCrashReport()
        {
            const string errorFilename = "Fatal.log";
            var libraryPath = Environment.GetFolderPath(Device.RuntimePlatform == Device.iOS ? Environment.SpecialFolder.Resources : Environment.SpecialFolder.Personal);
            var errorFilePath = Path.Combine(libraryPath, errorFilename);

            if (!File.Exists(errorFilePath))
            {
                return;
            }

            var errorText = File.ReadAllText(errorFilePath);
            if (string.IsNullOrEmpty(errorText))
                return;

            Instrumentation.ReportError(JsonConvert.DeserializeObject<Exception>(errorText), ErrorSeverityLevel.CRITICAL);

            File.WriteAllText(errorFilePath, "");

        }

    }
}
