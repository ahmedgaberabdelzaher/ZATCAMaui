using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VAT
{
    public class TaxCalculatorViewModel:BaseViewModel
    {
        int calcBy = 0;

        public int CalcBy
        {
            get { return calcBy; }

            set
            {
                calcBy = value;
                RaisePropertyChanged();
            }
        }

        bool isConsumerCalc=true;

        public bool IsConsumerCalc
        {
            get { return isConsumerCalc; }

            set
            {
                isConsumerCalc = value;
                RaisePropertyChanged();
            }
        }

        bool isMerchantCalc ;

        public bool IsMerchantCalc
        {
            get { return isMerchantCalc; }

            set
            {
                isMerchantCalc = value;
                RaisePropertyChanged();
            }
        }



        public TaxCalculatorViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
        }

        public ICommand MerchantCalcSelectedCommand
        {
            get
            {
                return new Command(() =>
                {
                    calcBy = 1;
                    IsConsumerCalc = false;
                    IsMerchantCalc = true;
                });
            }
        }
        public ICommand ConsumerCalcSelectedCommand
        {
            get
            {
                return new Command(() =>
                {
                    calcBy = 0;
                    IsConsumerCalc = true;
                    IsMerchantCalc = false;
                });
            }
        }


    }
}
