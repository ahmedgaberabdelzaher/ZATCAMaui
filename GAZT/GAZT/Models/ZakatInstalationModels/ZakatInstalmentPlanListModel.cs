using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Models.VATInstalmentModels;
using EGAZT.Models.ZakatInstalationModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel
{
    public class ZakatInstalmentPlanListViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        private bool _createZakatInstalmentBtnVisible = false;
        public bool CreateZakatInstalmentBtnVisible
        {
            get
            {
                return _createZakatInstalmentBtnVisible;
            }
            set
            {
                _createZakatInstalmentBtnVisible = value;
                RaisePropertyChanged("CreateZakatInstalmentBtnVisible");
            }
        }

        private bool _isZakatPlanViewVisible = false;
        public bool IsZakatPlanViewVisible
        {
            get
            {
                return _isZakatPlanViewVisible;
            }
            set
            {
                _isZakatPlanViewVisible = value;
                RaisePropertyChanged("IsZakatPlanViewVisible");
            }
        }

        public ICommand ReqInstalmentBtnTapped { get; set; }

        public ZakatInstalmentPlanListViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;

            ReqInstalmentBtnTapped = new Command(async () =>
            {
                _navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);
            });

            EnableViewSummaryPage();
        }

        public void EnableViewSummaryPage()
        {
            CreateZakatInstalmentBtnVisible = true;
            IsZakatPlanViewVisible = false;

        }

        public void EnableVAtCreationPage()
        {
            CreateZakatInstalmentBtnVisible = false;
            IsZakatPlanViewVisible = true;
        }



    }
}
