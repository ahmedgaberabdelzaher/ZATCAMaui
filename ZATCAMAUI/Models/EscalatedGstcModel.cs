using System;
using System.Globalization;
using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    
    public class EscalatedGstcModel
    {
        public D d { get; set; }
        [Preserve(AllMembers = true)]
         public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Deferred
        {
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class TINSet
        {
            public Deferred __deferred { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class CaseDetailsResultSet
        {
            public Metadata __metadata { get; set; }
            [JsonIgnore]
            public string CaseDateString { get; set; }

            [JsonIgnore]
            public DateTime? _caseDate;
            [JsonProperty("CaseDate")]
            public DateTime? CaseDate
            {
                get
                {
                    return _caseDate;
                }
                set
                {
                    _caseDate = value;
                    if (_caseDate != null)
                    {
                        CaseDateString = _caseDate?.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    }
                    else
                    {
                        CaseDateString = " -- ";
                    }
                }
            }

            [JsonProperty("TotalObjectItemsAmt")]
            public string TotalObjectItemsAmt { get; set; }
            [JsonProperty("Tin")]
            public string Tin { get; set; }
            [JsonProperty("CaseId")]
            public string CaseId { get; set; }
            [JsonProperty("GrevienceNumber")]
            public string GrevienceNumber { get; set; }
            [JsonProperty("Name")]
            public string Name { get; set; }
            [JsonProperty("CaseNumber")]
            public string CaseNumber { get; set; }
            [JsonProperty("CaseSubject")]
            public string CaseSubject { get; set; }


            [JsonIgnore]
            private string _type = " -- ";
            [JsonProperty("Type")]
            public string Type
            {
                get
                {
                    return _type;
                }
                set
                {
                    _type = value;
                    if (!App.IsArabic)
                    {
                        ZType = Type;
                    }
                    else
                    {

                    }
                }
            }

            [JsonIgnore]
            private string _typeAr = " -- ";
            [JsonProperty("TypeAr")]
            public string TypeAr
            {
                get
                {
                    return _typeAr;
                }
                set
                {
                    _typeAr = value;
                    if (App.IsArabic)
                    {
                        ZType = TypeAr;
                    }
                }
            }


            [JsonIgnore]
            private string _statusName = " -- ";
            [JsonProperty("StatusName")]
            public string StatusName
            {
                get
                {
                    return _statusName;
                }
                set
                {
                    _statusName = value;
                    if (!App.IsArabic)
                    {
                        ZStatusName = StatusName;
                    }
                }
            }
            [JsonIgnore]
            private string _statusNameAr = " -- ";
            [JsonProperty("StatusNameAr")]
            public string StatusNameAr
            {
                get
                {
                    return _statusNameAr;
                }
                set
                {
                    _statusNameAr = value;
                    if (App.IsArabic)
                    {
                        ZStatusName = StatusNameAr;
                    }
                }
            }
            [JsonIgnore]
            private string _mileStone = "--";
            [JsonProperty("Milestone")]
            public string Milestone
            {
                get
                {
                    return _mileStone;
                }
                set
                {
                    _mileStone = value;
                    if (!App.IsArabic)
                    {
                        ZMileStone = Milestone;
                    }
                }
            }
            [JsonIgnore]
            private string _mileStoneAr = "--";
            [JsonProperty("MilestoneAr")]
            public string MilestoneAr
            {
                get
                {
                    return _mileStoneAr;
                }
                set
                {
                    _mileStoneAr = value;
                    if (App.IsArabic)
                    {
                        ZMileStone = MilestoneAr;
                    }
                }
            }
            [JsonIgnore]
            private string _degreeLitigation = "--";
            [JsonProperty("DegreeLitigation")]
            public string DegreeLitigation
            {
                get
                {
                    return _degreeLitigation;
                }
                set
                {
                    _degreeLitigation = value;
                    if (!App.IsArabic)
                    {
                        ZDegreeLitigation = DegreeLitigation;
                    }
                }
            }
            private string _degreeLitigationAr = "--";
            [JsonProperty("DegreeLitigationAr")]
            public string DegreeLitigationAr
            {
                get
                {
                    return _degreeLitigationAr;
                }
                set
                {
                    _degreeLitigationAr = value;
                    if (App.IsArabic)
                    {
                        ZDegreeLitigation = DegreeLitigationAr;
                    }
                }
            }

            [JsonProperty("CaseType")]
            public string CaseType { get; set; }

            [JsonIgnore]
            private string _casetypeDes = " -- ";
            [JsonProperty("CasetypeDes")]
            public string CasetypeDes
            {
                get
                {
                    return _casetypeDes;
                }
                set
                {
                    _casetypeDes = value;
                    if (!App.IsArabic)
                    {
                        ZCaseType = CasetypeDes;
                    }
                }
            }

            [JsonIgnore]
            private string _casetypeDesAr = " -- ";
            [JsonProperty("CasetypeDesAr")]
            public string CasetypeDesAr
            {
                get
                {
                    return _casetypeDesAr;
                }
                set
                {
                    _casetypeDesAr = value;
                    if (App.IsArabic)
                    {
                        ZCaseType = CasetypeDesAr;
                    }
                }
            }

            [JsonProperty("CaseClassification")]
            public string CaseClassification { get; set; }

            [JsonIgnore]
            private string _caseClassificationDes = "--";
            [JsonProperty("CaseClassificationDes")]
            public string CaseClassificationDes
            {
                get
                {
                    return _caseClassificationDes;
                }
                set
                {
                    _caseClassificationDes = value;
                    if (!App.IsArabic)
                    {
                        ZCaseClassificationDes = CaseClassificationDes;
                    }
                }
            }
            [JsonIgnore]
            private string _caseClassificationDesAr = "--";
            [JsonProperty("CaseClassificationDesAr")]
            public string CaseClassificationDesAr
            {
                get
                {
                    return _caseClassificationDesAr;
                }
                set
                {
                    _caseClassificationDesAr = value;
                    if (App.IsArabic)
                    {
                        ZCaseClassificationDes = CaseClassificationDesAr;
                    }
                }
            }


            [JsonProperty("Link")]
            public string Link { get; set; }
            [JsonProperty("TINSet")]
            public TINSet TINSet { get; set; }


            [JsonIgnore]
            public string ZMileStone { get; set; }

            [JsonIgnore]
            public string ZType { get; set; }

            [JsonIgnore]
            public string ZDegreeLitigation { get; set; }

            [JsonIgnore]
            public string ZCaseType { get; set; }

            [JsonIgnore]
            public string ZCaseClassificationDes { get; set; }

            [JsonIgnore]
            public string ZStatusName { get; set; }

        }
        [Preserve(AllMembers = true)]
        public class CaseDetailSet
        {
            public List<CaseDetailsResultSet> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("Code")]
            public string Code { get; set; }
            [JsonProperty("MessageType")]
            public string MessageType { get; set; }
            [JsonProperty("TotalCount")]
            public int TotalCount { get; set; }
            [JsonProperty("Tin")]
            public string Tin { get; set; }
            [JsonProperty("CaseDetailSet")]
            public CaseDetailSet CaseDetailSet { get; set; }
        }
    }
}
