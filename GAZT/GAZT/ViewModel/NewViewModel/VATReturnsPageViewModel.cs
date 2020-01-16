using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.ViewModel.NewViewModel
{
    public class VATReturnsPageViewModel:ViewModelBase
    {
        #region Variable

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Property


        private bool _isDeclarationChecked = false;
        public bool IsDeclarationChecked
        {
            get
            {
                return _isDeclarationChecked;
            }
            set
            {
                _isDeclarationChecked = value;
                RaisePropertyChanged("IsDeclarationChecked");
            }
        }


        #endregion

        #region Constructor

        public VATReturnsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }


            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;

            //OnCopySadadNumberButtonClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    await _dialogService.ShowMessage("It has copied sadad payment number", AppResources.Information);
            //});


        }

        #endregion

        #region Method
        #endregion
    }
}
