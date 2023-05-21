using System;
using EGAZT.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.HomeViewModels
{
    public class CustomServiceMenuViewModel:BaseViewModel
    {

        ObservableCollection<MenuModel> customeMenuLst;
        public ObservableCollection<MenuModel> CustomeMenuLst { get { return customeMenuLst; } set { customeMenuLst = value; RaisePropertyChanged(); } }

        int itemCountPerRow = 2;
        public int ItemCountPerRow { get { return itemCountPerRow; } set { itemCountPerRow = value; RaisePropertyChanged(); } }


        public CustomServiceMenuViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
    {
            GetCustomServiceMenuLst();
    }
        public ICommand ChangeViewCommand
        {
            get
            {
                return new Command<string>((ItemCount) =>
                {
                    if (int.Parse(ItemCount) != ItemCountPerRow)
                    {
                        // int cIndex = 1;
                        // int RIndex = 1;
                        bool isvertical = ItemCount == "1" ? true : false;
                       GetCustomServiceMenuLst(isvertical);
                        ItemCountPerRow = int.Parse(ItemCount);
                      
                    }



                });
            }
        }


        public void GetCustomServiceMenuLst(bool isvertical = false)
        {

            CustomeMenuLst = new ObservableCollection<MenuModel>()
           {

                 new MenuModel()
                {
                   Name=AppResources.CustomsZATCAIntegrat, ID=App.TraifSectionsView,ImageSource="TarrrifSectionIcon",ColumnNo=0,Row=0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomNote1:" "
                },
                 new MenuModel() {
                   Name=AppResources.Inquiryaboutacustomsdeclaration, ID=App.InquiryAboutCustomsDeclarationView,ImageSource="InquireCustomDeclerations",ColumnNo=isvertical?0:1,Row=isvertical?1:0,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomNote2:" "
                }
                 ,
                 new MenuModel()
                {
                   Name=AppResources.CustomsDeclarationforTravelers, ID="EDeclarationPage",ImageSource="TravellerDecleration",ColumnNo=0,Row=isvertical?2:1,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.EDeclerationDesc:" "
                },
                 new MenuModel()
                {
                   Name=AppResources.InquireaboutPaymentofInsuranceTitle, ID="LaboratoryPaymentOfInsuranceFees",ImageSource="LabfeesInquiry",ColumnNo=isvertical?0:1,Row=isvertical?3:1,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomNote4:""
                },
                 new MenuModel()
                {
                   Name=AppResources.TransactionReception, ID="IAMLoginView",ImageSource="TransactionReceptionIcon",ColumnNo=0,Row=isvertical?4:2,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.TawreedServiceDesc:""
                } ,
                 new MenuModel()
                {
                   Name=AppResources.CustomFeesCalculator, ID="CustomFeesCalculator",ImageSource="TransactionReceptionIcon",ColumnNo=isvertical?0:1,Row=isvertical?5:2,IsVerticalView=isvertical,ServiceDesc=isvertical?AppResources.CustomFeesCalculator:""
                }
           };
        }
    }
}

