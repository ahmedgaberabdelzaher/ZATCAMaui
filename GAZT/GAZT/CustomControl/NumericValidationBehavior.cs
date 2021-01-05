using System;
using System.Linq;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
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
            
                if (!string.IsNullOrWhiteSpace(args.NewTextValue))
                {
                    bool isValidNumber = UtilityManager.IsOTPNumberValid(args.NewTextValue);
                    if (!isValidNumber)
                    {
                        ((Entry)sender).Text = isValidNumber ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
             
        }
    }
}
