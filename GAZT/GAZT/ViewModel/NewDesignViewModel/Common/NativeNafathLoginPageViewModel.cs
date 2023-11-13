using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.NativeNafath;
using EGAZT.Services.Interface;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    public class NativeNafathLoginPageViewModel : BaseViewModel
    {
        string nationalIqamaId = string.Empty;
        public string NationalIqamaId { get { return nationalIqamaId; } set { nationalIqamaId = value; RaisePropertyChanged(); } }

        string randomNumber;
        public string RandomNumber { get { return randomNumber; } set { randomNumber = value; RaisePropertyChanged(); } }

        public string TransactionId { get; set; }

        public string PageName { get; set; }

        public ICommand NativeNafathLoginCommand
        {
            get
            {
                return new Command(async () =>
                {

                    if (string.IsNullOrWhiteSpace(NationalIqamaId))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return;
                    }
                    await SubmitNafathLogin();

                });
            }
        }


        public INativeNafath NativeNafath;
        public NativeNafathLoginPageViewModel(INativeNafath nativeNafath, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            NativeNafath = nativeNafath;
        }

        private async Task SubmitNafathLogin()
        {
            try
            {
                IsLoading = true;
                var submitRes = await NativeNafath.SubmitNafath(NationalIqamaId);
                if (submitRes.IsSuccessStatusCode)
                {
                    var conent = await submitRes.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<NativeNafathModel>(conent);
                    if (data.header.status.code == "I000000")
                    {
                        if (data.result != null)
                        {
                            var login = data.result;
                            if (login != null)
                            {
                                RandomNumber = login.randomNumber.ToString();
                                TransactionId = login.transactionId;
                                IsLoading = false;
                                _navigationService.NavigateTo("NativeConfirmNafathPage");
                                await Task.Delay(45000);
                                await GetNafathStatus();
                            }
                        }
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
                MessageTxt = AppResources.Somethingwentwrong;
                IsShowMsgView = true;
                IsLoading = false;
            }
        }

        private async Task GetNafathStatus()
        {
            try
            {
                IsLoading = true;

                var submitRes = await NativeNafath.GetNafathStatus(NationalIqamaId, TransactionId, int.Parse(RandomNumber));
                if (submitRes.IsSuccessStatusCode)
                {
                    var conent = await submitRes.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<NativeNafathStatusModel>(conent);
                    if (data.header.status.code == "I000000")
                    {
                        if (data.result != null)
                        {
                            var userData = data;
                            if (userData != null)
                            {
                                _navigationService.NavigateTo(PageName, userData);
                                return;
                            }
                        }
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
                NationalIqamaId = string.Empty;
                RandomNumber = string.Empty;
                TransactionId = string.Empty;
                _navigationService.GoBack();

            }
            catch (Exception)
            {
                MessageTxt = AppResources.Somethingwentwrong;
                IsShowMsgView = true;
                IsLoading = false;
            }
        }
    }
}

