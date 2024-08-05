
using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models.TrackShipment
{
    public class ShipmentStatus : ObservableRecipient
    {
        bool hasVerticalLine;
        public bool HasVerticalLine { get { return hasVerticalLine; } set { hasVerticalLine = value; OnPropertyChanged(); } }

        string statusImage;
        public string StatusImage { get { return statusImage; } set { statusImage = value; OnPropertyChanged(); } }

        string shipmentStatusValue;
        public string ShipmentStatusValue { get { return shipmentStatusValue; } set { shipmentStatusValue = value; OnPropertyChanged(); } }

        string shipmentStatusDateString;
        public string ShipmentStatusDateString { get { return shipmentStatusDateString; } set { shipmentStatusDateString = value; OnPropertyChanged(); } }
    }
}

