using CommonServiceLocator;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.ViewModel.NewDesignViewModel.OnBoardingAnimation;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
#region OldUsing
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AboutUsPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AccountCreatedPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AcknowledgementDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AddNotePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AddPopPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AmendSalesDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.AttachmentPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.CreateGaztAccountPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.CreditCarriedPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.DisplayNotesPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.FAQPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ICRListPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.InternationalMobileNumber;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyBills_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyReturnsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.Pdf_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.PrivacyAndPolicyPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ReturnsListCountsByStatus_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SalesDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLoginPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SignUpFormPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SignUpTAndCPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.StylesTestUi;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionFormPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionRegistrationPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportListPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportTypePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATLookupPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;

using EGAZT.Views.NewDesign.DashBoardPages;
using EGAZT.Views.NewDesign.ForgotPasswordPages;
using EGAZT.Views.NewDesign.MyBillsPages;
using EGAZT.Views.NewDesign.MyReturnsNewPages;
using EGAZT.Views.NewDesign.OnboardingPages;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using EGAZT.Views.SyncFusionEnabledViews.AboutUs;
using EGAZT.Views.SyncFusionEnabledViews.AcknowledgementDetails;
using EGAZT.Views.SyncFusionEnabledViews.AddNote;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.AmendSalesDetails;
using EGAZT.Views.SyncFusionEnabledViews.AttachmentPage;
//using EGAZT.Views.SyncFusionEnabledViews.CheckTINStatus;
using EGAZT.Views.SyncFusionEnabledViews.ContactUsPage;
//using EGAZT.Views.SyncFusionEnabledViews.Correspondance;
//using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
//using EGAZT.Views.SyncFusionEnabledViews.CreateGaztAccount;
using EGAZT.Views.SyncFusionEnabledViews.CreditCarried;
using EGAZT.Views.SyncFusionEnabledViews.DisplayNotes;
using EGAZT.Views.SyncFusionEnabledViews.FAQPage;
using EGAZT.Views.SyncFusionEnabledViews.ICRList;
using EGAZT.Views.SyncFusionEnabledViews.InternationalMobileNumber;
using EGAZT.Views.SyncFusionEnabledViews.PdfView;
using EGAZT.Views.SyncFusionEnabledViews.PrivacyAndPolicy;
using EGAZT.Views.SyncFusionEnabledViews.SalesDetailsView;
using EGAZT.Views.SyncFusionEnabledViews.SFLogin;
using EGAZT.Views.SyncFusionEnabledViews.StylesTestUi;
using EGAZT.Views.SyncFusionEnabledViews.UnlockAccount;
using EGAZT.Views.SyncFusionEnabledViews.VATDeclarationPagesEX;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using EGAZT.Views.SyncFusionEnabledViews.ZakatReturnDetails_View;
using EGAZT.Views.SyncFusionEnabledViews.ZakatReturnList;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GAZT;
using System;

#endregion
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using EGAZT.Views.NewDesign.VATLookUp;
using EGAZT.Views.NewDesign.TaxpayersCertificatesPages;
using EGAZT.Views.NewDesign.ZakatForm5;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.TaxpayerCorrespondancePages;
using EGAZT.Views.NewDesign.VATDeRegistration;
using EGAZT.Views.NewDesign.ZAKATObjectionPages;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using EGAZT.Views.NewDesign.ZakatDeregistration;
using EGAZT.ViewModel.NewDesignViewModel.CalendarPickerPageViewModel;
using EGAZT.ViewModel.NewDesignViewModel.GenericPickers;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using EGAZT.Views.NewDesign.ZakatInstalmentPlan;
using EGAZT.Views.NewDesign.VatInstalmentPlan;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using EGAZT.Views.NewDesign.TAXEvasionPages;
using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionPageViewModel;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;
using EGAZT.Views.NewDesign.VATRefunds;
using EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM;
using EGAZT.Views.NewDesign.TaxpayerProfile;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using EGAZT.ViewModel.NewDesignViewModel.ContractRelease;
using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using EGAZT.ViewModel.NewDesignViewModel.InstalmentPlanViewModel;
using EGAZT.Views.NewDesign.InstalmentPlan;
using EGAZT.Views.NewDesign.EstablishmentRegistrationPages;
using EGAZT.Views.NewDesign.EstablishmentSignUP;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM;
using EGAZT.Views.NewDesign.Template;
using EGAZT.Views.NewDesign.ContractReleasePages;
using EGAZT.Views.NewDesign.ChangeFillingPeriodPages;
using EGAZT.Views.NewDesign;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using EGAZT.Views.NewDesign.VatReview;
using EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using EGAZT.Views.NewDesign.ZakatObjection;
using EGAZT.Views.NewDesign.VATRegistrationDetails;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.VATReview;
using EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using EGAZT.ViewModel.NewDesignViewModel.VATAmendReactivationPageViewModel;
using EGAZT.Views.NewDesign.VATAmendReactivationPages;
using EGAZT.ViewModel.NewDesignViewModel.VATAmendReactivationSuccessPageViewModel;
using EGAZT.ViewModel.NewDesignViewModel.AccountStatements;
using EGAZT.Views.NewDesign.AccountStatements;
using EGAZT.Views.NewDesign.EstablishmentAmendUpdatePages;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using EGAZT.ViewModel.NewDesignViewModel.Common;
using EGAZT.ViewModel.NewDesignViewModel.VATRefunds;

