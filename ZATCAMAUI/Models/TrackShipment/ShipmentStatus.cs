using Prism.Mvvm;

namespace ZATCAMAUI.Models.TrackShipment
{
    public class ShipmentStatus : BindableBase
    {
        bool hasVerticalLine;
        public bool HasVerticalLine { get { return hasVerticalLine; } set { hasVerticalLine = value; RaisePropertyChanged(); } }

        string statusImage;
        public string StatusImage { get { return statusImage; } set { statusImage = value; RaisePropertyChanged(); } }

        string shipmentStatusValue;
        public string ShipmentStatusValue { get { return shipmentStatusValue; } set { shipmentStatusValue = value; RaisePropertyChanged(); } }

        string shipmentStatusDateString;
        public string ShipmentStatusDateString { get { return shipmentStatusDateString; } set { shipmentStatusDateString = value; RaisePropertyChanged(); } }
    }
}

