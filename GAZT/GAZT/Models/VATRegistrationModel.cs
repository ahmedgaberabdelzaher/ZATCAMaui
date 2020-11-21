using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace EGAZT.Models
{
    public class __metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }



    public class ResultsItem
    {
        public __metadata __metadata { get; set; }
        public string Addrnumber { get; set; }
        public string City { get; set; }
        public string Quarter { get; set; }
        public string PostalCd { get; set; }
        public string Street { get; set; }
        public string AdditionalNo { get; set; }
        public string BuildingNo { get; set; }
        public string Region { get; set; }
        public string RegionDesc { get; set; }
    }

    public class ADDRESSSet
    {
        public List<ResultsItem> results { get; set; }
    }

    //public class NOTESSet
    //{
    //        public List <string> results { get; set; }
    //}



    public class ResultsItemForContact
    {
        public __metadata __metadata { get; set; }
        public string TransactionType { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Srcidentify { get; set; }
        public string Consnumber { get; set; }
        public string Begda { get; set; }
        public string Endda { get; set; }
        public string TelNumber { get; set; }
        public string R3User { get; set; }
        public string SmtpAddr { get; set; }
        public string MobNumber { get; set; }
    }

    public class CONTACTDTSet
    {
        public List<ResultsItemForContact> results { get; set; }
    }





    public class ResultsItemForContactPerson
    {
        public __metadata __metadata { get; set; }
        public string TransactionType { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Srcidentify { get; set; }
        public string Gpart { get; set; }
        public string Enddt { get; set; }
        public string Contacttp { get; set; }
        public bool Defaultfg { get; set; }
        public string Startdt { get; set; }
        public string Firstnm { get; set; }
        public string Lastnm { get; set; }
        public string Relationtp { get; set; }
        public string Fathernm { get; set; }
        public string Grandfathernm { get; set; }
        public string Familynm { get; set; }
        public string Dobdt { get; set; }
        public string StartdtC { get; set; }
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
    }

    public class CONTACT_PERSONSet
    {
        public List<ResultsItemForContactPerson> results { get; set; }

        public static implicit operator CONTACT_PERSONSet(List<CONTACT_PERSONSet> v)
        {
            throw new NotImplementedException();
        }
    }



    public class ResultsItemForQuestion
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ResidencyTy { get; set; }
        public string QueNo { get; set; }
        public string QoptNo { get; set; }
        public string QoptTxt { get; set; }
        public string QoptAns { get; set; }
    }

    public class ResultsItemForElgblDocSet
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Fbtyp { get; set; }
        public string TxnTp { get; set; }
        public string DmsTp { get; set; }
        public string DmsTxt { get; set; }
        public string Txt50 { get; set; }
    }
    public class ResultsItemForDOCSetforsubmit
    {
   //     public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Fbtyp { get; set; }
        public string TxnTp { get; set; }
        public string DmsTp { get; set; }
        public string DmsTxt { get; set; }
        //public string Txt50 { get; set; }
    }

    public class QUESTIONSSet
    {
        public List<ResultsItemForQuestion> results { get; set; }
    }


    public class ResultsForATTDETSet
    {
        public __metadata __metadata { get; set; }
        public string RetGuid { get; set; }
        public string Seqno { get; set; }
        public string SchGuid { get; set; }
        public string Dotyp { get; set; }
        public int Srno { get; set; }
        public string Doguid { get; set; }
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public string Erfdt { get; set; }
        public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
        public string Enbedit { get; set; }
        public string Enbdele { get; set; }
        public string Visedit { get; set; }
        public string Visdel { get; set; }

    }

    public class ATTDETSet
    {
        public List<Attachment> results { get; set; }
    }



    public class QUESLISTSet
    {
        public List<string> results { get; set; }
    }

    public class vATRegistration
    {
        // public RegistrationViewAvailability MyModel = new RegistrationViewAvailability();
        public __metadata __metadata { get; set; }
        public string SmartReg { get; set; }
        public string Source { get; set; }
        public string AgrFg { get; set; }
        public string Decname { get; set; }
        public string ConfTaxDt { get; set; }
        public string CrNm { get; set; }
        public string CrNo { get; set; }
        public string CrStdt { get; set; }
        public string DataVersion { get; set; }
        public string Decconno { get; set; }
        public string Decdate { get; set; }
        public string Decdesignation { get; set; }
        public string Decfg { get; set; }
        public string DecidNo { get; set; }
        public string DecidTy { get; set; }
        public string Euser { get; set; }
        public string ExAttch { get; set; }
        public string ExFg { get; set; }
        public string Fbguid { get; set; }
        public string Fbnumz { get; set; }
        public string FormGuid { get; set; }
        public string Formprocz { get; set; }
        public string FutureDt { get; set; }
        public string GlobalCalTy { get; set; }
        public string GoLiveDt { get; set; }
        public string Gpartz { get; set; }
        public string Iban { get; set; }
        public string ImAttch { get; set; }
        public string ImFg { get; set; }
        public string Langz { get; set; }
        public string Mandt { get; set; }
        public string NewRegTy { get; set; }
        public string NewRegTyFrDt { get; set; }
        public string Officerz { get; set; }
        public string Operationz { get; set; }
        public string OptIban { get; set; }
        public string PortalUsrz { get; set; }
        public string ReaFg { get; set; }
        public string Reason { get; set; }
        public string RegTy { get; set; }
        public string ResidencyTy { get; set; }
        public string ReturnIdz { get; set; }
        public string Statusz { get; set; }
        public string StepNumberz { get; set; }
        public string Stp2Cbbox { get; set; }
        public string Stp3Cbbox { get; set; }
        public string Stp4Cbbox1 { get; set; }
        public string Stp4Cbbox2 { get; set; }
        public string TinNm { get; set; }
        public string ToSflg { get; set; }
        public string TxnTpz { get; set; }
        public string UserTypz { get; set; }
        public string VatDt { get; set; }
        public string VatTaxDt { get; set; }
        public ADDRESSSet ADDRESSSet { get; set; }
        public NOTESSet NOTESSet { get; set; }
        public CONTACTDTSet CONTACTDTSet { get; set; }
        public ELGBL_DOCSetforsubmit ELGBL_DOCSet { get; set; }
        public CONTACT_PERSONSet CONTACT_PERSONSet { get; set; }
        public QUESTIONSSet QUESTIONSSet { get; set; }
        public QUESCONFIG_MSet QUESCONFIG_MSet { get; set; }
        public ATTDETSet ATTDETSet { get; set; }
        public IBANSet IBANSet { get; set; }
        public QUESLISTSet QUESLISTSet { get; set; }
    }
    public class QUESCONFIG_MSet
    {
        public IList<QuestionsetWithMinMax> results { get; set; }

    }

    public class QuestionsetWithMinMax
    {
        public __metadata __metadata { get; set; }
        public string FormGuid { get; set; }
        public string Gpart { get; set; }
        public string DataVersion { get; set; }
        public string ResidencyTy { get; set; }
        public int LineNo { get; set; }
        public string QoptNo { get; set; }
        public string QoptTxt { get; set; }
        public string RankingOrder { get; set; }
        public string QoptAns { get; set; }
        public string QueNo { get; set; }
        public string Fbnum { get; set; }
        public string Minvalue { get; set; }
        public string Maxvalue { get; set; }

    }

    public class VATRegistrationDetails
    {
        public vATRegistration d { get; set; }
    }
    public enum IsComeFromForAttachment
    {
        Import = 0,
        Export = 1,
        General = 3,
        FinancialReprsentative = 4
    }

    public class QuestionNumberWithMinMaxRange
    {
        public string QueNo = String.Empty;
        public double MinRangeValue = -1;
        public double MaxRangeValue = -1;
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
    public class InstAndConditionAvailability
    {
        public bool Parent { get; set; }
        public bool CBAgreeCondition { get; set; }
    }
    public class TaxPayer_DetailsAvailability: ViewModelBase
    {
        private bool _parent;
        public bool Parent {
            get { return _parent; }
            set { _parent = value;
                RaisePropertyChanged("Parent");
            }

       }
        private bool _taxPayerDetailsParent;

        public bool TaxPayerDetailsParent {
            get { return _taxPayerDetailsParent; }
            set
            {
                _taxPayerDetailsParent = value;
                RaisePropertyChanged("TaxPayerDetailsParent");
            }
        }
        private bool _AdditionalInfoParent;

        public bool AdditionalInfoParent
        {
            get { return _AdditionalInfoParent; }
            set
            {
                _AdditionalInfoParent = value;
                RaisePropertyChanged("AdditionalInfoParent");
            }
        }
        private bool _TinEntry1;
        public bool TinEntry1
        {
            get { return _TinEntry1; }
            set
            {
                _TinEntry1 = value;
                RaisePropertyChanged("TinEntry1");
            }
        }

        private bool _TinEntry2;
        public bool TinEntry2
        {
            get { return _TinEntry2; }
            set
            {
                _TinEntry2 = value;
                RaisePropertyChanged("TinEntry2");
            }
        }

        private bool _MainOutletEntry1;
        public bool MainOutletEntry1
        {
            get { return _MainOutletEntry1; }
            set
            {
                _MainOutletEntry1 = value;
                RaisePropertyChanged("MainOutletEntry1");
            }
        }

        private bool _MainOutletEntry2;

        public bool MainOutletEntry2
        {
            get { return _MainOutletEntry2; }
            set
            {
                _MainOutletEntry2 = value;
                RaisePropertyChanged("MainOutletEntry2");
            }
        }
        private bool _StartDateEntry;

        public bool StartDateEntry
        {
            get { return _StartDateEntry; }
            set
            {
                _StartDateEntry = value;
                RaisePropertyChanged("StartDateEntry");
            }
        }
        private bool _AddressEntry1;

        public bool AddressEntry1
        {
            get { return _AddressEntry1; }
            set
            {
                _AddressEntry1 = value;
                RaisePropertyChanged("AddressEntry1");
            }
        }
        private bool _AddressEntry2;

        public bool AddressEntry2
        {
            get { return _AddressEntry2; }
            set
            {
                _AddressEntry2 = value;
                RaisePropertyChanged("AddressEntry2");
            }
        }
        private bool _SourceEntry;

        public bool SourceEntry
        {
            get { return _SourceEntry; }
            set
            {
                _SourceEntry = value;
                RaisePropertyChanged("SourceEntry");
            }
        }
        private bool _AddInformationCB;

        public bool AddInformationCB
        {
            get { return _AddInformationCB; }
            set
            {
                _AddInformationCB = value;
                RaisePropertyChanged("AddInformationCB");
            }
        }
        private bool _AddInformationCBVisible;

        public bool AddInformationCBVisible
        {
            get { return _AddInformationCBVisible; }
            set
            {
                _AddInformationCBVisible = value;
                RaisePropertyChanged("AddInformationCBVisible");
            }
        }

        private bool _AddInformationParent;

        public bool AddInformationParent
        {
            get { return _AddInformationParent; }
            set
            {
                _AddInformationParent = value;
                RaisePropertyChanged("AddInformationParent");
            }
        }
        private bool _ImporterYesRB;

        public bool ImporterYesRB
        {
            get { return _ImporterYesRB; }
            set
            {
                _ImporterYesRB = value;
                RaisePropertyChanged("ImporterYesRB");
            }
        }
        private bool _ImporterNoRB;
        public bool ImporterNoRB {
            get
            { return _ImporterNoRB; }

            set {
                _ImporterNoRB = value;

                RaisePropertyChanged("ImporterNoRB");
            }

        }
        private bool _ImporterAttachmentsBtn;
        public bool ImporterAttachmentsBtn
        {
            get
            { return _ImporterAttachmentsBtn; }

            set
            {
                _ImporterAttachmentsBtn = value;

                RaisePropertyChanged("ImporterAttachmentsBtn");
            }

        }
        private bool _ExporterYesRB;
        public bool ExporterYesRB
        {
            get
            { return _ExporterYesRB; }

            set
            {
                _ExporterYesRB = value;

                RaisePropertyChanged("ExporterYesRB");
            }

        }
        private bool _ExporterNoRB;
        public bool ExporterNoRB
        {
            get
            { return _ExporterNoRB; }

            set
            {
                _ExporterNoRB = value;

                RaisePropertyChanged("ExporterNoRB");
            }

        }

        private bool _ExporterrAttachmentsBtn;
        public bool ExporterrAttachmentsBtn
        {
            get
            { return _ExporterrAttachmentsBtn; }

            set
            {
                _ExporterrAttachmentsBtn = value;

                RaisePropertyChanged("ExporterrAttachmentsBtn");
            }

        }
        private bool _ExistingIBANPicker;
        public bool ExistingIBANPicker
        {
            get
            { return _ExistingIBANPicker; }

            set
            {
                _ExistingIBANPicker = value;

                RaisePropertyChanged("ExistingIBANPicker");
            }

        }
        private bool _NewIBANPicker;
        public bool NewIBANPicker
        {
            get
            { return _NewIBANPicker; }

            set
            {
                _NewIBANPicker = value;

                RaisePropertyChanged("NewIBANPicker");
            }

        }
        private bool _CommencementDate;
        public bool CommencementDate
        {
            get
            { return _CommencementDate; }

            set
            {
                _CommencementDate = value;

                RaisePropertyChanged("CommencementDate");
            }

        }
    }
    public class FinancialDetailsAvailability
    {
        public bool Parent { get; set; }
        public bool VATEligibilityPoint1Parent { get; set; }
        public bool VATEligibilityPoint2Parent { get; set; }
        public bool VATEligibilityPoint3Parent { get; set; }
        public bool VATEligibilityPoint4Parent { get; set; }

        public bool AttachSectionCB { get; set; }
        public bool AttachSectionCBVisible { get; set; }
        public bool AttachSectionAddNewType { get; set; }
    }
    public class FinancialRepresentativeAvailability : ViewModelBase
    {
        private bool _parent;
        public bool Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                RaisePropertyChanged("Parent");
            }

        }
        private bool _ChangeMobileEmailCB;
        public bool ChangeMobileEmailCB
        {
            get { return _ChangeMobileEmailCB; }
            set
            {
                _ChangeMobileEmailCB = value;
                RaisePropertyChanged("ChangeMobileEmailCB");
            }

        }

        private bool _AddNewFinRepresentativeCB;
        public bool AddNewFinRepresentativeCB
        {
            get { return _AddNewFinRepresentativeCB; }
            set
            {
                _AddNewFinRepresentativeCB = value;
                RaisePropertyChanged("AddNewFinRepresentativeCB");
            }

        }

        private bool _AddNewFinRepCBVisible;
        public bool AddNewFinRepCBVisible
        {
            get { return _AddNewFinRepCBVisible; }
            set
            {
                _AddNewFinRepCBVisible = value;
                RaisePropertyChanged("AddNewFinRepCBVisible");
            }

        }
        private bool _SkipBtn;
        public bool SkipBtn
        {
            get { return _SkipBtn; }
            set
            {
                _SkipBtn = value;
                RaisePropertyChanged("SkipBtn");
            }

        }
        private bool _TinEntry;
        public bool TinEntry
        {
            get { return _TinEntry; }
            set
            {
                _TinEntry = value;
                RaisePropertyChanged("TinEntry");
            }

        }
        private bool _IDTypeEntry;
        public bool IDTypeEntry
        {
            get { return _IDTypeEntry; }
            set
            {
                _IDTypeEntry = value;
                RaisePropertyChanged("IDTypeEntry");
            }

        }
        private bool _IDNoEntry;
        public bool IDNoEntry
        {
            get { return _IDNoEntry; }
            set
            {
                _IDNoEntry = value;
                RaisePropertyChanged("IDNoEntry");
            }

        }
        private bool _FNameEntry;
        public bool FNameEntry
        {
            get { return _FNameEntry; }
            set
            {
                _FNameEntry = value;
                RaisePropertyChanged("FNameEntry");
            }

        }
        private bool _SurnameEntry;
        public bool SurnameEntry
        {
            get { return _SurnameEntry; }
            set
            {
                _SurnameEntry = value;
                RaisePropertyChanged("SurnameEntry");
            }

        }
        private bool _MobileNoEntry;
        public bool MobileNoEntry
        {
            get { return _MobileNoEntry; }
            set
            {
                _MobileNoEntry = value;
                RaisePropertyChanged("MobileNoEntry");
            }

        }
        private bool _EmailIDEntry;
        public bool EmailIDEntry
        {
            get { return _EmailIDEntry; }
            set
            {
                _EmailIDEntry = value;
                RaisePropertyChanged("EmailIDEntry");
            }

        }
     
       // public List<NewFinancialRepresentative> ListNewFinRepresentative { get; set; }
    }
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
    public class DeclarationAvailability: ViewModelBase
    {
        private bool _parent;
        public bool Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                RaisePropertyChanged("Parent");
            }

        }
        private bool _AcknowledgementCB;
        public bool AcknowledgementCB
        {
            get { return _AcknowledgementCB; }
            set
            {
                _AcknowledgementCB = value;
                RaisePropertyChanged("AcknowledgementCB");
            }

        }
        private bool _IDTypeOrNoPicker;
        public bool IDTypeOrNoPicker
        {
            get { return _IDTypeOrNoPicker; }
            set
            {
                _IDTypeOrNoPicker = value;
                RaisePropertyChanged("IDTypeOrNoPicker");
            }

        }
        private bool _IDTypeOrNoEntry;
        public bool IDTypeOrNoEntry
        {
            get { return _IDTypeOrNoEntry; }
            set
            {
                _IDTypeOrNoEntry = value;
                RaisePropertyChanged("IDTypeOrNoEntry");
            }

        }
        private bool _DOBEntry;
        public bool DOBEntry
        {
            get { return _DOBEntry; }
            set
            {
                _DOBEntry = value;
                RaisePropertyChanged("DOBEntry");
            }

        }
        private bool _ContactNameEntry;
        public bool ContactNameEntry
        {
            get { return _ContactNameEntry; }
            set
            {
                _ContactNameEntry = value;
                RaisePropertyChanged("ContactNameEntry");
            }

        }
    }
    public class FinancialRepresentativesModel
    {
        public string GpartFR { get; set; }
        public string TxtIDTypeFR { get; set; }
        public string IdnumberFR { get; set; }
        public string FirstnmFR { get; set; }
        public string LastnmFR { get; set; }
        public string SmtpAddrFR { get; set; }
        public string MobNumberFR { get; set; }
    }
}
