using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using System;
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
