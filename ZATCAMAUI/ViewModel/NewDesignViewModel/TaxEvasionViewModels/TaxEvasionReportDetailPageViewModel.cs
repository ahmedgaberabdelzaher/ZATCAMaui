
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{
    
    public class TaxEvasionReportDetailPageViewModel : BaseViewModel
    {
        #region properties
        public ICommand OnBackButtonClicked { get; set; }
        public TaxEvasionReportDetails _SelectedTaxEvasionListItem = null;
        public TaxEvasionReportDetails SelectedTaxEvasionListItem
        {
            get
            {
                return _SelectedTaxEvasionListItem;
            }
            set
            {
                if (_SelectedTaxEvasionListItem == value) return;
                _SelectedTaxEvasionListItem = value;
                OnPropertyChanged("SelectedTaxEvasionListItem");
            }
        }
        #endregion

        public TaxEvasionReportDetailPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
    }
}
