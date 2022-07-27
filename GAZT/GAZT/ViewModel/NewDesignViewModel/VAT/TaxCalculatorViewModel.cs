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

        string totaltaxablepurchases="3";

        public string Totaltaxablepurchases
        {
            get { return totaltaxablepurchases; }

            set
            {
                totaltaxablepurchases = value;
                RaisePropertyChanged();
            }
        }


        string totaltaxablesales;

        public string Totaltaxablesales
        {
            get { return totaltaxablesales; }

            set
            {
                totaltaxablesales = value;
                RaisePropertyChanged();
            }
        }

        string totalnontaxablesales;

        public string Totalnontaxablesales
        {
            get { return totalnontaxablesales; }

            set
            {
                totalnontaxablesales=value ;
                RaisePropertyChanged();
            }
        }

        string totalnontaxablepurchases;

        public string Totalnontaxablepurchases
        {
            get { return totalnontaxablepurchases; }

            set
            {
                totalnontaxablepurchases = value;
                RaisePropertyChanged();
            }
        }


        bool isConsumerCalc =true;

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
                    CalcBy = 1;
                    IsConsumerCalc = false;
                    IsMerchantCalc = true;
                    ClearAllValues();
                });
            }
        }

        public void ClearAllValues()
        {
            Totalnontaxablepurchases = Totalnontaxablesales = totaltaxablepurchases = totaltaxablesales = "";
            MessageTxt = "";
        }

        public ICommand ConsumerCalcSelectedCommand
        {
            get
            {
                return new Command(() =>
                {
                    CalcBy = 0;
                    IsConsumerCalc = true;
                    IsMerchantCalc = false;
                    ClearAllValues();
                });
            }
        }


        public ICommand CalculateTaxCommand
        {
            get
            {
                return new Command(() =>
                {
                    double Tax=0;
                    if (IsConsumerCalc)
                    {
                        if (!String.IsNullOrEmpty(Totaltaxablepurchases))
                        {
                         Tax = (double.Parse(Totaltaxablepurchases) * 2) * .15;

                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.ZZPleasefillthemandatoryfields;
                        }
                       
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Totaltaxablesales)&& !string.IsNullOrEmpty(Totaltaxablepurchases))
                        {
                            Tax = (double.Parse(Totaltaxablesales) * .15) - (double.Parse(Totaltaxablepurchases) * .15);

                        }

                        else
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.ZZPleasefillthemandatoryfields;
                    }

                    }
                    IsShowMsgView = true;
                    MessageTxt = Tax.ToString();
                });
            }
        }

    }
}
