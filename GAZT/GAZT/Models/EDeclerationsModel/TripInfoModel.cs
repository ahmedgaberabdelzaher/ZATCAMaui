using System;
using EGAZT.Helper;
using GalaSoft.MvvmLight;

namespace EGAZT.Models.EDeclerationsModel
{
	public class TripInfoModel: ViewModelBase
    {
        int _arrivingFromDepartingTo;
        public int arrivingFromDepartingTo { get { return _arrivingFromDepartingTo; } set { _arrivingFromDepartingTo = value;  } }

        string _arrivingFromDepartingToName;
        public string arrivingFromDepartingToName { get { return _arrivingFromDepartingToName; } set { _arrivingFromDepartingToName = value; RaisePropertyChanged(); } }

        int _port;
        public int port { get { return _port; } set { _port = value; } }

        string _portName;
        public string portName { get { return _portName; } set { _portName = value; RaisePropertyChanged(); } }

        int _tripeType;
        public int tripeType { get { return _tripeType; } set { _tripeType = value; RaisePropertyChanged(); } }

        string _flightNumber;
        public string flightNumber { get { return _flightNumber; } set { _flightNumber = value; RaisePropertyChanged(); } }

        int _travelPurpose;
        public int travelPurpose { get { return _travelPurpose; } set { _travelPurpose = value; } }

        string _travelPurposeName;
        public string travelPurposeName { get { return _travelPurposeName; } set { _travelPurposeName = value; RaisePropertyChanged(); } }

        public DateTime SelectedArrivalDepartureDate { get; set; } = DateTime.Now;

        public string travelDate { set { value = DateTimeHelper.DatetimeFormater(SelectedArrivalDepartureDate); } }

    }
}

