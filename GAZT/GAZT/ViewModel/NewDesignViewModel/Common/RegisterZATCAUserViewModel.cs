using System;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
	public class RegisterZATCAUserViewModel: BaseViewModel
    {
        string email;
        public string Email { get { return email; } set { email = value; RaisePropertyChanged(); } }

        string mobileNumber;
        public string MobileNumber { get { return mobileNumber; } set { mobileNumber = value; RaisePropertyChanged(); } }

        string address;
        public string Address { get { return address; } set { address = value; RaisePropertyChanged(); } }


        public RegisterZATCAUserViewModel( INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

