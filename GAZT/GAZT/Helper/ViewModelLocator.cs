using CommonServiceLocator;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.ViewModel.NewDesignViewModel.OnBoardingAnimation;

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
using EGAZT.ViewModel.SyncFusionEnabledViewModel.UnlockAccount;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATLookupPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATRealEstatePage;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnDetailsPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;

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
using EGAZT.Views.SyncFusionEnabledViews.FormBundleStatus;
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
using EGAZT.Views.NewDesign.GenericPickers;
using EGAZT.ViewModel.NewDesignViewModel.CalendarPickerPageViewModel;
using EGAZT.ViewModel.NewDesignViewModel.GenericPickers;

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
            // SimpleIoc.Default.Register<StyleTestUIPageViewModel>();



            SimpleIoc.Default.Register<StyleTestUIPageViewModel>();
            SimpleIoc.Default.Register<ZakatDeregistrationPageViewModel>();
            SimpleIoc.Default.Register<TINDeregistrationPageViewModel>();
            SimpleIoc.Default.Register<VATLookUpNewPageViewModel>();
            SimpleIoc.Default.Register<VATDeRegistrationDetailsPageViewModel>();
            SimpleIoc.Default.Register<VATDeRegistrationInstructionsPageViewModel>();
            SimpleIoc.Default.Register<CalendarPickerPageViewModel>();
            SimpleIoc.Default.Register<PickerPageViewModel>();

            SimpleIoc.Default.Register<VATDeregistrationSuccessPageViewModel>();

            SimpleIoc.Default.Register<ZakatForm5PageViewModel>();

            SimpleIoc.Default.Register<NewZakatObjectionPageViewModel>();
            SimpleIoc.Default.Register<VATReturnSuccessfullPageViewModel>();
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
            SimpleIoc.Default.Register<VATRegistrationPageViewModel>();
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



            SimpleIoc.Default.Register<InternationalCodeSearchPageViewModel>();
            SimpleIoc.Default.Register<UnlockAccountTINPageViewModel>();
            SimpleIoc.Default.Register<UnlockAccountSuccessPageViewModel>();
            SimpleIoc.Default.Register<TINDeregestrationSuccessPageViewModel>();

            #endregion
        }

        #region NewDesignViewModel

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
            //navigationService.Configure(App.GAZTNewDesignStyleTestUIPage, typeof(StyleTestUIPageViewModel));
            //navigationService.Configure(App.GAZTNewDesignStyleTestUIPageView, typeof(StyleTestUIPageViewModel));

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
            navigationService.Configure(App.FormBundleStatusPageView, typeof(FormBundleStatusPageView));
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

            #endregion

            return navigationService;
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
        //
    }
}
