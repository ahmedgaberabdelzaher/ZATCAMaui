using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
	public class NativeNafathLoginPageViewModel : BaseViewModel
	{
        string email;
        public string Email { get { return email; } set { email = value; RaisePropertyChanged(); } }



        public ICommand NativeNafathLoginCommand
        {
            get
            {
                return new Command( () =>
                {
                    _navigationService.NavigateTo("NativeConfirmNafathPage");
                    
                });
            }
        }



        public NativeNafathLoginPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
		{
		}
	}
}

