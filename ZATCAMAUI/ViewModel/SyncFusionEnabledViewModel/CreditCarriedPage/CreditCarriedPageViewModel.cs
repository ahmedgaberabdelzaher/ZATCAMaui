using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.CreditCarriedPage
{

    public class CreditCarriedPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
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
        public CreditCarriedPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }
    }
}
