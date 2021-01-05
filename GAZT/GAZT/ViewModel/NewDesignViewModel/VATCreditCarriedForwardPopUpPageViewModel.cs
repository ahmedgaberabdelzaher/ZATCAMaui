using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class VATCreditCarriedForwardPopUpPageViewModel : BaseViewModel
    {

        #region Properties
        private List<Result3> _creditCarriedsList;
        public List<Result3> CreditCarriedsList
        {
            get
            {
                return _creditCarriedsList;
            }
            set
            {
                if (_creditCarriedsList == value) return;
                _creditCarriedsList = value;
                RaisePropertyChanged("CreditCarriedsList");
            }
        }
        private VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                if (_vATDeclarationData == value) return;

                _vATDeclarationData = value;
                RaisePropertyChanged("VATDeclarationData");
            }
        }
        private bool _isNoDataLabelVisible;
        public bool IsNoDataLabelVisible
        {
            get
            {
                return _isNoDataLabelVisible;
            }
            set
            {
                if (_isNoDataLabelVisible == value) return;

                _isNoDataLabelVisible = value;
                RaisePropertyChanged("IsNoDataLabelVisible");
            }
        }
        private bool _isListViewVisible;
        public bool IsListViewVisible
        {
            get
            {
                return _isListViewVisible;
            }
            set
            {
                if (_isListViewVisible == value) return;

                _isListViewVisible = value;
                RaisePropertyChanged("IsListViewVisible");
            }
        }
        #endregion

        #region Constructor
        public VATCreditCarriedForwardPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }


        #endregion
    }
}
