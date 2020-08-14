using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{
    public class ZakatObjectionSuccessfullPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        //============================start===================================================
        public ICommand OnSubmitClicked { get; set; }
        #region Property

        private bool _isEditVisible = true;
        public bool isEditVisible
        {
            get
            {
                return _isEditVisible;
            }
            set
            {
                _isEditVisible = value;
                RaisePropertyChanged("isEditVisible");
            }
        }
        #endregion

        #region Constructor
        public ZakatObjectionSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            //=======================start==================================================

        }
        #endregion

        #region Method
        public void OnPageLoad()
        {

        }
        #endregion
    }
}
