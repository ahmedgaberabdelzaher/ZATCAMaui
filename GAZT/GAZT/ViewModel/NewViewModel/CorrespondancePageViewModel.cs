using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GAZT.ViewModel.NewViewModel
{
    public class CorrespondancePageViewModel : ViewModelBase
    {
        #region Properties
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand onZakatLabelClicked { get; set; }
        public ICommand onVATLabelClicked { get; set; }
        public ICommand onETLabelClicked { get; set; }
        public ICommand onCITLabelClicked { get; set; }
        private List<CorrespondanceModel> _listVATCorrespondance;
        public List<CorrespondanceModel> ListVATCorrespondance
        {
            get
            {
                return _listVATCorrespondance;
            }
            set
            {
                _listVATCorrespondance = value;
                RaisePropertyChanged("ListVATCorrespondance");
            }
        }

        private List<CorrespondanceModel> _listZAKATCorrespondance;
        public List<CorrespondanceModel> ListZAKATCorrespondance
        {
            get
            {
                return _listZAKATCorrespondance;
            }
            set
            {
                _listZAKATCorrespondance = value;
                RaisePropertyChanged("ListZAKATCorrespondance");
            }
        }

        private List<CorrespondanceModel> _listETCorrespondance;
        public List<CorrespondanceModel> ListETCorrespondance
        {
            get
            {
                return _listETCorrespondance;
            }
            set
            {
                _listETCorrespondance = value;
                RaisePropertyChanged("ListETCorrespondance");
            }
        }

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

        private bool _isZakatVisible = false;
        public bool IsZakatVisible
        {
            get
            {
                return _isZakatVisible;
            }
            set
            {
                _isZakatVisible = value;
                RaisePropertyChanged("IsZakatVisible");
            }
        }

        private bool _isVATVisible = false;
        public bool IsVATVisible
        {
            get
            {
                return _isVATVisible;
            }
            set
            {
                _isVATVisible = value;
                RaisePropertyChanged("IsVATVisible");
            }
        }

        private bool _isETVisible = false;
        public bool IsETVisible
        {
            get
            {
                return _isETVisible;
            }
            set
            {
                _isETVisible = value;
                RaisePropertyChanged("IsETVisible");
            }
        }
        #endregion

        #region Constructor
        public CorrespondancePageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
        }
        #endregion

        #region Methods
        public void onPageLoad()
        {
            CorrespondenceRootObject ZakatCorres = new CorrespondenceRootObject();

            ZakatCorres = WebServiceManager.GAZTGetZakatCorrespondece().Result;
            List<CorrespondanceModel> ZakatCo = new List<CorrespondanceModel>();
            foreach(CorrespondenceResult itemZakat in ZakatCorres.d.results)
            {
                CorrespondanceModel childZakat = new CorrespondanceModel();
                childZakat.Title = itemZakat.Descript;
                childZakat.RefNumber = itemZakat.LetterNum;
                
                //childZakat.RefNumber
            }

        }
        #endregion
    }
}
