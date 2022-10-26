using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ProductDeclarationViewModel: BaseEDeclarationViewModel
    {
        #region Properties
        bool isArrivingPlaneSelected = true;
        public bool Te { get { return isArrivingPlaneSelected; } set { isArrivingPlaneSelected = value; RaisePropertyChanged(); } }
        #endregion


        #region Commands
        public ICommand Tes
        {
            get
            {
                return new Command(() =>
                {
                    IsArrivingPlaneSelected = IsArrivingPlaneSelected == true ? false : true;
                });
            }
        }
        #endregion

        public ProductDeclarationViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

