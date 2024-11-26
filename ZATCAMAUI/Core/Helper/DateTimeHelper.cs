using System.Globalization;

namespace ZATCAMAUI.Core.Helper
{
    public static class DateTimeHelper
    {
        public static string DateTimeFormater(DateTime dateTime, string dateFormat = "dd/MM/yyyy")
        {
            try
            {
                DateTimeFormatInfo DTFormat;
                DTFormat = new CultureInfo("en-US", false).DateTimeFormat;
                DTFormat.Calendar = new GregorianCalendar();
                DTFormat.ShortDatePattern = dateFormat;
                return dateTime.Date.ToString(DTFormat.ShortDatePattern, new CultureInfo("en-US"));
            }
            catch (Exception)
            {
                return "dd/MM/yyyy";
            }


        }

        public static DateTime DateTimeFormater(string dateTimeString)
        {
            try
            {
                CultureInfo cu = new CultureInfo("en-US");
                cu.DateTimeFormat.Calendar = new GregorianCalendar();
                var formates = new string[] { "yyyy/MM/dd", "yyyy-MM-dd", "dd-MM-yyyy",
                                        "dd/MM/yyyy", "MM/dd/yyyy", "M/dd/yyyy",
                                        "d/MM/yyyy","M/d/yyyy","d/M/yyyy",
                                        "dd/M/yyyy","MM/d/yyyy","d-MM-yyyy",
                                        "dd-M-yyyy","MM-d-yyyy","yyyy/M/d","yyyy/M/d",
                                        "yyyy-M-d","yyyy-d-M","yyyy/dd/MM","yyyy-dd-MM",
                                        "dd-MM-yyyy'T'hh:mm:ss","MM-dd-yyyy'T'hh:mm:ss",
                                        "dd-MM-yyyy'T'HH:mm:ss","MM-dd-yyyy'T'HH:mm:ss",
                                        "dd-MM-yyyy","MM-dd-yyyy","M-d-yyyy","d-M-yyyy",
                                        "M-dd-yyyy","yyyy-MM-dd'T'hh:mm:ss","yyyy-MM-dd'T'HH:mm:ss",
                                        "yyyy-MM-dd'T'HH:mm:ss.sss","yyyy/MM/dd'T'HH:mm:ss.sss",
                                        "yyyy-MM-ddTHH:mm:ss.sss","yyyy/MM/ddTHH:mm:ss.sss",
                                        "yyyy/MM/dd'T'hh:mm:ss.sss","yyyy-MM-dd'T'hh:mm:ss.sss",
                                        "yyyy/MM/ddThh:mm:ss.sss","yyyy-MM-ddThh:mm:ss.sss"};
                return DateTime.ParseExact(dateTimeString, formates, cu, DateTimeStyles.None);
            }
            catch (Exception)
            {
                return new DateTime();
            }


        }

        public static Tuple<DateTime, string> ConvertToGregorian(string dayMonthYearHigriDateString, string format = "dd/MM/yyyy")
        {
            try
            {
                CultureInfo arSA = new CultureInfo("ar-SA");
                arSA.DateTimeFormat.Calendar = new UmAlQuraCalendar();
                DateTime.TryParse(dayMonthYearHigriDateString, arSA, DateTimeStyles.None, out DateTime gregorianDate);
                var gregorianDateString = gregorianDate.ToString(format);
                return Tuple.Create(gregorianDate, gregorianDateString);
            }
            catch (Exception)
            {
                return Tuple.Create(new DateTime(), "dd/MM/yyyy");
            }

        }
        public static Tuple<DateTime, string> ConvertToGregorian(DateTime UmAlQuraCalendarHigriDateTime, string format = "dd/MM/yyyy")
        {
            try
            {
                var higriDateSTring = UmAlQuraCalendarHigriDateTime.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("ar-SA"));
                CultureInfo arSA = new CultureInfo("ar-SA");
                arSA.DateTimeFormat.Calendar = new UmAlQuraCalendar();
                DateTime.TryParse(higriDateSTring, arSA, DateTimeStyles.None, out DateTime gregorianDate);
                var gregorianDateString = gregorianDate.ToString(format);
                return Tuple.Create(gregorianDate, gregorianDateString);
            }
            catch (Exception)
            {
                return Tuple.Create(new DateTime(), "dd/MM/yyyy");
            }

        }
        public static Tuple<DateTime, string> ConvertToUmAlQuraHigriDate(DateTime GregorianDateTime, string format = "dd/MM/yyyy")
        {
            try
            {
                CultureInfo arSA = CultureInfo.CreateSpecificCulture("ar-SA");
                string hijriDateString = GregorianDateTime.ToString(format, arSA).Replace(' ', '/');
                DateTime.TryParse(GregorianDateTime.ToString("MM/dd/yyyy", arSA).Replace(' ', '/'), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime hijriDate);
                return Tuple.Create(hijriDate, hijriDateString);
            }
            catch (Exception)
            {
                return Tuple.Create(new DateTime(), "dd/MM/yyyy");
            }

        }
        public static Tuple<DateTime, string> ConvertToUmAlQuraHigriDate(string GregorianMonthDayYearDateString, string format = "dd/MM/yyyy")
        {
            try
            {
                CultureInfo arSA = CultureInfo.CreateSpecificCulture("ar-SA");
                DateTime.TryParse(GregorianMonthDayYearDateString, out DateTime date);
                string hijriDateString = date.ToString(format, arSA).Replace(' ', '/');
                DateTime.TryParse(date.ToString("MM/dd/yyyy", arSA).Replace(' ', '/'), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime hijriDate);
                return Tuple.Create(hijriDate, hijriDateString);
            }
            catch (Exception)
            {
                return Tuple.Create(new DateTime(), "dd/MM/yyyy");
            }

        }

    }


}

