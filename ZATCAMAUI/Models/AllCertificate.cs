namespace ZATCAMAUI.Models
{


    public class Result
    {
        public string creationTime { get; set; }
        public string issueTime { get; set; }
        public string TIN { get; set; }
        public string language { get; set; }
        public string obligationStatus { get; set; }
        public string formKey { get; set; }
        public string description { get; set; }
        public string correspondenceNumber { get; set; }
        public string correspondencekey { get; set; }
        public string correspondenceType { get; set; }
        public string correspondenceDescription { get; set; }
        public string contractAccount { get; set; }
        public string contractRefrenece { get; set; }
        public string formBundleNumber { get; set; }
        public string caseId { get; set; }
        public string letterNumber { get; set; }
        public string attachedByPerson { get; set; }
        public string formBundleType { get; set; }
        public string auditor { get; set; }
        public string taxpayerType { get; set; }
        public string userTIN { get; set; }
        public string pdfURL { get; set; }
        public string creationDate { get; set; }
        public string printDate { get; set; }
        public string issueDate { get; set; }
    }
    
    public class ZakatSet
    {
        public List<Result> results { get; set; }
    }
    
    public class VATSet
    {
        public List<Result> results { get; set; }
    }
    
    public class ExciseSet
    {
        public List<Result> results { get; set; }
    }
    
    public class AllCertificate
    {
        public string TIN { get; set; }
        public string language { get; set; }
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public List<Result> ZakatSet { get; set; }
        public List<Result> VATSet { get; set; }
        public List<Result> exciseSet { get; set; }
    }
}


