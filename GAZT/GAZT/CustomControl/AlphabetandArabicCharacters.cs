using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    // class AlphabetandArabicCharacters
    public class AlphabetandArabicCharacters : Behavior<Entry>
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

                foreach (char letter in args.NewTextValue.ToCharArray())
                {
                    if ((letter >= 65 && letter <= 90) || (letter >= 97 && letter <= 122) || (letter == 127) || (letter == 8) || (letter == 32) || (letter >= 1536 && letter <= 1791))
                    {
                        if ((letter >= 1542 && letter <= 1545) || (letter >= 1642 && letter <= 1645) || (letter >= 1776 && letter <= 1785) || (letter >= 1632 && letter <= 1641) || (letter >= 1537 && letter <= 1539) || (letter == 1548) || (letter == 1563) || (letter == 1567) || (letter == 1547))
                        {
                            ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                        }
                        else
                        {
                       
                        }
                        
                    }
                    else
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
                //if (!isValidNumber)
                //    {
                //        ((Entry)sender).Text = isValidNumber ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                //    }
                //}
            }
        }
    }
}
