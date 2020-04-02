using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class TaxEvasionReportFormPageModel
    {

    }

    public class RegionList
    {
        public string RegionCode { get; set; }
        public string RegionGuid { get; set; }
        public string RegionNameEN { get; set; }
        public string RegionNameAR { get; set; }
    }

    public class TERFRegionRootObject
    {
        public List<RegionList> RegionList { get; set; }
        public List<object> RegionResultErrorList { get; set; }
        public bool RegionSuccess { get; set; }
    }


    public class CityList
    {
        public string CityCode { get; set; }
        public string CityGuid { get; set; }
        public string CityNameAR { get; set; }
        public string CityNameEN { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string RegionCode { get; set; }
        public string RegionGuid { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }
    }

    public class TERFCityRetrieveRootObject
    {
        public List<CityList> CityList { get; set; }
        public List<object> CityResultErrorList { get; set; }
        public bool CitySuccess { get; set; }
    }

    public class FAQList
    {
        public string AnswerAR { get; set; }
        public string AnswerEN { get; set; }
        public string FAQNumber { get; set; }
        public string QuestionAR { get; set; }
        public string QuestionEN { get; set; }
    }

    public class TERFFAQRootObject
    {
        public List<FAQList> FAQList { get; set; }
        public List<object> FAQResultErrorList { get; set; }
        public bool FAQSuccess { get; set; }
    }

    public class TERFFAQObject
    {
        public List<FAQList> FAQList { get; set; }
        public List<object> FAQResultErrorList { get; set; }
        public bool Success { get; set; }
    }

    public class MTaxEvasionReportList
    {
        public string CR { get; set; }
        public string CityCode { get; set; }
        public string CityGuid { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyName { get; set; }
        public string District { get; set; }
        public string ExciseNumber { get; set; }
        public string ID { get; set; }
        public bool IsNeedReward { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ReceivedDate { get; set; }
        public string RegionCode { get; set; }
        public string RegionGuid { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }
        public string ReportCategoryCode { get; set; }
        public string ReportCategoryGuid { get; set; }
        public string ReportCategoryNameAr { get; set; }
        public string ReportCategoryNameEn { get; set; }
        public string ReportDetails { get; set; }
        public string ReportNumber { get; set; }
        public int ReportStatus { get; set; }
        public string ReportStatusMessage { get; set; }
        public string ReportSubCategoryCode { get; set; }
        public string ReportSubCategoryGuid { get; set; }
        public string ReportSubCategoryNameAr { get; set; }
        public string ReportSubCategoryNameEn { get; set; }
        public string ReportTaxTypeCode { get; set; }
        public string ReportTaxTypeGuid { get; set; }
        public string ReportTaxTypeNameAr { get; set; }
        public string ReportTaxTypeNameEn { get; set; }
        public string ReportTypeCode { get; set; }
        public string ReportTypeGuid { get; set; }
        public string ReportTypeNameAr { get; set; }
        public string ReportTypeNameEn { get; set; }
        public string ReporterEmail { get; set; }
        public string ReporterId { get; set; }
        public string ReporterMobileNumber { get; set; }
        public string ReporterName { get; set; }
        public string TIN { get; set; }
        public int TaxType { get; set; }
        public string VAT { get; set; }
        public string VATNumber { get; set; }
        public string ViolationDate { get; set; }
        public int ViolationType { get; set; }
        public string WorkType { get; set; }
    }

    public class TaxEvasionMRootObject
    {
        public List<object> MTaxEvasionResultErrorList { get; set; }
        public bool MTaxEvasionSuccess { get; set; }
        public List<MTaxEvasionReportList> MTaxEvasionReportList { get; set; }
    }


   







    public class RegionPost
    { 
         public string WSUserName { get; set; }/*: "GAZT@CRM",*/
    public string WSPassword { get; set; } /*"gazt@123"*/
    
    }
    public class CityPost
    {
        public string WSUserName { get; set; }/*: "GAZT@CRM",*/
        public string WSPassword { get; set; } /*"gazt@123"*/
        
        public string RegionCode { get; set; }

    }

    public class FAQPost
    {
        public string WSUserName { get; set; }/*: "GAZT@CRM",*/
        public string WSPassword { get; set; } /*"gazt@123"*/

        public string Channel { get; set; }

    }
    public class ReportRetriveByMobileNumberPost
    {
        public string Channel { get; set; }
        public string MobileNumber { get; set; }
        public string WSUserName { get; set; }
        public string WSPassword { get; set; }
    }
    public class TaxEvasionReportList
    {
        public string CR { get; set; }
        public string CityCode { get; set; }
        public string CityGuid { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyName { get; set; }
        public string District { get; set; }
        public string ID { get; set; }
        public bool IsNeedReward { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ReceivedDate { get; set; }
        public string RegionCode { get; set; }
        public string RegionGuid { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }
        public string ReportCategoryCode { get; set; }
        public string ReportCategoryGuid { get; set; }
        public string ReportCategoryNameAr { get; set; }
        public string ReportCategoryNameEn { get; set; }
        public string ReportDetails { get; set; }
        public string ReportNumber { get; set; }
        public string ReportStatus { get; set; }
        public string ReportStatusMessage { get; set; }
        public string ReportSubCategoryCode { get; set; }
        public string ReportSubCategoryGuid { get; set; }
        public string ReportSubCategoryNameAr { get; set; }
        public string ReportSubCategoryNameEn { get; set; }
        public string ReportTaxTypeCode { get; set; }
        public string ReportTaxTypeGuid { get; set; }
        public string ReportTaxTypeNameAr { get; set; }
        public string ReportTaxTypeNameEn { get; set; }
        public string ReportTypeCode { get; set; }
        public string ReportTypeGuid { get; set; }
        public string ReportTypeNameAr { get; set; }
        public string ReportTypeNameEn { get; set; }
        public string ReporterEmail { get; set; }
        public string ReporterId { get; set; }
        public string ReporterMobileNumber { get; set; }
        public string ReporterName { get; set; }
        public string TIN { get; set; }
        public string TaxType { get; set; }
        public string VAT { get; set; }
        public string VATNumber { get; set; }
        public string ViolationDate { get; set; }
        public string ViolationType { get; set; }
        public string WorkType { get; set; }
    }

    public class ReportRetriveByMobNoRootObject
    {
        public List<object> ResultErrorList { get; set; }
        public bool Success { get; set; }
        public List<TaxEvasionReportList> TaxEvasionReportList { get; set; }
    }




    public class UploadedDocumentsList
    {
        public string DocBinaryInBase64 { get; set; }
        public string FileNameWithExtension { get; set; }
        public string MimeType { get; set; }
    }

    public class TEReport
    {
        public string Channel { get; set; }
        public string CityCode { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyMobileNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyOwnerName { get; set; }
        public string CompanyType { get; set; }
        public string District { get; set; }
        public string HavingTIN { get; set; }
        public string ID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ReceivedDate { get; set; }
        public string RegionCode { get; set; }
        public string ReportDetails { get; set; }
        public string ReporterEmail { get; set; }
        public string ReporterMobileNumber { get; set; }
        public string ReporterName { get; set; }
        public string TIN { get; set; }
        public string TaxType { get; set; }
        public List<UploadedDocumentsList> UploadedDocumentsList { get; set; }
        public string VAT { get; set; }
        public string ViolationType { get; set; }
        public string WSUserName { get; set; }
        public string WSPassword { get; set; }
        public string WorkType { get; set; }
    }

    public class FacilityCompanyType
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }


    //TEReportResponse
    public class ResultErrorList
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorField { get; set; }
    }

    public class TEReportResponsePostRootObject
    {
        public List<object> AttachmentsIds { get; set; }
        public List<ResultErrorList> ResultErrorList { get; set; }
        public bool Success { get; set; }
        public string TaxEvasionGuid { get; set; }
        public string TaxEvasionNumber { get; set; }
    }





}
