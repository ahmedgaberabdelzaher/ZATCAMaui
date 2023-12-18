namespace ZATCAMAUI.Models
{

    public class ValidateOTP
    {
        public ValidateOTPResponse d { get; set; }
    }
    
    public class ValidateOTPResponse
    {
        public Metadata __metadata { get; set; }
        public int Attempts { get; set; }
        public string Email { get; set; }
        public int CurrAttmps { get; set; }
        public string Langz { get; set; }
        public string FirstName { get; set; }
        public int Minutes { get; set; }
        public string LastName { get; set; }
        public string Result { get; set; }
        public string Password { get; set; }
        public string Userid { get; set; }
        public string Tin { get; set; }
        public string Mobile { get; set; }
        public string Otp { get; set; }
    }
}
