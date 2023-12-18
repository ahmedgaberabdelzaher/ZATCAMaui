namespace ZATCAMAUI.Models
{

    public class ConsumerRegisteration
    {
        public string Actnm { get; set; }
        public string Tin { get; set; }
        public string Caltyp { get; set; }
        public string Idnumber { get; set; }
        public string Udate { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
    }
    
    public class ItemSet
    {
        public List<ConsumerRegisteration> results { get; set; }
    }
    
    public class CheckTINStatus
    {
        public Metadata __metadata { get; set; }
        public string Langz { get; set; }
        public string Tin { get; set; }
        public string Caltyp { get; set; }
        public DateTime? Udate { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
        public ItemSet ItemSet { get; set; }
    }
    
    public class TINStatus
    {
        public CheckTINStatus d { get; set; }
    }
}
