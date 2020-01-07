using System;
using System.Linq;
using GAZT.Manager;
using Xamarin.Forms;

namespace GAZT
{
    public class NumericValidationBehavior : Behavior<Entry>
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

            if(args.NewTextValue.Length <= 4)
            {
                if (!string.IsNullOrWhiteSpace(args.NewTextValue))
                {
                    bool isValidNumber = UtilityManager.IsOTPNumberValid(args.NewTextValue);
                    if (!isValidNumber)
                    {
                        ((Entry)sender).Text = isValidNumber ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
                if(args.NewTextValue.Length == 4)
                    ((Entry)sender).Unfocus();
            }
            else
            {
                ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                ((Entry)sender).Unfocus();
            }
        }
    }
}
