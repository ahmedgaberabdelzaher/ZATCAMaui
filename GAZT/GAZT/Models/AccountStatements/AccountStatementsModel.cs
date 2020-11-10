using System;
using System.Globalization;
using GAZT.Manager;
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

    public class ASTaxpayerSelectedValues
    {
        public string TaxType { get; set; }
        public string StatementFilter { get; set; }
        public string Year { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
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
        public string StatementFilter { get; set; }

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

        private string _debit = string.Empty;
        [JsonProperty("Debit")]
        public string Debit
        {
            get
            {
                return _debit;
            }
            set
            {
                _debit = value;
                if (!string.IsNullOrEmpty(_debit))
                {
                    string format = "$#,##0.00;-$#,##0.00;Zero";
                    decimal d = Convert.ToDecimal(_debit);
                    decimal amount = d;
                    amount.ToString(format);  //will return $24,508,975.94
                    DebitAmount = UtilityManager.GetCommaSeparatedAmount(amount.ToString()) + " " + AppResources.ZSAR;
                }
            }
        }

        public string _debitAmount = String.Empty;
        public string DebitAmount
        {
            get
            {
                return _debitAmount;
            }
            set
            {
                _debitAmount = value;
            }
        }


        private string _credit = string.Empty;
        [JsonProperty("Credit")]
        public string Credit
        {
            get
            {
                return _credit;
            }
            set
            {
                _credit = value;
                if (!string.IsNullOrEmpty(_credit))
                {
                    string format = "$#,##0.00;-$#,##0.00;Zero";
                    decimal d = Convert.ToDecimal(_credit);
                    decimal amount = d;
                    amount.ToString(format);  //will return $24,508,975.94
                    CreditAmount = UtilityManager.GetCommaSeparatedAmount(amount.ToString()) + " " + AppResources.ZSAR;
                }
            }
        }

        public string _creditAmount = String.Empty;
        public string CreditAmount
        {
            get
            {
                return _creditAmount;
            }
            set
            {
                _creditAmount = value;
            }
        }

        private string _close = string.Empty;
        [JsonProperty("Close")]
        public string Close
        {
            get
            {
                return _close;
            }
            set
            {
                _close = value;
                if (!string.IsNullOrEmpty(_close))
                {
                    string format = "$#,##0.00;-$#,##0.00;Zero";
                    decimal d = Convert.ToDecimal(_close);
                    decimal amount = d;
                    amount.ToString(format);  //will return $24,508,975.94
                    CloseAmount = UtilityManager.GetCommaSeparatedAmount(amount.ToString()) + " " + AppResources.ZSAR;
                }
            }
        }

        public string _CloseAmount = String.Empty;
        public string CloseAmount
        {
            get
            {
                return _CloseAmount;
            }
            set
            {
                _CloseAmount = value;
            }
        }

        [JsonProperty("StatmenetLineItemsSet")]
        public StatmenetLineItemsSet StatmenetLineItemsSet { get; set; }

        [JsonProperty("TaxRelationSet")]
        public TaxRelationSet TaxRelationSet { get; set; }
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
        //FormatedAbrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        //                    string[] dts = FormatedAbrzu.Split('-');
        //string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
        //FormatedAbrzu = date;


        [JsonIgnore]
        private DateTime _Bldat;
        [JsonProperty("Bldat")]
        public DateTime Bldat
        {
            get
            {
                return _Bldat;
            }
            set
            {
                _Bldat = value;
                if (_Bldat != null)
                {


                    FormattedBldat = _Bldat.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    string[] dts = FormattedBldat.Split('-');
                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    FormattedBldat = date;

                }
            }
        }
        [JsonIgnore]
        public string FormattedBldat { get; set; }
        [JsonProperty("Bldat2")]
        public DateTime Bldat2 { get; set; }

        [JsonProperty("Faedn")]
        public object Faedn { get; set; }
        [JsonIgnore]
        private DateTime _PeriodEndDt;
        [JsonProperty("PeriodEndDt")]
        public DateTime PeriodEndDt
        {
            get
            {
                return _PeriodEndDt;
            }
            set
            {
                _PeriodEndDt = value;
                if (_PeriodEndDt != null)
                {


                    FormattedPeriodEndDate = _PeriodEndDt.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    string[] dts = FormattedPeriodEndDate.Split('-');
                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    FormattedPeriodEndDate = date;

                }
            }
        }
        [JsonIgnore]
        public string FormattedPeriodEndDate { get; set; }

        public bool IsTotalBalanceVisile { get; set; }

        public string OpeningBalance { get; set; }
        public string ClosingBalance { get; set; }
        public string TotalBalance { get; set; }
    }

    public partial class TaxRelationSet
    {
        [JsonProperty("results")]
        public TaxRelationSetResult[] Results { get; set; }
    }

    public partial class TaxRelationSetResult
    {
        [JsonIgnore]
        public int DisplayId { get; set; }

        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Spras")]
        public string Spras { get; set; }

        [JsonProperty("TaxType")]
        public string TaxType { get; set; }

        [JsonProperty("FromDate")]
        public string FromDate { get; set; }

        [JsonProperty("StatementFilter")]
        public string StatementFilter { get; set; }

        [JsonProperty("Counter")]
        public string Counter { get; set; }

        [JsonProperty("Contractobject")]
        public string Contractobject { get; set; }

        [JsonProperty("AbtypPs")]
        public string AbtypPs { get; set; }

        [JsonProperty("Txt30")]
        public string Txt30 { get; set; }

        [JsonProperty("ToDate")]
        public string ToDate { get; set; }
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

    public class ASFilters
    {
        public string FilterHeader { get; set; }
        public string SortAscending { get; set; }
        public string SortDescending { get; set; }
    }
}
