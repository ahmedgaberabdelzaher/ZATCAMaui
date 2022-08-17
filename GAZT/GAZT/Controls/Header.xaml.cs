using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EGAZT.Controls
{
    public partial class Header : ContentView
    {
        public bool BackButtonVisible { get; set; } = true;
        public Header()
        {
            InitializeComponent();
            backbutton.IsVisible = BackButtonVisible;
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
            get { return base.GetValue(TitleTextProperty).ToString(); }
            set { base.SetValue(TitleTextProperty, value); }
        }

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Header)bindable;
            control.titleTxt.Text = newValue.ToString();
        }
    }
}
