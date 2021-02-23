using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class MorePopUpViewModelRTwo : BaseViewModel
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
        public MorePopUpViewModelRTwo(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
