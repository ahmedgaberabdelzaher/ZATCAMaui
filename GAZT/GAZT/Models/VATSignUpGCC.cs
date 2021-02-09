using System;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
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
