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
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using Xamarin.Forms;
using GAZT;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ProductDeclarationViewModel : BaseEDeclarationViewModel
    {
        #region Properties
        bool isTobacoTypeSelected = false;
        bool isTobacotemSelected = false;
        static ObservableCollection<TobaccoItemsModel> TobacoItems;
        static ObservableCollection<TobacoTypesModel> TobacoTypes;
        bool isProductTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductTypes;
        ProductTypesModel selectedProductTypes;
        public ProductTypesModel SelectedProductTypes { get { return selectedProductTypes; } set { selectedProductTypes = value; RaisePropertyChanged(); } }

        bool isMaterialTypeSelected = false;
        static ObservableCollection<CoinTypesModel> MaterialTypes;
        CoinTypesModel selectedMaterialTypes;
        public CoinTypesModel SelectedMaterialTypes { get { return selectedMaterialTypes; } set { selectedMaterialTypes = value; RaisePropertyChanged(); } }

        bool isPurposeSelected = false;
        static ObservableCollection<PurposeModel> Purposes;
        PurposeModel selectedPurposes;
        public PurposeModel SelectedPurposes { get { return selectedPurposes; } set { selectedPurposes = value; RaisePropertyChanged(); } }


        bool isProductSubTypeSelected = false;
        static ObservableCollection<ProductTypesModel> ProductSubTypes;
        ProductTypesModel selectedProductSubTypes;
        public ProductTypesModel SelectedProductSubTypes { get { return selectedProductSubTypes; } set { selectedProductSubTypes = value; RaisePropertyChanged(); } }

        bool isCurrencySelected = false;
        static ObservableCollection<CurrencyModel> Currencies;
        CurrencyModel selectedCurrencie;
        public CurrencyModel SelectedCurrencie { get { return selectedCurrencie; } set { selectedCurrencie = value; RaisePropertyChanged(); } }

        bool isUnitsSelected = false;
        static ObservableCollection<UnitsModel> Units;
        UnitsModel selectedUnit;
        public UnitsModel SelectedUnit { get { return selectedUnit; } set { selectedUnit = value; RaisePropertyChanged(); } }


        TobaccoItemsModel selectedTobacoItem;
        public TobaccoItemsModel SelectedTobacoItem { get { return selectedTobacoItem; } set { selectedTobacoItem = value; RaisePropertyChanged(); } }
  
        string quantity;
        public string Quantity { get { return quantity; } set { quantity = value; RaisePropertyChanged(); } }

        string restrictedItem;
        public string RestrictedItem { get { return restrictedItem; } set { restrictedItem = value; RaisePropertyChanged(); } }


        int? totalValue;
        public int? TotalValue { get { return totalValue; } set { totalValue = value; RaisePropertyChanged(); } }


        string question;
        public string Question { get { return question; } set { question = value; RaisePropertyChanged(); } }

        string otherPurpose;
        public string OtherPurpose { get { return otherPurpose; } set { otherPurpose = value; RaisePropertyChanged(); } }

        TobacoTypesModel selectedTobacoType;
        public TobacoTypesModel SelectedTobacoType { get { return selectedTobacoType; } set { selectedTobacoType = value; RaisePropertyChanged(); } }


        int qFlow = 1;
        public int QFlow { get { return qFlow; } set { qFlow = value; RaisePropertyChanged(); } }

        bool isPermit ;
        public bool IsPermit { get { return isPermit; } set { isPermit = value; RaisePropertyChanged(); } }

        bool isProductItemHaveSubType;
        public bool IsProductItemHaveSubType { get { return isProductItemHaveSubType; } set { isProductItemHaveSubType = value; RaisePropertyChanged(); } }

        FeesCalculatorResponse feesCalculatorResponse;
        public FeesCalculatorResponse FeesCalculatorResponse { get { return feesCalculatorResponse; } set { feesCalculatorResponse = value; RaisePropertyChanged(); } }

        FeesCalculatorBody FeesCalculatorBody = new FeesCalculatorBody();

        ObservableCollection<EDeclerationCardModel> cardData=new ObservableCollection<EDeclerationCardModel>();
        public ObservableCollection<EDeclerationCardModel> CardData { get { return cardData; } set { cardData = value; RaisePropertyChanged(); } }

        
        #endregion


        #region Commands
        public ICommand OpenCartCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    EDeclarationCartPopUpPage poupWindow = new EDeclarationCartPopUpPage();
                    await PopupNavigation.Instance.PushAsync(poupWindow);

                });
            }
        }

        public ICommand CloseCartCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PopAsync(true);
                });
            }
        }
        public ICommand SelectedProductCommand
        {
            get
            {
                return new Command<object>((e) =>
                {

                });
            }
        }
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
                        if (TobacoTypes == null || TobacoTypes.Count > 0)
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
                        if (CheckNoItemAddedToCart())
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.EDeclerationNoItemAddedToCartMsg;
                            return;
                        }
                        if (IsArrivingPlaneSelected&&FeesCalculatorResponse?.totalPayment<3000&&QFlow==2)
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.EDeclerationenteredValuedoesnotrequirethedeclaration;
                            return;
                        }
                        if (QFlow < 4)
                        {
                            IsYesSelected = true;
                            QFlow++;
                            SetQuestion();
                        }
                        else
                        {
                            if (SubmitModel.travelerDeclaration.tobacco.Count < 1&& SubmitModel.travelerDeclaration.product.Count < 1&& SubmitModel.travelerDeclaration.restricted.Count < 1&& SubmitModel.travelerDeclaration.currency.Count < 1)
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.ALLanswersisNoMsg;
                                return;
                            }
                            _navigationService.NavigateTo("PassengerInformationPage");
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

        private bool CheckNoItemAddedToCart()
        {
            switch (QFlow)
            {
                case 1:
                    if (IsYesSelected && SubmitModel.travelerDeclaration.tobacco.Count < 1)
                    {
                        return true;
                    }
                    break;
                  case 2:
                    if (IsYesSelected && SubmitModel.travelerDeclaration.product.Count < 1)
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (IsYesSelected && SubmitModel.travelerDeclaration.currency.Count < 1)
                    {
                        return true;
                    }
                    break;
                case 4:
                    if (IsYesSelected && SubmitModel.travelerDeclaration.restricted.Count < 1)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
                    
            }
            return false;
        }

        public void SetQuestion()
        {
            switch (QFlow)
            {
                case 1:
                    Question = AppResources.ProductDeclarationSubTitle;
                    break;
                case 2:
                    Question = AppResources.EdeclerationSecQ;
                    break;
                case 3:
                    Question = AppResources.DeclerationFirstSecurityQuestion;
                    break;
                case 4:
                    Question = AppResources.DeclerationSecondSecurityQuestion;
                    break;
                default:
                    break;
            }
        }

        private void ClearData()
        {
            switch (QFlow)
            {
                case 1:
                    ClearTobacoData();
                    break;
                case 2:
                    ClearProductData();
                    break;
                case 3:
                    ClearCurrencyData();
                    break;
                case 4:
                 
                    break;
                default:
                    break;
            }
            TotalValue = null;
        }
        public ICommand PrevoiusCommand
        {
            get
            {

                return new Command(() =>
                {
                    try
                    {
                        if (IsShowBottomSheet)
                        {
                            IsShowBottomSheet = false;
                            HeaderTitle = AppResources.eDeclaration;
                            return;
                        }
                        ClearData();
                        if (QFlow > 1)
                        {
                            if (!IsArrivingPlaneSelected && QFlow == 3)
                            {
                                FeesCalculatorResponse = new FeesCalculatorResponse();
                                CardData = new ObservableCollection<EDeclerationCardModel>();
                                SubmitModel.travelerDeclaration = new TravelerDeclaration();
                                _navigationService.GoBack();

                                return;
                            }
                            QFlow--;
                            SetQuestion();
                        }
                        else
                        {
                            _navigationService.GoBack();
                            FeesCalculatorResponse = new FeesCalculatorResponse();
                            CardData = new ObservableCollection<EDeclerationCardModel>();
                            SubmitModel.travelerDeclaration = new TravelerDeclaration();

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
        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" }, 1);
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

        public ICommand OpenMaterialTypesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isMaterialTypeSelected = true;
                        if (MaterialTypes == null || MaterialTypes.Count > 0)
                        {
                            var materialTypes = await DeclerationServices.GetCoinTypes();
                            MaterialTypes = materialTypes?.Item1.data;
                        }

                        var result = MaterialTypes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.MaterialType;
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

        public ICommand OpenPurposesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isPurposeSelected = true;
                        if (Purposes == null || Purposes.Count > 0)
                        {
                            var purposes = await DeclerationServices.GetPurposes();
                            Purposes = purposes?.Item1.data;
                        }

                        var result = Purposes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.Purpose;
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
                        if (Currencies == null || Currencies.Count > 0)
                        {
                            var currencies = await DeclerationServices.GetCurrencies();
                            Currencies = currencies?.Item1.data;
                        }

                        var result = Currencies.Select(c => new BottomSheetModel() { Id = c.currencyCode.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.Purpose;
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

        public ICommand OpenUnitsCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        isUnitsSelected = true;
                        if (Units == null || Units.Count > 0)
                        {
                            var units = await DeclerationServices.GetUnits();
                            Units = units?.Item1.data;
                        }

                        var result = Units.Select(c => new BottomSheetModel() { Id = c.id.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.units;
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
        void ClearTobacoData()
        {
            SelectedTobacoItem = null;
            SelectedTobacoType = null;
            Quantity = "";
        }
        bool CheckTobacoDataNotNull()
        {
            if (SelectedTobacoItem!=null&& SelectedTobacoType!=null&&!String.IsNullOrWhiteSpace(Quantity))
            {
                return true;
            }
            return false;
        }
        bool CheckCurrencyDataNotNull()
        {
            if (SelectedCurrencie != null && SelectedMaterialTypes != null&& SelectedPurposes!=null&&TotalValue!=null )
            {
                return true;
            }
            return false;
        }
        bool CheckrestrictedDataNotNull()
        {
            if (SelectedCurrencie != null && SelectedPurposes != null && TotalValue != null && SelectedUnit!=null && !String.IsNullOrWhiteSpace(Quantity)&&int.Parse(Quantity)>0 && !String.IsNullOrWhiteSpace(RestrictedItem))
            {
                return true;
            }
            return false;
        }
        bool CheckProductDataNotNull()
        {
            if (SelectedProductTypes != null && !String.IsNullOrWhiteSpace(Quantity)&&TotalValue!=null)
            {
                if (IsProductItemHaveSubType&&SelectedProductSubTypes==null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        void ClearCurrencyData()
        {
            SelectedCurrencie = null;
            SelectedMaterialTypes = null;
            Quantity=OtherPurpose = "";
            SelectedPurposes = null;
            TotalValue = null;
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

                            IsLoading = true;
                            if (TobacoItems == null || TobacoItems.Count > 0)
                            {
                                var topacoTypes = await DeclerationServices.GetTobacoItem(int.Parse(SelectedTobacoType.typeID));
                                TobacoItems = topacoTypes?.Item1.data;
                            }

                            var result = TobacoItems.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
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
                            SelectedTobacoItem = TobacoItems.First(c => c.ID.ToString() == e.Id);
                            isTobacotemSelected = false;
                        }
                        else if (isProductTypeSelected)
                        {
                            SelectedProductTypes = ProductTypes.First(c => c.ID == int.Parse(e.Id));
                            if (String.IsNullOrWhiteSpace(SelectedProductTypes.code))
                            {
                                IsProductItemHaveSubType=true;
                            }
                            else
                            {
                                IsProductItemHaveSubType = false;
                            }
                            isProductTypeSelected = false;
                        }
                        else if (isProductSubTypeSelected)
                        {
                            SelectedProductSubTypes = ProductSubTypes.First(c => c.ID == int.Parse(e.Id));
                            isProductSubTypeSelected = false;
                        }
                        else if (isMaterialTypeSelected)
                        {
                            SelectedMaterialTypes = MaterialTypes.First(c => c.ID == int.Parse(e.Id));
                            isMaterialTypeSelected = false;
                        }
                        else if (isPurposeSelected)
                        {
                            SelectedPurposes = Purposes.First(c => c.ID == int.Parse(e.Id));
                            isPurposeSelected = false;
                        }
                        else if (isCurrencySelected)
                        {
                            SelectedCurrencie = Currencies.First(c => c.currencyCode == int.Parse(e.Id));
                            isCurrencySelected = false;
                        }
                        else if (isUnitsSelected)
                        {
                            SelectedUnit = Units.First(c => c.id == int.Parse(e.Id));
                            isUnitsSelected = false;
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

                return new Command(async() =>
                {
                    try
                    {
                        IsLoading = true;
                        switch (qFlow)
                        {
                            case 1:
                               await AddTobacoItem();
                                break;
                            case 2:
                               await AddProductItem();
                                break;
                            case 3:
                                AddCurrency();
                                break;
                            case 4:
                                AddRestricted();
                                break;
                            default:
                                break;
                        }
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

        public ICommand RemoveItemFromCardCommand
        {
            get
            {

                return new Command<EDeclerationCardModel>(async (e) =>
                {
                    try
                    {
                        IsLoading = true;
                        switch (e.Type)
                        {
                            case 1:
                              var tobaco= SubmitModel.travelerDeclaration.tobacco.First(c=>c.ID==e.ID);
                                var tobacofess = FeesCalculatorBody.tobacco?.First(c => c.ID == e.ID);
                               await CalculateFees(2, tobacofess);
                                SubmitModel.travelerDeclaration.tobacco.Remove(tobaco);
                                   break;
                            case 2:
                                var product = SubmitModel.travelerDeclaration.product.First(c => c.ID == e.ID);
                                var productfess = FeesCalculatorBody.product?.First(c => c.ID == e.ID);
                                await CalculateFees(2,null ,productfess);
                                SubmitModel.travelerDeclaration.product.Remove(product);
                                break;
                            case 3:
                                var currency = SubmitModel.travelerDeclaration.currency.First(c => c.ID == e.ID);
                                SubmitModel.travelerDeclaration.currency.Remove(currency);
                                break;
                            case 4:
                                var restricted = SubmitModel.travelerDeclaration.restricted.First(c => c.ID == e.ID);
                                SubmitModel.travelerDeclaration.restricted.Remove(restricted);
                                break;
                            default:
                                break;
                        }
                        CardData.Remove(e);
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


        public ICommand SelectIsPermitCommand
        {
            get
            {

                return new Command<string>((e) =>
                {
                    try
                    {
                        IsPermit =e=="0"?false:true;
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
        private  void AddCurrency()
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
                     ID=item.ID,
                     Type=3
                };
                CardData.Add(cardItem);
               
                ClearCurrencyData();
            }
            else
            {
                DisplayRequiredDataMsg();
            }
        }

        private void AddRestricted()
        {
            if (CheckrestrictedDataNotNull())
            {

         
            var item = new Models.EDeclerationsModel.SubmitModels.Restricted()
            {
                otherpurpose = OtherPurpose,
                typeName = RestrictedItem,
                unit = SelectedUnit.id,
                count = int.Parse(Quantity),
                value = TotalValue,
                purpose = SelectedPurposes.ID,
                currencyName = SelectedCurrencie.Name,
                currency = SelectedCurrencie.currencyCode,
                permit = IsPermit,
                attachment =UploadedFiles!=null&&UploadedFiles.Count>0? UploadedFiles[0].fileBase64:""
            };
                var cardItem = new EDeclerationCardModel()
                {
                    Name = item.typeName,
                    desc = SelectedPurposes.Name,
                    Price = TotalValue.ToString(),
                    ID = item.ID,
                    Type=4
                };
                CardData.Add(cardItem);
                SubmitModel.travelerDeclaration.restricted.Add(item);
            ClearRestrictedData();
         }
            else
            {
                DisplayRequiredDataMsg();
            }
        }
        void ClearRestrictedData()
        {
            SelectedCurrencie = null;
            SelectedMaterialTypes = null;
            Quantity = OtherPurpose = "";
            IsPermit = false;
            selectedUnit = null;
            UploadedFiles = null;
            SelectedPurposes = null;
            TotalValue = null;
        }
        private async Task AddTobacoItem()
        {
            if (CheckTobacoDataNotNull())
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
                var cardItem = new EDeclerationCardModel()
                {
                    Name = item.typeName,
                    desc = item.subTypeName,
                  //  Price = TotalValue.ToString(),
                    ID = item.ID,
                    Type=1
                };

              SubmitModel.travelerDeclaration.tobacco.Add(item);
             CardData.Add(cardItem);
            Models.EDeclerationsModel.FeesCalculators.Tobacco tobao = new Models.EDeclerationsModel.FeesCalculators.Tobacco()
            {
                harmonizedCode = item.itemCode.ToString(),
                count = int.Parse(Quantity),
                sequence = item.taxSequence,
                ID=item.ID
            };
           await CalculateFees(1,tobao, null);
            ClearTobacoData();
            }
            else
            {
                DisplayRequiredDataMsg();
            }
        }

        private void DisplayRequiredDataMsg()
        {
            IsShowMsgView = true;
            MessageTxt = AppResources.RequiredData;
        }



        private async Task AddProductItem()
        {
            if (CheckProductDataNotNull())
            {

            var item = new Models.EDeclerationsModel.SubmitModels.Product()
            {
                count = int.Parse(Quantity),
                typeName = IsProductItemHaveSubType ? SelectedProductSubTypes.Name : SelectedProductTypes.Name,
                itemCode = IsProductItemHaveSubType ? SelectedProductSubTypes.code : SelectedProductTypes.code,
                value = TotalValue

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
                value = TotalValue,
                 ID = item.ID
                };
                CardData.Add(cardItem);
           await CalculateFees(1,null, product);
               ClearProductData();

            }
            else
            {
                DisplayRequiredDataMsg();
            }
        }
        void ClearProductData()
        {
            SelectedProductTypes = null;
            SelectedProductSubTypes = null;
            Quantity = "";
            TotalValue = null;
        }
        private async Task CalculateFees(int operation=1,Models.EDeclerationsModel.FeesCalculators.Tobacco tobacco = null, Models.EDeclerationsModel.FeesCalculators.Product product = null)
        {
            try
            {

           
            if (tobacco != null)
            {
                    if (operation==1)
                    {
  FeesCalculatorBody.tobacco.Add(tobacco);
                ClearTobacoData();
                    }
                    else
                    {
                        FeesCalculatorBody.tobacco.Remove(tobacco);
                    }
              
            }
            if (product != null)
            {
                    if (operation==1)
                    {
 FeesCalculatorBody.product.Add(product);
                ClearProductData();
                    }
                    else
                    {
                        FeesCalculatorBody.product.Remove(product);

                    }

                }
            var calres = await DeclerationServices.FeesCalculator(FeesCalculatorBody);
            if (calres.IsSuccessStatusCode)
            {
                var conent = await calres.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DATAPowerBaseResponseResult<FeesCalculatorResponse>>(conent);
                if (data.result != null)
                {
                    FeesCalculatorResponse = data.result;
                }

            }
            }
            catch (Exception ex)
            {

            }

        }
        public ICommand SearchEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {
                        var entry = e as BorderlessEntry;
                        var value = entry.Text.ToLower();
                        if (string.IsNullOrWhiteSpace(value))
                            BottomSheetList = TempBottomSheetList;
                        else
                        {
                            var result = BottomSheetList.Where(s => s.Name.Contains(value)).ToList() ?? new List<BottomSheetModel>();
                            BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        }
                    }
                });
            }
        }

        private async Task AddItemToCart(EDeclerationCardModel eDeclerationCardModel)
        {
            CardData.Add(eDeclerationCardModel);
        }

        #endregion
        public ProductDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices DeclerationServices) : base(navigationService, dialogService, DeclerationServices)
        {
            
            
        }
    }
}

