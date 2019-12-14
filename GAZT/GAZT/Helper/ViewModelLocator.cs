using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GAZT.ViewModel;
using GAZT.Views;
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

            return navigationService;
        }

    }
}
