using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using EGAZT.Controls;
using GAZT;
using System.Linq;
using System.Collections.Generic;
using EGAZT.Services.Interface;
using System.Linq.Expressions;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using System.Text.RegularExpressions;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.Helper;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel : BaseEDeclarationViewModel
    {
        #region Properties

        private bool isItsSourceSelected;
        private bool isNationalitySelected;
        private bool isComingGoingSelected;
        private bool isPortSelected;
        private bool isTravelPurposeSelected;
        private bool isPlatesCountrySelected;
        private bool isPlatesCitySelected;
        private bool isFirstTime = true;
        public bool isPassengerPage = true;
        public bool isTripPage;
        public bool isContactPage;

        List<CountryModel> countries = new List<CountryModel>();

        string refNo;
        public string RefNo { get { return refNo; } set { refNo = value; RaisePropertyChanged(); } }

        DateTime _MinimumDate = DateTime.Now.Date;
        public DateTime MinimumDate { get { return _MinimumDate; } set { _MinimumDate = value; RaisePropertyChanged(); } }

        DateTime _MaximumDate = DateTime.Now.Date.AddHours(-24);
        public DateTime MaximumDate { get { return _MaximumDate; } set { _MaximumDate = value; RaisePropertyChanged(); } }
        #endregion


        #region Commands

        public ICommand OnAppearingInfoPagesCommand
        {

            get
            {
                return new Command(async () =>
                {
                    // this condition to load countries only once for
                    // 3 paages
                    if (isFirstTime)
                    {
                        IsLoading = true;
                        var result = await DeclerationServices.GetCountries();
                        countries = result?.Item1?.data?.ToList();
                        isFirstTime = false;
                        IsLoading = false;
                    }
                    if (isPassengerPage)
                    {

                        if (!SubmitModel.travelerDeclaration.Isvisitor)
                        {
                            // Set passenger data in case the user go to the passenger
                            // and decide to go back until reaching "New Declaration page".
                            // At this moment the passenger data will be removed so need to set
                            // it again.

                            if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.firstName))
                            {
                                var passengerData = App.Locator.StateManager.GetItem("IAMLoginPassengerData");
                                SetPassangerData(passengerData);
                            }

                            IDName = AppResources.ZZNationalID;
                            IDNumberPlaceHolder = "0000000000";
                            IDNumberKeyboard = Keyboard.Numeric;
                            ReleaseDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.passIssuingDate);
                            EndDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.passExpiryDate);
                            BirthDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.birthDate);
                            if (SubmitModel.travelerDeclaration.travelID != null)
                            {
                                if (SubmitModel.travelerDeclaration.travelID.ToLower().StartsWith("1"))
                                {
                                    SubmitModel.travelerDeclaration.travelDocumentType = 5; // Citizen
                                }
                                else
                                {
                                    SubmitModel.travelerDeclaration.travelDocumentType = 3; // Resident
                                }
                            }

                        }
                        else // Visitor
                        {
                            //Set Default value for first time only
                            if (SubmitModel.travelerDeclaration.gender == 0)
                                SubmitModel.travelerDeclaration.gender = 1; // Male

                            //Set Default value for first time only
                            if (SubmitModel.travelerDeclaration.travelDocumentType == 0)
                            {
                                IDName = AppResources.Passport;
                                IDNumberPlaceHolder = "XX000000";
                                IDNumberKeyboard = Keyboard.Text;
                                SubmitModel.travelerDeclaration.travelDocumentType = 4; // Visitor Passport => 4
                            }

                        }
                    }

                    else if (isContactPage & !SubmitModel.travelerDeclaration.Isvisitor)
                    {
                        MobileNumber = SubmitModel.travelerDeclaration.phoneNumber;
                    }
                    IsArrivingPlaneSelected = SubmitModel.travelerDeclaration.travelingType == 1 ? true : false;
                    HeaderTitle = IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;



                });
            }
        }

        public ICommand SearchEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    try
                    {
                        if (e != null)
                        {
                            var entry = e as BorderlessEntry;
                            var value = entry.Text.ToLower();
                            if (string.IsNullOrWhiteSpace(value))
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(TempBottomSheetList);
                            else
                            {
                                var result = TempBottomSheetList.Where(s => s.Name.ToLower().Contains(value));
                                BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        SearchText = string.Empty;
                    }

                });
            }
        }

        public ICommand SelectedBottomItemCommand
        {
            get
            {
                return new Command<BottomSheetModel>((e) =>
                {
                    try
                    {
                        if (isNationalitySelected)
                        {

                            SubmitModel.travelerDeclaration.NationalityName = e.Name;
                            SubmitModel.travelerDeclaration.nationality = int.Parse(e.Id);
                            isNationalitySelected = false;

                        }
                        else if (isItsSourceSelected)
                        {
                            SubmitModel.travelerDeclaration.travelIssuerID = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.travelIssuerName = e.Name;
                            SubmitModel.travelerDeclaration.passIssuingCountry = int.Parse(e.Id);
                            isItsSourceSelected = false;

                        }
                        else if (isComingGoingSelected)
                        {
                            SubmitModel.travelerDeclaration.arrivingFromDepartingTo = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.arrivingFromDepartingToName = e.Name;
                            isComingGoingSelected = false;

                        }
                        else if (isPortSelected)
                        {
                            SubmitModel.travelerDeclaration.port = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.portName = e.Name;
                            isPortSelected = false;

                        }
                        else if (isTravelPurposeSelected)
                        {
                            SubmitModel.travelerDeclaration.travelPurpose = e.Id;
                            SubmitModel.travelerDeclaration.travelPurposeName = e.Name;
                            isTravelPurposeSelected = false;

                        }
                        else if (isPlatesCountrySelected)
                        {
                            HasPlatesCity = false;
                            SubmitModel.travelerDeclaration.plateCountryCode = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.PlatesCountryName = e.Name;
                            isPlatesCountrySelected = false;
                            if (SubmitModel.travelerDeclaration.plateCountryCode == 120 || SubmitModel.travelerDeclaration.plateCountryCode == 110 || SubmitModel.travelerDeclaration.plateCountryCode == 115 | SubmitModel.travelerDeclaration.plateCountryCode == 113)
                            {
                                HasPlatesCity = true;
                            }
                            else
                            {
                                HasPlatesCity = false;
                            }
                           
                            SubmitModel.travelerDeclaration.plateCityCode = 0;
                            SubmitModel.travelerDeclaration.PlatesCityName = string.Empty;

                        }
                        else if (isPlatesCitySelected)
                        {
                            SubmitModel.travelerDeclaration.plateCityCode = int.Parse(e.Id);
                            SubmitModel.travelerDeclaration.PlatesCityName = e.Name;
                            isPlatesCitySelected = false;

                        }
                        else
                        {
                            SubmitModel.travelerDeclaration.CountryCode = $"+{Regex.Replace(e.Name, @"[^\d]", "")}";

                        }

                        HeaderTitle = IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
                        IsShowBottomSheet = false;
                        SearchText = string.Empty;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }


                });
            }
        }

        public ICommand BackToHomeCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("/Home", "0");

                });
            }
        }

        public ICommand DownlOadFileCommand
        {
            get
            {
                return new Command(() =>
                {
                    DownLoadEdeclerationPdf();

                });
            }
        }

        private void DownLoadEdeclerationPdf()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    IsLoading = true;
                    DownloadFile downloadFile = new DownloadFile();
                    string Lang = "ar";
                    if (!App.IsArabic)
                    {
                        Lang = "en";

                    }
                    else
                    {
                        Lang = "ar";
                    }
                    await downloadFile.DownloadAcknowledgementAsync($"{App.VatCustom}Reports?refCode={TravelerDeclarationResponse.ReferenceID}&travelId={TravelerDeclarationResponse.travelID}&languageCode={Lang}", _dialogService);
                    IsLoading = false;
                }
                catch (Exception)
                {
                    IsLoading = false;
                }
            });

        }


        public ICommand CloseAcknowledgePopUpPageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PopAsync(true);

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

        public ICommand GoToPaymentCommand
        {
            get
            {
                return new Command(_ =>
                {
                    _navigationService.NavigateTo("EDeclarationPaymentPage", TravelerDeclarationResponse);

                });
            }
        }
        public void BackMethod()
        {
            if (IsShowBottomSheet)
            {
                IsShowBottomSheet = false;
                HeaderTitle = IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;

                return;
            }

            _navigationService.GoBack();
        }


        #endregion

        public EDeclarationInformationsViewModel(INavigationService navigationService, IDialogService dialogService, IE_DeclerationServices declerationServices) : base(navigationService, dialogService, declerationServices)
        {
        }
    }
}

