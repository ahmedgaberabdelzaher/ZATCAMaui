using System;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public class AlphaNumericValidatorBehaviour : Behavior<Entry>
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
                bool isValidNumber = false;// = UtilityManager.IsUserNameValid(args.NewTextValue);
                foreach (char letter in args.NewTextValue.ToCharArray())
                {
                    if (letter <= 127)//  ||(letter >= 1536 && letter <= 1791)
                    {
                        isValidNumber = true;
                    }
                    else
                    {
                        ((Entry)sender).Text =  args.NewTextValue.Remove(args.NewTextValue.Length - 1);
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
