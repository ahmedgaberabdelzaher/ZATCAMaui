using System.Runtime.Serialization;

namespace ZATCAMAUI.Models
{

    public class VATSignUpGCC
    {
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CountryId { get; set; }


    }
}
