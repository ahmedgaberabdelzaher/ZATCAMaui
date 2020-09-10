using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models.ZakatObjectionsModel
{
    public class ZakatObjectionListModel
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
}
