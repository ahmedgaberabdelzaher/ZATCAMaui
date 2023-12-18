namespace ZATCAMAUI.Core.Behaviors
{

    public class RestrictSpecialCharactorsBehaviour : Behavior<Entry>
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
                    if (letter >= 48 && letter <= 57 || letter >= 65 && letter <= 90 || letter >= 97 && letter <= 122 || letter == 127 || letter == 8)
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
