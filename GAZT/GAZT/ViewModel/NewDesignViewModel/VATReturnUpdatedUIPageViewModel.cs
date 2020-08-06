using EGAZT.Views.NewDesign.VATDeclarationPages;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignVATReturnUpdatedUIPageViewModel : BaseViewModel
    {

        public ICommand OnBackStepClicked { get; set; }
        public ICommand OnMoreClicked { get; set; }
        public ICommand onCountinueClicked { get; set; }
        

        int CurrentView;

        #region Property

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

            secBox=thirdBox=fourthBox = Color.FromHex("#EBEBEB");

            IsTaxpayerView = IsVATReturnsView= IsSaleView = IsPurchaseView = IsTotalVatView = IsSummeryView = false;
            ContinueText = "Continue";

            OnMoreClicked = new Xamarin.Forms.Command(() =>
            {
                PopupNavigation.Instance.PushAsync(new MorePopUpPageView());
            });

            onCountinueClicked = new Xamarin.Forms.Command(() =>
            {
                    switch (CurrentView)
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
                            IsSummeryView = true;
                        OuterFrame= Color.FromHex("#006450");
                        fourthBox = Color.FromHex("#006450");
                        break;
//                        case 3:
                         IsTotalVatView = false;
                            IsSummeryView = true;
                        //ContinueText = "Confirm and Carry Forward";
                            CreditDetailsText = "Confirm and Request Refund";
                        break;
                    }

                    if (CurrentView < 2)
                        CurrentView++;
                    
            });

            OnBackStepClicked = new Xamarin.Forms.Command(() =>
            {
                switch (CurrentView)
                {
                    case 2:IsSummeryView = false;
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
                }

                if(CurrentView>0)
                CurrentView--;
                
            });
        }
        #endregion
 
    
    
    
    
    }
}
