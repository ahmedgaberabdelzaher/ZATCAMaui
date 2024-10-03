using System;
using System.Globalization;

using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class EscalatedGstcModel
    {
        [JsonProperty("data")]
        public D d { get; set; }

       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Deferred
        {
            public string uri { get; set; }
        }
       
        public class TINSet
        {
            public Deferred __deferred { get; set; }
        }
       
        public class CaseDetailsResultSet
        {
            public Metadata __metadata { get; set; }

            //   [JsonProperty("CaseDate")]
            //public DateTime CaseDate { get; set; }

            [JsonIgnore]
            public string CaseDateString { get; set; }

            [JsonIgnore]
            public DateTime? _caseDate;
            [JsonProperty("caseDate")]
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

            [JsonProperty("totalObjectItemsAmount")]
            public string TotalObjectItemsAmt { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("caseId")]
            public string CaseId { get; set; }
            [JsonProperty("grevienceNumber")]
            public string GrevienceNumber { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("caseNumber")]
            public string CaseNumber { get; set; }
            [JsonProperty("caseSubject")]
            public string CaseSubject { get; set; }


            [JsonIgnore]
            private string _type = " -- ";
            [JsonProperty("type")]
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
            [JsonProperty("typeArabic")]
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
                    else
                    {

                    }
                }
            }


            [JsonIgnore]
            private string _statusName = " -- ";
            [JsonProperty("statusName")]
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
            [JsonProperty("statusNameArabic")]
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
            [JsonProperty("milestone")]
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
            [JsonProperty("milestoneArabic")]
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
            [JsonProperty("degreeLitigation")]
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
            [JsonProperty("degreeLitigationArabic")]
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

            [JsonProperty("caseType")]
            public string CaseType { get; set; }

            [JsonIgnore]
            private string _casetypeDes = " -- ";
            [JsonProperty("casetypeDescription")]
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
            [JsonProperty("casetypeDescriptionArabic")]
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

            [JsonProperty("caseClassification")]
            public string CaseClassification { get; set; }

            [JsonIgnore]
            private string _caseClassificationDes = "--";
            [JsonProperty("caseClassificationDescription")]
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
            [JsonProperty("caseClassificationDescriptionArabic")]
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


            [JsonProperty("link")]
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
       
        public class CaseDetailSet
        {
            [JsonProperty("results")]
            public List<CaseDetailsResultSet> results { get; set; }
        }
       
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("code")]
            public string Code { get; set; }
            [JsonProperty("messageType")]
            public string MessageType { get; set; }
            [JsonProperty("totalCount")]
            public int TotalCount { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("caseDetailSet")]
            public CaseDetailSet CaseDetailSet { get; set; }
        }



    }
}
