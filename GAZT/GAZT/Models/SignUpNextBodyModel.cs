using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class SignUpNextBodyModel
    {
        public string ALang { get; set; }
        public string AType { get; set; }
        public string AFirstname { get; set; }
        public string ALastname { get; set; }
        public string AIdnumber { get; set; }
        public string ACommId { get; set; }
        public string AEmail { get; set; }
        public string APhone { get; set; }
        public string AMobile { get; set; }
        public string AIssuedBy { get; set; }
        public string ACity { get; set; }
        public string AIdtype { get; set; }      
        public string ABirthdt { get; set; }
        public string ATinExist { get; set; }
        public string CaseGuid { get; set; }
        public string ATin { get; set; }
        public string ALicenceNo { get; set; }
        public string ACityCode { get; set; }
        public string ACountry { get; set; }
    }
}
