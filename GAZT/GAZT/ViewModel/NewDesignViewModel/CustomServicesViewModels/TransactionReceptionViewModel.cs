using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Models.SubmitReportModel;
using GalaSoft.MvvmLight.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
    public class TransactionReceptionViewModel: BaseViewModel
    {
        string userType = "1";
        public string UserType { get { return userType; } set { userType = value; RaisePropertyChanged(); } }

         bool isSuccessView=false;
        public bool IsSuccessView { get { return isSuccessView; } set { isSuccessView = value; RaisePropertyChanged(); } }


        string email ;
        public string Email { get { return email; } set { email = value; RaisePropertyChanged(); } }

        string subject;
        public string Subject { get { return subject; } set { subject = value; RaisePropertyChanged(); } }

        string description;
        public string Description { get { return description; } set { description = value; RaisePropertyChanged(); } }

        public ICommand SelectUserTypeCommand
        {
            get
            {
                return new Command<string>((selectedType) =>
                {
                    UserType = selectedType;
                });
            }
        }

        public ICommand SendTransactionCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("/SuccessView");
                });
            }
        }



        public ICommand UploadFileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PickAndShow(new PickOptions() { PickerTitle = "Pick Files" },1);
                });
            }
        }


        public TransactionReceptionViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {

        }


    }
}

