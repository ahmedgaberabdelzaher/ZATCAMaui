using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
	public class ReviewRequestViewModel:BaseViewModel
	{


        List<string> _test = new List<string> { "Ahmed", "Hesham","Taha","asdasd", "asdasd", "asdasd", "ase1w13"};
        public List<string> TestData { get { return _test; } set { _test = value; RaisePropertyChanged(); } }

        public ICommand GoToPaymentCommand
        {
            get
            {
                return new Command( _ =>
                {
                    _navigationService.NavigateTo("EDeclarationPaymentPage");
                });
            }
        }


        public ReviewRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

