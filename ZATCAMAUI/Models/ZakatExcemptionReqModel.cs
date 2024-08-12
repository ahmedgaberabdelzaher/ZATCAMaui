using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    public class ZakatExcemptionReqModel
    {
        
        public List<MyMetaData> __metadata { get; set; }
        //[JsonProperty("declaration")]
        public string Decfg { get; set; }

        [JsonProperty("createdAt")]
        public string Erftime { get; set; }
        [JsonProperty("recommendation01")]
        public string Recom01 { get; set; }
        [JsonProperty("declaration")]
        public string SuDecfg { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtypz { get; set; }
        [JsonProperty("recommendation02")]
        public string Recom02 { get; set; }
        [JsonProperty("formBundleStatus")]
        public string Fbustz { get; set; }
        [JsonProperty("systemCode")]
        public string Mandtz { get; set; }
        [JsonProperty("decision")]
        public string Decision { get; set; }
        [JsonProperty("transactionType")]
        public string TransactionTypez { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("changedAt")]
        public string Aentime { get; set; }
        [JsonProperty("edit")]
        public string EditFgz { get; set; }
        //[JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }
        public string Mandt { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("operation")]
        public string Operationz { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumberz { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("returnId")]
        public string ReturnIdz { get; set; }
        [JsonProperty("TINName")]
        public string TinName { get; set; }
        [JsonProperty("CRNumber")]
        public string CrNumber { get; set; }
        [JsonProperty("userName")]
        public string Officerz { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        public string Gpartz { get; set; }
        [JsonProperty("periodKey")]
        public string Persl { get; set; }
        [JsonProperty("statusCode")]
        public string Statusz { get; set; }
        [JsonProperty("activityName")]
        public string Actnm { get; set; }
        public string UserTypz { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        public string TxnTpz { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("formProcess")]
        public string Formprocz { get; set; }
        [JsonProperty("inputChannel")]
        public string Inpch { get; set; }
        public string OfficerTz { get; set; }
        [JsonProperty("createdBy")]
        public string Erfuser { get; set; }
        [JsonProperty("sourceApplication")]
        public string SrcAppz { get; set; }
        [JsonProperty("changedBy")]
        public string Aenuser { get; set; }
        [JsonProperty("channel")]
        public string InChannelz { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [JsonProperty("entityType")]
        public string EnType { get; set; }
        public string entityCategory { get; set; }
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
        public string ReturnId { get; set; }
        [JsonProperty("lastDate")]
        public string LastADate { get; set; }
        [JsonProperty("lastDueAmount")]
        public string LastADueAmt { get; set; }
        [JsonProperty("lastPaidAmount")]
        public string LastAPaidAmt { get; set; }
        [JsonProperty("CRDate")]
        public object CrDate { get; set; }

        // public object CommDate { get; set; }
        [JsonProperty("createdOn")]
        public object Erfdate { get; set; }
        [JsonProperty("changedOn")]
        public object Aendate { get; set; }
        [JsonProperty("items")]
        public List<object> LItemSet { get; set; }
        [JsonProperty("offNotes")]
        public List<Off_notesSet> Off_notesSet { get; set; }
        [JsonProperty("attachments")]
        public List<object> AttDetSet { get; set; }
        [JsonProperty("periodKeys")]
        public List<object> PeriodKeySet { get; set; }
        [JsonProperty("entities")]
        public List<object> EntityListSet { get; set; }
        [JsonProperty("recommendations02")]
        public List<object> Recom_02Set { get; set; }
        [JsonProperty("recommendations01")]
        public List<object> Recom_01Set { get; set; }
        [JsonProperty("decisions")]
        public List<object> DecisionSet { get; set; }
        [JsonProperty("requestButtons")]
        public List<object> ZerqBtnSet { get; set; }

    }

    public class Off_notesSet
    {
        public string Notenoz { get; set; }

        public string Refnamez { get; set; }

        public string DataVersionz { get; set; }

        public string XInvoicez { get; set; }

        public string XObsoletez { get; set; }

        public string Rcodez { get; set; }

        public string Erfusrz { get; set; }

        public object Erfdtz { get; set; }

        public object Erftmz { get; set; }

        public string ByGpartz { get; set; }

        public string AttByz { get; set; }

        public string Noteno { get; set; }

        public int Lineno { get; set; }

        public int ElemNo { get; set; }

        public string Tdformat { get; set; }

        public string Tdline { get; set; }
    }

    public class MyMetaData
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class ZakatExemptionRequestResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Metadata
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class LItemSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class Result
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("Notenoz")]
            public string Notenoz { get; set; }

            [JsonProperty("Refnamez")]
            public string Refnamez { get; set; }

            [JsonProperty("XInvoicez")]
            public string XInvoicez { get; set; }

            [JsonProperty("XObsoletez")]
            public string XObsoletez { get; set; }

            [JsonProperty("Rcodez")]
            public string Rcodez { get; set; }

            [JsonProperty("Erfusrz")]
            public string Erfusrz { get; set; }

            [JsonProperty("Erfdtz")]
            public object Erfdtz { get; set; }

            [JsonProperty("Erftmz")]
            public string Erftmz { get; set; }

            [JsonProperty("AttByz")]
            public string AttByz { get; set; }

            [JsonProperty("ByPusrz")]
            public string ByPusrz { get; set; }

            [JsonProperty("ByGpartz")]
            public string ByGpartz { get; set; }

            [JsonProperty("DataVersionz")]
            public string DataVersionz { get; set; }

            [JsonProperty("Namez")]
            public string Namez { get; set; }

            [JsonProperty("Noteno")]
            public string Noteno { get; set; }

            [JsonProperty("Lineno")]
            public int Lineno { get; set; }

            [JsonProperty("ElemNo")]
            public int ElemNo { get; set; }

            [JsonProperty("Tdformat")]
            public string Tdformat { get; set; }

            [JsonProperty("Tdline")]
            public string Tdline { get; set; }

            [JsonProperty("Sect")]
            public string Sect { get; set; }

            [JsonProperty("Strdt")]
            public string Strdt { get; set; }

            [JsonProperty("Strtime")]
            public string Strtime { get; set; }

            [JsonProperty("Strline")]
            public string Strline { get; set; }
        }

        public class OffNotesSet
        {
            [JsonProperty("results")]
            public List<Result> Results { get; set; }
        }

        public class AttDetSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class PeriodKeySet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class EntityListSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class Recom02Set
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class Recom01Set
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class DecisionSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class ZerqBtnSet
        {
            [JsonProperty("results")]
            public List<object> Results { get; set; }
        }

        public class D
        {
            public string Decfg { get; set; }

            [JsonProperty("createdAt")]
            public string Erftime { get; set; }
            [JsonProperty("recommendation01")]
            public string Recom01 { get; set; }
            [JsonProperty("declaration")]
            public string SuDecfg { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtypz { get; set; }
            [JsonProperty("recommendation02")]
            public string Recom02 { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbustz { get; set; }
            [JsonProperty("systemCode")]
            public string Mandtz { get; set; }
            [JsonProperty("decision")]
            public string Decision { get; set; }
            [JsonProperty("transactionType")]
            public string TransactionTypez { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("changedAt")]
            public string Aentime { get; set; }
            [JsonProperty("edit")]
            public string EditFgz { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnumz { get; set; }
            public string Mandt { get; set; }
            [JsonProperty("portalUser")]
            public string PortalUsrz { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("language")]
            public string Langz { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("operation")]
            public string Operationz { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumberz { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("returnId")]
            public string ReturnIdz { get; set; }
            [JsonProperty("TINName")]
            public string TinName { get; set; }
            [JsonProperty("CRNumber")]
            public string CrNumber { get; set; }
            [JsonProperty("userName")]
            public string Officerz { get; set; }
            public string Fbnum { get; set; }
            public string Gpartz { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("statusCode")]
            public string Statusz { get; set; }
            [JsonProperty("activityName")]
            public string Actnm { get; set; }
            public string UserTypz { get; set; }
            [JsonProperty("mobile")]
            public string Mobile { get; set; }
            public string TxnTpz { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("formProcess")]
            public string Formprocz { get; set; }
            [JsonProperty("inputChannel")]
            public string Inpch { get; set; }
            public string OfficerTz { get; set; }
            [JsonProperty("createdBy")]
            public string Erfuser { get; set; }
            [JsonProperty("sourceApplication")]
            public string SrcAppz { get; set; }
            [JsonProperty("changedBy")]
            public string Aenuser { get; set; }
            [JsonProperty("channel")]
            public string InChannelz { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }
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
            public string ReturnId { get; set; }
            [JsonProperty("lastDate")]
            public string LastADate { get; set; }
            [JsonProperty("lastDueAmount")]
            public string LastADueAmt { get; set; }
            [JsonProperty("lastPaidAmount")]
            public string LastAPaidAmt { get; set; }
            [JsonProperty("CRDate")]
            public string CrDate { get; set; }

            // public object CommDate { get; set; }
            [JsonProperty("createdOn")]
            public string Erfdate { get; set; }
            [JsonProperty("changedOn")]
            public string Aendate { get; set; }

            [JsonProperty("items")]
            public List<object> LItemSet { get; set; }

            [JsonProperty("offNotes")]
            public List<Result> OffNotesSet { get; set; }

            [JsonProperty("attachments")]
            public List<object> AttDetSet { get; set; }

            [JsonProperty("periodKeys")]
            public List<object> PeriodKeySet { get; set; }

            [JsonProperty("entities")]
            public List<object> EntityListSet { get; set; }

            [JsonProperty("recommendations02")]
            public List<object> Recom02Set { get; set; }

            [JsonProperty("recommendations01")]
            public List<object> Recom01Set { get; set; }

            [JsonProperty("decisions")]
            public List<object> DecisionSet { get; set; }

            [JsonProperty("requestButtons")]
            public List<object> ZerqBtnSet { get; set; }
        }

        public class Root
        {
            [JsonProperty("result")]
            public D D { get; set; }
        }


    }
}
