using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EGAZT.Models
{
    public class RMContactDetailsBaseModel
    {
        [DataMember(Name = "d")]
        public RMSUPContactDetailsModel d { get; set; }
    }
    public class RMSUPContactDetailsModel
    {
        [DataMember(Name = "__metadata")]
        public Metadata __metadata { get; set; }

        [DataMember(Name = "Gpart")]
        public string Gpart { get; set; }

        [DataMember(Name = "TpEntityDesc")]
        public string TpEntityDesc { get; set; }

        [DataMember(Name = "Lang")]
        public string Lang { get; set; }

        [DataMember(Name = "ServiceName")]
        public string ServiceName { get; set; }

        [DataMember(Name = "Code")]
        public string Code { get; set; }

        [DataMember(Name = "Category")]
        public string Category { get; set; }

        [DataMember(Name = "TpEntity")]
        public string TpEntity { get; set; }

        [DataMember(Name = "TpMobile")]
        public string TpMobile { get; set; }

        [DataMember(Name = "RmName")]
        public string RmName { get; set; }

        [DataMember(Name = "RmMobile")]
        public string RmMobile { get; set; }

        [DataMember(Name = "RmEmail")]
        public string RmEmail { get; set; }

        [DataMember(Name = "SupName")]
        public string SupName { get; set; }

        [DataMember(Name = "SupMobile")]
        public string SupMobile { get; set; }

        [DataMember(Name = "SupEmail")]
        public string SupEmail { get; set; }
    }
}
