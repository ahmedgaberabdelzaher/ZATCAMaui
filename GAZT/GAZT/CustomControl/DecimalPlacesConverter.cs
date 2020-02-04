using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GAZT.CustomControl
{
    public class DecimalPlacesConverter : Behavior<Entry>
    {
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
            if (!string.IsNullOrEmpty(args.NewTextValue))
            {
                if (args.NewTextValue.Substring(args.NewTextValue.Length - 1) != ".")
                {
                    if (args.NewTextValue.Contains("."))
                    {
                        string[] SplitByDecimal = args.NewTextValue.Split('.');
                        string BeforeDecimal = string.Empty;
                        string AfterDecimal = string.Empty;
                        if (SplitByDecimal[0].Length > 14)
                        {
                            BeforeDecimal = SplitByDecimal[0].Remove(SplitByDecimal[0].Length - 1);

                            ((Entry)sender).Text = Math.Round(Convert.ToDecimal(BeforeDecimal + AfterDecimal), 2).ToString();

                        }
                        else
                        {
                            ((Entry)sender).Text = Math.Round(Convert.ToDecimal(args.NewTextValue), 2).ToString();
                        }
                    }
                    else
                    {
                        if (args.NewTextValue.Length > 14)
                        {
                            ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                        }
                    }
                }
            }
        }
    }
}
