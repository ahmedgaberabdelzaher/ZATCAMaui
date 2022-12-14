using System;
using System.Windows.Input;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclerationViewModel:BaseViewModel
    {
        public IE_DeclerationServices DeclerationServices;
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

        string _ReferenceNumber;
        public string ReferenceNumber { get { return _ReferenceNumber; } set { _ReferenceNumber = value; RaisePropertyChanged(); } }

        string _IDResidencePassportNumber;
        public string IDResidencePassportNumber { get { return _IDResidencePassportNumber; } set { _IDResidencePassportNumber = value; RaisePropertyChanged(); } }


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
        public ICommand GoToReviewPageCommand
        {
            get
            {
                return new Command(async() =>
                {
                    try
                    {
                        IsLoading = true;
                        var result = await DeclerationServices?.GetInquireDecleration(ReferenceNumber, IDResidencePassportNumber);
                    
                        if (result?.Item1?.header?.status.code == "I000000")
                        {
                            var inquireDecleration = result?.Item1?.data?.travelerDeclaration;
                            if(inquireDecleration != null)
                            {
                                App.Locator.StateManager.SetItem("inquireDecleration", inquireDecleration);
                                _navigationService.NavigateTo("ReviewRequestPage");

                            }
                            IsLoading = false;
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.Somethingwentwrong;
                            IsLoading = false;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        IsLoading = false;
                    }
                });
            }
        }


        public EDeclerationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService)
        {
            DeclerationServices = declerationServices;
        }
    }

   
}

