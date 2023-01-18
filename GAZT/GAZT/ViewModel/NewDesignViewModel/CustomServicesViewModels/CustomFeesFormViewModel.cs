using System;
using System.Windows.Input;
using EGAZT.Services.Interface;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
	public class CustomFeesFormViewModel: ProductDeclarationViewModel
    {
        bool isshowFeesView;
        public bool IsshowFeesView { get { return isshowFeesView; } set { isshowFeesView = value; RaisePropertyChanged(); } }

        public CustomFeesFormViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService,declerationServices)
        {

		}
        public override ICommand OpenPoductTypesCommand
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
                return new Command( () => {

                    BackMethod();
                });
            }
        }

        public virtual ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<Controls.BottomSheetModel>((e) =>
                {
                    HandleBottomSheetSelection(e,true
                        );

                });
            }
        }


        public void BackMethod()
        {
            SelectedCalcType = 0;
            SelectedCalcTypeName = "";
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
                FeesCalculatorResponse = new Models.EDeclerationsModel.FeesCalculators.FeesCalculatorResponse();
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
                        if (SelectedCalcType==1)
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
                                Models.EDeclerationsModel.FeesCalculators.Tobacco tobao = new Models.EDeclerationsModel.FeesCalculators.Tobacco()
                            {
                                harmonizedCode = long.Parse(SelectedTobacoItem.itemCode).ToString(),
                                count = int.Parse(Quantity ?? "0"),
                                sequence = SelectedTobacoItem.taxSequence,
                                ID = SelectedTobacoItem.ID,
                                value=double.Parse(TotalValue)
                            };
                            IsshowFeesView = await CalculateFees(1, tobao, null);
                                  if (IsshowFeesView)
                            {
                                ClearTobacoData();
                               
                            }
                          
                           
                        }
                        else
                        {
                            if (!CheckProductDataNotNull())
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
                            Models.EDeclerationsModel.FeesCalculators.Product product = new Models.EDeclerationsModel.FeesCalculators.Product()
                    {
                        harmonizedCode = IsProductItemHaveSubType ? SelectedProductSubTypes.code : IsProductItemHaveSubType ? SelectedProductSubTypes.code : SelectedProductTypes.code,
                        value = Double.Parse(TotalValue)
                    };
                            IsshowFeesView= await CalculateFees(1, null, product);
                            if (IsshowFeesView)
                            {
ClearProductData();
                                SelectedCalcType = 0;
                            }
                    
                        }
                    }
                    catch (Exception ex)
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
    }
}

