
/* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
Before:
using EGAZT.Models;
using System;
After:
using System;
*/
namespace ZATCAMAUI.Models
{

    public class ForgotCredentialType
    {
        public string id { get; set; }
        public string CredentialType { get; set; }
    }
    
    public class ChangePasswordForEmail
    {
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
        public ComingToOTPVerificationScreenFrom navigateTo { get; set; }
    }
    
    public class VATParameterType
    {
        public string id { get; set; }
        public string ParameterType { get; set; }
    }
}
