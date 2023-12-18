namespace ZATCAMAUI.Models
{

    public class IssuedByModel
    {
    }
    
    public class IssuedByMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class IssuedByResult
    {
        public IssuedByMetadata __metadata { get; set; }
        public string Response { get; set; }
        public string Request { get; set; }
    }
    
    public class IssuedByD
    {
        public List<IssuedByResult> results { get; set; }
    }
    
    public class IssuedByRootObject
    {
        public IssuedByD d { get; set; }
    }
    
    public class IssuedByResponse
    {
        public string mandt { get; set; }
        public string lang { get; set; }
        public string procsType { get; set; }
        public string elementCode { get; set; }
        public string txt50 { get; set; }
    }
}
