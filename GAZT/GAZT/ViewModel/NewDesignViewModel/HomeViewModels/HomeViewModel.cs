using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations;
using EGAZT.Views.NewDesign.HomePages;
using GalaSoft.MvvmLight.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.HomeViewModels
{
    public enum Tabs
    {
        Home,
        Account,
        Menu
    }

    public enum Services
    {
        CustomServices,
        VATServices,
        ExciseTaxServices,
        GeneralServices
    }


    public class HomeViewModel : BaseViewModel
    {


        int itemCountPerRow = 2;
        public int ItemCountPerRow { get { return itemCountPerRow; } set { itemCountPerRow = value; RaisePropertyChanged(); } }

        bool isHorizontalLstVisible = true;
        public bool IsHorizontalLstVisible { get { return isHorizontalLstVisible; } set { isHorizontalLstVisible = value; RaisePropertyChanged(); } }

        bool isVerticalLstVisible;
        public bool IsVerticalLstVisible { get { return isVerticalLstVisible; } set { isVerticalLstVisible = value; RaisePropertyChanged(); } }


        public Services CurrentService { get; set; } = Services.CustomServices;

        ObservableCollection<MenuModel> menuLst;
        public ObservableCollection<MenuModel> MenuLst { get { return menuLst; } set { menuLst = value; RaisePropertyChanged(); } }

        ObservableCollection<MenuModel> customeMenuLst;
        public ObservableCollection<MenuModel> CustomeMenuLst { get { return customeMenuLst; } set { customeMenuLst = value; RaisePropertyChanged(); } }

        ObservableCollection<MenuModel> sideMenuServiceLst;
        public ObservableCollection<MenuModel> SideMenuServiceLst { get { return sideMenuServiceLst; } set { sideMenuServiceLst = value; RaisePropertyChanged(); } }

        ObservableCollection<MenuModel> customeMenuVerticalLst;
        public ObservableCollection<MenuModel> CustomeMenuVerticalLst { get { return customeMenuVerticalLst; } set { customeMenuVerticalLst = value; RaisePropertyChanged(); } }

        public HomeViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            GetHomeMenuLst();

            GetCustomServiceMenuLst(); GetDashBoardMenuLst(3);
        }

        private void GetHomeMenuLst()
        {
            MenuLst = new ObservableCollection<MenuModel>()
            {
                new MenuModel()
                {
                   Name=AppResources.ZatcaInfoMenu, ID="1",ImageSource="ExciseTaxServices"
                },
                 new MenuModel()
                {
                   Name=AppResources.NDVATServices, ID="2",ImageSource="VatServices"
                },
                     new MenuModel()
                {
                   Name=AppResources.EXCISETAXServices, ID="3",ImageSource="CustomServices"
                },
                 new MenuModel()
                {
                   Name=AppResources.GeneralServices, ID="4",ImageSource="GeneralServices"
                },
            };
        }

        public async void OpenBrowser(Uri uri)
        {
            await Launcher.OpenAsync(uri);
        }
        public void GetSideMenuLst()
        {
            SideMenuServiceLst = new ObservableCollection<MenuModel>()
            {
                new MenuModel()
                {
                   Name=AppResources.AboutZATCA, ID=App.AboutUsPageView,ImageSource="AboutZatca"
                },
                 new MenuModel()
                {
                   Name=AppResources.FAQ, ID=App.FAQPageView,ImageSource="FAQ"
                },
                new MenuModel()
                {
                   Name=AppResources.RateUs, ID="RateUs",ImageSource="RateUS"
                },
                new MenuModel()
                {
                   Name=AppResources.PrivacyPolicy, ID=App.PrivacyAndPolicyPageView,ImageSource="PrivacyandPolicy"
                },
                 new MenuModel()
                {
                   Name=AppResources.ContactUs, ID="ContactUs",ImageSource="CallUS"
                },
                 new MenuModel()
                {
                   Name=AppResources.DBSMChat, ID="ChatPotView",ImageSource="thumbnail_chat"
                },
                new MenuModel()
                {
                   Name=AppResources.EdcuationJourney, ID="https://edujourneys.zatca.gov.sa/home/tracks",ImageSource="Education"
                },
                new MenuModel()
                {
                   Name=AppResources.Zakaty, ID="AboutZakatyView",ImageSource="ZAKATYlogoInMenu"
                },
                new MenuModel()
                {
                   Name=AppResources.Langauge, ID="ChangeLang",ImageSource="LangaugeIcon"
                },
            };

        }


        public ICommand SideMenuNavigationCommand
        {
            get
            {
                return new Command<MenuModel>((menuItem) =>
                {
                    if (menuItem.ID.ToLower().Contains("http"))
                    {
                        OpenBrowser(new Uri(menuItem.ID));
                        return;
                    }
                    if (menuItem.ID.Contains("ChangeLang"))
                    {
                        ChangeLanguage();
                        return;
                    }
                    _navigationService.NavigateTo(menuItem.ID);
                });
            }
        }

        private void ChangeLanguage()
        {

            if (App.IsArabic)
            {
                App.IsArabic = false;
                App.changeFontFamily(App.appObj);

                AppDirection = FlowDirection.LeftToRight;

            }
            else
            {
                App.IsArabic = true;
                App.changeFontFamily(App.appObj);
                AppDirection = FlowDirection.RightToLeft;
            }
            SetFlowDirection(); GetHomeMenuLst();
            GetSideMenuLst();

            _navigationService.NavigateTo("/SideMenuView");
        }

        public void GetVatServiceMenuLst(bool isvertical = false)
        {
            CustomeMenuLst = new ObservableCollection<MenuModel>()
           {

                 new MenuModel()
                {
                   Name=AppResources.VATRegistrationCertificate, ID=App.VATLookUpNewPageView,ImageSource="VatRegCheck",ColumnNo=0,Row=0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.VatRegistretionServiceDesc:""
                },
                 new MenuModel()
                {
                   Name=AppResources.TaxCalculator, ID="TaxCalculator",ImageSource="TaxCalcultor",ColumnNo=isvertical?0:1,Row=isvertical?1:0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.VatCalculationServiceDesc:""
                }
                 ,
                 new MenuModel()
                {
                   Name=AppResources.EinvoiceScanning, ID="E_InvoicesScan",ImageSource="EinvoiceScanning",ColumnNo=0,Row=isvertical?2:1,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.EinvoiceServiceDesc:""
                }

           };
        }

        public void GetExciseServiceMenuLst(bool isvertical = false)
        {

            CustomeMenuLst = new ObservableCollection<MenuModel>()
           {

                 new MenuModel()
                {
                   Name=AppResources.searchingandviewingtheindicativepricesforexciseGoods, ID="SearchIndiactivePriceForExciseGoods",ImageSource="SearchExciseTax",ColumnNo=0,Row=0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.TaxServicesTip1:""
                },
                 new MenuModel()
                {
                   Name=AppResources.TahqaqService, ID="TahqaqScanPage",ImageSource="EinvoiceScanning",ColumnNo=isvertical?0:1,Row=isvertical?1:0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.TaxServicesTip2:""
                }
           };
        }

        public void GetGeneralServiceMenuLst(bool isvertical = false)
        {
            CustomeMenuLst = new ObservableCollection<MenuModel>()
           {

                 new MenuModel()
                {
                Name=AppResources.Reports, ID="InquiryAboutAddOrShowReportsPage",ImageSource="Reports",ColumnNo=0,Row=0,IsVerticalView=isvertical
               //  Name=AppResources.Reports, ID="TaxEvasionPageWebView",ImageSource="Reports",ColumnNo=0,Row=0,IsVerticalView=isvertical

                 }

           };

        }

        public void GetCustomServiceMenuLst(bool isvertical = false)
        {

            CustomeMenuLst = new ObservableCollection<MenuModel>()
           {

                 new MenuModel()
                {
                   Name=AppResources.CustomsZATCAIntegrat, ID=App.TraifSectionsView,ImageSource="TarrrifSectionIcon",ColumnNo=0,Row=0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomNote1:""
                },
                 new MenuModel() {
                   Name=AppResources.Inquiryaboutacustomsdeclaration, ID=App.InquiryAboutCustomsDeclarationView,ImageSource="InquireCustomDeclerations",ColumnNo=isvertical?0:1,Row=isvertical?1:0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomNote2:""
                },
                    new MenuModel()
                {
                Name=AppResources.CustomsDeclarationforTravelers, ID="EDeclarationPage",ImageSource="TravellerDecleration",ColumnNo=0,Row=isvertical?2:1,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.EDeclerationDesc:""

                  // Name=AppResources.TransactionReception, ID="IAMLoginView",ImageSource="TransactionReceptionIcon",ColumnNo=0,Row=isvertical?2:1,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.TawreedServiceDesc:""
                } ,
                 new MenuModel()
                {
                   Name=AppResources.CustomFeesCalculator, ID="CustomFeesFormView",ImageSource="TransactionReceptionIcon",ColumnNo=isvertical?0:1,Row=isvertical?3:1,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomFeesCalculator:""
                }
                 ,
                 new MenuModel()
                {
                   Name=AppResources.TransactionReception, ID="IAMLoginView",ImageSource="TransactionReceptionIcon",ColumnNo=0,Row=isvertical?4:2,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.TawreedServiceDesc:""
                }
               
                 ,
                  new MenuModel()
                {
                   Name=AppResources.ShipmentTracking, ID="ShipmentTrackingTypesPage",ImageSource="shipmentIcon",ColumnNo=isvertical?0:1,Row=isvertical?5:2,IsVerticalView=isvertical,ServiceDesc=isvertical?"":""
                }
                  ,

                  new MenuModel()
                {
                     
                   Name=AppResources.CarImport, ID="FasahLoginView",ImageSource="BrokerOptionality",ColumnNo=0,Row=isvertical?6:3,IsVerticalView=isvertical,ServiceDesc=isvertical?"":""
                }
                    ,
                 new MenuModel()
                {
                   Name=AppResources.InquireaboutPaymentofInsuranceTitle, ID="LaboratoryPaymentOfInsuranceFees",ImageSource="LabfeesInquiry",ColumnNo=isvertical?0:1,Row=isvertical?7:3,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomNote4:""
                }
                
           };

            
        }

        public ICommand NavigateCommand
        {
            get
            {
                return new Command<MenuModel>((MenuItem) =>
                {

                    switch (MenuItem.ID)
                    {
                        case "1":
                          
                            _navigationService.NavigateTo("CusromServiceMenu");
                            break;
                        case "2":
                            _navigationService.NavigateTo("VatServicesMenu");
                            break;
                        case "3":
                            _navigationService.NavigateTo("ExciseServices");
                            break;
                        case "4":
                            _navigationService.NavigateTo("GeneralServices");
                            break;
                        default:
                            break;
                    }

                });
            }
        }

        public ICommand NavigateToServiceCommand
        {
            get
            {
                return new Command<MenuModel>((menuItem) =>
                {
                    switch (menuItem.ID)
                    {
                        case "CreateE_Declaration":
                            _navigationService.NavigateTo("CreateE_Declaration", AppResources.eDeclaration);
                            break;

                            // Depercated
                        case "TransactionReception":
                            _navigationService.NavigateTo("CreateE_Declaration", AppResources.Transactiondescription);
                            break;
                        case "CustomFeesCalculator":
                            _navigationService.NavigateTo("CreateE_Declaration", AppResources.CustomFeesCalculator);
                            break;
                        case "IAMLoginView":
                            _navigationService.NavigateTo("IAMLoginView", 2);
                            break;
                        default:
                            _navigationService.NavigateTo(menuItem.ID);
                            break;

                    }
                });
            }
        }

        public ICommand ChangeViewCommand
        {
            get
            {
                return new Command<string>((ItemCount) =>
                {
                    if (int.Parse(ItemCount) != ItemCountPerRow)
                    {
                        ItemCountPerRow = int.Parse(ItemCount);
                        bool isvertical = ItemCount == "1" ? true : false;
                        switch (CurrentService)
                        {
                            case Services.CustomServices:
                                GetCustomServiceMenuLst(isvertical);
                                break;
                            case Services.VATServices:
                                GetVatServiceMenuLst(isvertical);
                                break;
                            case Services.ExciseTaxServices:
                                GetExciseServiceMenuLst(isvertical);
                                break;
                            case Services.GeneralServices:
                                GetGeneralServiceMenuLst(isvertical);
                                break;

                            default:
                                break;
                        }

                    }



                });
            }
        }

    }

}
