using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels
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
                RaisePropertyChanged("SelectedTaxEvasionListItem");
            }
        }
        #endregion

        public TaxEvasionReportDetailPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }
    }
}
