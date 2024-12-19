using System.Windows.Input;
using System.Collections.ObjectModel;
using ZATCAMAUI.Models.EDeclerationsModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Helper;
#if ANDROID
using Microsoft.Maui.Handlers;
#endif
namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel
    {
        TripCardModel tripCard = new TripCardModel();
        public TripCardModel TripCard { get { return tripCard; } set { tripCard = value; } }

        string _ArrivalDepartureDateString;
        public string ArrivalDepartureDateString { get { return _ArrivalDepartureDateString; } set { _ArrivalDepartureDateString = value; OnPropertyChanged(); } }

        bool hasPlatesCity;
        public bool HasPlatesCity { get { return hasPlatesCity; } set { hasPlatesCity = value; OnPropertyChanged(); } }

        bool isCityVisible;
        public bool IsCityVisible { get { return isCityVisible; } set { isCityVisible = value; OnPropertyChanged(); } }

        public ICommand TripCardCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    var selectedTrip = int.Parse(e);

                    SubmitModel.travelerDeclaration.arrivingFromDepartingToName = string.Empty;
                    SubmitModel.travelerDeclaration.portName = string.Empty;
                    SubmitModel.travelerDeclaration.travelDate = DateTime.Now;
                    SubmitModel.travelerDeclaration.flightNumber = string.Empty;
                    SubmitModel.travelerDeclaration.plateCityCode = 0;
                    SubmitModel.travelerDeclaration.PlatesCityName = string.Empty;
                    SubmitModel.travelerDeclaration.plateCountryCode = 0;
                    SubmitModel.travelerDeclaration.PlatesCountryName = string.Empty;
                    SubmitModel.travelerDeclaration.plateNumber = string.Empty;
                    SubmitModel.travelerDeclaration.plateLetters = string.Empty;
                    HasPlatesCity = false;
                    if (selectedTrip == (int)TripName.AirTrip)
                    {
                        TripCard.AirImage = "QSelected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Colors.White;
                        TripCard.SeaTextColor = Color.FromArgb("#002447");
                        TripCard.LandTextColor = Color.FromArgb("#002447");

                        // if the user select the Tobacco & Product
                        // so we will remove "Traveler Count in XAML","Trip Number" & "Travel Purpose in XAML"
                        TripCard.IsAirTripSelected = SubmitModel.travelerDeclaration.IsDisclosure ? true : false;
                        TripCard.IsLandTripSelected = false;
                        SubmitModel.travelerDeclaration.tripeType = int.Parse(e); // 1=>Air
                    }

                    else if (selectedTrip == (int)TripName.LandTrip)
                    {
                        TripCard.AirImage = "QUnselected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QSelected.png";

                        TripCard.AirTextColor = Color.FromArgb("#002447");
                        TripCard.SeaTextColor = Color.FromArgb("#002447");
                        TripCard.LandTextColor = Colors.White;
                        SubmitModel.travelerDeclaration.tripeType = int.Parse(e); // 2=>Land
                        TripCard.IsAirTripSelected = false;
                        TripCard.IsLandTripSelected = true;
                    }
                    else
                    {
                        TripCard.AirImage = "QUnselected.png";
                        TripCard.SeaImage = "QSelected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Color.FromArgb("#002447");
                        TripCard.SeaTextColor = Colors.White;
                        TripCard.LandTextColor = Color.FromArgb("#002447");

                        TripCard.IsAirTripSelected = false;
                        TripCard.IsLandTripSelected = false;
                        SubmitModel.travelerDeclaration.tripeType = int.Parse(e); // 3> Sea
                    }

                });
            }
        }

        public ICommand OpenComingGoingCommand
        {
            get
            {
                return new Command(_ =>
                {
                    try
                    {
                        isNationalitySelected = false;
                        isItsSourceSelected = false;
                        isPortSelected = false;
                        isComingGoingSelected = true;
                        isTravelPurposeSelected = false;
                        isPlatesCountrySelected = false;
                        isPlatesCitySelected = false;
                        var result = countries?.Select(c => new BottomSheetModel() { Id = c.countryCode.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ZZZZCountry;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand GoToPersonalInfoCommand
        {
            get
            {
                return new Command(_ =>
                {
                    try
                    {
                        if (IsValidTripInfo() && !SubmitModel.travelerDeclaration.Isvisitor) // Is loggedIn
                        {
                            isPassengerPage = true;
                            _navigationService.NavigateTo("PassengerInformationPage");
                        }
                    }
                    catch (Exception)
                    {
                    }

                });
            }
        }

        public ICommand ArrivalSelectedDateCommand
        {
            get
            {
                return new Command<Entry>((control) =>
                {
                    try
                    {

                        ArrivalDepartureDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.travelDate);

                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand ArrivalDateClickedCommand
        {
            get
            {
                return new Command<DatePicker>((control) =>
                {
                    try
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
#if ANDROID
                                var handler = control.Handler as IDatePickerHandler;
                                handler.PlatformView.PerformClick();
#endif
#if IOS
                            control?.Focus();
#endif
                        });

                        if (SubmitModel.travelerDeclaration.travelDate.Date == DateTime.Now.Date)
                            ArrivalDepartureDateString = DateTimeHelper.DateTimeFormater(SubmitModel.travelerDeclaration.travelDate);
                    }
                    catch (Exception)
                    {

                    }

                });
            }
        }

        public ICommand OpenEntranceExitPortCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    try
                    {
                        IsLoading = true;
                        isNationalitySelected = false;
                        isItsSourceSelected = false;
                        isPortSelected = true;
                        isComingGoingSelected = false;
                        isTravelPurposeSelected = false;
                        isPlatesCountrySelected = false;
                        isPlatesCitySelected = false;
                        var result = await DeclerationServices.GetPorts(SubmitModel.travelerDeclaration.tripeType);
                        if (!string.IsNullOrWhiteSpace(result?.Item3))
                        {
                            IsShowMsgView = true;
                            MessageTxt = result?.Item3;
                            return;
                        }
                        var ports = result?.Item1?.data?.ToList();
                        var bottom = ports?.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(bottom);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.Port;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand OpenTravelPurposeCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    try
                    {
                        IsLoading = true;
                        isNationalitySelected = false;
                        isItsSourceSelected = false;
                        isPortSelected = false;
                        isComingGoingSelected = false;
                        isTravelPurposeSelected = true;
                        isPlatesCountrySelected = false;
                        isPlatesCitySelected = false;
                        var result = await DeclerationServices.GetTravelPurpose();
                        if (!string.IsNullOrWhiteSpace(result?.Item3))
                        {
                            IsShowMsgView = true;
                            MessageTxt = result?.Item3;
                            return;
                        }
                        var travelPurposes = result?.Item1?.data?.ToList();
                        var bottom = travelPurposes?.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(bottom);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.TravelPurpose;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand OpenPlatesCountryCommand
        {
            get
            {
                return new Command(_ =>
                {
                    try
                    {
                        isNationalitySelected = false;
                        isItsSourceSelected = false;
                        isPortSelected = false;
                        isComingGoingSelected = false;
                        isTravelPurposeSelected = false;
                        isPlatesCountrySelected = true;
                        isPlatesCitySelected = false;
                        var result = countries?.Select(c => new BottomSheetModel() { Id = c.countryCode.ToString(), Name = c.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ZZZZCountry;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand OpenPlatesCityCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    try
                    {
                        IsLoading = true;
                        isNationalitySelected = false;
                        isItsSourceSelected = false;
                        isPortSelected = false;
                        isComingGoingSelected = false;
                        isTravelPurposeSelected = false;
                        isPlatesCountrySelected = false;
                        isPlatesCitySelected = true;
                        var result = await DeclerationServices.GetPlatesCity(SubmitModel.travelerDeclaration.plateCountryCode);
                        if (!string.IsNullOrWhiteSpace(result?.Item3))
                        {
                            IsShowMsgView = true;
                            MessageTxt = result?.Item3;
                            return;
                        }
                        var platesCity = result?.Item1?.data?.ToList();
                        var bottom = platesCity?.Select(pc => new BottomSheetModel() { Id = pc.cityCode.ToString(), Name = pc.Name });
                        BottomSheetList = new ObservableCollection<BottomSheetModel>(bottom);
                        IsShowBottomSheet = true;
                        HeaderTitle = AppResources.ZZZZCity;
                        TempBottomSheetList = new ObservableCollection<BottomSheetModel>(BottomSheetList);
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand GoToContactCommand
        {
            get
            {
                return new Command(_ =>
                {
                    if (IsValidTripInfo())
                    {
                        isContactPage = true;
                        _navigationService.NavigateTo("ContactInformationPage");
                    }

                });
            }
        }
        public ICommand PlateLLetterTextChangedCommand
        {
            get
            {
                return new Command(_ =>
                {
                    if (!string.IsNullOrEmpty(SubmitModel.travelerDeclaration.plateLetters)
                        && SubmitModel.travelerDeclaration.plateCountryCode == 113)
                    {
                        HasPlatesCity = false;
                    }
                    else if (string.IsNullOrEmpty(SubmitModel.travelerDeclaration.plateLetters)
                    && SubmitModel.travelerDeclaration.plateCountryCode == 113)
                    {
                        HasPlatesCity = true;
                    }
                });
            }
        }

        private bool IsValidTripInfo()
        {
            if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.arrivingFromDepartingToName)
            || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.portName))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
            }

            else if (SubmitModel.travelerDeclaration.plateCountryCode == 120 ||
                     SubmitModel.travelerDeclaration.plateCountryCode == 110 ||
                     SubmitModel.travelerDeclaration.plateCountryCode == 115)
            {
                if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.PlatesCityName))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ChooseCity;
                    return false;
                }
            }

            else if (SubmitModel.travelerDeclaration.plateCountryCode == 113)
            {
                if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.PlatesCityName)
                    && !string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.plateNumber)
                    && string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.plateLetters))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ChooseCity;
                    return false;
                }
            }

            else if (SubmitModel.travelerDeclaration.travelDate.Date < DateTime.Now.Date)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.ComingGoingDateValidation;
                return false;
            }
            else if (SubmitModel.travelerDeclaration.IsDisclosure)
            {
                if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.travelPurposeName))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }

                else if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.travelersCount)
                              || int.Parse(SubmitModel.travelerDeclaration.travelersCount) <= 0)
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.TravelerCountValidation;
                    return false;
                }

                else if (SubmitModel.travelerDeclaration.tripeType == 1)
                {
                    if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.flightNumber))
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequiredData;
                        return false;
                    }
                }

            }

            return true;


        }
    }
}

