using System.Windows.Input;
using System.Collections.ObjectModel;
using ZATCAMAUI.Models.EDeclerationsModel;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct
{
    public partial class BaseProductDeclarationViewModel
    {

        #region Properties

        bool isProductTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductTypes;
        ProductTypesModel selectedProductTypes;
        public ProductTypesModel SelectedProductTypes { get { return selectedProductTypes; } set { selectedProductTypes = value; OnPropertyChanged(); } }


        bool isProductItemHaveSubType;
        public bool IsProductItemHaveSubType { get { return isProductItemHaveSubType; } set { isProductItemHaveSubType = value; OnPropertyChanged(); } }


        bool isProductSubTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductSubTypes;
        ProductTypesModel selectedProductSubTypes;
        public ProductTypesModel SelectedProductSubTypes { get { return selectedProductSubTypes; } set { selectedProductSubTypes = value; OnPropertyChanged(); } }

        private int countElectronicDevices = 0;
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
                        SelectedProductSubTypes = null;
                        if (ProductTypes == null || ProductTypes.Count > 0)
                        {
                            var productTypes = await DeclerationServices.GetProductTypes();
                            if(!string.IsNullOrWhiteSpace(productTypes?.Item3))
                            {
                                IsShowMsgView = true;
                                MessageTxt = productTypes?.Item3;
                                return;
                            }
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
                    catch (Exception)
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
                                if (!string.IsNullOrWhiteSpace(productSubTypes?.Item3))
                                {
                                    IsShowMsgView = true;
                                    MessageTxt = productSubTypes?.Item3;
                                    return;
                                }
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
                    catch (Exception)
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
            try
            {
                if (CheckProductDataNotNull())
                {
                    if (int.Parse(Quantity ?? "0") <= 0)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.QuantityValidation;
                        return;
                    }
                    if (Double.Parse(TotalValue) <= 3000)
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
                        value = double.Parse(TotalValue)

                    };

                    if (SelectedProductSubTypes?.code == "851712000001" ||
                        SelectedProductSubTypes?.code == "847130000002" ||
                        SelectedProductSubTypes?.code == "847130000003")
                    {
                        countElectronicDevices = int.Parse(Quantity ?? "0");
                    }

                    SubmitModel.travelerDeclaration.product.Add(item);

                    var cardItem = new EDeclerationCardModel()
                    {
                        Name = SelectedProductTypes.Name,
                        desc = IsProductItemHaveSubType ? SelectedProductSubTypes.Name : string.Empty,
                        Price = $"{TotalValue} {AppResources.ZSAR}",
                        QTY = Quantity,
                        ID = item.ID,
                        Type = 2
                    };
                    Models.EDeclerationsModel.FeesCalculators.Product product = new Models.EDeclerationsModel.FeesCalculators.Product()
                    {
                        typeName = IsProductItemHaveSubType ? SelectedProductSubTypes.Name : SelectedProductTypes.Name,
                        harmonizedCode = IsProductItemHaveSubType ? SelectedProductSubTypes.code : item.itemCode,
                        value = double.Parse(TotalValue),
                        count = int.Parse(Quantity ?? "0"),
                        ID = item.ID
                    };
                    var fees= await CalculateFees(1, null, product);
                    if (!fees)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.ServerError;
                        return;
                    }
                       
                    CardData.Add(cardItem);
                    ClearProductData();

                }
            }
            catch (Exception)
            {

            }
           
        }

        protected void ClearProductData()
        {
            SelectedProductTypes = null;
            SelectedProductSubTypes = null;
            Quantity = null;
            IsProductItemHaveSubType = false;
            TotalValue = null;
        }

        protected bool CheckProductDataNotNull(bool fromFees=false)
        {
            // Make sure total electronic devices only don't exceed 2
            if ((SelectedProductSubTypes?.code == "851712000001" ||
                SelectedProductSubTypes?.code == "847130000002" ||
                SelectedProductSubTypes?.code == "847130000003") && (int.Parse(Quantity ?? "0") + countElectronicDevices) > 2)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.ElectronicDevicesDisc;
                return false;
            }

            else if (string.IsNullOrWhiteSpace(Quantity) && fromFees == false)
            {
                DisplayRequiredDataMsg();
                return false;
            }
            else if (SelectedProductTypes != null && !string.IsNullOrWhiteSpace(TotalValue))
            {
                if (IsProductItemHaveSubType && SelectedProductSubTypes == null)
                {
                    DisplayRequiredDataMsg();
                    return false;
                }
            }
            return true;
        }

        public async Task GetProducts(bool includeTobaco = false)
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
                SelectedProductSubTypes = null;
                isUnitsSelected = false;
                if (ProductTypes == null || ProductTypes.Count > 0)
                {
                    var productTypes = await DeclerationServices.GetProductTypes();
                    if (!string.IsNullOrWhiteSpace(productTypes?.Item3))
                    {
                        IsShowMsgView = true;
                        MessageTxt = productTypes?.Item3;
                        return;
                    }
                    ProductTypes = productTypes?.Item1.data;
                    ProductSubTypes = null;
                }

                var result = ProductTypes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList();


                if (includeTobaco)
                {
                    result.Add(new BottomSheetModel() { Id = AppResources.Tobaco, Name = AppResources.Tobaco });
                    var newLst = result;
                }
                BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                IsShowBottomSheet = true;
                HeaderTitle = AppResources.TypeItem;
                TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}

