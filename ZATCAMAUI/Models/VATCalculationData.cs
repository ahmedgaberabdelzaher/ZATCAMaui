namespace ZATCAMAUI.Models
{

    public class VATCalculationData
    {
        public VATCalculationDataD d { get; set; }
    }
   
    public class VATRateDataWithStringDateType
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
   
    public class VATRateDataWithDateType
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
   
    public class VATCalculationDataDMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class ITUDSetResult
    {
        public Metadata2 __metadata { get; set; }
        public string Mandt { get; set; }
        public string SysFlg { get; set; }
        public string Lang { get; set; }
        public string Fbtyp { get; set; }
        public string SourceFg { get; set; }
        public string UrlPortal { get; set; }
    }
   
    public class ITUDSet
    {
        public List<ITUDSetResult> results { get; set; }
    }
   
    public class VATRSet
    {
        public List<VATCalculationDataVATRSet> results { get; set; }
    }
   
    public class VATCalculationDataVATRSet
    {
        public Metadata3 __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Type { get; set; }
        public string Penalty { get; set; }
        public DateTime Begda { get; set; }
        public DateTime Endda { get; set; }
    }
   
    public class VATCalculationDataVTTHSet
    {
        public Metadata5 __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Type { get; set; }
        public string MinVal { get; set; }
        public string MaxVal { get; set; }
        public string Percentage { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
    //public class Metadata3
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}
   
    public class IBANSetResult
    {
        public Metadata3 __metadata { get; set; }
        public string Partner { get; set; }
        public string Bkvid { get; set; }
        public string Iban { get; set; }
    }
   
    public class VATCalculationDataIBANSet
    {
        public List<IBANSetResult> results { get; set; }
    }

    //public class Metadata4
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}

   
    public class IGRTSetResult
    {
        public Metadata4 __metadata { get; set; }
        public string Mandt { get; set; }
        public string GrpNo { get; set; }
        public string RateTrtmt { get; set; }
    }

   
    public class IGRTSet
    {
        public List<IGRTSetResult> results { get; set; }
    }
    //public class Metadata5
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}
   
    public class VTTHSetResult
    {
        public Metadata5 __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Type { get; set; }
        public string MinVal { get; set; }
        public string MaxVal { get; set; }
        public string Percentage { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
   
    public class VTTHSet
    {
        public List<VTTHSetResult> results { get; set; }
    }
    //public class Metadata6
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }
    //}
   
    public class UIBTNSetResult
    {
        public Metadata6 __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Fbust { get; set; }
        public string Button { get; set; }
        public string TransactionType { get; set; }
        public string UserTyp { get; set; }
    }
   
    public class UIBTNSet
    {
        public List<Result5> results { get; set; }
    }
   
    public class VATCalculationDataD
    {
        public Metadata __metadata { get; set; }
        public string Mandtz { get; set; }
        public string Fbtypz { get; set; }
        public string Fbustz { get; set; }
        public string UserTypz { get; set; }
        public string TransactionTypez { get; set; }
        public string EditFgz { get; set; }
        public string Mandt { get; set; }
        public string Fbnum { get; set; }
        public string PortalUsr { get; set; }
        public string Lang { get; set; }
        public string Operation { get; set; }
        public string StepNumber { get; set; }
        public string ReturnId { get; set; }
        public string Officer { get; set; }
        public string Gpart { get; set; }
        public string Status { get; set; }
        public string UserTyp { get; set; }
        public string TxnTp { get; set; }
        public string Formproc { get; set; }
        public string OfficerT { get; set; }
        public string SrcApp { get; set; }
        public string Periodkey { get; set; }
        public object Begda { get; set; }
        public object Endda { get; set; }
        public string DestCheck { get; set; }
        public ITUDSet ITUDSet { get; set; }
        public VATRSet VATRSet { get; set; }
        public VATCalculationDataIBANSet IBANSet { get; set; }
        public IGRTSet IGRTSet { get; set; }
        public VTTHSet VTTHSet { get; set; }
        public UIBTNSet UI_BTNSet { get; set; }
    }

}
