using System.ComponentModel;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class EstimatedZakatReturns
    {
        [JsonProperty("data")]
        public EstimatedZakatReturnsD d { get; set; }
    }
    
    public class EstimatedZakatReturnsMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class EstimatedZakatReturnsMetadata2
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
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
        public string TaxPeriod { get; set; } //Date
        public string Fbnum { get; set; }
        public string Status { get; set; }
        public string CalendrTyp { get; set; }
        public string Abrzu { get; set; } //Date
        public string Abrzo { get; set; } //Date
        public string Stat { get; set; } // status
        public bool ObjFiled { get; set; }
        public bool RefundFiled { get; set; }
        public string SadadDoc1 { get; set; }
        public string SadadDoc2 { get; set; }
        public string InfoMsg { get; set; }
        public string Period { get; set; }
        public string Statflag { get; set; }
        public string DueDtC { get; set; }
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
        public string _dueDT;
        public string DueDt
        {
        get
            {
                return _dueDT;
            }
    set
            {
                _dueDT = value;
                if (_dueDT != null)
                {
                    if (_dueDT.Contains("T"))
                    {
                        string[] _dueDate = new String[2];
    _dueDate = _dueDT.Split('T');
                        DueDate = _dueDate[0];
                    }
                }
            }
        }//DueDate
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
    
    public class ListSet
    {
        public List<EstimatedZakatReturnsResult> results { get; set; }
    }
    
    public class EstimatedZakatReturnsD
    {
        //public Metadata __metadata { get; set; }

        [JsonProperty("userTIN")]
        public string UserTin { get; set; }

        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("user")]
        public string Zuser { get; set; }

        [JsonProperty("registrationStatus")]
        public string Zregstatus { get; set; }

        [JsonProperty("fillingObligation")]
        public int Zfillingoblig { get; set; }

        [JsonProperty("businessPartnerNumber")] //bussiness Partner Number
        public string Bpnum { get; set; }

        //public string Name { get; set; } // Not found
        
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("taxType")]
        public string Taxtype { get; set; }

        [JsonProperty("branchDescription")] // Branch description
        public string Branch { get; set; }

        [JsonProperty("portNumber")]
        public string PortNo { get; set; }

        [JsonProperty("systemName")]
        public string SystemName { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("referenceNumber")]
        public int Refnum { get; set; }

        [JsonProperty("requestNumber")]
        public int Reqnum { get; set; }

        [JsonProperty("correspondenceNumber")]
        public int Corrnum { get; set; }

        [JsonProperty("registrationNumber")]
        public int Regnum { get; set; }

        [JsonProperty("accountNumber")]
        public int Accnum { get; set; }

        [JsonProperty("obligationNumber")]
        public int Oblnum { get; set; }

        [JsonProperty("language")]
        public string Lang { get; set; }

        [JsonProperty("auditor")]
        public string Auditor { get; set; }

        [JsonProperty("isAuditorReturn")]
        public bool AudReturn { get; set; }

        [JsonProperty("isAuditorObjection")]
        public bool AudObjection { get; set; }

        [JsonProperty("isAuditorRequest")]
        public bool AudRequest { get; set; }

        [JsonProperty("isAuditorRefund")]
        public bool AudRefund { get; set; }

        [JsonProperty("calenderType")]
        public string Caltype { get; set; }

        [JsonProperty("isTileOutlet")] 
        public bool TileOutlet { get; set; }

        [JsonProperty("isTilePermit")]
        public bool TilePermit { get; set; }

        [JsonProperty("isTileTIN")]
        public bool TileTin { get; set; }

        [JsonProperty("isBankruptcy")]
        public bool IsBankruptcy { get; set; }

        [JsonProperty("authenticationUser1")]
        public string Euser1 { get; set; }

        [JsonProperty("authenticationUser2")]
        public string Euser2 { get; set; }

        [JsonProperty("authenticationUser3")]
        public string Euser3 { get; set; }

        [JsonProperty("authenticationUser4")]
        public string Euser4 { get; set; }

        [JsonProperty("authenticationUser5")]
        public string Euser5 { get; set; }

        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }

        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }

        [JsonProperty("isAuditorRefundTransaction")]
        public bool AudRefundTrn { get; set; }

        [JsonProperty("portalLink")]
        public string IntPortal { get; set; }

        [JsonProperty("isUpdateRegistrationOutlet")]
        public bool UpdregOutflag { get; set; }

        [JsonProperty("isDisplayShare")]
        public bool DisSharetile { get; set; }

        [JsonProperty("isNotificationLog")]
        public bool NotifLogFlag { get; set; }

        [JsonProperty("isEnableTile")]
        public bool EnableTile { get; set; }

        [JsonProperty("definedArea")]
        public string Dept { get; set; } // Not found

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("isEnableInstallementPlan")]
        public bool EnableInstPlan { get; set; }

        [JsonProperty("overDue")]
        public string Overdue { get; set; }

        [JsonProperty("interest")]
        public string Interest { get; set; }

        [JsonProperty("penaltyAmount")]
        public string Penalty { get; set; }

        [JsonProperty("exemptDetail")]
        public string ExeDtFlg { get; set; }

        [JsonProperty("exemptApplication")]
        public string ExeAppFlg { get; set; }

        [JsonProperty("hostName")]
        public string HostName { get; set; }

        [JsonProperty("indirectCorrespondenceNumber")]
        public int Indcorrnum { get; set; }

        [JsonProperty("closelyDefinedArea")]
        public string Ettr { get; set; } // Not found

        [JsonProperty("vatDetail")]
        public string VatDtFlg { get; set; }

        [JsonProperty("warehouseDetail")]
        public string WarDtFlg { get; set; }

        /*public string RetItFlg { get; set; }
        public int RetItCnt { get; set; }
        public int Actcnt { get; set; }
        public int Rencnt { get; set; }
        public int Cnlcnt { get; set; }
        public string NregDtFlg { get; set; }*/

        [JsonProperty("VATConfig")]
        public string VatConfFlg { get; set; }

        [JsonProperty("VATIndividualSignUp")]  // vat indivisual signup
        public string VtiaSignFg { get; set; }

        [JsonProperty("VATEligiblePerson")]
        public string VtepFg { get; set; } // vat eligable persion

        [JsonProperty("callService")]
        public string CallServ { get; set; }

        [JsonProperty("lists")]
        public List<EstimatedZakatReturnsResult> results { get; set; }
        //public List<ListSet> listSet { get; set; }
    }
    
    public class ZAKATStatus
    {
        public string Key { get; set; }
        public string KeyTwo { get; set; }
        public string Value { get; set; }
        public int index { get; set; }
    }
}
