using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        bool _isNeedReward = false;
        public bool IsNeedReward { get { return _isNeedReward; } set { _isNeedReward = value; RaisePropertyChanged(); } }

        bool _reporterWantToSharePersonalInfo = false;
        public bool ReporterWantToSharePersonalInfo { get { return _reporterWantToSharePersonalInfo; } set { _reporterWantToSharePersonalInfo = value; RaisePropertyChanged(); } }


        string _reporterNationalId ;
        public string ReporterNationalId { get { return _reporterNationalId; } set { _reporterNationalId = value; RaisePropertyChanged(); } }

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

        string _reporterNameEn;
        public string ReporterNameEn { get { return _reporterNameEn; } set { _reporterNameEn = value; RaisePropertyChanged(); } }

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

        string _location;
        public string Location { get { return _location; } set { _location = value; RaisePropertyChanged(); } }

        string _reportDetails;
        public string ReportDetails { get { return _reportDetails; } set { _reportDetails = value; RaisePropertyChanged(); } }

        string _tIN;
        public string TIN { get { return _tIN; } set { _tIN = value; RaisePropertyChanged(); } }

        string _CR;
        public string CR { get { return _CR; } set { _CR = value; RaisePropertyChanged(); } }

        string _missingFieldName;
        public string MissedFieldName { get { return _missingFieldName; } set { _missingFieldName = value; RaisePropertyChanged(); } }

        string _missedField;
        public string MissedField { get { return _missedField; } set { _missedField = value; RaisePropertyChanged(); } }

        List<ReportFileModel> _files = new List<ReportFileModel>();
        public List<ReportFileModel> files { get { return _files; } set { _files = value;} }

        public string fileBase64 { get; set; }
        public string[] fAraay { get; set; }

    }
}