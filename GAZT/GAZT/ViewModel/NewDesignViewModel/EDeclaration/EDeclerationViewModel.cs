using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using ZXing.Aztec.Internal;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclerationViewModel: BaseEDeclarationViewModel
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
                      
                        _navigationService.NavigateTo("NewDeclarationPage","");
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
                                App.Locator.StateManager.SetItem("inquireDeclaration", inquireDecleration);
                                _navigationService.NavigateTo("ReviewRequestPage");

                            }
                            IsLoading = false;
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequestTimeoutDescription;
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

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    BackMethod();

                });
            }
        }
        private void ResetData()
        {
            ReferenceNumber = string.Empty;
            IDResidencePassportNumber = string.Empty;
        }
        public void BackMethod()
        {
            ResetData();
            _navigationService.GoBack();
        }

        public EDeclerationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService,declerationServices)
        {
        }

      
    }


}

