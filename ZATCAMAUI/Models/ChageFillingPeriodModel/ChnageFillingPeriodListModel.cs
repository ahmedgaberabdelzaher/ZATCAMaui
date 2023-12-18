using System.Runtime.Serialization;

namespace ZATCAMAUI.Models.ChageFillingPeriodModel
{

    public class VATChangeFillingListModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
       
        public D d { get; set; }
       
        public class Metadata
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       

        public class Metadata2
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       
        public class Result
        {
            [DataMember]
            public Metadata2 __metadata { get; set; }
            [DataMember]
            public string FbtText { get; set; }
            [DataMember]
            public string Fbtyp { get; set; }
            [DataMember]
            public string UserErrFg { get; set; }
            [DataMember]
            public string SysFlg { get; set; }
            [DataMember]
            public string Ldate { get; set; }
        }
       
        public class REQTYPSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }
       
        public class Metadata3
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       
        public class Result2
        {
            [DataMember]
            public Metadata3 __metadata { get; set; }
            [DataMember]
            public string Mandt { get; set; }
            [DataMember]
            public string Stsma { get; set; }
            [DataMember]
            public string Estat { get; set; }
            [DataMember]
            public string Spras { get; set; }
            [DataMember]
            public string Txt04 { get; set; }
            [DataMember]
            public string Txt30 { get; set; }
            [DataMember]
            public bool Ltext { get; set; }
        }
       
        public class STATUSSet
        {
            [DataMember]
            public List<Result2> results { get; set; }
        }
       

        public class Metadata4
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       
        public class ChangeFillingFrequency
        {
            [DataMember]
            public Metadata4 __metadata { get; set; }
            [DataMember]
            public string Selector { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Fbtyp { get; set; }
            [DataMember]
            public string Gpart { get; set; }
            [DataMember]
            public string NameLast { get; set; }
            [DataMember]
            public string NameFirst { get; set; }
            [DataMember]
            public string NameOrg1 { get; set; }
            [DataMember]
            public string NameOrg2 { get; set; }
            [DataMember]
            public string FullNm { get; set; }
            [DataMember]
            public string GpartFullNm { get; set; }
            [DataMember]
            public string WfSub { get; set; }
            [DataMember]
            public string Fbust { get; set; }
            [DataMember]
            public string FbustTxt { get; set; }
            [DataMember]
            public string Receipt { get; set; }
            [DataMember]
            public string AssignUsr { get; set; }
            [DataMember]
            public string LoginUsr { get; set; }
            [DataMember]
            public string AssignMe { get; set; }
            [DataMember]
            public string NewUser { get; set; }
            [DataMember]
            public string TileInd { get; set; }
            [DataMember]
            public string FbtText { get; set; }
            [DataMember]
            public string TransactionType { get; set; }
            [DataMember]
            public string WiId { get; set; }
            [DataMember]
            public string WiPrio { get; set; }
            [DataMember]
            public string WiPrioDesc { get; set; }
        }
       
        public class ASSLISTSet
        {
            [DataMember]
            public List<ChangeFillingFrequency> results { get; set; }
        }

        [Serializable]
        [DataContract]
       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string UserTin { get; set; }
            [DataMember]
            public string AudTin { get; set; }
            [DataMember]
            public object Begda { get; set; }
            [DataMember]
            public string TaxType { get; set; }
            [DataMember]
            public object Endda { get; set; }
            [DataMember]
            public string Euser { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Fbsta { get; set; }
            [DataMember]
            public string Fbtyp { get; set; }
            [DataMember]
            public string Fbust { get; set; }
            [DataMember]
            public string Formproc { get; set; }
            [DataMember]
            public string Gpart { get; set; }
            [DataMember]
            public string Lang { get; set; }
            [DataMember]
            public string Mandt { get; set; }
            [DataMember]
            public string Officer { get; set; }
            [DataMember]
            public string Operation { get; set; }
            [DataMember]
            public string Persl { get; set; }
            [DataMember]
            public string PortalUsr { get; set; }
            [DataMember]
            public string ReturnId { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string StepNumber { get; set; }
            [DataMember]
            public string TransactionType { get; set; }
            [DataMember]
            public string TxnTp { get; set; }
            [DataMember]
            public string UserTyp { get; set; }
            [DataMember]
            public REQTYPSet REQTYPSet { get; set; }
            [DataMember]
            public STATUSSet STATUSSet { get; set; }
            [DataMember]
            public ASSLISTSet ASSLISTSet { get; set; }
        }


       
        public class VATChangeFillingListData
        {
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Gpart { get; set; }
            [DataMember]
            //Status code for Fbust and it used for sumamry purpose
            public string Fbust { get; set; }
            [DataMember]
            public string FbustTxt { get; set; }
            [DataMember]

            //FbtText means Request to change filing frequency
            public string FbtText { get; set; }

        }

    }
   
    public class VATChangeFillingSummaryModel
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
            public List<object> results { get; set; }
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
       
        public class ATTACHSet
        {
            public List<Attachment> results { get; set; }
        }
       
        public class ATTTYPSet
        {
            public List<object> results { get; set; }
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

       
        public class VATChangingSummaryData
        {

            //step1
            public string Attchk { get; set; }
            public string Fbnum { get; set; }

            //step2
            public string CureentF { get; set; }
            public string FilingF { get; set; }

            public string Persl { get; set; }
            //Step-3
            public string Decfg { get; set; }
            public string Decname { get; set; }
            public string DecidNo { get; set; }

            public string DecidTy { get; set; }

            public ATTACHSet AttachmentList { get; set; }
            public NOTESSet NOTESSet { get; set; }
        }




    }
}
