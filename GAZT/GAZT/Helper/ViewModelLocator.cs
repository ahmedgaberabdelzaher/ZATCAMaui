using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GAZT.ViewModel;
using GAZT.ViewModel.NewViewModel;
using GAZT.Views;
using GAZT.Views.NewViews;

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




        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(App.LoginView, typeof(LogInView));
            navigationService.Configure(App.OTPView, typeof(OTPView));
            navigationService.Configure(App.DashboardView, typeof(DashboardView));
            navigationService.Configure(App.MyCertificate, typeof(MyCertificate));
            navigationService.Configure(App.TaxPayerProfileView, typeof(TaxPayerProfileView));
            navigationService.Configure(App.PdfView, typeof(PdfView));
            navigationService.Configure(App.ForgotUsernamePassword, typeof(ForgotUsernamePassword));


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



            return navigationService;
        }

    }
}
