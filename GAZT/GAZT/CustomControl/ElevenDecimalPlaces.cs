using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT
{
    [Preserve(AllMembers = true)]
    public class ElevenDotTwoDecimalPlacesAndNoNegativeValue : Behavior<Entry>
    {
     
        static int decimalCount;
        public static bool iSValiedNumber = true;
        public bool isNegativeEnable { get; set; } 
        public  int Max { get; set; }
        public int IntegerV { get; set; }
        public int numberOfDigitBeforDecimal { get; set; }
        public int numberOfDigitAfterDecimal { get; set; }
        public static bool IsValiedNumber = true;
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
        private  void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            ((Entry)sender).TextColor = (Color)App.Current.Resources["Primary"];
            //Int16 a = new Int16();
            //Max = Max;
            GetDecimalCount(args.NewTextValue);
            char LastChar = ' ';
            if (!string.IsNullOrEmpty(args.NewTextValue))
            {
                char[] textValue = args.NewTextValue.ToCharArray();
                LastChar = textValue[textValue.Length - 1];
            }
            //   bool IsStringContainsNegativeSign = ISNumberContainsNegativeSign(args.NewTextValue);
            bool IsStringContainsNegativeSign = args.NewTextValue.Contains("-");
            if (((Entry)sender).Text.Length < Max)
            {
                if (!IsStringContainsNegativeSign)
                {
                    if (decimalCount > 1)
                    {
                        if (!string.IsNullOrEmpty(args.NewTextValue))
                            ((Entry)sender).Text = args.NewTextValue.Substring(0, args.NewTextValue.Length - 1).ToString();// need to change later
                    }                    
                        char[] textValue = args.NewTextValue.ToCharArray();
                        for (int i = 0; i < textValue.Length; i++)
                        {
                            if (!((textValue[i] >= 46 && textValue[i] <= 57) || textValue[i] == 44))
                            {
                            ((Entry)sender).Text =   args.NewTextValue.Remove(i,1);
                            break;
                                // ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                            }
                        }
                       

                        //if (!((LastChar >= 46 && LastChar <= 57) || LastChar == 44))
                        //{
                        //    ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                        //}

                        //if(letter >= 46 && letter <= 57 )
                   
                    if (args.NewTextValue.Length == Max)
                        ((Entry)sender).Unfocus();
                    //if ((LastChar >= 46 && LastChar <= 57) || LastChar == 44)
                    //{
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(args.NewTextValue))
                    //        ((Entry)sender).Text = args.NewTextValue.Substring(0, args.NewTextValue.Length - 1).ToString();
                    //}
                }
                else
                {
                    if (isNegativeEnable == false)
                    {
                        ((Entry)sender).Text = args.NewTextValue.Replace("-", "");
                    }
                    if (decimalCount > 1)
                    {
                        if (!string.IsNullOrEmpty(args.NewTextValue))
                            ((Entry)sender).Text = args.NewTextValue.Substring(0, args.NewTextValue.Length - 1).ToString();// need to change later
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(args.NewTextValue))
                {
                    if (args.NewTextValue.Length == Max)
                        ((Entry)sender).Unfocus();
                    ((Entry)sender).Text = args.NewTextValue.Substring(0, args.NewTextValue.Length - 1).ToString();
                }
            }
             GetCommaSeparatedAmount(((Entry)sender).Text);
            if(!iSValiedNumber)
            {
                ((Entry)sender).TextColor =  (Color)App.Current.Resources["NewRedColor"];

                IsValiedNumber = false;
            }
            else
            {
                ((Entry)sender).TextColor = (Color)App.Current.Resources["Primary"];
                IsValiedNumber = true;

            }
        }
        private static void GetDecimalCount(string DecimalNumber)
        {
            int _decimalcount = 0;
            char[] decimalNumber = new char[20];
            try
            {
                if (DecimalNumber != null)
                {
                    decimalNumber = DecimalNumber.ToCharArray();
                }
                for (int i = 0; i < decimalNumber.Length; i++)
                {
                    if (decimalNumber[i].Equals('.'))
                    {
                        _decimalcount++;
                    }
                }
                decimalCount = _decimalcount;
                if (decimalCount > 1)
                {
                    //if (!string.IsNullOrEmpty(args.NewTextValue))
                    //    ((Entry)sender).Text = args.NewTextValue.Substring(0, args.NewTextValue.Length - 1)
                    // message please remove extra decimal number
                }
            }
            catch (Exception )
            {
            }
        }
        private  void GetCommaSeparatedAmount(string amount)
        {
            try
            {
                if (amount != null && amount.Length < Max && amount.Length > 0)
                {
                    amount = amount.Replace(",", "");
                    if (amount.Contains("."))
                    {
                        string[] Amount = new String[2];
                        Amount = amount.Split('.');
                        if (Amount[0].Length > numberOfDigitBeforDecimal || Amount[1].Length > numberOfDigitAfterDecimal)
                        {
                            iSValiedNumber = false;
                        }
                        else
                        {
                            iSValiedNumber = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(amount.Length) > numberOfDigitBeforDecimal)
                        {
                            iSValiedNumber = false;
                        }
                        else
                        {
                            iSValiedNumber = true;
                        }
                    }
                }
                else
                {
                    //amountWithComma = amount;
                }
            }
            catch (Exception )
            {
            }
        }
    }
}
