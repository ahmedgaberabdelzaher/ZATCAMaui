using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.ViewModel.NewViewModel
{
    public class ZakatReturnListPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        #endregion

        #region Property
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


        public void OnPageLoad()
        {
            MyZakatReturns = new List<ZakatReturns>();
            for(int i=0;i<=5;i++)
            {
                ZakatReturns returns1 = new ZakatReturns();
                returns1.FiscalYear = "1430";
                returns1.ReturnPeriod = "2018/08/23.2017/8/11";
                MyZakatReturns.Add(returns1);
            }
        }


        #endregion
    }
}
