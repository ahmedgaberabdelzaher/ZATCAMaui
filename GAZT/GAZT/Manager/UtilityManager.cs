using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            foreach( char letter in userName.ToCharArray())
            {
                if(letter <= 127)
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
                        DateTime dateStart = DateTime.ParseExact(trimStartDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                        StartDate = dateStart.ToString("dd-MMMM-yyyy", new CultureInfo("ar-sa"));
                    }
                    if (EndDate != null)
                    {
                        string trimEndDate = EndDate.Trim();
                        DateTime dateEnd = DateTime.ParseExact(trimEndDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                        EndDate = dateEnd.ToString("dd-MMMM-yyyy", new CultureInfo("ar-sa"));
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
                        DateTime dateStart = DateTime.ParseExact(trimStartDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                        StartDate = dateStart.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                    }
                    if (EndDate != null)
                    {
                        string trimEndDate = EndDate.Trim();
                        DateTime dateEnd = DateTime.ParseExact(trimEndDate, "dd/MM/yyyy", new CultureInfo("en-US"));
                        EndDate = dateEnd.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
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
            string Day= ConvertNumerals(SplitDate[0]);
            string FinalDate = Date;
            if (Month =="January")
            {
                Month = "يناير";
            }
            else if(Month== "February")
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

        public static string ConvertNumerals(this string input)
        {
            if (new string[] { "en-US", "ar-SA" }
                  .Contains(Thread.CurrentThread.CurrentCulture.Name))
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
            else return input;
        }
        #endregion
    }
}
