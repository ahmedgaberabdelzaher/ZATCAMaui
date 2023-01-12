using System;
using EGAZT.Services.Interface;
using EGAZT.ViewModel.NewDesignViewModel.EDeclaration;
using GalaSoft.MvvmLight.Views;

namespace EGAZT.ViewModel.NewDesignViewModel.CustomServicesViewModels
{
	public class CustomFeesFormViewModel: ProductDeclarationViewModel
    {
        public CustomFeesFormViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService,declerationServices)
        {

		}
	}
}

