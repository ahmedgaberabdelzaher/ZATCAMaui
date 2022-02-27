using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class EstimatedZakatReturns
    {
        public EstimatedZakatReturnsD d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZakatReturnsMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZakatReturnsMetadata2
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZakatReturnsResult : INotifyPropertyChanged
    {
        public Metadata2 __metadata { get; set; }
        public string Liveflg { get; set; }
        public string Statfg { get; set; }
        public string Monthz { get; set; }
        public string UserErrFg { get; set; }
        public string Gpart { get; set; }
        public string Lang { get; set; }
        public string Begda { get; set; }//Date
        public string Endda { get; set; }//Date
        public string ObligFlag { get; set; }
        public string Vtref { get; set; }
        public string Incotyp { get; set; }
        public string Incotext { get; set; }
        public string Fbtyp { get; set; }
        public string FbtText { get; set; }
        public string Due { get; set; }
        public string Persl { get; set; }
        public string TaxPeriod { get; set; } //Date
        public string DueDt { get; set; }
        public string Fbnum { get; set; }
        public string Status { get; set; }
        public string CalendrTyp { get; set; }
        public string Abrzu { get; set; } //Date
        public string Abrzo { get; set; } //Date
        public string Stat { get; set; }
        public bool ObjFiled { get; set; }
        public bool RefundFiled { get; set; }
        public string SadadDoc1 { get; set; }
        public string SadadDoc2 { get; set; }
        public string InfoMsg { get; set; }
        public string Period { get; set; }
        public string Statflag { get; set; }
        public string DueDtC { get; set; }
        public string Flag { get; set; }
        public string Comb { get; set; }
        public string Auditor { get; set; }
        public bool AudFlag { get; set; }
        public string Branch { get; set; }
        public string Aedat { get; set; } //Date
        public bool Open { get; set; }
        public string Msg { get; set; }
        public string Dispflag { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public string Euser1 { get; set; }
        public string Euser2 { get; set; }
        public string Euser3 { get; set; }
        public string Euser4 { get; set; }
        public string Euser5 { get; set; }
        public string Retguid { get; set; }
        private string _dueDate;
        public string DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
            }
        }
        //public string _dueDT;
        //public string DueDt
        //{
        //    get
        //    {
        //        return _dueDT;
        //    }
        //    set
        //    {
        //        _dueDT = value;
        //        if (_dueDT != null)
        //        {
        //            if (_dueDT.Contains("T"))
        //            {
        //                string[] _dueDate = new String[2];
        //                _dueDate = _dueDT.Split('T');
        //                DueDate = _dueDate[0];
        //            }
        //        }
        //    }
        //}//DueDate
        //private string _statusImage;
        public string StatusImage { get; set; }
        //{
        //    get
        //    {
        //        return _statusImage;
        //    }
        //    set
        //    {
        //        _statusImage = value;
        //    }
        //}
        //private string _borderColour ;
        public string BorderColour { get; set; }
        //{
        //    get
        //    {
        //        return _borderColour;
        //    }
        //    set
        //    {
        //        _borderColour = value;
        //    }
        //}
        //private string _status;//StatusCode
        //public string Status
        //{
        //    get
        //    {
        //        return _status;
        //    }
        //    set
        //    {
        //        _status = value;
        //        OnPropertyChanged(nameof(Status));
        //        if (!string.IsNullOrEmpty(_status))
        //        {
        //            //For Border Colour
        //            if ((string.Equals(_status, "U")))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
        //            {
        //                    StatusImage = "ic_attachment.png";
        //                BorderColour = "#944E22";
        //            }
        //            else if (string.Equals(_status, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
        //            {
        //                BorderColour = "#003672";
        //                StatusImage = "ic_Paid.png";
        //            }
        //            else if (string.Equals(_status, "IP015"))//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
        //            {
        //                BorderColour = "#c49b2d"; 
        //                 StatusImage = "ic_loading.png";
        //            }
        //            else if (string.Equals(_status, "IP014"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
        //            {
        //                BorderColour = "#003672";
        //                StatusImage = "ic_Paid.png";
        //            }
        //            //For Image
        //            if (string.Equals(_status, "ZP017") || string.Equals(_status, "E0089") || string.Equals(_status, "E0090") || string.Equals(_status, "C0021") || string.Equals(_status, "ALL"))
        //            {
        //                StatusImage = "ic_Check_Gray.png";
        //            }
        //            else if (string.Equals(_status, "E0013") || string.Equals(_status, "E0056"))
        //            {
        //                StatusImage = "ic_save_Gray.png";
        //            }
        //            else if (string.Equals(_status, "E0057"))
        //            {
        //                StatusImage = "ic_save_golden.png";
        //            }
        //        }
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
    [Preserve(AllMembers = true)]
    public class ListSet
    {
        public List<EstimatedZakatReturnsResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZakatReturnsD
    {
        public Metadata __metadata { get; set; }
        public string UserTin { get; set; }
        public string Client { get; set; }
        public string Zuser { get; set; }
        public string Zregstatus { get; set; }
        public int Zfillingoblig { get; set; }
        public string Bpnum { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Taxtype { get; set; }
        public string Branch { get; set; }
        public string PortNo { get; set; }
        public string SystemName { get; set; }
        public string Protocol { get; set; }
        public int Refnum { get; set; }
        public int Reqnum { get; set; }
        public int Corrnum { get; set; }
        public int Regnum { get; set; }
        public int Accnum { get; set; }
        public int Oblnum { get; set; }
        public string Lang { get; set; }
        public string Auditor { get; set; }
        public bool AudReturn { get; set; }
        public bool AudObjection { get; set; }
        public bool AudRequest { get; set; }
        public bool AudRefund { get; set; }
        public string Caltype { get; set; }
        public bool TileOutlet { get; set; }
        public bool TilePermit { get; set; }
        public bool TileTin { get; set; }
        public bool IsBankruptcy { get; set; }
        public string Euser1 { get; set; }
        public string Euser2 { get; set; }
        public string Euser3 { get; set; }
        public string Euser4 { get; set; }
        public string Euser5 { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public bool AudRefundTrn { get; set; }
        public string IntPortal { get; set; }
        public bool UpdregOutflag { get; set; }
        public bool DisSharetile { get; set; }
        public bool NotifLogFlag { get; set; }
        public bool EnableTile { get; set; }
        public string Dept { get; set; }
        public string Type { get; set; }
        public bool EnableInstPlan { get; set; }
        public string Overdue { get; set; }
        public string Interest { get; set; }
        public string Penalty { get; set; }
        public string ExeDtFlg { get; set; }
        public string ExeAppFlg { get; set; }
        public string HostName { get; set; }
        public int Indcorrnum { get; set; }
        public string Ettr { get; set; }
        public string VatDtFlg { get; set; }
        public string WarDtFlg { get; set; }
        public string RetItFlg { get; set; }
        public int RetItCnt { get; set; }
        public int Actcnt { get; set; }
        public int Rencnt { get; set; }
        public int Cnlcnt { get; set; }
        public string NregDtFlg { get; set; }
        public string VatConfFlg { get; set; }
        public string VtiaSignFg { get; set; }
        public string VtepFg { get; set; }
        public string CallServ { get; set; }
        public ListSet listSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ZAKATStatus
    {
        public string Key { get; set; }
        public string KeyTwo { get; set; }
        public string Value { get; set; }
        public int index { get; set; }
    }
}
