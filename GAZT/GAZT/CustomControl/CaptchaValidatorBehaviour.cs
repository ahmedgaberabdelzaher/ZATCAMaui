using System;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public class CaptchaValidatorBehaviour : Behavior<Entry>
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
            bool isValidNumber = false;
            if (args.NewTextValue.Length <= 6)
            {
                foreach (char letter in args.NewTextValue.ToCharArray())
                {
                    if (!((letter >= 48 && letter <= 57) || (letter >= 65 && letter <= 90) || (letter >= 97 && letter <= 122)))
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
                if (args.NewTextValue.Length == 6)
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
