using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.EDeclerationsModel;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct
{
	public partial class BaseProductDeclarationViewModel
    {
        #region Properties

        bool isCurrencySelected = false;
        static ObservableCollection<CurrencyModel> Currencies;
        CurrencyModel selectedCurrencie;
        public CurrencyModel SelectedCurrencie { get { return selectedCurrencie; } set { selectedCurrencie = value; RaisePropertyChanged(); } }

        #endregion

        #region Commands
        public ICommand OpenCurrenciesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isCurrencySelected = true;
                        isPurposeSelected = false;
                        isMaterialTypeSelected = false;
                        isProductSubTypeSelected = false;
                        isProductTypeSelected = false;
                        isTobacoTypeSelected = false;
                        isTobacotemSelected = false;
                        isUnitsSelected = false;
                        if (Currencies == null || Currencies.Count > 0)
                        {
                            var currencies = await DeclerationServices.GetCurrencies();
                            Currencies = currencies?.Item1.data;
                        }

                        var result = Currencies.Select(c => new BottomSheetModel() { Id = c.currencyCode.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.Purpose;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                    }
                    finally
                    {
                        IsLoading = false;
                    }

                });

            }
        }


        #endregion

        #region Methods
        private bool CheckCurrencyDataNotNull()
        {
            if (SelectedCurrencie != null && SelectedMaterialTypes != null && SelectedPurposes != null && !string.IsNullOrWhiteSpace(TotalValue))
            {
                if (SelectedPurposes.ID == 8)
                {
                    if (string.IsNullOrWhiteSpace(OtherPurpose))
                        return false;
                }
                return true;
            }
            return false;
        }

        private void AddCurrency()
        {
            if (CheckCurrencyDataNotNull())
            {


                var item = new Models.EDeclerationsModel.SubmitModels.Currency()
                {
                    otherpurpose = OtherPurpose,
                    typeName = SelectedMaterialTypes.Name,
                    typeID = SelectedMaterialTypes.ID,
                    purpose = SelectedPurposes.ID,
                    currencyName = SelectedCurrencie.Name,
                    currency = SelectedCurrencie.currencyCode
                };
                SubmitModel.travelerDeclaration.currency.Add(item);
                var cardItem = new EDeclerationCardModel()
                {
                    Name = SelectedMaterialTypes.Name,
                    desc = SelectedPurposes.Name,
                    Price = TotalValue.ToString(),
                    ID = item.ID,
                    Type = 3
                };
                CardData.Add(cardItem);

                ClearCurrencyData();
            }
            else
            {
                DisplayRequiredDataMsg();
            }
        }

        private void ClearCurrencyData()
        {
            SelectedCurrencie = null;
            SelectedMaterialTypes = null;
            Quantity = null;
            OtherPurpose = "";
            SelectedPurposes = null;
            TotalValue = null;
        }
        #endregion
    }
}

