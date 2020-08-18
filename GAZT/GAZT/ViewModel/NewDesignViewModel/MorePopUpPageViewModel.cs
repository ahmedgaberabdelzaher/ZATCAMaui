using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class MorePopUpPageViewModel : BaseViewModel
    {
        #region Properties
        private List<String> _vatReturnUIButtons;
        public List<String> VatReturnUIButtons
        {
            get
            {
                return _vatReturnUIButtons;
            }
            set
            {
                _vatReturnUIButtons = value;
                RaisePropertyChanged("VatReturnUIButtons");
            }
        }


        #endregion

        public MorePopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
     }
}
