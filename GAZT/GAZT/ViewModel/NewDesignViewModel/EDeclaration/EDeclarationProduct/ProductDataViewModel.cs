using System;
using EGAZT.Models.EDeclerationsModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct
{
	public partial class BaseProductDeclarationViewModel
    {

        #region Properties

        bool isProductTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductTypes;
        ProductTypesModel selectedProductTypes;
        public ProductTypesModel SelectedProductTypes { get { return selectedProductTypes; } set { selectedProductTypes = value; RaisePropertyChanged(); } }


        bool isProductItemHaveSubType;
        public bool IsProductItemHaveSubType { get { return isProductItemHaveSubType; } set { isProductItemHaveSubType = value; RaisePropertyChanged(); } }


        bool isProductSubTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductSubTypes;
        ProductTypesModel selectedProductSubTypes;
        public ProductTypesModel SelectedProductSubTypes { get { return selectedProductSubTypes; } set { selectedProductSubTypes = value; RaisePropertyChanged(); } }

        #endregion

        #region Commands
        public ICommand OpenPoductTypesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isProductTypeSelected = true;
                        isTobacoTypeSelected = false;
                        isTobacotemSelected = false;
                        isProductSubTypeSelected = false;
                        isMaterialTypeSelected = false;
                        isPurposeSelected = false;
                        isCurrencySelected = false;
                        isUnitsSelected = false;
                        if (ProductTypes == null || ProductTypes.Count > 0)
                        {
                            var productTypes = await DeclerationServices.GetProductTypes();
                            ProductTypes = productTypes?.Item1.data;
                            ProductSubTypes = null;
                        }

                        var result = ProductTypes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.TypeItem;
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

        public ICommand OpenProductSubTypesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        if (SelectedProductTypes != null)
                        {
                            isProductSubTypeSelected = true;
                            isProductTypeSelected = false;
                            isTobacoTypeSelected = false;
                            isTobacotemSelected = false;
                            isMaterialTypeSelected = false;
                            isPurposeSelected = false;
                            isCurrencySelected = false;
                            isUnitsSelected = false;
                            IsLoading = true;
                            if (ProductSubTypes == null || ProductSubTypes.Count > 0)
                            {
                                var productSubTypes = await DeclerationServices.GetProductSubTypes(SelectedProductTypes.ID.ToString());
                                ProductSubTypes = productSubTypes?.Item1.data;
                            }

                            var result = ProductSubTypes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name });
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            IsShowBottomSheet = true;
                            HeaderTitle = AppResources.ProductName;
                            TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                            IsLoading = false;
                        }
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
        private async Task AddProductItem()
        {
            if (CheckProductDataNotNull())
            {
                if (int.Parse(Quantity ?? "0") <= 0)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.QuantityValidation;
                    return;
                }
                if (Double.Parse(TotalValue) < 3000)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.EDeclerationenteredValuedoesnotrequirethedeclaration;
                    return;
                }
                var item = new Models.EDeclerationsModel.SubmitModels.Product()
                {
                    count = int.Parse(Quantity ?? "0"),
                    typeName = IsProductItemHaveSubType ? SelectedProductSubTypes.Name : SelectedProductTypes.Name,
                    itemCode = IsProductItemHaveSubType ? SelectedProductSubTypes.code : SelectedProductTypes.code,
                    value = Double.Parse(TotalValue)

                };

                SubmitModel.travelerDeclaration.product.Add(item);
                var cardItem = new EDeclerationCardModel()
                {
                    Name = item.typeName,
                    desc = SelectedProductSubTypes?.Name,
                    Price = TotalValue.ToString(),
                    ID = item.ID,
                    Type = 2
                };
                Models.EDeclerationsModel.FeesCalculators.Product product = new Models.EDeclerationsModel.FeesCalculators.Product()
                {
                    harmonizedCode = IsProductItemHaveSubType ? SelectedProductSubTypes.code : item.itemCode,
                    value = Double.Parse(TotalValue),
                    ID = item.ID
                };
                CardData.Add(cardItem);
                await CalculateFees(1, null, product);
                ClearProductData();

            }
            else
            {
                DisplayRequiredDataMsg();
            }
        }

        private void ClearProductData()
        {
            SelectedProductTypes = null;
            SelectedProductSubTypes = null;
            Quantity = null;
            IsProductItemHaveSubType = false;
            TotalValue = null;
        }

        private bool CheckProductDataNotNull()
        {
            if (SelectedProductTypes != null && !string.IsNullOrWhiteSpace(Quantity) && !string.IsNullOrWhiteSpace(TotalValue))
            {
                if (IsProductItemHaveSubType && SelectedProductSubTypes == null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}

