using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace EGAZT.Models.AccountStatements
{
    public class AccountStatementsModel
    {
        public AccountStatementsModel()
        {
        }
    }

    public class ASReturnTypes
    {
        public string TaxType { get; set; }
        public string Id { get; set; }
    }

    public class ASChipModel
    {
        public string TemplateType { get; set; }
        public string Text { get; set; }
        public ImageSource ImageSource { get; set; }
    }

    public partial class ASTabIdentification
    {
        [JsonProperty("d")]
        public ASTabIdentificationData D { get; set; }
    }

    public partial class ASTabIdentificationData
    {
        [JsonProperty("__metadata")]
        public MetadataAS Metadata { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("Fbguid")]
        public string Fbguid { get; set; }

        [JsonProperty("Direct")]
        public string Direct { get; set; }

        [JsonProperty("Indirect")]
        public string Indirect { get; set; }
    }

    public partial class MetadataAS
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class ASRevenueDropDownSet
    {
        [JsonProperty("d")]
        public ASRevenueDropDownSetData D { get; set; }
    }

    public partial class ASRevenueDropDownSetData
    {
        [JsonProperty("results")]
        public ASRevenueDropDownSetDataResults[] Results { get; set; }
    }

    public partial class ASRevenueDropDownSetDataResults
    {
        [JsonProperty("__metadata")]
        public MetadataAS Metadata { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("Langz")]
        public string Langz { get; set; }

        [JsonProperty("StatementFilter")]
        public string StatementFilter { get; set; }

        [JsonProperty("Fbguid")]
        public string Fbguid { get; set; }

        [JsonProperty("TaxType")]
        public string TaxType { get; set; }

        [JsonProperty("Txt30")]
        public string Txt30 { get; set; }
    }

    public partial class ASStatementHeaderSet
    {
        [JsonProperty("d")]
        public ASStatementHeaderSetData D { get; set; }
    }

    public partial class ASStatementHeaderSetData
    {
        [JsonProperty("__metadata")]
        public MetadataAS Metadata { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("CalType")]
        public string CalType { get; set; }

        [JsonProperty("Open")]
        public string Open { get; set; }

        [JsonProperty("Close")]
        public string Close { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("Lang")]
        public string Lang { get; set; }

        [JsonProperty("Fbguid")]
        public string Fbguid { get; set; }

        [JsonProperty("StatementFilter")]
        public string StatementFilter { get; set; }

        [JsonProperty("TaxType")]
        public string TaxType { get; set; }

        [JsonProperty("TotalAmount")]
        public string TotalAmount { get; set; }

        [JsonProperty("FiscalYear")]
        public string FiscalYear { get; set; }

        [JsonProperty("StatmenetLineItemsSet")]
        public StatmenetLineItemsSet StatmenetLineItemsSet { get; set; }
    }

    public partial class StatmenetLineItemsSet
    {
        [JsonProperty("results")]
        public ASResult[] Results { get; set; }
    }

    public partial class ASResult
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("TaxType")]
        public string TaxType { get; set; }

        [JsonProperty("Desc")]
        public string Desc { get; set; }

        [JsonProperty("Opbel")]
        public string Opbel { get; set; }

        [JsonProperty("Fbnum")]
        public string Fbnum { get; set; }

        [JsonProperty("Vtre2")]
        public string Vtre2 { get; set; }

        [JsonProperty("Persl")]
        public string Persl { get; set; }

        [JsonProperty("PeriodTxt")]
        public string PeriodTxt { get; set; }

        [JsonProperty("Waers")]
        public string Waers { get; set; }

        [JsonProperty("StatusDesc")]
        public string StatusDesc { get; set; }

        [JsonProperty("TaxtypeDesc")]
        public string TaxtypeDesc { get; set; }

        [JsonProperty("BillDes")]
        public string BillDes { get; set; }

        [JsonProperty("Betrh")]
        public string Betrh { get; set; }

        [JsonProperty("Bldat")]
        public string Bldat { get; set; }

        [JsonProperty("Bldat2")]
        public string Bldat2 { get; set; }

        [JsonProperty("Faedn")]
        public object Faedn { get; set; }

        [JsonProperty("PeriodEndDt")]
        public string PeriodEndDt { get; set; }
    }

    public partial class ASYearValuesHeader
    {
        [JsonProperty("d")]
        public ASYearValuesData D { get; set; }
    }

    public partial class ASYearValuesData
    {
        [JsonProperty("results")]
        public ASYearValuesResults[] Results { get; set; }
    }

    public partial class ASYearValuesResults
    {
        [JsonProperty("__metadata")]
        public MetadataAS Metadata { get; set; }

        [JsonProperty("SeqNo")]
        public string SeqNo { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("TaxType")]
        public string TaxType { get; set; }

        [JsonProperty("Persl")]
        public string Persl { get; set; }

        [JsonProperty("StatementFilter")]
        public string StatementFilter { get; set; }

        [JsonProperty("Fguid")]
        public string Fguid { get; set; }
    }
}
