using EGAZT.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Serializable]
    [Preserve(AllMembers = true)]
    [DataContract]
    public class IBanManagementListModel
    {

        [DataContract]
        public class IbanListSetResult
        {

            [DataMember(Name = "FormGuid")]
            public string FormGuid { get; set; }

            [DataMember(Name = "Status")]
            public string Status { get; set; }

            [DataMember(Name = "StatusDesc")]
            public string StatusDesc { get; set; }

            [DataMember(Name = "Tin")]
            public string Tin { get; set; }

            [DataMember(Name = "AgreeFg")]
            public string AgreeFg { get; set; }

            [DataMember(Name = "IdtypeDesc")]
            public string IdtypeDesc { get; set; }

            [DataMember(Name = "ActiveIban")]
            public string ActiveIban { get; set; }

            [DataMember(Name = "Fbnum")]
            public string Fbnum { get; set; }

            [DataMember(Name = "VisibleUpdate")]
            public string VisibleUpdate { get; set; }

            [DataMember(Name = "Koinh")]
            public string Koinh { get; set; }

            [DataMember(Name = "Type")]
            public string Type { get; set; }

            [DataMember(Name = "Idnumber")]
            public string Idnumber { get; set; }

            [DataMember(Name = "Iban")]
            public string Iban { get; set; }

            [DataMember(Name = "Bankid")]
            public string Bankid { get; set; }

            [DataMember(Name = "Bkext")]
            public string Bkext { get; set; }

            [DataMember(Name = "EnableUpdate")]
            public string EnableUpdate { get; set; }

            [DataMember(Name = "Action")]
            public string Action { get; set; }
        }
        [DataContract]
        public class IbanListSet
        {

            [DataMember(Name = "results")]
            public List<IbanListSetResult> results { get; set; }
        }

        [DataContract]
        public class Result
        {

         
        [DataMember(Name = "Bankid")]
        public string Bankid { get; set; }

        [DataMember(Name = "Bkext")]
        public string Bkext { get; set; }
    }

        [DataContract]
        public class BankListSet
        {

            [DataMember(Name = "results")]
            public List<Result> results { get; set; }
        }

        [DataContract]
        public class IdNumberListSetResult
        {

        [DataMember(Name = "IdType")]
        public string IdType { get; set; }

        [DataMember(Name = "IdNumber")]
        public string IdNumber { get; set; }
    }

        [DataContract]
        public class IdNumberListSet
        {

            [DataMember(Name = "results")]
            public List<IdNumberListSetResult> results { get; set; }
        }

        [DataContract]
        public class IdTypeListSetResult
        {
        [DataMember(Name = "IdType")]
        public string IdType { get; set; }

        [DataMember(Name = "IdDesc")]
        public string IdDesc { get; set; }
    }

        [DataContract]
        public class IdTypeListSet
        {

            [DataMember(Name = "results")]
            public List<IdTypeListSetResult> results { get; set; }
        }

        [DataContract]
        public class IBanModelD
        {

            [DataMember(Name = "__metadata")]
            public Metadata __metadata { get; set; }

            [DataMember(Name = "Tin")]
            public string Tin { get; set; }

            [DataMember(Name = "Fbguid")]
            public string Fbguid { get; set; }

            [DataMember(Name = "Euser")]
            public string Euser { get; set; }

            [DataMember(Name = "Name")]
            public string Name { get; set; }

            [DataMember(Name = "IbanListSet")]
            public IbanListSet IbanListSet { get; set; }

            [DataMember(Name = "BankListSet")]
            public BankListSet BankListSet { get; set; }

            [DataMember(Name = "IdNumberListSet")]
            public IdNumberListSet IdNumberListSet { get; set; }

            [DataMember(Name = "IdTypeListSet")]
            public IdTypeListSet IdTypeListSet { get; set; }
        }

        [DataContract]
        public class IBanAccountManagementResponseModel
        {

            [DataMember(Name = "d")]
            public IBanModelD d { get; set; }
        }

    }


    [Serializable]
    [Preserve(AllMembers = true)]
    [DataContract]
    public class IBANPostRequest
    {
        public string Action { get; set; }
        public string Fbnum { get; set; }
        public string Koinh { get; set; }
        public string Type { get; set; }
        public string IdtypeDesc { get; set; }
        public string Idnumber { get; set; }
        public string Iban { get; set; }
        public string Bankid { get; set; }
        public string Bkext { get; set; }
        public string AgreeFg { get; set; }
        public string Tin { get; set; }
    }

    [Serializable]
    [Preserve(AllMembers = true)]
    [DataContract]
    public class IBANNewData
    {
        public Metadata __metadata { get; set; }
        public string FormGuid { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
        public string Tin { get; set; }
        public string AgreeFg { get; set; }
        public string IdtypeDesc { get; set; }
        public string ActiveIban { get; set; }
        public string Fbnum { get; set; }
        public string VisibleUpdate { get; set; }
        public string Koinh { get; set; }
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public string Iban { get; set; }
        public string Bankid { get; set; }
        public string Bkext { get; set; }
        public string EnableUpdate { get; set; }
        public string Action { get; set; }
    }



    public class IBANPostResponse
    {
        public IBANNewData d { get; set; }
    }
}
