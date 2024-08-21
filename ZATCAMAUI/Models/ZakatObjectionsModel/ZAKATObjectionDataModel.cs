using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ZATCAMAUI.Models.ZakatObjectionsModel
{
   
    public class ZAKATObjectionDataModel
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
            public string RetGuid { get; set; }
            [DataMember]
            public string DocUrl { get; set; }
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
            public DateTime Erfdt { get; set; }
            [DataMember]
            public string Erftm { get; set; }
            [DataMember]
            public string Enbedit { get; set; }
            [DataMember]
            public string Enbdele { get; set; }
            [DataMember]
            public string Visedit { get; set; }
            [DataMember]
            public string Visdel { get; set; }
        }

       
        public class AttDetSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }

       
        public class ZnotesSet
        {
            [DataMember]
            public List<object> results { get; set; }
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
            public string ASel { get; set; }
            [DataMember]
            public string ARefNo { get; set; }
            [DataMember]
            public string AAssnmtYr { get; set; }
            [DataMember]
            public DateTime APeriodFrom { get; set; }
            [DataMember]
            public DateTime APeriodTo { get; set; }
            [DataMember]
            public string ATaxTy { get; set; }
            [DataMember]
            public string ACurr { get; set; }
            [DataMember]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            public string ARevAmt { get; set; }
            [DataMember]
            public string ADisputeAmt { get; set; }
            [DataMember]
            public string ARetDet { get; set; }
        }

       
        public class ZobjItemsSet
        {
            [DataMember]
            public List<Result2> results { get; set; }
        }

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string AComments { get; set; }
            [DataMember]
            public string UserTin { get; set; }
            [DataMember]
            public string AErrorFg { get; set; }
            [DataMember]
            public string Auditorz { get; set; }
            [DataMember]
            public string Taxpayerz { get; set; }
            [DataMember]
            public string RegIdz { get; set; }
            [DataMember]
            public string PeriodKeyz { get; set; }
            [DataMember]
            public string Submitz { get; set; }
            [DataMember]
            public string Savez { get; set; }
            [DataMember]
            public string Fbnumz { get; set; }
            [DataMember]
            public string Langz { get; set; }
            [DataMember]
            public string PortalUsrz { get; set; }
            [DataMember]
            public string Monthz { get; set; }
            [DataMember]
            public string OfficerUidz { get; set; }
            [DataMember]
            public string Approvez { get; set; }
            [DataMember]
            public string Rejectz { get; set; }
            [DataMember]
            public string CreateTxAssesz { get; set; }
            [DataMember]
            public string Xvoidz { get; set; }
            [DataMember]
            public string AmdRsnz { get; set; }
            [DataMember]
            public string Mandt { get; set; }
            [DataMember]
            public string LegacyDocNo { get; set; }
            [DataMember]
            public string ABranch { get; set; }
            [DataMember]
            public string ABranchCd { get; set; }
            [DataMember]
            public string AReceiveBy { get; set; }
            [DataMember]
            public DateTime AReceiveDt { get; set; }
            [DataMember]
            public string AAppBy { get; set; }
            [DataMember]
            public object AAppDt { get; set; }
            [DataMember]
            public string AFbnum { get; set; }
            [DataMember]
            public string AGpart { get; set; }
            [DataMember]
            public string ADoc1 { get; set; }
            [DataMember]
            public string ADoc2 { get; set; }
            [DataMember]
            public string ADoc3 { get; set; }
            [DataMember]
            public string ADocOther { get; set; }
            [DataMember]
            public string ARemark { get; set; }
            [DataMember]
            public string AObjFbnum { get; set; }
            [DataMember]
            public string ATpName { get; set; }
            [DataMember]
            public string ACheck { get; set; }
            [DataMember]
            public string AGpart1 { get; set; }
            [DataMember]
            public string FormGuid { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string AAgree { get; set; }
            [DataMember]
            public int AStep { get; set; }
            [DataMember]
            public object AAgreeDt { get; set; }
            [DataMember]
            public string AAgreeTm { get; set; }
            [DataMember]
            public string CaseGuid { get; set; }
            [DataMember]
            public string Textnote { get; set; }
            [DataMember]
            public AttDetSet AttDetSet { get; set; }
            [DataMember]
            public ZnotesSet znotesSet { get; set; }
            [DataMember]
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }
    }

   
    public class ZakatBankListModel
    {
        [JsonProperty("data")]
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

       
        public class Result
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string bankId { get; set; }
            [DataMember]
            public string language { get; set; }
            [DataMember]
            public string bankDescription { get; set; }
        }
        
        public class data
        {
            [DataMember]
            public List<Result> banks { get; set; }
        }
    }

    public class ZakathDashboardModel
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
            public string Fbnum { get; set; }
            [DataMember]
            public string Objstatus { get; set; }
            [DataMember]
            public string Fbsta { get; set; }
            [DataMember]
            public string StatText { get; set; }
            [DataMember]
            public string Fbtyp { get; set; }
            [DataMember]
            public string FbtText { get; set; }
            [DataMember]
            public DateTime Erfdate { get; set; }
            [DataMember]
            public string Erftime { get; set; }
            [DataMember]
            public string Persl { get; set; }
            [DataMember]
            public string TaxPeriod { get; set; }
            [DataMember]
            public object DueDt { get; set; }
            [DataMember]
            public string Due { get; set; }
            [DataMember]
            public object Abrzu { get; set; }
            [DataMember]
            public object Abrzo { get; set; }
            [DataMember]
            public string Incotyp { get; set; }
            [DataMember]
            public string Incotext { get; set; }
            [DataMember]
            public string Flag { get; set; }
            [DataMember]
            public string CalendrTyp { get; set; }
            [DataMember]
            public string PrcBy { get; set; }
            [DataMember]
            public string Grp { get; set; }
            [DataMember]
            public string CrdtText { get; set; }
            [DataMember]
            public string Euser { get; set; }
            [DataMember]
            public string Fbguid { get; set; }
        }

       
        public class ListSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }

       
        public class Deferred
        {
            [DataMember]
            public string uri { get; set; }
        }

       
        public class AuthServSet
        {
            [DataMember]
            public Deferred __deferred { get; set; }
        }

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string CallServ { get; set; }
            [DataMember]
            public int Accnum { get; set; }
            [DataMember]
            public int Actcnt { get; set; }
            [DataMember]
            public string Auditor { get; set; }
            [DataMember]
            public bool AudObjection { get; set; }
            [DataMember]
            public bool AudRefund { get; set; }
            [DataMember]
            public bool AudRefundTrn { get; set; }
            [DataMember]
            public bool AudRequest { get; set; }
            [DataMember]
            public bool AudReturn { get; set; }
            [DataMember]
            public string Bpnum { get; set; }
            [DataMember]
            public string Branch { get; set; }
            [DataMember]
            public string Caltype { get; set; }
            [DataMember]
            public string Client { get; set; }
            [DataMember]
            public int Cnlcnt { get; set; }
            [DataMember]
            public int Corrnum { get; set; }
            [DataMember]
            public string Dept { get; set; }
            [DataMember]
            public bool DisSharetile { get; set; }
            [DataMember]
            public bool EnableInstPlan { get; set; }
            [DataMember]
            public bool EnableTile { get; set; }
            [DataMember]
            public string Ettr { get; set; }
            [DataMember]
            public string Euser { get; set; }
            [DataMember]
            public string Euser1 { get; set; }
            [DataMember]
            public string Euser2 { get; set; }
            [DataMember]
            public string Euser3 { get; set; }
            [DataMember]
            public string Euser4 { get; set; }
            [DataMember]
            public string Euser5 { get; set; }
            [DataMember]
            public string ExeAppFlg { get; set; }
            [DataMember]
            public string ExeDtFlg { get; set; }
            [DataMember]
            public string Fbguid { get; set; }
            [DataMember]
            public string HostName { get; set; }
            [DataMember]
            public int Indcorrnum { get; set; }
            [DataMember]
            public string Interest { get; set; }
            [DataMember]
            public string IntPortal { get; set; }
            [DataMember]
            public bool IsBankruptcy { get; set; }
            [DataMember]
            public string Lang { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public bool NotifLogFlag { get; set; }
            [DataMember]
            public string NregDtFlg { get; set; }
            [DataMember]
            public int Oblnum { get; set; }
            [DataMember]
            public string Overdue { get; set; }
            [DataMember]
            public string Penalty { get; set; }
            [DataMember]
            public string PortNo { get; set; }
            [DataMember]
            public string Protocol { get; set; }
            [DataMember]
            public int Refnum { get; set; }
            [DataMember]
            public int Regnum { get; set; }
            [DataMember]
            public int Rencnt { get; set; }
            [DataMember]
            public int Reqnum { get; set; }
            [DataMember]
            public int RetItCnt { get; set; }
            [DataMember]
            public string RetItFlg { get; set; }
            [DataMember]
            public string SystemName { get; set; }
            [DataMember]
            public string Taxtype { get; set; }
            [DataMember]
            public bool TileOutlet { get; set; }
            [DataMember]
            public bool TilePermit { get; set; }
            [DataMember]
            public bool TileTin { get; set; }
            [DataMember]
            public string Title { get; set; }
            [DataMember]
            public string Type { get; set; }
            [DataMember]
            public bool UpdregOutflag { get; set; }
            [DataMember]
            public string VatConfFlg { get; set; }
            [DataMember]
            public string VatDtFlg { get; set; }
            [DataMember]
            public string VtepFg { get; set; }
            [DataMember]
            public string VtiaSignFg { get; set; }
            [DataMember]
            public string WarDtFlg { get; set; }
            [DataMember]
            public int Zfillingoblig { get; set; }
            [DataMember]
            public string Zregstatus { get; set; }
            [DataMember]
            public string Zuser { get; set; }
            [DataMember]
            public ListSet ListSet { get; set; }
            [DataMember]
            public AuthServSet AuthServSet { get; set; }
        }

    }


   
    public class ZAKATObjectionCreateNewModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        [JsonProperty("data")]
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public object Abrzo { get; set; }
            [DataMember]
            public object Abrzu { get; set; }
            [DataMember]
            public object Aedat { get; set; }
            [DataMember]
            [JsonProperty("auditor")]
            public bool AudFlag { get; set; }
            [DataMember]
            [JsonProperty("auditor")]
            public string Auditor { get; set; }
            [DataMember]
            public object Begda { get; set; }
            [DataMember]
            [JsonProperty("branch")]
            public string Branch { get; set; }
            [DataMember]
            public string CalendrTyp { get; set; }
            [DataMember]
            [JsonProperty("combination")]
            public string Comb { get; set; }
            [DataMember]
            [JsonProperty("display")]
            public string Dispflag { get; set; }
            [DataMember]
            [JsonProperty("dueStatus")]
            public string Due { get; set; }
            [DataMember]
            public object DueDt { get; set; }
            [DataMember]
            [JsonProperty("dueDateCharacter")]
            public string DueDtC { get; set; }
            [DataMember]
            public object Endda { get; set; }
            [DataMember]
            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }
            [DataMember]
            [JsonProperty("authenticationUser1")]
            public string Euser1 { get; set; }
            [DataMember]
            [JsonProperty("authenticationUser2")]
            public string Euser2 { get; set; }
            [DataMember]
            [JsonProperty("authenticationUser3")]
            public string Euser3 { get; set; }
            [DataMember]
            [JsonProperty("authenticationUser4")]
            public string Euser4 { get; set; }
            [DataMember]
            [JsonProperty("authenticationUser5")]
            public string Euser5 { get; set; }
            [DataMember]
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [DataMember]
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [DataMember]
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [DataMember]
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [DataMember]
            [JsonProperty("selected")]
            public string Flag { get; set; }
            [DataMember]
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [DataMember]
            [JsonProperty("inboundCorrespondenceDescription")]
            public string Incotext { get; set; }
            [DataMember]
            [JsonProperty("inboundCorrespondenceType")]
            public string Incotyp { get; set; }
            [DataMember]
            [JsonProperty("informationMessage")]
            public string InfoMsg { get; set; }
            [DataMember]
            [JsonProperty("language")]
            public string Lang { get; set; }
            [DataMember]
            [JsonProperty("month")]
            public string Monthz { get; set; }
            [DataMember]
            [JsonProperty("errorMessage")]
            public string Msg { get; set; }
            [DataMember]
            [JsonProperty("isObjectionFiled")]
            public bool ObjFiled { get; set; }
            [DataMember]
            [JsonProperty("obligation")]
            public string ObligFlag { get; set; }
            [DataMember]
            [JsonProperty("isOpen")]
            public bool Open { get; set; }
            [DataMember]
            [JsonProperty("period")]
            public string Period { get; set; }
            [DataMember]
            [JsonProperty("periodkey")]
            public string Persl { get; set; }
            [DataMember]
            [JsonProperty("refundFiled")]
            public bool RefundFiled { get; set; }
            [DataMember]
            [JsonProperty("sadadBillNumber1")]
            public string SadadDoc1 { get; set; }
            [DataMember]
            [JsonProperty("sadadBillNumber2")]
            public string SadadDoc2 { get; set; }
            [DataMember]
            [JsonProperty("statusDescription")]
            public string Stat { get; set; }
            [DataMember]
            [JsonProperty("dueStatus")]
            public string Statflag { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            [JsonProperty("taxPeriod")]
            public string TaxPeriod { get; set; }
            [DataMember]
            [JsonProperty("contractNumber")]
            public string Vtref { get; set; }
        }
    }

   
    public class ZakatObjectionWDDropdownModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        [JsonProperty("data")]
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

       
        public class Result
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            [JsonProperty("FormBundleNumber")]
            public string ObjFbnum { get; set; }
            [DataMember]
            [JsonProperty("dispute")]
            public string FlagDis { get; set; }
            [DataMember]
            [JsonProperty("selectedRow")]
            public string ASel { get; set; }
            [DataMember]
            [JsonProperty("referenceAssessment")]
            public string ARefNo { get; set; }
            [DataMember]
            [JsonProperty("taxYear")]
            public string AAssnmtYr { get; set; }
            [DataMember]
            [JsonProperty("periodFrom")]
            public string APeriodFrom { get; set; }
            [DataMember]
            [JsonProperty("periodTo")]
            public string APeriodTo { get; set; }
            [DataMember]
            [JsonProperty("taxType")]
            public string ATaxTy { get; set; }
            [DataMember]
            [JsonProperty("currency")]
            public string ACurr { get; set; }
            [DataMember]
            [JsonProperty("assessmentAmount")]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            [JsonProperty("revisedAmount")]
            public string ARevAmt { get; set; }
            [DataMember]
            [JsonProperty("disputeAmount")]
            public string ADisputeAmt { get; set; }
            [DataMember]
            [JsonProperty("returnDetail")]
            public string ARetDet { get; set; }
        }

       
        public class D
        {
            [JsonProperty("withdrawalDetails")]
            public List<Result> results { get; set; }
        }

    }
   
    public class ZakatObjectionWithDrawListModel
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
       
        public class Result
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            [JsonProperty("TIN")]
            public string Taxpy { get; set; }
            [DataMember]
            [JsonProperty("objectionFormBundleNumber")]
            public string ObjFbnum { get; set; }
        }
        [Serializable]
        [DataContract]
        
        public class ZakatObjectionWithDrawListModelClass
        {
            [DataMember]
            [JsonProperty("data")]
            public List<Result> results { get; set; }
        }

    }

    public class ZakatWithdrawMainDataModel
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

       
        public class AttDetSet
        {
            [DataMember]
            public List<object> results { get; set; }
        }

       
        public class ZnotesSet
        {
            [DataMember]
            public List<object> results { get; set; }
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
            public string ASel { get; set; }
            [DataMember]
            public string ARefNo { get; set; }
            [DataMember]
            public string AAssnmtYr { get; set; }
            [DataMember]
            public object APeriodFrom { get; set; }
            [DataMember]
            public object APeriodTo { get; set; }
            [DataMember]
            public string ATaxTy { get; set; }
            [DataMember]
            public string ACurr { get; set; }
            [DataMember]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            public string ARevAmt { get; set; }
            [DataMember]
            public string ADisputeAmt { get; set; }
            [DataMember]
            public string ARetDet { get; set; }
        }

       
        public class ZobjItemsSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string AComments { get; set; }
            [DataMember]
            public string UserTin { get; set; }
            [DataMember]
            public string AErrorFg { get; set; }
            [DataMember]
            public string Auditorz { get; set; }
            [DataMember]
            public string Taxpayerz { get; set; }
            [DataMember]
            public string RegIdz { get; set; }
            [DataMember]
            public string PeriodKeyz { get; set; }
            [DataMember]
            public string Submitz { get; set; }
            [DataMember]
            public string Savez { get; set; }
            [DataMember]
            public string Fbnumz { get; set; }
            [DataMember]
            public string Langz { get; set; }
            [DataMember]
            public string PortalUsrz { get; set; }
            [DataMember]
            public string Monthz { get; set; }
            [DataMember]
            public string OfficerUidz { get; set; }
            [DataMember]
            public string Approvez { get; set; }
            [DataMember]
            public string Rejectz { get; set; }
            [DataMember]
            public string CreateTxAssesz { get; set; }
            [DataMember]
            public string Xvoidz { get; set; }
            [DataMember]
            public string AmdRsnz { get; set; }
            [DataMember]
            public string Mandt { get; set; }
            [DataMember]
            public string LegacyDocNo { get; set; }
            [DataMember]
            public string ABranch { get; set; }
            [DataMember]
            public string ABranchCd { get; set; }
            [DataMember]
            public string AReceiveBy { get; set; }
            [DataMember]
            public DateTime AReceiveDt { get; set; }
            [DataMember]
            public string AAppBy { get; set; }
            [DataMember]
            public object AAppDt { get; set; }
            [DataMember]
            public string AFbnum { get; set; }
            [DataMember]
            public string AGpart { get; set; }
            [DataMember]
            public string ADoc1 { get; set; }
            [DataMember]
            public string ADoc2 { get; set; }
            [DataMember]
            public string ADoc3 { get; set; }
            [DataMember]
            public string ADocOther { get; set; }
            [DataMember]
            public string ARemark { get; set; }
            [DataMember]
            public string AObjFbnum { get; set; }
            [DataMember]
            public string ATpName { get; set; }
            [DataMember]
            public string ACheck { get; set; }
            [DataMember]
            public string AGpart1 { get; set; }
            [DataMember]
            public string FormGuid { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string AAgree { get; set; }
            [DataMember]
            public int AStep { get; set; }
            [DataMember]
            public object AAgreeDt { get; set; }
            [DataMember]
            public string AAgreeTm { get; set; }
            [DataMember]
            public string CaseGuid { get; set; }
            [DataMember]
            public string Textnote { get; set; }
            [DataMember]
            public AttDetSet AttDetSet { get; set; }
            [DataMember]
            public ZnotesSet znotesSet { get; set; }
            [DataMember]
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }

    }

   
    public class ZAKATObjectionAmendReturnAndCloseModel
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string TotCit { get; set; }

            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string TotZkt { get; set; }
            [DataMember]
            public string TpTyp { get; set; }
            [DataMember]
            public string Zdisam { get; set; }
            [DataMember]
            public string Zundisam { get; set; }
            [DataMember]
            public string Citdisam { get; set; }
            [DataMember]
            public string Secamt { get; set; }
            [DataMember]
            public string Citundisam { get; set; }
            [DataMember]
            public string RevAmt { get; set; }
            [DataMember]
            public string Sectp { get; set; }
            [DataMember]
            public string Betrw { get; set; }
        }
    }

   
    public class ZAKATObjectionApplicationDetailsIfStatusIP017Model
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Euser { get; set; }
            [DataMember]
            public string Taxpayer { get; set; }
            [DataMember]

            public string Fbguid { get; set; }
            [DataMember]

            public string Account { get; set; }
            [DataMember]

            public string RegId { get; set; }
            [DataMember]

            public string Fbtyp { get; set; }
            [DataMember]

            public string PeriodKey { get; set; }
        }
    }

   
    public class ZAKATObjectionBusyIndicatorModel
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string Partner { get; set; }
            [DataMember]
            public string Flg { get; set; }
        }
    }

   
    public class ZAKATObjectionDetailsByReferenceNumberModel
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string Acr121gldtfg { get; set; }
            [DataMember]
            public string Secamt { get; set; }
            [DataMember]
            public string CalTyp { get; set; }
            [DataMember]
            public string Fbnums { get; set; }
            [DataMember]
            public string Fbguid { get; set; }
            [DataMember]
            public string Fbtyps { get; set; }
            [DataMember]
            public string Taxpayers { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string RegIds { get; set; }
            [DataMember]
            public string PeriodKeys { get; set; }
            [DataMember]
            public string Persl { get; set; }
            [DataMember]
            public DateTime Abrzu { get; set; }
            [DataMember]
            public DateTime Abrzo { get; set; }
            [DataMember]
            public string AbtypPs { get; set; }
            [DataMember]
            public string Waers { get; set; }
            [DataMember]
            public string Betrw { get; set; }
            [DataMember]
            public string Aud { get; set; }
            [DataMember]
            public string Sectp { get; set; }
            [DataMember]
            public string Euser { get; set; }
            [DataMember]
            public string Taxpayerz { get; set; }
            [DataMember]
            public string Fbnumz { get; set; }
        }
    }

   
    public class ZAKATObjectionDetailsToAmendReturnModel
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Refnum { get; set; }
        }
    }
   
    public class ZAKATObjectionGenerateSADADNumberModel
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string SopbelCit { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Sopbel { get; set; }
            [DataMember]
            public string Sadadid { get; set; }
        }
    }

   
    public class ZAKATObjectionOnPaymentMethodSelectionModel
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

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Zundisam { get; set; }
            [DataMember]
            public string TotCit { get; set; }
            [DataMember]
            public string Zdisam { get; set; }
            [DataMember]
            public string TpTyp { get; set; }
            [DataMember]
            public string Revam { get; set; }
            [DataMember]
            public string Citdisam { get; set; }
            [DataMember]
            public string TotZkt { get; set; }
            [DataMember]
            public string Disam { get; set; }
            [DataMember]
            public string Citundisam { get; set; }
            [DataMember]
            public string Sectp { get; set; }
            [DataMember]
            public string Secamt { get; set; }
        }
    }

   
    public class ZakatObjectionWithdrawPostModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
       
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

       
        public class ZnotesSet
        {
            [DataMember]
            public Metadata2 __metadata { get; set; }
            [DataMember]
            public string Notenoz { get; set; }
            [DataMember]
            public string Refnamez { get; set; }
            [DataMember]
            public string XInvoicez { get; set; }
            [DataMember]
            public string XObsoletez { get; set; }
            [DataMember]
            public string Rcodez { get; set; }
            [DataMember]
            public string Erfusrz { get; set; }
            [DataMember]
            public object Erfdtz { get; set; }
            [DataMember]
            public object Erftmz { get; set; }
            [DataMember]
            public string AttByz { get; set; }
            [DataMember]
            public string Noteno { get; set; }
            [DataMember]
            public int Lineno { get; set; }
            [DataMember]
            public int ElemNo { get; set; }
            [DataMember]
            public string Tdformat { get; set; }
            [DataMember]
            public string Tdline { get; set; }
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

       
        public class ZobjItemsSet
        {
            //public Metadata3 __metadata { get; set; }
            [DataMember]
            public string ASel { get; set; }
            [DataMember]
            public string ARefNo { get; set; }
            [DataMember]
            public string AAssnmtYr { get; set; }
            [DataMember]
            public string APeriodFrom { get; set; }
            [DataMember]
            public string APeriodTo { get; set; }
            [DataMember]
            public string ATaxTy { get; set; }
            [DataMember]
            public string ACurr { get; set; }
            [DataMember]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            public string ARevAmt { get; set; }
            [DataMember]
            public string ADisputeAmt { get; set; }
            [DataMember]
            public string ARetDet { get; set; }
        }

       
        public class Root
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string AComments { get; set; }
            [DataMember]
            public string UserTin { get; set; }
            [DataMember]
            public string AErrorFg { get; set; }
            [DataMember]
            public string Auditorz { get; set; }
            [DataMember]
            public string Taxpayerz { get; set; }
            [DataMember]
            public string RegIdz { get; set; }
            [DataMember]
            public string PeriodKeyz { get; set; }
            [DataMember]
            public string Submitz { get; set; }
            [DataMember]
            public string Savez { get; set; }
            [DataMember]
            public string Fbnumz { get; set; }
            [DataMember]
            public string Langz { get; set; }
            [DataMember]
            public string PortalUsrz { get; set; }
            [DataMember]
            public string Monthz { get; set; }
            [DataMember]
            public string OfficerUidz { get; set; }
            [DataMember]
            public string Approvez { get; set; }
            [DataMember]
            public string Rejectz { get; set; }
            [DataMember]
            public string CreateTxAssesz { get; set; }
            [DataMember]
            public string Xvoidz { get; set; }
            [DataMember]
            public string AmdRsnz { get; set; }
            [DataMember]
            public string Mandt { get; set; }
            [DataMember]
            public string LegacyDocNo { get; set; }
            [DataMember]
            public string ABranch { get; set; }
            [DataMember]
            public string ABranchCd { get; set; }
            [DataMember]
            public string AReceiveBy { get; set; }
            [DataMember]
            public string AReceiveDt { get; set; }
            [DataMember]
            public string AAppBy { get; set; }
            [DataMember]
            public object AAppDt { get; set; }
            [DataMember]
            public string AFbnum { get; set; }
            [DataMember]
            public string AGpart { get; set; }
            [DataMember]
            public string ADoc1 { get; set; }
            [DataMember]
            public string ADoc2 { get; set; }
            [DataMember]
            public string ADoc3 { get; set; }
            [DataMember]
            public string ADocOther { get; set; }
            [DataMember]
            public string ARemark { get; set; }
            [DataMember]
            public string AObjFbnum { get; set; }
            [DataMember]
            public string ATpName { get; set; }
            [DataMember]
            public string ACheck { get; set; }
            [DataMember]
            public string AGpart1 { get; set; }
            [DataMember]
            public string FormGuid { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string AAgree { get; set; }
            [DataMember]
            public int AStep { get; set; }
            [DataMember]
            public object AAgreeDt { get; set; }
            [DataMember]
            public string AAgreeTm { get; set; }
            [DataMember]
            public string CaseGuid { get; set; }
            [DataMember]
            public string Textnote { get; set; }
            [DataMember]
            public List<object> AttDetSet { get; set; }
            [DataMember]
            public List<ZnotesSet> znotesSet { get; set; }
            [DataMember]
            public List<ZobjItemsSet> zobj_itemsSet { get; set; }
        }


    }

   
    public class ZakatObjectionWithdrawPostResponceModel
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

       
        public class AttDetSet
        {
            [DataMember]
            public List<object> results { get; set; }
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
            public string Notenoz { get; set; }
            [DataMember]
            public string Refnamez { get; set; }
            [DataMember]
            public string XInvoicez { get; set; }
            [DataMember]
            public string XObsoletez { get; set; }
            [DataMember]
            public string Rcodez { get; set; }
            [DataMember]
            public string Erfusrz { get; set; }
            [DataMember]
            public object Erfdtz { get; set; }
            [DataMember]
            public string Erftmz { get; set; }
            [DataMember]
            public string AttByz { get; set; }
            [DataMember]
            public string Noteno { get; set; }
            [DataMember]
            public int Lineno { get; set; }
            [DataMember]
            public int ElemNo { get; set; }
            [DataMember]
            public string Tdformat { get; set; }
            [DataMember]
            public string Tdline { get; set; }
        }

       
        public class ZnotesSet
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
            public string ASel { get; set; }
            [DataMember]
            public string ARefNo { get; set; }
            [DataMember]
            public string AAssnmtYr { get; set; }
            [DataMember]
            public DateTime? APeriodFrom { get; set; }
            [DataMember]
            public DateTime? APeriodTo { get; set; }
            [DataMember]
            public string ATaxTy { get; set; }
            [DataMember]
            public string ACurr { get; set; }
            [DataMember]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            public string ARevAmt { get; set; }
            [DataMember]
            public string ADisputeAmt { get; set; }
            [DataMember]
            public string ARetDet { get; set; }
        }

       
        public class ZobjItemsSet
        {
            [DataMember]
            public List<Result2> results { get; set; }
        }

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string AComments { get; set; }
            [DataMember]
            public string UserTin { get; set; }
            [DataMember]
            public string AErrorFg { get; set; }
            [DataMember]
            public string Auditorz { get; set; }
            [DataMember]
            public string Taxpayerz { get; set; }
            [DataMember]
            public string RegIdz { get; set; }
            [DataMember]
            public string PeriodKeyz { get; set; }
            [DataMember]
            public string Submitz { get; set; }
            [DataMember]
            public string Savez { get; set; }
            [DataMember]
            public string Fbnumz { get; set; }
            [DataMember]
            public string Langz { get; set; }
            [DataMember]
            public string PortalUsrz { get; set; }
            [DataMember]
            public string Monthz { get; set; }
            [DataMember]
            public string OfficerUidz { get; set; }
            [DataMember]
            public string Approvez { get; set; }
            [DataMember]
            public string Rejectz { get; set; }
            [DataMember]
            public string CreateTxAssesz { get; set; }
            [DataMember]
            public string Xvoidz { get; set; }
            [DataMember]
            public string AmdRsnz { get; set; }
            [DataMember]
            public string Mandt { get; set; }
            [DataMember]
            public string LegacyDocNo { get; set; }
            [DataMember]
            public string ABranch { get; set; }
            [DataMember]
            public string ABranchCd { get; set; }
            [DataMember]
            public string AReceiveBy { get; set; }
            [DataMember]
            public DateTime? AReceiveDt { get; set; }
            [DataMember]
            public string AAppBy { get; set; }
            [DataMember]
            public object AAppDt { get; set; }
            [DataMember]
            public string AFbnum { get; set; }
            [DataMember]
            public string AGpart { get; set; }
            [DataMember]
            public string ADoc1 { get; set; }
            [DataMember]
            public string ADoc2 { get; set; }
            [DataMember]
            public string ADoc3 { get; set; }
            [DataMember]
            public string ADocOther { get; set; }
            [DataMember]
            public string ARemark { get; set; }
            [DataMember]
            public string AObjFbnum { get; set; }
            [DataMember]
            public string ATpName { get; set; }
            [DataMember]
            public string ACheck { get; set; }
            [DataMember]
            public string AGpart1 { get; set; }
            [DataMember]
            public string FormGuid { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string AAgree { get; set; }
            [DataMember]
            public int AStep { get; set; }
            [DataMember]
            public DateTime? AAgreeDt { get; set; }
            [DataMember]
            public string AAgreeTm { get; set; }
            [DataMember]
            public string CaseGuid { get; set; }
            [DataMember]
            public string Textnote { get; set; }
            [DataMember]
            public AttDetSet AttDetSet { get; set; }
            [DataMember]
            public ZnotesSet znotesSet { get; set; }
            [DataMember]
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }

    }

   
    public class ZakatObjectionSummaryModel
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

       
        public class AttDetSet
        {
            [DataMember]
            public List<object> results { get; set; }
        }

       
        public class ZnotesSet
        {
            [DataMember]
            public List<object> results { get; set; }
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
            public string ASel { get; set; }
            [DataMember]
            public string ARefNo { get; set; }
            [DataMember]
            public string AAssnmtYr { get; set; }
            [DataMember]
            public DateTime? APeriodFrom { get; set; }
            [DataMember]
            public DateTime? APeriodTo { get; set; }
            [DataMember]
            public string ATaxTy { get; set; }
            [DataMember]
            public string ACurr { get; set; }
            [DataMember]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            public string ARevAmt { get; set; }
            [DataMember]
            public string ADisputeAmt { get; set; }
            [DataMember]
            public string ARetDet { get; set; }
        }

       
        public class ZobjItemsSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }

       
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string AComments { get; set; }
            [DataMember]
            public string UserTin { get; set; }
            [DataMember]
            public string AErrorFg { get; set; }
            [DataMember]
            public string Auditorz { get; set; }
            [DataMember]
            public string Taxpayerz { get; set; }
            [DataMember]
            public string RegIdz { get; set; }
            [DataMember]
            public string PeriodKeyz { get; set; }
            [DataMember]
            public string Submitz { get; set; }
            [DataMember]
            public string Savez { get; set; }
            [DataMember]
            public string Fbnumz { get; set; }
            [DataMember]
            public string Langz { get; set; }
            [DataMember]
            public string PortalUsrz { get; set; }
            [DataMember]
            public string Monthz { get; set; }
            [DataMember]
            public string OfficerUidz { get; set; }
            [DataMember]
            public string Approvez { get; set; }
            [DataMember]
            public string Rejectz { get; set; }
            [DataMember]
            public string CreateTxAssesz { get; set; }
            [DataMember]
            public string Xvoidz { get; set; }
            [DataMember]
            public string AmdRsnz { get; set; }
            [DataMember]
            public string Mandt { get; set; }
            [DataMember]
            public string LegacyDocNo { get; set; }
            [DataMember]
            public string ABranch { get; set; }
            [DataMember]
            public string ABranchCd { get; set; }
            [DataMember]
            public string AReceiveBy { get; set; }
            [DataMember]
            public DateTime? AReceiveDt { get; set; }
            [DataMember]
            public string AAppBy { get; set; }
            [DataMember]
            public DateTime? AAppDt { get; set; }
            [DataMember]
            public string AFbnum { get; set; }
            [DataMember]
            public string AGpart { get; set; }
            [DataMember]
            public string ADoc1 { get; set; }
            [DataMember]
            public string ADoc2 { get; set; }
            [DataMember]
            public string ADoc3 { get; set; }
            [DataMember]
            public string ADocOther { get; set; }
            [DataMember]
            public string ARemark { get; set; }
            [DataMember]
            public string AObjFbnum { get; set; }
            [DataMember]
            public string ATpName { get; set; }
            [DataMember]
            public string ACheck { get; set; }
            [DataMember]
            public string AGpart1 { get; set; }
            [DataMember]
            public string FormGuid { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string AAgree { get; set; }
            [DataMember]
            public int AStep { get; set; }
            [DataMember]
            public DateTime? AAgreeDt { get; set; }
            [DataMember]
            public string AAgreeTm { get; set; }
            [DataMember]
            public string CaseGuid { get; set; }
            [DataMember]
            public string Textnote { get; set; }
            [DataMember]
            public AttDetSet AttDetSet { get; set; }
            [DataMember]
            public ZnotesSet znotesSet { get; set; }
            [DataMember]
            public ZobjItemsSet zobj_itemsSet { get; set; }
        }






    }
}
