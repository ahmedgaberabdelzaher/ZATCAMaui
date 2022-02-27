using EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLoginPage_ViewModel;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using BorderlessEntry = GAZTeServicesApp.Controls.BorderlessEntry;
namespace GAZTeServicesApp.Converters
{
    /// <summary>
    /// This class have methods to convert the Boolean values to color objects. 
    /// This is needed to validate in the Entry controls. If the validation is failed, it will return the color code of error, otherwise it will be transparent.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ErrorValidationColorConverter : IValueConverter
    {
        /// <summary>
        /// Identifies the simple and gradient login pages.
        /// </summary>
        public string PageVariantParameter { get; set; }
        /// <summary>
        /// This method is used to convert the bool to color.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the color.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // For Gradient login page 
            if (PageVariantParameter == "0")
            {
                var emailEntry = parameter as BorderlessEntry;
                if (!(emailEntry.BindingContext is SFLoginPageViewModel bindingContext))
                {
                    return Color.Transparent;
                }
                var isFocused = (bool)value;
                bindingContext.IsInvalidEmail = !isFocused && !CheckValidEmail(bindingContext.Email);
                if (isFocused)
                {
                    return Color.FromRgba(255, 255, 255, 0.6);
                }
                return bindingContext.IsInvalidEmail ?  (Color)Application.Current.Resources["Red"] : Color.Transparent;
            }
            // For Simple login page
            else
            {
                var emailEntry = parameter as BorderlessEntry;
                if (!(emailEntry.BindingContext is SFLoginPageViewModel bindingContext)) return  (Color)Application.Current.Resources["Gray"];
                var isFocused1 = (bool)value;
                bindingContext.IsInvalidEmail = !isFocused1 && !CheckValidEmail(bindingContext.Email);
                if (isFocused1)
                {
                    Application.Current.Resources.TryGetValue("Gray-500", out var retGray);
                    return (Color)retGray;
                }
                return bindingContext.IsInvalidEmail ?  (Color)Application.Current.Resources["Red"] :  (Color)Application.Current.Resources["Gray"];
            }
        }
        /// <summary>
        /// This method is used to convert the color to bool.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the string.</returns>        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <param name="email">Gets the email.</param>
        /// <returns>Returns the boolean value.</returns>
        private static bool CheckValidEmail(string email)
        {
            bool isNumber;
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            else
            {
               isNumber = IsEnglishNumber(email);
            }
            if (!isNumber)
            {
                var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                return regex.IsMatch(email) && !email.EndsWith(".");
            }
            else
            {
                return true;
            }
        }
        public static bool IsEnglishNumber(String arText)
        {
            bool isAllNumeric = true;
            foreach (char letter in arText.ToCharArray())
            {
                if (!(letter >= 48 && letter <= 57))
                {
                    isAllNumeric = false;
                }
            }
            return isAllNumeric;
        }
    }
}