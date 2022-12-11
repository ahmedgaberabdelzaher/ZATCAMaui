using System;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models.EDeclerationsModel;
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

                    if (selectedTrip == (int)TripName.AirTrip)
                    {
                        TripCard.AirImage = "QSelected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QUnselected.png";

                        TripCard.AirTextColor = Color.White;
                        TripCard.SeaTextColor = Color.FromHex("#002447");
                        TripCard.LandTextColor = Color.FromHex("#002447");

                        TripCard.IsAirTripSelected = true;
                    }

                    else if (selectedTrip == (int)TripName.LandTrip)
                    {
                        TripCard.AirImage = "QUnselected.png";
                        TripCard.SeaImage = "QUnselected.png";
                        TripCard.LandImage = "QSelected.png";

                        TripCard.AirTextColor = Color.FromHex("#002447");
                        TripCard.SeaTextColor = Color.FromHex("#002447");
                        TripCard.LandTextColor = Color.White;

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
                    IsArrivingPlaneSelected = IsArrivingPlaneSelected == true ? false : true;
                });
            }
        }

        public ICommand OpenEntranceExitPortCommand
        {
            get
            {
                return new Command(_ =>
                {
                    
                });
            }
        }

        public ICommand OpenTravelPurposeCommand
        {
            get
            {
                return new Command(_ =>
                {
                    
                });
            }
        }

        public ICommand GoToContactCommand
        {
            get
            {
                return new Command(_ =>
                {
                    _navigationService.NavigateTo("ContactInformationPage");
                });
            }
        }
    }
}

