using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Controls;
using ZATCAMAUI.Core.Services.Classes;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.GenericPickers;
using ZATCAMAUI.ViewModel.NewDesignViewModel.HomeViewModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.IBanAccManagementsViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.InstalmentPlanViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions;
using ZATCAMAUI.ViewModel.NewDesignViewModel.LiveVideoVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.MyReportsVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Nafat;
using ZATCAMAUI.ViewModel.NewDesignViewModel.PaymnetOptions;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ReportOTPVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SubmitReport;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SupportPageVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.SurveyViewModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TahqaqViewModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Template;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TrackShipment;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VAT;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATAmendReactivationPageViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATAmendReactivationSuccessPageViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATgoodsOnprofit;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatyViewModels;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AboutUsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AccountCreatedPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AcknowledgementDetailsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AddNotePage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AddPopPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AmendSalesDetailsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AttachmentPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.CreateGaztAccountPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.CreditCarriedPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.DisplayNotesPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.FAQPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ICRListPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.InternationalMobileNumber;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.LoginPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.MyBillsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.MyReturnsPageViewModel;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PdfViewPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PrivacyAndPolicyPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ReturnsPageViewModels;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.SalesDetailsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.SignUpFormPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.SignUpTAndCPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.StylesTestUi;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionRegistrationPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportListPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportTypePage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATLookupPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage;
using ZATCAMAUI.Views.NewDesign.AccountStatements;
using ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.Common.NativeNafath;
using ZATCAMAUI.Views.NewDesign.ContractReleasePages;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.CustomDashBoard;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.CustomFees;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.eDeclarations;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.InquiryaboutCustomsIssuesViews;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using ZATCAMAUI.Views.NewDesign.DashBoardPages;
using ZATCAMAUI.Views.NewDesign.EDeclaration;
using ZATCAMAUI.Views.NewDesign.EDeclaration.InfoPages;
using ZATCAMAUI.Views.NewDesign.EDeclaration.InquireRequestPages;
using ZATCAMAUI.Views.NewDesign.EDeclaration.QuestionsViews;
using ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages;
using ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages;
using ZATCAMAUI.Views.NewDesign.EstablishmentSignUP;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.ForgotPasswordPages;
using ZATCAMAUI.Views.NewDesign.FormBundleStatusPages;
using ZATCAMAUI.Views.NewDesign.HomePages;
using ZATCAMAUI.Views.NewDesign.IBanAccountsManagementPages;
using ZATCAMAUI.Views.NewDesign.InstalmentPlan;
using ZATCAMAUI.Views.NewDesign.LiveVideo;
using ZATCAMAUI.Views.NewDesign.LoginPages;
using ZATCAMAUI.Views.NewDesign.LoginPages.FasahLogin;
using ZATCAMAUI.Views.NewDesign.MyBillsPages;
using ZATCAMAUI.Views.NewDesign.MyReports;
using ZATCAMAUI.Views.NewDesign.MyReturnsPages;
using ZATCAMAUI.Views.NewDesign.Nafat;
using ZATCAMAUI.Views.NewDesign.OnboardingPages;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;
using ZATCAMAUI.Views.NewDesign.ReportOTP;
using ZATCAMAUI.Views.NewDesign.SubmitReport;
using ZATCAMAUI.Views.NewDesign.SupportPages;
using ZATCAMAUI.Views.NewDesign.Survey;
using ZATCAMAUI.Views.NewDesign.TahqaqViews;
using ZATCAMAUI.Views.NewDesign.TAXEvasionPages;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;
using ZATCAMAUI.Views.NewDesign.TaxpayerProfile;
using ZATCAMAUI.Views.NewDesign.TaxpayersCertificatesPages;
using ZATCAMAUI.Views.NewDesign.Template;
using ZATCAMAUI.Views.NewDesign.TrackShipment;
using ZATCAMAUI.Views.NewDesign.VAT;
using ZATCAMAUI.Views.NewDesign.VATAmendReactivationPages;
using ZATCAMAUI.Views.NewDesign.VATDeclarationPages;
using ZATCAMAUI.Views.NewDesign.VATDeRegistration;
using ZATCAMAUI.Views.NewDesign.VATgoodsOnprofit;
using ZATCAMAUI.Views.NewDesign.VatInstalmentPlan;
using ZATCAMAUI.Views.NewDesign.VATLookUp;
using ZATCAMAUI.Views.NewDesign.VATRefunds;
using ZATCAMAUI.Views.NewDesign.VATRegistrationDetails;
using ZATCAMAUI.Views.NewDesign.VATReview;
using ZATCAMAUI.Views.NewDesign.VATServices;
using ZATCAMAUI.Views.NewDesign.ZakatDeregistration;
using ZATCAMAUI.Views.NewDesign.ZakatForm5;
using ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan;
using ZATCAMAUI.Views.NewDesign.ZakatObjection;
using ZATCAMAUI.Views.NewDesign.ZAKATObjectionPages;
using ZATCAMAUI.Views.NewDesign.Zakaty;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AboutUsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AcknowledgementDetailsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddNotePages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AddPopPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AmendSalesDetailsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.AttachmentPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ContactUsPage;
using ZATCAMAUI.Views.SyncFusionEnabledViews.CreditCarriedPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.DisplayNotesPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.FAQPage;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ICRListPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.InternationalMobileNumber;
using ZATCAMAUI.Views.SyncFusionEnabledViews.LoginPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.PdfViewPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.PrivacyAndPolicyPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.SalesDetailsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.StylesTestUi;
using ZATCAMAUI.Views.SyncFusionEnabledViews.UnlockAccount;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATDeclarationPagesEX;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatReturnDetailsPages;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatReturnListPages;

