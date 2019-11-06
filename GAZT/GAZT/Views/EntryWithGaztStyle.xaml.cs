using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GAZT
{
    public partial class EntryWithGaztStyle : ContentView
    {
       

        public EntryWithGaztStyle()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            ImageSource.BindingContext = this;
            EntryLabelText.BindingContext = this;
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

        //public static BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEntry), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        //{
        //    var matEntry = (BorderlessEntry)bindable;
        //    matEntry.EntryField.Keyboard = (Keyboard)newVal;
        //});

        //public Keyboard Keyboard
        //{
        //    get
        //    {
        //        return (Keyboard)GetValue(KeyboardProperty);
        //    }
        //    set
        //    {
        //        SetValue(KeyboardProperty, value);
        //    }
        //}
    }
}
