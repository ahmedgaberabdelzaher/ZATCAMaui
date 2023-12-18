namespace ZATCAMAUI.Models
{

    public class IBANType
    {
        public string key { get; set; }
        public string Text { get; set; }
    }
    
    public class IBANIDNumber
    {
        public string Partner { get; set; }
        public string Idnumber { get; set; }
        public string Type { get; set; }
    }
    
    public class SadadNumber
    {
        public SadadNumberD d { get; set; }
    }
    
    public class SadadNumberMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class SadadNumberResult
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string Langu { get; set; }
        public string Sopbel { get; set; }
        public string TaxType { get; set; }
        public string Abtypt { get; set; }
        public string Betrh { get; set; }
        public string Waers { get; set; }
        public string Vtref { get; set; }
        public bool IsAutoAsmnt { get; set; }
        public string Fbust { get; set; }
    }
    
    public class SadadNumberD
    {
        public List<SadadNumberResult> results { get; set; }
    }
}
