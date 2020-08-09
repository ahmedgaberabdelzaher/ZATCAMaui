using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatDeregistration
{
    public class ZakatRegistrationDetailsListPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackClick { get; set; }
        #endregion

        public ZakatRegistrationDetailsListPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            IsArabic = App.IsArabic;
            PopulateZakatRegListData();
        }

        public ObservableCollection<ZakatDeregistrationDetailsListModel> _zakatRegListData { get; set; }
        public ObservableCollection<ZakatDeregistrationDetailsListModel> ZakatRegListData
        {
            get
            {
                return _zakatRegListData;
            }

            set
            {
                _zakatRegListData = value;
                RaisePropertyChanged("ZakatRegListData");
            }
        }

        private bool _isArabic = true;
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                _isArabic = value;
                RaisePropertyChanged("IsArabic");
            }
        }

        public void PopulateZakatRegListData()
        {
            ZakatRegListData = new ObservableCollection<ZakatDeregistrationDetailsListModel>();
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.ZZTaxPayerDetails,
                ZDImageSource = "vat_ic_taxpayerDetail",
            });
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.TinDeregistrationRegistrationOutlets,
                ZDImageSource = "establishments",
            });
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.ZZZZVATREFinancialDetails,
                ZDImageSource = "details",
            });
            ZakatRegListData.Add(new ZakatDeregistrationDetailsListModel
            {
                ZDTitle = AppResources.TinDeregistration,
                ZDImageSource = "deregistration",
            });
        }
    }
}
