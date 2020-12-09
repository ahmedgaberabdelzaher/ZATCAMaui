using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{
    [Preserve(AllMembers = true)]
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
