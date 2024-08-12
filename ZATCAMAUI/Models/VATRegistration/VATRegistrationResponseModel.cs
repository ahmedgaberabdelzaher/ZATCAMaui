namespace ZATCAMAUI.Models.VATRegistration
{
    public class VATRegistrationResponseModel
    {
        public Header header { get; set; }
        public Data data { get; set; }
        public class Address
        {
            public string addressNumber { get; set; }
            public string city { get; set; }
            public string quarter { get; set; }
            public string postalCode { get; set; }
            public string street { get; set; }
            public string additionalNumber { get; set; }
            public string buildingNumber { get; set; }
            public string region { get; set; }
            public string regionDescription { get; set; }
        }

        public class Attachment
        {
            public string returnGUID { get; set; }
            public string sequenceNumber { get; set; }
            public string formGUID { get; set; }
            public string documentCategory { get; set; }
            public string serialNumber { get; set; }
            public string documentId { get; set; }
            public string attachedByPerson { get; set; }
            public string fileName { get; set; }
            public string fileExtension { get; set; }
            public string MIMEType { get; set; }
            public string portalUser { get; set; }
            public DateTime entryDate { get; set; }
            public string createdAt { get; set; }
            public string dataVersion { get; set; }
            public string documentURL { get; set; }
            public string outletReference { get; set; }
            public string enableEdit { get; set; }
            public string enableDelete { get; set; }
            public string visibleEdit { get; set; }
            public string visibleDelete { get; set; }
        }

        public class Contact
        {
            public string formGUID { get; set; }
            public string transactionType { get; set; }
            public string dataVersion { get; set; }
            public int lineNumber { get; set; }
            public string rankingOrder { get; set; }
            public string sourceIdentifier { get; set; }
            public string TIN { get; set; }
            public DateTime endDate { get; set; }
            public bool isDefault { get; set; }
            public string contactType { get; set; }
            public DateTime startDate { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string relationshipType { get; set; }
            public string fatherName { get; set; }
            public string grandFatherName { get; set; }
            public string familyName { get; set; }
            public DateTime birthDate { get; set; }
            public string startDateCalendarType { get; set; }
            public string idType { get; set; }
            public string idNumber { get; set; }
            public string title { get; set; }
            public string initials { get; set; }
        }

        public class ContactDetail
        {
            public string formGUID { get; set; }
            public string transactionType { get; set; }
            public string dataVersion { get; set; }
            public int lineNumber { get; set; }
            public string rankingOrder { get; set; }
            public string sourceIdentifier { get; set; }
            public string consumerNumber { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public string telephoneNumber { get; set; }
            public string R3User { get; set; }
            public string email { get; set; }
            public string mobileNumber { get; set; }
        }

        public class Data
        {
            public string serialNumber { get; set; }
            public string formBundleGUID { get; set; }
            public string formBundleNumber { get; set; }
            public string TIN { get; set; }
            public string language { get; set; }
            public string userName { get; set; }
            public string portalUser { get; set; }
            public string transactionType { get; set; }
            public string instructionAgree { get; set; }
            public DateTime billEffectiveDate { get; set; }
            public string CR1645Golive { get; set; }
            public string bankKey { get; set; }
            public DateTime confirmTaxableDate { get; set; }
            public string pendingIBANMessage { get; set; }
            public string CRName { get; set; }
            public string CRNumber { get; set; }
            public DateTime CRStartDate { get; set; }
            public string dataVersion { get; set; }
            public string declarationContactNumber { get; set; }
            public DateTime declarationDate { get; set; }
            public string declarationDesignation { get; set; }
            public string declaration { get; set; }
            public string declarationIdNumber { get; set; }
            public string declarationIdType { get; set; }
            public string declarationName { get; set; }
            public string exporterAttachment { get; set; }
            public string VATExporter { get; set; }
            public string formGUID { get; set; }
            public string formProcess { get; set; }
            public DateTime futureDate { get; set; }
            public string globalCalendarType { get; set; }
            public DateTime goLiveDate { get; set; }
            public string IBAN { get; set; }
            public string importAttachment { get; set; }
            public string VATImport { get; set; }
            public string systemCode { get; set; }
            public string newRegistrationType { get; set; }
            public DateTime newRegistrationTypeFromDate { get; set; }
            public double nonResidentBankGuaranty { get; set; }
            public DateTime bankGuarantyValidFrom { get; set; }
            public string nonResidentBankGuarantyId { get; set; }
            public DateTime bankGuarantyValidTo { get; set; }
            public string nonResidentDecription { get; set; }
            public string nonResidentIndivid { get; set; }
            public string nonResidentFinanicalReport { get; set; }
            public double nonResidentSaleAmount { get; set; }
            public string nonResidentSectionType { get; set; }
            public string currency { get; set; }
            public string operation { get; set; }
            public string optionalIBAN { get; set; }
            public string reasonDescription { get; set; }
            public string reason { get; set; }
            public string registrationType { get; set; }
            public string residencyType { get; set; }
            public string returnId { get; set; }
            public string smartRegistration { get; set; }
            public string source { get; set; }
            public string statusCode { get; set; }
            public string stepNumber { get; set; }
            public string step2CheckBox { get; set; }
            public string step3CheckBox { get; set; }
            public string step4CheckBox1 { get; set; }
            public string step4CheckBox2 { get; set; }
            public string TINName { get; set; }
            public string toSubmit { get; set; }
            public string userType { get; set; }
            public DateTime VATDate { get; set; }
            public DateTime VATTaxableDate { get; set; }
            public List<Address> addresses { get; set; }
            public List<Attachment> attachments { get; set; }
            public List<Contact> contacts { get; set; }
            public List<ContactDetail> contactDetails { get; set; }
            public List<EligibleDocument> eligibleDocuments { get; set; }
            public List<IBANList> IBANList { get; set; }
            public List<Note> notes { get; set; }
            public QuestionConfigurations questionConfigurations { get; set; }
            public List<QuestionsList> questionsList { get; set; }
            public List<Question> questions { get; set; }
        }

        public class EligibleDocument
        {
            public string systemCode { get; set; }
            public string formGUID { get; set; }
            public string dataVersion { get; set; }
            public int lineNumber { get; set; }
            public string rankingOrder { get; set; }
            public string formBundleType { get; set; }
            public string transactionType { get; set; }
            public string documentCategory { get; set; }
            public string documentName { get; set; }
        }

        public class Header
        {
            public string requestID { get; set; }
            public Status status { get; set; }
        }

        public class IBANList
        {
            public string TIN { get; set; }
            public string bankDetails { get; set; }
            public string IBAN { get; set; }
        }

        public class Note
        {
            public string noteNumber { get; set; }
            public string referenceName { get; set; }
            public string displayOnAssessment { get; set; }
            public string completed { get; set; }
            public string processingReason { get; set; }
            public string userName { get; set; }
            public DateTime entryDate { get; set; }
            public string createdAt { get; set; }
            public string attachedByPerson { get; set; }
            public string portalUser { get; set; }
            public string TIN { get; set; }
            public string dataVersion { get; set; }
            public string name { get; set; }
            public int lineNumber { get; set; }
            public int elementNumber { get; set; }
            public string notesFormat { get; set; }
            public string textLine { get; set; }
            public string section { get; set; }
            public string startDate { get; set; }
            public string startTime { get; set; }
            public string notesDescription { get; set; }
        }

        public class Question
        {
            public string systemCode { get; set; }
            public string formGUID { get; set; }
            public string dataVersion { get; set; }
            public int lineNumber { get; set; }
            public string rankingOrder { get; set; }
            public string residencyType { get; set; }
            public string questionNumber { get; set; }
            public string questionOptionsNumber { get; set; }
            public string questionOptionsDescription { get; set; }
            public string questionOptionsAnswer { get; set; }
        }

        public class QuestionConfigurations
        {
            public string formGUID { get; set; }
            public string TIN { get; set; }
            public string dataVersion { get; set; }
            public string residencyType { get; set; }
            public int lineNumber { get; set; }
            public string questionOptionsNumber { get; set; }
            public string questionOptionsDescription { get; set; }
            public string rankingOrder { get; set; }
            public string questionOptionsAnswer { get; set; }
            public string questionNumber { get; set; }
            public string formBundleNumber { get; set; }
            public int minValue { get; set; }
            public int maxValue { get; set; }
        }

        public class QuestionsList
        {
            public string questionNumber { get; set; }
            public string required { get; set; }
            public string formBundleType { get; set; }
            public string transactionType { get; set; }
            public string processingReason { get; set; }
            public string questionDescription { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }
    }
}
