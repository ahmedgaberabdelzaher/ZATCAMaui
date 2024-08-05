using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models.EDeclerationsModel.SubmitModels
{

    public class Currency
    {
        public string typeName { get; set; }
        public int typeID { get; set; }
        public int purpose { get; set; }
        public string otherpurpose { get; set; }
        public double value { get; set; }
        public int currency { get; set; }
        public string currencyName { get; set; }
        public bool permit { get; set; }
        public string attachment { get; set; }
        public double attachmentSize { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();
    }

    public class Product
    {
        public string typeName { get; set; }
        public string itemCode { get; set; }
        public int count { get; set; }
        public double value { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();
    }

    public class Restricted
    {
        public string typeName { get; set; }
        public int purpose { get; set; }
        public string otherpurpose { get; set; }
        public int count { get; set; }
        public int unit { get; set; }
        public double value { get; set; }
        public int currency { get; set; }
        public string currencyName { get; set; }
        public bool permit { get; set; }
        public string attachment { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();
    }

    public class EDeclerationSubmitModel : ObservableRecipient
    {
        TravelerDeclaration _travelerDeclaration = new TravelerDeclaration();
        public TravelerDeclaration travelerDeclaration { get { return _travelerDeclaration; } set { _travelerDeclaration = value; OnPropertyChanged(); } }
    }

    public class Tobacco
    {
        public string typeName { get; set; }
        public string subTypeName { get; set; }
        public long? itemCode { get; set; }
        public int taxSequence { get; set; }
        public int count { get; set; }
        public int measurementUnit { get; set; }
        public double value { get; set; }
        public double weight { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();
    }

    public class TravelerDeclaration : ObservableRecipient
    {

        #region Passenger Model
        bool _isvisitor = true;
        public bool Isvisitor { get { return _isvisitor; } set { _isvisitor = value; OnPropertyChanged(); } }

        bool _isDisclosure;
        public bool IsDisclosure { get { return _isDisclosure; } set { _isDisclosure = value; OnPropertyChanged(); } }

        int _travelDocumentType;
        public int travelDocumentType { get { return _travelDocumentType; } set { _travelDocumentType = value; OnPropertyChanged(); } }

        int _travelingType = 1;
        public int travelingType { get { return _travelingType; } set { _travelingType = value; OnPropertyChanged(); } }

        string _firstName;
        public string firstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged(); } }

        string _middleName;
        public string middleName { get { return _middleName; } set { _middleName = value; OnPropertyChanged(); } }

        string _lastName;
        public string lastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged(); } }

        string _fullName;
        public string FullName { get { return _fullName; } set { _fullName = value; OnPropertyChanged(); } }

        string _nationalityName;
        public string NationalityName { get { return _nationalityName; } set { _nationalityName = value; OnPropertyChanged(); } }

        int _nationality;
        public int nationality { get { return _nationality; } set { _nationality = value; } }

        int _gender;
        public int gender { get { return _gender; } set { _gender = value; OnPropertyChanged(); } }

        string _travelID;
        public string travelID { get { return _travelID; } set { _travelID = value; OnPropertyChanged(); } }

        int _travelIssuerID;
        public int travelIssuerID { get { return _travelIssuerID; } set { _travelIssuerID = value; } }


        bool _isPremiumResidency;
        public bool isPremiumResidency { get { return _isPremiumResidency; } set { _isPremiumResidency = value; OnPropertyChanged(); } }


        string _premiumResidencyExpiryDate;
        public string premiumResidencyExpiryDate { get { return _premiumResidencyExpiryDate; } set { _premiumResidencyExpiryDate = value; OnPropertyChanged(); } }


        string iqamaTypeDescription;
        public string IqamaTypeDescription { get { return iqamaTypeDescription; } set { iqamaTypeDescription = value; OnPropertyChanged(); } }

        /// <summary>
        // the same value as travelIssuerID
        /// </summary>
        int _passIssuingCountry;
        public int passIssuingCountry { get { return _passIssuingCountry; } set { _passIssuingCountry = value; } }

        string _travelIssuerName;
        public string travelIssuerName { get { return _travelIssuerName; } set { _travelIssuerName = value; OnPropertyChanged(); } }

        public DateTime passIssuingDate { get; set; } = DateTime.Now.Date.AddHours(-24);

        DateTime _passExpiryDate = DateTime.Now.Date;
        public DateTime passExpiryDate { get { return _passExpiryDate; }
            set {

                _passExpiryDate = value;
                OnPropertyChanged();
            } }


        public DateTime birthDate { get; set; } = DateTime.Now.Date.AddHours(-24);

        string _travelersCount;
        public string travelersCount { get { return _travelersCount; } set { _travelersCount = value; OnPropertyChanged(); } }
        #endregion

        #region Trip Model
        int _arrivingFromDepartingTo;
        public int arrivingFromDepartingTo { get { return _arrivingFromDepartingTo; } set { _arrivingFromDepartingTo = value; } }

        string _arrivingFromDepartingToName;
        public string arrivingFromDepartingToName { get { return _arrivingFromDepartingToName; } set { _arrivingFromDepartingToName = value; OnPropertyChanged(); } }

        int _port;
        public int port { get { return _port; } set { _port = value; } }

        string _portName;
        public string portName { get { return _portName; } set { _portName = value; OnPropertyChanged(); } }

        int _tripeType = 1;
        public int tripeType { get { return _tripeType; } set { _tripeType = value; OnPropertyChanged(); } }

        string _flightNumber;
        public string flightNumber { get { return _flightNumber; } set { _flightNumber = value; OnPropertyChanged(); } }

        string _travelPurpose;
        public string travelPurpose { get { return _travelPurpose; } set { _travelPurpose = value; } }

        string _travelPurposeName;
        public string travelPurposeName { get { return _travelPurposeName; } set { _travelPurposeName = value; OnPropertyChanged(); } }

        public DateTime travelDate { get; set; } = DateTime.Now;

        string _plateLetters;
        public string plateLetters { get { return _plateLetters; } set { _plateLetters = value; OnPropertyChanged(); } }

        string _plateNumber;
        public string plateNumber { get { return _plateNumber; } set { _plateNumber = value; OnPropertyChanged(); } }

        string _platesCountryName;
        public string PlatesCountryName { get { return _platesCountryName; } set { _platesCountryName = value; OnPropertyChanged(); } }

        int _plateCountryCode;
        public int plateCountryCode { get { return _plateCountryCode; } set { _plateCountryCode = value; } }

        string _PlatesCityName;
        public string PlatesCityName { get { return _PlatesCityName; } set { _PlatesCityName = value; OnPropertyChanged(); } }

        int _plateCityCode;
        public int plateCityCode { get { return _plateCityCode; } set { _plateCityCode = value; } }

        #endregion

        #region Contact Model
        string _CountryCode = "+966";
        public string CountryCode { get { return _CountryCode; } set { _CountryCode = value; OnPropertyChanged(); } }

        string _phoneNumber;
        public string phoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; OnPropertyChanged(); } }

        string _address;
        public string address { get { return _address; } set { _address = value; OnPropertyChanged(); } }

        string _email;
        public string email { get { return _email; } set { _email = value; OnPropertyChanged(); } }

        bool _IsTermsChecked;
        public bool IsTermsChecked { get { return _IsTermsChecked; } set { _IsTermsChecked = value; OnPropertyChanged(); } }

        bool _isUserExists;
        public bool IsUserExists { get { return _isUserExists; } set { _isUserExists = value; OnPropertyChanged(); } }
        #endregion


        public List<Tobacco> tobacco { get; set; } = new List<Tobacco>();
        public List<Product> product { get; set; } = new List<Product>();
        public List<Currency> currency { get; set; } = new List<Currency>();
        public List<Restricted> restricted { get; set; } = new List<Restricted>();
    }
}

