using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    // [Serializable]

    // [DataContract]
    public class __metadata
    {
        //[DataMember]
        public string id { get; set; }
        //[DataMember]
        public string uri { get; set; }
        // [DataMember]
        public string type { get; set; }
    }

    // [Serializable]

    //[DataContract]
    public class ResultsItem
    {
        /*//[DataMember]
        public __metadata __metadata { get; set; }*/
        [DataMember]
        [JsonProperty ("addressNumber")]
        public string Addrnumber { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string City { get; set; }
        [DataMember]
        [JsonProperty("quarter")]
        public string Quarter { get; set; }
        [DataMember]
        [JsonProperty("postalCode")]
        public string PostalCd { get; set; }
        [DataMember]
        [JsonProperty("street")]
        public string Street { get; set; }
        [DataMember]
        [JsonProperty("additionalNumber")]
        public string AdditionalNo { get; set; }
        [DataMember]
        [JsonProperty("buildingNumber")]
        public string BuildingNo { get; set; }
        [DataMember]
        [JsonProperty("region")]
        public string Region { get; set; }
        [DataMember]
        [JsonProperty("regionDescription")]
        public string RegionDesc { get; set; }
    }

    // [Serializable]
 
    // [DataContract]
    public class ADDRESSSet
    {
        //  [DataMember]
        public List<ResultsItem> results { get; set; }
    }

    //public class NOTESSet
    //{
    //        public List <string> results { get; set; }
    //}

    // [Serializable]
 
    // [DataContract]
    public class ResultsItemForContact
    {
        //[DataMember]
        //public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("sourceIdentifier")]
        public string Srcidentify { get; set; }
        [DataMember]
        [JsonProperty("consumerNumber")]
        public string Consnumber { get; set; }
        [DataMember]
        [JsonProperty("startDate")]
        public string Begda { get; set; }
        [DataMember]
        [JsonProperty("endDate")]
        public string Endda { get; set; }
        [DataMember]
        [JsonProperty("telephoneNumber")]
        public string TelNumber { get; set; }
        [DataMember]
        [JsonProperty("R3User")]
        public string R3User { get; set; }
        [DataMember]
        [JsonProperty("email")]
        public string SmtpAddr { get; set; }
        [DataMember]
        [JsonProperty("mobileNumber")]
        public string MobNumber { get; set; }
    }

    // [Serializable]
 
    // [DataContract]
    public class CONTACTDTSet
    {
        // [DataMember]
        public List<ResultsItemForContact> results { get; set; }
    }

    // [Serializable]
 
    //[DataContract]
    public class ResultsItemForContactPerson
    {
        //[DataMember]
        // public __metadata __metadata { get; set; }
        //[DataMember]
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }
        //[DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        //[DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        //[DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        //[DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        //[DataMember]
        [JsonProperty("sourceIdentifier")]
        public string Srcidentify { get; set; }
        //[DataMember]
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        //[DataMember]
        [JsonProperty("endDate")]
        public string Enddt { get; set; }
        //[DataMember]
        [JsonProperty("contactType")]
        public string Contacttp { get; set; }
        //[DataMember]
        [JsonProperty("isDefault")]
        public bool Defaultfg { get; set; }
        //[DataMember]
        [JsonProperty("startDate")]
        public string Startdt { get; set; }
        //[DataMember]
        [JsonProperty("firstName")]
        public string Firstnm { get; set; }
        //[DataMember]
        [JsonProperty("lastName")]
        public string Lastnm { get; set; }
        //[DataMember]
        [JsonProperty("relationshipType")]
        public string Relationtp { get; set; }
        //[DataMember]
        [JsonProperty("fatherName")]
        public string Fathernm { get; set; }
        //[DataMember]
        [JsonProperty("grandFatherName")]
        public string Grandfathernm { get; set; }
        //[DataMember]
        [JsonProperty("familyName")]
        public string Familynm { get; set; }
        //[DataMember]
        [JsonProperty("birthDate")]
        public string Dobdt { get; set; }
        //[DataMember]
        [JsonProperty("startDateCalendarType")]
        public string StartdtC { get; set; }
        //[DataMember]
        [JsonProperty("idType")]
        public string Type { get; set; }
        //[DataMember]
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        //[DataMember]
        [JsonProperty("title")]
        public string Title { get; set; }
        //[DataMember]
        [JsonProperty("initials")]
        public string Initials { get; set; }

    }

    // [Serializable]
 
    //[DataContract]
    public class CONTACT_PERSONSet
    {
        //[DataMember]
        public List<ResultsItemForContactPerson> results { get; set; }

        public static implicit operator CONTACT_PERSONSet(List<CONTACT_PERSONSet> v)
        {
            throw new NotImplementedException();
        }
    }

    //[Serializable]
 
    //[DataContract]
    public class ResultsItemForQuestion
    {
        //[DataMember]
        //public __metadata __metadata { get; set; }
        //[DataMember]
        // public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("residencyType")]
        public string ResidencyTy { get; set; }
        [DataMember]
        [JsonProperty("questionNumber")]
        public string QueNo { get; set; }
        [DataMember]
        [JsonProperty("questionOptionsNumber")]
        public string QoptNo { get; set; }
        [DataMember]
        [JsonProperty("questionOptionsDescription")]
        public string QoptTxt { get; set; }
        [DataMember]
        [JsonProperty("questionOptionsAnswer")]
        public string QoptAns { get; set; }
    }

    [Serializable]
 
    [DataContract]
    public class ResultsItemForElgblDocSet
    {


         //{
         //           "__metadata": {
         //               "id": "https://sapgatewayqa.zatca.gov.sa/sap/opu/odata/SAP/ZDP_VRUH_SRV/ELGBL_DOCSet('')",
         //               "uri": "https://sapgatewayqa.zatca.gov.sa/sap/opu/odata/SAP/ZDP_VRUH_SRV/ELGBL_DOCSet('')",
         //               "type": "ZDP_VRUH_SRV.ELGBL_DOC"
         //           },
         //           "Mandt": "",
         //           "Spras": "",
         //           "Fbtyp": "RGVT",
         //           "TxnTp": "CRE_RGVT",
         //           "DmsTp": "ZVTD",
         //           "StartDt": "\/Date(1483228800000)\/",
         //           "EndDt": "\/Date(253402214400000)\/",
         //           "Txt50": "Income Statement"
         //       },


      //  {
      //  "systemCode": "",
      //  "language": "",
      //  "formBundleType": "RGVT",
      //  "transactionType": "CRE_RGVT",
      //  "startDate": "2017-01-01T00:00:00",
      //  "endDate": "9999-12-31T00:00:00",
      //  "documentName": "Income Statement",
      //  "documentCategory": "ZVTD"
      //},

        /*[DataMember]
        public __metadata __metadata { get; set; }*/
        /*[DataMember]
        public string Mandt { get; set; }*/
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Language { get; set; }
        [DataMember]
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [DataMember]
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [DataMember]
        [JsonProperty("documentCategory")]
        public string DmsTp { get; set; }
        [DataMember]
        public string DmsTxt { get; set; }
        [DataMember]
        [JsonProperty("documentName")] //do not change the key for this property using in VAT Attachment category - Naga
        public string Txt50 { get; set; }
    }

    // [Serializable]
 
    //  [DataContract]
    public class ResultsItemForDOCSetforsubmit
    {
        //public __metadata __metadata { get; set; }
        //[DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; } 
        //[DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        //[DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        //[DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        //[DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        //[DataMember]
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        //[DataMember]
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        //[DataMember]
        [JsonProperty("documentCategory")]
        public string DmsTp { get; set; }
        //[DataMember]
        [JsonProperty("documentDescription")]
        public string DmsTxt { get; set; }
        //public string Txt50 { get; set; }
    }

    //[Serializable]
 
    //[DataContract]
    public class QUESTIONSSet
    {
        //[DataMember]
        public List<ResultsItemForQuestion> results { get; set; }
    }

    [Serializable]
 
    [DataContract]
    public class ResultsForATTDETSet
    {
        /*[DataMember]
        public __metadata __metadata { get; set; }*/
        [DataMember]
        [JsonProperty("returnGUID")]
        public string RetGuid { get; set; }
        [DataMember]
        [JsonProperty("sequenceNumber")]
        public string Seqno { get; set; }
        [DataMember]
        [JsonProperty("formGuid")]
        public string SchGuid { get; set; }
        [DataMember]
        [JsonProperty("documentCategory")]
        public string Dotyp { get; set; }
        [DataMember]
        [JsonProperty("serialNumber")]
        public int Srno { get; set; }
        [DataMember]
        [JsonProperty("documentId")]
        public string Doguid { get; set; }
        [DataMember]
        [JsonProperty("attachedByPerson")]
        public string AttBy { get; set; }
        [DataMember]
        [JsonProperty("fileName")]
        public string Filename { get; set; }
        [DataMember]
        [JsonProperty("fileExtension")]
        public string FileExtn { get; set; }
        [DataMember]
        [JsonProperty("MIMEType")]
        public string Mimetype { get; set; }
        [DataMember]
        [JsonProperty("portalUser")]
        public string ByPusr { get; set; }
        [DataMember]
        [JsonProperty("entryDate")]
        public string Erfdt { get; set; }
        [DataMember]
        [JsonProperty("createdAt")]
        public string Erftm { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("documentURL")]
        public string DocUrl { get; set; }
        [DataMember]
        [JsonProperty("outletReference")]
        public string OutletRef { get; set; }
        [DataMember]
        [JsonProperty("enableEdit")]
        public string Enbedit { get; set; }
        [DataMember]
        [JsonProperty("enableDelete")]
        public string Enbdele { get; set; }
        [DataMember]
        [JsonProperty("visibleEdit")]
        public string Visedit { get; set; }
        [DataMember]
        [JsonProperty("visibleDelete")]
        public string Visdel { get; set; }
    }

    //[Serializable]
 
    //[DataContract]
    public class ATTDETSet
    {
        //[DataMember]
        public List<Attachment> results { get; set; }
    }

    //[Serializable]
 
    //[DataContract]
    public class QUESLISTSet
    {
        //[DataMember]
        //public List<string> results { get; set; }
        public string questionNumber { get; set; }
        public string required { get; set; }
        public string formBundleType { get; set; }
        public string transactionType { get; set; }
        public string processingReason { get; set; }
        public string questionDescription { get; set; }
    }

    
    [DataContract]
    public class vATRegistration
    {
        // public RegistrationViewAvailability MyModel = new RegistrationViewAvailability();
        //[DataMember]
        //public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("smartRegistration")]
        public string SmartReg { get; set; } // Not Found

        [DataMember]
        [JsonProperty("nonResidentIndivid")]
        public string NresFg { get; set; }

        [DataMember]
        [JsonProperty("bankGuarantyValidTo")]
        public string NresBgTo { get; set; } // Not Found
        [DataMember]
        [JsonProperty("bankGuarantyValidToFrom")]
        public string NresBgFrom { get; set; } // Not Found

        [DataMember]
        [JsonProperty("pendingIBANMessage")]
        public string PendingIbanMsg { get; set; }

        [DataMember]
        [JsonProperty("source")]
        public string Source { get; set; }
        [DataMember]
        [JsonProperty("instructionAgree")]
        public string AgrFg { get; set; }
        [DataMember]
        [JsonProperty("declarationName")]
        public string Decname { get; set; }
        [DataMember]
        [JsonProperty("confirmTaxableDate")]
        public string ConfTaxDt { get; set; }
        [DataMember]
        [JsonProperty("CRName")]
        public string CrNm { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string CrNo { get; set; }
        [DataMember]
        [JsonProperty("CRStartDate")]
        public string CrStdt { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("declarationContactNumber")]
        public string Decconno { get; set; }
        [DataMember]
        [JsonProperty("declarationDate")]
        public string Decdate { get; set; }
        [DataMember]
        [JsonProperty("declarationDesignation")]
        public string Decdesignation { get; set; }
        [DataMember]
        [JsonProperty("declaration")]
        public string Decfg { get; set; }
        [DataMember]
        [JsonProperty("declarationIdNumber")]
        public string DecidNo { get; set; }
        [DataMember]
        [JsonProperty("declarationIdType")]
        public string DecidTy { get; set; }
        [DataMember]
        [JsonProperty("serialNumber")]
        public string Euser { get; set; }
        [DataMember]
        [JsonProperty("exporterAttachment")]
        public string ExAttch { get; set; }

        [DataMember]
        [JsonProperty("billEffectiveDate")]
        public string EffDtAfter { get; set;}

        [DataMember]
        [JsonProperty("VATExporter")]
        public string ExFg { get; set; }
        [DataMember]
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("formProcess")]
        public string Formprocz { get; set; }
        [DataMember]
        [JsonProperty("futureDate")]
        public string FutureDt { get; set; }
        [DataMember]
        [JsonProperty("globalCalendarType")]
        public string GlobalCalTy { get; set; }
        [DataMember]
        [JsonProperty("goLiveDate")]
        public string GoLiveDt { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Gpartz { get; set; }

        [DataMember]
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
        [DataMember]
        [JsonProperty("importAttachment")]
        public string ImAttch { get; set; }
        [DataMember]
        [JsonProperty("VATImport")]
        public string ImFg { get; set; }

        [DataMember]
        [JsonProperty("language")]
        public string Langz { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("newRegistrationType")]
        public string NewRegTy { get; set; }
        [DataMember]
        [JsonProperty("newRegistrationTypeFromDate")]
        public string NewRegTyFrDt { get; set; } // Not Found

       [DataMember]
        [JsonProperty("userName")]
        public string Officerz { get; set; }

        [DataMember]
        [JsonProperty("operation")]
        public string Operationz { get; set; }
        [DataMember]
        [JsonProperty("optionalIBAN")]
        public string OptIban { get; set; }
        [DataMember]
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }
        [DataMember]
        [JsonProperty("reasonDescription")]
        public string ReaFg { get; set; }
        [DataMember]
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [DataMember]
        [JsonProperty("registrationType")]
        public string RegTy { get; set; }
        [DataMember]
        [JsonProperty("residencyType")]
        public string ResidencyTy { get; set; }
        [DataMember]
        [JsonProperty("returnId")]
        public string ReturnIdz { get; set; }
        [DataMember]
        [JsonProperty("statusCode")]
        public string Statusz { get; set; }
        [DataMember]
        [JsonProperty("stepNumber")]
        public string StepNumberz { get; set; }
        [DataMember]
        [JsonProperty("step2CheckBox")]
        public string Stp2Cbbox { get; set; }
        [DataMember]
        [JsonProperty("step3CheckBox")]
        public string Stp3Cbbox { get; set; }
        [DataMember]
        [JsonProperty("step4CheckBox1")]
        public string Stp4Cbbox1 { get; set; }
        [DataMember]
        [JsonProperty("step4CheckBox2")]
        public string Stp4Cbbox2 { get; set; }
        [DataMember]
        [JsonProperty("TINName")]
        public string TinNm { get; set; }
        [DataMember]
        [JsonProperty("toSubmit")]
        public string ToSflg { get; set; }
        [DataMember]
        [JsonProperty("transactionType")]
        public string TxnTpz { get; set; }
        [DataMember]
        [JsonProperty("userType")]
        public string UserTypz { get; set; }
        [DataMember]
        [JsonProperty("VATDate")]
        public string VatDt { get; set; }
        [DataMember]
        [JsonProperty("VATTaxableDate")]
        public string VatTaxDt { get; set; }
        [DataMember]
        [JsonProperty("addresses")]
        public List<ResultsItem> ADDRESSSet { get; set; }
        [DataMember]
        [JsonProperty("notes")]
        public List<Note> NOTESSet { get; set; }
        [DataMember]
        [JsonProperty("contactDetails")]
        public List<ResultsItemForContact> CONTACTDTSet { get; set; }
        [DataMember]
        [JsonProperty("eligibleDocuments")]
        public List<ResultsItemForDOCSetforsubmit> ELGBL_DOCSet { get; set; }
        [DataMember]
        [JsonProperty("contacts")]
        public List<ResultsItemForContactPerson> CONTACT_PERSONSet { get; set; }
        [DataMember]
        [JsonProperty("questions")]
        public List<ResultsItemForQuestion> QUESTIONSSet { get; set; }
        [JsonProperty("questionConfigurations")]
        public List<QuestionsetWithMinMax> QUESCONFIG_MSet { get; set; }
        [DataMember]
        [JsonProperty("attachments")]
        public List<Attachment> ATTDETSet { get; set; }
        [DataMember]
        [JsonProperty("IBANList")]
        public List<Result2> IBANSet { get; set; }
        [DataMember]
        [JsonProperty("questionsList")]
        public List<QUESLISTSet> QUESLISTSet { get; set; }
    }

    public class QUESCONFIG_MSet
    {
        //[DataMember]
        public List<QuestionsetWithMinMax> results { get; set; }

    }

    // [Serializable]
 
    //[DataContract]
    public class QuestionsetWithMinMax
    {
        /*//[DataMember]
        public __metadata __metadata { get; set; }*/
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("residencyType")]
        public string ResidencyTy { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("questionOptionsNumber")]
        public string QoptNo { get; set; }
        [DataMember]
        [JsonProperty("questionOptionsDescription")]
        public string QoptTxt { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("questionOptionsAnswers")]
        public string QoptAns { get; set; }
        [DataMember]
        [JsonProperty("questionNumber")]
        public string QueNo { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("minValue")]
        public string Minvalue { get; set; }
        [DataMember]
        [JsonProperty("maxValue")]
        public string Maxvalue { get; set; }

    }


    [Serializable]
    
    public class VATRegistrationDetails
    {
        [DataMember]
        [JsonProperty("data")]
        public vATRegistration d { get; set; }
    }

    [Serializable]
    
    public class VATRegistrationDetailsResponse
    {
        [DataMember]
        [JsonProperty("result")]
        public vATRegistration d { get; set; }
    }

    //
    //public class VATRegistrationDetailsTest
    //{
    //    public vATRegistrationTest d { get; set; }
    //}

    [Serializable]
 
    //[DataContract]
    public enum IsComeFromForAttachment
    {

        Import = 0,
        Export = 1,
        General = 3,
        FinancialReprsentative = 4
    }

    [Serializable]
 
    [DataContract]
    public class QuestionNumberWithMinMaxRange
    {
        [DataMember]
        public string QueNo = string.Empty;
        [DataMember]
        public double MinRangeValue = -1;
        [DataMember]
        public double MaxRangeValue = -1;
        [DataMember]
        public int CountOfProbableAnswersForThisQuestions = -1;
    }


    //public class RegistrationViewAvailability
    //{

    //    public RegistrationViewAvailability()
    //    {
    //        InstAndCondition = new InstAndCondition();
    //        TaxPayerDetails = new TaxPayer_Details();
    //        FinancialDetails = new FinancialDetails();
    //        FinancialRepresentative = new FinancialRepresentative();
    //        Declaration = new Declaration();
    //    }
    //}
    [Serializable]
 
    [DataContract]
    public class InstAndConditionAvailability
    {
        [DataMember]
        public bool Parent { get; set; }
        [DataMember]
        public bool CBAgreeCondition { get; set; }
    }

    [Serializable]
 
    [DataContract]
    public class TaxPayer_DetailsAvailability : ObservableRecipient
    {
        [DataMember]
        private bool _parent;
        [DataMember]
        public bool Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged("Parent");
            }

        }
        [DataMember]
        private bool _taxPayerDetailsParent;

        [DataMember]
        public bool TaxPayerDetailsParent
        {
            get { return _taxPayerDetailsParent; }
            set
            {
                _taxPayerDetailsParent = value;
                OnPropertyChanged("TaxPayerDetailsParent");
            }
        }
        [DataMember]
        private bool _AdditionalInfoParent;
        [DataMember]
        public bool AdditionalInfoParent
        {
            get { return _AdditionalInfoParent; }
            set
            {
                _AdditionalInfoParent = value;
                OnPropertyChanged("AdditionalInfoParent");
            }
        }
        [DataMember]
        private bool _TinEntry1;
        [DataMember]
        public bool TinEntry1
        {
            get { return _TinEntry1; }
            set
            {
                _TinEntry1 = value;
                OnPropertyChanged("TinEntry1");
            }
        }
        [DataMember]
        private bool _TinEntry2;
        [DataMember]
        public bool TinEntry2
        {
            get { return _TinEntry2; }
            set
            {
                _TinEntry2 = value;
                OnPropertyChanged("TinEntry2");
            }
        }
        [DataMember]
        private bool _MainOutletEntry1;

        [DataMember]
        public bool MainOutletEntry1
        {
            get { return _MainOutletEntry1; }
            set
            {
                _MainOutletEntry1 = value;
                OnPropertyChanged("MainOutletEntry1");
            }
        }
        [DataMember]
        private bool _MainOutletEntry2;

        [DataMember]
        public bool MainOutletEntry2
        {
            get { return _MainOutletEntry2; }
            set
            {
                _MainOutletEntry2 = value;
                OnPropertyChanged("MainOutletEntry2");
            }
        }
        [DataMember]
        private bool _StartDateEntry;

        [DataMember]
        public bool StartDateEntry
        {
            get { return _StartDateEntry; }
            set
            {
                _StartDateEntry = value;
                OnPropertyChanged("StartDateEntry");
            }
        }
        [DataMember]
        private bool _AddressEntry1;

        [DataMember]
        public bool AddressEntry1
        {
            get { return _AddressEntry1; }
            set
            {
                _AddressEntry1 = value;
                OnPropertyChanged("AddressEntry1");
            }
        }
        [DataMember]
        private bool _AddressEntry2;

        [DataMember]
        public bool AddressEntry2
        {
            get { return _AddressEntry2; }
            set
            {
                _AddressEntry2 = value;
                OnPropertyChanged("AddressEntry2");
            }
        }
        [DataMember]
        private bool _SourceEntry;

        [DataMember]
        public bool SourceEntry
        {
            get { return _SourceEntry; }
            set
            {
                _SourceEntry = value;
                OnPropertyChanged("SourceEntry");
            }
        }
        [DataMember]
        private bool _AddInformationCB;

        [DataMember]
        public bool AddInformationCB
        {
            get { return _AddInformationCB; }
            set
            {
                _AddInformationCB = value;
                OnPropertyChanged("AddInformationCB");
            }
        }
        [DataMember]
        private bool _AddInformationCBVisible;

        [DataMember]
        public bool AddInformationCBVisible
        {
            get { return _AddInformationCBVisible; }
            set
            {
                _AddInformationCBVisible = value;
                OnPropertyChanged("AddInformationCBVisible");
            }
        }
        [DataMember]
        private bool _AddInformationParent;

        [DataMember]
        public bool AddInformationParent
        {
            get { return _AddInformationParent; }
            set
            {
                _AddInformationParent = value;
                OnPropertyChanged("AddInformationParent");
            }
        }
        [DataMember]
        private bool _ImporterYesRB;

        [DataMember]
        public bool ImporterYesRB
        {
            get { return _ImporterYesRB; }
            set
            {
                _ImporterYesRB = value;
                OnPropertyChanged("ImporterYesRB");
            }
        }
        [DataMember]
        private bool _ImporterNoRB;

        [DataMember]
        public bool ImporterNoRB
        {
            get
            { return _ImporterNoRB; }

            set
            {
                _ImporterNoRB = value;

                OnPropertyChanged("ImporterNoRB");
            }

        }
        [DataMember]
        private bool _ImporterAttachmentsBtn;

        [DataMember]
        public bool ImporterAttachmentsBtn
        {
            get
            { return _ImporterAttachmentsBtn; }

            set
            {
                _ImporterAttachmentsBtn = value;

                OnPropertyChanged("ImporterAttachmentsBtn");
            }

        }
        [DataMember]
        private bool _ExporterYesRB;

        [DataMember]
        public bool ExporterYesRB
        {
            get
            { return _ExporterYesRB; }

            set
            {
                _ExporterYesRB = value;

                OnPropertyChanged("ExporterYesRB");
            }

        }
        [DataMember]
        private bool _ExporterNoRB;

        [DataMember]
        public bool ExporterNoRB
        {
            get
            { return _ExporterNoRB; }

            set
            {
                _ExporterNoRB = value;

                OnPropertyChanged("ExporterNoRB");
            }

        }
        [DataMember]
        private bool _ExporterrAttachmentsBtn;

        [DataMember]
        public bool ExporterrAttachmentsBtn
        {
            get
            { return _ExporterrAttachmentsBtn; }

            set
            {
                _ExporterrAttachmentsBtn = value;

                OnPropertyChanged("ExporterrAttachmentsBtn");
            }

        }
        [DataMember]
        private bool _ExistingIBANPicker;

        [DataMember]
        public bool ExistingIBANPicker
        {
            get
            { return _ExistingIBANPicker; }

            set
            {
                _ExistingIBANPicker = value;

                OnPropertyChanged("ExistingIBANPicker");
            }

        }
        [DataMember]
        private bool _NewIBANPicker;

        [DataMember]
        public bool NewIBANPicker
        {
            get
            { return _NewIBANPicker; }

            set
            {
                _NewIBANPicker = value;

                OnPropertyChanged("NewIBANPicker");
            }

        }
        [DataMember]
        private bool _CommencementDate;

        [DataMember]
        public bool CommencementDate
        {
            get
            { return _CommencementDate; }

            set
            {
                _CommencementDate = value;

                OnPropertyChanged("CommencementDate");
            }

        }
    }

    [Serializable]
 
    [DataContract]
    public class FinancialDetailsAvailability
    {
        [DataMember]
        public bool Parent { get; set; }
        [DataMember]
        public bool VATEligibilityPoint1Parent { get; set; }
        [DataMember]
        public bool VATEligibilityPoint2Parent { get; set; }
        [DataMember]
        public bool VATEligibilityPoint3Parent { get; set; }
        [DataMember]
        public bool VATEligibilityPoint4Parent { get; set; }
        [DataMember]
        public bool AttachSectionCB { get; set; }
        [DataMember]
        public bool AttachSectionCBVisible { get; set; }
        [DataMember]
        public bool AttachSectionAddNewType { get; set; }
    }


    [Serializable]
 
    [DataContract]
    public class FinancialRepresentativeAvailability : ObservableRecipient
    {
        [DataMember]
        private bool _parent;
        [DataMember]
        public bool Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged("Parent");
            }

        }
        [DataMember]
        private bool _ChangeMobileEmailCB;
        [DataMember]
        public bool ChangeMobileEmailCB
        {
            get { return _ChangeMobileEmailCB; }
            set
            {
                _ChangeMobileEmailCB = value;
                OnPropertyChanged("ChangeMobileEmailCB");
            }

        }
        [DataMember]
        private bool _AddNewFinRepresentativeCB;
        [DataMember]
        public bool AddNewFinRepresentativeCB
        {
            get { return _AddNewFinRepresentativeCB; }
            set
            {
                _AddNewFinRepresentativeCB = value;
                OnPropertyChanged("AddNewFinRepresentativeCB");
            }

        }
        [DataMember]
        private bool _AddNewFinRepCBVisible;
        [DataMember]
        public bool AddNewFinRepCBVisible
        {
            get { return _AddNewFinRepCBVisible; }
            set
            {
                _AddNewFinRepCBVisible = value;
                OnPropertyChanged("AddNewFinRepCBVisible");
            }

        }
        [DataMember]
        private bool _SkipBtn;
        [DataMember]
        public bool SkipBtn
        {
            get { return _SkipBtn; }
            set
            {
                _SkipBtn = value;
                OnPropertyChanged("SkipBtn");
            }

        }
        [DataMember]
        private bool _TinEntry;
        [DataMember]
        public bool TinEntry
        {
            get { return _TinEntry; }
            set
            {
                _TinEntry = value;
                OnPropertyChanged("TinEntry");
            }

        }
        [DataMember]
        private bool _IDTypeEntry;
        [DataMember]
        public bool IDTypeEntry
        {
            get { return _IDTypeEntry; }
            set
            {
                _IDTypeEntry = value;
                OnPropertyChanged("IDTypeEntry");
            }

        }
        [DataMember]
        private bool _IDNoEntry;
        [DataMember]
        public bool IDNoEntry
        {
            get { return _IDNoEntry; }
            set
            {
                _IDNoEntry = value;
                OnPropertyChanged("IDNoEntry");
            }

        }
        [DataMember]
        private bool _FNameEntry;
        [DataMember]
        public bool FNameEntry
        {
            get { return _FNameEntry; }
            set
            {
                _FNameEntry = value;
                OnPropertyChanged("FNameEntry");
            }

        }
        [DataMember]
        private bool _SurnameEntry;
        [DataMember]
        public bool SurnameEntry
        {
            get { return _SurnameEntry; }
            set
            {
                _SurnameEntry = value;
                OnPropertyChanged("SurnameEntry");
            }

        }
        [DataMember]
        private bool _MobileNoEntry;
        [DataMember]
        public bool MobileNoEntry
        {
            get { return _MobileNoEntry; }
            set
            {
                _MobileNoEntry = value;
                OnPropertyChanged("MobileNoEntry");
            }

        }
        [DataMember]
        private bool _EmailIDEntry;
        [DataMember]
        public bool EmailIDEntry
        {
            get { return _EmailIDEntry; }
            set
            {
                _EmailIDEntry = value;
                OnPropertyChanged("EmailIDEntry");
            }

        }

        // public List<NewFinancialRepresentative> ListNewFinRepresentative { get; set; }
    }

    //  
    //public class NewFinancialRepresentative
    //{
    //    public bool TinEntry { get; set; }
    //    public bool IDTypeEntry { get; set; }
    //    public bool IDNoEntry { get; set; }
    //    public bool FNameEntry { get; set; }
    //    public bool SurnameEntry { get; set; }
    //    public bool MobileNoEntry { get; set; }
    //    public bool EmailIDEntry { get; set; }
    //}
    [Serializable]
 
    [DataContract]
    public class DeclarationAvailability : ObservableRecipient
    {
        [DataMember]
        private bool _parent;
        [DataMember]
        public bool Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged("Parent");
            }

        }
        [DataMember]
        private bool _AcknowledgementCB;
        [DataMember]
        public bool AcknowledgementCB
        {
            get { return _AcknowledgementCB; }
            set
            {
                _AcknowledgementCB = value;
                OnPropertyChanged("AcknowledgementCB");
            }

        }
        [DataMember]
        private bool _IDTypeOrNoPicker;
        [DataMember]
        public bool IDTypeOrNoPicker
        {
            get { return _IDTypeOrNoPicker; }
            set
            {
                _IDTypeOrNoPicker = value;
                OnPropertyChanged("IDTypeOrNoPicker");
            }

        }
        [DataMember]
        private bool _IDTypeOrNoEntry;
        [DataMember]
        public bool IDTypeOrNoEntry
        {
            get { return _IDTypeOrNoEntry; }
            set
            {
                _IDTypeOrNoEntry = value;
                OnPropertyChanged("IDTypeOrNoEntry");
            }

        }
        [DataMember]
        private bool _DOBEntry;
        [DataMember]
        public bool DOBEntry
        {
            get { return _DOBEntry; }
            set
            {
                _DOBEntry = value;
                OnPropertyChanged("DOBEntry");
            }

        }
        [DataMember]
        private bool _ContactNameEntry;
        [DataMember]
        public bool ContactNameEntry
        {
            get { return _ContactNameEntry; }
            set
            {
                _ContactNameEntry = value;
                OnPropertyChanged("ContactNameEntry");
            }

        }
    }

    [Serializable]
 
    [DataContract]
    public class FinancialRepresentativesModel : ObservableRecipient
    {
        [DataMember]
        public string GpartFR { get; set; }
        [DataMember]
        public string TxtIDTypeFR { get; set; }
        [DataMember]
        public string IdnumberFR { get; set; }
        [DataMember]
        public string FirstnmFR { get; set; }
        [DataMember]
        public string LastnmFR { get; set; }
        [DataMember]
        private string _SmtpAddrFR;
        [DataMember]
        public string SmtpAddrFR { get { return _SmtpAddrFR; } set { _SmtpAddrFR = value; OnPropertyChanged("SmtpAddrFR"); } }
        [DataMember]
        private string _MobNumberFR;
        [DataMember]
        public string MobNumberFR { get { return _MobNumberFR; } set { _MobNumberFR = value; OnPropertyChanged("MobNumberFR"); } }
    }
}
