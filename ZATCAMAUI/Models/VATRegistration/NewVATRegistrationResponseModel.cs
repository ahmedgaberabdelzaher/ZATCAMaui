using Newtonsoft.Json;
namespace ZATCAMAUI.Models.VATRegistration
{
    public class NewVATRegistrationResponseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public Header header { get; set; }
        //public Data data { get; set; }

        [JsonProperty("Data")]
        public Data data { get; set; }
        public class Address
        {
            public string addressNumber { get; set; }
            public string city { get; set; }
            public string postalCode { get; set; }
            public string street { get; set; }
            public string additionalNumber { get; set; }
            public string buildingNumber { get; set; }
            public string region { get; set; }
            public string regionDescription { get; set; }
            public string quarter { get; set; }
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
            public string fatherName { get; set; }
            public string relationshipType { get; set; }
            public string lastName { get; set; }
            public string firstName { get; set; }
            public bool isDefault { get; set; }
            public string contactType { get; set; }
            public string initials { get; set; }
            public string title { get; set; }
            public string idNumber { get; set; }
            public string idType { get; set; }
            public string familyName { get; set; }
            public string grandFatherName { get; set; }
        }

        public class ContactDetail
        {
            public string mobileNumber { get; set; }
            public string transactionType { get; set; }
            public string formGUID { get; set; }
            public string dataVersion { get; set; }
            public int lineNumber { get; set; }
            public string rankingOrder { get; set; }
            public string sourceIdentifier { get; set; }
            public string consumerNumber { get; set; }
            public string telephoneNumber { get; set; }
            public string R3User { get; set; }
            public string email { get; set; }
        }

        public class Data
        {
            [JsonProperty("step4CheckBox2")]
            public string Stp4Cbbox2 { get; set; }

            [JsonProperty("step4CheckBox1")]
            public string Stp4Cbbox1 { get; set; }

            [JsonProperty("step3CheckBox")]
            public string Stp3Cbbox { get; set; }

            [JsonProperty("step2CheckBox")]
            public string Stp2Cbbox { get; set; }
            public List<object> notes { get; set; }
            public List<object> eligibleDocuments { get; set; }
            public List<Address> addresses { get; set; }

            [JsonProperty("TINName")]
            public string TinNm { get; set; }

            [JsonProperty("toSubmit")]
            public string ToSflg { get; set; }

            [JsonProperty("stepNumber")]
            public string StepNumberz { get; set; }

            [JsonProperty("userType")]
            public string UserTypz { get; set; }

            [JsonProperty("source")]
            public string Source { get; set; }

            [JsonProperty("returnId")]
            public string ReturnIdz { get; set; }

            [JsonProperty("residencyType")]
            public string ResidencyTy { get; set; }

            [JsonProperty("reason")]
            public string Reason { get; set; }

            [JsonProperty("reasonDescription")]
            public string ReaFg { get; set; }

            [JsonProperty("CRName")]
            public string CrNm { get; set; }

            [JsonProperty("CRName")]
            public string CrNo { get; set; }

            [JsonProperty("CRStartDate")]
            public DateTime CrStdt { get; set; }

            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }

            [JsonProperty("declarationContactNumber")]
            public string Decconno { get; set; }

            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }

            [JsonProperty("formProcess")]
            public string Formprocz { get; set; }

            [JsonProperty("futureDate")]
            public DateTime FutureDt { get; set; }

            [JsonProperty("goLiveDate")]
            public DateTime GoLiveDt { get; set; }

            [JsonProperty("IBAN")]
            public string Iban { get; set; }

            [JsonProperty("importAttachment")]
            public string ImAttch { get; set; }

            [JsonProperty("VATImport")]
            public string ImFg { get; set; }

            [JsonProperty("systemCode")]
            public string Mandt { get; set; }

            public string nonResidentBankGuaranty { get; set; } // Not Found
            public string nonResidentDecription { get; set; }  // Not Found

            [JsonProperty("serialNumber")]
            public string Srno { get; set; }

            public string instructionAgree { get; set; } // Not Found

            public string bankKey { get; set; } // Not Found

            [JsonProperty("pendingIBANMessage")]
            public string PendingIbanMsg { get; set; }

            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }

            [JsonProperty("formBundleNumber")]
            public string Fbnumz { get; set; }

            [JsonProperty("language")]
            public string Langz { get; set; }

            [JsonProperty("TIN")]
            public string Gpart { get; set; }

            [JsonProperty("transactionType")]
            public string TxnTp { get; set; }

            [JsonProperty("portalUser")]
            public string ByPusr { get; set; }

            [JsonProperty("operation")]
            public string Operationz { get; set; }

            [JsonProperty("optionalIBAN")]
            public string OptIban { get; set; }

            public string nonResidentSaleAmount { get; set; } // Not Found
            public string nonResidentSectionType { get; set; } // Not Found
            public string currency { get; set; } // Not Found

            [JsonProperty("VATExporter")]
            public string ExFg { get; set; }

            [JsonProperty("exporterAttachment")]
            public string ExAttch { get; set; }

            [JsonProperty("declaration")]
            public string Decfg { get; set; }

            [JsonProperty("declarationDesignation")]
            public string Decdesignation { get; set; }

            [JsonProperty("declarationName")]
            public string Decname { get; set; }

            [JsonProperty("nonResidentIndivid")]
            public string NresFg { get; set; }
            public List<object> attachments { get; set; }
            public List<object> IBANList { get; set; }
            public List<object> questionsList { get; set; }
            public List<Contact> contacts { get; set; }
            public List<ContactDetail> contactDetails { get; set; }
            public List<object> questions { get; set; }

            public string CR1645Golive { get; set; } // Not Found

            [JsonProperty("declarationIdNumber")]
            public string DecidNo { get; set; }

            [JsonProperty("declarationIdType")]
            public string DecidTy { get; set; }

            public string nonResidentBankGuarantyId { get; set; } // Not Found
            public string statusCode { get; set; }

            [JsonProperty("userName")]
            public string Officerz { get; set; }
        }

        public class Header
        {
            public string requestID { get; set; }
            public Status status { get; set; }
        }

        /*public class Root
        {
            public Header header { get; set; }
            public Data data { get; set; }
        }*/

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }


    }
}
