using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.BaseModels;
using EGAZT.Models.TahqaqModels;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using EGAZT.Views.NewDesign.EDeclaration;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    public class RegisterZATCAUserViewModel : BaseViewModel
    {
        public int CommingFrom { get; set; }

        private readonly ICommonServices CommonServices;

        ZATCAUserRegisterModel unRegisteredUser;
        public ZATCAUserRegisterModel UnRegisteredUser { get { return unRegisteredUser; } set { unRegisteredUser = value; RaisePropertyChanged(); } }

        public RegisterZATCAUserViewModel(INavigationService navigationService, IDialogService dialogService, ICommonServices commonServices) : base(navigationService, dialogService)
        {
            this.CommonServices = commonServices;
        }

        public ICommand SubmitUserCommand
        {
            get
            {
                return new Command(async() =>
                {
                    await RegisterUser(UnRegisteredUser);
                });
            }
        }
        

        private async Task RegisterUser(ZATCAUserRegisterModel UnRegisteredUser)
        {
            try
            {
                IsLoading = true;
                var result = await this.CommonServices.ZATCAUserRegister(UnRegisteredUser);
                if (result.IsSuccessStatusCode)
                {
                    var conent = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Header>(conent);
                    if (data.status.code == "I000000")
                    {
                        HandleSuccessUserNavigation();
                    }
                    else if (!string.IsNullOrWhiteSpace(data.moreInformation?.backendErrors))
                    {
                        MessageTxt = data.moreInformation?.backendErrors;
                        IsShowMsgView = true;
                        IsLoading = false;
                    }
                    else
                    {
                        MessageTxt = AppResources.RequestTimeoutDescription;
                        IsShowMsgView = true;
                        IsLoading = false;

                    }
                }
                else
                {
                    MessageTxt = AppResources.RequestTimeoutDescription;
                    IsShowMsgView = true;
                    IsLoading = false;

                }
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }

        private void HandleSuccessUserNavigation()
        {
            var navigation = Application.Current.MainPage.Navigation;

            var currentPage = navigation.NavigationStack.LastOrDefault();

            var payload = App.Locator.StateManager.GetItem("IAMLoginPassengerData");

            if (payload != null)
            {
                if (CommingFrom == 1)
                {
                    navigation.InsertPageBefore(new NewDeclarationPage(payload), currentPage);
                    _navigationService.GoBack();
                }
                else
                {
                    navigation.InsertPageBefore(new TransactionReceptionView(payload), currentPage);
                    _navigationService.GoBack();
                }
            }

        }

    }
}

