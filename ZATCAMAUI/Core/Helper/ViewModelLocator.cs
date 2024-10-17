using CommunityToolkit.Mvvm.DependencyInjection;
using ZATCAMAUI.Controls;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Classes;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeNumber;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EscalatedCasesGSTCPageViewModel;
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
using ZATCAMAUI.ViewModel.NewDesignViewModel.UpdateVatEffectiveDateVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VAT;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATAmendReactivationPageViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATAmendReactivationSuccessPageViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATgoodsOnprofit;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATRefunds;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatExemptionRequestViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatRejectionPopUpViewModel;
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
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionRegistrationPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportFormPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportListPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportTypePage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TINOutletDeregister;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATLookupPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage;
using ZATCAMAUI.Views.NewDesign.AccountStatements;
using ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages;
using ZATCAMAUI.Views.NewDesign.ChangeMobile;
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
using ZATCAMAUI.Views.NewDesign.EscalatedCasesGSTC;
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
using ZATCAMAUI.Views.NewDesign.UpdateVatEffectiveDate;
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
using ZATCAMAUI.Views.NewDesign.ZakatExemptionRequest;
using ZATCAMAUI.Views.NewDesign.ZakatForm5;
using ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan;
using ZATCAMAUI.Views.NewDesign.ZakatObjection;
using ZATCAMAUI.Views.NewDesign.ZAKATObjectionPages;
using ZATCAMAUI.Views.NewDesign.ZakatRejectPopUp;
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
using ZATCAMAUI.Views.SyncFusionEnabledViews.TINOutletDeregister;
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

            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IDialogService>(new DialogService())
                .AddSingleton<ICustomInquiryService, CustomInquiryService>()
                .AddSingleton<ITraiffSectionsServices, TraiffSectionsServices>()
                .AddSingleton<IBalaghServices, BalaghServices>()
                .AddSingleton<IlaboratoryInsuranseFeesServices, laboratoryInsuranseFeesServices>()
                .AddSingleton<ICommonServices, CommonServices>()
                .AddSingleton<ITahqaqServices, TahqaqServices>()
                .AddSingleton<ITaxCalculatorServices, TaxCalculatorServices>()
                .AddSingleton<ISubmitReportServices, SubmitReportServices>()
                .AddSingleton<IMyReportsServices, MyReportsServices>()
                .AddSingleton<ISurveyServices, SurveyServices>()
                .AddSingleton<IUserServices, UserServices>()
                .AddSingleton<ITwareedServices, TwareedServices>()
                .AddSingleton<IE_DeclerationServices, E_DeclerationServices>()
                .AddSingleton<ITrackShipment, TrackShipmentServices>()
                .AddSingleton<INativeNafath, NativeNafathServices>()
                .AddSingleton<EDeclerationSubmitModel>()
                .AddSingleton<BankAccountManagementPageViewModel>()
                .AddSingleton<BankAccountAddorUpdateIBANViewModel>()
            #region NewDesignIOC
                .AddSingleton<GAZTNewDesignRecoverUsernameViewModel>()
                .AddSingleton<GAZTNewDesignRecoverPasswordPageViewModel>()

                .AddSingleton<GAZTNewDesignOnBoardingAnimationPageViewModel>()
                .AddSingleton<GAZTNewDesignVATReturnUpdatedUIPageViewModel>()
                .AddSingleton<GAZTNewDesignForgotPasswordPageViewModel>()
                .AddSingleton<GAZTNewDesignMyBillsPageViewModel>()
                .AddSingleton<GAZTNewDesignDashBoardPageViewModel>()
                .AddSingleton<GAZTNewDesignMyReturnsNewPageViewModel>()
                .AddSingleton<TaxpayerCorrespondancePageViewModel>()
                .AddSingleton<TaxpayerCorrespondanceDetailPageViewModel>()
                .AddSingleton<VATAmendReactivationSuccesssulPageViewModel>()
                .AddSingleton<TaxEvasionPageWebView>()
                .AddSingleton<ZakatDeregistrationPageViewModel>()
                .AddSingleton<TINDeregistrationPageViewModel>()
                .AddSingleton<VATLookUpNewPageViewModel>()
                .AddSingleton<VATDeRegistrationDetailsPageViewModel>()
                .AddSingleton<VATDeRegistrationInstructionsPageViewModel>()
                .AddSingleton<CalendarPickerPageViewModel>()
                .AddSingleton<PickerPageViewModel>()
                .AddSingleton<ZakatRegistrationDetailsListPageViewModel>()
                .AddSingleton<GeneralServicesViewModel>()
                .AddSingleton<TINDeregistrationCloseIndividualOutletsPageViewModel>()
                .AddSingleton<ZakatRegistrationOutletsDetailsPageViewModel>()
                .AddSingleton<ZakatRegistrationTaxPayerDetailsPageViewModel>()
                .AddSingleton<ZakatRegistrationFinancialDetailsPageViewModel>()

                .AddSingleton<VATDeregistrationSuccessPageViewModel>()

                .AddSingleton<ZakatForm5PageViewModel>()

                .AddSingleton<TaxEvasionVerifyMobileViewModel>()

                .AddSingleton<NewZakatObjectionPageViewModel>()
                .AddSingleton<VATReturnSuccessfullPageViewModel>()
                .AddSingleton<VatReturnNewSuccessViewModel>()
                .AddSingleton<EstablishmentRegistrationPageViewModel>()
                .AddSingleton<EstablishmentAmendUpdatePageViewModel>()
                .AddSingleton<OutletDetailsPageViewModel>()
                .AddSingleton<OutletDetailsAmendUpdatePageViewModel>()
                .AddSingleton<ActivityItemPageViewModel>()
                .AddSingleton<ActivityItemAmendUpdatePageViewModel>()
                .AddSingleton<RegistrationSuccessfulViewModel>()
                .AddSingleton<ZakatReturnDetailsSuccessfullPageViewModel>()
                .AddSingleton<ZakatReturnNewSuccessViewModel>()
                .AddSingleton<NewTaxEvasionFormPageViewModel>()
                .AddSingleton<GAZTNewDesignShowVatInformationPopUpPageViewModel>()
                .AddSingleton<ZakatObjectionSuccessfullPageViewModel>()
                .AddSingleton<DashboardAnonymousMenuPageViewModel>()
                .AddSingleton<VATCreditCarriedForwardPopUpPageViewModel>()
                .AddSingleton<SupportPageViewModel>()
                .AddSingleton<ZatcaInfoMenuPageViewModel>()

                .AddSingleton<NotesDescriptionPopUpPageViewModel>()
                .AddSingleton<NotesPopUpPageViewModel>()
                .AddSingleton<TaxManagementPageViewModel>()
                .AddSingleton<ViewModel.NewDesignViewModel.VATServicesPageViewModel.VATServicesPageViewModel>()
                .AddSingleton<TaxEvasionPageWebViewModel>()
                .AddSingleton<TaxpayerSubsidyViewModel>()
            #endregion

            #region NewDesignRelease2IOC
                .AddSingleton<ZakatInstalmentPlanViewModel>()
                .AddSingleton<ZakatInstalmentPlanListViewModel>()
                .AddSingleton<OldZakatInstalmentPlanViewModel>()
                .AddSingleton<OldZakatInstalmentPlanListViewModel>()
                .AddSingleton<ViewNotePopUpViewModel>()
                .AddSingleton<AddNotePopUpViewModel>()
                .AddSingleton<MyBillsMultiplePayableListViewModel>()

                .AddSingleton<MorePopUpViewModelRTwo>()
                .AddSingleton<InstalmentPlanViewModel>()
                .AddSingleton<VATInstalmentPlanViewModel>()
                .AddSingleton<VATInstalmentPlanListViewModel>()
                .AddSingleton<TaxEvasionMyReportsListPageViewModel>()
                .AddSingleton<TaxEvasionReportDetailPageViewModel>()
                .AddSingleton<ZakatAcknowledgmentPageViewModel>()
                .AddSingleton<ChangeFillingPeriodViewModel>()
                .AddSingleton<ContractReleaseViewModel>()
                .AddSingleton<FilesUploadPopUpViewModel>()
                .AddTransient<InstructionsBottomPopUpViewModel>()
                .AddSingleton<EstablishmentSignUPPageViewModel>()
                .AddSingleton<SignUpForEstablishmentPageViewModel>()
                .AddSingleton<AccountCreatedSuccessfullyPageViewModel>()
                .AddSingleton<ContractReleaseListViewModel>()
                .AddSingleton<ChangeFillingPeriodListViewModel>()
                .AddSingleton<ChangeFillingPeriodSuccessPage>()
                .AddSingleton<VatReviewViewModel>()
                .AddSingleton<VatReviewListViewModel>()
                .AddSingleton<ObjectionViewModel>()
                .AddSingleton<ZakatObjectionsListViewModel>()
                .AddSingleton<ZakatObjectionViewModel>()
                .AddSingleton<QuickActionPopUpPageViewModel>()
                .AddSingleton<VATDeclarationAttachmentPageViewModel>()
                .AddSingleton<VATRegistrationDisplayDetailsPageViewModel>()

                //CR6094
                .AddSingleton<EscalatedCasesGSTCPageViewModel>()
                .AddSingleton<RelationShipManagerInfoPageViewModel>()
                .AddSingleton<ZakatRejectionReasonPopupViewModel>()
                .AddSingleton<FilterVatEffectiveDatePageViewModel>()
                .AddSingleton<ZakatExemptionPageViewModel>()
                .AddSingleton<UpdateActivityInstructionsPageViewModel>()
                .AddSingleton<NafathChangeMobileNumberOptionsViewModel>()
                .AddSingleton<NafathChangeMobileNumberViewModel>()
                .AddSingleton<NafathLoginViewModel>()
                .AddSingleton<NafathAuthenticationViewModel>()
                .AddSingleton<NafathChangeMobileNumberOTPViewModel>()
                .AddSingleton<NafathChangeMobileNumberSuccessViewModel>()
                .AddSingleton<VATInstalmentNotesPageViewModel>()
                .AddSingleton<VATInstalmentPlanRevokeViewModel>()
                .AddSingleton<OTPPageViewModel>()
                .AddTransient<AttachmentViewModel>()
                .AddSingleton<AccountLockedViewModel>()

            #endregion

            #region PaymentImplementation
                .AddSingleton<PaymnetProcessWebviewViewModel>()

            #endregion

            #region OldIOC

                .AddSingleton<SFLoginPageViewModel>()
                .AddSingleton<PdfViewModel>()
                .AddSingleton<MyBillsViewModel>()
                .AddSingleton<VATLookupPageViewModel>()
                .AddSingleton<ZakatReturnListPageViewModel>()
                .AddSingleton<ZakatReturnDetailsPageViewModel>()
                .AddSingleton<SalesDetailsPageViewModel>()
                .AddSingleton<AmendSalesDetailsPageViewModel>()
                .AddSingleton<ICRListPageViewModel>()
                .AddSingleton<VATReturnsPageViewModelEX>()
                .AddSingleton<AcknowledgementDetailsPageViewModel>()
                .AddSingleton<DisplayNotesPageViewModel>()
                .AddSingleton<AttachmentPageViewModel>()
                .AddSingleton<AddNotePageViewModel>()
                .AddSingleton<AddPopPageViewModel>()
                .AddSingleton<CreditCarriedPageViewModel>()
                .AddSingleton<FormBundleStatusPageViewModel>()
                .AddSingleton<SignUpTAndCPageViewModel>()
                .AddSingleton<SignUpFormPageViewModel>()
                .AddSingleton<CreateGaztAccountPageViewModel>()
                .AddSingleton<TaxEvasionRegistrationViewModel>()
                .AddSingleton<TaxEvasionReportTypePageViewModel>()
                .AddSingleton<TaxEvasionReportFormPageViewModel>()
                .AddSingleton<TaxEvasionReportMobilePageViewModel>()
                .AddSingleton<TaxEvasionReportListPageViewModel>()
                .AddSingleton<AccountCreatedPageViewModel>()
                .AddSingleton<ReturnsPageViewModel>()
                .AddSingleton<FAQPageViewModel>()
                .AddSingleton<AboutUsPageViewModel>()
                .AddSingleton<PrivacyAndPolicyPageViewModel>()
                .AddSingleton<MyReturnsPageViewModel>()
                .AddSingleton<ContactUsPageViewModel>()
                .AddSingleton<TaxEvasionFormPageViewModel>()
                .AddSingleton<VATIndividualSignupPageViewModel>()
                .AddSingleton<IndividualRegistrationPageViewModel>()
                .AddSingleton<RegistrationSuccessfulPageViewModel>()
                .AddSingleton<EstablishmentAmendUpdateSuccessfulPageViewModel>()
                .AddSingleton<VATRegistrationPageViewModel>()
                .AddSingleton<VATAmendReactivationPageViewModel>()
                .AddSingleton<VATRegistrationSuccessfullPageViewModel>()
                .AddSingleton<InternationalMobileNumberCodePagesViewModel>()

                //AttachmentPopupPageView
                .AddSingleton<TaxEvasionReportAttachmentPageViewModel>()
                .AddSingleton<FileAttachmentPopUpPageViewModel>()
                .AddSingleton<VATIndividualSignupTnCPageViewModel>()
                .AddSingleton<FinancialDetailAttachmentPopupPageViewModel>()
                .AddSingleton<NewAccountPopUpPageViewModel>()
                .AddSingleton<NewAccountPopUpPageViewModel>()
                .AddSingleton<TaxpayersCertificatesPageViewModel>()
                .AddSingleton<ZAKATReturnDetailsViewModel>()
                .AddSingleton<TINDeregistrationPageView>()
                .AddSingleton<ZakatRegistrationDetailsListPageView>()

                .AddSingleton<InternationalCodeSearchPageViewModel>()
                .AddSingleton<UnlockAccountTINPageViewModel>()
                .AddSingleton<UnlockAccountSuccessPageViewModel>()
                .AddSingleton<TINDeregestrationSuccessPageViewModel>()
                .AddSingleton<TINDeregistrationCloseIndividualOutletsPageViewModel>()

                .AddSingleton<VATRefundListPageViewModel>()
                .AddSingleton<VATRefundDetailsPageViewModel>()
                .AddSingleton<VATRefundsNewRequestViewModel>()
                .AddSingleton<VATRefundsSuccessPageViewModel>()
                .AddSingleton<VATRefundsInstructionsPageViewModel>()

                .AddSingleton<AttachmentPopUpViewModel>()
                .AddSingleton<MorePopUpPageViewModel>()

                // * Taxpayer Profile
                .AddSingleton<NewTaxpayerProfileViewModel>()
                .AddSingleton<UpdateMobileViewModel>()
                .AddSingleton<UpdateEmailViewModel>()
                .AddSingleton<VerificationEmailPasswordViewModel>()
                .AddSingleton<UpdatePasswordViewModel>()
                .AddSingleton<TaxpayerProfileSuccessViewModel>()
                .AddSingleton<ShowVatInformationConfirmationPageViewModel>()
                .AddSingleton<RefundAccountPopupPageViewModel>()
                .AddSingleton<NewAccountPopPageViewModel>()

                //Account Statements
                //AccountStatementsPageView
                .AddSingleton<AccountStatementsPageViewModel>()

                .AddSingleton<AccountStatementBillsPageViewModel>()
                .AddSingleton<AccountStatementsFiltersPageViewModel>()
                .AddSingleton<AccountStatementsDownloadPageViewModel>()
                //Cr6264
                .AddSingleton<NewYesorNoPageViewModel>()
                //AccountStatementsDownloadPageView
                //
            #endregion

            #region Customs Service IoC
                .AddSingleton<InquiryAboutCustomsDeclarationViewModel>()
                .AddSingleton<TraifSectionsViewModel>()
                .AddSingleton<ReportFinancialViolationViewModel>()
                .AddSingleton<ReportsMenuViewModel>()
                .AddSingleton<LaboratoryPaymentOfInsuranceFeesViewModel>()
                .AddSingleton<SearchIndiactivePriceForExciseGoodsViewModel>()
                .AddSingleton<ExciseTaxViewModel>()
                .AddSingleton<TahqaqScanPageViewModel>()
                .AddSingleton<TaxCalculatorViewModel>()
                .AddSingleton<SubmitReportViewModel>()
                .AddSingleton<LiveVideoViewModel>()
                .AddSingleton<ReportOTPViewModel>()

                .AddSingleton<MyReportsViewModel>()
                .AddSingleton<UploadingPopupViewModel>()

                .AddSingleton<E_DeclerationViewModel>()
                .AddSingleton<TrackShipmentViewModel>()
                .AddSingleton<ListUserRequestsViewModel>()
                .AddSingleton<HomeViewModel>()
                .AddSingleton<RateUsViewModel>()
                .AddSingleton<CustomLoginViewModel>()
                .AddSingleton<BaseEDeclarationViewModel>()
                .AddSingleton<BaseProductDeclarationViewModel>()
                .AddSingleton<EDeclarationInformationsViewModel>()
                .AddSingleton<RegisterZATCAUserViewModel>()
                .AddSingleton<NativeNafathLoginPageViewModel>()
                .AddSingleton<NativeConfirmNafathPageViewModel>()
                .AddSingleton<EDeclarationPaymentViewModel>()
                .AddSingleton<TransactionReceptionViewModel>()
                .AddSingleton<IAMLoginViewModel>()
                .AddSingleton<ReviewRequestViewModel>()
                .AddSingleton<EDeclerationViewModel>()
                .AddSingleton<CustomsPaymentViewModel>()
                .AddSingleton<StateManager>()
                .AddSingleton<AboutZakatyViewModel>()
                .AddSingleton<CustomServiceMenuViewModel>()
                .AddSingleton<ChatViewModel>()
                .AddSingleton<CustomFeesFormViewModel>()
                .AddSingleton<CustomServiceMenuViewModel>()
                .AddSingleton<ChatViewModel>()
                .AddSingleton<FasahLoginViewModel>()
                .AddSingleton<BaseLoginViewModel>()
                .AddSingleton<InquiryaboutCustomsIssuesViewModel>()

                .AddSingleton<UpdateManagerViewModel>()
                .AddSingleton<ChangeMobileRequestViewModel>()
                .AddSingleton<NafathPopupPageViewModel>()
                .AddSingleton<ZakatExemptionRequestListViewModel>()
            #endregion

                .BuildServiceProvider());
            this.CreateNavigationService();
        }

        #region NewDesignViewModel
        public ZakatExemptionRequestListViewModel ZakatExemptionRequestListPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<ZakatExemptionRequestListViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public TINOutletDeregistrationViewModel TINOutletDeregistrationPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<TINOutletDeregistrationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public TinOutletDeRegRequestViewModel TinOutletDeRegRequestPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<TinOutletDeRegRequestViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public EscalatedCasesGSTCPageViewModel EscalatedCasesGSTCPageView
        {
            get
            {
                try
                {

                    return Ioc.Default.GetService<EscalatedCasesGSTCPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ViewModel.NewDesignViewModel.VATServicesPageViewModel.VATServicesPageViewModel VATServicesPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<ViewModel.NewDesignViewModel.VATServicesPageViewModel.VATServicesPageViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }

        //CR6094
        public ZakatExemptionPageViewModel ZakatExemptionPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<ZakatExemptionPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public AccountStatementDetailPageViewModel AccPageDetailVM
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<AccountStatementDetailPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public AccountStatementObjectiondetailVM AccPageObjDetailVM
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<AccountStatementObjectiondetailVM>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public UpdateVatEffectiveDateViewModel UpdateVatEffectiveDateView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<UpdateVatEffectiveDateViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
      
        public VATInstalmentPlanRevokeViewModel VatInstalmentPlanRevokePageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<VATInstalmentPlanRevokeViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public VATInstalmentNotesPageViewModel VATInstalmentPopupNotesPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<VATInstalmentNotesPageViewModel>();
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
                    return Ioc.Default.GetService<NativeNafathLoginPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NativeConfirmNafathPageViewModel NativeConfirmNafathPageViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NativeConfirmNafathPageViewModel>();
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
                    return Ioc.Default.GetService<TaxManagementPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionPageWebViewModel>();
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
                    return Ioc.Default.GetService<TaxpayerSubsidyViewModel>();
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
                    return Ioc.Default.GetService<NotesPopUpPageViewModel>();
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
                    return Ioc.Default.GetService<NotesDescriptionPopUpPageViewModel>();
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
                    return Ioc.Default.GetService<SupportPageViewModel>();
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
                    return Ioc.Default.GetService<ZatcaInfoMenuPageViewModel>();
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
                    return Ioc.Default.GetService<VATCreditCarriedForwardPopUpPageViewModel>();
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
                    return Ioc.Default.GetService<DashboardAnonymousMenuPageViewModel>();
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
                    return Ioc.Default.GetService<TINDeregistrationCloseIndividualOutletsPageViewModel>();
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
                    return Ioc.Default.GetService<SignUpForEstablishmentPageViewModel>();
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
                    return Ioc.Default.GetService<EstablishmentSignUPPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignOnBoardingAnimationPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionMyReportsListPageViewModel>();
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
                    return Ioc.Default.GetService<TaxpayerCorrespondancePageViewModel>();
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
                    return Ioc.Default.GetService<TaxpayerCorrespondanceDetailPageViewModel>();
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
                    return Ioc.Default.GetService<TaxpayersCertificatesPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignVATReturnUpdatedUIPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignDashBoardPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignDashBoardPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatForm5PageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignForgotPasswordPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignMyBillsPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignMyReturnsNewPageViewModel>();
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
                    return Ioc.Default.GetService<VATLookUpNewPageViewModel>();
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
                    return Ioc.Default.GetService<VATDeRegistrationDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<VATDeRegistrationInstructionsPageViewModel>();
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

                    return Ioc.Default.GetService<VATDeregistrationSuccessPageViewModel>();
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
                    return Ioc.Default.GetService<NewZakatObjectionPageViewModel>();
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
                    return Ioc.Default.GetService<EstablishmentRegistrationPageViewModel>();
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
                    return Ioc.Default.GetService<EstablishmentAmendUpdatePageViewModel>();
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
                    return Ioc.Default.GetService<OutletDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<OutletDetailsAmendUpdatePageViewModel>();
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
                    return Ioc.Default.GetService<ActivityItemPageViewModel>();
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
                    return Ioc.Default.GetService<ActivityItemAmendUpdatePageViewModel>();
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
                    return Ioc.Default.GetService<RegistrationSuccessfulViewModel>();
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
                    return Ioc.Default.GetService<EstablishmentAmendUpdateSuccessfulPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionVerifyMobileViewModel>();
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
                    return Ioc.Default.GetService<NewTaxEvasionFormPageViewModel>();
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
                    return Ioc.Default.GetService<NewTaxEvasionFormPageViewModel>();
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
                    return Ioc.Default.GetService<NewTaxpayerProfileViewModel>();
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
                    return Ioc.Default.GetService<UpdateMobileViewModel>();
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
                    return Ioc.Default.GetService<UpdateEmailViewModel>();
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
                    return Ioc.Default.GetService<VerificationEmailPasswordViewModel>();
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
                    return Ioc.Default.GetService<UpdatePasswordViewModel>();
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
                    return Ioc.Default.GetService<TaxpayerProfileSuccessViewModel>();
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
                    return Ioc.Default.GetService<PaymnetProcessWebviewViewModel>();
                }
                catch (Exception)
                {


                    return null;
                }
            }
        }
        #endregion


        #region OldDesignViewModel
        public PdfViewModel pdfView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<PdfViewModel>();
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
                    return Ioc.Default.GetService<FinancialDetailAttachmentPopupPageViewModel>();
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
                    return Ioc.Default.GetService<VATIndividualSignupTnCPageViewModel>();
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
                    return Ioc.Default.GetService<InternationalMobileNumberCodePagesViewModel>();
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
                    return Ioc.Default.GetService<InternationalCodeSearchPageViewModel>();
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
                    return Ioc.Default.GetService<MyBillsViewModel>();
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
                    return Ioc.Default.GetService<VATLookupPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatReturnListPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatReturnDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<SalesDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<AmendSalesDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<ICRListPageViewModel>();
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
                    return Ioc.Default.GetService<VATReturnsPageViewModelEX>();
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
                    return Ioc.Default.GetService<AcknowledgementDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<DisplayNotesPageViewModel>();
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
                    return Ioc.Default.GetService<AttachmentPageViewModel>();
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
                    return Ioc.Default.GetService<AddNotePageViewModel>();
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
                    return Ioc.Default.GetService<CreditCarriedPageViewModel>();
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
                    return Ioc.Default.GetService<AddPopPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignShowVatInformationPopUpPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionReportMobilePageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionReportTypePageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionReportFormPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionFormPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionRegistrationViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionReportAttachmentPageViewModel>();
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
                    return Ioc.Default.GetService<FormBundleStatusPageViewModel>();
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
                    return Ioc.Default.GetService<SignUpTAndCPageViewModel>();
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
                    return Ioc.Default.GetService<SignUpFormPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionReportListPageViewModel>();
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
                    return Ioc.Default.GetService<CreateGaztAccountPageViewModel>();
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
                    return Ioc.Default.GetService<AccountCreatedPageViewModel>();
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
                    return Ioc.Default.GetService<AccountCreatedSuccessfullyPageViewModel>();
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
                    return Ioc.Default.GetService<SFLoginPageViewModel>();
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
                    return Ioc.Default.GetService<ReturnsPageViewModel>();
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
                    return Ioc.Default.GetService<FAQPageViewModel>();
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
                    return Ioc.Default.GetService<AboutUsPageViewModel>();
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
                    return Ioc.Default.GetService<PrivacyAndPolicyPageViewModel>();
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
                    return Ioc.Default.GetService<MyReturnsPageViewModel>();
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
                    return Ioc.Default.GetService<ContactUsPageViewModel>();
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
                    return Ioc.Default.GetService<VATIndividualSignupPageViewModel>();
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
                    return Ioc.Default.GetService<IndividualRegistrationPageViewModel>();
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
                    return Ioc.Default.GetService<RegistrationSuccessfulPageViewModel>();
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
                    return Ioc.Default.GetService<VATRegistrationPageViewModel>();
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
                    return Ioc.Default.GetService<VATAmendReactivationPageViewModel>();
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
                    return Ioc.Default.GetService<VATRegistrationDisplayDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<VATRegistrationSuccessfullPageViewModel>();
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
                    return Ioc.Default.GetService<VATAmendReactivationSuccesssulPageViewModel>();
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
                    return Ioc.Default.GetService<FileAttachmentPopUpPageViewModel>();
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
                    return Ioc.Default.GetService<NewAccountPopUpPageViewModel>();
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
                    return Ioc.Default.GetService<UnlockAccountTINPageViewModel>();
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
                    return Ioc.Default.GetService<UnlockAccountSuccessPageViewModel>();
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
                    return Ioc.Default.GetService<TINDeregestrationSuccessPageViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignRecoverUsernameViewModel>();
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
                    return Ioc.Default.GetService<GAZTNewDesignRecoverPasswordPageViewModel>();
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
                    return Ioc.Default.GetService<ZAKATReturnDetailsViewModel>();
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
                    return Ioc.Default.GetService<ZAKATReturnDetailsViewModel>();
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
                    return Ioc.Default.GetService<VATRefundListPageViewModel>();
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
                    return Ioc.Default.GetService<VATRefundDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<VATRefundsNewRequestViewModel>();
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
                    return Ioc.Default.GetService<VATRefundsSuccessPageViewModel>();
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
                    return Ioc.Default.GetService<VATRefundsInstructionsPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatReturnDetailsSuccessfullPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatReturnNewSuccessViewModel>();
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
                    return Ioc.Default.GetService<AttachmentPopUpViewModel>();
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
                    return Ioc.Default.GetService<ZakatObjectionSuccessfullPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatAcknowledgmentPageViewModel>();
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
                    return Ioc.Default.GetService<QuickActionPopUpPageViewModel>();
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
                    return Ioc.Default.GetService<VATDeclarationAttachmentPageViewModel>();
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

                    return Ioc.Default.GetService<AccountStatementsPageViewModel>();
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
                    return Ioc.Default.GetService<AccountStatementBillsPageViewModel>();
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

                    return Ioc.Default.GetService<AccountStatementBillsPageViewModel>();
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
                    return Ioc.Default.GetService<AccountStatementsDownloadPageViewModel>();
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
                    return Ioc.Default.GetService<AccountStatementsFiltersPageViewModel>();
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
            var navigationService = (NavigationService)Ioc.Default.GetService<INavigationService>();

            #region NewDesign

            navigationService.Configure(App.GAZTNewDesignOnBoardingAnimationPageView, typeof(GAZTNewDesignOnBoardingAnimationPageView));
            navigationService.Configure(App.GAZTNewDesignVATReturnUpdatedUIPageView, typeof(GAZTNewDesignVATReturnUpdatedUIPageView));
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
            navigationService.Configure(App.ZakatRejectionReasonPopupPageView, typeof(ZakatRejectionReasonPopupPageView));
            navigationService.Configure(App.RelationShipManagerInfoPageView, typeof(RelationShipManagerInfoPageView));
            navigationService.Configure(App.UpdateVatEffectiveDatePageView, typeof(UpdateVatEffectiveDatePageView));
            navigationService.Configure(App.ZakatExemptionPageView, typeof(ZakatExemptionPageView));

            navigationService.Configure(App.TINOutletDeregistrationPageView, typeof(TINOutletDeregistrationPageView));
            navigationService.Configure(App.TinOutletDeRegRequestPageView, typeof(TinOutletDeRegRequestPageView));
            navigationService.Configure(App.FilterVatEffectiveDatePageView, typeof(FilterVatEffectiveDatePageView));
            navigationService.Configure(App.VATInstalmentPopupRevokePageView, typeof(VATInstalmentPopupRevokePageView));
            navigationService.Configure(App.VatInstalmentPlanRevokePageView, typeof(VatInstalmentPlanRevokePageView));
            navigationService.Configure(App.VATInstalmentPopupNotesPageView, typeof(VATInstalmentPopupNotesPageView));
            navigationService.Configure(App.NafathLoginView, typeof(NafathLoginView));
            navigationService.Configure(App.NafathChangeMobleNumberOptionsView, typeof(NafathChangeMobleNumberOptionsView));

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
            navigationService.Configure(App.NewYesorNoPageView, typeof(NewYesorNoPageView));//Cr6264
            navigationService.Configure("FasahLoginView", typeof(FasahLoginView));
            navigationService.Configure("InquiryaboutCustomsIssuesView", typeof(InquiryaboutCustomsIssuesView));

            //CR6003
            navigationService.Configure(App.ChangeMobileRequestPageView, typeof(ChangeMobileRequestPageView));
            navigationService.Configure(App.UpdateManagerDetailsPopUp, typeof(UpdateManagerDetailsPopUp));
            navigationService.Configure(App.EscalatedCasesGSTCPageView, typeof(EscalatedCasesGSTCPageView));//CR4820

            navigationService.Configure(App.ZakatExemptionRequestListPageView, typeof(ZakatExemptionRequestListPageView));
            navigationService.Configure(App.NafathPopUpPage,typeof(NafathPopUpPage));
            navigationService.Configure(App.OtpLoginPageView, typeof(OTPPageView));
            navigationService.Configure(App.NafathAuthenticationView, typeof(NafathAuthenticationView));
            navigationService.Configure(App.NafathChangeMobileNumberView, typeof(NafathChangeMobileNumberView));
            navigationService.Configure(App.NafathChangeMobileNumberOTPView, typeof(NafathChangeMobileNumberOTPView));
            navigationService.Configure(App.NafathChangeMobileNumberSuccessView, typeof(NafathChangeMobileNumberSuccessView));
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
                    return Ioc.Default.GetService<InquiryAboutCustomsDeclarationViewModel>();
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
                    return Ioc.Default.GetService<TraifSectionsViewModel>();
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
                    return Ioc.Default.GetService<ReportFinancialViolationViewModel>();
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
                    return Ioc.Default.GetService<ReportsMenuViewModel>();
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
                    return Ioc.Default.GetService<LaboratoryPaymentOfInsuranceFeesViewModel>();
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
                    return Ioc.Default.GetService<ExciseTaxViewModel>();
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
                    return Ioc.Default.GetService<SearchIndiactivePriceForExciseGoodsViewModel>();
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
                    return Ioc.Default.GetService<TahqaqScanPageViewModel>();
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
                    return Ioc.Default.GetService<TaxCalculatorViewModel>();
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
                    return Ioc.Default.GetService<E_DeclerationViewModel>();
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
                    return Ioc.Default.GetService<HomeViewModel>();
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
                    return Ioc.Default.GetService<RateUsViewModel>();
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
                    return Ioc.Default.GetService<CustomLoginViewModel>();
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
                    return Ioc.Default.GetService<TransactionReceptionViewModel>();
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
                    return Ioc.Default.GetService<IAMLoginViewModel>();
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
                    return Ioc.Default.GetService<EDeclerationViewModel>();
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
                    return Ioc.Default.GetService<CustomsPaymentViewModel>();
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
                    return Ioc.Default.GetService<AboutZakatyViewModel>();
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
                    return Ioc.Default.GetService<CustomServiceMenuViewModel>();
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
                    return Ioc.Default.GetService<ChatViewModel>();
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
                    return Ioc.Default.GetService<CustomFeesFormViewModel>();
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
                    return Ioc.Default.GetService<FasahLoginViewModel>();
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
                    return Ioc.Default.GetService<InquiryaboutCustomsIssuesViewModel>();
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
                    return Ioc.Default.GetService<FilesUploadPopUpViewModel>();
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
                    return Ioc.Default.GetService<NewYesorNoPageViewModel>();
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
                    return Ioc.Default.GetService<InstalmentPlanViewModel>();
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
                    return Ioc.Default.GetService<VATInstalmentPlanViewModel>();
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
                    return Ioc.Default.GetService<VATInstalmentPlanViewModel>();
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
                    return Ioc.Default.GetService<VATInstalmentPlanListViewModel>();
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
                    return Ioc.Default.GetService<InstructionsBottomPopUpViewModel>();
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
                    return Ioc.Default.GetService<MorePopUpViewModelRTwo>();
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
                    return Ioc.Default.GetService<MyBillsMultiplePayableListViewModel>();
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
                    return Ioc.Default.GetService<AddNotePopUpViewModel>();
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
                    return Ioc.Default.GetService<ViewNotePopUpViewModel>();
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
                    return Ioc.Default.GetService<ZakatInstalmentPlanViewModel>();
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

                    return Ioc.Default.GetService<ZakatInstalmentPlanViewModel>();
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
                    return Ioc.Default.GetService<ZakatInstalmentPlanListViewModel>();
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
                    return Ioc.Default.GetService<OldZakatInstalmentPlanViewModel>();
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
                    return Ioc.Default.GetService<OldZakatInstalmentPlanViewModel>();
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
                    return Ioc.Default.GetService<OldZakatInstalmentPlanListViewModel>();
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
                    return Ioc.Default.GetService<ContractReleaseViewModel>();
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
                    return Ioc.Default.GetService<ContractReleaseListViewModel>();
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
                    return Ioc.Default.GetService<ChangeFillingPeriodViewModel>();
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
                    return Ioc.Default.GetService<ChangeFillingPeriodViewModel>();
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
                    return Ioc.Default.GetService<ChangeFillingPeriodListViewModel>();
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
                    return Ioc.Default.GetService<VatReviewViewModel>();
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
                    return Ioc.Default.GetService<VatReviewViewModel>();
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
                    return Ioc.Default.GetService<VatReviewListViewModel>();
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
                    return Ioc.Default.GetService<ObjectionViewModel>();
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
                    return Ioc.Default.GetService<ZakatObjectionsListViewModel>();
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
                    return Ioc.Default.GetService<ZakatObjectionViewModel>();
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
                    return Ioc.Default.GetService<ZakatObjectionViewModel>();
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
                    return Ioc.Default.GetService<ZakatDeregistrationPageViewModel>();
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
                    return Ioc.Default.GetService<TaxEvasionReportDetailPageViewModel>();
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
                    return Ioc.Default.GetService<TINDeregistrationPageViewModel>();
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
                    return Ioc.Default.GetService<CalendarPickerPageViewModel>();
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
                    return Ioc.Default.GetService<PickerPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatRegistrationDetailsListPageViewModel>();
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
                    return Ioc.Default.GetService<GeneralServicesViewModel>();
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
                    return Ioc.Default.GetService<ZakatRegistrationTaxPayerDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatRegistrationOutletsDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<ZakatRegistrationFinancialDetailsPageViewModel>();
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
                    return Ioc.Default.GetService<VATReturnSuccessfullPageViewModel>();
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
                    return Ioc.Default.GetService<VatReturnNewSuccessViewModel>();
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
                    return Ioc.Default.GetService<MorePopUpPageViewModel>();
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
                    return Ioc.Default.GetService<ShowVatInformationConfirmationPageViewModel>();
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
                    return Ioc.Default.GetService<RefundAccountPopupPageViewModel>();
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
                    return Ioc.Default.GetService<NewAccountPopPageViewModel>();
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
                    return Ioc.Default.GetService<SubmitReportViewModel>();
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
                    return Ioc.Default.GetService<MyReportsViewModel>();
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
                    return Ioc.Default.GetService<LiveVideoViewModel>();
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
                    return Ioc.Default.GetService<ReportOTPViewModel>();
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
                    return Ioc.Default.GetService<BaseEDeclarationViewModel>();
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
                    return Ioc.Default.GetService<BaseProductDeclarationViewModel>();
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
                    return Ioc.Default.GetService<EDeclarationInformationsViewModel>();
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
                    return Ioc.Default.GetService<RegisterZATCAUserViewModel>();
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
                    return Ioc.Default.GetService<ReviewRequestViewModel>();
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
                    return Ioc.Default.GetService<EDeclarationPaymentViewModel>();
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
                    return Ioc.Default.GetService<EDeclerationSubmitModel>();
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
                    return Ioc.Default.GetService<TrackShipmentViewModel>();
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
                    return Ioc.Default.GetService<StateManager>();
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
                    return Ioc.Default.GetService<UploadingPopupViewModel>();
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
                    return Ioc.Default.GetService<ListUserRequestsViewModel>();
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
                    return Ioc.Default.GetService<ChangeMobileRequestViewModel>();
                }
                catch (Exception)
                {
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
                    return Ioc.Default.GetService<UpdateManagerViewModel>();
                }
                catch (Exception)
                {
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
                    return Ioc.Default.GetService<BankAccountManagementPageViewModel>();
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
                    return Ioc.Default.GetService<BankAccountAddorUpdateIBANViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public RelationShipManagerInfoPageViewModel RelationShipManagerInfoPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<RelationShipManagerInfoPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public ZakatRejectionReasonPopupViewModel ZakatRejectionReasonPopupPageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<ZakatRejectionReasonPopupViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public FilterVatEffectiveDatePageViewModel FilterVatEffectiveDatePageView
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<FilterVatEffectiveDatePageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public UpdateActivityInstructionsPageViewModel UpdateActivityInstructionsPage
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<UpdateActivityInstructionsPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NafathChangeMobileNumberOptionsViewModel NafathChangeMobileNumberOptionsViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NafathChangeMobileNumberOptionsViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NafathLoginViewModel NafathLoginViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NafathLoginViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NafathAuthenticationViewModel NafathAuthenticationViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NafathAuthenticationViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NafathChangeMobileNumberViewModel NafathChangeMobileNumberViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NafathChangeMobileNumberViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NafathChangeMobileNumberOTPViewModel NafathChangeMobileNumberOTPViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NafathChangeMobileNumberOTPViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public NafathChangeMobileNumberSuccessViewModel NafathChangeMobileNumberSuccessViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NafathChangeMobileNumberSuccessViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ViewModel.NewDesignViewModel.VATDeclarationPagesVM.MoreOptionsVIewModel vIewModelOptions
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<ViewModel.NewDesignViewModel.VATDeclarationPagesVM.MoreOptionsVIewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public ZakatInstalmentPlanViewModel ZakatInstalmentPlanPageView1
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<ZakatInstalmentPlanViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public OTPPageViewModel OtpPageViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<OTPPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public AccountLockedViewModel AccountLockedViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<AccountLockedViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public AttachmentViewModel AttachmentViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<AttachmentViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public NafathPopupPageViewModel NafathPopupPageViewModel
        {
            get
            {
                try
                {
                    return Ioc.Default.GetService<NafathPopupPageViewModel>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }


}

