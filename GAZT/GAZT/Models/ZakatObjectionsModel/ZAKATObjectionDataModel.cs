using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.ZakatObjectionsModel
{
    [Preserve(AllMembers = true)]
    public class ZAKATObjectionDataModel
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
            public string RetGuid { get; set; }
            public string DocUrl { get; set; }
            public string Seqno { get; set; }
            public string SchGuid { get; set; }
            public string Dotyp { get; set; }
            public int Srno { get; set; }
            public string Doguid { get; set; }
            public string AttBy { get; set; }
            public string Filename { get; set; }
            public string FileExtn { get; set; }
            public string Mimetype { get; set; }
            public DateTime Erfdt { get; set; }
            public string Erftm { get; set; }
            public string Enbedit { get; set; }
            public string Enbdele { get; set; }
            public string Visedit { get; set; }
            public string Visdel { get; set; }
        }

        public class AttDetSet
        {
            public List<Result> results { get; set; }
        }

        public class ZnotesSet
        {
            public List<object> results { get; set; }
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
            public string ASel { get; set; }
            public string ARefNo { get; set; }
            public string AAssnmtYr { get; set; }
            public DateTime APeriodFrom { get; set; }
            public DateTime APeriodTo { get; set; }
            public string ATaxTy { get; set; }
            public string ACurr { get; set; }
            public string AAssnmtAmt { get; set; }
            public string ARevAmt { get; set; }
            public string ADisputeAmt { get; set; }
            public string ARetDet { get; set; }
        }

        public class ZobjItemsSet
        {
            public List<Result2> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string AComments { get; set; }
            public string UserTin { get; set; }
            public string AErrorFg { get; set; }
            public string Auditorz { get; set; }
            public string Taxpayerz { get; set; }
            public string RegIdz { get; set; }
            public string PeriodKeyz { get; set; }
            public string Submitz { get; set; }
            public string Savez { get; set; }
            public string Fbnumz { get; set; }
            public string Langz { get; set; }
            public string PortalUsrz { get; set; }
            public string Monthz { get; set; }
            public string OfficerUidz { get; set; }
            public string Approvez { get; set; }
            public string Rejectz { get; set; }
            public string CreateTxAssesz { get; set; }
            public string Xvoidz { get; set; }
            public string AmdRsnz { get; set; }
            public string Mandt { get; set; }
            public string LegacyDocNo { get; set; }
            public string ABranch { get; set; }
            public string ABranchCd { get; set; }
            public string AReceiveBy { get; set; }
            public DateTime AReceiveDt { get; set; }
            public string AAppBy { get; set; }
            public object AAppDt { get; set; }
            public string AFbnum { get; set; }
            public string AGpart { get; set; }
            public string ADoc1 { get; set; }
            public string ADoc2 { get; set; }
            public string ADoc3 { get; set; }
            public string ADocOther { get; set; }
            public string ARemark { get; set; }
            public string AObjFbnum { get; set; }
            public string ATpName { get; set; }
            public string ACheck { get; set; }
            public string AGpart1 { get; set; }
            public string FormGuid { get; set; }
            public string Fbnum { get; set; }
            public string Status { get; set; }
            public string AAgree { get; set; }
            public int AStep { get; set; }
            public object AAgreeDt { get; set; }
            public string AAgreeTm { get; set; }
            public string CaseGuid { get; set; }
            public string Textnote { get; set; }
            public AttDetSet AttDetSet { get; set; }
            public ZnotesSet znotesSet { get; set; }
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }
    }

    public class ZakatBankListModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Result
        {
            public Metadata __metadata { get; set; }
            public string Bankid { get; set; }
            public string Langu { get; set; }
            public string Bkext { get; set; }
        }

        public class D
        {
            public List<Result> results { get; set; }
        }
    }

    public class ZakathDashboardModel
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
            public string Fbnum { get; set; }
            public string Objstatus { get; set; }
            public string Fbsta { get; set; }
            public string StatText { get; set; }
            public string Fbtyp { get; set; }
            public string FbtText { get; set; }
            public DateTime Erfdate { get; set; }
            public string Erftime { get; set; }
            public string Persl { get; set; }
            public string TaxPeriod { get; set; }
            public object DueDt { get; set; }
            public string Due { get; set; }
            public object Abrzu { get; set; }
            public object Abrzo { get; set; }
            public string Incotyp { get; set; }
            public string Incotext { get; set; }
            public string Flag { get; set; }
            public string CalendrTyp { get; set; }
            public string PrcBy { get; set; }
            public string Grp { get; set; }
            public string CrdtText { get; set; }
            public string Euser { get; set; }
            public string Fbguid { get; set; }
        }

        public class ListSet
        {
            public List<Result> results { get; set; }
        }

        public class Deferred
        {
            public string uri { get; set; }
        }

        public class AuthServSet
        {
            public Deferred __deferred { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string CallServ { get; set; }
            public int Accnum { get; set; }
            public int Actcnt { get; set; }
            public string Auditor { get; set; }
            public bool AudObjection { get; set; }
            public bool AudRefund { get; set; }
            public bool AudRefundTrn { get; set; }
            public bool AudRequest { get; set; }
            public bool AudReturn { get; set; }
            public string Bpnum { get; set; }
            public string Branch { get; set; }
            public string Caltype { get; set; }
            public string Client { get; set; }
            public int Cnlcnt { get; set; }
            public int Corrnum { get; set; }
            public string Dept { get; set; }
            public bool DisSharetile { get; set; }
            public bool EnableInstPlan { get; set; }
            public bool EnableTile { get; set; }
            public string Ettr { get; set; }
            public string Euser { get; set; }
            public string Euser1 { get; set; }
            public string Euser2 { get; set; }
            public string Euser3 { get; set; }
            public string Euser4 { get; set; }
            public string Euser5 { get; set; }
            public string ExeAppFlg { get; set; }
            public string ExeDtFlg { get; set; }
            public string Fbguid { get; set; }
            public string HostName { get; set; }
            public int Indcorrnum { get; set; }
            public string Interest { get; set; }
            public string IntPortal { get; set; }
            public bool IsBankruptcy { get; set; }
            public string Lang { get; set; }
            public string Name { get; set; }
            public bool NotifLogFlag { get; set; }
            public string NregDtFlg { get; set; }
            public int Oblnum { get; set; }
            public string Overdue { get; set; }
            public string Penalty { get; set; }
            public string PortNo { get; set; }
            public string Protocol { get; set; }
            public int Refnum { get; set; }
            public int Regnum { get; set; }
            public int Rencnt { get; set; }
            public int Reqnum { get; set; }
            public int RetItCnt { get; set; }
            public string RetItFlg { get; set; }
            public string SystemName { get; set; }
            public string Taxtype { get; set; }
            public bool TileOutlet { get; set; }
            public bool TilePermit { get; set; }
            public bool TileTin { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
            public bool UpdregOutflag { get; set; }
            public string VatConfFlg { get; set; }
            public string VatDtFlg { get; set; }
            public string VtepFg { get; set; }
            public string VtiaSignFg { get; set; }
            public string WarDtFlg { get; set; }
            public int Zfillingoblig { get; set; }
            public string Zregstatus { get; set; }
            public string Zuser { get; set; }
            public ListSet ListSet { get; set; }
            public AuthServSet AuthServSet { get; set; }
        }

    }


    public class ZAKATObjectionCreateNewModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public object Abrzo { get; set; }
            public object Abrzu { get; set; }
            public object Aedat { get; set; }
            public bool AudFlag { get; set; }
            public string Auditor { get; set; }
            public object Begda { get; set; }
            public string Branch { get; set; }
            public string CalendrTyp { get; set; }
            public string Comb { get; set; }
            public string Dispflag { get; set; }
            public string Due { get; set; }
            public object DueDt { get; set; }
            public string DueDtC { get; set; }
            public object Endda { get; set; }
            public string Euser { get; set; }
            public string Euser1 { get; set; }
            public string Euser2 { get; set; }
            public string Euser3 { get; set; }
            public string Euser4 { get; set; }
            public string Euser5 { get; set; }
            public string Fbguid { get; set; }
            public string Fbnum { get; set; }
            public string FbtText { get; set; }
            public string Fbtyp { get; set; }
            public string Flag { get; set; }
            public string Gpart { get; set; }
            public string Incotext { get; set; }
            public string Incotyp { get; set; }
            public string InfoMsg { get; set; }
            public string Lang { get; set; }
            public string Monthz { get; set; }
            public string Msg { get; set; }
            public bool ObjFiled { get; set; }
            public string ObligFlag { get; set; }
            public bool Open { get; set; }
            public string Period { get; set; }
            public string Persl { get; set; }
            public bool RefundFiled { get; set; }
            public string SadadDoc1 { get; set; }
            public string SadadDoc2 { get; set; }
            public string Stat { get; set; }
            public string Statflag { get; set; }
            public string Status { get; set; }
            public string TaxPeriod { get; set; }
            public string Vtref { get; set; }
        }
    }

    public class ZakatObjectionWDDropdownModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Result
        {
            public Metadata __metadata { get; set; }
            public string ObjFbnum { get; set; }
            public string FlagDis { get; set; }
            public string ASel { get; set; }
            public string ARefNo { get; set; }
            public string AAssnmtYr { get; set; }
            public string APeriodFrom { get; set; }
            public string APeriodTo { get; set; }
            public string ATaxTy { get; set; }
            public string ACurr { get; set; }
            public string AAssnmtAmt { get; set; }
            public string ARevAmt { get; set; }
            public string ADisputeAmt { get; set; }
            public string ARetDet { get; set; }
        }

        public class D
        {
            public List<Result> results { get; set; }
        }

    }

    public class ZakatObjectionWithDrawListModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Result
        {
            public Metadata __metadata { get; set; }
            public string Taxpy { get; set; }
            public string ObjFbnum { get; set; }
        }

        public class D
        {
            public List<Result> results { get; set; }
        }

    }

    public class ZakatWithdrawMainDataModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class AttDetSet
        {
            public List<object> results { get; set; }
        }

        public class ZnotesSet
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
            public string ASel { get; set; }
            public string ARefNo { get; set; }
            public string AAssnmtYr { get; set; }
            public object APeriodFrom { get; set; }
            public object APeriodTo { get; set; }
            public string ATaxTy { get; set; }
            public string ACurr { get; set; }
            public string AAssnmtAmt { get; set; }
            public string ARevAmt { get; set; }
            public string ADisputeAmt { get; set; }
            public string ARetDet { get; set; }
        }

        public class ZobjItemsSet
        {
            public List<Result> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string AComments { get; set; }
            public string UserTin { get; set; }
            public string AErrorFg { get; set; }
            public string Auditorz { get; set; }
            public string Taxpayerz { get; set; }
            public string RegIdz { get; set; }
            public string PeriodKeyz { get; set; }
            public string Submitz { get; set; }
            public string Savez { get; set; }
            public string Fbnumz { get; set; }
            public string Langz { get; set; }
            public string PortalUsrz { get; set; }
            public string Monthz { get; set; }
            public string OfficerUidz { get; set; }
            public string Approvez { get; set; }
            public string Rejectz { get; set; }
            public string CreateTxAssesz { get; set; }
            public string Xvoidz { get; set; }
            public string AmdRsnz { get; set; }
            public string Mandt { get; set; }
            public string LegacyDocNo { get; set; }
            public string ABranch { get; set; }
            public string ABranchCd { get; set; }
            public string AReceiveBy { get; set; }
            public DateTime AReceiveDt { get; set; }
            public string AAppBy { get; set; }
            public object AAppDt { get; set; }
            public string AFbnum { get; set; }
            public string AGpart { get; set; }
            public string ADoc1 { get; set; }
            public string ADoc2 { get; set; }
            public string ADoc3 { get; set; }
            public string ADocOther { get; set; }
            public string ARemark { get; set; }
            public string AObjFbnum { get; set; }
            public string ATpName { get; set; }
            public string ACheck { get; set; }
            public string AGpart1 { get; set; }
            public string FormGuid { get; set; }
            public string Fbnum { get; set; }
            public string Status { get; set; }
            public string AAgree { get; set; }
            public int AStep { get; set; }
            public object AAgreeDt { get; set; }
            public string AAgreeTm { get; set; }
            public string CaseGuid { get; set; }
            public string Textnote { get; set; }
            public AttDetSet AttDetSet { get; set; }
            public ZnotesSet znotesSet { get; set; }
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }

    }

    public class ZAKATObjectionAmendReturnAndCloseModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string TotCit { get; set; }
            public string Fbnum { get; set; }
            public string TotZkt { get; set; }
            public string TpTyp { get; set; }
            public string Zdisam { get; set; }
            public string Zundisam { get; set; }
            public string Citdisam { get; set; }
            public string Secamt { get; set; }
            public string Citundisam { get; set; }
            public string RevAmt { get; set; }
            public string Sectp { get; set; }
            public string Betrw { get; set; }
        }
    }

    public class ZAKATObjectionApplicationDetailsIfStatusIP017Model
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string Fbnum { get; set; }
            public string Euser { get; set; }
            public string Taxpayer { get; set; }
            public string Fbguid { get; set; }
            public string Account { get; set; }
            public string RegId { get; set; }
            public string Fbtyp { get; set; }
            public string PeriodKey { get; set; }
        }
    }

    public class ZAKATObjectionBusyIndicatorModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string Partner { get; set; }
            public string Flg { get; set; }
        }
    }

    public class ZAKATObjectionDetailsByReferenceNumberModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string Acr121gldtfg { get; set; }
            public string Secamt { get; set; }
            public string CalTyp { get; set; }
            public string Fbnums { get; set; }
            public string Fbguid { get; set; }
            public string Fbtyps { get; set; }
            public string Taxpayers { get; set; }
            public string Fbnum { get; set; }
            public string RegIds { get; set; }
            public string PeriodKeys { get; set; }
            public string Persl { get; set; }
            public DateTime Abrzu { get; set; }
            public DateTime Abrzo { get; set; }
            public string AbtypPs { get; set; }
            public string Waers { get; set; }
            public string Betrw { get; set; }
            public string Aud { get; set; }
            public string Sectp { get; set; }
            public string Euser { get; set; }
            public string Taxpayerz { get; set; }
            public string Fbnumz { get; set; }
        }
    }

    public class ZAKATObjectionDetailsToAmendReturnModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string Fbnum { get; set; }
            public string Refnum { get; set; }
        }
    }
    public class ZAKATObjectionGenerateSADADNumberModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string SopbelCit { get; set; }
            public string Fbnum { get; set; }
            public string Sopbel { get; set; }
            public string Sadadid { get; set; }
        }
    }

    public class ZAKATObjectionOnPaymentMethodSelectionModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string Fbnum { get; set; }
            public string Zundisam { get; set; }
            public string TotCit { get; set; }
            public string Zdisam { get; set; }
            public string TpTyp { get; set; }
            public string Revam { get; set; }
            public string Citdisam { get; set; }
            public string TotZkt { get; set; }
            public string Disam { get; set; }
            public string Citundisam { get; set; }
            public string Sectp { get; set; }
            public string Secamt { get; set; }
        }
    }

    public class ZakatObjectionWithdrawPostModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
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

        public class ZnotesSet
        {
            public Metadata2 __metadata { get; set; }
            public string Notenoz { get; set; }
            public string Refnamez { get; set; }
            public string XInvoicez { get; set; }
            public string XObsoletez { get; set; }
            public string Rcodez { get; set; }
            public string Erfusrz { get; set; }
            public object Erfdtz { get; set; }
            public object Erftmz { get; set; }
            public string AttByz { get; set; }
            public string Noteno { get; set; }
            public int Lineno { get; set; }
            public int ElemNo { get; set; }
            public string Tdformat { get; set; }
            public string Tdline { get; set; }
        }

        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class ZobjItemsSet
        {
            //public Metadata3 __metadata { get; set; }
            public string ASel { get; set; }
            public string ARefNo { get; set; }
            public string AAssnmtYr { get; set; }
            public string APeriodFrom { get; set; }
            public string APeriodTo { get; set; }
            public string ATaxTy { get; set; }
            public string ACurr { get; set; }
            public string AAssnmtAmt { get; set; }
            public string ARevAmt { get; set; }
            public string ADisputeAmt { get; set; }
            public string ARetDet { get; set; }
        }

        public class Root
        {
            public Metadata __metadata { get; set; }
            public string AComments { get; set; }
            public string UserTin { get; set; }
            public string AErrorFg { get; set; }
            public string Auditorz { get; set; }
            public string Taxpayerz { get; set; }
            public string RegIdz { get; set; }
            public string PeriodKeyz { get; set; }
            public string Submitz { get; set; }
            public string Savez { get; set; }
            public string Fbnumz { get; set; }
            public string Langz { get; set; }
            public string PortalUsrz { get; set; }
            public string Monthz { get; set; }
            public string OfficerUidz { get; set; }
            public string Approvez { get; set; }
            public string Rejectz { get; set; }
            public string CreateTxAssesz { get; set; }
            public string Xvoidz { get; set; }
            public string AmdRsnz { get; set; }
            public string Mandt { get; set; }
            public string LegacyDocNo { get; set; }
            public string ABranch { get; set; }
            public string ABranchCd { get; set; }
            public string AReceiveBy { get; set; }
            public string AReceiveDt { get; set; }
            public string AAppBy { get; set; }
            public object AAppDt { get; set; }
            public string AFbnum { get; set; }
            public string AGpart { get; set; }
            public string ADoc1 { get; set; }
            public string ADoc2 { get; set; }
            public string ADoc3 { get; set; }
            public string ADocOther { get; set; }
            public string ARemark { get; set; }
            public string AObjFbnum { get; set; }
            public string ATpName { get; set; }
            public string ACheck { get; set; }
            public string AGpart1 { get; set; }
            public string FormGuid { get; set; }
            public string Fbnum { get; set; }
            public string Status { get; set; }
            public string AAgree { get; set; }
            public int AStep { get; set; }
            public object AAgreeDt { get; set; }
            public string AAgreeTm { get; set; }
            public string CaseGuid { get; set; }
            public string Textnote { get; set; }
            public List<object> AttDetSet { get; set; }
            public List<ZnotesSet> znotesSet { get; set; }
            public List<ZobjItemsSet> zobj_itemsSet { get; set; }
        }


    }

    public class ZakatObjectionWithdrawPostResponceModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class AttDetSet
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
            public string Notenoz { get; set; }
            public string Refnamez { get; set; }
            public string XInvoicez { get; set; }
            public string XObsoletez { get; set; }
            public string Rcodez { get; set; }
            public string Erfusrz { get; set; }
            public object Erfdtz { get; set; }
            public string Erftmz { get; set; }
            public string AttByz { get; set; }
            public string Noteno { get; set; }
            public int Lineno { get; set; }
            public int ElemNo { get; set; }
            public string Tdformat { get; set; }
            public string Tdline { get; set; }
        }

        public class ZnotesSet
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
            public string ASel { get; set; }
            public string ARefNo { get; set; }
            public string AAssnmtYr { get; set; }
            public DateTime? APeriodFrom { get; set; }
            public DateTime? APeriodTo { get; set; }
            public string ATaxTy { get; set; }
            public string ACurr { get; set; }
            public string AAssnmtAmt { get; set; }
            public string ARevAmt { get; set; }
            public string ADisputeAmt { get; set; }
            public string ARetDet { get; set; }
        }

        public class ZobjItemsSet
        {
            public List<Result2> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string AComments { get; set; }
            public string UserTin { get; set; }
            public string AErrorFg { get; set; }
            public string Auditorz { get; set; }
            public string Taxpayerz { get; set; }
            public string RegIdz { get; set; }
            public string PeriodKeyz { get; set; }
            public string Submitz { get; set; }
            public string Savez { get; set; }
            public string Fbnumz { get; set; }
            public string Langz { get; set; }
            public string PortalUsrz { get; set; }
            public string Monthz { get; set; }
            public string OfficerUidz { get; set; }
            public string Approvez { get; set; }
            public string Rejectz { get; set; }
            public string CreateTxAssesz { get; set; }
            public string Xvoidz { get; set; }
            public string AmdRsnz { get; set; }
            public string Mandt { get; set; }
            public string LegacyDocNo { get; set; }
            public string ABranch { get; set; }
            public string ABranchCd { get; set; }
            public string AReceiveBy { get; set; }
            public DateTime? AReceiveDt { get; set; }
            public string AAppBy { get; set; }
            public object AAppDt { get; set; }
            public string AFbnum { get; set; }
            public string AGpart { get; set; }
            public string ADoc1 { get; set; }
            public string ADoc2 { get; set; }
            public string ADoc3 { get; set; }
            public string ADocOther { get; set; }
            public string ARemark { get; set; }
            public string AObjFbnum { get; set; }
            public string ATpName { get; set; }
            public string ACheck { get; set; }
            public string AGpart1 { get; set; }
            public string FormGuid { get; set; }
            public string Fbnum { get; set; }
            public string Status { get; set; }
            public string AAgree { get; set; }
            public int AStep { get; set; }
            public DateTime? AAgreeDt { get; set; }
            public string AAgreeTm { get; set; }
            public string CaseGuid { get; set; }
            public string Textnote { get; set; }
            public AttDetSet AttDetSet { get; set; }
            public ZnotesSet znotesSet { get; set; }
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }

    }

    public class ZakatObjectionSummaryModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class AttDetSet
        {
            public List<object> results { get; set; }
        }

        public class ZnotesSet
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
            public string ASel { get; set; }
            public string ARefNo { get; set; }
            public string AAssnmtYr { get; set; }
            public DateTime? APeriodFrom { get; set; }
            public DateTime? APeriodTo { get; set; }
            public string ATaxTy { get; set; }
            public string ACurr { get; set; }
            public string AAssnmtAmt { get; set; }
            public string ARevAmt { get; set; }
            public string ADisputeAmt { get; set; }
            public string ARetDet { get; set; }
        }

        public class ZobjItemsSet
        {
            public List<Result> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string AComments { get; set; }
            public string UserTin { get; set; }
            public string AErrorFg { get; set; }
            public string Auditorz { get; set; }
            public string Taxpayerz { get; set; }
            public string RegIdz { get; set; }
            public string PeriodKeyz { get; set; }
            public string Submitz { get; set; }
            public string Savez { get; set; }
            public string Fbnumz { get; set; }
            public string Langz { get; set; }
            public string PortalUsrz { get; set; }
            public string Monthz { get; set; }
            public string OfficerUidz { get; set; }
            public string Approvez { get; set; }
            public string Rejectz { get; set; }
            public string CreateTxAssesz { get; set; }
            public string Xvoidz { get; set; }
            public string AmdRsnz { get; set; }
            public string Mandt { get; set; }
            public string LegacyDocNo { get; set; }
            public string ABranch { get; set; }
            public string ABranchCd { get; set; }
            public string AReceiveBy { get; set; }
            public DateTime? AReceiveDt { get; set; }
            public string AAppBy { get; set; }
            public DateTime? AAppDt { get; set; }
            public string AFbnum { get; set; }
            public string AGpart { get; set; }
            public string ADoc1 { get; set; }
            public string ADoc2 { get; set; }
            public string ADoc3 { get; set; }
            public string ADocOther { get; set; }
            public string ARemark { get; set; }
            public string AObjFbnum { get; set; }
            public string ATpName { get; set; }
            public string ACheck { get; set; }
            public string AGpart1 { get; set; }
            public string FormGuid { get; set; }
            public string Fbnum { get; set; }
            public string Status { get; set; }
            public string AAgree { get; set; }
            public int AStep { get; set; }
            public DateTime? AAgreeDt { get; set; }
            public string AAgreeTm { get; set; }
            public string CaseGuid { get; set; }
            public string Textnote { get; set; }
            public AttDetSet AttDetSet { get; set; }
            public ZnotesSet znotesSet { get; set; }
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }






    }
}
