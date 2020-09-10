using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models.ZakatObjectionsModel
{
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
}
