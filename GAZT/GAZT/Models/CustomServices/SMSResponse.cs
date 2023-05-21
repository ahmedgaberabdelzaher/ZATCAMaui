using System;
namespace EGAZT.Models.CustomServices
{
    public class SMSResponse
    {
        
            public bool issuccess { get; set; }
            public int code { get; set; }
            public string message { get; set; }
            public bool data { get; set; }
            public int count { get; set; }
            public string correlationid { get; set; }
        
    }
}
