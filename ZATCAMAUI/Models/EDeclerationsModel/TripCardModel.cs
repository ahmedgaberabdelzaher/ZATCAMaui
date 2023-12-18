using GalaSoft.MvvmLight;

namespace ZATCAMAUI.Models.EDeclerationsModel
{
    public class TripCardModel : ViewModelBase
    {
        public TripCardModel()
        {
            AirImage = "QSelected.png";
            SeaImage = "QUnselected.png";
            LandImage = "QUnselected.png";

            AirTextColor = Colors.White;
            SeaTextColor = Color.FromHex("#002447");
            LandTextColor = Color.FromHex("#002447");
        }
        public TripName Name { get; set; }

        Color _AirTextColor;
        public Color AirTextColor { get { return _AirTextColor; } set { _AirTextColor = value; RaisePropertyChanged(); } }
        Color _LandTextColor;
        public Color LandTextColor { get { return _LandTextColor; } set { _LandTextColor = value; RaisePropertyChanged(); } }
        Color _SeaTextColor;
        public Color SeaTextColor { get { return _SeaTextColor; } set { _SeaTextColor = value; RaisePropertyChanged(); } }

        string _AirImage;
        public string AirImage { get { return _AirImage; } set { _AirImage = value; RaisePropertyChanged(); } }
        string _LandImage;
        public string LandImage { get { return _LandImage; } set { _LandImage = value; RaisePropertyChanged(); } }
        string _SeaImage;
        public string SeaImage { get { return _SeaImage; } set { _SeaImage = value; RaisePropertyChanged(); } }

        bool _IsAirTripSelected = true;
        public bool IsAirTripSelected { get { return _IsAirTripSelected; } set { _IsAirTripSelected = value; RaisePropertyChanged(); } }

        bool _IsLandTripSelected;
        public bool IsLandTripSelected { get { return _IsLandTripSelected; } set { _IsLandTripSelected = value; RaisePropertyChanged(); } }

    }

    public enum TripName
    {
        AirTrip = 1,
        LandTrip = 2,
        SeaTrip = 3
    }
}

