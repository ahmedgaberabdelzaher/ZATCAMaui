using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GAZT.ViewModel.NewViewModel
{
    public class ZakatReturnListPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Property



        private ZakatReturns _selectedZakatReturn;
        public ZakatReturns SelectedZakatReturn
        {
            get
            {
                return _selectedZakatReturn;
            }
            set
            {
                _selectedZakatReturn = value;
                if (_selectedZakatReturn != null)
                {
                    _navigationService.NavigateTo(App.ZakatReturnDetailsPageView);
                }
                RaisePropertyChanged("SelectedZakatReturn");
            }
        }

        private List<ZakatReturns> _myZakatReturns;
        public List<ZakatReturns> MyZakatReturns
        {
            get
            {
                return _myZakatReturns;
            }
            set
            {
                _myZakatReturns = value;
                RaisePropertyChanged("MyZakatReturns");
            }
        }

        private List<ZakatReturnStatus> _zakatReturnStatus;
        public List<ZakatReturnStatus> ZakatReturnStatus
        {
            get
            {
                return _zakatReturnStatus;
            }
            set
            {
                _zakatReturnStatus = value;
                RaisePropertyChanged("ZakatReturnStatus");
            }
        }

        private ZakatReturnStatus _selectedZakatStatus;
        public ZakatReturnStatus SelectedZakatStatus
        {
            get
            {
                return _selectedZakatStatus;
            }
            set
            {
                _selectedZakatStatus = value;
               
                RaisePropertyChanged("SelectedZakatStatus");
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

        #endregion

        #region Constructor
        public ZakatReturnListPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

        #region Method


        public async void OnPageLoad()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            MyZakatReturns = new List<ZakatReturns>();
            for(int i=0;i<=5;i++)
            {
                ZakatReturns returns1 = new ZakatReturns();
                returns1.FiscalYear = "2018";
                returns1.ReturnPeriod = "2018/08/08 - 2017/06/07";
                returns1.DueDate = "31/01/2020";
                returns1.IDNumber = "100000988";
                returns1.Status = "Billed";
                MyZakatReturns.Add(returns1);
            }
            ZakatReturnStatus = new List<ZakatReturnStatus>();

            ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 1, Value = "ABC1" });
            ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 2, Value = "ABC2" });
            ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 3, Value = "ABC3" });
            ZakatReturnStatus.Add(new Models.ZakatReturnStatus { ID = 4, Value = "ABC4" });

            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }


        #endregion
    }
}
