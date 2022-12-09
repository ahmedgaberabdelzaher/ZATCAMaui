using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.BaseModels;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ProductDeclarationViewModel: BaseEDeclarationViewModel
    {
        #region Properties
        //bool isArrivingPlaneSelected = true;
        //public bool Te { get { return isArrivingPlaneSelected; } set { isArrivingPlaneSelected = value; RaisePropertyChanged(); } }
         bool isTobacoTypeSelected = false;
         bool isTobacotemSelected = false;
        static ObservableCollection<TobaccoItemsModel> TobacoItems;
        static ObservableCollection<TobacoTypesModel> TobacoTypes;
        bool isProductTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductTypes;
        ProductTypesModel selectedProductTypes;
        public ProductTypesModel SelectedProductTypes { get { return selectedProductTypes; } set { selectedProductTypes = value; RaisePropertyChanged(); } }

        bool isProductSubTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductSubTypes;
        ProductTypesModel selectedProductSubTypes;
        public ProductTypesModel SelectedProductSubTypes { get { return selectedProductSubTypes; } set { selectedProductSubTypes = value; RaisePropertyChanged(); } }

        TobaccoItemsModel selectedTobacoItem;
        public TobaccoItemsModel SelectedTobacoItem { get { return selectedTobacoItem; } set { selectedTobacoItem = value; RaisePropertyChanged(); } }
        EDeclerationSubmitModel SubmitModel=new EDeclerationSubmitModel();
        string quantity;
        public string Quantity { get { return quantity; } set { quantity = value; RaisePropertyChanged(); } }

        int totalValue;
        public int TotalValue { get { return totalValue; } set { totalValue = value; RaisePropertyChanged(); } }


        string question;
        public string Question { get { return question; } set { question = value; RaisePropertyChanged(); } }

        TobacoTypesModel selectedTobacoType;
        public TobacoTypesModel SelectedTobacoType { get { return selectedTobacoType; } set { selectedTobacoType = value; RaisePropertyChanged(); } }

        int qFlow = 1;
        public int QFlow { get { return qFlow; } set { qFlow = value;RaisePropertyChanged(); }}

        FeesCalculatorResponse feesCalculatorResponse ;
        public FeesCalculatorResponse FeesCalculatorResponse { get { return feesCalculatorResponse; } set { feesCalculatorResponse = value; RaisePropertyChanged(); }}

        FeesCalculatorBody FeesCalculatorBody = new FeesCalculatorBody();
        #endregion


        #region Commands
        //public ICommand Tes
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {

        //        });
        //    }
        //}
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
                        if (TobacoTypes==null||TobacoTypes.Count>0)
                        {
            var topacoTypes = await DeclerationServices.GetTobacoTypes();
                            TobacoTypes = topacoTypes?.Item1.data;
                            TobacoItems = null;
                        }

                        var result = TobacoTypes.Select(c => new BottomSheetModel() { Id = c.typeID, Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.TypeItem;
                        TempBottomSheetList = BottomSheetList;
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

        public ICommand NextCommand
        {
            get
            {

                return new Command<string>((QNo) =>
                {
                    try
                    {
                        QFlow++;
                        SetQuestion();

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

        private void SetQuestion()
        {
            switch (QFlow)
            {
                case 1:
                    Question = AppResources.ProductDeclarationSubTitle;
                    break;
                case 2:
                    Question = AppResources.EdeclerationSecQ;
                    break;
                default:
                    break;
            }
        }

        public ICommand BackCommandCommand
        {
            get
            {

                return new Command(() =>
                {
                    try
                    {
                        QFlow--;
                        SetQuestion();
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
                        if (ProductTypes == null || ProductTypes.Count > 0)
                        {
                            var productTypes = await DeclerationServices.GetProductTypes();
                            ProductTypes = productTypes?.Item1.data;
                            ProductSubTypes = null;
                        }

                        var result = ProductTypes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.TypeItem;
                        TempBottomSheetList = BottomSheetList;
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

                            IsLoading = true;
                            if (ProductSubTypes == null || ProductSubTypes.Count > 0)
                            {
                                var productSubTypes = await DeclerationServices.GetProductSubTypes(SelectedProductTypes.ID.ToString());
                                ProductSubTypes = productSubTypes?.Item1.data;
                            }

                            var result = ProductSubTypes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            IsShowBottomSheet = true;
                            HeaderTitle = AppResources.ProductName;
                            TempBottomSheetList = BottomSheetList;
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

        void ClearTobacoData()
        {
            SelectedTobacoItem = null;
            SelectedTobacoType = null;
            Quantity = "";
        }
        public ICommand OpenTobacoItemssCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        if (SelectedTobacoType!=null)
                        {
                            isTobacotemSelected = true;
                           
                        IsLoading = true;
                            if (TobacoItems == null || TobacoItems.Count > 0)
                            {
                                var topacoTypes = await DeclerationServices.GetTobacoItem(int.Parse(SelectedTobacoType.typeID));
                                TobacoItems = topacoTypes?.Item1.data;
                            }
                         
                            var result = TobacoItems.Select(c => new BottomSheetModel() { Id = c.itemCode, Name = c.productName }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ProductName;
                        TempBottomSheetList = BottomSheetList;
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

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>(async (e) =>
                {
                    try
                    {
                        IsLoading = true;

                        if (isTobacoTypeSelected)
                        {
                            SelectedTobacoType = TobacoTypes.First(c => c.typeID == e.Id);
                            isTobacoTypeSelected = false;
                        }
                        else if (isTobacotemSelected)
                        {
                            SelectedTobacoItem = TobacoItems.First(c => c.itemCode == e.Id);
                            isTobacotemSelected = false;
                        }
                        else if (isProductTypeSelected)
                        {
                            SelectedProductTypes = ProductTypes.First(c => c.ID == int.Parse(e.Id));
                            isProductTypeSelected = false;
                        }
                        else if (isProductSubTypeSelected)
                        {
                            SelectedProductSubTypes = ProductSubTypes.First(c => c.ID == int.Parse(e.Id));
                            isProductSubTypeSelected = false;
                        }
                        IsShowBottomSheet = false;
                        HeaderTitle = AppResources.eDeclaration;
                        SearchText = string.Empty;
                        TempBottomSheetList = BottomSheetList;
                        IsLoading = false;
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }


                });
            }
        }

        public ICommand AddCommand
        {
            get
            {

                return new Command(() =>
                {
                    try
                    {
                        IsLoading = true;
                        switch (qFlow)
                        {
                            case 1:
                                AddTobacoItem();
                                break;
                            case 2:
                                AddProductItem();
                                break; 
                            default:
                                break;
                        }
                        AddTobacoItem();
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

        private void AddTobacoItem()
        {
            var item = new Models.EDeclerationsModel.SubmitModels.Tobacco()
            {
                count = int.Parse(Quantity),
                typeName = SelectedTobacoType.Name,
                itemCode = long.Parse(selectedTobacoItem.itemCode),
                measurementUnit = selectedTobacoItem.measurementUnit,
                subTypeName = selectedTobacoItem.productName,
                taxSequence = selectedTobacoItem.taxSequence
            };
           /* if (SubmitModel.travelerDeclaration == null)
            {
                SubmitModel.travelerDeclaration = new TravelerDeclaration()
                {
                    tobacco = new List<Models.EDeclerationsModel.SubmitModels.Tobacco>
                    ()
                };
            }*/
            SubmitModel.travelerDeclaration.tobacco.Add(item);
            Models.EDeclerationsModel.FeesCalculators.Tobacco tobao = new Models.EDeclerationsModel.FeesCalculators.Tobacco()
            {
                harmonizedCode = item.itemCode.ToString(),
                 count=int.Parse(Quantity),
                  sequence=item.taxSequence
             };
            CalculateFees(tobao, null);
            ClearTobacoData();
        }

        private void AddProductItem()
        {
            var item = new Models.EDeclerationsModel.SubmitModels.Product()
            {
                count = int.Parse(Quantity),
                typeName = SelectedProductTypes.Name,
                itemCode = SelectedProductTypes.code == null ? 0 :long.Parse(SelectedProductTypes.code),
                value=totalValue
            
            };
           /* if (SubmitModel.travelerDeclaration == null|| SubmitModel.travelerDeclaration.product==null)
            {
                SubmitModel.travelerDeclaration = new TravelerDeclaration()
                {
                    product = new List<Models.EDeclerationsModel.SubmitModels.Product>
                    ()
                };
            }*/

            SubmitModel.travelerDeclaration.product.Add(item);
            Models.EDeclerationsModel.FeesCalculators.Product product = new Models.EDeclerationsModel.FeesCalculators.Product()
            {
                harmonizedCode = item.itemCode==null?0:item.itemCode.Value,
                value = TotalValue
            };
             CalculateFees(null, product);
           // ClearProductData();
        }
        void ClearProductData()
        {
            SelectedProductTypes = null;
            SelectedProductSubTypes = null;
            Quantity = "";
            TotalValue = 0;
        }
        private async Task CalculateFees(Models.EDeclerationsModel.FeesCalculators.Tobacco tobacco=null, Models.EDeclerationsModel.FeesCalculators.Product product=null )
        {
            if (tobacco!=null)
            {
                FeesCalculatorBody.tobacco.Add(tobacco);
                ClearTobacoData();
            }
            if (product!=null)
            {
                FeesCalculatorBody.product.Add(product);
                ClearProductData();
            }
            var calres =await DeclerationServices.FeesCalculator(FeesCalculatorBody);
            if (calres.IsSuccessStatusCode)
            {
                var conent = await calres.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DATAPowerBaseResponseResult<FeesCalculatorResponse>>(conent);
                if (data.result!=null)
                {
                    FeesCalculatorResponse = data.result;
                }
                
            }
          
        }


        #endregion
        public ProductDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices DeclerationServices) : base(navigationService, dialogService,DeclerationServices)
        {
            Question = AppResources.ProductDeclarationSubTitle;
        }
    }
}

