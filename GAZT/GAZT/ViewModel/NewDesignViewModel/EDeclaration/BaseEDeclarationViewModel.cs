using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class BaseEDeclarationViewModel : BaseViewModel
    {

        #region Properties
        bool isArrivingPlaneSelected = true;
        public bool IsArrivingPlaneSelected { get { return isArrivingPlaneSelected; } set { isArrivingPlaneSelected = value; RaisePropertyChanged(); } }

        bool isYesSelected = true;
        public bool IsYesSelected { get { return isYesSelected; } set { isYesSelected = value; RaisePropertyChanged(); } }

        public static Dictionary<string,object> QAnswereDictionary { get; set; }


        ObservableCollection<BottomSheetModel> bottomSheetList;
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        ObservableCollection<BottomSheetModel> tempBottomSheetList;
        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get { return tempBottomSheetList; } set { tempBottomSheetList = value; RaisePropertyChanged(); } }

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; RaisePropertyChanged(); } }

        BottomSheetModel selectedItem;
        public BottomSheetModel SelectedItem { get { return selectedItem; } set { selectedItem = value; RaisePropertyChanged(); } }


        #endregion


        #region Commands
        public ICommand CardSelectionCommand
        {
            get
            {
                return new Command( () =>
                {
                    IsArrivingPlaneSelected = IsArrivingPlaneSelected == true ? false : true;
                });
            }
        }
        public ICommand QuestionSelectionCommand
        {
            get
            {
                return new Command( () =>
                {
                    IsYesSelected = IsYesSelected == true ? false : true;
                });
            }
        }
        public ICommand GoToProductDeclarationCommand
        {
            get
            {
                return new Command( () =>
                {
                    _navigationService.NavigateTo("ProductDeclarationPage");
                });
            }
        }
        #endregion
      public  IE_DeclerationServices DeclerationServices;
        public BaseEDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService)
        {
            DeclerationServices = declerationServices;
        }
    }
}

