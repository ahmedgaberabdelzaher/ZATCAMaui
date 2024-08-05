

using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models.SubmitReportModel
{
    public class SubmitReportModel : ObservableRecipient
    {

        public string CityCode { get; set; }

        string _city;
        public string City { get { return _city; } set { _city = value; OnPropertyChanged(); } }

        string _companyAddress;
        public string CompanyAddress { get { return _companyAddress; } set { _companyAddress = value; OnPropertyChanged(); } }

        string _companyName;
        public string CompanyName { get { return _companyName; } set { _companyName = value; OnPropertyChanged(); } }

        string _district;
        public string District { get { return _district; } set { _district = value; OnPropertyChanged(); } }

        bool _isNeedReward = false;
        public bool IsNeedReward { get { return _isNeedReward; } set { _isNeedReward = value; OnPropertyChanged(); } }

        bool _reporterWantToSharePersonalInfo = false;
        public bool ReporterWantToSharePersonalInfo { get { return _reporterWantToSharePersonalInfo; } set { _reporterWantToSharePersonalInfo = value; OnPropertyChanged(); } }


        string _reporterNationalId;
        public string ReporterNationalId { get { return _reporterNationalId; } set { _reporterNationalId = value; OnPropertyChanged(); } }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        string _region;
        public string Region { get { return _region; } set { _region = value; OnPropertyChanged(); } }

        public string RegionCode { get; set; }

        string _reportCategoryName;
        public string ReportCategoryName { get { return _reportCategoryName; } set { _reportCategoryName = value; OnPropertyChanged(); } }

        public string ReportCategory { get; set; }
        public string ReportSubCategory { get; set; }

        string _reportTypeName;
        public string ReportTypeName { get { return _reportTypeName; } set { _reportTypeName = value; OnPropertyChanged(); } }

        public string ReportTaxType { get; set; }

        string _vATNumber;
        public string VATNumber { get { return _vATNumber; } set { _vATNumber = value; OnPropertyChanged(); } }

        string _reporterNameAr;
        public string ReporterNameAr { get { return _reporterNameAr; } set { _reporterNameAr = value; OnPropertyChanged(); } }

        string _reporterNameEn;
        public string ReporterNameEn { get { return _reporterNameEn; } set { _reporterNameEn = value; OnPropertyChanged(); } }

        string _reporterEmail;
        public string ReporterEmail { get { return _reporterEmail; } set { _reporterEmail = value; OnPropertyChanged(); } }

        string _reporterMobileNumber;
        public string ReporterMobileNumber { get { return _reporterMobileNumber; } set { _reporterMobileNumber = value; OnPropertyChanged(); } }


        string _violationDate;
        public string ViolationDate { get { return _violationDate; } set { _violationDate = value; OnPropertyChanged(); } }

        string _workType;
        public string WorkType { get { return _workType; } set { _workType = value; OnPropertyChanged(); } }

        string _street;
        public string Street { get { return _street; } set { _street = value; OnPropertyChanged(); } }

        string _location;
        public string Location { get { return _location; } set { _location = value; OnPropertyChanged(); } }

        string _reportDetails;
        public string ReportDetails { get { return _reportDetails; } set { _reportDetails = value; OnPropertyChanged(); } }

        string _tIN;
        public string TIN { get { return _tIN; } set { _tIN = value; OnPropertyChanged(); } }

        string _CR;
        public string CR { get { return _CR; } set { _CR = value; OnPropertyChanged(); } }

        string _missingFieldName;
        public string MissedFieldName { get { return _missingFieldName; } set { _missingFieldName = value; OnPropertyChanged(); } }

        string _missedField;
        public string MissedField { get { return _missedField; } set { _missedField = value; OnPropertyChanged(); } }

        List<ReportFileModel> _files = new List<ReportFileModel>();
        public List<ReportFileModel> files { get { return _files; } set { _files = value; } }

        public string fileBase64 { get; set; }
        public string[] fAraay { get; set; }

        string _reportSubCategoryName;
        public string ReportSubCategoryName { get { return _reportSubCategoryName; } set { _reportSubCategoryName = value; OnPropertyChanged(); } }


    }
}