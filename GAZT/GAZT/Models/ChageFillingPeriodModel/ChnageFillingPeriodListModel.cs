using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models.ChageFillingPeriodModel
{
    public class VATChangeFillingListModel
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
            public string FbtText { get; set; }
            public string Fbtyp { get; set; }
            public string UserErrFg { get; set; }
            public string SysFlg { get; set; }
            public string Ldate { get; set; }
        }

        public class REQTYPSet
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

        public class ChangeFillingFrequency
        {
            public Metadata4 __metadata { get; set; }
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
            public string Receipt { get; set; }
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
            public List<ChangeFillingFrequency> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
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
            public REQTYPSet REQTYPSet { get; set; }
            public STATUSSet STATUSSet { get; set; }
            public ASSLISTSet ASSLISTSet { get; set; }
        }



        public class VATChangeFillingListData
        {
            public string Fbnum { get; set; }
            public string Gpart { get; set; }
            //Status code for Fbust and it used for sumamry purpose
            public string Fbust { get; set; }
            public string FbustTxt { get; set; }

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
