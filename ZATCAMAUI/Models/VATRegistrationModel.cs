using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

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
        //[DataMember]
        public __metadata __metadata { get; set; }
        //[DataMember]
        public string Addrnumber { get; set; }
        //[DataMember]
        public string City { get; set; }
        //[DataMember]
        public string Quarter { get; set; }
        //[DataMember]
        public string PostalCd { get; set; }
        //[DataMember]
        public string Street { get; set; }
        //[DataMember]
        public string AdditionalNo { get; set; }
        //[DataMember]
        public string BuildingNo { get; set; }
        //[DataMember]
        public string Region { get; set; }
        //[DataMember]
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
        public __metadata __metadata { get; set; }
        //[DataMember]
        public string TransactionType { get; set; }
        //[DataMember]
        public string FormGuid { get; set; }
        //[DataMember]
        public string DataVersion { get; set; }
        //[DataMember]
        public int LineNo { get; set; }
        //[DataMember]
        public string RankingOrder { get; set; }
        //[DataMember]
        public string Srcidentify { get; set; }
        //[DataMember]
        public string Consnumber { get; set; }
        //[DataMember]
        public string Begda { get; set; }
        //[DataMember]
        public string Endda { get; set; }
        //[DataMember]
        public string TelNumber { get; set; }
        //[DataMember]
        public string R3User { get; set; }
        //[DataMember]
        public string SmtpAddr { get; set; }
        //[DataMember]
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
        public __metadata __metadata { get; set; }
        //[DataMember]
        public string TransactionType { get; set; }
        //[DataMember]
        public string FormGuid { get; set; }
        //[DataMember]
        public string DataVersion { get; set; }
        //[DataMember]
        public int LineNo { get; set; }
        //[DataMember]
        public string RankingOrder { get; set; }
        //[DataMember]
        public string Srcidentify { get; set; }
        //[DataMember]
        public string Gpart { get; set; }
        //[DataMember]
        public string Enddt { get; set; }
        //[DataMember]
        public string Contacttp { get; set; }
        //[DataMember]
        public bool Defaultfg { get; set; }
        //[DataMember]
        public string Startdt { get; set; }
        //[DataMember]
        public string Firstnm { get; set; }
        //[DataMember]
        public string Lastnm { get; set; }
        //[DataMember]
        public string Relationtp { get; set; }
        //[DataMember]
        public string Fathernm { get; set; }
        //[DataMember]
        public string Grandfathernm { get; set; }
        //[DataMember]
        public string Familynm { get; set; }
        //[DataMember]
        public string Dobdt { get; set; }
        //[DataMember]
        public string StartdtC { get; set; }
        //[DataMember]
        public string Type { get; set; }
        //[DataMember]
        public string Idnumber { get; set; }
        //[DataMember]
        public string Title { get; set; }
        //[DataMember]
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
        public __metadata __metadata { get; set; }
        //[DataMember]
        public string Mandt { get; set; }
        //[DataMember]
        public string FormGuid { get; set; }
        //[DataMember]
        public string DataVersion { get; set; }
        //[DataMember]
        public int LineNo { get; set; }
        //[DataMember]
        public string RankingOrder { get; set; }
        //[DataMember]
        public string ResidencyTy { get; set; }
        //[DataMember]
        public string QueNo { get; set; }
        //[DataMember]
        public string QoptNo { get; set; }
        //[DataMember]
        public string QoptTxt { get; set; }
        //[DataMember]
        public string QoptAns { get; set; }
    }

    [Serializable]
 
    [DataContract]
    public class ResultsItemForElgblDocSet
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string Fbtyp { get; set; }
        [DataMember]
        public string TxnTp { get; set; }
        [DataMember]
        public string DmsTp { get; set; }
        [DataMember]
        public string DmsTxt { get; set; }
        [DataMember]
        public string Txt50 { get; set; }
    }

    // [Serializable]
 
    //  [DataContract]
    public class ResultsItemForDOCSetforsubmit
    {
        //     public __metadata __metadata { get; set; }
        //[DataMember]
        public string Mandt { get; set; }
        //[DataMember]
        public string FormGuid { get; set; }
        //[DataMember]
        public string DataVersion { get; set; }
        //[DataMember]
        public int LineNo { get; set; }
        //[DataMember]
        public string RankingOrder { get; set; }
        //[DataMember]
        public string Fbtyp { get; set; }
        //[DataMember]
        public string TxnTp { get; set; }
        //[DataMember]
        public string DmsTp { get; set; }
        //[DataMember]
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
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string RetGuid { get; set; }
        [DataMember]
        public string Seqno { get; set; }
        [DataMember]
        public string SchGuid { get; set; }
        [DataMember]
        public string Dotyp { get; set; }
        [DataMember]
        public int Srno { get; set; }
        [DataMember]
        public string Doguid { get; set; }
        [DataMember]
        public string AttBy { get; set; }
        [DataMember]
        public string Filename { get; set; }
        [DataMember]
        public string FileExtn { get; set; }
        [DataMember]
        public string Mimetype { get; set; }
        [DataMember]
        public string ByPusr { get; set; }
        [DataMember]
        public string Erfdt { get; set; }
        [DataMember]
        public string Erftm { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string DocUrl { get; set; }
        [DataMember]
        public string OutletRef { get; set; }
        [DataMember]
        public string Enbedit { get; set; }
        [DataMember]
        public string Enbdele { get; set; }
        [DataMember]
        public string Visedit { get; set; }
        [DataMember]
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
        public List<string> results { get; set; }
    }

    [Serializable]
 
    [DataContract]
    public class vATRegistration
    {
        // public RegistrationViewAvailability MyModel = new RegistrationViewAvailability();
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string SmartReg { get; set; }
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string NresFg { get; set; }
        [DataMember]
        public string NresBgTo { get; set; }
        [DataMember]
        public string NresBgFrom { get; set; }
        [DataMember]
        public string PendingIbanMsg { get; set; }
        [DataMember]
        public string AgrFg { get; set; }
        [DataMember]
        public string Decname { get; set; }
        [DataMember]
        public string ConfTaxDt { get; set; }
        [DataMember]
        public string CrNm { get; set; }
        [DataMember]
        public string CrNo { get; set; }
        [DataMember]
        public string CrStdt { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string Decconno { get; set; }
        [DataMember]
        public string Decdate { get; set; }
        [DataMember]
        public string Decdesignation { get; set; }
        [DataMember]
        public string Decfg { get; set; }
        [DataMember]
        public string DecidNo { get; set; }
        [DataMember]
        public string DecidTy { get; set; }
        [DataMember]
        public string Euser { get; set; }
        [DataMember]
        public string ExAttch { get; set; }
        [DataMember]
        public string ExFg { get; set; }
        [DataMember]
        public string Fbguid { get; set; }
        [DataMember]
        public string Fbnumz { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string Formprocz { get; set; }
        [DataMember]
        public string FutureDt { get; set; }
        [DataMember]
        public string GlobalCalTy { get; set; }
        [DataMember]
        public string GoLiveDt { get; set; }
        [DataMember]
        public string Gpartz { get; set; }
        [DataMember]
        public string Iban { get; set; }
        [DataMember]
        public string ImAttch { get; set; }
        [DataMember]
        public string ImFg { get; set; }
        [DataMember]
        public string Langz { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string NewRegTy { get; set; }
        [DataMember]
        public string NewRegTyFrDt { get; set; }
        [DataMember]
        public string Officerz { get; set; }
        [DataMember]
        public string Operationz { get; set; }
        [DataMember]
        public string OptIban { get; set; }
        [DataMember]
        public string PortalUsrz { get; set; }
        [DataMember]
        public string ReaFg { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public string RegTy { get; set; }
        [DataMember]
        public string ResidencyTy { get; set; }
        [DataMember]
        public string ReturnIdz { get; set; }
        [DataMember]
        public string Statusz { get; set; }
        [DataMember]
        public string StepNumberz { get; set; }
        [DataMember]
        public string Stp2Cbbox { get; set; }
        [DataMember]
        public string Stp3Cbbox { get; set; }
        [DataMember]
        public string Stp4Cbbox1 { get; set; }
        [DataMember]
        public string Stp4Cbbox2 { get; set; }
        [DataMember]
        public string TinNm { get; set; }
        [DataMember]
        public string ToSflg { get; set; }
        [DataMember]
        public string TxnTpz { get; set; }
        [DataMember]
        public string UserTypz { get; set; }
        [DataMember]
        public string VatDt { get; set; }
        [DataMember]
        public string VatTaxDt { get; set; }
        [DataMember]
        public ADDRESSSet ADDRESSSet { get; set; }
        [DataMember]
        public NOTESSet NOTESSet { get; set; }
        [DataMember]
        public CONTACTDTSet CONTACTDTSet { get; set; }
        [DataMember]
        public ELGBL_DOCSetforsubmit ELGBL_DOCSet { get; set; }
        [DataMember]
        public CONTACT_PERSONSet CONTACT_PERSONSet { get; set; }
        [DataMember]
        public QUESTIONSSet QUESTIONSSet { get; set; }
        [DataMember]
        public QUESCONFIG_MSet QUESCONFIG_MSet { get; set; }
        [DataMember]
        public ATTDETSet ATTDETSet { get; set; }
        [DataMember]
        public IBANSet IBANSet { get; set; }
        [DataMember]
        public QUESLISTSet QUESLISTSet { get; set; }
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
        //[DataMember]
        public __metadata __metadata { get; set; }
        //[DataMember]
        public string FormGuid { get; set; }
        //[DataMember]
        public string Gpart { get; set; }
        //[DataMember]
        public string DataVersion { get; set; }
        //[DataMember]
        public string ResidencyTy { get; set; }
        //[DataMember]
        public int LineNo { get; set; }
        //[DataMember]
        public string QoptNo { get; set; }
        //[DataMember]
        public string QoptTxt { get; set; }
        //[DataMember]
        public string RankingOrder { get; set; }
        //[DataMember]
        public string QoptAns { get; set; }
        //[DataMember]
        public string QueNo { get; set; }
        //[DataMember]
        public string Fbnum { get; set; }
        //[DataMember]
        public string Minvalue { get; set; }
        //[DataMember]
        public string Maxvalue { get; set; }

    }

    // [Serializable]
 
    // [DataContract]
    public class VATRegistrationDetails
    {
        //[DataMember]
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
