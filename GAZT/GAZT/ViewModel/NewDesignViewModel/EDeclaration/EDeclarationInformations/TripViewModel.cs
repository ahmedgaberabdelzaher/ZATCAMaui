using System;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
    public partial class EDeclarationInformationsViewModel
    {
        TripCardModel tripCard = new TripCardModel();
        public TripCardModel TripCard { get { return tripCard; } set { tripCard = value; } }

        public ICommand TripCardCommand
        {
            get
            {
                return new Command<string>((e) =>
                {
                    var selectedTrip = int.Parse(e);

                    SubmitModel.travelerDeclaration.arrivingFromDepartingToName = string.Empty;
                    SubmitModel.travelerDeclaration.portName = string.Empty;
                    SubmitModel.travelerDeclaration.SelectedArrivalDepartureDate = DateTime.Now;
                    if (selectedTrip == (int)TripName.AirTrip)
                    {
                        TripCard.AirImage = "QSelected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Color.White;
                        TripCard.SeaTextColor = Color.FromHex("#002447");
                        TripCard.LandTextColor = Color.FromHex("#002447");

                        TripCard.IsAirTripSelected = true;
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
                return new Command(async _ =>
                {
                    IsLoading = true;
                    await Task.Delay(1000);
                    isComingGoingSelected = true;
                    var result = countries?.Select(c => new BottomSheetModel() { Id = c.countryCode.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(result);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.ZZZZCountry;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
                });
            }
        }

        public ICommand OpenEntranceExitPortCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    IsLoading = true;
                    isPortSelected = true;
                    var result = await DeclerationServices.GetPorts(SubmitModel.travelerDeclaration.tripeType);
                    var ports = result.Item1.data.ToList();
                    var bottom = ports?.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(bottom);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.Port;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
                });
            }
        }

        public ICommand OpenTravelPurposeCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    IsLoading = true;
                    isTravelPurposeSelected = true;
                    var result = await DeclerationServices.GetTravelPurpose();
                    var travelPurposes = result.Item1.data.ToList();
                    var bottom = travelPurposes?.Select(c => new BottomSheetModel() { Id = c.ID.ToString(), Name = c.Name }).ToList() ?? new List<BottomSheetModel>();
                    BottomSheetList = new ObservableCollection<BottomSheetModel>(bottom);
                    IsShowBottomSheet = true;
                    HeaderTitle = AppResources.TravelPurpose;
                    TempBottomSheetList = BottomSheetList;
                    IsLoading = false;
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
            || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.portName)
            || SubmitModel.travelerDeclaration.SelectedArrivalDepartureDate.Date < DateTime.Now.Date
            || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.travelPurposeName))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;
            }
            if (SubmitModel.travelerDeclaration.tripeType == 1)
            {
                if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.flightNumber))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.RequiredData;
                    return false;
                }

            }
            return true;


        }
    }
}

