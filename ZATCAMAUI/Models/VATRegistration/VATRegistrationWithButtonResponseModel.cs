namespace ZATCAMAUI.Models.VATRegistration
{
    public class VATRegistrationWithButtonResponseModel
    {
        public Header header { get; set; }
        public Data data { get; set; }
        public class Bank
        {
            public string systemCode { get; set; }
            public string bankCountry { get; set; }
            public string bankKey { get; set; }
            public string bankName { get; set; }
        }

        public class Data
        {
            public string formBundleNumber { get; set; }
            public string language { get; set; }
            public string userName { get; set; }
            public string TIN { get; set; }
            public string statusCode { get; set; }
            public string transactionType { get; set; }
            public string formProcess { get; set; }
            public string formBundleType { get; set; }
            public string userStatus { get; set; }
            public string systemCode { get; set; }
            public string edit { get; set; }
            public string userType { get; set; }
            public string portalUser { get; set; }
            public string operation { get; set; }
            public string stepNumber { get; set; }
            public string returnId { get; set; }
            public List<UIButton> UIButtons { get; set; }
            public List<Bank> banks { get; set; }
            public List<EligibleDocument> eligibleDocuments { get; set; }
        }

        public class EligibleDocument
        {
            public string systemCode { get; set; }
            public string language { get; set; }
            public string formBundleType { get; set; }
            public string transactionType { get; set; }
            public string documentCategory { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public string documentName { get; set; }
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

        public class UIButton
        {
            public string formBundleType { get; set; }
            public string userStatus { get; set; }
            public string button { get; set; }
        }
    }
}
