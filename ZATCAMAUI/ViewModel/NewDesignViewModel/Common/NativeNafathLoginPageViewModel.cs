using System.Windows.Input;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.NativeNafath;
using ZATCAMAUI.Views.NewDesign.Common.NativeNafath;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Common
{
    public class NativeNafathLoginPageViewModel : BaseViewModel
    {
       
        string nationalIqamaId = string.Empty;
        public string NationalIqamaId { get { return nationalIqamaId; } set { nationalIqamaId = value; OnPropertyChanged(); } }

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
                if (!NetworkCheck.IsInternet())
                {
                    IsShowMsgView = true;
                    IsLoading = false;
                    MessageTxt = AppResources.NoInternet;
                    return;
                }
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
                                TransactionId = login.transactionId;
                                IsLoading = false;
                                var navigation = Application.Current.MainPage.Navigation;
                                var currentPage = navigation.NavigationStack.LastOrDefault();
                                navigation.InsertPageBefore(new NativeConfirmNafathPage(PageName,NationalIqamaId, login.randomNumber.ToString(), TransactionId), currentPage);
                                _navigationService.GoBack();
                                NationalIqamaId = string.Empty;

                            }
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(data.header.moreInformation?.errorDetails[0].message))
                    {
                        NationalIqamaId = string.Empty;
                        MessageTxt = data.header.moreInformation?.errorDetails[0].message;
                        IsShowMsgView = true;
                        IsLoading = false;

                    }
                    else if (!string.IsNullOrWhiteSpace(data.header.status?.description))
                    {
                        NationalIqamaId = string.Empty;
                        MessageTxt = AppResources.NAFATHStatus;
                        IsShowMsgView = true;
                        IsLoading = false;

                    }

                    else
                    {
                        NationalIqamaId = string.Empty;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                        IsShowMsgView = true;
                        IsLoading = false;

                    }
                }
              
                else
                {
                    NationalIqamaId = string.Empty;
                    MessageTxt = AppResources.RequestTimeoutDescription;
                    IsShowMsgView = true;
                    IsLoading = false;

                }

            }
            catch (Exception)
            {
                NationalIqamaId = string.Empty;
                MessageTxt = AppResources.Somethingwentwrong;
                IsShowMsgView = true;
                IsLoading = false;
            }
        }

       
       

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    NationalIqamaId = string.Empty;
                    IsLoading = false;
                    _navigationService.GoBack();

                });
            }
        }
    }
}

