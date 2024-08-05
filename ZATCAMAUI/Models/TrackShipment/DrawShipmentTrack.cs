using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models.TrackShipment
{
    public class DrawShipmentTrack : ObservableRecipient
    {
        bool hasImportYear;
        public bool HasImportYear { get { return hasImportYear; } set { hasImportYear = value; OnPropertyChanged(); } }

        bool hasSubTitle;
        public bool HasSubTitle { get { return hasSubTitle; } set { hasSubTitle = value; OnPropertyChanged(); } }

        bool hasPortName;
        public bool HasPortName { get { return hasPortName; } set { hasPortName = value; OnPropertyChanged(); } }

        bool hasSearchBy;
        public bool HasSearchBy { get { return hasSearchBy; } set { hasSearchBy = value; OnPropertyChanged(); } }

        bool hasDeclarationNumber;
        public bool HasDeclarationNumber { get { return hasDeclarationNumber; } set { hasDeclarationNumber = value; OnPropertyChanged(); } }

        bool hasDeclarationDate;
        public bool HasDeclarationDate { get { return hasDeclarationDate; } set { hasDeclarationDate = value; OnPropertyChanged(); } }

        bool hasBillNumber;
        public bool HasBillNumber { get { return hasBillNumber; } set { hasBillNumber = value; OnPropertyChanged(); } }

        bool hasContainerNumber;
        public bool HasContainerNumber { get { return hasContainerNumber; } set { hasContainerNumber = value; OnPropertyChanged(); } }

        bool hasDeclarationCards;
        public bool HasDeclarationCards { get { return hasDeclarationCards; } set { hasDeclarationCards = value; OnPropertyChanged(); } }

        bool isDeclarationSelected = true;
        public bool IsDeclarationSelected { get { return isDeclarationSelected; } set { isDeclarationSelected = value; OnPropertyChanged(); } }

        string shipmentTrackName;
        public string ShipmentTrackName { get { return shipmentTrackName; } set { shipmentTrackName = value; OnPropertyChanged(); } }

        string shipmentCardImage;
        public string ShipmentCardImage { get { return shipmentCardImage; } set { shipmentCardImage = value; OnPropertyChanged(); } }
    }
}

