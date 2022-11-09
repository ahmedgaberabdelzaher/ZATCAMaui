using System;
using System.Windows.Input;
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

        public BaseEDeclarationViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

