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
        #endregion

        public BaseEDeclarationViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

