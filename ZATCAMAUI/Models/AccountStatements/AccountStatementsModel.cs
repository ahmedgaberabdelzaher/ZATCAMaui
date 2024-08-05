using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Mangers;
using static ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements.AccountStatementsPageViewModel;

namespace ZATCAMAUI.Models.AccountStatements
{

    public class AccountStatementsModel
    {
        public AccountStatementsModel()
        {

        }
    }
    public class GroupedAccountStatements : List<ASResult>
    {
        public DateTime? Date { get; set; }
        public string Month { get; set; }
        public GroupedAccountStatements(ASResult groupingItem, List<ASResult> groupingItems) : base()
        {
            Date = DateTime.Parse(groupingItem.FormattedBldat);
            Month = DateTime.Parse(groupingItem.FormattedBldat).ToString("MMMM");
            AddRange(groupingItems);
        }
    }
    
    public class DataForDownloadPage
    {
        public ASTaxpayerSelectedValues ASTaxpayerSelectedValues;
        public List<ObservableGroupCollection<string, ASResult>> GroupedDataForDownload;
        public ObservableCollection<ASResult> StatementsLineItems;
        public bool isNormalList;
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

        [JsonIgnore]
        public bool IsOpeningBalancePositive { get; set; }

        [JsonIgnore]
        public bool IsTotalBalancePositive { get; set; }

        /*[JsonProperty("CalType")]
        public string CalType { get; set; }*/
        [JsonIgnore]
        private string _CalType;
        [JsonProperty("CalType")]
        public string CalType
        {
            get
            {
                return _CalType;
            }
            set
            {
                _CalType = value;
                if (_CalType != null)
                {
                    App.CalType = _CalType;
                }
            }
        }
        private string openingBalance = string.Empty;

        [JsonIgnore]
        public string _open { get; set; }

