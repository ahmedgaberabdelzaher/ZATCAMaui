namespace ZATCAMAUI.Models.ChageFillingPeriodModel
{

    public class ChangeFillingPeriodModel
    {
        public ChangeFillingPeriodModel()
        {
        }
        public string CardLabel { get => ActiveOutletDecisionOptions; }
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }

   
    public class InstalmentAgreementAttachmentsModel
    {
        public InstalmentAgreementAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }

   
    public class MyRequestsListModel
    {
        public MyRequestsListModel()
        {
        }

        public string Title { get; set; }
        public string ReferenceNumber { get; set; }
        public string Status { get; set; }
        public string CurrentFrequency { get; set; }
        public string NewFrequency { get; set; }
        public string EffectiveDate { get; set; }
        public string ReleaseDate { get; set; }
    }


/* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
Before:
    #region VATchangeFillingPeriodPostModel
    
    public class VATchangeFillingPeriodPostModel
After:
    #region VATchangeFillingPeriodPostModel

    public class VATchangeFillingPeriodPostModel
*/
    #region VATchangeFillingPeriodPostModel

    public class VATchangeFillingPeriodPostModel
    {
       
        public RequestVATFillingPeriod d { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class UIBTNSet
        {
            public List<object> results { get; set; }
        }
       
        public class QuesListSet
        {
            public List<object> results { get; set; }
        }
       
        public class RequestVATFillingPeriod
        {
            public Metadata __metadata { get; set; }
            public string Attchk { get; set; }
            public string CPersl { get; set; }
            public string Fbnumz { get; set; }
            public string Iagrfg { get; set; }
            public string Reqfg { get; set; }
            public string StepNumber { get; set; }
            public object Begda { get; set; }
            public string PortalUsrz { get; set; }
            public string Langz { get; set; }
            public string TransType { get; set; }
            public string Gpart { get; set; }
            public string Operationz { get; set; }
            public string Fbtyp { get; set; }
            public string StepNumberz { get; set; }
            public string Fbust { get; set; }
            public string ReturnIdz { get; set; }
            public string Officerz { get; set; }
            public string UserTyp { get; set; }
            public string Gpartz { get; set; }
            public string TransactionType { get; set; }
            public string EditFg { get; set; }
            public string Statusz { get; set; }
            public string Euser { get; set; }
            public string UserTypz { get; set; }
            public string Fbguid { get; set; }
            public string TxnTpz { get; set; }
            public string DmodeFlg { get; set; }
            public string Formprocz { get; set; }
            public string EvStatus { get; set; }
            public string OfficerTz { get; set; }
            public string SrcAppz { get; set; }
            public string Mandt { get; set; }
            public string FormGuid { get; set; }
            public string DataVersion { get; set; }
            public string ReturnId { get; set; }
            public string CureentF { get; set; }
            public string FilingF { get; set; }
            public string Persl { get; set; }
            public string Decfg { get; set; }
            public string Decname { get; set; }
            public string Decdesignation { get; set; }
            public object Decdate { get; set; }
            public string DecidTy { get; set; }
            public string DecidNo { get; set; }
            public List<object> EffDateSet { get; set; }
            public VATChangeFillingPeriodRequestModel.UIBTNSet UI_BTNSet { get; set; }
            public List<NotesSetResult> NOTESSet { get; set; }
            public List<Attachment> ATTACHSet { get; set; }
            public List<AttTypSetList> ATT_TYPSet { get; set; }
            public VATChangeFillingPeriodRequestModel.QuesListSet QuesListSet { get; set; }
        }


    }
    #endregion

   
    public class ATTTYPSet
    {
        public List<AttTypSetList> results { get; set; }
    }
   
    public partial class AttTypSetList
    {
        public Metadata __metadata { get; set; }
        public string DmsTp { get; set; }
        public string Txt50 { get; set; }
    }

   
    public partial class NotesSetResult
    {
        public Metadata Metadata { get; set; }
        public string Notenoz { get; set; }
        public string Refnamez { get; set; }
        public string XInvoicez { get; set; }
        public string XObsoletez { get; set; }
        public string Rcodez { get; set; }
        public string Erfusrz { get; set; }
        public string Erfdtz { get; set; }
        public string Erftmz { get; set; }
        public string AttByz { get; set; }
        public string ByPusrz { get; set; }
        public string ByGpartz { get; set; }
        public string DataVersionz { get; set; }
        public string Namez { get; set; }
        public string Noteno { get; set; }
        public long Lineno { get; set; }
        public long ElemNo { get; set; }
        public string Tdformat { get; set; }
        public string Tdline { get; set; }
        public string Sect { get; set; }
        public string Strdt { get; set; }
        public string Strtime { get; set; }
        public string Strline { get; set; }
    }
    #region VATChangeFillingPeriodRequestModel

    public class VATChangeFillingPeriodRequestModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
       
        public D d { get; set; }
       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       

        public class EffDateSet
        {
            public List<object> results { get; set; }
        }
       
        public class UIBTNSet
        {
            public List<object> results { get; set; }
        }
       
        public class NOTESSet
        {
            public List<NotesSetResult> results { get; set; }
        }
       


        public class ATTACHSet
        {
            public List<Attachment> results { get; set; }
        }
       


        public class QuesListSet
        {
            public List<object> results { get; set; }
        }
       
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Attchk { get; set; }
            public string CPersl { get; set; }
            public string Fbnumz { get; set; }
            public string Iagrfg { get; set; }
            public string Reqfg { get; set; }
            public string StepNumber { get; set; }
            public object Begda { get; set; }
            public string PortalUsrz { get; set; }
            public string Langz { get; set; }
            public string TransType { get; set; }
            public string Gpart { get; set; }
            public string Operationz { get; set; }
            public string Fbtyp { get; set; }
            public string StepNumberz { get; set; }
            public string Fbust { get; set; }
            public string ReturnIdz { get; set; }
            public string Officerz { get; set; }
            public string UserTyp { get; set; }
            public string Gpartz { get; set; }
            public string TransactionType { get; set; }
            public string EditFg { get; set; }
            public string Statusz { get; set; }
            public string Euser { get; set; }
            public string UserTypz { get; set; }
            public string Fbguid { get; set; }
            public string TxnTpz { get; set; }
            public string DmodeFlg { get; set; }
            public string Formprocz { get; set; }
            public string EvStatus { get; set; }
            public string OfficerTz { get; set; }
            public string SrcAppz { get; set; }
            public string Mandt { get; set; }
            public string FormGuid { get; set; }
            public string DataVersion { get; set; }
            public string ReturnId { get; set; }
            public string CureentF { get; set; }
            public string FilingF { get; set; }
            public string Persl { get; set; }
            public string Decfg { get; set; }
            public string Decname { get; set; }
            public string Decdesignation { get; set; }
            public object Decdate { get; set; }
            public string DecidTy { get; set; }
            public string DecidNo { get; set; }
            public EffDateSet EffDateSet { get; set; }
            public UIBTNSet UI_BTNSet { get; set; }
            public NOTESSet NOTESSet { get; set; }
            public ATTACHSet ATTACHSet { get; set; }
            public ATTTYPSet ATT_TYPSet { get; set; }
            public QuesListSet QuesListSet { get; set; }
        }
    }
    #endregion
    #region VATRefillingDropdownModel

    public class VATRefillingDropdownModel
    {
       
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result
        {
            public Metadata2 __metadata { get; set; }
            public string Mandt { get; set; }
            public string Fbtyp { get; set; }
            public string Fbust { get; set; }
            public string Button { get; set; }
            public string TransactionType { get; set; }
            public string UserTyp { get; set; }
        }
       
        public class UIBTNSet
        {
            public List<Result> results { get; set; }
        }
       
        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class EffDate
        {
            public Metadata3 __metadata { get; set; }
            public string Mandt { get; set; }
            public string Spras { get; set; }
            public string Persl { get; set; }
            public string Txt50 { get; set; }
        }
       
        public class EffDateSet
        {
            public List<EffDate> results { get; set; }
        }
       
        public class Metadata4
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result3
        {
            public Metadata4 __metadata { get; set; }
            public string Mandt { get; set; }
            public string Spras { get; set; }
            public string Fbtyp { get; set; }
            public string TxnTp { get; set; }
            public string DmsTp { get; set; }
            public object StartDt { get; set; }
            public object EndDt { get; set; }
            public string Txt50 { get; set; }
        }
       
        public class ATTTYPSet
        {
            public List<Result3> results { get; set; }
        }
       
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Mandtz { get; set; }
            public string Fbtypz { get; set; }
            public string Fbustz { get; set; }
            public string UserTypz { get; set; }
            public string TransactionTypez { get; set; }
            public string EditFgz { get; set; }
            public string Mandt { get; set; }
            public string Fbnum { get; set; }
            public string PortalUsr { get; set; }
            public string Lang { get; set; }
            public string Operation { get; set; }
            public string StepNumber { get; set; }
            public string Officer { get; set; }
            public string Gpart { get; set; }
            public string Status { get; set; }
            public string UserTyp { get; set; }
            public string TxnTp { get; set; }
            public string Formproc { get; set; }
            public string OfficerT { get; set; }
            public string SrcApp { get; set; }
            public string DestCheck { get; set; }
            public UIBTNSet UI_BTNSet { get; set; }
            public EffDateSet EffDateSet { get; set; }
            public ATTTYPSet ATT_TYPSet { get; set; }
        }

    }
    #endregion
    #region VATRefillingWorkItemsModel

    public class VATRefillingWorkItemsModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
       
        public D d { get; set; }
       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result
        {
            public Metadata2 __metadata { get; set; }
            public string Selector { get; set; }
            public string Fbnum { get; set; }
            public string Fbtyp { get; set; }
            public string Gpart { get; set; }
            public string NameLast { get; set; }
            public string NameFirst { get; set; }
            public string NameOrg1 { get; set; }
            public string NameOrg2 { get; set; }
            public string FullNm { get; set; }
            public string GpartFullNm { get; set; }
            public string WfSub { get; set; }
            public string Fbust { get; set; }
            public string FbustTxt { get; set; }
            public DateTime Receipt { get; set; }
            public string AssignUsr { get; set; }
            public string LoginUsr { get; set; }
            public string AssignMe { get; set; }
            public string NewUser { get; set; }
            public string TileInd { get; set; }
            public string FbtText { get; set; }
            public string TransactionType { get; set; }
            public string WiId { get; set; }
            public string WiPrio { get; set; }
            public string WiPrioDesc { get; set; }
        }
       
        public class ASSLISTSet
        {
            public List<Result> results { get; set; }
        }
       
        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result2
        {
            public Metadata3 __metadata { get; set; }
            public string Mandt { get; set; }
            public string Stsma { get; set; }
            public string Estat { get; set; }
            public string Spras { get; set; }
            public string Txt04 { get; set; }
            public string Txt30 { get; set; }
            public bool Ltext { get; set; }
        }
       
        public class STATUSSet
        {
            public List<Result2> results { get; set; }
        }
       
        public class Metadata4
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result3
        {
            public Metadata4 __metadata { get; set; }
            public string UserErrFg { get; set; }
            public string Fbtyp { get; set; }
            public string FbtText { get; set; }
            public string SysFlg { get; set; }
            public DateTime? Ldate { get; set; }
        }
       
        public class REQTYPSet
        {
            public List<Result3> results { get; set; }
        }
       
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Filffreqalwd { get; set; }
            public string UserTin { get; set; }
            public string AudTin { get; set; }
            public object Begda { get; set; }
            public string TaxType { get; set; }
            public object Endda { get; set; }
            public string Euser { get; set; }
            public string Fbnum { get; set; }
            public string Fbsta { get; set; }
            public string Fbtyp { get; set; }
            public string Fbust { get; set; }
            public string Formproc { get; set; }
            public string Gpart { get; set; }
            public string Lang { get; set; }
            public string Mandt { get; set; }
            public string Officer { get; set; }
            public string Operation { get; set; }
            public string Persl { get; set; }
            public string PortalUsr { get; set; }
            public string ReturnId { get; set; }
            public string Status { get; set; }
            public string StepNumber { get; set; }
            public string TransactionType { get; set; }
            public string TxnTp { get; set; }
            public string UserTyp { get; set; }
            public ASSLISTSet ASSLISTSet { get; set; }
            public STATUSSet STATUSSet { get; set; }
            public REQTYPSet REQTYPSet { get; set; }
        }

    }


    #endregion

    #region ValidateIdNumber
   
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
   
    public class D
    {
        public Metadata __metadata { get; set; }
        public DateTime Birthdt { get; set; }
        public string Bpkind { get; set; }
        public string Country { get; set; }
        public string IdIssueingCountry { get; set; }
        public string Source { get; set; }
        public string TaxpDob { get; set; }
        public string PassExpDt { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Floor { get; set; }
        public string Tin { get; set; }
        public string AdditionalNo { get; set; }
        public string HouseNo { get; set; }
        public string Idtype { get; set; }
        public string BirthdtC { get; set; }
        public string BuildingNo { get; set; }
        public string Birthdt10 { get; set; }
        public string Idnum { get; set; }
        public string FatherName { get; set; }
        public string PoBox { get; set; }
        public string GrandfatherName { get; set; }
        public string Street1 { get; set; }
        public string FamilyName { get; set; }
        public string Street2 { get; set; }
        public string Initials { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Quarter { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string FaxNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DefltComm { get; set; }
        public string Adrnr { get; set; }
        public string Website { get; set; }
        public string Augrp { get; set; }
        public string BranchDesc { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string BpkindDesc { get; set; }
        public string RegionDesc { get; set; }
    }

    public class ValidateIDResponse
    {
       
        public D d { get; set; }
       
        public string errorMessage { get; set; }
    }
    #endregion
}
