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
using EGAZT.AppConfigurations;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct
{
    public partial class BaseProductDeclarationViewModel : BaseEDeclarationViewModel
    {
        #region Properties
      
        bool isMaterialTypeSelected = false;
        static ObservableCollection<CoinTypesModel> MaterialTypes;
        CoinTypesModel selectedMaterialTypes;
        public CoinTypesModel SelectedMaterialTypes { get { return selectedMaterialTypes; } set { selectedMaterialTypes = value; RaisePropertyChanged(); } }

        bool isPurposeSelected = false;
        static ObservableCollection<PurposeModel> Purposes;
        PurposeModel selectedPurposes;
        public PurposeModel SelectedPurposes { get { return selectedPurposes; } set { selectedPurposes = value; RaisePropertyChanged(); } }

        bool isUnitsSelected = false;
        static ObservableCollection<UnitsModel> Units;
        UnitsModel selectedUnit;
        public UnitsModel SelectedUnit { get { return selectedUnit; } set { selectedUnit = value; RaisePropertyChanged(); } }

        string quantity;
        public string Quantity { get { return quantity; } set { quantity = value; RaisePropertyChanged(); } }

        string totalValue;
        public string TotalValue { get { return totalValue; } set { totalValue = value; RaisePropertyChanged(); } }


        string question;
        public string Question { get { return question; } set { question = value; RaisePropertyChanged(); } }

        string otherPurpose;
        public string OtherPurpose { get { return otherPurpose; } set { otherPurpose = value; RaisePropertyChanged(); } }

        int qFlow;
        public int QFlow { get { return qFlow; } set { qFlow = value; RaisePropertyChanged(); } }

      
        FeesCalculatorResponse feesCalculatorResponse;
        public FeesCalculatorResponse FeesCalculatorResponse { get { return feesCalculatorResponse; } set { feesCalculatorResponse = value; RaisePropertyChanged(); } }

        public FeesCalculatorBody FeesCalculatorBody = new FeesCalculatorBody();

        ObservableCollection<EDeclerationCardModel> cardData = new ObservableCollection<EDeclerationCardModel>();
        public ObservableCollection<EDeclerationCardModel> CardData { get { return cardData; } set { cardData = value; RaisePropertyChanged(); } }

        ObservableCollection<QuestionModel> questionList = new ObservableCollection<QuestionModel>
         {
             new QuestionModel{QuestionName = AppResources.EdeclerationTobbacoQuestion ,QuestionId =1},
             new QuestionModel{QuestionName = AppResources.EdeclerationProductQuestion ,QuestionId =2},
             new QuestionModel{QuestionName = AppResources.EDeclerationCurrencyQuestion ,QuestionId =3},
             new QuestionModel{QuestionName = AppResources.EDeclerationRestrictedQuestion ,QuestionId =4}
         };
        public ObservableCollection<QuestionModel> QuestionList { get { return questionList; } set { questionList = value; RaisePropertyChanged(); } }


        private List<int> selectedQuestionList = new List<int>();
        private int questionIndex = 0;
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
        public ICommand GoToSelectedQuestionsViewCommand
        {
            get
            {
                return new Command( _ =>
                {
                    if(selectedQuestionList.Count == 0)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.ALLanswersisNoMsg;
                        return;
                    }
                    selectedQuestionList.Sort();
                    QFlow = selectedQuestionList[questionIndex]; // Set First Item
                    _navigationService.NavigateTo("ProductDeclarationPage");
                   
                });
            }
        }

        public ICommand SelectedCheckBoxQuestionsCommand
        {
            get
            {
                return new Command<object>((e) =>
                {
                    try
                    {
                        if (e != null)
                        {
                            var question = e as QuestionModel;
                            question.IsChecked = !question.IsChecked;

                            if(question.IsChecked)
                                selectedQuestionList.Add(question.QuestionId);
                            else
                                selectedQuestionList.RemoveAt(question.QuestionId - 1);

                        }
                    }
                    catch (Exception ex)
                    {

                    }

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

        public ICommand NextCommand
        {
            get
            {

                return new Command(() =>
                {
                    try
                    {
                        if (CheckNoItemAddedToCart())
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.EDeclerationNoItemAddedToCartMsg;
                            return;
                        }
                        if (IsArrivingPlaneSelected && (Double.Parse(TotalValue ?? "0") < 3000 && (SubmitModel.travelerDeclaration.product == null || SubmitModel.travelerDeclaration.product.Count <= 0)) && QFlow == 2 && IsYesSelected)
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.EDeclerationenteredValuedoesnotrequirethedeclaration;
                            return;
                        }
                        if (questionIndex < selectedQuestionList.Count)
                        {
                            IsYesSelected = true;
                            questionIndex++;
                            QFlow = selectedQuestionList[questionIndex];
                            SetQuestion();
                        }
                        else
                        {
                            if (SubmitModel.travelerDeclaration.tobacco.Count < 1 && SubmitModel.travelerDeclaration.product.Count < 1 && SubmitModel.travelerDeclaration.restricted.Count < 1 && SubmitModel.travelerDeclaration.currency.Count < 1)
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

        public ICommand PrevoiusCommand
        {
            get
            {

                return new Command(() =>
                {
                    try
                    {
                        BackMethod();

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
                        isProductSubTypeSelected = false;
                        isProductTypeSelected = false;
                        isTobacoTypeSelected = false;
                        isTobacotemSelected = false;
                        isPurposeSelected = false;
                        isCurrencySelected = false;
                        isUnitsSelected = false;
                        if (MaterialTypes == null || MaterialTypes.Count > 0)
                        {
                            var materialTypes = await DeclerationServices.GetCoinTypes();
                            MaterialTypes = materialTypes?.Item1.data;
                        }

                        var result = MaterialTypes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.MaterialType;
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
                        isMaterialTypeSelected = false;
                        isProductSubTypeSelected = false;
                        isProductTypeSelected = false;
                        isTobacoTypeSelected = false;
                        isTobacotemSelected = false;
                        isCurrencySelected = false;
                        isUnitsSelected = false;
                        if (Purposes == null || Purposes.Count > 0)
                        {
                            var purposes = await DeclerationServices.GetPurposes();
                            Purposes = purposes?.Item1.data;
                        }

                        var result = Purposes.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name });
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
                        isCurrencySelected = false;
                        isPurposeSelected = false;
                        isMaterialTypeSelected = false;
                        isProductSubTypeSelected = false;
                        isProductTypeSelected = false;
                        isTobacoTypeSelected = false;
                        isTobacotemSelected = false;
                        if (Units == null || Units.Count > 0)
                        {
                            var units = await DeclerationServices.GetUnits();
                            Units = units?.Item1.data;
                        }

                        var result = Units.Select(c => new BottomSheetModel() { Id = c.id.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.units;
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

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>((e) =>
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
                            IsWeighVisible = selectedTobacoItem.HasWeight;
                            isTobacotemSelected = false;
                        }
                        else if (isProductTypeSelected)
                        {
                            SelectedProductTypes = ProductTypes.First(c => c.ID == int.Parse(e.Id));
                            if (String.IsNullOrWhiteSpace(SelectedProductTypes.code))
                            {
                                IsProductItemHaveSubType = true;
                            }
                            else
                            {
                                IsProductItemHaveSubType = false;
                            }
                            SelectedProductSubTypes = null;
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
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
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

                return new Command(async () =>
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
                                var tobaco = SubmitModel.travelerDeclaration.tobacco.First(c => c.ID == e.ID);
                                var tobacofess = FeesCalculatorBody.tobacco?.First(c => c.ID == e.ID);
                                                                await CalculateFees(2, tobacofess);
                                SubmitModel.travelerDeclaration.tobacco.Remove(tobaco);
                                break;
                            case 2:
                                var product = SubmitModel.travelerDeclaration.product.First(c => c.ID == e.ID);
                                var productfess = FeesCalculatorBody.product?.First(c => c.ID == e.ID);
                                await CalculateFees(2, null, productfess);
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

        public ICommand SearchEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    try
                    {
                        if (e != null)
                        {
                            var entry = e as BorderlessEntry;
                            var value = entry.Text.ToLower();
                            if (string.IsNullOrWhiteSpace(value))
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(TempBottomSheetList);
                            else
                            {
                                var result = TempBottomSheetList.Where(s => s.Name.ToLower().Contains(value));
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
        }

        #endregion

        #region Methods

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
                case 1: // Tobbaco
                    Question = AppResources.EdeclerationTobbacoQuestion;
                    break;
                case 2: // product
                    Question = AppResources.EdeclerationProductQuestion;
                    break;
                case 3: // Currency
                    Question = AppResources.EDeclerationCurrencyQuestion;
                    break;
                case 4: // Restricted 
                    Question = AppResources.EDeclerationRestrictedQuestion;
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

        private void DisplayRequiredDataMsg()
        {
            IsShowMsgView = true;
            MessageTxt = AppResources.RequiredData;
        }

        public void BackMethod()
        {
            if (IsShowBottomSheet)
            {
                IsShowBottomSheet = false;
                HeaderTitle = AppResources.eDeclaration;
                return;
            }
            ClearData();
            if (questionIndex > 0)
            {
                // Due to user have only 2 questions to answer in case not arriving
                if (!IsArrivingPlaneSelected && QFlow == 3) 
                {

                    ClearObjectData();
                    return;
                }
                questionIndex--;
                SetQuestion();
            }
            else
            {
                ClearObjectData();

            }
        }

        private void ClearObjectData()
        {
            FeesCalculatorResponse = new FeesCalculatorResponse();
            CardData = new ObservableCollection<EDeclerationCardModel>();
            SubmitModel.travelerDeclaration = new TravelerDeclaration();
            QuestionList = new ObservableCollection<QuestionModel>();
            selectedQuestionList = new List<int>();
            questionIndex = 0;
            _navigationService.GoBack();
        }

        private async Task CalculateFees(int operation = 1, Models.EDeclerationsModel.FeesCalculators.Tobacco tobacco = null, Models.EDeclerationsModel.FeesCalculators.Product product = null)
        {
            try
            {

                if (FeesCalculatorBody.tobacco == null)
                {
                    FeesCalculatorBody.tobacco = new List<Models.EDeclerationsModel.FeesCalculators.Tobacco>();

                }
                if (FeesCalculatorBody.product == null)
                {
                    FeesCalculatorBody.product = new List<Models.EDeclerationsModel.FeesCalculators.Product>();

                }
                if (tobacco != null)
                {
                    if (operation == 1)
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
                    if (operation == 1)
                    {
                        FeesCalculatorBody.product.Add(product);
                        ClearProductData();
                    }
                    else
                    {
                        FeesCalculatorBody.product.Remove(product);

                    }

                }
                if ((FeesCalculatorBody.product == null || FeesCalculatorBody.product.Count <= 0) && (FeesCalculatorBody.tobacco == null || FeesCalculatorBody.tobacco.Count <= 0))
                {
                    FeesCalculatorResponse = new FeesCalculatorResponse();
                    await PopupNavigation.Instance.PopAsync(true);
                    return;
                }
                var calres = await DeclerationServices.FeesCalculator(FeesCalculatorBody);
                if (calres.IsSuccessStatusCode)
                {
                    var conent = await calres.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CustomApiFeesCalculatorResponse>(conent);
                    if (data.data != null)
                    {
                        var Result = data.data;
                        if (Result != null)
                        {
                            FeesCalculatorResponse.customsPercentage = Result.CustomesPercentage;
                            FeesCalculatorResponse.excise = Result.Excise;
                            FeesCalculatorResponse.totalDuty = Result.TotalDuty;
                            FeesCalculatorResponse.totalPayment = Result.TotalPayment;
                            FeesCalculatorResponse.vat = Result.VAT;

                            FeesCalculatorResponse.extraFees = Result.ExtraFees;

                            return;
                        }
                        FeesCalculatorResponse = new FeesCalculatorResponse();
                        return;
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }

        #endregion 

        public BaseProductDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices DeclerationServices) : base(navigationService, dialogService, DeclerationServices)
        {


        }
    }

    public class QuestionModel: ViewModelBase
    {
        string questionName;
        public string QuestionName { get { return questionName; } set { questionName = value; RaisePropertyChanged(); } }

        int questionId;
        public int QuestionId { get { return questionId; } set { questionId = value; RaisePropertyChanged(); } }

        bool isChecked;
        public bool IsChecked { get { return isChecked; } set { isChecked = value; RaisePropertyChanged(); } }

        string questionBackgroundColor= "#FFFFFF";
        public string QuestionBackgroundColor { get { return questionBackgroundColor; } set { questionBackgroundColor = value; RaisePropertyChanged(); } }


    }
}

