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
using EGAZT.ViewModel.SyncFusionEnabledViewModel.BillDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeEmailPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeEmailPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangeMobileNumberPage_ViewModel;

using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChangePasswordPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ChecKTINStatus_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.CorrespondancePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.CorrespondenceDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.CreateGaztAccountPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.CreditCarriedPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.DisplayNotesPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.FAQPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ForgotUsernamePasswordPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ICRListPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.InternationalMobileNumber;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyBills_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyCertificate_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyCommitmentsPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.MyReturnsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.OTPPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.Pdf_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.PrivacyAndPolicyPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ReturnsListCountsByStatus_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SalesDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFAnonymousLandingPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLandingPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLoginPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFOptionsPage_ViewModel;
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
using EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxPayerProfilePage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATLookupPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPage_ViewModel;
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
using EGAZT.Views.SyncFusionEnabledViews.AccountCreated;
using EGAZT.Views.SyncFusionEnabledViews.AcknowledgementDetails;
using EGAZT.Views.SyncFusionEnabledViews.AddNote;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using EGAZT.Views.SyncFusionEnabledViews.AmendSalesDetails;
using EGAZT.Views.SyncFusionEnabledViews.AttachmentPage;
using EGAZT.Views.SyncFusionEnabledViews.BillDetails;
using EGAZT.Views.SyncFusionEnabledViews.ChangeEmail;
using EGAZT.Views.SyncFusionEnabledViews.ChangeEmailPages;
using EGAZT.Views.SyncFusionEnabledViews.ChangeMobileNumber;
using EGAZT.Views.SyncFusionEnabledViews.ChangePassword;
using EGAZT.Views.SyncFusionEnabledViews.CheckTINStatus;
using EGAZT.Views.SyncFusionEnabledViews.ContactUsPage;
using EGAZT.Views.SyncFusionEnabledViews.Correspondance;
using EGAZT.Views.SyncFusionEnabledViews.CorrespondenceDetails;
using EGAZT.Views.SyncFusionEnabledViews.CreateGaztAccount;
using EGAZT.Views.SyncFusionEnabledViews.CreditCarried;
using EGAZT.Views.SyncFusionEnabledViews.DisplayNotes;
using EGAZT.Views.SyncFusionEnabledViews.FAQPage;
using EGAZT.Views.SyncFusionEnabledViews.ForgotUsernamePassword;
using EGAZT.Views.SyncFusionEnabledViews.ICRList;
using EGAZT.Views.SyncFusionEnabledViews.InternationalMobileNumber;
using EGAZT.Views.SyncFusionEnabledViews.MyBillsView;
using EGAZT.Views.SyncFusionEnabledViews.MyCertificate;
using EGAZT.Views.SyncFusionEnabledViews.MyCommitmentsPage;
using EGAZT.Views.SyncFusionEnabledViews.MyReturnsPage;
using EGAZT.Views.SyncFusionEnabledViews.OTPPage;
using EGAZT.Views.SyncFusionEnabledViews.PdfView;
using EGAZT.Views.SyncFusionEnabledViews.PrivacyAndPolicy;
using EGAZT.Views.SyncFusionEnabledViews.ReturnsPage;
using EGAZT.Views.SyncFusionEnabledViews.SalesDetailsView;
using EGAZT.Views.SyncFusionEnabledViews.SFAnonymousLanding;
using EGAZT.Views.SyncFusionEnabledViews.SFLanding;
using EGAZT.Views.SyncFusionEnabledViews.SFLogin;
using EGAZT.Views.SyncFusionEnabledViews.SFOptionsPage;
using EGAZT.Views.SyncFusionEnabledViews.SignUpTAndC;
using EGAZT.Views.SyncFusionEnabledViews.StylesTestUi;
using EGAZT.Views.SyncFusionEnabledViews.TaxEvasionPages;
using EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportForm;
using EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportList;
using EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportMobile;
using EGAZT.Views.SyncFusionEnabledViews.TaxEvasionReportType;
using EGAZT.Views.SyncFusionEnabledViews.TaxPayerProfile_View;
using EGAZT.Views.SyncFusionEnabledViews.UnlockAccount;
using EGAZT.Views.SyncFusionEnabledViews.VATDeclarationPagesEX;
using EGAZT.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using EGAZT.Views.SyncFusionEnabledViews.VATLookup;
using EGAZT.Views.SyncFusionEnabledViews.VATRealEstatePages;
using EGAZT.Views.SyncFusionEnabledViews.VATReturnsPage;
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

