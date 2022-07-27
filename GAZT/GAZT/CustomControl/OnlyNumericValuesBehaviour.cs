using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT.CustomControl
{
    [Preserve(AllMembers = true)]
    class OnlyNumericValuesBehaviour : Xamarin.Forms.Behavior<Entry>
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
                    if ((letter >= 48 && letter <= 57)  || (letter == 127) || (letter == 8))
                    {
                     
                    }
                    else
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
              
            }
        }
    }
    class OnlyNumericWithDecimalValuesBehaviour : Xamarin.Forms.Behavior<Entry>
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
                    if (((letter < 48 || letter > 57) && letter != 8 && letter != 46))
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);

                    }
                    else
                    {
                    }
                    if (letter == 46)
                    {
                        if (((Entry)sender).Text.IndexOf(letter) != -1)
                        {

                        }
                        else {
                            ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);

                        }

                    }
                }
              
            }
        }
    }

}
