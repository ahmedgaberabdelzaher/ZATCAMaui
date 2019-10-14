using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
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

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(App.LoginView, typeof(LogInView));
                     return navigationService;
        }

    }
}