namespace EGAZT
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();

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
            SimpleIoc.Default.Register<EstablishmentRegistrationPageViewModel>();
            SimpleIoc.Default.Register<EstablishmentAmendUpdatePageViewModel>();
            SimpleIoc.Default.Register<OutletDetailsPageViewModel>();
            SimpleIoc.Default.Register<OutletDetailsAmendUpdatePageViewModel>();
            SimpleIoc.Default.Register<ActivityItemPageViewModel>();
            SimpleIoc.Default.Register<ActivityItemAmendUpdatePageViewModel>();
            SimpleIoc.Default.Register<RegistrationSuccessfulViewModel>();
            SimpleIoc.Default.Register<ZakatReturnDetailsSuccessfullPageViewModel>();
            SimpleIoc.Default.Register<NewTaxEvasionFormPageViewModel>();
            SimpleIoc.Default.Register<GAZTNewDesignShowVatInformationPopUpPageViewModel>();
            SimpleIoc.Default.Register<ZakatObjectionSuccessfullPageViewModel>();
            SimpleIoc.Default.Register<DashboardAnonymousMenuPageViewModel>();
            SimpleIoc.Default.Register<VATCreditCarriedForwardPopUpPageViewModel>();
            SimpleIoc.Default.Register<SupportPageViewModel>();
            SimpleIoc.Default.Register<NotesDescriptionPopUpPageViewModel>();
            SimpleIoc.Default.Register<NotesPopUpPageViewModel>();
            SimpleIoc.Default.Register<TaxManagementPageViewModel>();
            SimpleIoc.Default.Register<ViewModel.NewDesignViewModel.VATServicesPageViewModel.VATServicesPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionPageWebViewModel>();
            #endregion

            #region NewDesignRelease2IOC
            SimpleIoc.Default.Register<ZakatInstalmentPlanViewModel>();
            SimpleIoc.Default.Register<ZakatInstalmentPlanListViewModel>();

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

            #region OldIOC

            SimpleIoc.Default.Register<SFLandingPageViewModel>();
            SimpleIoc.Default.Register<SFLoginPageViewModel>();
            SimpleIoc.Default.Register<SFOptionsPageViewModel>();
            SimpleIoc.Default.Register<SFAnonymousLandingPageViewModel>();
            SimpleIoc.Default.Register<MyCertificateViewModel>();
            SimpleIoc.Default.Register<PdfViewModel>();
            SimpleIoc.Default.Register<MyBillsViewModel>();
            SimpleIoc.Default.Register<TaxPayerProfilePageViewModel>();
            SimpleIoc.Default.Register<ChangeMobileNumberPageViewModel>();
            SimpleIoc.Default.Register<ChangeEmailPageViewModel>();
            SimpleIoc.Default.Register<UpdateEmailVerificationPageViewModel>();
            SimpleIoc.Default.Register<ChangePasswordPageViewModel>();
            SimpleIoc.Default.Register<OTPPageViewModel>();
            SimpleIoc.Default.Register<ForgotUsernamePasswordPageViewModel>();
            SimpleIoc.Default.Register<VATLookupPageViewModel>();
            SimpleIoc.Default.Register<ZakatReturnListPageViewModel>();
            SimpleIoc.Default.Register<ZakatReturnDetailsPageViewModel>();
            SimpleIoc.Default.Register<BillDetailsPageViewModel>();
            SimpleIoc.Default.Register<SalesDetailsPageViewModel>();
            SimpleIoc.Default.Register<AmendSalesDetailsPageViewModel>();
            SimpleIoc.Default.Register<ICRListPageViewModel>();
            SimpleIoc.Default.Register<ChecKTINStatusViewModel>();
            SimpleIoc.Default.Register<VATReturnsPageViewModel>();
            SimpleIoc.Default.Register<VATReturnsPageViewModelEX>();
            SimpleIoc.Default.Register<AcknowledgementDetailsPageViewModel>();
            SimpleIoc.Default.Register<DisplayNotesPageViewModel>();
            SimpleIoc.Default.Register<AttachmentPageViewModel>();
            SimpleIoc.Default.Register<AddNotePageViewModel>();
            SimpleIoc.Default.Register<AddPopPageViewModel>();
            SimpleIoc.Default.Register<CreditCarriedPageViewModel>();
            SimpleIoc.Default.Register<CorrespondancePageViewModel>();
            SimpleIoc.Default.Register<CorrespondenceDetailsPageViewModel>();
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
            SimpleIoc.Default.Register<MyCommitmentsPageViewModel>();
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
            SimpleIoc.Default.Register<VATRealEstateServicesPageViewModel>();
            SimpleIoc.Default.Register<PropertyRegistrationPageViewModel>();
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
            SimpleIoc.Default.Register<AccountStatementsFiltersPageViewModel>();

            //
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatForm5PageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignMyBillsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<GAZTNewDesignMyReturnsNewPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATLookUpNewPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATDeRegistrationDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATDeRegistrationInstructionsPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<NewZakatObjectionPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return null;
                }
            }
        }
        /// <summary>
        /// Returns the current instance of MyCertificateViewModel
        /// </summary>
        public MyCertificateViewModel MyCertificate
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MyCertificateViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public MyBillsViewModel MyBillsView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MyBillsViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public TaxPayerProfilePageViewModel TaxPayerProfilePageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxPayerProfilePageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ChangeMobileNumberPageViewModel ChangeMobileNumberPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChangeMobileNumberPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ChangeEmailPageViewModel ChangeEmailPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChangeEmailPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ChangePasswordPageViewModel ChangePasswordPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChangePasswordPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public UpdateEmailVerificationPageViewModel UpdateEmailVerificationPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<UpdateEmailVerificationPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public OTPPageViewModel OTPPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<OTPPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public VATRealEstateServicesPageViewModel VATRealEstateServicesPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATRealEstateServicesPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public PropertyRegistrationPageViewModel PropertyRegistrationPage
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<PropertyRegistrationPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ForgotUsernamePasswordPageViewModel ForgotUsernamePasswordPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ForgotUsernamePasswordPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATLookupPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatReturnListPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatReturnDetailsPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public BillDetailsPageViewModel BillDetailsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<BillDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<SalesDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<AmendSalesDetailsPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public ChecKTINStatusViewModel CheckTINStatusPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChecKTINStatusViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public VATReturnsPageViewModel VATReturnsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VATReturnsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATReturnsPageViewModelEX>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<AcknowledgementDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        public CorrespondancePageViewModel CorrespondancePageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CorrespondancePageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public CorrespondenceDetailsPageViewModel CorrespondenceDetailsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<CorrespondenceDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportMobilePageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportTypePageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportFormPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<TaxEvasionFormPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<TaxEvasionRegistrationViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        #endregion

        #region OldSFViewModels
        public SFLandingPageViewModel SFLandingPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SFLandingPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public SFOptionsPageViewModel OptionsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SFOptionsPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
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
                    return null;
                }
            }
        }
        public SFAnonymousLandingPageViewModel SFAnonymousLandingPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<SFAnonymousLandingPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public MyCommitmentsPageViewModel MyCommitmentsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<MyCommitmentsPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATIndividualSignupPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<IndividualRegistrationPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATRegistrationPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATAmendReactivationPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATRegistrationDisplayDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATRefundDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATRefundsNewRequestViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATRefundsInstructionsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatReturnDetailsSuccessfullPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<AccountStatementsPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

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
            navigationService.Configure(App.GAZTNewDesignDashBoardPageView, typeof(GAZTNewDesignDashBoardPageView));
            navigationService.Configure(App.GAZTNewDesignMyReturnsNewPageView, typeof(GAZTNewDesignMyReturnsNewPageView));
            navigationService.Configure(App.TaxpayerCorrespondancePageView, typeof(TaxpayerCorrespondancePageView));
            navigationService.Configure(App.TaxpayerCorrespondanceDetailPageView, typeof(TaxpayerCorrespondanceDetailPageView));
            navigationService.Configure(App.NewZakatObjectionPageView, typeof(NewZakatObjectionPageView));
            navigationService.Configure(App.VATReturnSuccessfullPageView, typeof(VATReturnSuccessfullPageView));
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
            navigationService.Configure(App.NotesPopUpPageView, typeof(NotesPopUpPageView));
            navigationService.Configure(App.NotesDescriptionPopUpPageView, typeof(NotesDescriptionPopUpPageView));
            navigationService.Configure(App.TaxManagementPageView, typeof(TaxManagementPageView));
            //navigationService.Configure(App.GAZTNewDesignStyleTestUIPage, typeof(StyleTestUIPageViewModel));
            //navigationService.Configure(App.GAZTNewDesignStyleTestUIPageView, typeof(StyleTestUIPageViewModel));
            navigationService.Configure(App.QuickActionPopUpPageView, typeof(QuickActionPopUpPageView));
            navigationService.Configure(App.VATAmendReactivationPageView, typeof(VATAmendReactivationPageView));
            navigationService.Configure(App.VATServicesPageView, typeof(Views.NewDesign.VATServices.VATServicesPageView));
            navigationService.Configure(App.TaxEvasionPageWebView, typeof(TaxEvasionPageWebView));

            #endregion

            #region NewDesignRelease2
            navigationService.Configure(App.InstalmentPlanPageView, typeof(InstalmentPlanPageView));
            navigationService.Configure(App.ZakatInstalmentPlanPageView, typeof(ZakatInstalmentPlanPageView));
            navigationService.Configure(App.ZakatInstalmentPlanListPageView, typeof(ZakatInstalmentPlanListPageView));

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


            #region SYNCFUSION INTEGRATION

            navigationService.Configure(App.SFLandingPageView, typeof(SFLandingPageView));
            navigationService.Configure(App.SFOptionsPageView, typeof(SFOptionsPageView));
            navigationService.Configure(App.SFLoginPageView, typeof(SFLoginPageView));
            navigationService.Configure(App.SFAnonymousLandingPageView, typeof(SFAnonymousLandingPageView));
            //navigationService.Configure(App.OnBoardingAnimationPageView, typeof(OnBoardingAnimationPageView));

            //SYNCFUSION INTEGRATION
            navigationService.Configure(App.MyCertificate, typeof(MyCertificate));
            navigationService.Configure(App.PdfView, typeof(PdfView));
            //   navigationService.Configure(App.ForgotUsernamePassword, typeof(ForgotUsernamePassword));
            navigationService.Configure(App.MyBillsView, typeof(MyBillsView));
            navigationService.Configure(App.TaxPayerProfilePageView, typeof(TaxPayerProfilePageView));
            navigationService.Configure(App.ChangeMobileNumberPageView, typeof(ChangeMobileNumberPageView));
            navigationService.Configure(App.ChangeEmailPageView, typeof(ChangeEmailPageView));
            navigationService.Configure(App.UpdateEmailVerificationPage, typeof(UpdateEmailVerificationPage));

            navigationService.Configure(App.ChangePasswordPageView, typeof(ChangePasswordPageView));
            navigationService.Configure(App.OTPPageView, typeof(OTPPageView));
            navigationService.Configure(App.ForgotUsernamePasswordPageView, typeof(ForgotUsernamePasswordPageView));
            navigationService.Configure(App.VATLookupPageView, typeof(VATLookupPageView));
            navigationService.Configure(App.ZakatReturnListPageView, typeof(ZakatReturnListPageView));
            navigationService.Configure(App.ZakatReturnDetailsPageView, typeof(ZakatReturnDetailsPageView));
            navigationService.Configure(App.BillDetailsPageView, typeof(BillDetailsPageView));
            navigationService.Configure(App.SalesDetailsPageView, typeof(SalesDetailsPageView));
            navigationService.Configure(App.AmendSalesDetailsPageView, typeof(AmendSalesDetailsPageView));
            navigationService.Configure(App.CheckTINStatusPageView, typeof(CheckTINStatusPageView));
            navigationService.Configure(App.ICRListPageView, typeof(ICRListPageView));
            navigationService.Configure(App.VATReturnsPageView, typeof(VATReturnsPageView));
            navigationService.Configure(App.VATReturnsPageViewEX, typeof(VATReturnsPageViewEX));
            navigationService.Configure(App.AAcknowledgementView, typeof(AAcknowledgementView));
            navigationService.Configure(App.AcknowledgementDetailsPageView, typeof(AcknowledgementDetailsPageView));
            navigationService.Configure(App.DisplayNotesPageView, typeof(DisplayNotesPageView));
            navigationService.Configure(App.AttachmentPageView, typeof(AttachmentPageView));
            navigationService.Configure(App.AddNotePageView, typeof(AddNotePageView));
            navigationService.Configure(App.AddPopPageView, typeof(AddPopPageView));
            navigationService.Configure(App.CreditCarriedPageView, typeof(CreditCarriedPageView));
            navigationService.Configure(App.CorrespondancePageView, typeof(CorrespondancePageView));
            navigationService.Configure(App.CorrespondenceDetailsPageView, typeof(CorrespondenceDetailsPageView));
            navigationService.Configure(App.FormBundleStatusPageView, typeof(EGAZT.Views.NewDesign.FormBundleStatusPages.FormBundleStatusPageView));
            navigationService.Configure(App.SignUpTAndCViewPage, typeof(SignUpTAndCViewPage));
            navigationService.Configure(App.SignUpFormPageView, typeof(SignUpFormPageView));
            navigationService.Configure(App.CreditCarriedPageView, typeof(CreditCarriedPageView));
            navigationService.Configure(App.TaxEvasionRegistrationPageView, typeof(TaxEvasionRegistrationPageView));
            navigationService.Configure(App.TaxEvasionReportTypePageView, typeof(TaxEvasionReportTypePageView));
            navigationService.Configure(App.TaxEvasionReportFormPageView, typeof(TaxEvasionReportFormPageView));
            navigationService.Configure(App.CreateGaztAccountPageView, typeof(CreateGaztAccountPageView));
            navigationService.Configure(App.AccountCreatedPageView, typeof(AccountCreatedPageView));
            navigationService.Configure(App.TaxEvasionReportListPageView, typeof(TaxEvasionReportListPageView));
            navigationService.Configure(App.TaxEvasionReportMobilePageView, typeof(TaxEvasionReportMobilePageView));
            navigationService.Configure(App.ReturnsPageView, typeof(ReturnsPageView));
            navigationService.Configure(App.FAQPageView, typeof(FAQPageView));
            navigationService.Configure(App.AboutUsPageView, typeof(AboutUsPageView));
            navigationService.Configure(App.PrivacyAndPolicyPageView, typeof(PrivacyAndPolicyPageView));
            navigationService.Configure(App.MyReturnsPageView, typeof(MyReturnsPageView));
            navigationService.Configure(App.MyCommitmentsPageView, typeof(MyCommitmentsPageView));
            navigationService.Configure(App.ContactUsPageView, typeof(ContactUsPageView));
            navigationService.Configure(App.VATIndividualSignupPageView, typeof(VATIndividualSignupPageView));
            navigationService.Configure(App.IndividualRegistrationPageView, typeof(IndividualRegistrationPageView));
            navigationService.Configure(App.RegistrationSuccessfulPageView, typeof(RegistrationSuccessfulPageView));
            navigationService.Configure(App.VATRegistrationPageView, typeof(VATRegistrationPageView));
            navigationService.Configure(App.VATRegistrationSuccessfullPageView, typeof(VATRegistrationSuccessfullPageView));
            navigationService.Configure(App.VATAmendReactivationSuccessfulPageView, typeof(VATAmendReactivationSuccessfulPageView));

            navigationService.Configure(App.VATRealEstateServicesPageView, typeof(VATRealEstateServicesPage));
            navigationService.Configure(App.PropertyRegistrationPage, typeof(PropertyRegistrationPage));

            navigationService.Configure(App.FileAttachmentPopUpPageView, typeof(FileAttachmentPopUpPageView));
            navigationService.Configure(App.TaxEvasionFormPage, typeof(TaxEvasionFormPage));

            navigationService.Configure(App.TaxEvasionAttachmentPageView, typeof(TaxEvasionAttachmentPageView));
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
            navigationService.Configure(App.ZakatRegistrationTaxPayerDetails, typeof(ZakatRegistrationTaxPayerDetails));
            navigationService.Configure(App.ZakatRegistrationOutletsDetails, typeof(ZakatRegistrationOutletsDetails));
            navigationService.Configure(App.ZakatRegistrationFinancialDetails, typeof(ZakatRegistrationFinancialDetails));

            navigationService.Configure(App.ZakatReturnDetailsSuccessfullPageView, typeof(ZakatReturnDetailsSuccessfullPageView));

            navigationService.Configure(App.TaxEvasionVerifyMobileNumberPage, typeof(TaxEvasionVerifyMobileNumberPage));
            navigationService.Configure(App.VATRefundsListPageView, typeof(VATRefundsListPageView));
            navigationService.Configure(App.VATRefundDetailsPageView, typeof(VATRefundDetailsPageView));
            navigationService.Configure(App.VATRefundsNewRequestPageView, typeof(VATRefundsNewRequestPageView));
            navigationService.Configure(App.VATRefundsSuccessPageView, typeof(VATRefundsSuccessPageView));
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
            navigationService.Configure(App.AccountStatementsFiltersPageView, typeof(AccountStatementsFiltersPageView));

            //AccountStatementsFiltersPageViewModel
            //End Account Statements
            #endregion

            return navigationService;
        }
        #endregion

        #region Release2 FileUpload

        public FilesUploadPopUpViewModel FilesUploadPopUpView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<FilesUploadPopUpViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<InstalmentPlanViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATInstalmentPlanViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VATInstalmentPlanListViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<InstructionsBottomPopUpViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatInstalmentPlanViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatInstalmentPlanListViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ContractReleaseViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ContractReleaseListViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ChangeFillingPeriodViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ChangeFillingPeriodListViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VatReviewViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<VatReviewListViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ObjectionViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatObjectionsListViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatObjectionViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatDeregistrationPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportDetailPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<TINDeregistrationPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<CalendarPickerPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<PickerPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationDetailsListPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationTaxPayerDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationOutletsDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ZakatRegistrationFinancialDetailsPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
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
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<ShowVatInformationConfirmationPageViewModel>();
                }
                catch (Exception ex)
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
                    return ServiceLocator.Current.GetInstance<RefundAccountPopupPageViewModel>();
                }
                catch (Exception ex)
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
                catch (Exception ex)
                {
                    return null;
                }
            }
        }



        //
    }


}
