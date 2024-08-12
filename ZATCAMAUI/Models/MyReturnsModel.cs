using System.Globalization;
using Foundation;
using ZATCAMAUI.Core.Mangers;
namespace ZATCAMAUI.Models
{

    [Preserve(AllMembers = true)]
    public class MyReturnsModel
    {
    }
    [Preserve(AllMembers = true)]
    public class MyReturnsMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class MyReturnsResult
    {
        public Metadata __metadata { get; set; }
        public string formBundleGUID { get; set; }
        public string language { get; set; }
        public string TIN { get; set; }
        public string goLive { get; set; }
        public string periodStartDate { get; set; }
        public string periodEndDate { get; set; }
        public string paymentStatus { get; set; }
        public string systemStatus { get; set; }
        public string taxTypeDescription { get; set; }
        public bool isOpen { get; set; }
        public string correspondenceKey { get; set; }
        public string message { get; set; }
        public string sadadBillNumber1 { get; set; }
        public string sadadBillNumber2 { get; set; }
        public string formBundleNumber { get; set; }
        public string sortperiod { get; set; }
        public string taxType { get; set; }
        public string dueStatus { get; set; }
        public string returnStatusDescription { get; set; }
        public string status { get; set; }
        public string inboundCorrespondenceType { get; set; }
        public string inboundCorrespondenceText { get; set; }
        public string formBundleType { get; set; }
        public string formBundleDescription { get; set; }
        public string taxPeriod { get; set; }
        public string periodKey { get; set; }
        public string contractReference { get; set; }
        public string clearingReason { get; set; }
        public string userStatus { get; set; }
        public string error2064 { get; set; }
        public string CR2215GoLive { get; set; }


        private string _CalendarTyp;
        public string calendarType
        {
            get
            {
                return _CalendarTyp;
            }
            set
            {
                _CalendarTyp = value;
                if (_CalendarTyp != null)
                {
                    if (periodStartDateCharacter != null)
                    {
                        try
                        {
                            if (!calendarType.Equals("G"))
                            {
                                //string[] dts = null;
                                //periodStartDateCharacter = UtilityManager.ConvertToGreg(periodStartDateCharacter);
                            }
                            string[] dts = periodStartDateCharacter.Split('-');
                            // FormatedAbrzu = dts[2] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[0];
                            //string[] dts = periodStartDateCharacter.Split('/');
                            string DUEdate = dts[2] + "/" + dts[1] + "/" + dts[0];
                            FormatedAbrzu = UtilityManager.FormatAccordingToDeviceHijriEnglish(DUEdate);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }
                    if (periodEndDateCharacter != null)
                    {
                        if (!calendarType.Equals("H"))
                        {
                            //string[] dts = null;
                            //periodEndDateCharacter = UtilityManager.ConvertToGreg(periodEndDateCharacter);
                        }
                        string[] dts = periodEndDateCharacter.Split('-');
                        //FormatedAbrzo = dts[2] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[0];
                        //string[] dts = periodEndDateCharacter.Split('/');

                        string DUEdate = dts[2] + "/" + dts[1] + "/" + dts[0];
                        FormatedAbrzo = UtilityManager.FormatAccordingToDeviceHijriEnglish(DUEdate);
                    }
                    if (dueDate != null)
                    {
                        try
                        {
                            DateTime date = DateTime.Parse(dueDate);
                            FormatedSingleDueDate = date.ToString("dd-MMMM-yyyy");
                        }
                        catch (Exception ex)
                        {
                            FormatedSingleDueDate = "";
                        }
                    }
                }

            }
        }




        public string statusDescription
        {
            get; set;
        }

        public string StatusMessage
        {
            get; set;
        }

        //private DateTime _abrzu;
        //public DateTime Abrzu
        //{
        //    get
        //    {
        //        return _abrzu;
        //    }
        //    set
        //    {
        //        _abrzu = value;
        //        if (_abrzu != null)
        //        {
        //            if (CalendarTyp != null)
        //            {
        //                if (CalendarTyp.Equals("G"))
        //                {

        //                    FormatedAbrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        //                    string[] dts = FormatedAbrzu.Split('-');
        //                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
        //                    FormatedAbrzu = date;
        //                }

        //            }  
        //        }
        //    }
        //}
        private string _AbrzuC;
        public string periodStartDateCharacter
        {
            get
            {
                return _AbrzuC;
            }
            set
            {
                _AbrzuC = value;
                if (_AbrzuC != null && _AbrzuC != "Invalid date")
                {
                    if (calendarType != null)
                    {
                        if (!calendarType.Equals("G"))
                        {
                            //string[] dts = null;
                            _AbrzuC = UtilityManager.ConvertToGreg(value);
                        }
                        string[] dts = _AbrzuC.Split('-');
                        FormatedAbrzu = dts[2] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[0];
                    }
                }
            }
        }
        //private DateTime _abrzo;
        //public DateTime Abrzo
        //{
        //    get
        //    {
        //        return _abrzo;
        //    }
        //    set
        //    {
        //        _abrzo = value;
        //        if (_abrzo != null)
        //        {
        //            if (CalendarTyp != null)
        //            {
        //                if (CalendarTyp.Equals("G"))
        //                {

        //                    FormatedAbrzo = _abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
        //                    string[] dts = FormatedAbrzo.Split('-');
        //                    string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
        //                    FormatedAbrzo = date;
        //                }

        //            }

        //        }
        //    }
        //}
        private string _AbrzoC;
        public string periodEndDateCharacter
        {
            get
            {
                return _AbrzoC;
            }
            set
            {
                _AbrzoC = value;
                if (_AbrzoC != null && _AbrzoC != "Invalid date")
                {
                    if (calendarType != null)
                    {
                        if (!calendarType.Equals("G"))
                        {
                            //string[] dts = null;
                            _AbrzoC = UtilityManager.ConvertToGreg(value);
                        }
                        string[] dts = _AbrzuC.Split('-');
                        FormatedAbrzo = dts[2] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[0];
                    }
                }
            }
        }

        private string _dueDTC;
        public string dueDate
        {
            get
            {
                return _dueDTC;
            }
            set
            {
                _dueDTC = value;
                if (_dueDTC != null)
                {
                    if (calendarType != null)
                    {
                        try
                        {
                            DateTime date = DateTime.Parse(_dueDTC);
                            FormatedSingleDueDate = date.ToString("dd-MMMM-yyyy");
                        }
                        catch (Exception ex)
                        {
                            FormatedSingleDueDate = "";
                        }
                    }

                }

            }
        }


        private string _formatedSingleDueDate;
        public string FormatedSingleDueDate
        {
            get
            {
                return _formatedSingleDueDate;
            }
            set
            {
                _formatedSingleDueDate = value;
            }
        }
        private string _formatedAbrzu;
        public string FormatedAbrzu
        {
            get
            {
                return _formatedAbrzu;
            }
            set
            {
                _formatedAbrzu = value;
            }
        }
        private string _formatedAbrzo;
        public string FormatedAbrzo
        {
            get
            {
                return _formatedAbrzo;
            }
            set
            {
                _formatedAbrzo = value;
            }
        }
    }

    [Preserve(AllMembers = true)]
    public class MyReturnsD
    {
        public List<MyReturnsResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class MyReturnsRootObject
    {
        public List<MyReturnsResult> ICRReturns { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ReturnTypes
    {
        public string TaxType { get; set; }
        public string Id { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ChipModel
    {
        public string TemplateType { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public ImageSource ImageSource { get; set; }
        public string ZTSTScts { get; set; }
    }
}