using Xamarin.Forms.Internals;
using EGAZT.Views.NewDesign.PaymentOptions;
using EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using EGAZT.Views.NewDesign.CustomServicesPages;
using EGAZT.Services.Interface;
using EGAZT.Services.Classes;
using EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels;
using EGAZT.Views.NewDesign.TahqaqViews;
using EGAZT.Views.NewDesign.VAT;
using EGAZT.ViewModel.NewDesignViewModel.VAT;
using EGAZT.ViewModel.NewDesignViewModel.SubmitReport;
using EGAZT.Views.NewDesign.SubmitReport;
using EGAZT.Controls;

using EGAZT.ViewModel.NewDesignViewModel.MyReportsVM;
using EGAZT.Views.NewDesign.MyReports;

using EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations;
using EGAZT.ViewModel.NewDesignViewModel.HomeViewModels;
using EGAZT.Views.NewDesign.HomePages;
using EGAZT.ViewModel.NewDesignViewModel.LiveVideoVM;
using EGAZT.Views.NewDesign.LiveVideo;
using EGAZT.Views.NewDesign.Survey;
using EGAZT.ViewModel.NewDesignViewModel.SurveyViewModels;
using EGAZT.ViewModel.NewDesignViewModel.LoginViewModels;
using EGAZT.Views.NewDesign.LoginPages;
using EGAZT.Views.NewDesign.CustomServicesPages.CustomDashBoard;
using EGAZT.ViewModel.NewDesignViewModel.ReportOTPVM;
using EGAZT.Views.NewDesign.ReportOTP;
using System.Net.Http;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using EGAZT.Views.NewDesign.EDeclaration;
using EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;
using EGAZT.Helper;
using EGAZT.ViewModel.NewDesignViewModel.ZakatyViewModels;
using EGAZT.Views.NewDesign.Zakaty;

namespace EGAZT
{
    [Preserve(AllMembers = true)]
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
            SimpleIoc.Default.Register<EDeclerationSubmitModel>();

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
            // SimpleIoc.Default.Register<StyleTestUIPageViewModel>();

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

