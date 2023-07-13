using System;
namespace EGAZT.Models.CustomServices.FasahModels
{
	public class FasahLoginResponse
	{
        public string name { get; set; }
        public string token { get; set; }
        public string lastDigitsMobile { get; set; }
        public bool updateProfile { get; set; }
        public bool passwordExpired { get; set; }
        public bool passwordForceChange { get; set; }
        public bool impersonated { get; set; }
        public string firstEmailLetters { get; set; }
        public int restLengthEmailLetters { get; set; }
        public bool isMobileEmailGlobalUpdate { get; set; }
        public int code { get; set; }

    }
    public class FasahLoginErrorResponse
	{
        public string message { get; set; }
        public int code { get; set; }

    }
}

