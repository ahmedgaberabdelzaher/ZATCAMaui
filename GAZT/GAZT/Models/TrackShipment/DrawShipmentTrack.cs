using System;
using Prism.Mvvm;

namespace EGAZT.Models.TrackShipment
{
    public class DrawShipmentTrack : BindableBase
    {
        bool hasSubTitle;
        public bool HasSubTitle { get { return hasSubTitle; } set { hasSubTitle = value; RaisePropertyChanged(); } }

        bool hasPortName;
        public bool HasPortName { get { return hasPortName; } set { hasPortName = value; RaisePropertyChanged(); } }

        bool hasSearchBy;
        public bool HasSearchBy { get { return hasSearchBy; } set { hasSearchBy = value; RaisePropertyChanged(); } }

        bool hasDeclarationNumber;
        public bool HasDeclarationNumber { get { return hasDeclarationNumber; } set { hasDeclarationNumber = value; RaisePropertyChanged(); } }

        bool hasDeclarationDate;
        public bool HasDeclarationDate { get { return hasDeclarationDate; } set { hasDeclarationDate = value; RaisePropertyChanged(); } }

        bool hasBillNumber;
        public bool HasBillNumber { get { return hasBillNumber; } set { hasBillNumber = value; RaisePropertyChanged(); } }

        bool hasContainerNumber;
        public bool HasContainerNumber { get { return hasContainerNumber; } set { hasContainerNumber = value; RaisePropertyChanged(); } }

        bool hasDeclarationCards;
        public bool HasDeclarationCards { get { return hasDeclarationCards; } set { hasDeclarationCards = value; RaisePropertyChanged(); } }

        bool isDeclarationSelected = true;
        public bool IsDeclarationSelected { get { return isDeclarationSelected; } set { isDeclarationSelected = value; RaisePropertyChanged(); } }

        string shipmentTrackName;
        public string ShipmentTrackName { get { return shipmentTrackName; } set { shipmentTrackName = value; RaisePropertyChanged(); } }

        string shipmentCardImage;
        public string ShipmentCardImage { get { return shipmentCardImage; } set { shipmentCardImage = value; RaisePropertyChanged(); } }
    }
}

