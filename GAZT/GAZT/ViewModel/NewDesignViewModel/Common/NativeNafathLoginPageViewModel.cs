using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.NativeNafath;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.Common.NativeNafath;
using EGAZT.Views.NewDesign.CustomServicesPages.Transaction_Reception;
using EGAZT.Views.NewDesign.EDeclaration;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Prism.Navigation.Xaml;
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
                                var navigation = Application.Current.MainPage.Navigation;
                                var currentPage = navigation.NavigationStack.LastOrDefault();
                                //_navigationService.NavigateTo("NativeConfirmNafathPage");
                                navigation.InsertPageBefore(new NativeConfirmNafathPage(), currentPage);

                                _navigationService.GoBack();
                                // _navigationService.NavigateTo("NativeConfirmNafathPage");
                                await Task.Delay(10000);
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
        NativeNafathStatusModel userData;
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
                            if (data.result.status == "EXPIRED"||data.result.status== "REJECTED")
                            {
                                var navigation = Application.Current.MainPage.Navigation;
                                var currentPage = navigation.NavigationStack.LastOrDefault();
                                IsLoading = false;
                               // navigation.InsertPageBefore(new NativeNafathPage(""),currentPage);
                                _navigationService.GoBack();
                                return;
                            }
                            if (data.result.status== "WAITING")
                            {
                              await  GetNafathStatus();
                                //return;
                            }
                          
                            
                             userData = data;
                            if (userData != null&& userData.result.userInfo!=null)
                            {


                                /*var navigation = Application.Current.MainPage.Navigation;
                                var currentPage = navigation.NavigationStack.LastOrDefault();
                                navigation.InsertPageBefore(new TransactionReceptionView(userData), currentPage);*/
                               await GetNafathCustomProfile(userData.result.userInfo.id.ToString(), userData.result.userInfo.dateOfBirthH.Replace('-','/'), userData.result.userInfo.dateOfBirthG, userData.result.userInfo.idInfo.idExpiryDateG, userData.result.userInfo.idInfo.idIssueDateG);
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
            }
            catch (Exception ex)
            {
                MessageTxt = AppResources.Somethingwentwrong;
                IsShowMsgView = true;
                IsLoading = false;
            }
        }
        private async Task GetNafathCustomProfile(string NationalIqamaId, string HBD,string BD, string IdEXPDATe, string IdissueEXPDATe)
        {
            try
            {
                IsLoading = true;
               // HBD = "1380/07/13";
                
                var submitRes = await NativeNafath.GetNfathProfile(HBD.Replace('/','-'), NationalIqamaId);
                if (submitRes.Item2)
                {
                    var data = submitRes.Item1;
                    if (data.data != null&&data.data.id!=0)
                    {
                        var navigation = Application.Current.MainPage.Navigation;
                        var currentPage = navigation.NavigationStack.LastOrDefault();
                        IsLoading = false;
                        NationalIqamaId = "";
                        var IamaDATA = data.data;
                        IamaDATA.birthDate =DateTime.Parse(BD);
                        IamaDATA.cardIssueDateHijri = IdissueEXPDATe;
                        IamaDATA.idExpiryDateHijri = IdEXPDATe;
                        if (PageName == "NewDeclarationPage")
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
                        //  _navigationService.NavigateTo("RegisterZATCAUserPage",2);
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
            catch (Exception ex)
            {
                MessageTxt = AppResources.Somethingwentwrong;
                IsShowMsgView = true;
                IsLoading = false;
            }
        }
        ZATCAUserRegisterModel User = new ZATCAUserRegisterModel();
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

            User.identityTypeId =userData.result.userInfo.id.ToString().StartsWith("2")?1:4;

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


    }
}

