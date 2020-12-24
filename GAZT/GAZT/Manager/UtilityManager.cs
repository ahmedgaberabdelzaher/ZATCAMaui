using EGAZT;
using EGAZT.Models;
using GAZT.Models;
using Newtonsoft.Json;
using PanCardView.Extensions;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Xamarin.Forms;

namespace GAZT.Manager
{
    public static class UtilityManager
    {
        #region variable
        public static string emailIdValidation = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public static string passwordValidation = "^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+=]).*$";
        public static string numberRegex = "^[0-9]+$";
        public static string mobileNumberValidation = "^([0-9]{9,9})$";
        public static string EnglishString = "^[a-zA-Z0-9,./+&-]*$";
        public static string IBANValidator = @"^[S][A]\d{22}$";


        public static string NewPasswordValidationRegx = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\\]).{8,16}$";
        public static bool ValidMinEight = false;
        public static bool ValidCapsL = false;
        public static bool ValidSmallL = false;
        public static bool ValidMaxSixteen = false;
        public static bool ValidNumber = false;
        public static bool ValidSymbol = false;
        public static string MobileNumberRegX = @"(.{9})\s*$";
        public static string MobileNumberLastThreeDigitsRegX = @"(.{3})\s*$";


        public static string TPTaxAvalable = string.Empty;
        public static string IsZakatAvailable = string.Empty;

        #endregion
        #region Method
        public static bool IsValidEmailAddress(string EmailAddress)
        {
            Match emailMatch = Regex.Match(EmailAddress, emailIdValidation);
            if (emailMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static bool IsIBANValid(string IBAN)
        {
            Match emailMatch = Regex.Match(IBAN, IBANValidator);
            if (emailMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetLanguageParameter()
        {
            if (App.IsArabic)
            {
                return "AR";
            }
            else
            {
                return "EN";
            }
        }
        public static bool IsPasswordValid(string password)
        {
            Match mobileMatch = Regex.Match(password, passwordValidation);
            if (mobileMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsMobileNumberValidValid(string mobilenumber)
        {
            Match mobileMatch = Regex.Match(mobilenumber, mobileNumberValidation);
            if (mobileMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsOTPNumberValid(string OTP)
        {
            Match mobileMatch = Regex.Match(OTP, numberRegex);
            if (mobileMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsUserNameValid(string userName)
        {
            bool isCorrectUserName = false;
            foreach (char letter in userName.ToCharArray())
            {
                if (letter <= 127)
                {
                    isCorrectUserName = true;
                }
                else
                {
                    isCorrectUserName = false;
                }
            }
            Match UserNameMatch = Regex.Match(userName, EnglishString);
            if (UserNameMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string SingleDateConversion(string Date)
        {
            String StartDate = Date;
            if (!string.IsNullOrEmpty(StartDate))
            {
                if (App.IsArabic)
                {
                    if (StartDate != null)
                    {
                        string trimStartDate = StartDate.Trim();
                        DateTime dateStart = DateTime.ParseExact(trimStartDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                        StartDate = dateStart.ToString("dd-MMMM-yyyy", new CultureInfo("ar-sa"));
                    }
                }
                else
                {
                    if (StartDate != null)
                    {
                        string trimStartDate = StartDate.Trim();
                        DateTime dateStart = DateTime.ParseExact(trimStartDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                        StartDate = dateStart.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                }
            }
            return StartDate;
        }
        public static string dateConversion(string Date)
        {
            String FullDate = string.Empty;
            String StartDate = string.Empty;
            String EndDate = string.Empty;
            if (!string.IsNullOrEmpty(Date))
            {
                string[] _dueDate = new String[2];
                _dueDate = Date.Split('-');
                StartDate = _dueDate[0];
                EndDate = _dueDate[1];
                if (App.IsArabic)
                {
                    if (StartDate != null)
                    {
                        string trimStartDate = StartDate.Trim();
                        string dateStart = FormatAccordingToDeviceForVAT(trimStartDate);
                        StartDate = ToArabicDate(dateStart);
                    }
                    if (EndDate != null)
                    {
                        string trimEndDate = EndDate.Trim();
                        string dateEnd = FormatAccordingToDeviceForVAT(trimEndDate);
                        EndDate = ToArabicDate(dateEnd);
                    }
                    //StartDate = ReverseString(StartDate);
                    //EndDate= ReverseString(EndDate);
                    FullDate = StartDate + "،" + EndDate;
                }
                else
                {
                    if (StartDate != null)
                    {
                        string trimStartDate = StartDate.Trim();
                        string dateStart = FormatAccordingToDeviceForVAT(trimStartDate);
                        StartDate = dateStart;
                    }
                    if (EndDate != null)
                    {
                        string trimEndDate = EndDate.Trim();
                        string dateEnd = FormatAccordingToDeviceForVAT(trimEndDate);
                        EndDate = dateEnd;
                    }
                    FullDate = StartDate + " - " + EndDate;
                }
            }
            return FullDate;
        }
        public static string englishDateConversion(string Date)
        {
            String FullDate = string.Empty;
            String StartDate = string.Empty;
            String EndDate = string.Empty;
            if (!string.IsNullOrEmpty(Date))
            {
                string[] _dueDate = new String[2];
                _dueDate = Date.Split('-');
                StartDate = _dueDate[0];
                EndDate = _dueDate[1];
                if (StartDate != null)
                {
                    string trimStartDate = StartDate.Trim();
                    string dateStart = FormatAccordingToDeviceForVAT(trimStartDate);
                    StartDate = dateStart;
                }
                if (EndDate != null)
                {
                    string trimEndDate = EndDate.Trim();
                    string dateEnd = FormatAccordingToDeviceForVAT(trimEndDate);
                    EndDate = dateEnd;
                }
                FullDate = StartDate + " - " + EndDate;
            }
            return FullDate;
        }
        public static bool IsEnglishNumber(String arText)
        {
            bool isAllNumeric = true;
            foreach (char letter in arText.ToCharArray())
            {
                if (!((letter >= 46 && letter <= 57) || letter == 44))
                {
                    isAllNumeric = false;
                }
            }
            return isAllNumeric;
        }
        public static bool IsEnglishNumberWithMinus(String arText)
        {
            bool isAllNumeric = true;
            foreach (char letter in arText.ToCharArray())
            {
                if (!((letter >= 46 && letter <= 57) || letter == 44 || letter == 45))
                {
                    isAllNumeric = false;
                }
            }
            return isAllNumeric;
        }
        public static string ToArabicDate(string Date)
        {
            string[] SplitDate = Date.Split('-');
            string Month = SplitDate[1];
            string Year = ConvertNumerals(SplitDate[2]);
            string Day = ConvertNumerals(SplitDate[0]);
            string FinalDate = Date;
            if (Month == "January")
            {
                Month = "يناير";
            }
            else if (Month == "February")
            {
                Month = "فبراير";
            }
            else if (Month == "March")
            {
                Month = "مارس";
            }
            else if (Month == "April")
            {
                Month = "أبريل";
            }
            else if (Month == "May")
            {
                Month = "مايو";
            }
            else if (Month == "June")
            {
                Month = "يونيو";
            }
            else if (Month == "July")
            {
                Month = "يوليو";
            }
            else if (Month == "August")
            {
                Month = "أغسطس";
            }
            else if (Month == "September")
            {
                Month = "سبتمبر";
            }
            else if (Month == "October")
            {
                Month = "أكتوبر";
            }
            else if (Month == "November")
            {
                Month = "نوفمبر";
            }
            else if (Month == "December")
            {
                Month = "ديسمبر";
            }
            FinalDate = Day + "-" + Month + "-" + Year;
            return FinalDate;
        }
        public static string getNumberAndConvert(string value)
        {
            String msg = RemoveDigits(value);
            return msg;
        }
        public static DateTime ConvertTiktoDate(string TikDate)
        {
            DateTime date = new DateTime();
            if (!string.IsNullOrEmpty(TikDate))
            {
                VATRateDataWithDateType vATRateDataWithDate;
                VATRateDataWithStringDateType dataWithStringDateType = new VATRateDataWithStringDateType();
                dataWithStringDateType.StartDate = TikDate;
                dataWithStringDateType.EndDate = TikDate;
                string JsonString = JsonConvert.SerializeObject(dataWithStringDateType);
                vATRateDataWithDate = JsonConvert.DeserializeObject<VATRateDataWithDateType>(JsonString);
                date = vATRateDataWithDate.StartDate;
            }
            return date;
        }
        public static string RemoveDigits(string key)
        {
            string CValue = ConvertNumerals(key);
            return Regex.Replace(key, @"\d", CValue);
        }
        public static string ConvertNumerals(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.Replace('0', '\u0660')
                  .Replace('1', '\u0661')
                  .Replace('2', '\u0662')
                  .Replace('3', '\u0663')
                  .Replace('4', '\u0664')
                  .Replace('5', '\u0665')
                  .Replace('6', '\u0666')
                  .Replace('7', '\u0667')
                  .Replace('8', '\u0668')
                  .Replace('9', '\u0669');
            }
            else
            {
                return input;
            }
        }
        public static string GetTaxPeriodDate(string PeriodDate)
        {
            string Date = "";
            if (PeriodDate.Contains("-"))
            {
                string[] date = new String[2];
                date = PeriodDate.Split('-');
                Date = ConvertNumerals(date[0]) + " " + AppResources.To + " " + ConvertNumerals(date[1]);
            }
            return Date;
        }
        public static string DownloadDataFromLink(string url)
        {
            string Base64String = string.Empty;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        String folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                        //  string completePath = Path.Combine(folderPath, "GAZTeServices");
                        string Url = url;
                        if (!string.IsNullOrEmpty(Url))
                        {
                            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(Url);
                            WebResponse myResp = myReq.GetResponse();
                            using (Stream streams = myResp.GetResponseStream())
                            using (MemoryStream ms = new MemoryStream())
                            {
                                int count = 0;
                                do
                                {
                                    byte[] buf = new byte[1024];
                                    count = streams.Read(buf, 0, 1024);
                                    ms.Write(buf, 0, count);
                                } while (streams.CanRead && count > 0);
                                Base64String = Convert.ToBase64String(ms.ToArray());
                                byte[] bytes = System.Convert.FromBase64String(Base64String);
                                File.WriteAllBytes(folderPath, bytes);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException();
                }
                return Base64String;
            }
        }
        public static string FormatAccordingToDevice(string Date)
        {
            string dt = string.Empty;
            string[] dts = null;
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            if (Date.Contains("/"))
            {
                dts = Date.Split('/');
            }
            else
            {
                dts = Date.Split('-');
            }
            if (dts != null)
            {
                if (sysFormat == "MM-dd-yyyy" || sysFormat == "MMM-dd-yyyy" || sysFormat == "MM-dd-yy" ||
                   sysFormat == "MM/dd/yyyy" || sysFormat == "MMM/dd/yyyy" || sysFormat == "MM/dd/yy" ||
                   sysFormat == "M/d/yyyy" || sysFormat == "M-d-yyyy")
                {
                    dt = dts[1] + "-" + GetMonthName(dts[0]) + "-" + dts[2];
                }
                else
                {
                    dt = dts[0] + "-" + GetMonthName(dts[1]) + "-" + dts[2];
                }
            }
            return dt;
        }
        public static string FormatAccordingToDeviceHijriArabic(string Date)
        {
            string dt = string.Empty;
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string[] dts = null;
            if (Date.Contains("/"))
            {
                dts = Date.Split('/');
            }
            else
            {
                dts = Date.Split('-');
            }
            if (dts != null)
            {
                if (sysFormat == "MM-dd-yyyy" || sysFormat == "MMM-dd-yyyy" || sysFormat == "MM-dd-yy" ||
                   sysFormat == "MM/dd/yyyy" || sysFormat == "MMM/dd/yyyy" || sysFormat == "MM/dd/yy" ||
                   sysFormat == "M/d/yyyy" || sysFormat == "M-d-yyyy")
                {
                    dt = ConvertNumerals(dts[0]) + "-" + GetMonthNameHijriArabic(dts[1]) + "-" + ConvertNumerals(dts[2]);
                }
                else
                {
                    dt = ConvertNumerals(dts[0]) + "-" + GetMonthNameHijriArabic(dts[1]) + "-" + ConvertNumerals(dts[2]);
                }
            }
            return dt;
        }
        public static string FormatAccordingToDeviceHijriEnglish(string Date)
        {
            string dt = string.Empty;
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string[] dts = null;
            if (Date.Contains("/"))
            {
                dts = Date.Split('/');
            }
            else
            {
                dts = Date.Split('-');
            }
            if (dts != null)
            {
                if (sysFormat == "MM-dd-yyyy" || sysFormat == "MMM-dd-yyyy" || sysFormat == "MM-dd-yy" ||
               sysFormat == "MM/dd/yyyy" || sysFormat == "MMM/dd/yyyy" || sysFormat == "MM/dd/yy" ||
               sysFormat == "M/d/yyyy" || sysFormat == "M-d-yyyy")
                {
                    dt = dts[0] + "-" + GetMonthNameHijri(dts[1]) + "-" + dts[2];
                }
                else
                {
                    dt = dts[0] + "-" + GetMonthNameHijri(dts[1]) + "-" + dts[2];
                }
            }
            return dt;
        }
        public static string FormatAccordingToDeviceForVAT(string Date)
        {
            string dt = string.Empty;
            string[] dts = null;
            if (Date.Contains("/"))
            {
                dts = Date.Split('/');
            }
            else
            {
                dts = Date.Split('-');
            }
            if (dts != null)
            {
                dt = dts[0] + "-" + GetMonthName(dts[1]) + "-" + dts[2];
            }
            return dt;
        }

        public static string GetFileImage(string Extention)
        {
            if (Extention.ToLower() == "doc")
            {
               return "Generic_attachment_icon.png";

            }
            else if (Extention.ToLower() == "docx")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "jpg")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "jpeg")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "pdf")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "xlsx")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "xls")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "png")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "ppt")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "pptx")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "gif")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "txt")
            {
                return "Generic_attachment_icon.png";
            }
            else if (Extention.ToLower() == "bmp")
            {
                return "Generic_attachment_icon.png";
            }
            else
            {
                return null;
            }

        }
        public static string GetContentType(String Extention)
        {
            if (Extention.ToLower() == "doc")
            {
                return "application/msword";
            }
            else if (Extention.ToLower() == "docx")
            {
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }
            else if (Extention.ToLower() == "jpg")
            {
                return "application/jpg";
            }
            else if (Extention.ToLower() == "jpeg")
            {
                return "application/jpeg";
            }
            else if (Extention.ToLower() == "pdf")
            {
                return "application/pdf";
            }
            else if (Extention.ToLower() == "xlsx")
            {
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
            else if (Extention.ToLower() == "xls")
            {
                return "application/vnd.ms-excel";
            }
            else if (Extention.ToLower() == "png")
            {
                return "application/png";
            }
            else if (Extention.ToLower() == "ppt")
            {
                return "application/vnd.ms-powerpoint";
            }
            else if (Extention.ToLower() == "pptx")
            {
                return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            }
            else if (Extention.ToLower() == "gif")
            {
                return "application/gif";
            }
            else if (Extention.ToLower() == "txt")
            {
                return "text/plain";
            }
            else if (Extention.ToLower() == "bmp")
            {
                return "application/bmp";
            }
            else
            {
                return null;
            }
        }
        public static string GetMonthName(string Month)
        {
            if (Month == "01" || Month == "1" || Month == "January")
            {
                Month = !App.IsArabic ? "January" : "يناير";
                // Month = "January";
            }
            else if (Month == "02" || Month == "2" || Month == "February")
            {
                Month = !App.IsArabic ? "February" : "فبراير";
                //  Month = "February";
            }
            else if (Month == "03" || Month == "3" || Month == "March")
            {
                Month = !App.IsArabic ? "March" : "مارس";
                // Month = "March";
            }
            else if (Month == "04" || Month == "4" || Month == "April")
            {
                Month = !App.IsArabic ? "April" : "أبريل";
                // Month = "April";
            }
            else if (Month == "05" || Month == "5" || Month == "May")
            {
                Month = !App.IsArabic ? "May" : "مايو";
                // Month = "May";
            }
            else if (Month == "06" || Month == "6" || Month == "June")
            {
                Month = !App.IsArabic ? "June" : "يونيو";
                //  Month = "June";
            }
            else if (Month == "07" || Month == "7" || Month == "July")
            {
                Month = !App.IsArabic ? "July" : "يوليو";
                //Month = "July";
            }
            else if (Month == "08" || Month == "8" || Month == "August")
            {

                Month = !App.IsArabic ? "August" : "أغسطس";
                //  Month = "August";
            }
            else if (Month == "09" || Month == "9" || Month == "September")
            {

                Month = !App.IsArabic ? "September" : "سبتمبر";
                //  Month = "September";
            }
            else if (Month == "10" || Month == "October")
            {
                Month = !App.IsArabic ? "October" : "أكتوبر";
                //Month = "October";
            }
            else if (Month == "11" || Month == "November")
            {
                Month = !App.IsArabic ? "November" : "نوفمبر";
                // Month = "November";
            }
            else if (Month == "12" || Month == "December")
            {
                Month = !App.IsArabic ? "December" : "ديسمبر";
                //   Month = "December";
            }
            return Month;
        }


        public static string GetShortMonthName(string Month)
        {
            if (Month == "01" || Month == "1")
            {
                Month = "Jan";
            }
            else if (Month == "02" || Month == "2")
            {
                Month = "Feb";
            }
            else if (Month == "03" || Month == "3")
            {
                Month = "Mar";
            }
            else if (Month == "04" || Month == "4")
            {
                Month = "Apr";
            }
            else if (Month == "05" || Month == "5")
            {
                Month = "May";
            }
            else if (Month == "06" || Month == "6")
            {
                Month = "Jun";
            }
            else if (Month == "07" || Month == "7")
            {
                Month = "Jul";
            }
            else if (Month == "08" || Month == "8")
            {
                Month = "Aug";
            }
            else if (Month == "09" || Month == "9")
            {
                Month = "Sep";
            }
            else if (Month == "10")
            {
                Month = "Oct";
            }
            else if (Month == "11")
            {
                Month = "Nov";
            }
            else if (Month == "12")
            {
                Month = "Dec";
            }
            return Month;
        }

        public static string GetMonthNameHijri(string Month)
        {
            if (Month == "جمادى الأولى")
            {
                Month = "Jumada I";
            }
            else if (Month == "جمادى الآخرة")
            {
                Month = "Jumada II";
            }
            else if (Month == "رجب")
            {
                Month = "Rajab";
            }
            else if (Month == "شعبان")
            {
                Month = "Shaban";
            }
            else if (Month == "رمضان")
            {
                Month = "Ramadan";
            }
            else if (Month == "شوال")
            {
                Month = "Shawwal";
            }
            else if (Month == "ذو القعدة")
            {
                Month = "Dhu al-Qidah";
            }
            else if (Month == "ذو الحجة")
            {
                Month = "Dhu al-Hijjah";
            }
            else if (Month == "محرم")
            {
                Month = "Muharram";
            }
            else if (Month == "صفر")
            {
                Month = "Safar";
            }
            else if (Month == "ربيع الأول")
            {
                Month = "Rabi I";
            }
            else if (Month == "ربيع الآخر")
            {
                Month = "Rabi II";
            }
            else if (Month == "01" || Month == "1")
            {
                Month = !App.IsArabic ? "Muharram" : " محرم";
            }
            else if (Month == "02" || Month == "2")
            {
                //Month = "Safar";
                Month = !App.IsArabic ? "Safar" : "صفر";
            }
            else if (Month == "03" || Month == "3")
            {
                // Month = "Rabi I";
                Month = !App.IsArabic ? "Rabi-Al-Awal" : "ربيع أول";
            }
            else if (Month == "04" || Month == "4")
            {
                // Month = "Rabi-Al-Thani";
                Month = !App.IsArabic ? "Rabi-Al-Thani" : "ربيع ثاني";
            }
            else if (Month == "05" || Month == "5")
            {
                //  Month = " Jumada-Al-Awal";
                Month = !App.IsArabic ? " Jumada-Al-Awal" : "جمادي أولى";
            }
            else if (Month == "06" || Month == "6")
            {
                //Month = "Jumada II"; Jumada - Al - Thani
                Month = !App.IsArabic ? " Jumada-Al-Thani" : "جمادي ثاني";
            }
            else if (Month == "07" || Month == "7")
            {
                //   Month = "Rajab";
                Month = !App.IsArabic ? "Rajab" : "رجب";
            }
            else if (Month == "08" || Month == "8")
            {
                // Month = "Sha ban"; 
                Month = !App.IsArabic ? "Shaban" : "شعبان";
            }
            else if (Month == "09" || Month == "9")
            {
                //Month = "Ramadan";
                Month = !App.IsArabic ? "Ramadan" : "رمضان";
            }
            else if (Month == "10")
            {

                //   Month = "shawwal";
                Month = !App.IsArabic ? "Shawwal" : "شوال";
            }
            else if (Month == "11")
            {
                // Month = "Dhul-Qi dah"; 
                Month = !App.IsArabic ? "Dhul-Qa'dah" : "ذو القعدة";
            }
            else if (Month == "12")
            {
                // Month = "Dhul-Hijjah"; Dhul - Hijjah
                Month = !App.IsArabic ? "Dhul-Hijjah" : "ذو الحجة";
            }
            return Month;
        }

        public static string Converthijri(DateTime FormatedFaedn)
        {
            // FormatedFaedn = _faedn.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
            var calendar = new HijriCalendar();
            var day = calendar.GetDayOfMonth(FormatedFaedn);
            var year = calendar.GetYear(FormatedFaedn);
            var month = calendar.GetMonth(FormatedFaedn);
            string hijriDate = UtilityManager.FormatAccordingToDeviceHijriEnglish(day + "/" + month + "/" + year);
            return hijriDate;

        }
        //public static string GetMonthNameHijriArabic(string Month)
        //{
        //    if (Month == "جمادى الأولى")
        //    {
        //        Month = "جمادى أول";
        //    }
        //    else if (Month == "جمادى الآخرة")
        //    {
        //        Month = "جمادى ثاني";
        //    }
        //    else if (Month == "ربيع الأول")
        //    {
        //        Month = "ربيع أول";
        //    }
        //    else if (Month == "ربيع الآخر")
        //    {
        //        Month = "ربيع ثاني";
        //    }
        //    return Month;
        //}
        public static string GetMonthNameHijriArabic(string Month)
        {
            if (Month == "جمادى الأولى")
            {
                Month = "جمادى أول";
            }
            else if (Month == "جمادى الآخرة")
            {
                Month = "جمادى ثاني";
            }
            else if (Month == "ربيع الأول")
            {
                Month = "ربيع أول";
            }
            else if (Month == "ربيع الآخر")
            {
                Month = "ربيع ثاني";
            }
            else if (Month == "01")
            {
                Month = !App.IsArabic ? "Muharram" : " محرم";
            }
            else if (Month == "02")
            {
                //Month = "Safar";
                Month = !App.IsArabic ? "Safar" : "Y-صفر-M";
            }
            else if (Month == "03")
            {
                // Month = "Rabi I";
                Month = !App.IsArabic ? "Rabi-Al-Awal" : "Y-ربيع أول-M";
            }
            else if (Month == "04")
            {
                // Month = "Rabi-Al-Thani";
                Month = !App.IsArabic ? "Rabi-Al-Thani" : "Y-ربيع ثاني-M";
            }
            else if (Month == "05")
            {
                //  Month = " Jumada-Al-Awal";
                Month = !App.IsArabic ? " Jumada-Al-Awal" : "Y-جمادي أولى-M";
            }
            else if (Month == "06")
            {
                //Month = "Jumada II"; Jumada - Al - Thani
                Month = !App.IsArabic ? " Jumada-Al-Thani" : "Y-جمادي ثاني-M";
            }
            else if (Month == "07")
            {
                //   Month = "Rajab";
                Month = !App.IsArabic ? "Rajab" : "Y-رجب-M";
            }
            else if (Month == "08")
            {
                // Month = "Sha ban"; 
                Month = !App.IsArabic ? "Shaban" : "Y-شعبان-M";
            }
            else if (Month == "09")
            {
                //Month = "Ramadan";
                Month = !App.IsArabic ? "Ramadan" : "Y-رمضان-M";
            }
            else if (Month == "10")
            {

                //   Month = "shawwal";
                Month = !App.IsArabic ? "Shawwal" : "Y-شوال-M";
            }
            else if (Month == "11")
            {
                // Month = "Dhul-Qi dah"; 
                Month = !App.IsArabic ? "Dhul-Qa'dah" : "Y-ذو القعدة-M";
            }
            else if (Month == "12")
            {
                // Month = "Dhul-Hijjah"; Dhul - Hijjah
                Month = !App.IsArabic ? "Dhul-Hijjah" : "Y-ذو الحجة-M";
            }
            return Month;
        }
        public static string GetCommaSeparatedAmount(string amount)
        {
            string amountWithComma = "";
            try
            {
                if (amount != null && amount.Length > 0)
                {
                    double testDueAmount = Convert.ToDouble(amount);
                    CultureInfo ci = new CultureInfo("en-us");
                    string _testDueAmount;//= testDueAmount.ToString("#,##0");
                    double floating = Convert.ToDouble(amount);
                    _testDueAmount = floating.ToString("N02", ci);
                    amountWithComma = _testDueAmount;
                }
            }
            catch (Exception ex)
            {
            }
            return amountWithComma;
        }
        #endregion

        public static IEnumerable<IGrouping<string, QuestionsetWithMinMax>> GetQuestionsGroupedByQuestionNo(QUESCONFIG_MSet qUESCONFIG_MSet)
        {
            IEnumerable<IGrouping<string, QuestionsetWithMinMax>> QuestionsGroupedByQuestionNo = qUESCONFIG_MSet.results.GroupBy(qn => qn.QueNo);
            return QuestionsGroupedByQuestionNo;
        }

        //Get n group of QuestionNumberWithMinMaxRangeWithCountOfAnswers
        public static List<QuestionNumberWithMinMaxRange> GetLowAndHighRangeForEachQuestionSet(QUESCONFIG_MSet qUESCONFIG_MSet)
        {
            List<QuestionNumberWithMinMaxRange> QuestionsGroupedyMinMaxRange = null;

            if (qUESCONFIG_MSet != null && qUESCONFIG_MSet.results != null)
            {
                IEnumerable<IGrouping<string, QuestionsetWithMinMax>> QuestionsGroupedByQuestionNo = GetQuestionsGroupedByQuestionNo(qUESCONFIG_MSet);
                if (QuestionsGroupedByQuestionNo != null)
                {
                    QuestionsGroupedyMinMaxRange = new List<QuestionNumberWithMinMaxRange>();
                    // 4 question groups
                    foreach (var QuestionGroup in QuestionsGroupedByQuestionNo)
                    {
                        QuestionsGroupedyMinMaxRange.Add(new QuestionNumberWithMinMaxRange { QueNo = QuestionGroup.First().QueNo, MinRangeValue = Convert.ToDouble(QuestionGroup.First().Minvalue), MaxRangeValue = Convert.ToDouble(QuestionGroup.Last().Minvalue), CountOfProbableAnswersForThisQuestions = QuestionGroup.Count() });
                    }
                }
            }
            return QuestionsGroupedyMinMaxRange;
        }
        public static QuestionsetWithMinMax FindTheAnswerApplicableBasedOntheValue(string QuestionNumber, double CurrentValue, QUESCONFIG_MSet qUESCONFIG_MSet)
        {
            IEnumerable<IGrouping<string, QuestionsetWithMinMax>> QuestionsGroupedByQuestionNo = GetQuestionsGroupedByQuestionNo(qUESCONFIG_MSet);

            IEnumerable<IGrouping<string, QuestionsetWithMinMax>> AnswersGroupedByQuestionNo = QuestionsGroupedByQuestionNo.Where(x => x.Key == QuestionNumber);

            int n = (int)Math.Ceiling(CurrentValue);
            QuestionsetWithMinMax qs = AnswersGroupedByQuestionNo.FirstOrDefault().ElementAt(n);

            return qs;

        }

        public static int FindTheAnswerIndexBasedOntheAnswerId(string QuestionNumber, string AnswerId, QUESCONFIG_MSet qUESCONFIG_MSet)
        {
            int AnswerIndexInTheQuestionSet = -1;

            IEnumerable<IGrouping<string, QuestionsetWithMinMax>> QuestionsGroupedByQuestionNo = GetQuestionsGroupedByQuestionNo(qUESCONFIG_MSet);

            IEnumerable<IGrouping<string, QuestionsetWithMinMax>> AnswersGroupedByQuestionNo = QuestionsGroupedByQuestionNo.Where(x => x.Key == QuestionNumber);

            IGrouping<string, QuestionsetWithMinMax> answers = AnswersGroupedByQuestionNo.FirstOrDefault();
            int index = answers.ToList().FindIndex(a => a.QoptNo == AnswerId);
            //QuestionsetWithMinMax qmm = answers.Where(x => x.QoptNo == AnswerId).FirstOrDefault();

            //return AnswersGroupedByQuestionNo.FindIndex(qmm);
            return index;
        }
        private static  string[] allFormats ={"yyyy/MM/dd","yyyy/M/d",
        "dd/MM/yyyy","d/M/yyyy",
        "dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd",
        "yyyy-M-d","dd-MM-yyyy","d-M-yyyy",
        "dd-M-yyyy","d-MM-yyyy","yyyy MM dd",
        "yyyy M d","dd MM yyyy","d M yyyy",
        "dd M yyyy","d MM yyyy"};
        public static string HijriToGreg(string hijri)
        {
            CultureInfo arCul = new CultureInfo("ar-SA");
            CultureInfo enCul = new CultureInfo("en-US");
            DateTime tempDate = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
            return tempDate.ToString("yyyy/MM/dd", enCul.DateTimeFormat);
        }


        public static string ConvertToHijri(string date)
        {
            try
            {
                CultureInfo arSA = new CultureInfo("ar-SA");
                CultureInfo enCul = new CultureInfo("en-US");
                DateTime tempDate = DateTime.ParseExact(date, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                return tempDate.ToString("yyyy/MM/dd", arSA.DateTimeFormat);

            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public static string ConvertToGreg(string date)
        {
            try
            {
                CultureInfo arSA = new CultureInfo("en-US");
                arSA.DateTimeFormat.Calendar = new GregorianCalendar();
                return DateTime.ParseExact(date, "yyyy/MM/dd", arSA).ToString("yyyy/MM/dd");
            }
            catch(Exception ex)
            {
                return "";
            }
             
        }

        public static string ConvertDateFormat(object newDate)
        {
            if (newDate == null)
                return null;

            if (!newDate.ToString().Contains("/Date("))
            {
                DateTime dateTime = Convert.ToDateTime(newDate);

                string ConvertedDate = string.Empty;
                TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                string unixTime = span.TotalSeconds.ToString("N0");
                unixTime = unixTime.Replace(",", "");
                ConvertedDate = "" + "/Date(" + unixTime + ")/";

                long unixTimestamp = ((long)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

                unixTimestamp = unixTimestamp * 1000;

                ConvertedDate = "" + "/Date(" + unixTimestamp + ")/";
                return ConvertedDate;
            }

            return newDate.ToString();
        }

        // * New Password Validation
        public static bool ValidateNewPassword(string password)
        {
            bool valid = Regex.IsMatch(password, NewPasswordValidationRegx);
            CharValidation(password);
            return valid;
        }

        public static bool ValidateNewPasswordForTP(string password)
        {
            bool valid = Regex.IsMatch(password, passwordValidation);
            return valid;
        }

        // * Character wise validation
        private static void CharValidation(string passwordchar)
        {
            int validConditions = 0;

            // * Small
            foreach (char c in passwordchar)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            ValidSmallL = NewPasswordValidation(validConditions);

            // * Caps
            validConditions = 0;
            foreach (char c in passwordchar)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            ValidCapsL = NewPasswordValidation(validConditions);

            // * Numbers
            validConditions = 0;
            foreach (char c in passwordchar)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            ValidNumber = NewPasswordValidation(validConditions);

            // * Special Char
            char[] special = { '!', '@', '#', '$', '%', '^', '&', '*', '-', '_', '+', '=' };
            if (passwordchar.IndexOfAny(special) == -1) validConditions = 0;
            else validConditions = 1;

            ValidSymbol = NewPasswordValidation(validConditions);
            ValidMinEight = PasswordMinLValidation(passwordchar.Length);
            ValidMaxSixteen = PasswordMaxLValidation(passwordchar.Length);
        }

        // * Password : Number + Symbol + Caps + Small : Validation
        private static bool NewPasswordValidation(int validConditions)
        {
            if (validConditions == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // * Password Length Validation - Min 8
        private static bool PasswordMinLValidation(int passwordLength)
        {
            if (passwordLength >= 8) return true;
            else return false;
        }

        // * Password Length Validation - Max 16
        private static bool PasswordMaxLValidation(int passwordLength)
        {
            if (passwordLength >= 8 && passwordLength <= 16) return true;
            else return false;
        }

        // * Global Method For Changing Flow Direction LANG Based
        public static FlowDirection SetLTRAndRTL()
        {
            if (App.IsArabic)
            {
                return FlowDirection.RightToLeft;
            }
            else
            {
                return FlowDirection.LeftToRight;
            }
        }
    }
   

    public enum ArButtons
    {
        None = -01,
        تقديم = 01,
        Approve = 02,
        Reject = 03,
        إلغاء = 04,
        حفظكمسودة = 05,
        NotesforER = 06,
        عرضملاحظات = 07,
        التحقق = 08,
        Forward = 09,
        Assigntome = 10,
        Calendar = 11,
        Confirm = 12,
        SendforInspection = 13,
        AssignInspector = 14,
        المرفقات = 15,
        SendBack = 16,
        AttachBankGuarantee = 17,
        ExtendDueDate = 18,
        عادةتعيين = 19,
        Next = 20,
        إضافةملاحظات = 21,
        تعديل = 22,
        InspectorSubmit = 23,
        إغلاق = 24,
        ApplicationDownloadforInspector = 25,
        Reviewed = 26,
        AssignOfficer = 27,
        EditaMovementActivity = 28,
        CancelMovementActivity = 29,
        AddNewMovementActivity = 30,
        SavetheDeclaration = 31,
        CancelDeclaration = 32,
        SubmittheDeclaration = 33,
        SendforAudit = 34,
        SendtoDirector = 35,
        SubmitInspector = 36,
        AttachUnloadingDocument = 37,
        ClearDocument = 38,
        ExtendApprovalTime = 39,
        Change = 40,
        Extend = 41,
        Revoke = 42,
        SendtoTaxpayer = 43,
        SummaryDetails = 44,
        PrintSDReleaseLetter = 45,
        ReleaseBankGuarantee = 46,
        ComplianceAndHistory = 47,
        Previous = 48,
        CancelReturn = 49,
        RequestAdditionalInformation = 50,
        Salesdetails = 51,
        Changefromestimatetoaccounting = 52,
        Invoice = 53,
        إصدار = 54,
        ReviseDownPayment = 55
    }



    public enum Buttons
    {
        None = -01,
        Submit = 01,
        Approve = 02,
        Reject = 03,
        Void = 04,
        SaveasDraft = 05,
        NotesforER = 06,
        DisplayNotes = 07,
        Validate = 08,
        Forward = 09,
        Assigntome = 10,
        Calendar = 11,
        Confirm = 12,
        SendforInspection = 13,
        AssignInspector = 14,
        Attachments = 15,
        SendBack = 16,
        AttachBankGuarantee = 17,
        ExtendDueDate = 18,
        Reset = 19,
        Next = 20,
        CreateNotes = 21,
        Amend = 22,
        InspectorSubmit = 23,
        Closed = 24,
        ApplicationDownloadforInspector = 25,
        Reviewed = 26,
        AssignOfficer = 27,
        EditaMovementActivity = 28,
        CancelMovementActivity = 29,
        AddNewMovementActivity = 30,
        SavetheDeclaration = 31,
        CancelDeclaration = 32,
        SubmittheDeclaration = 33,
        SendforAudit = 34,
        SendtoDirector = 35,
        SubmitInspector = 36,
        AttachUnloadingDocument = 37,
        ClearDocument = 38,
        ExtendApprovalTime = 39,
        Change = 40,
        Extend = 41,
        Revoke = 42,
        SendtoTaxpayer = 43,
        SummaryDetails = 44,
        PrintSDReleaseLetter = 45,
        ReleaseBankGuarantee = 46,
        ComplianceAndHistory = 47,
        Previous = 48,
        CancelReturn = 49,
        RequestAdditionalInformation = 50,
        Salesdetails = 51,
        Changefromestimatetoaccounting = 52,
        Invoice = 53,
        Release = 54,
        ReviseDownPayment = 55
    }


    

}

