using System;
using Xamarin.Forms;

namespace GAZT.CustomControl
{
    public class AmountValidator : Behavior<Entry>
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
                    if ((letter >= 48 && letter <= 57) || letter == 127 || letter == 8 || letter == 46 || letter == 44)
                    {
                    }
                    else
                    {
                        (sender as Entry).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
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
