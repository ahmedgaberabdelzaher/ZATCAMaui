using System;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public class MobileNumberValidatorBehaviour : Behavior<Entry>
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
            if (!string.IsNullOrEmpty(args.NewTextValue) && args.NewTextValue.Length <= 15)
            {
         
                foreach (char letter in args.NewTextValue.ToCharArray())
                {
                    if (letter >= 48 && letter <= 57)
                    {
                    
                    }
                    else
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
              
            }
            else
            {
                if(!string.IsNullOrEmpty(args.NewTextValue))
                ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }
    }
}
