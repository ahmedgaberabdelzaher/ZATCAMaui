using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models;
using Syncfusion.GridCommon;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignVATReturnUpdatedUIPageViewModel : BaseViewModel
    {

        public ICommand OnBackStepClicked { get; set; }
        public ICommand OnMoreClicked { get; set; }
        public ICommand onCountinueClicked { get; set; }
        

        int CurrentView;

        #region Variable

        private VATReturnUpdatedUITabEnum _currentTab = VATReturnUpdatedUITabEnum.Instrunction;
        public VATReturnUpdatedUITabEnum currentTab
        {
            get => _currentTab;
            private set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }

        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 7;
#endregion

        #region Property

        public Color _BoxSevenFrame;
        public Color BoxSevenFrame
        {
            get
            {
                return _BoxSevenFrame;
            }
            set
            {
                _BoxSevenFrame = value;
                RaisePropertyChanged("BoxSevenFrame");
            }
        }

        public Color _BoxSixFrame;
        public Color BoxSixFrame
        {
            get
            {
                return _BoxSixFrame;
            }
            set
            {
                _BoxSixFrame = value;
                RaisePropertyChanged("BoxSixFrame");
            }
        }

        public Color _BoxFiveFrame;
        public Color BoxFiveFrame
        {
            get
            {
                return _BoxFiveFrame;
            }
            set
            {
                _BoxFiveFrame = value;
                RaisePropertyChanged("BoxFiveFrame");
            }
        }

        public Color _sevenbox;
        public Color sevenbox
        {
            get
            {
                return _sevenbox;
            }
            set
            {
                _sevenbox = value;
                RaisePropertyChanged("sevenbox");
            }
        }

        public Color _sixbox;
        public Color sixbox
        {
            get
            {
                return _sixbox;
            }
            set
            {
                _sixbox = value;
                RaisePropertyChanged("sixbox");
            }
        }

        public Color _fivebox;
        public Color fivebox
        {
            get
            {
                return _fivebox;
            }
            set
            {
                _fivebox = value;
                RaisePropertyChanged("fivebox");
            }
        }

        public bool _isBtnVisible;
        public bool isBtnVisible
        {
            get
            {
                return _isBtnVisible;
            }
            set
            {
                _isBtnVisible = value;
                RaisePropertyChanged("isBtnVisible");
            }
        }

        public bool _isCheckVisible;
        public bool isCheckVisible
        {
            get
            {
                return _isCheckVisible;
            }
            set
            {
                _isCheckVisible = value;
                RaisePropertyChanged("isCheckVisible");
            }
        }

        public Color _OuterFrame;
        public Color OuterFrame
        {
            get
            {
                return _OuterFrame;
            }
            set
            {
                _OuterFrame = value;
                RaisePropertyChanged("OuterFrame");
            }
        }

        public Color _secOuterFrame;
        public Color secOuterFrame
        {
            get
            {
                return _secOuterFrame;
            }
            set
            {
                _secOuterFrame = value;
                RaisePropertyChanged("secOuterFrame");
            }
        }

        public Color _innerFrame;
        public Color innerFrame
        {
            get
            {
                return _innerFrame;
            }
            set
            {
                _innerFrame = value;
                RaisePropertyChanged("innerFrame");
            }
        }

        public Color _secBox;
        public Color secBox
        {
            get
            {
                return _secBox;
            }
            set
            {
                _secBox = value;
                RaisePropertyChanged("secBox");
            }
        }

        public Color _thirdBox;
        public Color thirdBox
        {
            get
            {
                return _thirdBox;
            }
            set
            {
                _thirdBox = value;
                RaisePropertyChanged("thirdBox");
            }
        }

        public Color _fourthBox;
        public Color fourthBox
        {
            get
            {
                return _fourthBox;
            }
            set
            {
                _fourthBox = value;
                RaisePropertyChanged("fourthBox");
            }
        }

        public bool _IsInstrunctionView ;
        public bool IsInstrunctionView
        {
            get
            {
                return _IsInstrunctionView;
            }
            set
            {
                _IsInstrunctionView = value;
                RaisePropertyChanged("IsInstrunctionView");
            }
        }

        public bool _IsSaleView;
        public bool IsSaleView
        {
            get
            {
                return _IsSaleView;
            }
            set
            {
                _IsSaleView = value;
                RaisePropertyChanged("IsSaleView");
            }
        }

        public bool _IsPurchaseView;
        public bool IsPurchaseView
        {
            get
            {
                return _IsPurchaseView;
            }
            set
            {
                _IsPurchaseView = value;
                RaisePropertyChanged("IsPurchaseView");
            }
        }

        public bool _IsTotalVatView;
        public bool IsTotalVatView
        {
            get
            {
                return _IsTotalVatView;
            }
            set
            {
                _IsTotalVatView = value;
                RaisePropertyChanged("IsTotalVatView");
            }
        }

        public bool _IsSummeryView;
        public bool IsSummeryView
        {
            get
            {
                return _IsSummeryView;
            }
            set
            {
                _IsSummeryView = value;
                RaisePropertyChanged("IsSummeryView");
            }
        }

        public bool _IsVATReturnsView;
        public bool IsVATReturnsView
        {
            get
            {
                return _IsVATReturnsView;
            }
            set
            {
                _IsVATReturnsView = value;
                RaisePropertyChanged("IsVATReturnsView");
            }
        }

        public bool _IsTaxpayerView;
        public bool IsTaxpayerView
        {
            get
            {
                return _IsTaxpayerView;
            }
            set
            {
                _IsTaxpayerView = value;
                RaisePropertyChanged("IsTaxpayerView");
            }
        }


        public string _CreditDetailsText;
        public string CreditDetailsText
        {
            get
            {
                return _CreditDetailsText;
            }
            set
            {
                _CreditDetailsText = value;
                RaisePropertyChanged("CreditDetailsText");
            }
        }

        public string _ContinueText;
        public string ContinueText
        {
            get
            {
                return _ContinueText;
            }
            set
            {
                _ContinueText = value;
                RaisePropertyChanged("ContinueText");
            }
        }
        #endregion

        #region Constructor
        public GAZTNewDesignVATReturnUpdatedUIPageViewModel(INavigationService navigationService, IDialogService dialogService):base(navigationService, dialogService)
        {
            //OnLoginPageLinkClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.NavigateTo(App.LogInPageView, App.SFLandingPageView);
            //});
            
            CurrentView = 0;

            IsInstrunctionView = true;

            secBox=thirdBox=fourthBox=fivebox=sixbox=sevenbox = Color.FromHex("#EBEBEB");

            IsTaxpayerView = IsVATReturnsView= IsSaleView = IsPurchaseView = IsTotalVatView = IsSummeryView = false;
            isBtnVisible=isCheckVisible = false;
            ContinueText = "Continue";

            OnMoreClicked = new Xamarin.Forms.Command(() =>
            {
                PopupNavigation.Instance.PushAsync(new MorePopUpPageView());
            });

            onCountinueClicked = new Xamarin.Forms.Command(() =>
            {

                switch (currentTab)
                {
                    case VATReturnUpdatedUITabEnum.Instrunction:
                        currentTab = VATReturnUpdatedUITabEnum.TaxpayerDetails;
                        break;

                    case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                        currentTab = VATReturnUpdatedUITabEnum.VATReturns;
                        break;
                    case VATReturnUpdatedUITabEnum.VATReturns: currentTab = VATReturnUpdatedUITabEnum.Sales;
                        break;

                    case VATReturnUpdatedUITabEnum.Sales: currentTab = VATReturnUpdatedUITabEnum.Purchase;
                        break;

                    case VATReturnUpdatedUITabEnum.Purchase:
                        currentTab = VATReturnUpdatedUITabEnum.TotalVat;
                        break;

                    case VATReturnUpdatedUITabEnum.TotalVat: 
                        currentTab = VATReturnUpdatedUITabEnum.Summery;
                        break;

                    case VATReturnUpdatedUITabEnum.Summery: 
//                        currentTab = VATReturnUpdatedUITabEnum.Summery;
                        break;
                }

/*                    switch (CurrentView)
                    {
                        case 0:
                            IsInstrunctionView = false;
                            IsTaxpayerView = true;
                        innerFrame= Color.FromHex("#006450");
                        secBox = Color.FromHex("#006450");
                        break;
                        case 1:
                            IsTaxpayerView = false;
                            IsVATReturnsView = true;
                        secOuterFrame= Color.FromHex("#006450");
                        thirdBox = Color.FromHex("#006450");
                        break;
                        case 2:
                            IsVATReturnsView = false;
                            IsSaleView = true;
                        
                        OuterFrame = Color.FromHex("#006450");
                        fourthBox = Color.FromHex("#006450");
                        break;

                        case 3:
                        IsPurchaseView = true;
                        IsSaleView = false;
                        fivebox= Color.FromHex("#006450");
                        BoxFiveFrame = Color.FromHex("#006450");
                        //ContinueText = "Confirm and Carry Forward";
                        //                            CreditDetailsText = "Confirm and Request Refund";
                        break;

                    case 4:IsTotalVatView = true;
                        IsPurchaseView = false;
                      sixbox= Color.FromHex("#006450");
                        BoxSixFrame= Color.FromHex("#006450");
                        isBtnVisible =  true;
                        CreditDetailsText = "Carried Credit Details";
                        break;
                    case 5: IsSummeryView = isCheckVisible = true;
                        IsTotalVatView = false;
                        sevenbox= Color.FromHex("#006450");
                        BoxSevenFrame= Color.FromHex("#006450");
                        CreditDetailsText = "Confrim and Generate SADAD Bill";
                        break;

                    case 6: navigationService.NavigateTo(App.VATReturnSuccessfullPageView);
                        break;
                }

                    if (CurrentView < 6)
                        CurrentView++;*/
                    
            });

            OnBackStepClicked = new Xamarin.Forms.Command(() =>
            {
                switch (currentTab)
                {
                    case VATReturnUpdatedUITabEnum.TaxpayerDetails:
                        currentTab = VATReturnUpdatedUITabEnum.Instrunction;
                        break;
                    case VATReturnUpdatedUITabEnum.VATReturns:
                        currentTab = VATReturnUpdatedUITabEnum.TaxpayerDetails;
                        break;

                    case VATReturnUpdatedUITabEnum.Sales:
                        currentTab = VATReturnUpdatedUITabEnum.VATReturns;
                        break;

                    case VATReturnUpdatedUITabEnum.Purchase:
                        currentTab = VATReturnUpdatedUITabEnum.Sales;
                        break;

                    case VATReturnUpdatedUITabEnum.TotalVat:
                        currentTab = VATReturnUpdatedUITabEnum.Purchase;
                        break;

                    case VATReturnUpdatedUITabEnum.Summery:
                         currentTab = VATReturnUpdatedUITabEnum.TotalVat;
                        break;
                }

                /*                if (CurrentView > 0)
                                    CurrentView--;
                                switch (CurrentView)
                                {
                                    case 5:
                                        isCheckVisible = IsSummeryView = false;
                                        IsTotalVatView = true;
                                        sevenbox= Color.FromHex("#EBEBEB");
                                        BoxSevenFrame= Color.Transparent;
                                        CreditDetailsText = "Carried Credit Details";
                                        break;
                                    case 4:
                                        isBtnVisible = IsTotalVatView = false;
                                        IsPurchaseView = true;
                                        sixbox= Color.FromHex("#EBEBEB");
                                        BoxSixFrame= Color.Transparent;

                                        break;
                                    case 3:IsPurchaseView = false;
                                        IsSaleView = true;
                                        fivebox= Color.FromHex("#EBEBEB");
                                        BoxFiveFrame=Color.Transparent;
                                        break;
                                    case 2:
                                        IsSaleView = false;
                                        IsVATReturnsView = true;
                                             fourthBox= Color.FromHex("#EBEBEB");
                                        OuterFrame = Color.Transparent;
                                        break;
                                    case 1: IsVATReturnsView = false;
                                        IsTaxpayerView = true;
                                        thirdBox = Color.FromHex("#EBEBEB");
                                        secOuterFrame = Color.Transparent;
                                        break;
                                    case 0: IsTaxpayerView = false;
                                        IsInstrunctionView = true;
                                       secBox = Color.FromHex("#EBEBEB");
                                        innerFrame=Color.Transparent ;
                                        break;
                //                    case 0: IsSaleView = false;
                                        IsInstrunctionView = true;

                                        break;
                                    default: CurrentView = 0;
                                        break;
                                }*/
            });
        }
        #endregion
 
    
    
    
    
    }
}
