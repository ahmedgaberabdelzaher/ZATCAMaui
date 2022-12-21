using System;
using System.Windows.Input;
using EGAZT.Models.EDeclerationsModel;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using System.Text.RegularExpressions;
using EGAZT.Models.BaseModels;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using Newtonsoft.Json;
using System.Threading.Tasks;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using Xamarin.Essentials;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using System.Linq;
using EGAZT.Helper;
using EGAZT.Controls;
using EGAZT.Converters;
namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel
    {

        TravelerDeclarationResponse travelerDeclarationResponse;
        public TravelerDeclarationResponse TravelerDeclarationResponse { get { return travelerDeclarationResponse; } set { travelerDeclarationResponse = value; RaisePropertyChanged(); } }

        bool isPaymentRequired;
        public bool IsPaymentRequired { get { return isPaymentRequired; } set { isPaymentRequired = value; RaisePropertyChanged(); } }

        private ObservableCollection<BottomSheetModel> countryWithFlags { get; set; } = new ObservableCollection<BottomSheetModel>();


        public ICommand GoToSuccessCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    if (IsValidateContactInfo())
                    {

                        AcknowledgePopUpPage poupWindow = new AcknowledgePopUpPage();
                        await PopupNavigation.Instance.PushAsync(poupWindow);
                    }

                });
            }
        }
        
        public ICommand GetCountryCodeCommand
        {
            get
            {
                return new Command( async _ =>
                {
                    
                    isNationalitySelected = false;
                    isItsSourceSelected = false;
                    isPortSelected = false;
                    isComingGoingSelected = false;
                    isTravelPurposeSelected = false;
                    BottomSheetList = new ObservableCollection<BottomSheetModel>();

                    if (countryWithFlags.Count == 0)
                    {
                        IsLoading = true;
                        await Task.Delay(1000);
                        foreach (var item in CountryCodeHelper.CountriesWithFlags)
                        {
                            BottomSheetList.Add(new BottomSheetModel()
                            {
                                Name = $"({item[0]}) {item[1]} {item[2]}"
                            });
                        }
                        countryWithFlags = BottomSheetList;
                        IsLoading = false;
                    }

                    else
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(countryWithFlags);

                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ZZZZCountry;
                    TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                  
                });
            }
        }

        private async Task<bool> SubmitDecleration()
        {
            try
            {
                IsLoading = true;
                var submitRes = await DeclerationServices.SubmitDecleration(SubmitModel);
                if (submitRes.IsSuccessStatusCode)
                {
                    var conent = await submitRes.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<EDeclerationSubmitResponseModel>(conent);
                    if (data.header.status.code == "I000000")
                    {
                        if (data.result != null)
                        {
                            TravelerDeclarationResponse = data.result.travelerDeclarationResponse;
                            if (TravelerDeclarationResponse != null)
                            {
                                IsPaymentRequired = TravelerDeclarationResponse.paymentIsRequired && !TravelerDeclarationResponse.paymentIsCompleted ? true :
                                    false;

                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
            return false;

        }


        public ICommand ApproveDeclarationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (SubmitModel.travelerDeclaration.IsTermsChecked)
                    {
                        await PopupNavigation.Instance.PopAsync(true);
                        var res = await SubmitDecleration();
                        if (res)
                        {
                            isSuccessPage = true;
                            _navigationService.NavigateTo("EDeclarationSuccessPage");
                        }
                        else
                        {
                            SubmitModel.travelerDeclaration.phoneNumber = SubmitModel.travelerDeclaration.phoneNumber.Remove(0, SubmitModel.travelerDeclaration.CountryCode.Length);
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequestTimeoutDescription;
                        }

                    }

                });
            }
        }

        public ICommand CheckBoxCommand
        {
            get
            {
                return new Command(() =>
                {
                    SubmitModel.travelerDeclaration.IsTermsChecked = SubmitModel.travelerDeclaration.IsTermsChecked == true ? false : true;
                });
            }
        }

        public ICommand CopyCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Clipboard.SetTextAsync(travelerDeclarationResponse.ReferenceID);
                    UserDialogs.Instance.Toast(AppResources.Copied, TimeSpan.FromSeconds(1));
                });
            }
        }

        private bool IsValidateContactInfo()
        {

           // Regex phoneRegex = new Regex(@"^5[0-9]{8}$");
            Regex phoneRegex = new Regex(@"[^\d]");
            Regex Email = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.phoneNumber)
                    || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.address)
                    || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.email))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;

            }
            else if (!Email.IsMatch(SubmitModel.travelerDeclaration.email))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.InvalidEmailFormat;
                return false;
            }
            else if (phoneRegex.IsMatch(SubmitModel.travelerDeclaration.phoneNumber))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.EnterValidMobileNumber;
                return false;
            }
            SubmitModel.travelerDeclaration.phoneNumber = SubmitModel.travelerDeclaration.CountryCode + SubmitModel.travelerDeclaration.phoneNumber;
            return true;

        }
    }
}

