using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{
    public class TaxEvasionReportDetailPageViewModel : BaseViewModel
    {
        #region properties

        public TaxEvasionReportDetails _SelectedTaxEvasionListItem = null;
        public TaxEvasionReportDetails SelectedTaxEvasionListItem
        {
            get
            {
                return _SelectedTaxEvasionListItem;
            }
            set
            {
                _SelectedTaxEvasionListItem = value;
                RaisePropertyChanged("SelectedTaxEvasionListItem");
            }
        }
        #endregion

        public TaxEvasionReportDetailPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
