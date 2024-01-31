namespace ZATCAMAUI.Views.NewDesign
{
    public partial class BaseContentPage : ContentPage
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                         propertyName: "TitleText",
                                         returnType: typeof(string),
                                         declaringType: typeof(BaseContentPage),
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
            var control = (BaseContentPage)bindable;
            control.titel.TitleText = newValue.ToString();
        }
        // public bool HasBackButton { get; set; } = true;

        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create(
                                 propertyName: "HasBackButton",
                                 returnType: typeof(bool),
                                 declaringType: typeof(BaseContentPage),
                                 defaultValue: true,
                                 defaultBindingMode: BindingMode.TwoWay,
                                 propertyChanged: HasBackButtonPropertyChanged);

        private static void HasBackButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (BaseContentPage)bindable;
            control.titel.HasBackButton = bool.Parse(newValue.ToString());
        }

        public bool HasBackButton
        {
            get { return bool.Parse(GetValue(HasBackButtonProperty).ToString()); }
            set { SetValue(HasBackButtonProperty, value); }
        }



        public BaseContentPage()
        {
            InitializeComponent();


        }
        public View PancakeView
        {
            get => MainContainer;
            set => MainContainer.Content = value;
        }

        public Thickness PancakMargin
        {
            get => MainContainer.Margin;
            set => MainContainer.Margin = value;
        }
    }
}
