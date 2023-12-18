namespace ZATCAMAUI.Core.CustomControls
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckboxCustom : ContentView
    {
        public event EventHandler<bool> CheckChanged;

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(GetValue(IsCheckedProperty));
            }
            set
            {
                SetValue(IsCheckedProperty, value);
                //img.Source = !value ? "unchecked_box" : "checked_box";
            }
        }
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
        propertyName: "IsChecked",
        returnType: typeof(bool),
        declaringType: typeof(CheckboxCustom),
        defaultValue: false,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: IsCheckedPropertyChanged);
        private static void IsCheckedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CheckboxCustom)bindable;
            control.img.Source = !(bool)newValue ? "unchecked_box" : "checked_box";
        }


        public CheckboxCustom()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsChecked = !IsChecked;
            CheckChanged?.Invoke(sender, IsChecked);
        }
    }
}