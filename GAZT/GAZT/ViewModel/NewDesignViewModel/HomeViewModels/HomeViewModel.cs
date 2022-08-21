using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Views.NewDesign.CustomServicesPages.eDeclarations;
using EGAZT.Views.NewDesign.HomePages;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.HomeViewModels
{
    public enum Tabs
    {
        Home,
        Account,
        Menu
    }


    public class HomeViewModel:BaseViewModel
    {

        string title ;
        public string Title { get { return title; } set { title = value; RaisePropertyChanged(); } }
        int itemCountPerRow=2;
        public int ItemCountPerRow { get { return itemCountPerRow; } set { itemCountPerRow = value; RaisePropertyChanged(); } }

       ObservableCollection<MenuModel> menuLst;
        public ObservableCollection<MenuModel> MenuLst { get { return menuLst; } set { menuLst = value; RaisePropertyChanged(); } }

        ObservableCollection<MenuModel> customeMenuLst;
        public ObservableCollection<MenuModel> CustomeMenuLst { get { return customeMenuLst; } set { customeMenuLst = value; RaisePropertyChanged(); } }



        int currentTab =1;
        public int CurrentTab { get { return currentTab; } set { currentTab = value; RaisePropertyChanged(); }  }


        public HomeViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {
            MenuLst = new ObservableCollection<MenuModel>()
            {
                new MenuModel()
                {
                   Name=AppResources.ZatcaInfoMenu, ID="1",ImageSource="CustomServices"
                },
                 new MenuModel()
                {
                   Name=AppResources.NDVATServices, ID="2",ImageSource="EDeclerationicon"
                },
                     new MenuModel()
                {
                   Name=AppResources.EXCISETAXServices, ID="3",ImageSource="ExciseTaxServices"
                },
                 new MenuModel()
                {
                   Name=AppResources.GeneralServices, ID="4",ImageSource="EDeclerationicon"
                },
            };

            CustomeMenuLst = new ObservableCollection<MenuModel>()
           {
                
                 new MenuModel()
                {
                   Name=AppResources.CustomsZATCAIntegrat+"            ", ID=App.TraifSectionsView,ImageSource="TarrrifSectionIcon"
                },
                 new MenuModel()
                {
                   Name=AppResources.Inquiryaboutacustomsdeclaration, ID=App.InquiryAboutCustomsDeclarationView,ImageSource="InquireCustomDeclerations"
                },
                     new MenuModel()
                {
                   Name=AppResources.CustomsDeclarationforTravelers, ID="EDeclerationView",ImageSource="CustomDeclerationsforTravellers"
                },
                 new MenuModel()
                {
                   Name=AppResources.InquireaboutPaymentofInsuranceTitle, ID="LaboratoryPaymentOfInsuranceFees",ImageSource="LabFees"
                },
           };
        }
        public ICommand ChangeCurrentTabCommand
        {
            get
            {
                return new Command<string>((tab) =>
                {
                    if (tab!=currentTab.ToString())
                    {
                        if (tab=="1")
                        {
                           
                            _navigationService.NavigateTo($"../NavigationPage/{App.SFLoginPageView}", App.GAZTNewDesignDashBoardPageView);
                          
                        }
                        CurrentTab = int.Parse(tab);
                        Title = tab == "0" ? AppResources.Home : AppResources.ZZZMenu;
                    }
              
                });
            }
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
                            _navigationService.NavigateTo(App.ZatcaInfoMenuPageView);
                            break;
                        case "3":
                            _navigationService.NavigateTo(App.ZatcaInfoMenuPageView);
                            break;
                        case "4":
                            _navigationService.NavigateTo(App.ZatcaInfoMenuPageView);
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
                                       _navigationService.NavigateTo(menuItem.ID);
                });
            }
        }

        public ICommand ChangeViewCommand
        {
            get
            {
                return new Command<string>((ItemCount) =>
                {
                    if (int.Parse(ItemCount)!= ItemCountPerRow)
                    {
                        foreach (var item in CustomeMenuLst)
                        {
                            item.IsVerticalView = !item.IsVerticalView;
                        }
                    }
                    ItemCountPerRow = int.Parse(ItemCount);
                });
            }
        }

    }
}