            SimpleIoc.Default.Register<E_DeclerationViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<RateUsViewModel>();
            SimpleIoc.Default.Register<CustomLoginViewModel>();
            SimpleIoc.Default.Register<BaseEDeclarationViewModel>();
            SimpleIoc.Default.Register<ProductDeclarationViewModel>();
            SimpleIoc.Default.Register<EDeclarationInformationsViewModel>();
            SimpleIoc.Default.Register<EDeclarationPaymentViewModel>();
            SimpleIoc.Default.Register<TransactionReceptionViewModel>();
            SimpleIoc.Default.Register<IAMLoginViewModel>();
            SimpleIoc.Default.Register<ReviewRequestViewModel>();
            SimpleIoc.Default.Register<EDeclerationViewModel>();
            SimpleIoc.Default.Register<CustomsPaymentViewModel>();
            SimpleIoc.Default.Register<StateManager>();
            SimpleIoc.Default.Register<AboutZakatyViewModel>();
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    SimpleIoc.Default.Unregister<TaxpayerSubsidyViewModel>();
                    SimpleIoc.Default.Register<TaxpayerSubsidyViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxpayerSubsidyViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    SimpleIoc.Default.Unregister<TaxpayerCorrespondancePageViewModel>();
                    SimpleIoc.Default.Register<TaxpayerCorrespondancePageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxpayerCorrespondancePageViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    SimpleIoc.Default.Unregister<TaxpayerCorrespondanceDetailPageViewModel>();
                    SimpleIoc.Default.Register<TaxpayerCorrespondanceDetailPageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxpayerCorrespondanceDetailPageViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    SimpleIoc.Default.Unregister<TaxpayersCertificatesPageViewModel>();
                    SimpleIoc.Default.Register<TaxpayersCertificatesPageViewModel>();
                    return ServiceLocator.Current.GetInstance<TaxpayersCertificatesPageViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    SimpleIoc.Default.Unregister<GAZTNewDesignVATReturnUpdatedUIPageViewModel>();
                    SimpleIoc.Default.Register<GAZTNewDesignVATReturnUpdatedUIPageViewModel>();
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignVATReturnUpdatedUIPageViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    SimpleIoc.Default.Unregister<GAZTNewDesignMyBillsPageViewModel>();
                    SimpleIoc.Default.Register<GAZTNewDesignMyBillsPageViewModel>();
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignMyBillsPageViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    return null;
                }
            }
        }
        #endregion

        //private INavigationService CreateNavigationService()
        //{
        //    var navigationService = new NavigationService();

        //    #region NewDesign

        //    navigationService.Configure(App.GAZTNewDesignOnBoardingAnimationPageView, typeof(GAZTNewDesignOnBoardingAnimationPageView));
        //    navigationService.Configure(App.GAZTNewDesignVATReturnUpdatedUIPageView, typeof(GAZTNewDesignVATReturnUpdatedUIPageView));
        //    navigationService.Configure(App.GAZTNewDesignStyleTestUIPageView, typeof(StyleTestUIPageView));
        //    navigationService.Configure(App.ForgotPasswordPageView, typeof(GAZTNewDesignForgotPasswordPageView));
        //    navigationService.Configure(App.GAZTNewDesignMyBillsPageView, typeof(GAZTNewDesignMyBillsPageView));
        //    navigationService.Configure(App.GAZTNewDesignDashBoardPageView, typeof(GAZTNewDesignDashBoardPageView));
        //    navigationService.Configure(App.GAZTNewDesignMyReturnsNewPageView, typeof(GAZTNewDesignMyReturnsNewPageView));
        //    //navigationService.Configure(App.GAZTNewDesignStyleTestUIPage, typeof(StyleTestUIPageViewModel));
        //    //navigationService.Configure(App.GAZTNewDesignStyleTestUIPageView, typeof(StyleTestUIPageViewModel));

        //    #endregion

        //    #region SYNCFUSION INTEGRATION
        //    navigationService.Configure(App.SFLandingPageView, typeof(SFLandingPageView));
        //    navigationService.Configure(App.SFOptionsPageView, typeof(SFOptionsPageView));
        //    navigationService.Configure(App.SFLoginPageView, typeof(SFLoginPageView));
        //    navigationService.Configure(App.SFAnonymousLandingPageView, typeof(SFAnonymousLandingPageView));
        //    navigationService.Configure(App.MyCertificate, typeof(MyCertificate));
        //    navigationService.Configure(App.PdfView, typeof(PdfView));
        //    navigationService.Configure(App.MyBillsView, typeof(MyBillsView));
        //    navigationService.Configure(App.TaxPayerProfilePageView, typeof(TaxPayerProfilePageView));
        //    navigationService.Configure(App.ChangeMobileNumberPageView, typeof(ChangeMobileNumberPageView));
        //    navigationService.Configure(App.ChangeEmailPageView, typeof(ChangeEmailPageView));
        //    navigationService.Configure(App.UpdateEmailVerificationPage, typeof(UpdateEmailVerificationPage));
        //    navigationService.Configure(App.ChangePasswordPageView, typeof(ChangePasswordPageView));
        //    navigationService.Configure(App.OTPPageView, typeof(OTPPageView));
        //    navigationService.Configure(App.ForgotUsernamePasswordPageView, typeof(ForgotUsernamePasswordPageView));
        //    navigationService.Configure(App.VATLookupPageView, typeof(VATLookupPageView));
        //    navigationService.Configure(App.ZakatReturnListPageView, typeof(ZakatReturnListPageView));
        //    navigationService.Configure(App.ZakatReturnDetailsPageView, typeof(ZakatReturnDetailsPageView));
        //    navigationService.Configure(App.BillDetailsPageView, typeof(BillDetailsPageView));
        //    navigationService.Configure(App.SalesDetailsPageView, typeof(SalesDetailsPageView));
        //    navigationService.Configure(App.AmendSalesDetailsPageView, typeof(AmendSalesDetailsPageView));
        //    navigationService.Configure(App.CheckTINStatusPageView, typeof(CheckTINStatusPageView));
        //    navigationService.Configure(App.ICRListPageView, typeof(ICRListPageView));
        //    navigationService.Configure(App.VATReturnsPageView, typeof(VATReturnsPageView));
        //    navigationService.Configure(App.VATReturnsPageViewEX, typeof(VATReturnsPageViewEX));
        //    navigationService.Configure(App.AAcknowledgementView, typeof(AAcknowledgementView));
        //    navigationService.Configure(App.AcknowledgementDetailsPageView, typeof(AcknowledgementDetailsPageView));
        //    navigationService.Configure(App.DisplayNotesPageView, typeof(DisplayNotesPageView));
        //    navigationService.Configure(App.AttachmentPageView, typeof(AttachmentPageView));
        //    navigationService.Configure(App.AddNotePageView, typeof(AddNotePageView));
        //    navigationService.Configure(App.AddPopPageView, typeof(AddPopPageView));
        //    navigationService.Configure(App.CreditCarriedPageView, typeof(CreditCarriedPageView));
        //    navigationService.Configure(App.CorrespondancePageView, typeof(CorrespondancePageView));
        //    navigationService.Configure(App.CorrespondenceDetailsPageView, typeof(CorrespondenceDetailsPageView));
        //    navigationService.Configure(App.FormBundleStatusPageView, typeof(FormBundleStatusPageView));
        //    navigationService.Configure(App.SignUpTAndCViewPage, typeof(SignUpTAndCViewPage));
        //    navigationService.Configure(App.SignUpFormPageView, typeof(SignUpFormPageView));
        //    navigationService.Configure(App.CreditCarriedPageView, typeof(CreditCarriedPageView));
        //    navigationService.Configure(App.TaxEvasionRegistrationPageView, typeof(TaxEvasionRegistrationPageView));
        //    navigationService.Configure(App.TaxEvasionReportTypePageView, typeof(TaxEvasionReportTypePageView));
        //    navigationService.Configure(App.TaxEvasionReportFormPageView, typeof(TaxEvasionReportFormPageView));
        //    navigationService.Configure(App.CreateGaztAccountPageView, typeof(CreateGaztAccountPageView));
        //    navigationService.Configure(App.AccountCreatedPageView, typeof(AccountCreatedPageView));
        //    navigationService.Configure(App.TaxEvasionReportListPageView, typeof(TaxEvasionReportListPageView));
        //    navigationService.Configure(App.TaxEvasionReportMobilePageView, typeof(TaxEvasionReportMobilePageView));
        //    navigationService.Configure(App.ReturnsPageView, typeof(ReturnsPageView));
        //    navigationService.Configure(App.FAQPageView, typeof(FAQPageView));
        //    navigationService.Configure(App.AboutUsPageView, typeof(AboutUsPageView));
        //    navigationService.Configure(App.PrivacyAndPolicyPageView, typeof(PrivacyAndPolicyPageView));
        //    navigationService.Configure(App.MyReturnsPageView, typeof(MyReturnsPageView));
        //    navigationService.Configure(App.MyCommitmentsPageView, typeof(MyCommitmentsPageView));
        //    navigationService.Configure(App.ContactUsPageView, typeof(ContactUsPageView));
        //    navigationService.Configure(App.VATIndividualSignupPageView, typeof(VATIndividualSignupPageView));
        //    navigationService.Configure(App.IndividualRegistrationPageView, typeof(IndividualRegistrationPageView));
        //    navigationService.Configure(App.RegistrationSuccessfulPageView, typeof(RegistrationSuccessfulPageView));
        //    navigationService.Configure(App.VATRegistrationPageView, typeof(VATRegistrationPageView));
        //    navigationService.Configure(App.VATRegistrationSuccessfullPageView, typeof(VATRegistrationSuccessfullPageView));
        //    navigationService.Configure(App.VATRealEstateServicesPageView, typeof(VATRealEstateServicesPage));
        //    navigationService.Configure(App.PropertyRegistrationPage, typeof(PropertyRegistrationPage));
        //    navigationService.Configure(App.FileAttachmentPopUpPageView, typeof(FileAttachmentPopUpPageView));
        //    navigationService.Configure(App.TaxEvasionFormPage, typeof(TaxEvasionFormPage));
        //    navigationService.Configure(App.TaxEvasionAttachmentPageView, typeof(TaxEvasionAttachmentPageView));
        //    navigationService.Configure(App.VATIndividualSignupTnCPageView, typeof(VATIndividualSignupTnCPageView));
        //    navigationService.Configure(App.FinancialDetailAttachmentPopupPageView, typeof(FinancialDetailAttachmentPopupPageView));
        //    #endregion

        //    SimpleIoc.Default.Register<InternationalCodeSearchPageViewModel>();
        //    SimpleIoc.Default.Register<UnlockAccountTINPageViewModel>();
        //    SimpleIoc.Default.Register<UnlockAccountSuccessPageViewModel>();

        //    return navigationService;
        //}

        #region OldDesignViewModel
        public StyleTestUIPageViewModel StyleTestUIPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<StyleTestUIPageViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                    SimpleIoc.Default.Unregister<ZAKATReturnDetailsViewModel>();
                    SimpleIoc.Default.Register<ZAKATReturnDetailsViewModel>();
                    return ServiceLocator.Current.GetInstance<ZAKATReturnDetailsViewModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
            navigationService.Configure(App.NewTaxEvasionFormPageView, typeof(Views.NewDesign.TAXEvasionPages.NewTaxEvasionFormPageView));
            navigationService.Configure(App.NewTaxEvasionFormSuccessPaveView, typeof(Views.NewDesign.TAXEvasionPages.NewTaxEvasionFormSuccessPaveView));
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
            navigationService.Configure(App.VATServicesPageView, typeof(Views.NewDesign.VATServices.VATServicesPageView));
            navigationService.Configure(App.TaxEvasionPageWebView, typeof(TaxEvasionPageWebView));
            navigationService.Configure(App.TaxpayerSubsidyRequest, typeof(TaxpayerSubsidyRequest));

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
            navigationService.Configure(App.FormBundleStatusPageView, typeof(EGAZT.Views.NewDesign.FormBundleStatusPages.FormBundleStatusPageView));
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
            navigationService.Configure("ProductDeclarationPage", typeof(ProductDeclarationPage));
            navigationService.Configure("TransactionReceptionView", typeof(TransactionReceptionView));
            navigationService.Configure("SuccessView", typeof(SuccessView));
            navigationService.Configure("IAMLoginView", typeof(IAMLoginView));
            navigationService.Configure("PassengerInformationPage", typeof(PassengerInformationPage));
            navigationService.Configure("ReviewRequestPage", typeof(ReviewRequestPage));
            navigationService.Configure("TripInformationPage", typeof(TripInformationPage));
            navigationService.Configure("ContactInformationPage", typeof(ContactInformationPage));
            navigationService.Configure("EDeclarationSuccessPage", typeof(EDeclarationSuccessPage));
            navigationService.Configure("EDeclarationPaymentPage", typeof(EDeclarationPaymentPage));
            navigationService.Configure("EDeclarationPage", typeof(EDeclarationPage));
            navigationService.Configure("PaymentWebView", typeof(PaymentWebView));
            navigationService.Configure("AboutZakatyView", typeof(AboutZakatyView));
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ProductDeclarationViewModel ProductDeclarationViewModel
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ProductDeclarationViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    //
    }


}
