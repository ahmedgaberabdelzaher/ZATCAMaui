using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
namespace EGAZT.Models
{
    //public class Metadata
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}
    public class VATDeclarationsMetadata
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
    public class Note
    {
        public Metadata2 __metadata { get; set; }
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
        public string Namez { get; set; } //Name
        public string Noteno { get; set; }
        public int Lineno { get; set; }
        public int ElemNo { get; set; }
        public string Tdformat { get; set; }
        public string Tdline { get; set; }
        public string Sect { get; set; } //Section
        public string Strdt { get; set; } //date
        public string Strtime { get; set; } //time
        public string Strline { get; set; }   //note
    }
    public class NOTESSet
    {
        public List<Note> results { get; set; }
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
        public string Partner { get; set; } = string.Empty;
        public string Bkvid { get; set; } = string.Empty;
        public string Iban { get; set; }
    }
    public class IBANSet
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
        public int LineNo { get; set; }
        public string CreditAmt { get; set; }
        public string Fbnum { get; set; }
        public string DocNo { get; set; }
        public string Currency { get; set; }
        public string ReturnId { get; set; }
    }
    public class CFSet
    {
        public List<Result3> results { get; set; }
    }
    public class Metadata5
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class Attachment
    {
        public Metadata5 __metadata { get; set; }
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

    public class VATAttachment
    {
        public Metadata5 __metadata { get; set; }
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
        public string DeleteImageSource { get; set; }
    }

    public class ATTACHSet
    {
        public List<Attachment> results { get; set; }
    }
    public class Metadata6
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class Result5
    {
        public Metadata6 __metadata { get; set; }
        public string RegionDesc { get; set; }
        public string AddrType { get; set; }
        public string Srcidentify { get; set; }
        public object Begda { get; set; }
        public object Endda { get; set; }
        public string Addrnumber { get; set; }
        public string City { get; set; }
        public string Quarter { get; set; }
        public string PostalCd { get; set; }
        public string Street { get; set; }
        public string AdditionalNo { get; set; }
        public string BuildingNo { get; set; }
        public string Region { get; set; }
        public string SizUn { get; set; }
    }
    public class ADRSet
    {
        public List<Result5> results { get; set; }
    }
    public class VATRMSGSet
    {
        public List<object> results { get; set; }
    }
    public class AttachmentRootOject
    {
        public Attachment d { get; set; }
    }
    public class VATDeclarationD
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public VATDeclarationsMetadata __metadata { get; set; }

        public string GoliveFg { get; set; }
        public string Yesno { get; set; }
        public string TcFlg { get; set; }
        public string Idtype { get; set; }
        public string Betrh { get; set; }
        public string ImporterFg { get; set; }
        public string Idnum { get; set; }
        public string GrpNo { get; set; }
        public string SubmitFg { get; set; }
        public string IdType { get; set; }
        public string Idnumber { get; set; }
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
        public string EstimatedFg { get; set; }
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
        public string Fbust { get; set; }
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
        public string TpregFg { get; set; }
        public string CreditVatRef { get; set; }
        public string VatPost { get; set; }
        public string IbanCb { get; set; }
        public string LfpVat { get; set; }
        public string Incotext { get; set; }
        public string Mandtz { get; set; }
        public string Periodkeyz { get; set; }
        public string Perslt { get; set; }
        public string ConfStp2 { get; set; }
        public string TotaldueVat { get; set; }
        public string Fbnumz { get; set; }
        public string PortalUsrz { get; set; }
        public string Langz { get; set; }
        public string Operationz { get; set; }
        public string StepNumberz { get; set; }
        public string ReturnIdz { get; set; }
        public string Officerz { get; set; }
        public string Gpartz { get; set; }
        public string Statusz { get; set; }
        public string UserTypz { get; set; }
        public string TxnTpz { get; set; }
        public string Formprocz { get; set; }
        public string OfficerTz { get; set; }
        public string SrcAppz { get; set; }
        public string Fbnum { get; set; }
        public string Gpart { get; set; }
        public string Vrtaxret { get; set; }
        public string Incotyp { get; set; }
        public string Persl { get; set; }
        public string Abrzu { get; set; }
        public string Abrzo { get; set; }
        public string Fin { get; set; }
        public string Tpnm { get; set; }
        public string TcFg { get; set; }
        public string StdsalesAmt { get; set; }
        public string StdsalesAdj { get; set; }
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
        public string SalesGccAmt { get; set; }
        public string SalesGccAdj { get; set; }
        public string ZerosalesAmt { get; set; }
        public string ZerosalesAdj { get; set; }
        public string ExportsAmt { get; set; }
        public string ExportsAdj { get; set; }
        public string ExemptsalesAmt { get; set; }
        public string ExemptsalesAdj { get; set; }
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
        public string StdpurchaseAmt { get; set; }
        public string StdpurchaseAdj { get; set; }
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
        public string ImportspaidAmt { get; set; }
        public string ImportspaidAdj { get; set; }
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
        public string ImportsaccAmt { get; set; }
        public string ImportsaccAdj { get; set; }
        public string ImportsaccVat { get; set; }
        public string ZeropurchaseAmt { get; set; }
        public string ZeropurchaseAdj { get; set; }
        public string ExemptpurchaseAmt { get; set; }
        public string ExemptpurchaseAdj { get; set; }
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
        public string Preperiodcorr { get; set; }
        public string CreditVat { get; set; }
        public string NetdueVat { get; set; }
        public string CorrPen { get; set; }
        public string FinaldueVat { get; set; }
        public string Currency { get; set; }
        public string RefundFg { get; set; }
        public string ExporterFg { get; set; }
        public string DecFg { get; set; }
        public string ReceiptDt { get; set; }
        public string Sopbel { get; set; }
        public string Caltp { get; set; }
        public string StepNumber { get; set; }
        public string HotlineNo { get; set; }
        public string RetSource { get; set; }
        public string Iban { get; set; }
        public string Activity { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public string ToSflg { get; set; }
        public string DmodeFlg { get; set; }
        public string Block { get; set; }
        public string FldNm { get; set; }
        public VATPERITEMSet VATPERITEMSet { get; set; }
        public NOTESSet NOTESSet { get; set; }
        public IBANSet IBANSet { get; set; }
        public CFSet CFSet { get; set; }
        public ATTACHSet ATTACHSet { get; set; }
        public ADRSet ADRSet { get; set; }
        public VATRMSGSet VATR_MSGSet { get; set; }
        //protected void OnPropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
    public class VATAttachments
    {
        public string Id { get; set; }
        public string DocumentName { get; set; }
        public string Size { get; set; }
    }
    public class CreditCarried
    {
        public string SerialNumber { get; set; }
        public string ReturnReferenceNumber { get; set; }
        public string DocumentNumber { get; set; }
        public string Amount { get; set; }
    }
    public class VATDeclaration
    {
        public VATDeclarationD d { get; set; }
    }
    public class VATDeclarationTabbedPageName : INotifyPropertyChanged
    {
        Color textColor = Color.FromHex("#FFFFFF");
        Font _font = Font.Default;
        Font fontnew;
        //public VATDeclarationTabbedPageName()
        //{
        //    //_font.FontFamily=
        //}
        public string pageName { get; set; }
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
    public class ApplicableButton
    {
        public string Fbtyp { get; set; }
        public string Fbust { get; set; }
        public string Button { get; set; }
        public string TransactionType { get; set; }
        public string UserTyp { get; set; }
        public Buttons buttonEnumId = Buttons.None;
    }


    #region New Added models for VAT 15%change
    public class VATPERITEMSet
    {
        public List<Result6> results { get; set; }
    }


    public class Result6
    {
        public VATDeclarationsMetadata __metadata { get; set; }
        public string FormGuid { get; set; }
        public string Type { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Rate { get; set; }
        public string StdsalesAmt { get; set; }
        public string StdsalesAdj { get; set; }
        public string StdsalesVat { get; set; }
        public string StdpurchaseAmt { get; set; }
        public string StdpurchaseAdj { get; set; }
        public string StdpurchasesVat { get; set; }
        public string ImportspaidAmt { get; set; }
        public string ImportspaidAdj { get; set; }
        public string ImportspaidVat { get; set; }
        public string ImportsaccAmt { get; set; }
        public string ImportsaccAdj { get; set; }
        public string ImportsaccVat { get; set; }
        public string ReturnId { get; set; }
        public string TimestampCr { get; set; }
        public string TimestampCh { get; set; }
        public string Waers { get; set; }
    }

    #endregion


}
