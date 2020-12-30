using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.AccountStatements;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    public class AccountStatementsFiltersPageViewModel: BaseViewModel
    {
        public ICommand SortAscendingTapped { get; set; }
        public ICommand SortDescendingTapped { get; set; }

        public ObservableCollection<ASFilters> _filterList = null;
        public ObservableCollection<ASFilters> FilterList
        {
            get
            {
                return _filterList;
            }
            set
            {
                if (_filterList == value) return;
                _filterList = value;
                RaisePropertyChanged("FilterList");
            }
        }

        public AccountStatementsFiltersPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            SortAscendingTapped = new Command(SortAscendingCliked);
            SortDescendingTapped = new Command(SortDescendingClicked);
        }

        private void SortDescendingClicked(object obj)
        {
            
        }

        private void SortAscendingCliked(object obj)
        {
            
        }

        public void PopulateFiltersData()
        {
            List<ASFilters> filters = new List<ASFilters>();
            filters.Add(new ASFilters {

                FilterHeader = AppResources.ASTransactionDate
            });
            filters.Add(new ASFilters
            {

                FilterHeader = AppResources.TaxType
            });
            filters.Add(new ASFilters
            {

                FilterHeader = AppResources.ASFBNum
            });
            filters.Add(new ASFilters
            {

                FilterHeader = AppResources.ASSadadBillNumber
            });
            filters.Add(new ASFilters
            {

                FilterHeader = AppResources.ASTaxperiod
            });
            filters.Add(new ASFilters
            {

                FilterHeader = AppResources.ASDueDate
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASBillDescription
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASBillAmount
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ZStatus
            });

            FilterList = new ObservableCollection<ASFilters>(filters);
        }
    }
}
