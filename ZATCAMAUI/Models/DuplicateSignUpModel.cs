namespace ZATCAMAUI.Models
{

    class DuplicateSignUpModel
    {
    }
    
    public class DuplicateSignUpModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class DuplicateSignUpModelD
    {
        public DuplicateSignUpModelMetadata __metadata { get; set; }
        public string Flag { get; set; }
        public string ErrorFlag { get; set; }
        public string StartDt { get; set; }
        public string Partner { get; set; }
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public string Institute { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
    
    public class DuplicateSignUpModelRootObject
    {
        public DuplicateSignUpModelD d { get; set; }
    }
}
