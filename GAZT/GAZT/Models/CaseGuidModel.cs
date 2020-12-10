using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    class CaseGuidModel
    {
    }
    public class CaseGuidModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class CaseGuidModelResult
    {
        public CaseGuidModelMetadata __metadata { get; set; }
        public object AAgreeDt { get; set; }
        public string ATinExist { get; set; }
        public string ACityCode { get; set; }
        public object ABirthdt { get; set; }
        public string AExternal { get; set; }
        public string ALang { get; set; }
        public string ACity { get; set; }
        public object ACrexpdt { get; set; }
        public string AInternal { get; set; }
        public string AIdtype { get; set; }
        public string APassword { get; set; }
        public string CaseGuid { get; set; }
        public string AIssuedBy { get; set; }
        public string FormGuid { get; set; }
        public string ASubmit { get; set; }
        public string Fbnum { get; set; }
        public string AType { get; set; }
        public string AFirstname { get; set; }
        public string ALastname { get; set; }
        public string ATin { get; set; }
        public string AIdnumber { get; set; }
        public string ALicenceNo { get; set; }
        public string AEmail { get; set; }
        public string APhone { get; set; }
        public string AMobile { get; set; }
        public string ACompany { get; set; }
        public string ASmsCode { get; set; }
        public string AEmailCode { get; set; }
        public string ACommId { get; set; }
        public string AContNo { get; set; }
        public string AGovtAgency { get; set; }
        public string ATaxNo { get; set; }
        public string APractcingCert { get; set; }
        public string ALicDoc { get; set; }
        public string ACompBaseId { get; set; }
        public string AAgree { get; set; }
        public string AAgreeTm { get; set; }
    }
    public class CaseGuidModelD
    {
        public List<CaseGuidModelResult> results { get; set; }
    }
    public class CaseGuidModelRootObject
    {
        public CaseGuidModelD d { get; set; }
    }
}
