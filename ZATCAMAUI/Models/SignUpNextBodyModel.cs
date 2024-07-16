using System.Runtime.Serialization;

namespace ZATCAMAUI.Models
{

    public class SignUpNextBodyModel
    {
        [DataMember]

        public string ALang { get; set; }
        [DataMember]

        public string AType { get; set; }
        [DataMember]

        public string AFirstname { get; set; }
        [DataMember]

        public string ALastname { get; set; }
        [DataMember]

        public string AIdnumber { get; set; }
        [DataMember]

        public string ACommId { get; set; }
        [DataMember]

        public string AEmail { get; set; }
        [DataMember]

        public string APhone { get; set; }
        [DataMember]

        public string AMobile { get; set; }
        [DataMember]

        public string AIssuedBy { get; set; }
        [DataMember]

        public string ACity { get; set; }
        [DataMember]

        public string AIdtype { get; set; }
        [DataMember]

        public string ABirthdt { get; set; }
        [DataMember]

        public string ATinExist { get; set; }
        [DataMember]

        public string ACaptcha { get; set; }
        [DataMember]

        public string CaseGuid { get; set; }
        [DataMember]

        public string ATin { get; set; }
        [DataMember]

        public string ALicenceNo { get; set; }
        [DataMember]

        public string ACityCode { get; set; }
        [DataMember]

        public string ACountry { get; set; }

        [DataMember]

        public string AAbsherGuid { get; set; } = "";
        [DataMember]

        public string AAbsherOtp { get; set; } = "";

    }
}
