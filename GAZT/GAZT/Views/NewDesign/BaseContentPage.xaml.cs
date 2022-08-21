using System;
using System.Collections.Generic;
using EGAZT.Controls;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign
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
            get { return base.GetValue(TitleTextProperty).ToString(); }
            set { base.SetValue(TitleTextProperty, value); }
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
            get { return bool.Parse(base.GetValue(HasBackButtonProperty).ToString()); }
            set { base.SetValue(HasBackButtonProperty, value); }
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
    }
}
