using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.NativeNafath;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Common
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
                return new Command(async () =>
                {
                    if (IsValidInfo())
                        await RegisterUser(UnRegisteredUser);
                });
            }
        }
        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    var navigation = Application.Current.MainPage.Navigation;

                    var currentPage = navigation.NavigationStack.LastOrDefault();

                    if (CommingFrom == 1)
                    {
                        // navigation.InsertPageBefore(new EDeclarationPage(), currentPage);
                        _navigationService.GoBack();
                    }
                    else
                    {
                        // navigation.InsertPageBefore(new Home(), currentPage);
                        _navigationService.GoBack();
                    }

                    ResetData();
                });
            }
        }

        private void ResetData()
        {
            UnRegisteredUser.mobileNumber = string.Empty;
            UnRegisteredUser.emailAddress = string.Empty;
            UnRegisteredUser.address = string.Empty;
        }

        private bool IsValidInfo()
        {

            Regex KSAphoneRegex = new Regex(@"^5[0-9]{8}$");
            Regex phoneRegex = new Regex(@"^[0-9]+$");
            Regex Email = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            Regex address = new Regex(@"[^a-zA-Z0-9\u0621-\u064Aa\u0660-\u0669\s]");
            if (string.IsNullOrWhiteSpace(UnRegisteredUser.mobileNumber)
                    || string.IsNullOrWhiteSpace(UnRegisteredUser.emailAddress)
                    || string.IsNullOrWhiteSpace(UnRegisteredUser.address))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;

            }
            else if (!Email.IsMatch(UnRegisteredUser.emailAddress.ToLower()))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.InvalidEmailFormat;
                return false;
            }
            else if (!KSAphoneRegex.IsMatch(UnRegisteredUser.mobileNumber))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.EnterValidMobileNumber;
                return false;
            }
            else if (address.IsMatch(UnRegisteredUser.address))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.AddressKSAValidation;
                return false;
            }
            return true;

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
                    var data = JsonConvert.DeserializeObject<DATAPowerBaseResponse<RegisterationResponseModel>>(conent);
                    if (data.header.status.code == "I000000")
                    {
                        HandleSuccessUserNavigation();
                        ResetData();
                    }
                    else if (!string.IsNullOrWhiteSpace(data.header.moreInformation?.backendErrors))
                    {
                        MessageTxt = data.header.moreInformation?.backendErrors;
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
            catch (Exception)
            {
                IsLoading = false;
            }
        }

        private void HandleSuccessUserNavigation()
        {
            var navigation = Application.Current.MainPage.Navigation;

            var currentPage = navigation.NavigationStack.LastOrDefault();

            var payload = App.Locator.StateManager.GetItem("IAMLoginPassengerData");
            _navigationService.GoBack();
            IsLoading = false;

        }
    }
}