        [JsonProperty("Open")]
        public string Open
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
                    OpeningAmount = UtilityManager.GetCommaSeparatedAmount(Math.Abs(amount).ToString());
                    if (amount.ToString().Contains("-"))
                    {
                        IsOpeningBalancePositive = false;
                    }
                    else
                    {
                        IsOpeningBalancePositive = true;
                    }
                }
            }
        }

        [JsonIgnore]
        private string _openingAmount = string.Empty;

        [JsonIgnore]
        public string OpeningAmount
        {
            get
            {
                return _openingAmount;
            }
            set
            {
                _openingAmount = value;
            }
        }

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
                    DebitAmount = UtilityManager.GetCommaSeparatedAmount(Math.Abs(amount).ToString());
                }
            }
        }

        public string _debitAmount = string.Empty;
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
                    string format = "$#,##0.00;$#,##0.00-;Zero";
                    decimal d = Convert.ToDecimal(_credit);
                    decimal amount = d;
                    amount.ToString(format);  //will return $24,508,975.94
                    CreditAmount = UtilityManager.GetCommaSeparatedAmount(Math.Abs(amount).ToString());
                }
            }
        }

        public string _creditAmount = string.Empty;
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
                    CloseAmount = UtilityManager.GetCommaSeparatedAmount(Math.Abs(amount).ToString()) + " " + AppResources.ZSAR;
                    if (amount.ToString().Contains("-"))
                    {
                        IsTotalBalancePositive = false;
                    }
                    else
                    {
                        IsTotalBalancePositive = true;
                    }
                }
            }
        }

        public string _CloseAmount = string.Empty;
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
    
    public partial class ASResult : ObservableRecipient
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }


        private string status;
        [JsonProperty("Status")]
        public string Status
        {
            get { return status; }
            set
            {
                status = value;

            }
        }

        [JsonIgnore]
        public Color StatusColor { get; set; }

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

        private string _StatusDesc;
        [JsonProperty("StatusDesc")]
        public string StatusDesc
        {
            get { return _StatusDesc; }
            set
            {
                _StatusDesc = value;

            }
        }

        [JsonProperty("TaxtypeDesc")]
        public string TaxtypeDesc { get; set; }

        [JsonProperty("BillDes")]
        public string BillDes { get; set; }

        [JsonIgnore]
        private string _betrh;

        [JsonProperty("Betrh")]
        public string Betrh
        {
            get
            {
                return _betrh;
            }
            set
            {
                _betrh = value;
                if (!string.IsNullOrEmpty(_betrh))
                {
                    string format = "$#,##0.00;-$#,##0.00;Zero";
                    decimal d = Convert.ToDecimal(_betrh);
                    decimal amount = d;
                    amount.ToString(format);  //will return $24,508,975.94
                    BetrhAmount = UtilityManager.GetCommaSeparatedAmount(Math.Abs(amount).ToString());
                }
            }
        }

        private string _betrhAmount = string.Empty;
        public string BetrhAmount
        {
            get
            {
                return _betrhAmount;
            }
            set
            {
                _betrhAmount = value;
            }
        }

        [JsonIgnore]
        private Color _StatusBG = (Color)Application.Current.Resources["BackgroundGray"];
        [JsonIgnore]
        public Color StatusBG
        {
            get
            {
                if (Betrh != null && double.Parse(Betrh) < 0)
                {
                    _StatusBG = Colors.Transparent;
                    return _StatusBG;
                }

                return _StatusBG;
            }
            set
            {

                _StatusBG = value;

                OnPropertyChanged("StatusBG");
            }
        }

        [JsonIgnore]
        private Color _AmountTextColor = (Color)Application.Current.Resources["Primary"];
        [JsonIgnore]
        public Color AmountTextColor
        {
            get
            {
                if (Betrh != null && double.Parse(Betrh) < 0)
                {
                    _AmountTextColor = Colors.Black;
                    return _AmountTextColor;
                }

                return _AmountTextColor;
            }
            set
            {

                _AmountTextColor = value;

                OnPropertyChanged("AmountTextColor");
            }
        }



        [JsonIgnore]
        public string FormattedBetrh { get; set; }


        //FormatedAbrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        //                    string[] dts = FormatedAbrzu.Split('-');
        //string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
        //FormatedAbrzu = date;

        [JsonIgnore]
        private DateTime? _Bldat;
        [JsonProperty("Bldat")]
        public DateTime? Bldat
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



                    if (App.CalType.Equals("G"))
                    {
                        FormattedBldat = _Bldat?.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                    else
                    {
                        FormattedBldat = _Bldat?.ToString("dd-MMMM-yyyy", new CultureInfo("ar-SA"));
                    }


                    //if (App.CalType.Equals("G"))
                    //{
                    //    FormattedBldat = string.Format(_Bldat?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));

                    //    string[] dts = FormattedBldat.Split(' ');
                    //    if (App.IsArabic) {

                    //        string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2]; 

                    //        FormattedBldat = date;
                    //    }
                    //    else {
                    //        string date = dts[0] + " "  + UtilityManager.GetMonthName(dts[1]) + " " + dts[2] ;

                    //        FormattedBldat = date;
                    //    }



                    //}
                    //else if (App.CalType.Equals("H"))
                    //{
                    //    FormattedBldat = string.Format(_Bldat?.ToString("d/M/yyyy", new CultureInfo("ar-SA")));

                    //    string[] dts = FormattedBldat.Split('/');
                    //    if (App.IsArabic)
                    //    {

                    //        string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                    //        FormattedBldat = date;
                    //    }
                    //    else
                    //    {
                    //        string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                    //        FormattedBldat = date;
                    //    }



                    //}
                    //else
                    //{
                    //    FormattedBldat = string.Format(_Bldat?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));

                    //    string[] dts = FormattedBldat.Split(' ');
                    //    if (App.IsArabic)
                    //    {

                    //        string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                    //        FormattedBldat = date;
                    //    }
                    //    else
                    //    {
                    //        string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                    //        FormattedBldat = date;
                    //    }
                    //}
                }
            }
        }
        [JsonIgnore]
        public string FormattedBldat { get; set; }


        [JsonIgnore]
        public string FormattedBldat2 { get; set; }

        [JsonIgnore]
        private DateTime? PeriodEndDt { get; set; }

        [JsonProperty("PeriodStartDt")]
        public DateTime? PeriodStartDt { get; set; }
        [JsonIgnore]
        private DateTime? _Bldat2 { get; set; }

        [JsonProperty("Bldat2")]
        public DateTime? Bldat2
        {
            get
            {
                return _Bldat2;
            }
            set
            {
                _Bldat2 = value;
                if (_Bldat2 != null)
                {
                    //if (App.CalType.Equals("G"))
                    //{
                    //    FormattedBldat2 = string.Format(_Bldat2?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));

                    //    string[] dts = FormattedBldat2.Split(' ');

                    //    if (App.IsArabic)
                    //    {

                    //        string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                    //        FormattedBldat2 = date;
                    //    }
                    //    else
                    //    {
                    //        string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                    //        FormattedBldat2 = date;
                    //    }
                    //}
                    //else
                    //{

                    //    FormattedBldat2 = string.Format(_Bldat2?.ToString("d/M/yyyy", new CultureInfo("ar-SA")));
                    //    string[] dts = FormattedBldat2.Split('/');
                    //    if (App.IsArabic)
                    //    {

                    //        string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                    //        FormattedBldat2 = date;
                    //    }
                    //    else
                    //    {
                    //        string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                    //        FormattedBldat2 = date;
                    //    }
                    //}
                    /*FormattedBldat2 = _Bldat2?.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    string[] dts = FormattedBldat2.Split('-');
                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    FormattedBldat2 = date;*/
                }
            }
        }

        //[JsonIgnore]
        //private DateTime _PeriodEndDt;
        //[JsonProperty("PeriodEndDt")]
        //public DateTime PeriodEndDt
        //{
        //    get
        //    {
        //        return _PeriodEndDt;
        //    }
        //    set
        //    {    
        //        if (_PeriodEndDt != null)
        //        {
        //            _PeriodEndDt = value;
        //            FormattedPeriodEndDate = _PeriodEndDt.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        //            string[] dts = FormattedPeriodEndDate.Split('-');
        //            string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
        //            FormattedPeriodEndDate = date;
        //        }
        //    }
        //}

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
    
    public class AccountStatementsListItem
    {
        public string taxType { get; set; }
        public string lastDate { get; set; }
        public string amount { get; set; }
    }
}
