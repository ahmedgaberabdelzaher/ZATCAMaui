

namespace ZATCAMAUI.Core.Behaviors
{
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

                foreach (char letter in args.NewTextValue.ToCharArray())
                {
                    if (letter <= 127)//  ||(letter >= 1536 && letter <= 1791)
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
}
