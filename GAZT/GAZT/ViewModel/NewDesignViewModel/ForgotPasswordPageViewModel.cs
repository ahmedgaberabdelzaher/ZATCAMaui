
using System;
using System.Windows.Input;
using EGAZT.ViewModel.NewDesignViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EGAZT
{
    public class ForgotPasswordPageViewModel : ViewModelBase 
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnVerifyButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        #region Property
       
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        #endregion

        #region Constructor
        public ForgotPasswordPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
         
        }
        #endregion

        #region Method

        public void OnPageLoad()
        {

        }

        #endregion



    }
}
