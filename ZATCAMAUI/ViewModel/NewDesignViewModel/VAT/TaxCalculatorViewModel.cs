using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VAT
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
                OnPropertyChanged();
            }
        }

        string totaltaxablepurchases;

        public string Totaltaxablepurchases
        {
            get { return totaltaxablepurchases; }

            set
            {
                totaltaxablepurchases = value;
                OnPropertyChanged();
            }
        }

        decimal taxValue;

        public decimal TaxValue
        {
            get { return taxValue; }

            set
            {
                taxValue = value;
                OnPropertyChanged();
            }
        }
        string totaltaxablesales;

        public string Totaltaxablesales
        {
            get { return totaltaxablesales; }

            set
            {
                totaltaxablesales = value;
                OnPropertyChanged();
            }
        }



        string totalnontaxablesales;

        public string Totalnontaxablesales
        {
            get { return totalnontaxablesales; }

            set
            {
                totalnontaxablesales = value;
                OnPropertyChanged();
            }
        }

        string totalnontaxablepurchases;

        public string Totalnontaxablepurchases
        {
            get { return totalnontaxablepurchases; }

            set
            {
                totalnontaxablepurchases = value;
                OnPropertyChanged();
            }
        }


        bool isConsumerCalc = true;

        public bool IsConsumerCalc
        {
            get { return isConsumerCalc; }

            set
            {
                isConsumerCalc = value;
                OnPropertyChanged();
            }
        }

        bool isMerchantCalc;

        public bool IsMerchantCalc
        {
            get { return isMerchantCalc; }

            set
            {
                isMerchantCalc = value;
                OnPropertyChanged();
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
                                Tax = decimal.Parse(Totaltaxablepurchases) * .15m + decimal.Parse(Totaltaxablepurchases);
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
                            if (!string.IsNullOrEmpty(Totaltaxablepurchases))
                                purchases = decimal.Parse(Totaltaxablepurchases);

                            Tax = sales * .15m - purchases * .15m;
                            if (Tax < 0)
                            {

                                MessageTxt = AppResources.RefunableAmount;
                                TaxValue = Math.Round(Tax, 2, MidpointRounding.AwayFromZero) * -1;
                            }
                            else
                            {

                                MessageTxt = AppResources.VATPayable;
                                TaxValue = Math.Round(Tax, 2, MidpointRounding.AwayFromZero);
                            }

                        }
                        IsShowMsgView = true;
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

    }
}
