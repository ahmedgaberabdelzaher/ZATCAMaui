using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace EGAZT.Helper
{
    public static class DateTimeHelper
    {
        public static string DatetimeFormater(DateTime dateTime, string format = "dd/MM/yyyy")
        {

            System.Globalization.DateTimeFormatInfo DTFormat;
            DTFormat = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
            DTFormat.Calendar = new System.Globalization.GregorianCalendar();
            DTFormat.ShortDatePattern = format;
            return dateTime.Date.ToString(DTFormat.ShortDatePattern);

        }

       
    }
    

}

    