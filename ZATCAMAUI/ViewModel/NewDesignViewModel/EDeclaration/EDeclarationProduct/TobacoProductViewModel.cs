using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EDeclerationsModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct
{
    public partial class BaseProductDeclarationViewModel
    {

        #region Properties
        bool isTobacoTypeSelected = false;
        bool isTobacotemSelected = false;
        static ObservableCollection<TobaccoItemsModel> TobacoItems;
        static ObservableCollection<TobacoTypesModel> TobacoTypes;

        TobaccoItemsModel selectedTobacoItem;
        public TobaccoItemsModel SelectedTobacoItem { get { return selectedTobacoItem; } set { selectedTobacoItem = value; OnPropertyChanged(); } }


        TobacoTypesModel selectedTobacoType;
        public TobacoTypesModel SelectedTobacoType { get { return selectedTobacoType; } set { selectedTobacoType = value; OnPropertyChanged(); } }

        string weight;
        public string Weight { get { return weight; } set { weight = value; OnPropertyChanged(); } }

        bool isWeighVisible = false;
        public bool IsWeighVisible { get { return isWeighVisible; } set { isWeighVisible = value; OnPropertyChanged(); } }


        #endregion

        #region Commands

        public ICommand OpenTobacoTypesCommand
        {
            get
            {

                return new Command(async () =>
                {

                    try
                    {
                        IsLoading = true;
                        isTobacoTypeSelected = true;
                        isTobacotemSelected = false;
                        isProductTypeSelected = false;
                        isProductSubTypeSelected = false;
                        isMaterialTypeSelected = false;
                        isPurposeSelected = false;
                        isCurrencySelected = false;
                        isUnitsSelected = false;
                        if (TobacoTypes == null || TobacoTypes.Count > 0)
                        {
                            var topacoTypes = await DeclerationServices.GetTobacoTypes();
                            if (!string.IsNullOrWhiteSpace(topacoTypes?.Item3))
                            {
                                IsShowMsgView = true;
                                MessageTxt = topacoTypes?.Item3;
                                return;
                            }
                            TobacoTypes = topacoTypes?.Item1.data;
                            TobacoItems = null;
                        }

                        var result = TobacoTypes.Select(c => new BottomSheetModel() { Id = c.typeID, Name = c.Name });
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


        public ICommand OpenTobacoItemssCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        if (SelectedTobacoType != null)
                        {
                            isTobacotemSelected = true;
                            isUnitsSelected = false;
                            isCurrencySelected = false;
                            isPurposeSelected = false;
                            isMaterialTypeSelected = false;
                            isProductSubTypeSelected = false;
                            isProductTypeSelected = false;
                            isTobacoTypeSelected = false;
                            IsLoading = true;
                            if (TobacoItems == null || TobacoItems.Count > 0)
                            {
                                var topacoTypes = await DeclerationServices.GetTobacoItem(int.Parse(SelectedTobacoType.typeID));
                                if (!string.IsNullOrWhiteSpace(topacoTypes?.Item3))
                                {
                                    IsShowMsgView = true;
                                    MessageTxt = topacoTypes?.Item3;
                                    return;
                                }
                                TobacoItems = topacoTypes?.Item1.data;
                            }

                            var result = TobacoItems.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name });
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
        protected void ClearTobacoData()
        {
            SelectedTobacoItem = null;
            SelectedTobacoType = null;
            Quantity = weight = TotalValue = null;
            IsWeighVisible = false;
        }

        protected bool CheckTobacoDataNotNull()
        {
            if (SelectedTobacoItem != null && SelectedTobacoType != null && !string.IsNullOrWhiteSpace(Quantity) && !string.IsNullOrWhiteSpace(totalValue))
            {
                if (IsWeighVisible && string.IsNullOrWhiteSpace(Weight))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private async Task AddTobacoItem()
        {
            try
            {
                if (CheckTobacoDataNotNull())
                {

                    if (int.Parse(Quantity ?? "0") <= 0)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.QuantityValidation;
                        return;
                    }
                    var item = new Models.EDeclerationsModel.SubmitModels.Tobacco()
                    {
                        count = int.Parse(Quantity ?? "0"),
                        typeName = SelectedTobacoType.Name,
                        itemCode = long.Parse(selectedTobacoItem.itemCode),
                        measurementUnit = selectedTobacoItem.measurementUnit,
                        subTypeName = SelectedTobacoItem.Name,
                        value = double.Parse(TotalValue),
                        weight = string.IsNullOrWhiteSpace(Weight) ? 0 : double.Parse(Weight),
                        taxSequence = SelectedTobacoItem.taxSequence
                    };
                    var cardItem = new EDeclerationCardModel()
                    {
                        Name = item.typeName,
                        desc = item.subTypeName,
                        Price = $"{TotalValue} {AppResources.ZSAR}",
                        QTY = Quantity,
                        ID = item.ID,
                        Type = 1
                    };

                    SubmitModel.travelerDeclaration.tobacco.Add(item);
                    
                    Models.EDeclerationsModel.FeesCalculators.Tobacco tobao = new Models.EDeclerationsModel.FeesCalculators.Tobacco()
                    {
                        harmonizedCode = item.itemCode.ToString(),
                        count = int.Parse(Quantity ?? "0"),
                        sequence = item.taxSequence,
                        measurementUnit = item.measurementUnit,
                        typeName = item.typeName,
                        subTypeName = item.subTypeName,
                        ID = item.ID,
                        weight = string.IsNullOrWhiteSpace(Weight) ? 0 : double.Parse(Weight),
                        value = double.Parse(TotalValue)
                    };
                    var fees = await CalculateFees(1, tobao, null);
                    if (!fees)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.ServerError;
                        return;
                    }
                        
                    CardData.Add(cardItem);
                    ClearTobacoData();
                }
                else
                {

                    DisplayRequiredDataMsg();
                }
            }
            catch (Exception)
            {

            }

        }
        #endregion


    }
}

