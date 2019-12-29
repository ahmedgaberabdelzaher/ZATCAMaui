using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GAZT.Manager
{
    public static class UtilityManager
    {
        #region variable
        public static string emailIdValidation = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public static string passwordValidation = "^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+=]).*$";

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

        #endregion
    }
}
