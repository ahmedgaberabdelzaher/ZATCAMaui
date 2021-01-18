using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public class NineDotTwoDecimalPlacesAndNoNegativeValue : Behavior<Entry>
    {
        static int decimalCount;
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
        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                GetDecimalCount(args.NewTextValue);
                char LastChar = ' ';
                if (!string.IsNullOrEmpty(args.NewTextValue))
                {
                    char[] textValue = args.NewTextValue.ToCharArray();
                    LastChar = textValue[textValue.Length - 1];
                }
                if (LastChar >= 46 && LastChar <= 57)
                {
                    if (NineDotTwoDecimalPlacesAndNoNegativeValue.decimalCount < 2)
                    {
                        if (!string.IsNullOrEmpty(args.NewTextValue))
                        {
                            if (args.NewTextValue.Substring(args.NewTextValue.Length - 1) != ".")
                            {
                                if (args.NewTextValue.Contains("."))
                                {
                                    string[] SplitByDecimal = args.NewTextValue.Split('.');
                                    string BeforeDecimal = string.Empty;
                                    string AfterDecimal = string.Empty;
                                    if (SplitByDecimal[0].Length > 9)
                                    {
                                        BeforeDecimal = SplitByDecimal[0].Remove(SplitByDecimal[0].Length - 1);
                                        if (BeforeDecimal.Contains("-"))
                                        {
                                            BeforeDecimal = (Convert.ToInt32(BeforeDecimal) * -1).ToString();
                                        }
                                        ((Entry)sender).Text = Math.Round(Convert.ToDecimal(BeforeDecimal + AfterDecimal), 2).ToString();
                                    }
                                    else
                                    {
                                        if (args.NewTextValue.Contains("-"))
                                        {
                                            ((Entry)sender).Text = (Math.Round(Convert.ToDecimal(args.NewTextValue), 2) * -1).ToString();
                                        }
                                        else
                                        {
                                            ((Entry)sender).Text = Math.Round(Convert.ToDecimal(args.NewTextValue), 2).ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    if (args.NewTextValue.Length > 9)
                                    {
                                        if (args.NewTextValue.Contains("-"))
                                        {
                                            ((Entry)sender).Text = (Math.Round(Convert.ToDecimal(args.NewTextValue.Remove(args.NewTextValue.Length - 1)), 2) * -1).ToString();
                                        }
                                        else
                                        {
                                            ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                                        }
                                    }
                                    else
                                    {
                                        if (args.NewTextValue.Contains("-"))
                                        {
                                            ((Entry)sender).Text = args.NewTextValue.Replace("-", "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(args.NewTextValue))
                        ((Entry)sender).Text = args.NewTextValue.Substring(0, args.NewTextValue.Length - 1).ToString();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(args.NewTextValue))
                {
                    char LastChar = ' ';
                    if (!string.IsNullOrEmpty(args.NewTextValue))
                    {
                        char[] textValue = args.NewTextValue.ToCharArray();
                        LastChar = textValue[textValue.Length - 1];
                    }
                    if (LastChar >= 46 && LastChar <= 57)
                    {
                        if (args.NewTextValue.Substring(args.NewTextValue.Length - 1) != ".")
                        {
                            if (args.NewTextValue.Contains("."))
                            {
                                string[] SplitByDecimal = args.NewTextValue.Split('.');
                                string BeforeDecimal = string.Empty;
                                string AfterDecimal = string.Empty;
                                if (SplitByDecimal[0].Length > 9)
                                {
                                    BeforeDecimal = SplitByDecimal[0].Remove(SplitByDecimal[0].Length - 1);
                                    if (BeforeDecimal.Contains("-"))
                                    {
                                        BeforeDecimal = (Convert.ToInt32(BeforeDecimal) * -1).ToString();
                                    }
                                    ((Entry)sender).Text = Math.Round(Convert.ToDecimal(BeforeDecimal + AfterDecimal), 2).ToString();
                                }
                                else
                                {
                                    if (args.NewTextValue.Contains("-"))
                                    {
                                        ((Entry)sender).Text = (Math.Round(Convert.ToDecimal(args.NewTextValue), 2) * -1).ToString();
                                    }
                                    else
                                    {
                                        ((Entry)sender).Text = Math.Round(Convert.ToDecimal(args.NewTextValue), 2).ToString();
                                    }
                                }
                            }
                            else
                            {
                                if (args.NewTextValue.Length > 9)
                                {
                                    if (args.NewTextValue.Contains("-"))
                                    {
                                        ((Entry)sender).Text = (Math.Round(Convert.ToDecimal(args.NewTextValue.Remove(args.NewTextValue.Length - 1)), 2) * -1).ToString();
                                    }
                                    else
                                    {
                                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                                    }
                                }
                                else
                                {
                                    if (args.NewTextValue.Contains("-"))
                                    {
                                        ((Entry)sender).Text = args.NewTextValue.Replace("-", "");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(args.NewTextValue))
                            ((Entry)sender).Text = args.NewTextValue.Substring(0, args.NewTextValue.Length - 1).ToString();
                    }
                }
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
                    // message please remove extra decimal number
                }
            }
            catch (Exception )
            {
            }
        }
    }
}
