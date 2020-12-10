using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public partial class GaztEntry : ContentView
    {
        public GaztEntry()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            EntryLabelText.BindingContext = this;
            SetLabelMArgin();
        }
        public void SetLabelMArgin()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {
                EntryLabelText.Margin = new Thickness(15, -13, 15, 0);
            }
            else
            {
                EntryLabelText.Margin = new Thickness(15, -20, 15, 0);
            }
        }
        public static void Init()
        {
        }
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryWithGaztStyle), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(string), typeof(EntryWithGaztStyle), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(EntryWithGaztStyle), defaultBindingMode: BindingMode.TwoWay);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }
        public string LabelText
        {
            get
            {
                return (string)GetValue(LabelTextProperty);
            }
            set
            {
                SetValue(LabelTextProperty, value);
            }
        }
    }
}