namespace ZATCAMAUI.Core.Helper
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<ICustomInquiryService, CustomInquiryService>();
            SimpleIoc.Default.Register<ITraiffSectionsServices, TraiffSectionsServices>();
            SimpleIoc.Default.Register<IBalaghServices, BalaghServices>();
            SimpleIoc.Default.Register<IlaboratoryInsuranseFeesServices, laboratoryInsuranseFeesServices>();
            SimpleIoc.Default.Register<ICommonServices, CommonServices>();
            SimpleIoc.Default.Register<ITahqaqServices, TahqaqServices>();
            SimpleIoc.Default.Register<ITaxCalculatorServices, TaxCalculatorServices>();
            SimpleIoc.Default.Register<ISubmitReportServices, SubmitReportServices>();
            SimpleIoc.Default.Register<IMyReportsServices, MyReportsServices>();
            SimpleIoc.Default.Register<ISurveyServices, SurveyServices>();
            SimpleIoc.Default.Register<IUserServices, UserServices>();
            SimpleIoc.Default.Register<ITwareedServices, TwareedServices>();
            SimpleIoc.Default.Register<IE_DeclerationServices, E_DeclerationServices>();
            SimpleIoc.Default.Register<ITrackShipment, TrackShipmentServices>();
            SimpleIoc.Default.Register<INativeNafath, NativeNafathServices>();
            SimpleIoc.Default.Register<EDeclerationSubmitModel>();
            SimpleIoc.Default.Register<BankAccountManagementPageViewModel>();
            SimpleIoc.Default.Register<BankAccountAddorUpdateIBANViewModel>();
            #region NewDesignIOC
            SimpleIoc.Default.Register<GAZTNewDesignRecoverUsernameViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignRecoverPasswordPageViewModel>();
            //SimpleIoc.Default.Register<GAZTNewDesignStyleTestUIPageViewModel>();

            SimpleIoc.Default.Register<GAZTNewDesignOnBoardingAnimationPageViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignVATReturnUpdatedUIPageViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignForgotPasswordPageViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignMyBillsPageViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignDashBoardPageViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignMyReturnsNewPageViewModel>();
            SimpleIoc.Default.Register<TaxpayerCorrespondancePageViewModel>();
            SimpleIoc.Default.Register<TaxpayerCorrespondanceDetailPageViewModel>();
            SimpleIoc.Default.Register<VATAmendReactivationSuccesssulPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionPageWebView>();
            SimpleIoc.Default.Register<StyleTestUIPageViewModel>();
            SimpleIoc.Default.Register<ZakatDeregistrationPageViewModel>();
            SimpleIoc.Default.Register<TINDeregistrationPageViewModel>();
            SimpleIoc.Default.Register<VATLookUpNewPageViewModel>();
            SimpleIoc.Default.Register<VATDeRegistrationDetailsPageViewModel>();
            SimpleIoc.Default.Register<VATDeRegistrationInstructionsPageViewModel>();
            SimpleIoc.Default.Register<CalendarPickerPageViewModel>();
            SimpleIoc.Default.Register<PickerPageViewModel>();
            SimpleIoc.Default.Register<ZakatRegistrationDetailsListPageViewModel>();
            SimpleIoc.Default.Register<GeneralServicesViewModel>();
            SimpleIoc.Default.Register<TINDeregistrationCloseIndividualOutletsPageViewModel>();
            SimpleIoc.Default.Register<ZakatRegistrationOutletsDetailsPageViewModel>();
            SimpleIoc.Default.Register<ZakatRegistrationTaxPayerDetailsPageViewModel>();
            SimpleIoc.Default.Register<ZakatRegistrationFinancialDetailsPageViewModel>();

            //TINDeregistrationCloseIndividualOutletsPageView
            SimpleIoc.Default.Register<VATDeregistrationSuccessPageViewModel>();

            SimpleIoc.Default.Register<ZakatForm5PageViewModel>();

            SimpleIoc.Default.Register<TaxEvasionVerifyMobileViewModel>();

            SimpleIoc.Default.Register<NewZakatObjectionPageViewModel>();
            SimpleIoc.Default.Register<VATReturnSuccessfullPageViewModel>();
            SimpleIoc.Default.Register<VatReturnNewSuccessViewModel>();
            SimpleIoc.Default.Register<EstablishmentRegistrationPageViewModel>();
            SimpleIoc.Default.Register<EstablishmentAmendUpdatePageViewModel>();
            SimpleIoc.Default.Register<OutletDetailsPageViewModel>();
            SimpleIoc.Default.Register<OutletDetailsAmendUpdatePageViewModel>();
            SimpleIoc.Default.Register<ActivityItemPageViewModel>();
            SimpleIoc.Default.Register<ActivityItemAmendUpdatePageViewModel>();
            SimpleIoc.Default.Register<RegistrationSuccessfulViewModel>();
            SimpleIoc.Default.Register<ZakatReturnDetailsSuccessfullPageViewModel>();
            SimpleIoc.Default.Register<ZakatReturnNewSuccessViewModel>();
            SimpleIoc.Default.Register<NewTaxEvasionFormPageViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignShowVatInformationPopUpPageViewModel>();
            SimpleIoc.Default.Register<ZakatObjectionSuccessfullPageViewModel>();
            SimpleIoc.Default.Register<DashboardAnonymousMenuPageViewModel>();
            SimpleIoc.Default.Register<VATCreditCarriedForwardPopUpPageViewModel>();
            SimpleIoc.Default.Register<SupportPageViewModel>();
            SimpleIoc.Default.Register<ZatcaInfoMenuPageViewModel>();

            SimpleIoc.Default.Register<NotesDescriptionPopUpPageViewModel>();
            SimpleIoc.Default.Register<NotesPopUpPageViewModel>();
            SimpleIoc.Default.Register<TaxManagementPageViewModel>();
            SimpleIoc.Default.Register<ViewModel.NewDesignViewModel.VATServicesPageViewModel.VATServicesPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionPageWebViewModel>();
            SimpleIoc.Default.Register<TaxpayerSubsidyViewModel>();
            #endregion

            #region NewDesignRelease2IOC
            SimpleIoc.Default.Register<ZakatInstalmentPlanViewModel>();
            SimpleIoc.Default.Register<ZakatInstalmentPlanListViewModel>();
            SimpleIoc.Default.Register<OldZakatInstalmentPlanViewModel>();
            SimpleIoc.Default.Register<OldZakatInstalmentPlanListViewModel>();
            SimpleIoc.Default.Register<ViewNotePopUpViewModel>();
            SimpleIoc.Default.Register<AddNotePopUpViewModel>();
            SimpleIoc.Default.Register<MyBillsMultiplePayableListViewModel>();

            SimpleIoc.Default.Register<MorePopUpViewModelRTwo>();
            SimpleIoc.Default.Register<InstalmentPlanViewModel>();
            SimpleIoc.Default.Register<VATInstalmentPlanViewModel>();
            SimpleIoc.Default.Register<VATInstalmentPlanListViewModel>();
            SimpleIoc.Default.Register<TaxEvasionMyReportsListPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionReportDetailPageViewModel>();
            SimpleIoc.Default.Register<ZakatAcknowledgmentPageViewModel>();
            SimpleIoc.Default.Register<ChangeFillingPeriodViewModel>();
            SimpleIoc.Default.Register<ContractReleaseViewModel>();
            SimpleIoc.Default.Register<FilesUploadPopUpViewModel>();
            SimpleIoc.Default.Register<InstructionsBottomPopUpViewModel>();
            SimpleIoc.Default.Register<EstablishmentSignUPPageViewModel>();
            SimpleIoc.Default.Register<SignUpForEstablishmentPageViewModel>();
            SimpleIoc.Default.Register<AccountCreatedSuccessfullyPageViewModel>();
            SimpleIoc.Default.Register<ContractReleaseListViewModel>();
            SimpleIoc.Default.Register<ChangeFillingPeriodListViewModel>();
            SimpleIoc.Default.Register<ChangeFillingPeriodSuccessPage>();
            SimpleIoc.Default.Register<VatReviewViewModel>();
            SimpleIoc.Default.Register<VatReviewListViewModel>();
            SimpleIoc.Default.Register<ObjectionViewModel>();
            SimpleIoc.Default.Register<ZakatObjectionsListViewModel>();
            SimpleIoc.Default.Register<ZakatObjectionViewModel>();
            SimpleIoc.Default.Register<QuickActionPopUpPageViewModel>();
            SimpleIoc.Default.Register<VATDeclarationAttachmentPageViewModel>();
            SimpleIoc.Default.Register<VATRegistrationDisplayDetailsPageViewModel>();

            //CR6094
            SimpleIoc.Default.Register<NafathPopupPageViewModel>();
            SimpleIoc.Default.Register<NafathLoginPageViewModel>();

            #endregion

            #region PaymentImplementation
            SimpleIoc.Default.Register<PaymnetProcessWebviewViewModel>();

            #endregion

            #region OldIOC

            SimpleIoc.Default.Register<SFLoginPageViewModel>();
            SimpleIoc.Default.Register<PdfViewModel>();
            SimpleIoc.Default.Register<MyBillsViewModel>();
            SimpleIoc.Default.Register<VATLookupPageViewModel>();
            SimpleIoc.Default.Register<ZakatReturnListPageViewModel>();
            SimpleIoc.Default.Register<ZakatReturnDetailsPageViewModel>();
            SimpleIoc.Default.Register<SalesDetailsPageViewModel>();
            SimpleIoc.Default.Register<AmendSalesDetailsPageViewModel>();
            SimpleIoc.Default.Register<ICRListPageViewModel>();
            SimpleIoc.Default.Register<VATReturnsPageViewModelEX>();
            SimpleIoc.Default.Register<AcknowledgementDetailsPageViewModel>();
            SimpleIoc.Default.Register<DisplayNotesPageViewModel>();
            SimpleIoc.Default.Register<AttachmentPageViewModel>();
            SimpleIoc.Default.Register<AddNotePageViewModel>();
            SimpleIoc.Default.Register<AddPopPageViewModel>();
            SimpleIoc.Default.Register<CreditCarriedPageViewModel>();
            SimpleIoc.Default.Register<FormBundleStatusPageViewModel>();
            SimpleIoc.Default.Register<SignUpTAndCPageViewModel>();
            SimpleIoc.Default.Register<SignUpFormPageViewModel>();
            SimpleIoc.Default.Register<CreateGaztAccountPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionRegistrationViewModel>();
            SimpleIoc.Default.Register<TaxEvasionReportTypePageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionReportFormPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionReportMobilePageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionReportListPageViewModel>();
            SimpleIoc.Default.Register<AccountCreatedPageViewModel>();
            SimpleIoc.Default.Register<ReturnsPageViewModel>();
            SimpleIoc.Default.Register<FAQPageViewModel>();
            SimpleIoc.Default.Register<AboutUsPageViewModel>();
            SimpleIoc.Default.Register<PrivacyAndPolicyPageViewModel>();
            SimpleIoc.Default.Register<MyReturnsPageViewModel>();
            SimpleIoc.Default.Register<ContactUsPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionFormPageViewModel>();
            SimpleIoc.Default.Register<VATIndividualSignupPageViewModel>();
            SimpleIoc.Default.Register<IndividualRegistrationPageViewModel>();
            SimpleIoc.Default.Register<RegistrationSuccessfulPageViewModel>();
            SimpleIoc.Default.Register<EstablishmentAmendUpdateSuccessfulPageViewModel>();
            SimpleIoc.Default.Register<VATRegistrationPageViewModel>();
            SimpleIoc.Default.Register<VATAmendReactivationPageViewModel>();
            SimpleIoc.Default.Register<VATRegistrationSuccessfullPageViewModel>();
            SimpleIoc.Default.Register<InternationalMobileNumberCodePagesViewModel>();

            //AttachmentPopupPageView
            SimpleIoc.Default.Register<TaxEvasionReportAttachmentPageViewModel>();
            SimpleIoc.Default.Register<FileAttachmentPopUpPageViewModel>();
            SimpleIoc.Default.Register<VATIndividualSignupTnCPageViewModel>();
            SimpleIoc.Default.Register<FinancialDetailAttachmentPopupPageViewModel>();
            SimpleIoc.Default.Register<NewAccountPopUpPageViewModel>();
            SimpleIoc.Default.Register<NewAccountPopUpPageViewModel>();
            SimpleIoc.Default.Register<TaxpayersCertificatesPageViewModel>();
            SimpleIoc.Default.Register<ZAKATReturnDetailsViewModel>();
            SimpleIoc.Default.Register<TINDeregistrationPageView>();
            SimpleIoc.Default.Register<ZakatRegistrationDetailsListPageView>();

            SimpleIoc.Default.Register<InternationalCodeSearchPageViewModel>();
            SimpleIoc.Default.Register<UnlockAccountTINPageViewModel>();
            SimpleIoc.Default.Register<UnlockAccountSuccessPageViewModel>();
            SimpleIoc.Default.Register<TINDeregestrationSuccessPageViewModel>();
            SimpleIoc.Default.Register<TINDeregistrationCloseIndividualOutletsPageViewModel>();

            SimpleIoc.Default.Register<VATRefundListPageViewModel>();
            SimpleIoc.Default.Register<VATRefundDetailsPageViewModel>();
            SimpleIoc.Default.Register<VATRefundsNewRequestViewModel>();
            SimpleIoc.Default.Register<VATRefundsSuccessPageViewModel>();
            SimpleIoc.Default.Register<VATRefundsInstructionsPageViewModel>();

            SimpleIoc.Default.Register<AttachmentPopUpViewModel>();
            SimpleIoc.Default.Register<MorePopUpPageViewModel>();

            // * Taxpayer Profile
            SimpleIoc.Default.Register<NewTaxpayerProfileViewModel>();
            SimpleIoc.Default.Register<UpdateMobileViewModel>();
            SimpleIoc.Default.Register<UpdateEmailViewModel>();
            SimpleIoc.Default.Register<VerificationEmailPasswordViewModel>();
            SimpleIoc.Default.Register<UpdatePasswordViewModel>();
            SimpleIoc.Default.Register<TaxpayerProfileSuccessViewModel>();
            SimpleIoc.Default.Register<ShowVatInformationConfirmationPageViewModel>();
            SimpleIoc.Default.Register<RefundAccountPopupPageViewModel>();
            SimpleIoc.Default.Register<NewAccountPopPageViewModel>();

            //Account Statements
            //AccountStatementsPageView
            SimpleIoc.Default.Register<AccountStatementsPageViewModel>();

            SimpleIoc.Default.Register<AccountStatementBillsPageViewModel>();
            SimpleIoc.Default.Register<AccountStatementsFiltersPageViewModel>();
            SimpleIoc.Default.Register<AccountStatementsDownloadPageViewModel>();
            //Cr6264
            SimpleIoc.Default.Register<NewYesorNoPageViewModel>();
            //AccountStatementsDownloadPageView
            //
            #endregion

            #region Customs Service IoC
            SimpleIoc.Default.Register<InquiryAboutCustomsDeclarationViewModel>();
            SimpleIoc.Default.Register<TraifSectionsViewModel>();
            SimpleIoc.Default.Register<ReportFinancialViolationViewModel>();
            SimpleIoc.Default.Register<ReportsMenuViewModel>();
            SimpleIoc.Default.Register<LaboratoryPaymentOfInsuranceFeesViewModel>();
            SimpleIoc.Default.Register<SearchIndiactivePriceForExciseGoodsViewModel>();
            SimpleIoc.Default.Register<ExciseTaxViewModel>();
            SimpleIoc.Default.Register<TahqaqScanPageViewModel>();
            SimpleIoc.Default.Register<TaxCalculatorViewModel>();
            SimpleIoc.Default.Register<SubmitReportViewModel>();
            SimpleIoc.Default.Register<LiveVideoViewModel>();
            SimpleIoc.Default.Register<ReportOTPViewModel>();

            SimpleIoc.Default.Register<MyReportsViewModel>();
            SimpleIoc.Default.Register<UploadingPopupViewModel>();

            SimpleIoc.Default.Register<E_DeclerationViewModel>();
            SimpleIoc.Default.Register<TrackShipmentViewModel>();
            SimpleIoc.Default.Register<ListUserRequestsViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<RateUsViewModel>();
            SimpleIoc.Default.Register<CustomLoginViewModel>();
            SimpleIoc.Default.Register<BaseEDeclarationViewModel>();
            SimpleIoc.Default.Register<BaseProductDeclarationViewModel>();
            SimpleIoc.Default.Register<EDeclarationInformationsViewModel>();
            SimpleIoc.Default.Register<RegisterZATCAUserViewModel>();
            SimpleIoc.Default.Register<NativeNafathLoginPageViewModel>();
            SimpleIoc.Default.Register<EDeclarationPaymentViewModel>();
            SimpleIoc.Default.Register<TransactionReceptionViewModel>();
            SimpleIoc.Default.Register<IAMLoginViewModel>();
            SimpleIoc.Default.Register<ReviewRequestViewModel>();
            SimpleIoc.Default.Register<EDeclerationViewModel>();
            SimpleIoc.Default.Register<CustomsPaymentViewModel>();
            SimpleIoc.Default.Register<StateManager>();
            SimpleIoc.Default.Register<AboutZakatyViewModel>();
            SimpleIoc.Default.Register<CustomServiceMenuViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();
            SimpleIoc.Default.Register<CustomFeesFormViewModel>();
            SimpleIoc.Default.Register<CustomServiceMenuViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();
            SimpleIoc.Default.Register<FasahLoginViewModel>();
            SimpleIoc.Default.Register<BaseLoginViewModel>();
            SimpleIoc.Default.Register<InquiryaboutCustomsIssuesViewModel>();

            SimpleIoc.Default.Register<UpdateManagerViewModel>();
            SimpleIoc.Default.Register<ChangeMobNafathPageViewMode>();
            SimpleIoc.Default.Register<ChangeMobileRequestViewModel>();

            
            #endregion
        }

        #region NewDesignViewModel

        public ViewModel.NewDesignViewModel.VATServicesPageViewModel.VATServicesPageViewModel VATServicesPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ViewModel.NewDesignViewModel.VATServicesPageViewModel.VATServicesPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        //CR6094
        public NafathPopupPageViewModel NafathPopupPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NafathPopupPageViewModel>();
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        public NafathLoginPageViewModel NafathLoginPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NafathLoginPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NativeNafathLoginPageViewModel NativeNafathLoginPageViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NativeNafathLoginPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public TaxManagementPageViewModel TaxManagementPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxManagementPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public TaxEvasionPageWebViewModel TaxEvasionPageWebView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxEvasionPageWebViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public TaxpayerSubsidyViewModel TaxpayerSubsidyRequest
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxpayerSubsidyViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public NotesPopUpPageViewModel NotesPopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NotesPopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public NotesDescriptionPopUpPageViewModel NotesDescriptionPopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NotesDescriptionPopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public SupportPageViewModel SupportPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SupportPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZatcaInfoMenuPageViewModel ZatcaInfoMenuPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZatcaInfoMenuPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATCreditCarriedForwardPopUpPageViewModel VATCreditCarriedForwardPopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATCreditCarriedForwardPopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public DashboardAnonymousMenuPageViewModel DashboardAnonymousMenuPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<DashboardAnonymousMenuPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TINDeregistrationCloseIndividualOutletsPageViewModel TINDeregistrationCloseIndividualOutletsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TINDeregistrationCloseIndividualOutletsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public SignUpForEstablishmentPageViewModel SignUpForEstablishmentPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SignUpForEstablishmentPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public EstablishmentSignUPPageViewModel EstablishmentSignUPPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<EstablishmentSignUPPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public GAZTNewDesignOnBoardingAnimationPageViewModel GAZTNewDesignOnBoardingAnimationPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignOnBoardingAnimationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionMyReportsListPageViewModel TaxEvasionMyReportsListPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxEvasionMyReportsListPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public TaxpayerCorrespondancePageViewModel TaxpayerCorrespondancePageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxpayerCorrespondancePageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxpayerCorrespondanceDetailPageViewModel TaxpayerCorrespondanceDetailPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxpayerCorrespondanceDetailPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public TaxpayersCertificatesPageViewModel TaxpayersCertificatesPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxpayersCertificatesPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel GAZTNewDesignVATReturnUpdatedUIPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignVATReturnUpdatedUIPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public GAZTNewDesignDashBoardPageViewModel GAZTNewDesignDashBoardPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignDashBoardPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public GAZTNewDesignDashBoardPageViewModel InfoPopUpPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignDashBoardPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatForm5PageViewModel ZakatForm5PageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatForm5PageViewModel>();
                    SimpleIoc.Default.Register<ZakatForm5PageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatForm5PageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public GAZTNewDesignForgotPasswordPageViewModel GAZTNewDesignForgotPasswordPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignForgotPasswordPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public GAZTNewDesignMyBillsPageViewModel GAZTNewDesignMyBillsPageView
        {
            get
            {
                try
                {
                    //SimpleIoc.Default.Unregister<GAZTNewDesignMyBillsPageViewModel>();
                    //SimpleIoc.Default.Register<GAZTNewDesignMyBillsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignMyBillsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public GAZTNewDesignMyReturnsNewPageViewModel GAZTNewDesignMyReturnsNewPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<GAZTNewDesignMyReturnsNewPageViewModel>();
                    SimpleIoc.Default.Register<GAZTNewDesignMyReturnsNewPageViewModel>();
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignMyReturnsNewPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATLookUpNewPageViewModel VATLookUpNewPageView
        {
            get
            {
                try
                {

                    SimpleIoc.Default.Unregister<VATLookUpNewPageViewModel>();
                    SimpleIoc.Default.Register<VATLookUpNewPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATLookUpNewPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATDeRegistrationDetailsPageViewModel VATDeregistrationDetailsPage
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATDeRegistrationDetailsPageViewModel>();
                    SimpleIoc.Default.Register<VATDeRegistrationDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATDeRegistrationDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATDeRegistrationInstructionsPageViewModel VATDeregistrationInstructionsPage
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATDeRegistrationInstructionsPageViewModel>();
                    SimpleIoc.Default.Register<VATDeRegistrationInstructionsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATDeRegistrationInstructionsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATDeregistrationSuccessPageViewModel VATDeregistrationSuccessPage
        {
            get
            {
                try
                {

                    return ServiceLocator.Current.GetInstance<VATDeregistrationSuccessPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public NewZakatObjectionPageViewModel NewZakatObjectionPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<NewZakatObjectionPageViewModel>();
                    SimpleIoc.Default.Register<NewZakatObjectionPageViewModel>();
                    return ServiceLocator.Current.GetInstance<NewZakatObjectionPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public EstablishmentRegistrationPageViewModel EstablishmentRegistrationPage
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<EstablishmentRegistrationPageViewModel>();
                    SimpleIoc.Default.Register<EstablishmentRegistrationPageViewModel>();

                    return ServiceLocator.Current.GetInstance<EstablishmentRegistrationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public EstablishmentAmendUpdatePageViewModel EstablishmentAmendUpdatePage
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<EstablishmentAmendUpdatePageViewModel>();
                    SimpleIoc.Default.Register<EstablishmentAmendUpdatePageViewModel>();

                    return ServiceLocator.Current.GetInstance<EstablishmentAmendUpdatePageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public OutletDetailsPageViewModel OutletDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<OutletDetailsPageViewModel>();
                    SimpleIoc.Default.Register<OutletDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<OutletDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public OutletDetailsAmendUpdatePageViewModel OutletDetailsAmendUpdatePageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<OutletDetailsAmendUpdatePageViewModel>();
                    SimpleIoc.Default.Register<OutletDetailsAmendUpdatePageViewModel>();

                    return ServiceLocator.Current.GetInstance<OutletDetailsAmendUpdatePageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ActivityItemPageViewModel ActivityItemPage
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ActivityItemPageViewModel>();
                    SimpleIoc.Default.Register<ActivityItemPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ActivityItemPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ActivityItemAmendUpdatePageViewModel ActivityItemAmendUpdatePageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ActivityItemAmendUpdatePageViewModel>();
                    SimpleIoc.Default.Register<ActivityItemAmendUpdatePageViewModel>();
                    return ServiceLocator.Current.GetInstance<ActivityItemAmendUpdatePageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public RegistrationSuccessfulViewModel RegistrationSuccessfulPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<RegistrationSuccessfulViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public EstablishmentAmendUpdateSuccessfulPageViewModel EstablishmentAmendUpdateSuccessfulPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<EstablishmentAmendUpdateSuccessfulPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionVerifyMobileViewModel TaxEvasionVerifyMobileNumberPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxEvasionVerifyMobileViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public NewTaxEvasionFormPageViewModel NewTaxEvasionFormPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NewTaxEvasionFormPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public NewTaxEvasionFormPageViewModel NewTaxEvasionFormSuccessPaveView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NewTaxEvasionFormPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        // * Taxpayer Profile
        public NewTaxpayerProfileViewModel TaxpayerProfilePageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NewTaxpayerProfileViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public UpdateMobileViewModel UpdateMobilePopUp
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UpdateMobileViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public UpdateEmailViewModel UpdateEmailPopUp
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UpdateEmailViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VerificationEmailPasswordViewModel VerificationPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VerificationEmailPasswordViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public UpdatePasswordViewModel UpdatePasswordPopUp
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UpdatePasswordViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxpayerProfileSuccessViewModel TaxpayerProfileSuccessPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxpayerProfileSuccessViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        #endregion


        #region Payment Implementations

        public PaymnetProcessWebviewViewModel PaymentProcessWebview
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<PaymnetProcessWebviewViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        #endregion


        #region OldDesignViewModel
        public StyleTestUIPageViewModel StyleTestUIPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<StyleTestUIPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }


        }
        public PdfViewModel pdfView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<PdfViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public StyleTestUIPageViewModel StyleTestUIPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<StyleTestUIPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public FinancialDetailAttachmentPopupPageViewModel FinancialDetailAttachmentPopupPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<FinancialDetailAttachmentPopupPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATIndividualSignupTnCPageViewModel VATIndividualSignupTnCPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATIndividualSignupTnCPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public InternationalMobileNumberCodePagesViewModel InternationalMobileNumberCodePages
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<InternationalMobileNumberCodePagesViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public InternationalCodeSearchPageViewModel InternationalCodeSearchPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<InternationalCodeSearchPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        /// <summary>
        /// Returns the current instance of MyCertificateViewModel
        /// </summary>
        public MyBillsViewModel MyBillsView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<MyBillsViewModel>();
                    SimpleIoc.Default.Register<MyBillsViewModel>();
                    return ServiceLocator.Current.GetInstance<MyBillsViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATLookupPageViewModel VATLookupPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATLookupPageViewModel>();
                    SimpleIoc.Default.Register<VATLookupPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATLookupPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ZakatReturnListPageViewModel ZakatReturnListPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatReturnListPageViewModel>();
                    SimpleIoc.Default.Register<ZakatReturnListPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatReturnListPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ZakatReturnDetailsPageViewModel ZakatReturnDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatReturnDetailsPageViewModel>();
                    SimpleIoc.Default.Register<ZakatReturnDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatReturnDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public SalesDetailsPageViewModel SalesDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<SalesDetailsPageViewModel>();
                    SimpleIoc.Default.Register<SalesDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<SalesDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AmendSalesDetailsPageViewModel AmendSalesDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<AmendSalesDetailsPageViewModel>();
                    SimpleIoc.Default.Register<AmendSalesDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<AmendSalesDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ICRListPageViewModel ICRListPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ICRListPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATReturnsPageViewModelEX VATReturnsPageViewEX
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATReturnsPageViewModelEX>();
                    SimpleIoc.Default.Register<VATReturnsPageViewModelEX>();
                    return ServiceLocator.Current.GetInstance<VATReturnsPageViewModelEX>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AcknowledgementDetailsPageViewModel AcknowledgementDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<AcknowledgementDetailsPageViewModel>();
                    SimpleIoc.Default.Register<AcknowledgementDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<AcknowledgementDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public DisplayNotesPageViewModel DisplayNotesPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<DisplayNotesPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AttachmentPageViewModel AttachmentPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AttachmentPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AddNotePageViewModel AddNotePageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AddNotePageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public CreditCarriedPageViewModel CreditCarriedPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CreditCarriedPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AddPopPageViewModel AddPopPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AddPopPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public GAZTNewDesignShowVatInformationPopUpPageViewModel GAZTNewDesignShowVatInformationPopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignShowVatInformationPopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionReportMobilePageViewModel TaxEvasionReportPhonePageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<TaxEvasionReportMobilePageViewModel>();
                    SimpleIoc.Default.Register<TaxEvasionReportMobilePageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportMobilePageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionReportTypePageViewModel TaxEvasionReportTypePageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<TaxEvasionReportTypePageViewModel>();
                    SimpleIoc.Default.Register<TaxEvasionReportTypePageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportTypePageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionReportFormPageViewModel TaxEvasionReportFormPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<TaxEvasionReportFormPageViewModel>();
                    SimpleIoc.Default.Register<TaxEvasionReportFormPageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportFormPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionFormPageViewModel TaxEvasionFormPage
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<TaxEvasionFormPageViewModel>();
                    SimpleIoc.Default.Register<TaxEvasionFormPageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxEvasionFormPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionRegistrationViewModel TaxEvasionRegistrationFormPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<TaxEvasionRegistrationViewModel>();
                    SimpleIoc.Default.Register<TaxEvasionRegistrationViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxEvasionRegistrationViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionReportAttachmentPageViewModel TaxEvasionAttachmentPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportAttachmentPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public FormBundleStatusPageViewModel FormBundleStatusPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<FormBundleStatusPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public SignUpTAndCPageViewModel SignUpTAndCPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SignUpTAndCPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public SignUpFormPageViewModel SignUpFormPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SignUpFormPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionReportListPageViewModel TaxEvasionReportListPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportListPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public CreateGaztAccountPageViewModel CreateGaztAccountPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CreateGaztAccountPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AccountCreatedPageViewModel AccountCreatedPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AccountCreatedPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AccountCreatedSuccessfullyPageViewModel AccountCreatedSuccessfullyPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AccountCreatedSuccessfullyPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        #endregion

        #region OldSFViewModels

        public SFLoginPageViewModel SFLoginPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SFLoginPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ReturnsPageViewModel ReturnsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ReturnsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public FAQPageViewModel FAQPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<FAQPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AboutUsPageViewModel AboutUsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AboutUsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public PrivacyAndPolicyPageViewModel PrivacyAndPolicyPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<PrivacyAndPolicyPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public MyReturnsPageViewModel MyReturnsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MyReturnsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ContactUsPageViewModel ContactUsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ContactUsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATIndividualSignupPageViewModel VATIndividualSignupPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATIndividualSignupPageViewModel>();
                    SimpleIoc.Default.Register<VATIndividualSignupPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATIndividualSignupPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public IndividualRegistrationPageViewModel IndividualRegistrationPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<IndividualRegistrationPageViewModel>();
                    SimpleIoc.Default.Register<IndividualRegistrationPageViewModel>();
                    return ServiceLocator.Current.GetInstance<IndividualRegistrationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public RegistrationSuccessfulPageViewModel VATRegistrationSuccessfulPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<RegistrationSuccessfulPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATRegistrationPageViewModel VATRegistrationPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATRegistrationPageViewModel>();
                    SimpleIoc.Default.Register<VATRegistrationPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATRegistrationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATAmendReactivationPageViewModel VATAmendReactivationPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATAmendReactivationPageViewModel>();
                    SimpleIoc.Default.Register<VATAmendReactivationPageViewModel>();

                    return ServiceLocator.Current.GetInstance<VATAmendReactivationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATRegistrationDisplayDetailsPageViewModel VATRegistrationDisplayDetails
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATRegistrationDisplayDetailsPageViewModel>();
                    SimpleIoc.Default.Register<VATRegistrationDisplayDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATRegistrationDisplayDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATRegistrationSuccessfullPageViewModel VATRegistrationSuccessfullPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATRegistrationSuccessfullPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VATAmendReactivationSuccesssulPageViewModel VATAmendReactivationSuccesssulPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATAmendReactivationSuccesssulPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public FileAttachmentPopUpPageViewModel FileAttachmentPopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<FileAttachmentPopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public NewAccountPopUpPageViewModel NewAccountPopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NewAccountPopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public UnlockAccountTINPageViewModel UnlockAccountTINPageViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UnlockAccountTINPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public UnlockAccountSuccessPageViewModel UnlockAccountSuccessPageViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UnlockAccountSuccessPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public TINDeregestrationSuccessPageViewModel TINDeregestrationSuccessPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TINDeregestrationSuccessPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public GAZTNewDesignRecoverUsernameViewModel GAZTNewDesignRecoverUsernameViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignRecoverUsernameViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public GAZTNewDesignRecoverPasswordPageViewModel GAZTNewDesignRecoverPasswordPageViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignRecoverPasswordPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZAKATReturnDetailsViewModel ZAKATReturnDetailsView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZAKATReturnDetailsViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZAKATReturnDetailsViewModel ZAKATReturnDetailsSuccessView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZAKATReturnDetailsViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATRefundListPageViewModel VATRefundsListPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATRefundListPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATRefundDetailsPageViewModel VATRefundDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATRefundDetailsPageViewModel>();
                    SimpleIoc.Default.Register<VATRefundDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATRefundDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATRefundsNewRequestViewModel VATRefundsNewRequestPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATRefundsNewRequestViewModel>();
                    SimpleIoc.Default.Register<VATRefundsNewRequestViewModel>();
                    return ServiceLocator.Current.GetInstance<VATRefundsNewRequestViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATRefundsSuccessPageViewModel VATRefundsSuccessPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATRefundsSuccessPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATRefundsInstructionsPageViewModel VATRefundsInstructionsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATRefundsInstructionsPageViewModel>();
                    SimpleIoc.Default.Register<VATRefundsInstructionsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<VATRefundsInstructionsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }


        //
        public ZakatReturnDetailsSuccessfullPageViewModel ZakatReturnDetailsSuccessfullPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatReturnDetailsSuccessfullPageViewModel>();
                    SimpleIoc.Default.Register<ZakatReturnDetailsSuccessfullPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatReturnDetailsSuccessfullPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatReturnNewSuccessViewModel ZakatReturnNewSuccessView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZakatReturnNewSuccessViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }


        public AttachmentPopUpViewModel AttachmentPopUp
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AttachmentPopUpViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }


        public ZakatObjectionSuccessfullPageViewModel ZakatObjectionSuccessfullPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZakatObjectionSuccessfullPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ZakatAcknowledgmentPageViewModel ZakatAcknowledgmentPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZakatAcknowledgmentPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public QuickActionPopUpPageViewModel QuickActionPopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<QuickActionPopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }


        public VATDeclarationAttachmentPageViewModel VATDeclarationAttachmentPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATDeclarationAttachmentPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public AccountStatementsPageViewModel AccountStatementsPageView
        {
            get
            {
                try
                {

                    SimpleIoc.Default.Unregister<AccountStatementsPageViewModel>();
                    SimpleIoc.Default.Register<AccountStatementsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<AccountStatementsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public AccountStatementBillsPageViewModel AccountStatementBillsPageView
        {
            get
            {
                try
                {

                    SimpleIoc.Default.Unregister<AccountStatementBillsPageViewModel>();
                    SimpleIoc.Default.Register<AccountStatementBillsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<AccountStatementBillsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public AccountStatementBillsPageViewModel AccountStatementsFilterPageView
        {
            get
            {
                try
                {

                    //SimpleIoc.Default.Unregister<AccountStatementsPageViewModel>();
                    //SimpleIoc.Default.Register<AccountStatementsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<AccountStatementBillsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public AccountStatementsDownloadPageViewModel AccountStatementsDownloadPageView
        {
            get
            {
                try
                {

                    SimpleIoc.Default.Unregister<AccountStatementsDownloadPageViewModel>();
                    SimpleIoc.Default.Register<AccountStatementsDownloadPageViewModel>();
                    return ServiceLocator.Current.GetInstance<AccountStatementsDownloadPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        //AccountStatementsDownloadPageViewModel
        public AccountStatementsFiltersPageViewModel AccountStatementsFiltersPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AccountStatementsFiltersPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        //AccountStatementsFiltersPageViewModel

        //SYNC FUSION INTEGRATION
        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            #region NewDesign

            navigationService.Configure(App.GAZTNewDesignOnBoardingAnimationPageView, typeof(GAZTNewDesignOnBoardingAnimationPageView));
            navigationService.Configure(App.GAZTNewDesignVATReturnUpdatedUIPageView, typeof(GAZTNewDesignVATReturnUpdatedUIPageView));
            navigationService.Configure(App.GAZTNewDesignStyleTestUIPageView, typeof(StyleTestUIPageView));
            navigationService.Configure(App.GAZTNewDesignForgotPasswordPageView, typeof(GAZTNewDesignForgotPasswordPageView));
            navigationService.Configure(App.GAZTNewDesignMyBillsPageView, typeof(GAZTNewDesignMyBillsPageView));
            navigationService.Configure(App.MyBillsSuccessPageView, typeof(MyBillsSuccessPageView));
            navigationService.Configure(App.MyBillsSadadDetailsPageView, typeof(MyBillsSadadDetailsPageView));
            navigationService.Configure(App.GAZTNewDesignDashBoardPageView, typeof(GAZTNewDesignDashBoardPageView));
            navigationService.Configure(App.GAZTNewDesignMyReturnsNewPageView, typeof(GAZTNewDesignMyReturnsNewPageView));
            navigationService.Configure(App.TaxpayerCorrespondancePageView, typeof(TaxpayerCorrespondancePageView));
            navigationService.Configure(App.TaxpayerCorrespondanceDetailPageView, typeof(TaxpayerCorrespondanceDetailPageView));
            navigationService.Configure(App.NewZakatObjectionPageView, typeof(NewZakatObjectionPageView));
            navigationService.Configure(App.VATReturnSuccessfullPageView, typeof(VATReturnSuccessfullPageView));
            navigationService.Configure(App.VatReturnNewSuccessPageView, typeof(VatReturnNewSuccessPageView));
            navigationService.Configure(App.NewTaxEvasionFormPageView, typeof(NewTaxEvasionFormPageView));
            navigationService.Configure(App.NewTaxEvasionFormSuccessPaveView, typeof(NewTaxEvasionFormSuccessPaveView));
            navigationService.Configure(App.AttachmentPopUp, typeof(AttachmentPopUp));
            navigationService.Configure(App.RefundAccountPopupPageView, typeof(RefundAccountPopupPageView));
            navigationService.Configure(App.NewAccountPopPageView, typeof(NewAccountPopUpPageView));
            navigationService.Configure(App.EstablishmentSignUPPageView, typeof(EstablishmentSignUPPageView));
            navigationService.Configure(App.SignUpForEstablishmentPageView, typeof(SignUpForEstablishmentPageView));
            navigationService.Configure(App.AccountCreatedSuccessfullyPageView, typeof(AccountCreatedSuccessfullyPageView));
            navigationService.Configure(App.DashboardAnonymousMenuPageView, typeof(DashboardAnonymousMenuPageView));
            navigationService.Configure(App.VATCreditCarriedForwardPopUpPageView, typeof(VATCreditCarriedForwardPopUpPageView));
            navigationService.Configure(App.SupportPageView, typeof(SupportPageView));
            navigationService.Configure(App.ZatcaInfoMenuPageView, typeof(ZatcaInfoMenuPageView));

            navigationService.Configure(App.NotesPopUpPageView, typeof(NotesPopUpPageView));
            navigationService.Configure(App.NotesDescriptionPopUpPageView, typeof(NotesDescriptionPopUpPageView));
            navigationService.Configure(App.TaxManagementPageView, typeof(TaxManagementPageView));

            navigationService.Configure(App.QuickActionPopUpPageView, typeof(QuickActionPopUpPageView));
            navigationService.Configure(App.VATAmendReactivationPageView, typeof(VATAmendReactivationPageView));
            navigationService.Configure(App.VATServicesPageView, typeof(VATServicesPageView));
            navigationService.Configure(App.TaxEvasionPageWebView, typeof(TaxEvasionPageWebView));
            navigationService.Configure(App.TaxpayerSubsidyRequest, typeof(TaxpayerSubsidyRequest));
            navigationService.Configure(App.GAZTBankAccountManagementPageView, typeof(BankAccountManagementPageView));
            navigationService.Configure(App.GAZTBankAccountAddOrUpdatePageView, typeof(BankAccountAddorUpdateIBANPageView));
            #endregion

            #region NewDesignRelease2
            navigationService.Configure(App.InstalmentPlanPageView, typeof(InstalmentPlanPageView));
            navigationService.Configure(App.ZakatInstalmentPlanPageView, typeof(ZakatInstalmentPlanPageView));
            navigationService.Configure(App.ZakatInstalmentPlanListPageView, typeof(ZakatInstalmentPlanListPageView));
            navigationService.Configure(App.OldZakatInstalmentPlanPageView, typeof(OldZakatInstalmentPlanPageView));
            navigationService.Configure(App.OldZakatInstalmentPlanListPageView, typeof(OldZakatInstalmentPlanListPageView));
            navigationService.Configure(App.VatInstalmentPlanPageView, typeof(VatInstalmentPlanPageView));
            navigationService.Configure(App.VatInstalmentPlanListPageView, typeof(VatInstalmentPlanListPageView));
            navigationService.Configure(App.TaxEvasionMyReportsListPageView, typeof(TaxEvasionMyReportsListPageView));
            navigationService.Configure(App.TaxEvasionReportDetailPageView, typeof(TaxEvasionReportDetailPageView));
            navigationService.Configure(App.ZakatAcknowledgmentPageView, typeof(ZakatAcknowledgmentPageView));
            navigationService.Configure(App.ChangeFillingPeriodPageView, typeof(ChangeFillingPeriodPageView));
            navigationService.Configure(App.ContractReleasePageView, typeof(ContractReleasePageView));
            navigationService.Configure(App.ContractReleaseSuccessPageView, typeof(ContractReleaseSuccessPageView));
            navigationService.Configure(App.VatInstalmentPlanSuccessPage, typeof(VatInstalmentPlanSuccessPage));
            navigationService.Configure(App.ContractReleaseListPageView, typeof(ContractReleaseListPageView));
            navigationService.Configure(App.ChangeFillingPeriodListPageView, typeof(ChangeFillingPeriodListPageView));
            navigationService.Configure(App.InfoPopUpPage, typeof(InfoPopUpPage));
            navigationService.Configure(App.VatReviewPageView, typeof(VatReviewPageView));
            navigationService.Configure(App.VatReviewListPageView, typeof(VatReviewListPageView));
            navigationService.Configure(App.VatReviewSuccessPageView, typeof(VatReviewSuccessPageView));
            navigationService.Configure(App.VatReviewViewApplicationPageView, typeof(VatReviewViewApplicationPageView));
            navigationService.Configure(App.ObjectionsSelectionPageView, typeof(ObjectionsSelectionPageView));
            navigationService.Configure(App.ZakatObjectionsListPageView, typeof(ZakatObjectionsListPageView));
            navigationService.Configure(App.ObjectionsSelectionPageView, typeof(ObjectionsSelectionPageView));
            navigationService.Configure(App.ZakatObjectionPageView, typeof(ZakatObjectionPageView));
            navigationService.Configure(App.ZakatObjectionSuccessPageView, typeof(ZakatObjectionSuccessPageView));
            navigationService.Configure(App.VATDeclarationAttachmentPageView, typeof(VATDeclarationAttachmentPageView));
            navigationService.Configure(App.MoreMenuPopUpPageViewRTwo, typeof(MoreMenuPopUpPageViewRTwo));
            navigationService.Configure(App.VRVatDeRegViewAppPageView, typeof(VRVatDeRegViewAppPageView));
            navigationService.Configure(App.VRInputTDViewAppPageViewApp, typeof(VRInputTDViewAppPageViewApp));
            navigationService.Configure(App.VRSuspensionViewAppPageView, typeof(VRSuspensionViewAppPageView));
            navigationService.Configure(App.VRVatRegViewPageView, typeof(VRVatRegViewPageView));
            navigationService.Configure(App.VRVatGroupPageView, typeof(VRVatGroupPageView));
            #endregion

            #region Payment Implementatoin

            navigationService.Configure(App.PaymentProcessWebview, typeof(PaymentProcessWebview));

            #endregion

            #region SYNCFUSION INTEGRATION
            navigationService.Configure(App.SFLoginPageView, typeof(SFLoginPageView));

            //SYNCFUSION INTEGRATION
            navigationService.Configure(App.PdfView, typeof(PdfView));
            navigationService.Configure(App.ZakatReturnListPageView, typeof(ZakatReturnListPageView));
            navigationService.Configure(App.ZakatReturnDetailsPageView, typeof(ZakatReturnDetailsPageView));
            navigationService.Configure(App.SalesDetailsPageView, typeof(SalesDetailsPageView));
            navigationService.Configure(App.AmendSalesDetailsPageView, typeof(AmendSalesDetailsPageView));
            navigationService.Configure(App.ICRListPageView, typeof(ICRListPageView));
            navigationService.Configure(App.VATReturnsPageViewEX, typeof(VATReturnsPageViewEX));
            navigationService.Configure(App.AAcknowledgementView, typeof(AAcknowledgementView));
            navigationService.Configure(App.AcknowledgementDetailsPageView, typeof(AcknowledgementDetailsPageView));
            navigationService.Configure(App.DisplayNotesPageView, typeof(DisplayNotesPageView));
            navigationService.Configure(App.AttachmentPageView, typeof(AttachmentPageView));
            navigationService.Configure(App.AddNotePageView, typeof(AddNotePageView));
            navigationService.Configure(App.AddPopPageView, typeof(AddPopPageView));
            navigationService.Configure(App.CreditCarriedPageView, typeof(CreditCarriedPageView));
            navigationService.Configure(App.FormBundleStatusPageView, typeof(FormBundleStatusPageView));
            navigationService.Configure(App.CreditCarriedPageView, typeof(CreditCarriedPageView));
            navigationService.Configure(App.FAQPageView, typeof(FAQPageView));
            navigationService.Configure(App.AboutUsPageView, typeof(AboutUsPageView));
            navigationService.Configure(App.PrivacyAndPolicyPageView, typeof(PrivacyAndPolicyPageView));

            navigationService.Configure(App.ContactUsPageView, typeof(ContactUsPageView));
            navigationService.Configure(App.VATIndividualSignupPageView, typeof(VATIndividualSignupPageView));
            navigationService.Configure(App.IndividualRegistrationPageView, typeof(IndividualRegistrationPageView));
            navigationService.Configure(App.RegistrationSuccessfulPageView, typeof(RegistrationSuccessfulPageView));
            navigationService.Configure(App.VATRegistrationPageView, typeof(VATRegistrationPageView));
            navigationService.Configure(App.VATRegistrationSuccessfullPageView, typeof(VATRegistrationSuccessfullPageView));
            navigationService.Configure(App.VATAmendReactivationSuccessfulPageView, typeof(VATAmendReactivationSuccessfulPageView));

            navigationService.Configure(App.FileAttachmentPopUpPageView, typeof(FileAttachmentPopUpPageView));

            navigationService.Configure(App.VATIndividualSignupTnCPageView, typeof(VATIndividualSignupTnCPageView));
            navigationService.Configure(App.FinancialDetailAttachmentPopupPageView, typeof(FinancialDetailAttachmentPopupPageView));

            navigationService.Configure(App.InternationalMobileNumberCodePages, typeof(InternationalMobileNumberCodePages));
            navigationService.Configure(App.UnlockAccountTINPageView, typeof(UnlockAccountTINPageView));
            navigationService.Configure(App.UnlockAccountSuccessPageView, typeof(UnlockAccountSuccessPageView));
            navigationService.Configure(App.InternationalCodeSearchPage, typeof(InternationalCodeSearchPage));

            navigationService.Configure(App.GAZTNewDesignRecoverUsername, typeof(GAZTNewDesignRecoverUsernamePageView));
            navigationService.Configure(App.GAZTNewDesignRecoverPasswordPageView, typeof(GAZTNewDesignRecoverPasswordPageView));
            navigationService.Configure(App.VATLookUpNewPageView, typeof(VATLookUpNewPageView));
            navigationService.Configure(App.TaxpayersCertificatesPageView, typeof(TaxpayersCertificatesPageView));

            navigationService.Configure(App.VATDeregistrationDetailsPage, typeof(VATDeregistrationDetailsPage));
            navigationService.Configure(App.VATDeregistrationInstructionsPage, typeof(VATDeregistrationInstructionsPage));
            navigationService.Configure(App.VATDeregistrationSuccessPage, typeof(VATDeregistrationSuccessPage));

            navigationService.Configure(App.GAZTForm5PageView, typeof(ZakatForm5PageView));
            navigationService.Configure(App.ZAKATReturnDetailsView, typeof(ZAKATReturnDetailsView));
            navigationService.Configure(App.TINDeregestrationSuccessPageView, typeof(TINDeregestrationSuccessPageView));
            navigationService.Configure(App.TINDeregistrationPageView, typeof(TINDeregistrationPageView));
            navigationService.Configure(App.TINDeregistrationCloseIndividualOutletsPageView, typeof(TINDeregistrationCloseIndividualOutletsPageView));

            navigationService.Configure(App.ZakatRegistrationDetailsListPageView, typeof(ZakatRegistrationDetailsListPageView));
            navigationService.Configure(App.GeneralServicesListPageView, typeof(GeneralServicesListPageView));
            navigationService.Configure(App.RefundRequestMenuListPageView, typeof(RefundRequestMenuListPageView));
            navigationService.Configure(App.FillingFreuencyMenuListPageView, typeof(FillingFreuencyMenuListPageView));
            navigationService.Configure(App.ZakatRegistrationTaxPayerDetails, typeof(ZakatRegistrationTaxPayerDetails));
            navigationService.Configure(App.ZakatRegistrationOutletsDetails, typeof(ZakatRegistrationOutletsDetails));
            navigationService.Configure(App.ZakatRegistrationFinancialDetails, typeof(ZakatRegistrationFinancialDetails));

            navigationService.Configure(App.ZakatReturnDetailsSuccessfullPageView, typeof(ZakatReturnDetailsSuccessfullPageView));
            navigationService.Configure(App.ZakatReturnNewSuccessPageView, typeof(ZakatReturnNewSuccessPageView));

            navigationService.Configure(App.TaxEvasionVerifyMobileNumberPage, typeof(TaxEvasionVerifyMobileNumberPage));
            navigationService.Configure(App.VATRefundsListPageView, typeof(VATRefundsListPageView));
            navigationService.Configure(App.VATRefundDetailsPageView, typeof(VATRefundDetailsPageViewModel));
            navigationService.Configure(App.VATRefundsNewRequestPageView, typeof(VATRefundsNewRequestPageView));
            navigationService.Configure(App.VATRefundsSuccessPageView, typeof(VATRefundsSuccessPageView));
            navigationService.Configure(App.GAZTNewDesignShowVatInformationPopUpPageView, typeof(GAZTNewDesignShowVatInformationPopUpPageView));
            navigationService.Configure(App.ZakatObjectionSuccessfullPageView, typeof(ZakatObjectionSuccessfullPageView));
            navigationService.Configure(App.VATRefundsInstructionsPageView, typeof(VATRefundsInstructionsPageView));
            navigationService.Configure(App.MorePopUpPageView, typeof(MorePopUpPageView));
            navigationService.Configure(App.ShowVatInformationConfirmationPageView, typeof(ShowVatInformationConfirmationPageView));

            navigationService.Configure(App.VATRegistrationDisplayDetails, typeof(VATRegistrationDisplayDetails));

            // * Taxpayer Profile
            navigationService.Configure(App.TaxpayerProfilePageView, typeof(TaxpayerProfilePageView));
            navigationService.Configure(App.UpdateMobilePopUp, typeof(UpdateMobilePopUp));
            navigationService.Configure(App.UpdateEmailPopUp, typeof(UpdateEmailPopUp));
            navigationService.Configure(App.VerificationPageView, typeof(VerificationPageView));
            navigationService.Configure(App.UpdatePasswordPopUp, typeof(UpdatePasswordPopUp));
            navigationService.Configure(App.TaxpayerProfileSuccessPage, typeof(TaxpayerProfileSuccessPage));
            // * End

            //EST
            navigationService.Configure(App.EstablishmentRegistrationPage, typeof(EstablishmentRegistrationPage));
            navigationService.Configure(App.EstablishmentAmendUpdatePage, typeof(EstablishmentAmendUpdatePageView));
            navigationService.Configure(App.ActivityItemPage, typeof(ActivityItemPage));
            navigationService.Configure(App.ActivityItemAmendUpdatePage, typeof(ActivityItemAmendUpdatePage));
            navigationService.Configure(App.OutletDetailsPageView, typeof(OutletDetailsPageView));
            navigationService.Configure(App.OutletDetailsAmendUpdatePageView, typeof(OutletDetailsAmendUpdatePageView));
            navigationService.Configure(App.RegistrationSuccessfulPage, typeof(RegistrationSuccessfulPage));
            navigationService.Configure(App.EstablishmentAmendUpdateSuccessfulPage, typeof(EstablishmentAmendUpdateSuccessfulPage));
            //End EST


            //Account Statements
            navigationService.Configure(App.AccountStatementsPageView, typeof(AccountStatementsPageView));
            navigationService.Configure(App.AccountStatementBillsPageView, typeof(AccountStatementBillsPageView));
            navigationService.Configure(App.AccountStatementsFiltersPageView, typeof(AccountStatementsFiltersPageView));
            navigationService.Configure(App.AccountStatementsNewFilterPageView, typeof(AccountStatementsNewFilterPageView));
            navigationService.Configure(App.AccountStatementsDownloadPageView, typeof(AccountStatementsDownloadPageView));

            //AccountStatementsDownloadPageViewModel
            //AccountStatementsFiltersPageViewModel
            //End Account Statements

            //Custom Services
            navigationService.Configure(App.InquiryAboutCustomsDeclarationView, typeof(InquiryAboutCustomsDeclaration));
            navigationService.Configure(App.TraifSectionsView, typeof(TraifSections));
            navigationService.Configure("ReportFinancialViolation", typeof(ReportFinancialViolation));
            navigationService.Configure("ReportsPage", typeof(ReportsPage));
            navigationService.Configure(App.LaboratoryPaymentOfInsuranceFees, typeof(LaboratoryPaymentOfInsuranceFees));
            navigationService.Configure("ExciseTax", typeof(ExciseTax));
            navigationService.Configure("SearchIndiactivePriceForExciseGoods", typeof(SearchIndiactivePriceForExciseGoods));
            navigationService.Configure("TahqaqScanPage", typeof(TahqaqScanPage));
            navigationService.Configure("TaxCalculator", typeof(TaxCalculator));
            navigationService.Configure("E_InvoicesScan", typeof(E_InvoicesScan));
            navigationService.Configure("SubmitReportPage", typeof(SubmitReportPage));
            navigationService.Configure("TermsPage", typeof(TermsPage));
            navigationService.Configure("MyReportsPage", typeof(MyReportsPage));
            navigationService.Configure("LiveVideoPage", typeof(LiveVideoPage));
            navigationService.Configure("MyReportDetailsPage", typeof(MyReportDetailsPage));
            navigationService.Configure("ReportSuccessPage", typeof(ReportSuccessPage));
            navigationService.Configure("EDeclerationView", typeof(EDeclerationView));
            navigationService.Configure("CreateE_Declaration", typeof(CreateE_Declaration));
            navigationService.Configure("ReiewPreviousDeclerations", typeof(ReiewPreviousDeclerations));
            navigationService.Configure("Home", typeof(Home));
            navigationService.Configure("CusromServiceMenu", typeof(CusromServiceMenu));
            navigationService.Configure("ExciseServices", typeof(ExciseServices));
            navigationService.Configure("VatServicesMenu", typeof(VatServicesMenu));
            navigationService.Configure("GeneralServices", typeof(GeneralServices));
            navigationService.Configure("SideMenuView", typeof(SideMenuView));
            navigationService.Configure("RateUs", typeof(RateUs));
            navigationService.Configure("CustomLogin", typeof(CustomLogin));
            navigationService.Configure("CustomDashBoardVi", typeof(CustomDashBoardVi));
            navigationService.Configure("LoginSelectionView", typeof(LoginSelectionView));
            navigationService.Configure("ReportOTPPage", typeof(ReportOTPPage));
            navigationService.Configure("InquiryAboutMyReportsPage", typeof(InquiryAboutMyReportsPage));
            navigationService.Configure("InquiryAboutAddOrShowReportsPage", typeof(InquiryAboutAddOrShowReportsPage));
            navigationService.Configure("ContactUs", typeof(ContactUs));
            navigationService.Configure("NewDeclarationPage", typeof(NewDeclarationPage));
            navigationService.Configure("ChooseQuestionsPage", typeof(ChooseQuestionsPage));
            navigationService.Configure("ProductDeclarationPage", typeof(ProductDeclarationPage));
            navigationService.Configure("TransactionReceptionView", typeof(TransactionReceptionView));
            navigationService.Configure("SuccessView", typeof(SuccessView));
            navigationService.Configure("IAMLoginView", typeof(IAMLoginView));
            navigationService.Configure("PassengerInformationPage", typeof(PassengerInformationPage));
            navigationService.Configure("ReviewRequestPage", typeof(ReviewRequestPage));
            navigationService.Configure("TripInformationPage", typeof(TripInformationPage));
            navigationService.Configure("ListUserRequestsPage", typeof(ListUserRequestsPage));
            navigationService.Configure("ContactInformationPage", typeof(ContactInformationPage));
            navigationService.Configure("EDeclarationSuccessPage", typeof(EDeclarationSuccessPage));
            navigationService.Configure("EDeclarationPaymentPage", typeof(EDeclarationPaymentPage));
            navigationService.Configure("RegisterZATCAUserPage", typeof(RegisterZATCAUserPage));
            navigationService.Configure("EDeclarationPage", typeof(EDeclarationPage));
            navigationService.Configure("TrackShipmentPage", typeof(TrackShipmentPage));
            navigationService.Configure("PaymentWebView", typeof(PaymentWebView));
            navigationService.Configure("AboutZakatyView", typeof(AboutZakatyView));
            navigationService.Configure("NativeNafathPage", typeof(NativeNafathPage));
            navigationService.Configure("NativeConfirmNafathPage", typeof(NativeConfirmNafathPage));

            navigationService.Configure("CustomFeesFormView", typeof(CustomFeesFormView));
            navigationService.Configure("ChatPotView", typeof(ChatPotView));
            navigationService.Configure("ShipmentTrackingTypesPage", typeof(ShipmentTrackingTypesPage));
            navigationService.Configure("ShipmentStatusPage", typeof(ShipmentStatusPage));
            navigationService.Configure("UploadingPopup", typeof(UploadingPopup));
            //CR6094
            navigationService.Configure(App.NafathPopUpPage, typeof(NafathPopUpPage));
            navigationService.Configure(App.NafathLoginPageView, typeof(NafathLoginPageView));
            navigationService.Configure(App.NewYesorNoPageView, typeof(NewYesorNoPageView));//Cr6264
            navigationService.Configure("FasahLoginView", typeof(FasahLoginView));
            navigationService.Configure("InquiryaboutCustomsIssuesView", typeof(InquiryaboutCustomsIssuesView));

            //CR6003
            navigationService.Configure(App.ChangeMobileRequestPageView, typeof(ChangeMobileRequestPageView));
            navigationService.Configure(App.ChangeMobNafathLoginPage, typeof(ChangeMobNafathLoginPage));
            navigationService.Configure(App.UpdateManagerDetailsPopUp, typeof(UpdateManagerDetailsPopUp));

            #endregion

            return navigationService;
        }

        #endregion

        public InquiryAboutCustomsDeclarationViewModel InquiryAboutCustomsDeclarationViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<InquiryAboutCustomsDeclarationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public TraifSectionsViewModel traifSectionsViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TraifSectionsViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public ReportFinancialViolationViewModel reportFinancialViolationViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ReportFinancialViolationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public ReportsMenuViewModel reportsMenuViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ReportsMenuViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public LaboratoryPaymentOfInsuranceFeesViewModel LaboratoryPaymentOfInsuranceFeesViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<LaboratoryPaymentOfInsuranceFeesViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public ExciseTaxViewModel exciseTaxViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ExciseTaxViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public SearchIndiactivePriceForExciseGoodsViewModel searchIndiactivePriceForExciseGoodsViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SearchIndiactivePriceForExciseGoodsViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public TahqaqScanPageViewModel tahqaqScanPageViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TahqaqScanPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public TaxCalculatorViewModel taxCalculatorViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxCalculatorViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public E_DeclerationViewModel eDeclerationViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<E_DeclerationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public HomeViewModel homeViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<HomeViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public RateUsViewModel rateUsViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<RateUsViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public CustomLoginViewModel CustomLoginViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CustomLoginViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public TransactionReceptionViewModel TransactionReceptionViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TransactionReceptionViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public IAMLoginViewModel IAMLoginViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<IAMLoginViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public EDeclerationViewModel EDeclerationViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<EDeclerationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public CustomsPaymentViewModel CustomsPaymentViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CustomsPaymentViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public AboutZakatyViewModel AboutZakatyViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AboutZakatyViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public CustomServiceMenuViewModel CustomServiceMenuViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CustomServiceMenuViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ChatViewModel ChatViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChatViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public CustomFeesFormViewModel CustomFeesFormViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CustomFeesFormViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public FasahLoginViewModel FasahLoginViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<FasahLoginViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public InquiryaboutCustomsIssuesViewModel InquiryaboutCustomsIssuesViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<InquiryaboutCustomsIssuesViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #region Release2 FileUpload

        public FilesUploadPopUpViewModel FilesUploadPopUpView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<FilesUploadPopUpViewModel>();
                    SimpleIoc.Default.Register<FilesUploadPopUpViewModel>();
                    return ServiceLocator.Current.GetInstance<FilesUploadPopUpViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #endregion

        #region Vatprofit on goods CR6264

        public NewYesorNoPageViewModel NewYesorNoView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<NewYesorNoPageViewModel>();
                    SimpleIoc.Default.Register<NewYesorNoPageViewModel>();
                    return ServiceLocator.Current.GetInstance<NewYesorNoPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #endregion

        #region Release2 InstalmentPlan

        public InstalmentPlanViewModel InstalmentPlanPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<InstalmentPlanViewModel>();
                    SimpleIoc.Default.Register<InstalmentPlanViewModel>();
                    return ServiceLocator.Current.GetInstance<InstalmentPlanViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #endregion

        #region Release2 VATInstalmentPlan

        public VATInstalmentPlanViewModel VatInstalmentPlanPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATInstalmentPlanViewModel>();
                    SimpleIoc.Default.Register<VATInstalmentPlanViewModel>();
                    return ServiceLocator.Current.GetInstance<VATInstalmentPlanViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATInstalmentPlanViewModel VatInstalmentPlanSuccessPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATInstalmentPlanViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }


        public VATInstalmentPlanListViewModel VatInstalmentPlanListPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VATInstalmentPlanListViewModel>();
                    SimpleIoc.Default.Register<VATInstalmentPlanListViewModel>();
                    return ServiceLocator.Current.GetInstance<VATInstalmentPlanListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #endregion

        public InstructionsBottomPopUpViewModel InstructionsBottomPopUpView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<InstructionsBottomPopUpViewModel>();
                    SimpleIoc.Default.Register<InstructionsBottomPopUpViewModel>();
                    return ServiceLocator.Current.GetInstance<InstructionsBottomPopUpViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public MorePopUpViewModelRTwo MoreMenuPopUpPageViewRTwo
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MorePopUpViewModelRTwo>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public MyBillsMultiplePayableListViewModel MyBillsMultiplePayableList
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MyBillsMultiplePayableListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public AddNotePopUpViewModel AddNotesPopupPageViewModel
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<AddNotePopUpViewModel>();
                    SimpleIoc.Default.Register<AddNotePopUpViewModel>();
                    return ServiceLocator.Current.GetInstance<AddNotePopUpViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ViewNotePopUpViewModel ViewNotePopUpViewModel
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ViewNotePopUpViewModel>();
                    SimpleIoc.Default.Register<ViewNotePopUpViewModel>();
                    return ServiceLocator.Current.GetInstance<ViewNotePopUpViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #region Release2 ZakatInstalmentPlan

        public ZakatInstalmentPlanViewModel ZakatInstalmentPlanPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatInstalmentPlanViewModel>();
                    SimpleIoc.Default.Register<ZakatInstalmentPlanViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatInstalmentPlanViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatInstalmentPlanViewModel ZakatInstalmentPlanSuccessPageView
        {
            get
            {
                try
                {

                    return ServiceLocator.Current.GetInstance<ZakatInstalmentPlanViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatInstalmentPlanListViewModel ZakatInstalmentPlanListPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatInstalmentPlanListViewModel>();
                    SimpleIoc.Default.Register<ZakatInstalmentPlanListViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatInstalmentPlanListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public OldZakatInstalmentPlanViewModel OldZakatInstalmentPlanPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<OldZakatInstalmentPlanViewModel>();
                    SimpleIoc.Default.Register<OldZakatInstalmentPlanViewModel>();
                    return ServiceLocator.Current.GetInstance<OldZakatInstalmentPlanViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public OldZakatInstalmentPlanViewModel OldZakatInstalmentPlanSuccessPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<OldZakatInstalmentPlanViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public OldZakatInstalmentPlanListViewModel OldZakatInstalmentPlanListPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<OldZakatInstalmentPlanListViewModel>();
                    SimpleIoc.Default.Register<OldZakatInstalmentPlanListViewModel>();
                    return ServiceLocator.Current.GetInstance<OldZakatInstalmentPlanListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }



        #endregion
        #region Contract Release

        public ContractReleaseViewModel ContractReleasePageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ContractReleaseViewModel>();
                    SimpleIoc.Default.Register<ContractReleaseViewModel>();

                    return ServiceLocator.Current.GetInstance<ContractReleaseViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ContractReleaseListViewModel ContractReleasePageListView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ContractReleaseListViewModel>();
                    SimpleIoc.Default.Register<ContractReleaseListViewModel>();
                    return ServiceLocator.Current.GetInstance<ContractReleaseListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #endregion

        #region ChnageFillingPeriod

        public ChangeFillingPeriodViewModel ChangeFillingPeriodPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ChangeFillingPeriodViewModel>();
                    SimpleIoc.Default.Register<ChangeFillingPeriodViewModel>();
                    return ServiceLocator.Current.GetInstance<ChangeFillingPeriodViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ChangeFillingPeriodViewModel ChangeFillingPeriodSuccessPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChangeFillingPeriodViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ChangeFillingPeriodListViewModel ChangeFillingPeriodListPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ChangeFillingPeriodListViewModel>();
                    SimpleIoc.Default.Register<ChangeFillingPeriodListViewModel>();
                    return ServiceLocator.Current.GetInstance<ChangeFillingPeriodListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #region VatReview
        public VatReviewViewModel VatReviewView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VatReviewViewModel>();
                    SimpleIoc.Default.Register<VatReviewViewModel>();
                    return ServiceLocator.Current.GetInstance<VatReviewViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VatReviewViewModel VatReviewSuccessView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VatReviewViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public VatReviewListViewModel VatReviewListView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<VatReviewListViewModel>();
                    SimpleIoc.Default.Register<VatReviewListViewModel>();
                    return ServiceLocator.Current.GetInstance<VatReviewListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }


        public ObjectionViewModel ObjectionsSelectionPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ObjectionViewModel>();
                    SimpleIoc.Default.Register<ObjectionViewModel>();
                    return ServiceLocator.Current.GetInstance<ObjectionViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }




        #endregion



        #endregion


        #region ZakatObjection



        public ZakatObjectionsListViewModel ZakatObjectionListView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatObjectionsListViewModel>();
                    SimpleIoc.Default.Register<ZakatObjectionsListViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatObjectionsListViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatObjectionViewModel ZakatObjectionView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatObjectionViewModel>();
                    SimpleIoc.Default.Register<ZakatObjectionViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatObjectionViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ZakatObjectionViewModel ZakatObjectionSuccessView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZakatObjectionViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        #endregion

        public ZakatDeregistrationPageViewModel ZakatDeregistrationPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatDeregistrationPageViewModel>();
                    SimpleIoc.Default.Register<ZakatDeregistrationPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatDeregistrationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public TaxEvasionReportDetailPageViewModel TaxEvasionReportDetailPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<TaxEvasionReportDetailPageViewModel>();
                    SimpleIoc.Default.Register<TaxEvasionReportDetailPageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportDetailPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public TINDeregistrationPageViewModel TINDeregistrationPageView
        {
            get
            {
                try
                {

                    SimpleIoc.Default.Unregister<TINDeregistrationPageViewModel>();
                    SimpleIoc.Default.Register<TINDeregistrationPageViewModel>();

                    return ServiceLocator.Current.GetInstance<TINDeregistrationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public CalendarPickerPageViewModel CalendarPickerPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<CalendarPickerPageViewModel>();
                    SimpleIoc.Default.Register<CalendarPickerPageViewModel>();
                    return ServiceLocator.Current.GetInstance<CalendarPickerPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public PickerPageViewModel PickerPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<PickerPageViewModel>();
                    SimpleIoc.Default.Register<PickerPageViewModel>();
                    return ServiceLocator.Current.GetInstance<PickerPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public ZakatRegistrationDetailsListPageViewModel ZakatRegistrationDetailsListPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatRegistrationDetailsListPageViewModel>();
                    SimpleIoc.Default.Register<ZakatRegistrationDetailsListPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationDetailsListPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public GeneralServicesViewModel GeneralServicesListView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<GeneralServicesViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatRegistrationTaxPayerDetailsPageViewModel ZakatRegistrationTaxPayerDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatRegistrationTaxPayerDetailsPageViewModel>();
                    SimpleIoc.Default.Register<ZakatRegistrationTaxPayerDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationTaxPayerDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatRegistrationOutletsDetailsPageViewModel ZakatRegistrationOutletsDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatRegistrationOutletsDetailsPageViewModel>();
                    SimpleIoc.Default.Register<ZakatRegistrationOutletsDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationOutletsDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ZakatRegistrationFinancialDetailsPageViewModel ZakatRegistrationFinancialDetailsPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ZakatRegistrationFinancialDetailsPageViewModel>();
                    SimpleIoc.Default.Register<ZakatRegistrationFinancialDetailsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationFinancialDetailsPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VATReturnSuccessfullPageViewModel VATReturnSuccessfullPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATReturnSuccessfullPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public VatReturnNewSuccessViewModel VatReturnNewSuccessView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VatReturnNewSuccessViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public MorePopUpPageViewModel MorePopUpPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MorePopUpPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public ShowVatInformationConfirmationPageViewModel ShowVatInformationConfirmationPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ShowVatInformationConfirmationPageViewModel>();
                    SimpleIoc.Default.Register<ShowVatInformationConfirmationPageViewModel>();
                    return ServiceLocator.Current.GetInstance<ShowVatInformationConfirmationPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }


        public RefundAccountPopupPageViewModel RefundAccountPopupPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<RefundAccountPopupPageViewModel>();
                    SimpleIoc.Default.Register<RefundAccountPopupPageViewModel>();
                    return ServiceLocator.Current.GetInstance<RefundAccountPopupPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        public NewAccountPopPageViewModel NewAccountPopPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<NewAccountPopPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        public SubmitReportViewModel SubmitReportViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SubmitReportViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public MyReportsViewModel MyReportsViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MyReportsViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public LiveVideoViewModel LiveVideoViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<LiveVideoViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ReportOTPViewModel ReportOTPViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ReportOTPViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public BaseEDeclarationViewModel BaseEDeclarationViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<BaseEDeclarationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public BaseProductDeclarationViewModel ProductDeclarationViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<BaseProductDeclarationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public EDeclarationInformationsViewModel EDeclarationInformationsViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<EDeclarationInformationsViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public RegisterZATCAUserViewModel RegisterZATCAUserViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<RegisterZATCAUserViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ReviewRequestViewModel ReviewRequestViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ReviewRequestViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public EDeclarationPaymentViewModel EDeclarationPaymentViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<EDeclarationPaymentViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public EDeclerationSubmitModel EDeclerationSubmitModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<EDeclerationSubmitModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public TrackShipmentViewModel TrackShipmentViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TrackShipmentViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public StateManager StateManager
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<StateManager>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public UploadingPopupViewModel UploadingPopupViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UploadingPopupViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ListUserRequestsViewModel ListUserRequestsViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ListUserRequestsViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ChangeMobileRequestViewModel ChangeMobileRequestPageView
        {
            get
            {
                try
                {
                    SimpleIoc.Default.Unregister<ChangeMobileRequestViewModel>();
                    SimpleIoc.Default.Register<ChangeMobileRequestViewModel>();
                    return ServiceLocator.Current.GetInstance<ChangeMobileRequestViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    return null;
                }
            }
        }
        public ChangeMobNafathPageViewMode ChangeMobNafathLoginPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChangeMobNafathPageViewMode>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    return null;
                }
            }
        }
        public UpdateManagerViewModel UpdateManagerPopUp
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UpdateManagerViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    return null;
                }
            }
        }
        public BankAccountManagementPageViewModel BankAccountManagementPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<BankAccountManagementPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public BankAccountAddorUpdateIBANViewModel BankAccountAddOrUpdatePageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<BankAccountAddorUpdateIBANViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        //
    }


}

