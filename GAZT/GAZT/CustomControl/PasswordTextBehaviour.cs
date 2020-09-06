using System;
using Xamarin.Forms;

namespace EGAZT
{
    public class PasswordTextBehaviour : Behavior<Entry>
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
                bool isValidNumber = false;// = UtilityManager.IsUserNameValid(args.NewTextValue);
                char[] textValue = args.NewTextValue.ToCharArray();
                for (int i = 0; i < textValue.Length; i++)
                {
                    if (textValue[i] > 256)
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(i, 1);
                        break;
                        // ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                    }
                }
            }
        }
    }
}
