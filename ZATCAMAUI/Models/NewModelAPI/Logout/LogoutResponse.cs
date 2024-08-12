using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI.Logout
{
    public class LogoutResponseModel
    {
        public Header header { get; set; }
        public Result result { get; set; }
    }



    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
    }



    public class Result
    {
        public string mobileNumber { get; set; }
        public string accessToken { get; set; }
        public string message { get; set; }
    }



    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}
