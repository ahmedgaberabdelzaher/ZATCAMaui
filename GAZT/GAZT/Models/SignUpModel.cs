using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    class SignUpModel
    {
    }
    public class SignUpModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    [Serializable]
    [Preserve(AllMembers = true)]
    [DataContract]
    public class SignUpModelD
    {
        [DataMember]
        public SignUpModelMetadata __metadata { get; set; }
        [DataMember]
        public string AAgreeDt { get; set; }
        [DataMember]
        public string ATinExist { get; set; }
        [DataMember]
        public string ACityCode { get; set; }
        [DataMember]
        public string ABirthdt { get; set; }
        [DataMember]
        public string AExternal { get; set; }
        [DataMember]
        public string ALang { get; set; }
        [DataMember]
        public string ACity { get; set; }
        [DataMember]
        public string ACrexpdt { get; set; }
        [DataMember]
        public string AInternal { get; set; }
        [DataMember]
        public string AIdtype { get; set; }
        [DataMember]
        public string APassword { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string ACaptcha { get; set; }
        [DataMember]
        public string AIssuedBy { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string ASubmit { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string AType { get; set; }
        [DataMember]
        public string AFirstname { get; set; }
        [DataMember]
        public string ALastname { get; set; }
        [DataMember]
        public string ATin { get; set; }
        [DataMember]
        public string AIdnumber { get; set; }
        [DataMember]
        public string ALicenceNo { get; set; }
        [DataMember]
        public string AEmail { get; set; }
        [DataMember]
        public string APhone { get; set; }
        [DataMember]
        public string AMobile { get; set; }
        [DataMember]
        public string ACompany { get; set; }
        [DataMember]
        public string ASmsCode { get; set; }
        [DataMember]
        public string AEmailCode { get; set; }
        [DataMember]
        public string ACommId { get; set; }
        [DataMember]
        public string AContNo { get; set; }
        [DataMember]
        public string AGovtAgency { get; set; }
        [DataMember]
        public string ATaxNo { get; set; }
        [DataMember]
        public string APractcingCert { get; set; }
        [DataMember]
        public string ALicDoc { get; set; }
        [DataMember]
        public string ACompBaseId { get; set; }
        [DataMember]
        public string AAgree { get; set; }
        [DataMember]
        public string AAgreeTm { get; set; }
        [DataMember]
        public string ACountry { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SignUpModelRootObject
    {
        public SignUpModelD d { get; set; }
    }
}
