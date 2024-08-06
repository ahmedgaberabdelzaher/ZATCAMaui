
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

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
                OnPropertyChanged("CreditCarriedsList");
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
                OnPropertyChanged("VATDeclarationData");
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
                OnPropertyChanged("IsNoDataLabelVisible");
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
                OnPropertyChanged("IsListViewVisible");
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
