using System;
using System.Windows.Input;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclerationViewModel:BaseViewModel
    {
        /// <summary>
        /// 1 for New Decleration
        /// 2 for Prevous Requests
        /// </summary>
        int serviceType;
        public int ServiceType { get { return serviceType; } set { serviceType = value; RaisePropertyChanged(); } }

        /// <summary>
        /// 1 for Visitor
        /// 2 for Citizen
        /// </summary>
        int identityType;
        public int IdentityType { get { return identityType; } set { identityType = value; RaisePropertyChanged(); } }


        public ICommand SelcectServiceTypeCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    ServiceType =int.Parse(e);
                });
            }
        }

        public ICommand SelcectIdentityTypeCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    IdentityType = int.Parse(e);
                    if (IdentityType==1)
                    {
                        _navigationService.NavigateTo("NewDeclarationPage");
                    }
                    else
                    {
                        _navigationService.NavigateTo("IAMLoginView", 1);
                    }
                });
            }
        }


        public EDeclerationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService)
        {

        }
    }

   
}

