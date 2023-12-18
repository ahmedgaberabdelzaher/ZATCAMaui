using System.ComponentModel;
using System.Runtime.Serialization;
using ZATCAMAUI.Core.Mangers;
using Font = Microsoft.Maui.Font;

namespace ZATCAMAUI.Models
{
    //public class Metadata
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}
    [Serializable]
   
    [DataContract]
    public class VATDeclarationsMetadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    //[Serializable]
   
    //[DataContract]
    public class Metadata2
    {
        //[DataMember]
        public string id { get; set; }
        //[DataMember]
        public string uri { get; set; }
        //[DataMember]
        public string type { get; set; }
    }

    // [Serializable]
   
    // [DataContract]
    public class Note
    {
        //[DataMember]
        public Metadata2 __metadata { get; set; }
        //[DataMember]
        public string Notenoz { get; set; }
        //[DataMember]
        public string Refnamez { get; set; }
        //[DataMember]
        public string XInvoicez { get; set; }
        //[DataMember]
        public string XObsoletez { get; set; }
        //[DataMember]
        public string Rcodez { get; set; }
        //[DataMember]
        public string Erfusrz { get; set; }
        //[DataMember]
        public string Erfdtz { get; set; }
        //[DataMember]
        public string Erftmz { get; set; }
        //[DataMember]
        public string AttByz { get; set; }
        //[DataMember]
        public string ByPusrz { get; set; }
        //[DataMember]
        public string ByGpartz { get; set; }
        //[DataMember]
        public string DataVersionz { get; set; }
        //[DataMember]
        public string Namez { get; set; } //Name
        //[DataMember]
        public string Noteno { get; set; }
        //[DataMember]
        public int Lineno { get; set; }
        //[DataMember]
        public int ElemNo { get; set; }
        //[DataMember]
        public string Tdformat { get; set; }
        //[DataMember]
        public string Tdline { get; set; }
        //[DataMember]
        public string Sect { get; set; } //Section
        //[DataMember]
        public string Strdt { get; set; } //date
        //[DataMember]
        public string Strtime { get; set; } //time
        //[DataMember]
        public string Strline { get; set; }   //note
    }

    // [Serializable]
   
    // [DataContract]
    public class NOTESSet
    {
        // [DataMember]
        public List<Note> results { get; set; }
    }

    // [Serializable]
   
    //[DataContract]
    public class Metadata3
    {
        //[DataMember]
        public string id { get; set; }
        //[DataMember]
        public string uri { get; set; }
        //[DataMember]
        public string type { get; set; }
    }

    //[Serializable]
   
    //[DataContract]
    public class Result2
    {
        //[DataMember]
        public Metadata3 __metadata { get; set; }
        //[DataMember]
        public string Partner { get; set; } = string.Empty;
        //[DataMember]
        public string Bkvid { get; set; } = string.Empty;
        //[DataMember]
        public string Iban { get; set; }
    }

    //[Serializable]
   
    //[DataContract]
    public class IBANSet
    {
        //[DataMember]
        public List<Result2> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Metadata4
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Result3
    {
        [DataMember]
        public Metadata4 __metadata { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string CreditAmt { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string DocNo { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string ReturnId { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class CFSet
    {
        [DataMember]
        public List<Result3> results { get; set; }
    }

    // [Serializable]
   
    // [DataContract]
    public class Metadata5
    {
        //[DataMember]
        public string id { get; set; }
        //[DataMember]
        public string uri { get; set; }
        //[DataMember]
        public string type { get; set; }
    }

    //[Serializable]
   
    //[DataContract]
    public class Attachment
    {
        //[DataMember]
        public Metadata5 __metadata { get; set; }
        //[DataMember]
        public string RetGuid { get; set; }
        //[DataMember]
        public string Seqno { get; set; }
        //[DataMember]
        public string SchGuid { get; set; }
        //[DataMember]
        public string Dotyp { get; set; }
        //[DataMember]
        public int Srno { get; set; }
        //[DataMember]
        public string Doguid { get; set; }
        //[DataMember]
        public string AttBy { get; set; }
        //[DataMember]
        public string Filename { get; set; }
        //[DataMember]
        public string FileExtn { get; set; }
        //[DataMember]
        public string Mimetype { get; set; }
        //[DataMember]
        public string ByPusr { get; set; }
        //[DataMember]
        public string Erfdt { get; set; }
        //[DataMember]
        public string Erftm { get; set; }
        //[DataMember]
        public string DataVersion { get; set; }
        //[DataMember]
        public string DocUrl { get; set; }
        //[DataMember]
        public string OutletRef { get; set; }
        //[DataMember]
        public string Enbedit { get; set; }
        //[DataMember]
        public string Enbdele { get; set; }
        //[DataMember]
        public string Visedit { get; set; }
        //[DataMember]
        public string Visdel { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATAttachment
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [DataMember]
        public string RetGuid { get; set; }
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
        public string ByPusr { get; set; }
        [DataMember]
        public string Erfdt { get; set; }
        [DataMember]
        public string Erftm { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string DocUrl { get; set; }
        [DataMember]
        public string OutletRef { get; set; }
        [DataMember]
        public string Enbedit { get; set; }
        [DataMember]
        public string Enbdele { get; set; }
        [DataMember]
        public string Visedit { get; set; }
        [DataMember]
        public string Visdel { get; set; }
        [DataMember]
        public string DeleteImageSource { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class ATTACHSet
    {
        [DataMember]
        public List<Attachment> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Metadata6
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Result5
    {
        [DataMember]
        public Metadata6 __metadata { get; set; }
        [DataMember]
        public string RegionDesc { get; set; }
        [DataMember]
        public string AddrType { get; set; }
        [DataMember]
        public string Srcidentify { get; set; }
        [DataMember]
        public object Begda { get; set; }
        [DataMember]
        public object Endda { get; set; }
        [DataMember]
        public string Addrnumber { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Quarter { get; set; }
        [DataMember]
        public string PostalCd { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string AdditionalNo { get; set; }
        [DataMember]
        public string BuildingNo { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string SizUn { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class ADRSet
    {
        [DataMember]
        public List<Result5> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATRMSGSet
    {
        [DataMember]
        public List<object> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class AttachmentRootOject
    {
        [DataMember]
        public Attachment d { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATDeclarationD
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [DataMember]
        public VATDeclarationsMetadata __metadata { get; set; }
        [DataMember]

        public string GoliveFg { get; set; }
        [DataMember]
        public string Cr3487golive { get; set; }
        [DataMember]
        public string Cr1317golive { get; set; }
        [DataMember]
        public string Cr2215 { get; set; }
        [DataMember]
        public string GovsupYesno { get; set; }

        [DataMember]
        public string ReviewNaMsg { get; set; }
        [DataMember]
        public string Cr1645GoliveFg { get; set; }
        [DataMember]

        public string Yesno { get; set; }
        [DataMember]
        public string TcFlg { get; set; }
        [DataMember]
        public string Idtype { get; set; }
        [DataMember]
        public string Betrh { get; set; }
        [DataMember]
        public string ImporterFg { get; set; }
        [DataMember]
        public string Idnum { get; set; }
        [DataMember]
        public string GrpNo { get; set; }
        [DataMember]
        public string SubmitFg { get; set; }
        [DataMember]
        public string IdType { get; set; }
        [DataMember]
        public string Idnumber { get; set; }
        [DataMember]
        public string TotalsalesAmt { get; set; }
        //public string _totalsalesAmt;
        //public string TotalsalesAmt
        //{ 
        //     get
        //    {
        //        return _totalsalesAmt;
        //    }
        //    set
        //    {
        //        _totalsalesAmt = value;
        //        OnPropertyChanged("TotalsalesAmt");
        //    }
        //}
        [DataMember]
        public string EstimatedFg { get; set; }
        [DataMember]
        public string TotalsalesAdj { get; set; }
        //public string _totalsalesAdj;
        //public string TotalsalesAdj
        //{
        //    get
        //    {
        //        return _totalsalesAdj;
        //    }
        //    set
        //    {
        //        _totalsalesAdj = value;
        //        OnPropertyChanged("TotalsalesAdj");
        //    }
        //}
        [DataMember]
        public string Fbust { get; set; }
        [DataMember]
        public string TotalpurchaseAmt { get; set; }
        //public string _totalpurchaseAmt;
        //public string TotalpurchaseAmt
        //{
        //    get
        //    {
        //        return _totalpurchaseAmt;
        //    }
        //    set
        //    {
        //        _totalpurchaseAmt = value;
        //        OnPropertyChanged("TotalpurchaseAmt");
        //    }
        //}
        [DataMember]
        public string TotalpurchaseAdj { get; set; }
        //public string _totalpurchaseAdj;
        //public string TotalpurchaseAdj
        //{
        //    get
        //    {
        //        return _totalpurchaseAdj;
        //    }
        //    set
        //    {
        //        _totalpurchaseAdj = value;
        //        OnPropertyChanged("TotalpurchaseAdj");
        //    }
        //}
        [DataMember]
        public string TpregFg { get; set; }
        [DataMember]
        public string CreditVatRef { get; set; }
        [DataMember]
        public string VatPost { get; set; }
        [DataMember]
        public string IbanCb { get; set; }
        [DataMember]
        public string LfpVat { get; set; }
        [DataMember]
        public string Incotext { get; set; }
        [DataMember]
        public string Mandtz { get; set; }
        [DataMember]
        public string Periodkeyz { get; set; }
        [DataMember]
        public string Perslt { get; set; }
        [DataMember]
        public string ConfStp2 { get; set; }
        [DataMember]
        public string TotaldueVat { get; set; }
        [DataMember]
        public string Fbnumz { get; set; }
        [DataMember]
        public string PortalUsrz { get; set; }
        [DataMember]
        public string Langz { get; set; }
        [DataMember]
        public string Operationz { get; set; }
        [DataMember]
        public string StepNumberz { get; set; }
        [DataMember]
        public string ReturnIdz { get; set; }
        [DataMember]
        public string Officerz { get; set; }
        [DataMember]
        public string Gpartz { get; set; }
        [DataMember]
        public string Statusz { get; set; }
        [DataMember]
        public string UserTypz { get; set; }
        [DataMember]
        public string TxnTpz { get; set; }
        [DataMember]
        public string Formprocz { get; set; }
        [DataMember]
        public string OfficerTz { get; set; }
        [DataMember]
        public string SrcAppz { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string Gpart { get; set; }
        [DataMember]
        public string Vrtaxret { get; set; }
        [DataMember]
        public string Incotyp { get; set; }
        [DataMember]
        public string Persl { get; set; }
        [DataMember]
        public string Abrzu { get; set; }
        [DataMember]
        public string Abrzo { get; set; }
        [DataMember]
        public string Fin { get; set; }
        [DataMember]
        public string Tpnm { get; set; }
        [DataMember]
        public string TcFg { get; set; }
        [DataMember]
        public string StdsalesAmt { get; set; }
        [DataMember]
        public string StdsalesAdj { get; set; }
        [DataMember]
        public string StdsalesVat { get; set; }
        //public string _stdsalesVat;
        //public string StdsalesVat
        //{
        //    get
        //    {
        //        return _stdsalesVat;
        //    }
        //    set
        //    {
        //        _stdsalesVat = value;
        //        OnPropertyChanged("StdsalesVat");
        //    }
        //}
        [DataMember]
        public string SalesGccAmt { get; set; }
        [DataMember]
        public string SalesGccAdj { get; set; }
        [DataMember]
        public string ZerosalesAmt { get; set; }
        [DataMember]
        public string ZerosalesAdj { get; set; }
        [DataMember]
        public string ExportsAmt { get; set; }
        [DataMember]
        public string ExportsAdj { get; set; }
        [DataMember]
        public string ExemptsalesAmt { get; set; }
        [DataMember]
        public string ExemptsalesAdj { get; set; }
        [DataMember]
        public string TotalsalesVat { get; set; }
        //public string _totalsalesVat;
        //public string TotalsalesVat
        //{
        //    get
        //    {
        //        return _totalsalesVat;
        //    }
        //    set
        //    {
        //        _totalsalesVat = value;
        //        OnPropertyChanged("TotalsalesVat");
        //    }
        //}
        [DataMember]
        public string StdpurchaseAmt { get; set; }
        [DataMember]
        public string StdpurchaseAdj { get; set; }
        [DataMember]
        public string StdpurchasesVat { get; set; }
        //public string _stdpurchasesVat;
        //public string StdpurchasesVat
        //{
        //    get
        //    {
        //        return _stdpurchasesVat;
        //    }
        //    set
        //    {
        //        _stdpurchasesVat = value;
        //        OnPropertyChanged("StdpurchasesVat");
        //    }
        //}
        [DataMember]
        public string ImportspaidAmt { get; set; }
        [DataMember]
        public string ImportspaidAdj { get; set; }
        [DataMember]
        public string ImportspaidVat { get; set; }
        //public string _importspaidVat;
        //public string ImportspaidVat
        //{
        //    get
        //    {
        //        return _importspaidVat;
        //    }
        //    set
        //    {
        //        _importspaidVat = value;
        //        OnPropertyChanged("ImportspaidVat");
        //    }
        //}
        [DataMember]
        public string ImportsaccAmt { get; set; }
        [DataMember]
        public string ImportsaccAdj { get; set; }
        [DataMember]
        public string ImportsaccVat { get; set; }
        [DataMember]
        public string ZeropurchaseAmt { get; set; }
        [DataMember]
        public string ZeropurchaseAdj { get; set; }
        [DataMember]
        public string ExemptpurchaseAmt { get; set; }
        [DataMember]
        public string ExemptpurchaseAdj { get; set; }
        [DataMember]
        public string TotalpurchaseVat { get; set; }
        //public string _totalpurchaseVat;
        //public string TotalpurchaseVat
        //{
        //    get
        //    {
        //        return _totalpurchaseVat;
        //    }
        //    set
        //    {
        //        _totalpurchaseVat = value;
        //        OnPropertyChanged("TotalpurchaseVat");
        //    }
        //}
        [DataMember]
        public string Preperiodcorr { get; set; }
        [DataMember]
        public string CreditVat { get; set; }
        [DataMember]
        public string NetdueVat { get; set; }
        [DataMember]
        public string CorrPen { get; set; }
        [DataMember]
        public string FinaldueVat { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string RefundFg { get; set; }
        [DataMember]
        public string ExporterFg { get; set; }
        [DataMember]
        public string DecFg { get; set; }
        [DataMember]
        public string ReceiptDt { get; set; }
        [DataMember]
        public string Sopbel { get; set; }
        [DataMember]
        public string Caltp { get; set; }
        [DataMember]
        public string StepNumber { get; set; }
        [DataMember]
        public string HotlineNo { get; set; }
        [DataMember]
        public string RetSource { get; set; }
        [DataMember]
        public string Iban { get; set; }
        [DataMember]
        public string Activity { get; set; }
        [DataMember]
        public string Euser { get; set; }
        [DataMember]
        public string Fbguid { get; set; }
        [DataMember]
        public string ToSflg { get; set; }
        [DataMember]
        public string DmodeFlg { get; set; }
        [DataMember]
        public string Block { get; set; }
        [DataMember]
        public string FldNm { get; set; }
        [DataMember]
        public VATPERITEMSet VATPERITEMSet { get; set; }
        [DataMember]
        public NOTESSet NOTESSet { get; set; }
        [DataMember]
        public IBANSet IBANSet { get; set; }
        [DataMember]
        public CFSet CFSet { get; set; }
        [DataMember]
        public ATTACHSet ATTACHSet { get; set; }
        [DataMember]
        public ADRSet ADRSet { get; set; }
        [DataMember]
        public VATRMSGSet VATR_MSGSet { get; set; }
        [DataMember]
        public string MadabutFg { get; set; }
        [DataMember]
        public string OpenliMsg { get; set; }
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }

    [Serializable]
   
    [DataContract]
    public class VATAttachments
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string Size { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class CreditCarried
    {
        [DataMember]
        public string SerialNumber { get; set; }
        [DataMember]
        public string ReturnReferenceNumber { get; set; }
        [DataMember]
        public string DocumentNumber { get; set; }
        [DataMember]
        public string Amount { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATDeclaration
    {
        [DataMember]
        public VATDeclarationD d { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class VATDeclarationTabbedPageName : INotifyPropertyChanged
    {
        [DataMember]
        Color textColor = (Color)Application.Current.Resources["White"];
        [DataMember]
        Font _font = Font.Default;
        [DataMember]
        Font fontnew;
        //public VATDeclarationTabbedPageName()
        //{
        //    //_font.FontFamily=
        //}
        [DataMember]
        public string pageName { get; set; }
        [DataMember]
        public Color TextColor
        {
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TextColor"));
                    }
                }
            }
            get
            {
                return textColor;
            }
        }
        [DataMember]
        public Font Font
        {
            set
            {
                if (_font != value)
                {
                    _font = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Font"));
                    }
                }
            }
            get
            {
                return _font;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable]
   
    [DataContract]
    public class ApplicableButton
    {
        [DataMember]
        public string Fbtyp { get; set; }
        [DataMember]
        public string Fbust { get; set; }
        [DataMember]
        public string Button { get; set; }
        [DataMember]
        public string TransactionType { get; set; }
        [DataMember]
        public string UserTyp { get; set; }
        [DataMember]
        public Buttons buttonEnumId = Buttons.None;
    }


    #region New Added models for VAT 15%change

    [Serializable]
   
    [DataContract]
    public class VATPERITEMSet
    {
        [DataMember]
        public List<Result6> results { get; set; }
    }

    [Serializable]
   
    [DataContract]
    public class Result6
    {
        [DataMember]
        public VATDeclarationsMetadata __metadata { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string Rate { get; set; }
        [DataMember]
        public string StdsalesAmt { get; set; }
        [DataMember]
        public string StdsalesAdj { get; set; }
        [DataMember]
        public string StdsalesVat { get; set; }
        [DataMember]
        public string StdpurchaseAmt { get; set; }
        [DataMember]
        public string StdpurchaseAdj { get; set; }
        [DataMember]
        public string StdpurchasesVat { get; set; }
        [DataMember]
        public string ImportspaidAmt { get; set; }
        [DataMember]
        public string ImportspaidAdj { get; set; }
        [DataMember]
        public string ImportspaidVat { get; set; }
        [DataMember]
        public string ImportsaccAmt { get; set; }
        [DataMember]
        public string ImportsaccAdj { get; set; }
        [DataMember]
        public string ImportsaccVat { get; set; }
        [DataMember]
        public string ReturnId { get; set; }
        [DataMember]
        public string TimestampCr { get; set; }
        [DataMember]
        public string TimestampCh { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string GovsupsalesAdj { get; set; }
        [DataMember]
        public string GovsupsalesVat { get; set; }
        [DataMember]
        public string GovsupsalesAmt { get; set; }
    }

    #endregion


}
