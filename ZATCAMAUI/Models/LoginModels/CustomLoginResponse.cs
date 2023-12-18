/* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
Before:
using System;
namespace EGAZT.Models.LoginModels
After:
namespace EGAZT.Models.LoginModels
*/

namespace ZATCAMAUI.Models.LoginModels
{
    public class USerDate
    {
        public string token { get; set; }
        public int id { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string fourthName { get; set; }
        public bool gender { get; set; }
        public string nationalId { get; set; }
        public string address { get; set; }
        public string emailAddress { get; set; }
        public string mobileNumber { get; set; }
        public int role { get; set; }
    }

    public class CustomLoginResponse
    {
        public bool isSuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public USerDate data { get; set; }
        public int count { get; set; }
    }

}
