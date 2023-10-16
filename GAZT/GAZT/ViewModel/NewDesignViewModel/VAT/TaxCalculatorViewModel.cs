using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.VAT
{
    public class TaxCalculatorViewModel : BaseViewModel
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

        string totaltaxablepurchases;

        public string Totaltaxablepurchases
        {
            get { return totaltaxablepurchases; }

            set
            {
                totaltaxablepurchases = value;
                RaisePropertyChanged();
            }
        }

        decimal taxValue;

        public decimal TaxValue
        {
            get { return taxValue; }

            set
            {
                taxValue = value;
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
                totalnontaxablesales = value;
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


        bool isConsumerCalc = true;

        public bool IsConsumerCalc
        {
            get { return isConsumerCalc; }

            set
            {
                isConsumerCalc = value;
                RaisePropertyChanged();
            }
        }

        bool isMerchantCalc;

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
                    Totaltaxablepurchases = "";
                    ClearAllValues();
                });
            }
        }

        public void ClearAllValues()
        {
            Totalnontaxablepurchases = Totalnontaxablesales = Totaltaxablepurchases = Totaltaxablesales = "";
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
                    try
                    {
                        decimal Tax = 0.00m;
                        decimal sales = 0.00m;
                        decimal purchases = 0.00m;
                        if (IsConsumerCalc)
                        {
                            if (!string.IsNullOrEmpty(Totaltaxablepurchases))
                            {
                                Tax = (decimal.Parse(Totaltaxablepurchases) * .15m) + decimal.Parse(Totaltaxablepurchases);
                                MessageTxt = AppResources.TotalPriceValue;
                                TaxValue = Tax;
                            }
                            else
                            {
                                IsValidationError = true;
                                MessageTxt = AppResources.ZZPleasefillthemandatoryfields;
                                return;
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Totaltaxablesales))
                                sales = decimal.Parse(Totaltaxablesales);
                            if(!string.IsNullOrEmpty(Totaltaxablepurchases))
                                purchases = decimal.Parse(Totaltaxablepurchases);

                            Tax = (sales * .15m) - (purchases * .15m);
                            if (Tax < 0)
                            {

                                MessageTxt = AppResources.RefunableAmount;
                                TaxValue = (Math.Round(Tax, 2, MidpointRounding.AwayFromZero)) * -1;
                            }
                            else
                            {

                                MessageTxt = AppResources.VATPayable;
                                TaxValue = Math.Round(Tax,2,MidpointRounding.AwayFromZero);
                            }



                            //else
                            //{
                            //    IsValidationError = true;
                            //     MessageTxt = AppResources.ZZPleasefillthemandatoryfields;
                            //    return;
                            //}

                        }
                        IsShowMsgView = true;
                        // MessageTxt = Tax.ToString();
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

    }
}
