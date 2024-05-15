using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using EGAZT.Controls;
using EGAZT.Helper;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using EGAZT.AppConfigurations;
using Xamarin.Forms;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;
using EGAZT.Models.TahqaqModels;
using EGAZT.Models.NativeNafath;
using EGAZT.Views.NewDesign.Common.NativeNafath;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class BaseEDeclarationViewModel : BaseViewModel
    {

        #region Properties
        bool isArrivingPlaneSelected = true;
        public bool IsArrivingPlaneSelected { get { return isArrivingPlaneSelected; } set { isArrivingPlaneSelected = value; RaisePropertyChanged(); } }

        bool isYesSelected = true;
        public bool IsYesSelected { get { return isYesSelected; } set { isYesSelected = value; RaisePropertyChanged(); } }

        public static Dictionary<string, object> QAnswereDictionary { get; set; }


        ObservableCollection<BottomSheetModel> bottomSheetList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> BottomSheetList { get { return bottomSheetList; } set { bottomSheetList = value; RaisePropertyChanged(); } }

        public ObservableCollection<BottomSheetModel> TempBottomSheetList { get; set; } = new ObservableCollection<BottomSheetModel>();

        bool isShowBottomSheet;
        public bool IsShowBottomSheet { get { return isShowBottomSheet; } set { isShowBottomSheet = value; RaisePropertyChanged(); } }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { headerTitle = value; RaisePropertyChanged(); } }

        string searchText;
        public string SearchText { get { return searchText; } set { searchText = value; RaisePropertyChanged(); } }

        EDeclerationSubmitModel _submitModel;
        public EDeclerationSubmitModel SubmitModel { get { return _submitModel; } set { _submitModel = value; RaisePropertyChanged(); } }

        public IDictionary<string, object> IamLoginPayloadData;

        string iqamaTypeDescription;
        public string IqamaTypeDescription { get { return iqamaTypeDescription; } set { iqamaTypeDescription = value; RaisePropertyChanged(); } }

        bool isIqama;
        public bool IsIqama { get { return isIqama; } set { isIqama = value; RaisePropertyChanged(); } }



        string _EndDateString;
        public string EndDateString {
            get { return _EndDateString; }
            set {

                _EndDateString = value;
                RaisePropertyChanged(); } }

        #endregion


        #region Commands
        public ICommand CardSelectionCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    IsArrivingPlaneSelected = e == "1" ? true : false;
                    SubmitModel.travelerDeclaration.travelingType = IsArrivingPlaneSelected ? 1 : 2;
                    HeaderTitle = IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
                });
            }
        }
        public ICommand ReviewPreviousLoggedInCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("ListUserRequestsPage");
                });
            }
        }

        public ICommand GoToProductDeclarationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PopAsync(true);
                    SubmitModel.travelerDeclaration.travelingType = IsArrivingPlaneSelected ? 1 : 2;
                    HeaderTitle = IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
                    _navigationService.NavigateTo("ChooseQuestionsPage");
                });
            }
        }

        public ICommand GoToEDeclarationTermsPopupPageCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    EDeclarationTermsPopupPage poupWindow = new EDeclarationTermsPopupPage();
                    await PopupNavigation.Instance.PushAsync(poupWindow);

                });
            }
        }
        public ICommand OpenCustomInformationLinkCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                     Xamarin.Essentials.Launcher.OpenAsync(PageSettings.GetCustomDeclarationInformationURl());
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }
        #endregion
        public IE_DeclerationServices DeclerationServices;
        public INativeNafath _nativeNafath;
        public BaseEDeclarationViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices,INativeNafath nativeNafath) : base(navigationService, dialogService)
        {
            SubmitModel = App.Locator.EDeclerationSubmitModel;
            DeclerationServices = declerationServices;
            _nativeNafath = nativeNafath;
        }

        public  void SetPassangerData(object data)
        {

            try
            {
                if (data == null) return;

                //IDictionary<string, object> iamLoginPayloadData = data as IDictionary<string, object>;
                 var iamLoginPayloadData = data as CustomsIamUser;
                SubmitModel.travelerDeclaration.email = iamLoginPayloadData.email.ToString();
                SubmitModel.travelerDeclaration.phoneNumber = iamLoginPayloadData.mobileNumber.ToString();
                SubmitModel.travelerDeclaration.firstName = iamLoginPayloadData.firstName.ToString();
                SubmitModel.travelerDeclaration.middleName = iamLoginPayloadData.secondName.ToString();
                SubmitModel.travelerDeclaration.lastName = iamLoginPayloadData.thirdName.ToString();
                SubmitModel.travelerDeclaration.FullName = $"{SubmitModel.travelerDeclaration.firstName} {SubmitModel.travelerDeclaration.lastName}";
                App.Locator.StateManager.SetItem("FullName", SubmitModel.travelerDeclaration.FullName);
                SubmitModel.travelerDeclaration.NationalityName = iamLoginPayloadData.nationalityNameArabic?? iamLoginPayloadData.nationalityNameEnglish.ToString();
                SubmitModel.travelerDeclaration.nationality = int.Parse(iamLoginPayloadData.nationalityId.ToString());

                // Its source is empty so it must be KSA as the user maybe resident or citizen.
                SubmitModel.travelerDeclaration.travelIssuerName = App.IsArabic ? "السعودية" : "SAUDI ARABIA";
                SubmitModel.travelerDeclaration.travelIssuerID = 100; // it must be KSA => 100 because the user is resident or citizen

                SubmitModel.travelerDeclaration.gender = iamLoginPayloadData.gender.ToString() == "Male" ? 1 : 2;
                SubmitModel.travelerDeclaration.travelID = iamLoginPayloadData.nationalId.ToString();
                App.Locator.StateManager.SetItem("TravelId", SubmitModel.travelerDeclaration.travelID);
                SubmitModel.travelerDeclaration.birthDate = iamLoginPayloadData.birthDate;

                SubmitModel.travelerDeclaration.passIssuingDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData.cardIssueDateHijri.ToString());

                SubmitModel.travelerDeclaration.passExpiryDate = DateTimeHelper.DateTimeFormater(iamLoginPayloadData.idExpiryDateHijri.ToString());
                if (iamLoginPayloadData.nationalId.StartsWith("2"))
                {
                    IsIqama = true;
                }
                else
                {
                    IsIqama = false;
                }
               GetPremiumResidencyType(iamLoginPayloadData.nationalId, iamLoginPayloadData.dateOfBirthHijri).ConfigureAwait(false);
            }
            catch (Exception)
            {

            }

        }

        private async Task GetPremiumResidencyType(string id, string HBD)
        {
            try
            {
                IsLoading = true;
                PremiumResidencytypeBody premiumResidencytypeBody = new PremiumResidencytypeBody()
                {
                    personID = id,
                    birthDate = HBD
                };
                var premiumResidencytypeRes = await _nativeNafath.PremiumResidencyType(premiumResidencytypeBody);
                if (premiumResidencytypeRes.IsSuccessStatusCode)
                {
                    var conent = await premiumResidencytypeRes.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PremiumResidencytypeResponse>(conent);
                    if (data.header.status.code == "I000000")
                    {
                        if (data.result != null)
                        {
                            var premiumResidencytype = data.result;
                            if (premiumResidencytype != null)
                            {
                                if (premiumResidencytype.iqamaType == 2)
                                {
                                    if (DateTime.Parse(premiumResidencytype.iqamaExpiryDate) < DateTime.Now)
                                    {
                                        SubmitModel.travelerDeclaration.IqamaTypeDescription = AppResources.Residency;
                                        SubmitModel.travelerDeclaration.isPremiumResidency = false;


                                        IsLoading = false;
                                        return;
                                    }
                                }
                                    // IqamaExpiryDate = premiumResidencytype.iqamaExpiryDate;
                                    IqamaTypeDescription = premiumResidencytype.iqamaType==2?AppResources.SpecialIqamawithexpirydate:AppResources.SpecialIqamawithoutexpirydate;
                                SubmitModel.travelerDeclaration.isPremiumResidency = true;
                                SubmitModel.travelerDeclaration.premiumResidencyExpiryDate = premiumResidencytype.iqamaExpiryDate;
                                if (premiumResidencytype.iqamaType == 2)
                                {
                                 
                                    EndDateString = DateTimeHelper.DateTimeFormater(DateTime.Parse(premiumResidencytype.iqamaExpiryDate));
                                    SubmitModel.travelerDeclaration.passExpiryDate = DateTime.Parse(premiumResidencytype.iqamaExpiryDate);

                                }
                                SubmitModel.travelerDeclaration.IqamaTypeDescription = premiumResidencytype.iqamaType == 2 ? AppResources.SpecialIqamawithexpirydate : AppResources.SpecialIqamawithoutexpirydate;
                              
                                IsLoading = false;
                                
                            }
                            else
                            {
                                SubmitModel.travelerDeclaration.IqamaTypeDescription = AppResources.Residency;
                            }

                        }
                    }
                   /* else if (!string.IsNullOrWhiteSpace(data.header.status.description))
                    {
                        MessageTxt = data.header.status.description;
                        IsShowMsgView = true;
                        IsLoading = false;

                    }*/
                    else
                    {
                        SubmitModel.travelerDeclaration.IqamaTypeDescription = AppResources.Residency;
                        IsLoading = false;

                    }
                }
                else
                {
                    SubmitModel.travelerDeclaration.IqamaTypeDescription = AppResources.Residency;
                    IsLoading = false;

                }

            }
            catch (Exception)
            {
                SubmitModel.travelerDeclaration.IqamaTypeDescription = AppResources.Residency;
                IsLoading = false;
            }
        }


    }
}

