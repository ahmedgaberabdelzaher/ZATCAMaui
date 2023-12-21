namespace ZATCAMAUI.Core.Behaviors
{ 
    class OnlyNumericValuesBehaviour : Behavior<Entry>
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
                    if (letter >= 48 && letter <= 57 || letter == 127 || letter == 8)
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
    class OnlyNumericWithDecimalValuesBehaviour : Behavior<Entry>
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
            string newvalue = args.NewTextValue;
            if (!string.IsNullOrEmpty(newvalue) && args.NewTextValue != args.OldTextValue)
            {

                foreach (char letter in args.NewTextValue.ToCharArray())
                {
                    if ((letter < 48 || letter > 57) && letter != 8 && letter != 46)
                    {
                        ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                        return;
                    }
                    else
                    {
                    }
                    if (letter == 46)
                    {
                        if (((Entry)sender).Text.IndexOf(letter) != -1)
                        {

                        }
                        else
                        {
                            ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                            return;
                        }

                    }
                }
            }
            else
            {
                return;
            }
        }
    }

}
