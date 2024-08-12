namespace ZATCAMAUI.Models.VATRegistration
{
    public class FormSummaryDetailsEnquiryResponseModel
    {
        public Header header { get; set; }
        public Data data { get; set; }
        public class Data
        {
            public string display { get; set; }
            public string authenticationUser1 { get; set; }
            public string formBundleGUID { get; set; }
            public string formBundleNumber { get; set; }
            public string formBundleType { get; set; }
            public string TIN { get; set; }
            public string language { get; set; }
            public string periodkey { get; set; }
            public string statusDescription { get; set; }
            public DateTime periodEndDate { get; set; }
            public DateTime periodStartDate { get; set; }
            public DateTime changeDate { get; set; }
            public string auditor { get; set; }
            public string auditorNumber { get; set; }
            public DateTime startDate { get; set; }
            public string branch { get; set; }
            public string calendarType { get; set; }
            public string combination { get; set; }
            public string dueStatus { get; set; }
            public DateTime dueDate { get; set; }
            public string dueDateCharacter { get; set; }
            public DateTime endDate { get; set; }
            public string authenticationUser { get; set; }
            public string authenticationUser2 { get; set; }
            public string authenticationUser3 { get; set; }
            public string authenticationUser4 { get; set; }
            public string authenticationUser5 { get; set; }
            public string formBundleTypeDescription { get; set; }
            public string selected { get; set; }
            public string inboundCorrespondenceDescription { get; set; }
            public string inboundCorrespondenceType { get; set; }
            public string informationMessage { get; set; }
            public string month { get; set; }
            public string errorMessage { get; set; }
            public bool isObjectionFiled { get; set; }
            public string obligation { get; set; }
            public bool isOpen { get; set; }
            public string period { get; set; }
            public bool refundFiled { get; set; }
            public string sadadBillNumber1 { get; set; }
            public string sadadBillNumber2 { get; set; }
            public string statusCode { get; set; }
            public string status { get; set; }
            public string taxPeriod { get; set; }
            public string contractNumber { get; set; }
        }

        public class Header
        {
            public string requestID { get; set; }
            public Status status { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }
    }
}
