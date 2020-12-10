using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    class CRValidationModel
    {
    }
    public class CRValidationModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class CRValidationModelD
    {
        public CRValidationModelMetadata __metadata { get; set; }
        public string Crnum { get; set; }
        public string CityAry { get; set; }
        public object Validfrm { get; set; }
        public string CountryAry { get; set; }
        public object Validto { get; set; }
        public string NotFound { get; set; }
        public DateTime? Issuedt { get; set; }
        public object Expdt { get; set; }
        public string Crname { get; set; }
        public string Excption { get; set; }
        public string TelephoneNumbery { get; set; }
        public string FaxNumbery { get; set; }
        public string Emaily { get; set; }
        public string Telexy { get; set; }
        public string InternetAddressy { get; set; }
        public string AddressPostaly { get; set; }
        public string Addresstypey { get; set; }
        public string AddressPhysicaly { get; set; }
    }
    public class CRValidationModelRootObject
    {
        public CRValidationModelD d { get; set; }
    }
}
