using System.Windows.Input;

using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.EDeclerationsModel.FeesCalculators;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class CustomFeesFormViewModel : BaseProductDeclarationViewModel
    {
        bool isshowFeesView;
        public bool IsshowFeesView { get { return isshowFeesView; } set { isshowFeesView = value; OnPropertyChanged(); } }

        string vatPercenntage;
        public string VatPercenntage { get { return vatPercenntage; } set { vatPercenntage = value; OnPropertyChanged(); } }

        string customFeesPercentage;
        public string CustomFeesPercentage { get { return customFeesPercentage; } set { customFeesPercentage = value; OnPropertyChanged(); } }


        string vatCalculte;
        public string VatCalculte { get { return vatCalculte; } set { vatCalculte = value; OnPropertyChanged(); } }

        string customFeesCalculate;
        public string CustomFeesCalculate { get { return customFeesCalculate; } set { customFeesCalculate = value; OnPropertyChanged(); } }

        string exiseTaxCaluclat;
        public string ExiseTaxCaluclat { get { return exiseTaxCaluclat; } set { exiseTaxCaluclat = value; OnPropertyChanged(); } }

        double productValue;
        public double ProductValue { get { return productValue; } set { productValue = value; OnPropertyChanged(); } }

        public CustomFeesFormViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices,INativeNafath nativeNafath) : base(navigationService, dialogService, declerationServices,nativeNafath)
        {

        }
        public new ICommand OpenPoductTypesCommand
        {
            get
            {

                return new Command(async () =>
                {
                    await GetProducts(true);

                });

            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {

                    BackMethod();
                });
            }
        }

        public virtual ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>((e) =>
                {
                    HandleBottomSheetSelection(e, true);

                });
            }
        }


        public void BackMethod()
        {
            SelectedCalcType = 0;
            SelectedCalcTypeName = "";

            TotalValue = "0";

            if (IsShowBottomSheet)
            {
                IsShowBottomSheet = false;
                return;
            }
            if (IsshowFeesView)
            {
                IsshowFeesView = false;
                return;
            }
            else
            {
                FeesCalculatorResponse = new FeesCalculatorResponse();
                _navigationService.GoBack();

            }
        }

        public ICommand CalculateCommand
        {
            get
            {

                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        ProductValue = double.Parse(TotalValue);

                        if (SelectedCalcType == 1)
                        {
                            if (!CheckTobacoDataNotNull())
                            {

                                if (int.Parse(Quantity ?? "0") <= 0)
                                {
                                    IsShowMsgView = true;
                                    MessageTxt = AppResources.QuantityValidation;
                                    return;
                                }
                                DisplayRequiredDataMsg();
                                return;
                            }
                            Tobacco tobao = new Tobacco()
                            {
                                harmonizedCode = long.Parse(SelectedTobacoItem.itemCode).ToString(),
                                count = int.Parse(Quantity ?? "0"),
                                sequence = SelectedTobacoItem.taxSequence,
                                ID = SelectedTobacoItem.ID,
                                value = double.Parse(TotalValue),
                                measurementUnit = SelectedTobacoItem.measurementUnit,
                                typeName = SelectedTobacoItem?.Name,
                                subTypeName = SelectedTobacoItem?.Name,
                                weight = string.IsNullOrWhiteSpace(Weight) ? 0 : double.Parse(Weight),
                            };
                            IsshowFeesView = await CalculateFees(1, tobao, null);
                            if (IsshowFeesView)
                            {
                                ClearTobacoData();

                            }


                        }
                        else
                        {
                            if (!CheckProductDataNotNull(true))
                            {
                                DisplayRequiredDataMsg();
                                return;
                            }
                            Product product = new Product()
                            {
                                harmonizedCode = IsProductItemHaveSubType ? SelectedProductSubTypes.code : IsProductItemHaveSubType ? SelectedProductSubTypes.code : SelectedProductTypes.code,
                                value = double.Parse(TotalValue),
                                count = int.Parse(Quantity ?? "0"),
                                typeName = IsProductItemHaveSubType ? SelectedProductSubTypes.Name : SelectedProductTypes.Name,
                            };
                            IsshowFeesView = await CalculateFees(1, null, product);
                            if (IsshowFeesView)
                            {
                                ClearProductData();
                                SelectedCalcType = 0;
                            }

                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        FeesCalculatorBody = new Models.EDeclerationsModel.FeesCalculators.FeesCalculatorBody();
                        IsLoading = false;
                    }

                });

            }
        }

        public async Task<bool> CalculateFees(int operation = 1, Tobacco tobacco = null, Product product = null)
        {
            try
            {
                if (!NetworkCheck.IsInternet())
                {
                    IsShowMsgView = true;
                    IsLoading = false;
                    MessageTxt = AppResources.NoInternet;
                    return false;
                }

                if (FeesCalculatorBody.tobacco == null)
                {
                    FeesCalculatorBody.tobacco = new List<Tobacco>();

                }
                if (FeesCalculatorBody.product == null)
                {
                    FeesCalculatorBody.product = new List<Product>();

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
                var calres = await DeclerationServices.FeesCalculator(FeesCalculatorBody);
                var conent = await calres.Content.ReadAsStringAsync();
                if (calres.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<DATAPowerBaseResponseResult<FeesCalculatorResponse>>(conent);
                    if (data.result != null)
                    {
                        FeesCalculatorResponse = data.result;
                        if (FeesCalculatorResponse.vat != null && FeesCalculatorResponse.vat > 0)
                        {
                            VatPercenntage = "15%";
                            VatCalculte = $"{AppResources.VATCertificates} = ({FeesCalculatorResponse.productFinalPrice} + {FeesCalculatorResponse.totalDuty} + {FeesCalculatorResponse.excise} + {FeesCalculatorResponse.extraFees})*15%";
                        }
                        else
                        {
                            VatPercenntage = "";
                        }
                        if (FeesCalculatorResponse.totalDuty > 0)
                        {
                            if (FeesCalculatorResponse.excise != null && FeesCalculatorResponse.excise > 0)
                            {
                                CustomFeesPercentage = "";
                                CustomFeesCalculate = $"{AppResources.CustomsFees} ={FeesCalculatorResponse.tobaccoCustomsTaxEquation}";
                            }
                            else
                            {

                                CustomFeesPercentage = "5%";
                                CustomFeesCalculate = $"{AppResources.CustomsFees} ={FeesCalculatorResponse.totalDuty} *5%";
                            }
                        }
                        else
                        {
                            CustomFeesCalculate = $"{AppResources.CustomsFees} ={FeesCalculatorResponse.totalDuty} * 0%";
                            CustomFeesPercentage = "";
                        }
                        if (FeesCalculatorResponse.excise != null && FeesCalculatorResponse.excise > 0)
                        {

                            ExiseTaxCaluclat = $"{AppResources.ExciseTax2} ={FeesCalculatorResponse.tobaccoExciseTaxEquation}";

                        }

                        return true;
                    }

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}

