
namespace ZATCAMAUI.Core.CustomControls
{
    public partial class Header : ContentView
    {
        //   public bool BackButtonVisible { get; set; } = true;
        public Header()
        {
            InitializeComponent();
            //  backbutton.IsVisible = BackButtonVisible;
        }


        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create(
                                 propertyName: "HasBackButton",
                                 returnType: typeof(bool),
                                 declaringType: typeof(Header),
                                 defaultValue: true,
                                 defaultBindingMode: BindingMode.TwoWay,
                                 propertyChanged: HasBackButtonPropertyChanged);

        private static void HasBackButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Header)bindable;
            control.backbutton.IsVisible = bool.Parse(newValue.ToString());
        }

        public bool HasBackButton
        {
            get { return bool.Parse(GetValue(HasBackButtonProperty).ToString()); }
            set { SetValue(HasBackButtonProperty, value); }
        }



        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                 propertyName: "TitleText",
                                                 returnType: typeof(string),
                                                 declaringType: typeof(Header),
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
            var control = (Header)bindable;
            control.titleTxt.Text = newValue.ToString();
        }
    }
}
