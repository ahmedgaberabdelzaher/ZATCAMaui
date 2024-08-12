using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI
{
    public class AbsherTaxPayerResponse
    {
        public Header header { get; set; }
        public Result result { get; set; }
    }

    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
    }
    public class Result
    {
        public string formBundleGUID { get; set; }
        public string OTPCode { get; set; }
        public string country { get; set; }
        public string taxpayerBirthDate { get; set; }
        public string passExpiryDate { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
        public DateTime birthDate { get; set; }
        public string taxpayerTitle { get; set; }
        public string partnerKind { get; set; }
        public string taxpayerFullName { get; set; }
        public string idIssueingCountry { get; set; }
        public string source { get; set; }
        public string title { get; set; }
        public string fullName { get; set; }
        public string floor { get; set; }
        public string TIN { get; set; }
        public string additionalNumber { get; set; }
        public string houseNumber { get; set; }
        public string birthDateCalendarType { get; set; }
        public string buildingNumber { get; set; }
        public string birthDate10 { get; set; }
        public string fatherName { get; set; }
        public string poBox { get; set; }
        public string grandfatherName { get; set; }
        public string familyName { get; set; }
        public string addressNumber { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string initials { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string quarter { get; set; }
        public string postalCode { get; set; }
        public string telephone { get; set; }
        public string faxNumber { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string deafultCommunication { get; set; }
        public string website { get; set; }
        public string authorizationGroup { get; set; }
        public string branchDescription { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string partnerKindDescription { get; set; }
        public string regionDescription { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}
