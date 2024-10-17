
using System.Globalization;
using System.Windows.Input;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.NativeNafath;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using ZATCAMAUI.Views.NewDesign.EDeclaration;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Common
{
	public class NativeConfirmNafathPageViewModel:BaseViewModel
	{
		public INativeNafath NativeNafath;
        NativeNafathStatusModel userData;
        ZATCAUserRegisterModel User = new ZATCAUserRegisterModel();
        public string pageName;

        string randomNumber;
        public string RandomNumber { get { return randomNumber; } set { randomNumber = value; OnPropertyChanged(); } }

        public string nationalIqamaId;

        public string transactionId;

        public bool isCancel;

        public NativeConfirmNafathPageViewModel(INativeNafath nativeNafath, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            NativeNafath = nativeNafath;
        }

        public ICommand OnAppearingConfirmNafathPageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        IsShowMsgView = true;
                        IsLoading = false;
                        MessageTxt = AppResources.NoInternet;
                        return;
                    }
                    if (isCancel) return;
                    await Task.Delay(10000);
                    await GetNafathStatus();

                });
            }
        }

        private async Task GetNafathStatus()
        {
            try
            {

                if (isCancel) return;
                IsLoading = true;
                var submitRes = await NativeNafath.GetNafathStatus(nationalIqamaId, transactionId, int.Parse(RandomNumber));
                if (submitRes.IsSuccessStatusCode)
                {
                    var conent = await submitRes.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<NativeNafathStatusModel>(conent);
                    if (data.header.status.code == "I000000")
                    {
                        if (data.result != null)
                        {
                            if (data.result.status == "EXPIRED" || data.result.status == "REJECTED")
                            {
                                if (data.result.status == "EXPIRED") MessageTxt = AppResources.NAFATHEXPIRED;
                                else MessageTxt = AppResources.NAFATHREJECTED;
                                IsShowMsgView = true;

                                var navigation = Application.Current.MainPage.Navigation;
                                var currentPage = navigation.NavigationStack.LastOrDefault();
                                IsLoading = false;
                                isCancel = false;
                               
                                await Task.Delay(10000);
                                _navigationService.GoBack();
                                return;
                            }
                            if (data.result.status == "WAITING")
                            {
                                if (isCancel) return;
                                await GetNafathStatus();
                            }


                            userData = data;
                            if (userData != null && userData.result.userInfo != null)
                            {
                                isCancel = false;
                                await GetNafathCustomProfile(userData.result.userInfo.id.ToString(), userData.result.userInfo.dateOfBirthH.Replace('-', '/'), userData.result.userInfo.dateOfBirthG, userData.result.userInfo.idInfo.idExpiryDateG, userData.result.userInfo.idInfo.idIssueDateG);
                                return;
                            }
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(data.header.moreInformation?.errorDetails[0].message))
                    {
                        isCancel = false;
                        MessageTxt = data.header.moreInformation?.errorDetails[0].message;
                        IsShowMsgView = true;
                        IsLoading = false;
                    }
                    else
                    {
                        isCancel = false;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                        IsShowMsgView = true;
                        IsLoading = false;

                    }
                }
                else
                {
                    isCancel = false;
                    MessageTxt = AppResources.RequestTimeoutDescription;
                    IsShowMsgView = true;
                    IsLoading = false;

                }
                nationalIqamaId = string.Empty;
                RandomNumber = string.Empty;
                transactionId = string.Empty;
            }
            catch (Exception)
            {
                isCancel = false;
                MessageTxt = AppResources.Somethingwentwrong;
                IsShowMsgView = true;
                IsLoading = false;
            }
        }

        private async Task GetNafathCustomProfile(string NationalIqamaId, string HBD, string BD, string IdEXPDATe, string IdissueEXPDATe)
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
                // HBD = "1380/07/13";

                var submitRes = await NativeNafath.GetNfathProfile(HBD.Replace('/', '-'), NationalIqamaId);
                if (submitRes.Item2)
                {
                    var data = submitRes.Item1;
                    if (data.data != null && data.data.id != 0)
                    {
                        var navigation = Application.Current.MainPage.Navigation;
                        var currentPage = navigation.NavigationStack.LastOrDefault();
                        IsLoading = false;
                        NationalIqamaId = "";
                        var IamaDATA = data.data;
                        IamaDATA.birthDate = DateTime.ParseExact(BD, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        IamaDATA.cardIssueDateHijri = IdissueEXPDATe;
                        IamaDATA.idExpiryDateHijri = IdEXPDATe;
                        if (pageName == "NewDeclarationPage")
                        {

                            navigation.InsertPageBefore(new NewDeclarationPage(IamaDATA), currentPage);
                        }
                        else
                        {
                            navigation.InsertPageBefore(new TransactionReceptionView(data.data), currentPage);
                        }
                        _navigationService.GoBack();
                        return;

                    }
                    else
                    {
                        HandleUnRegisteredUser();
                        var navigation = Application.Current.MainPage.Navigation;
                        var currentPage = navigation.NavigationStack.LastOrDefault();
                        IsLoading = false;
                        NationalIqamaId = "";
                        navigation.InsertPageBefore(new RegisterZATCAUserPage(2), currentPage);
                        _navigationService.GoBack();
                        return;
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

        private void HandleUnRegisteredUser()
        {
            bool language = App.IsArabic;

            User.firstName = language ? userData.result.userInfo.FirstName :
                    userData.result.userInfo.FirstName;

            User.secondName = language ? userData.result.userInfo.FatherName :
                userData.result.userInfo.FatherName;

            User.thirdName = language ? userData.result.userInfo.GrandFatherName :
                userData.result.userInfo.GrandFatherName;

            User.fourthName = language ? userData.result.userInfo.FamilyName :
                userData.result.userInfo.FamilyName;

            User.FullName = userData.result.userInfo.FullName;

            User.nationalityId = userData.result.userInfo.nationality.code;

            User.gender = userData.result.userInfo.gender == "M" ? true : false;

            User.nationalId = userData.result.userInfo.id.ToString();

            User.birthDate = userData.result.userInfo.dateOfBirthG.ToString();

            User.identityTypeId = userData.result.userInfo.id.ToString().StartsWith("2") ? 1 : 4;

            User.cityId = 30;

            User.maritalStatusId = 1;

            User.iqamaExpiryDateHijri = userData.result.userInfo.idInfo.idExpiryDateH.ToString();

            User.idExpiryDateHijri = userData.result.userInfo.idInfo.idExpiryDateH.ToString();

            User.cardIssueDateHijri = userData.result.userInfo.idInfo.idExpiryDateH.ToString();

            User.dateOfBirthHijri = userData.result.userInfo.dateOfBirthH.Replace('/', '-');
            User.mobileNumber = "";
            User.emailAddress = "";
            User.address = "";
            App.Locator.StateManager.SetItem("UnRegisteredUser", User);
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    isCancel = true;
                    IsLoading = false;
                    _navigationService.GoBack();

                });
            }
        }
    }
}

