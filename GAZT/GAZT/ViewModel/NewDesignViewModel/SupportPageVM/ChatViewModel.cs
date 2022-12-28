using System;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.SupportPageVM
{
    public class ChatViewModel:BaseViewModel
    {
        public ChatViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

