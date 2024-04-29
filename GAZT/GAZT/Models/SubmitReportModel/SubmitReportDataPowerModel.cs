using System;
using System.Collections.Generic;

namespace EGAZT.Models.SubmitReportModel
{
    public class SubmitReportDataPowerModelAttachement
    {
        public string fileContent { get; set; }
        public string fileName { get; set; }
        public string fileExtinction { get; set; }
    }

    public class SubmitReportDataPowerModel
    {
        public string LanguageCode { get; set; }
        public string cityCode { get; set; }
        public string city { get; set; }
        public string companyAddress { get; set; }
        public string companyName { get; set; }
        public string district { get; set; }
        public bool isNeedReward { get; set; }
        public bool reporterWantToSharePersonalInfo { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string regionCode { get; set; }
        public string regionName { get; set; }
        public string reportCategory { get; set; }
        public string reportCategoryName { get; set; }
        public string reportSubCategoryName { get; set; }
        public string reportTypeName { get; set; }
        public string reportDetails { get; set; }
        public string missedField { get; set; }
        public string reportSubCategory { get; set; }
        public string reportTaxType { get; set; }
        public string reporterID { get; set; }
        public string reporterEmail { get; set; }
        public string reporterMobileNumber { get; set; }
        public string reporterName_Arabic { get; set; }
        public string reporterName_English { get; set; }
        public int reporterResidentID { get; set; }
        public string reporterNationalID { get; set; }
        public long reporterGccID { get; set; }
        public string reporterPassportNumber { get; set; }
        public string TIN { get; set; }
        public string CR { get; set; }
        public string violationDate { get; set; }
        public string workType { get; set; }
        public string street { get; set; }
        public List<SubmitReportDataPowerModelAttachement> attachements { get; set; }
    }
}

