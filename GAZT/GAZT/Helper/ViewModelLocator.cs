using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GAZT.ViewModel;
using GAZT.ViewModel.NewViewModel;
using GAZT.Views;
using GAZT.Views.NewViews;
using GAZTeServicesApp.ViewModels.LandingPage;
using GAZTeServicesApp.ViewModels.Options;
using GAZTeServicesApp.Views.LandingPage;
using GAZTeServicesApp.Views.Options;

namespace GAZT
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            //SYNCFUSION INTEGRATION

            SimpleIoc.Default.Register<LandingPageViewModel>();
            SimpleIoc.Default.Register<OptionsPageViewModel>();

            //SYNCFUSION INTEGRATION


            SimpleIoc.Default.Register<LogInViewModel>();
            SimpleIoc.Default.Register<OTPViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<MyCertificateViewModel>();
            SimpleIoc.Default.Register<TaxPayerProfileViewModel>();
            SimpleIoc.Default.Register<PdfViewModel>();
            SimpleIoc.Default.Register<ForgotUsernamePasswordViewModel>();

            SimpleIoc.Default.Register<TPProfileViewModel>();
            SimpleIoc.Default.Register<VerifyEmailAddressViewModel>();
            SimpleIoc.Default.Register<ChangeMobileNumberViewModel>();
            SimpleIoc.Default.Register<ChangePasswordViewModel>();
            SimpleIoc.Default.Register<MyBillsViewModel>();
            SimpleIoc.Default.Register<PdfiOSViewModel>();

            SimpleIoc.Default.Register<LogInPageViewModel>();
            SimpleIoc.Default.Register<DashboardPageViewModel>();
            SimpleIoc.Default.Register<TaxPayerProfilePageViewModel>();
            SimpleIoc.Default.Register<ChangeMobileNumberPageViewModel>();
            SimpleIoc.Default.Register<ChangeEmailPageViewModel>();
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
            SimpleIoc.Default.Register<ZakatBillDetailsPageViewModel>();
            SimpleIoc.Default.Register<AAcknowledgementViewModel>();
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
            SimpleIoc.Default.Register<TaxEvasionReportTypePageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionReportFormPageViewModel>();
            SimpleIoc.Default.Register<TaxEvasionReportListPageViewModel>();
            

            SimpleIoc.Default.Register<AccountCreatedPageViewModel>();

        }

        /// <summary>
        /// Returns the current instance of LogInViewModel
        /// </summary>
        public LogInViewModel LogInView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<LogInViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the current instance of OTPViewModel
        /// </summary>
        public OTPViewModel OTPView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<OTPViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the current instance of OTPViewModel
        /// </summary>
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

        /// <summary>
        /// Returns the current instance of DashboardViewModel
        /// </summary>
        public DashboardViewModel DashboardView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<DashboardViewModel>();
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

        /// <summary>
        /// Returns the current instance of TaxPayerProfileViewModel
        /// </summary>
        public TaxPayerProfileViewModel TaxPayerProfileView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxPayerProfileViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the current instance of ForgotUsernamePasswordViewModel
        /// </summary>
        public ForgotUsernamePasswordViewModel ForgotUsernamePassword
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ForgotUsernamePasswordViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Returns the current instance of ForgotUsernamePasswordViewModel
        /// </summary>
        public TPProfileViewModel TPProfileView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TPProfileViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the current instance of ForgotUsernamePasswordViewModel
        /// </summary>
        public VerifyEmailAddressViewModel VerifyEmailAddressView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<VerifyEmailAddressViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the current instance of ForgotUsernamePasswordViewModel
        /// </summary>
        public ChangeMobileNumberViewModel ChangeMobileNumberView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChangeMobileNumberViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public ChangePasswordViewModel ChangePasswordView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ChangePasswordViewModel>();
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


        public LogInPageViewModel LogInPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<LogInPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }


        public DashboardPageViewModel DashboardPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<DashboardPageViewModel>();
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

        /// <summary>
        /// Returns the current instance of ForgotUsernamePasswordViewModel
        /// </summary>
        public PdfiOSViewModel PdfiOSView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<PdfiOSViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns the current instance of ForgotUsernamePasswordViewModel
        /// </summary>
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

        public ZakatBillDetailsPageViewModel ZakatBillDetailsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<ZakatBillDetailsPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public AAcknowledgementViewModel AAcknowledgementView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<AAcknowledgementViewModel>();
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
        public TaxEvasionReportFormAttachmentPageViewModel TaxEvasionReportFormAttachmentPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<TaxEvasionReportFormAttachmentPageViewModel>();
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


        //SYNC FUSION INTEGRATION

        public LandingPageViewModel LandingPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<LandingPageViewModel>();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public OptionsPageViewModel OptionsPageView
        {
            get
            {
                try
                {
                    return ServiceLocator.Current.GetInstance<OptionsPageViewModel>();
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

            //SYNCFUSION INTEGRATION

            navigationService.Configure(App.LandingPageView, typeof(LandingPageView));
            navigationService.Configure(App.OptionsPageView, typeof(OptionsPageView));

            //SYNCFUSION INTEGRATION


            navigationService.Configure(App.LoginView, typeof(LogInView));
            navigationService.Configure(App.OTPView, typeof(OTPView));
            navigationService.Configure(App.DashboardView, typeof(DashboardView));
            navigationService.Configure(App.MyCertificate, typeof(MyCertificate));
            navigationService.Configure(App.TaxPayerProfileView, typeof(TaxPayerProfileView));
            navigationService.Configure(App.PdfView, typeof(PdfView));
         //   navigationService.Configure(App.ForgotUsernamePassword, typeof(ForgotUsernamePassword));


            navigationService.Configure(App.TPProfileView, typeof(TPProfileView));
            navigationService.Configure(App.VerifyEmailAddressView, typeof(VerifyEmailAddressView));
            navigationService.Configure(App.ChangeMobileNumberView, typeof(ChangeMobileNumberView));
            navigationService.Configure(App.ChangePasswordView, typeof(ChangePasswordView));
            navigationService.Configure(App.MyBillsView, typeof(MyBillsView));
            navigationService.Configure(App.PdfiOSView, typeof(PdfiOSView));


            navigationService.Configure(App.LogInPageView, typeof(LogInPageView));
            navigationService.Configure(App.DashboardPageView, typeof(DashboardPageView));
            navigationService.Configure(App.TaxPayerProfilePageView, typeof(TaxPayerProfilePageView));
            navigationService.Configure(App.ChangeMobileNumberPageView, typeof(ChangeMobileNumberPageView));
            navigationService.Configure(App.ChangeEmailPageView, typeof(ChangeEmailPageView));
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
            navigationService.Configure(App.ZakatBillDetailsPageView, typeof(ZakatBillDetailsPageView));
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
            navigationService.Configure(App.TaxEvasionReportTypePageView, typeof(TaxEvasionReportTypePageView));
            navigationService.Configure(App.TaxEvasionReportFormPageView, typeof(TaxEvasionReportFormPageView));
            navigationService.Configure(App.TaxEvasionReportFormAttachmentPageView, typeof(TaxEvasionReportFormAttachmentPageView));
            navigationService.Configure(App.CreateGaztAccountPageView, typeof(CreateGaztAccountPageView));
            navigationService.Configure(App.AccountCreatedPageView, typeof(AccountCreatedPageView));
            navigationService.Configure(App.TaxEvasionReportListPageView, typeof(TaxEvasionReportListPageView));


            return navigationService;
        }

    }
}
