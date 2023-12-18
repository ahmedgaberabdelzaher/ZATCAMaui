namespace ZATCAMAUI.Models
{


    public class Result
    {
        public string Descript { get; set; }
        public string CaseId { get; set; }
        public string LetterNum { get; set; }
        public string Auditor { get; set; }
        public string TaxtpFg { get; set; }
        public string UserTin { get; set; }
        public string Pdfurl { get; set; }
    }
    
    public class ZakatSet
    {
        public List<Result> results { get; set; }
    }
    
    public class VATSet
    {
        public List<Result> results { get; set; }
    }
    
    public class ExciseSet
    {
        public List<Result> results { get; set; }
    }
    
    public class AllCertificate
    {
        public ZakatSet ZakatSet { get; set; }
        public VATSet VATSet { get; set; }
        public ExciseSet ExciseSet { get; set; }
    }
}
