using System.ComponentModel;

namespace ZATCAMAUI.Core.CustomControls
{
    public partial class EntryWithSAR : ContentView, INotifyPropertyChanged
    {
        public EntryWithSAR()
        {

            InitializeComponent();

        }
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: "TitleText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(EntryWithSAR),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TitleTextPropertyChanged);

        public string TitleText
        {
            get { return GetValue(TitleTextProperty).ToString(); }
            set { SetValue(TitleTextProperty, value); }
        }

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EntryWithSAR)bindable;
            control.Title.Text = newValue.ToString();
        }

        public static readonly BindableProperty ValueTextProperty = BindableProperty.Create(
                                                        propertyName: "ValueText",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(EntryWithSAR),
                                                        defaultValue: "",
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanging: ValueTextPropertyChanging);

        private static void ValueTextPropertyChanging(BindableObject bindable, object oldValue, object newValue)
        {

            var control = (EntryWithSAR)bindable;
            if (newValue != null)
            {
                control.TextValue.Text = newValue?.ToString();
            }
        }




        public string ValueText
        {
            get { return GetValue(ValueTextProperty)?.ToString(); }
            set { SetValue(ValueTextProperty, value); OnPropertyChanged(); }
        }

        private static void ValueTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

            var control = (EntryWithSAR)bindable;
            control.TextValue.Text = newValue?.ToString();


            // control.TextValue.Placeholder = newValue.ToString();
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(EntryWithSAR));
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static BindableProperty TextProperty = BindableProperty.Create("TextProperty", typeof(string), typeof(EntryWithSAR), defaultBindingMode: BindingMode.TwoWay);
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
    }
}
