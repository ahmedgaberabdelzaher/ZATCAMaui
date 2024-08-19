using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    public class ZakatExemptionListModel
    {
        [JsonProperty("data")]
        public ZakatExemptionListResponse D { get; set; }

        public class Metadata
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class FbnumListSetResult
        {
            public Metadata Metadata { get; set; }

            [JsonProperty("lineNumber")]
            public int LineNo { get; set; }

            [JsonProperty("isErrorDisplayed")]
            public bool DispErr { get; set; }

            [JsonProperty("systemCode")]
            public string Mandt { get; set; }

            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }

            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }

            [JsonProperty("rankingOrder")]
            public string RankingOrder { get; set; }

            [JsonProperty("TIN")]
            public string Tin { get; set; }

            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }

            [JsonProperty("periodkey")]
            public string Persl { get; set; }

            [JsonProperty("entityType")]
            public string EnType { get; set; }

            [JsonProperty("typeOfTrust")]
            public string TypTrust { get; set; }

            [JsonProperty("entityNature")]
            public string EnNature { get; set; }

            [JsonProperty("offSpringPercentage")]
            public string OffSprP { get; set; }

            [JsonProperty("charityPercentage")]
            public string CharityP { get; set; }

            [JsonProperty("lastFormBundleNumber")]
            public string LastAFbnum { get; set; }

            [JsonProperty("LastADate")]
            public string LastADate { get; set; }

            [JsonProperty("lastDueAmount")]
            public string LastADueAmt { get; set; }

            [JsonProperty("lastPaidAmount")]
            public string LastAPaidAmt { get; set; }

            [JsonProperty("returnId")]
            public string ReturnId { get; set; }

            [JsonProperty("recommendation01")]
            public string Recom01 { get; set; }

            [JsonProperty("recommendation02")]
            public string Recom02 { get; set; }

            [JsonProperty("decision")]
            public string Decision { get; set; }

            [JsonProperty("declaration")]
            public string SuDecfg { get; set; }

            [JsonProperty("createdBy")]
            public string Erfuser { get; set; }

            [JsonProperty("createdOn")]
            public object Erfdate { get; set; }

            [JsonProperty("createdAt")]
            public string Erftime { get; set; }

            [JsonProperty("changedBy")]
            public string Aenuser { get; set; }

            [JsonProperty("changedOn")]
            public object Aendate { get; set; }

            [JsonProperty("changedAt")]
            public string Aentime { get; set; }

            [JsonProperty("statusDescription")]
            public string StatusDesc { get; set; }

            [JsonProperty("isGoLive")]
            public bool GoLive { get; set; }

            [JsonProperty("submissionDate")]
            public DateTime SubDate { get; set; }

            [JsonIgnore]
            private string _fbsta;
            [JsonProperty("formBundleStatus")]
            public string Fbsta {
                get
                {
                    return _fbsta;
                }
                set
                {
                    _fbsta = value;
                    if(_fbsta != null && _fbsta.Equals("IP014"))
                    {
                        IsDownloadCert = true;
                    }
                }
            }

            [JsonProperty("formBundleUserStatus")]
            public string Fbust { get; set; }

            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }

            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }


            public bool IsDownloadCert { get; set; }
        }

        public class StatusSetResult
        {
            public Metadata Metadata { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }

            [JsonProperty("formBundleUserStatus")]
            public string Fbust { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }

        public class FbnumListSet
        {
            [JsonProperty("results")]
            public List<FbnumListSetResult> Results { get; set; }
        }

        public class StatusSet
        {
            [JsonProperty("results")]
            public List<StatusSetResult> Results { get; set; }
        }

        public class ZakatExemptionListResponse
        {
            public Metadata Metadata { get; set; }

            [JsonProperty("language")]
            public string Langz { get; set; }

            [JsonProperty("systemCode")]
            public string Mandt { get; set; }

            [JsonProperty("userType")]
            public string UserTyp { get; set; }

            [JsonProperty("TIN")]
            public string Tin { get; set; }

            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }

            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }

            [JsonProperty("statusCode")]
            public string Status { get; set; }

            [JsonProperty("GFlg")]
            public bool GFlg { get; set; }

            [JsonProperty("formBundleNumbers")]
            public List<FbnumListSetResult> FbnumListSet { get; set; }

            [JsonProperty("statuses")]
            public List<StatusSetResult> StatusSet { get; set; }
        }
    }
}
