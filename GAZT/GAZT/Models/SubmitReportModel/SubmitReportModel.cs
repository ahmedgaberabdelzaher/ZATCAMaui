using GalaSoft.MvvmLight;

namespace EGAZT.Models.SubmitReportModel
{
    public class SubmitReportModel : ViewModelBase
    {
        public string CityCode { get; set; }

        string _city;
        public string City { get { return _city; } set { _city = value; RaisePropertyChanged(); } }

        string _companyAddress;
        public string CompanyAddress { get { return _companyAddress; } set { _companyAddress = value; RaisePropertyChanged(); } }

        string _companyName;
        public string CompanyName { get { return _companyName; } set { _companyName = value; RaisePropertyChanged(); } }

        string _district;
        public string District { get { return _district; } set { _district = value; RaisePropertyChanged(); } }

        bool _isNeedReward;
        public bool IsNeedReward { get { return _isNeedReward; } set { _isNeedReward = value; RaisePropertyChanged(); } }

        bool _reporterWantToSharePersonalInfo = false;
        public bool ReporterWantToSharePersonalInfo { get { return _reporterWantToSharePersonalInfo; } set { _reporterWantToSharePersonalInfo = value; RaisePropertyChanged(); } }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        string _region;
        public string Region { get { return _region; } set { _region = value; RaisePropertyChanged(); } }

        public string RegionCode { get; set; }

        string _reportCategoryName;
        public string ReportCategoryName { get { return _reportCategoryName; } set { _reportCategoryName = value; RaisePropertyChanged(); } }

        public string ReportCategory { get; set; }

        string _reportTypeName;
        public string ReportTypeName { get { return _reportTypeName; } set { _reportTypeName = value; RaisePropertyChanged(); } }

        public string ReportTaxType { get; set; }

        string _vATNumber;
        public string VATNumber { get { return _vATNumber; } set { _vATNumber = value; RaisePropertyChanged(); } }

        string _reporterNameAr;
        public string ReporterNameAr { get { return _reporterNameAr; } set { _reporterNameAr = value; RaisePropertyChanged(); } }

        string _reporterEmail;
        public string ReporterEmail { get { return _reporterEmail; } set { _reporterEmail = value; RaisePropertyChanged(); } }

        string _reporterMobileNumber;
        public string ReporterMobileNumber { get { return _reporterMobileNumber; } set { _reporterMobileNumber = value; RaisePropertyChanged(); } }

        string _violationDate;
        public string ViolationDate { get { return _violationDate; } set { _violationDate = value; RaisePropertyChanged(); } }

        string _workType;
        public string WorkType { get { return _workType; } set { _workType = value; RaisePropertyChanged(); } }

        string _street;
        public string Street { get { return _street; } set { _street = value; RaisePropertyChanged(); } }

        string _reportDetails;
        public string ReportDetails { get { return _reportDetails; } set { _reportDetails = value; RaisePropertyChanged(); } }

        string _tIN;
        public string TIN { get { return _tIN; } set { _tIN = value; RaisePropertyChanged(); } }

        bool _hasViolationDateError = true;
        public bool HasViolationDateError { get { return _hasViolationDateError; } set { _hasViolationDateError = value; RaisePropertyChanged(); } }

    }
}



#region Not Used
/*Validations Properties */
//bool _hasCity;
//public bool HasCity { get { return _hasCity; } set { _hasCity = value; RaisePropertyChanged(); } }

//bool _hasCompanyAddress;
//public bool HasCompanyAddress { get { return _hasCompanyAddress; } set { _hasCompanyAddress = value; RaisePropertyChanged(); } }

//bool _hasCompanyName;
//public bool HasCompanyName { get { return _hasCompanyName; } set { _hasCompanyName = value; RaisePropertyChanged(); } }

//bool _hasDistrict;
//public bool HasDistrict { get { return _hasDistrict; } set { _hasDistrict = value; RaisePropertyChanged(); } }

//bool _hasRegion;
//public bool HasRegion { get { return _hasRegion; } set { _hasRegion = value; RaisePropertyChanged(); } }

//bool _hasReportCategory;
//public bool HasReportCategory { get { return _hasReportCategory; } set { _hasReportCategory = value; RaisePropertyChanged(); } }

//bool _hasReportType;
//public bool HasReportType { get { return _hasReportType; } set { _hasReportType = value; RaisePropertyChanged(); } }



//bool _hasReportDetails;
//public bool HasReportDetails { get { return _hasReportDetails; } set { _hasReportDetails = value; RaisePropertyChanged(); } }

//bool _hasReporterMobileNumber;
//public bool HasReporterMobileNumber { get { return _hasReporterMobileNumber; } set { _hasReporterMobileNumber = value; RaisePropertyChanged(); } }

//bool _hasReporterNameAr;
//public bool HasReporterNameAr { get { return _hasReporterNameAr; } set { _hasReporterNameAr = value; RaisePropertyChanged(); } }
//public string FullName { get; set; }
//public string Email { get; set; }
//public string PhoneNumber { get; set; }
//public string Location { get; set; }
//public string Region { get; set; }
//public string City { get; set; }
//public string CityCode { get; set; }
//public string District { get; set; }
//public string Street { get; set; }
//public string CommercialRegistration { get; set; }
//public string Editor { get; set; }
//string _reportType;
//public string ReportType { get { return _reportType; } set { _reportType = value; RaisePropertyChanged(); } }
//public string ReportCategory { get; set; }
//public string FacilityName { get; set; }
//public string TIN { get; set; } 
#endregion