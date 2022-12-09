using System;
using EGAZT.Helper;
using GalaSoft.MvvmLight;

namespace EGAZT.Models.EDeclerationsModel
{
	public class PassengerModel : ViewModelBase
    {
        int _travelDocumentType;
        public int travelDocumentType { get { return _travelDocumentType; } set { _travelDocumentType = value; RaisePropertyChanged(); } }

        string _firstName;
        public string firstName { get { return _firstName; } set { _firstName = value; RaisePropertyChanged(); } }

        string _middleName;
        public string middleName { get { return _middleName; } set { _middleName = value; RaisePropertyChanged(); } }

        string _lastName;
        public string lastName { get { return _lastName; } set { _lastName = value; RaisePropertyChanged(); } }

        string _nationalityName;
        public string NationalityName { get { return _nationalityName; } set { _nationalityName = value; RaisePropertyChanged(); } }

        int _nationality;
        public int nationality { get { return _nationality; } set { _nationality = value; } }

        int _gender;
        public int gender { get { return _gender; } set { _gender = value; RaisePropertyChanged(); } }

        string _travelID;
        public string travelID { get { return _travelID; } set { _travelID = value; RaisePropertyChanged(); } }

        int _travelIssuerID;
        public int travelIssuerID { get { return _travelIssuerID; } set { _travelIssuerID = value; } }

        int _travelIssuerName;
        public int travelIssuerName { get { return _travelIssuerName; } set { _travelIssuerName = value; RaisePropertyChanged(); } }

        public DateTime SelectedPassIssuingDate { get; set; } = DateTime.Now;

        public string passIssuingDate { set { value = DateTimeHelper.DatetimeFormater(SelectedPassIssuingDate); } }

        public DateTime SelectedPassExpiryDate { get; set; } = DateTime.Now;

        public string passExpiryDate { set { value = DateTimeHelper.DatetimeFormater(SelectedPassExpiryDate); } }

        public DateTime SelectedBirthDate { get; set; } = DateTime.Now;

        public string birthDate { set { value = DateTimeHelper.DatetimeFormater(SelectedBirthDate); } }

        int _travelersCount;
        public int travelersCount { get { return _travelersCount; } set { _travelersCount = value; RaisePropertyChanged(); } }


    }
}

