using System;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.Helper;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel
    {
        TripCardModel tripCard = new TripCardModel();
        public TripCardModel TripCard { get { return tripCard; } set { tripCard = value; } }

        string _ArrivalDepartureDateString;
        public string ArrivalDepartureDateString { get { return _ArrivalDepartureDateString; } set { _ArrivalDepartureDateString = value; RaisePropertyChanged(); } }

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
                    if (selectedTrip == (int)TripName.AirTrip)
                    {
                        TripCard.AirImage = "QSelected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Color.White;
                        TripCard.SeaTextColor = Color.FromHex("#002447");
                        TripCard.LandTextColor = Color.FromHex("#002447");

                        // if the user select the Tobacco & Product
                        // so we will remove "Traveler Count in XAML","Trip Number" & "Travel Purpose in XAML"
                        TripCard.IsAirTripSelected = SubmitModel.travelerDeclaration.IsDisclosure ? true : false;
                        SubmitModel.travelerDeclaration.tripeType = int.Parse(e); // 1=>Air
                    }

                    else if (selectedTrip == (int)TripName.LandTrip)
                    {
                        TripCard.AirImage = "QUnselected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QSelected.png";

                        TripCard.AirTextColor = Color.FromHex("#002447");
                        TripCard.SeaTextColor = Color.FromHex("#002447");
                        TripCard.LandTextColor = Color.White;
                        SubmitModel.travelerDeclaration.tripeType = int.Parse(e); // 2=>Land
                        TripCard.IsAirTripSelected = false;
                    }
                    else
                    {
                        TripCard.AirImage = "QUnselected.png";
                        TripCard.SeaImage = "QSelected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Color.FromHex("#002447");
                        TripCard.SeaTextColor = Color.White;
                        TripCard.LandTextColor = Color.FromHex("#002447");

                        TripCard.IsAirTripSelected = false;
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
                        if (IsValidateTripInfo() && !SubmitModel.travelerDeclaration.Isvisitor) // Is loggedIn
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
                        control?.Focus();

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
                        var result = await DeclerationServices.GetPorts(SubmitModel.travelerDeclaration.tripeType);
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
                        var result = await DeclerationServices.GetTravelPurpose();
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

        public ICommand GoToContactCommand
        {
            get
            {
                return new Command(_ =>
                {
                    if (IsValidateTripInfo())
                    {
                        isContactPage = true;
                        _navigationService.NavigateTo("ContactInformationPage");
                    }

                });
            }
        }

        private bool IsValidateTripInfo()
        {
            if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.arrivingFromDepartingToName)
            || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.portName))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
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

