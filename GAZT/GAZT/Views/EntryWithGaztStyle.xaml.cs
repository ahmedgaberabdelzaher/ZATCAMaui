using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public partial class EntryWithGaztStyle : ContentView
    {
        public EntryWithGaztStyle()
        {
            InitializeComponent();
            ImageSourceOne.ImageClicked += RightImageOn_Clicked;
            ImageSource.ImageClicked += LeftImageOn_Clicked;
            EntryField.BindingContext = this;
            //EntryLabelText.BindingContext = this;
            this.BindingContext = this;
        }
        public event EventHandler LeftImageClicked;
        public virtual void LeftImageOn_Clicked(object sender, EventArgs e)
        {
            LeftImageClicked?.Invoke(sender, e);
        }
        public event EventHandler RightImageClicked;
        public virtual void RightImageOn_Clicked(object sender, EventArgs e)
        {
            RightImageClicked?.Invoke(sender, e);
        }
        public static void Init()
        {
        }
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryWithGaztStyle), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(string), typeof(EntryWithGaztStyle), defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty SourceOneProperty = BindableProperty.Create(nameof(SourceOne), typeof(string), typeof(EntryWithGaztStyle), defaultBindingMode: BindingMode.TwoWay);
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
        public string SourceOne
        {
            get
            {
                return (string)GetValue(SourceOneProperty);
            }
            set
            {
                SetValue(SourceOneProperty, value);
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
