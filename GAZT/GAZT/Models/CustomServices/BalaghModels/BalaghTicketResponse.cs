using System;
namespace EGAZT.Models.CustomServices.BalaghModels
{
    public class BalaghTicketResponse
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
