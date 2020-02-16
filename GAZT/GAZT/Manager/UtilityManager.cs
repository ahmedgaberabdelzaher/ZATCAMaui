using GAZT.Models;
using Newtonsoft.Json;
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
                    FullDate = StartDate + " , " + EndDate;
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

                return input.Replace('0', '\u06f0')
                        .Replace('1', '\u06f1')
                        .Replace('2', '\u06f2')
                        .Replace('3', '\u06f3')
                        .Replace('4', '\u06f4')
                        .Replace('5', '\u06f5')
                        .Replace('6', '\u06f6')
                        .Replace('7', '\u06f7')
                        .Replace('8', '\u06f8')
                        .Replace('9', '\u06f9');


            }
            else
            {
                return input;
            }
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
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string[] dts = Date.Split('/');

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

            return dt;

        }

        public static string FormatAccordingToDeviceHijriArabic(string Date)
        {
            string dt = string.Empty;
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string[] dts = Date.Split('/');

            if (sysFormat == "MM-dd-yyyy" || sysFormat == "MMM-dd-yyyy" || sysFormat == "MM-dd-yy" ||
               sysFormat == "MM/dd/yyyy" || sysFormat == "MMM/dd/yyyy" || sysFormat == "MM/dd/yy" ||
               sysFormat == "M/d/yyyy" || sysFormat == "M-d-yyyy")
            {
                dt = ConvertNumerals(dts[0]) + "-" + dts[1] + "-" + ConvertNumerals(dts[2]);
            }
            else
            {
                dt = ConvertNumerals(dts[0]) + "-" + dts[1] + "-" + ConvertNumerals(dts[2]);
            }

            return dt;

        }

        public static string FormatAccordingToDeviceHijriEnglish(string Date)
        {
            string dt = string.Empty;
            string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string[] dts = Date.Split('/');

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

            return dt;

        }

        public static string FormatAccordingToDeviceForVAT(string Date)
        {
            string dt = string.Empty;
           
            string[] dts = Date.Split('/');

          
            dt = dts[0] + "-" + GetMonthName(dts[1]) + "-" + dts[2];
           

            return dt;

        }
        public static string GetMonthName(string Month)
        {
           
            if (Month == "01" || Month == "1")
            {
                Month = "January";
            }
            else if (Month == "02" || Month == "2")
            {
                Month = "February";
            }
            else if (Month == "03" || Month == "3")
            {
                Month = "March";
            }
            else if (Month == "04" || Month == "4")
            {
                Month = "April";
            }
            else if (Month == "05" || Month == "5")
            {
                Month = "May";
            }
            else if (Month == "06" || Month == "6")
            {
                Month = "June";
            }
            else if (Month == "07" || Month == "7")
            {
                Month = "July";
            }
            else if (Month == "08" || Month == "8")
            {
                Month = "August";
            }
            else if (Month == "09" || Month == "9")
            {
                Month = "September";
            }
            else if (Month == "10")
            {
                Month = "October";
            }
            else if (Month == "11")
            {
                Month = "November";
            }
            else if (Month == "12")
            {
                Month = "December";
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
                Month = "Junho";
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
                Month = "Dhu al-Qi'dah";
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
                Month = "Rabi 'Al-Awwal";
            }
            else if (Month == "ربيع الآخر")
            {
                Month = "Rabih Al-Akher";
            }

            return Month;
        }

        #endregion

    }

    public enum Buttons
    {
        None = -01,
        Submit = 01,
        Approve = 02,
        Reject = 03,
        Void = 04,
        Save = 05,
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
        Createnotes = 21,
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
