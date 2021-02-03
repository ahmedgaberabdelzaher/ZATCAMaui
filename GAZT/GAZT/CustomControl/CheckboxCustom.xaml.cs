using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace GAZT.CustomControl
{
    [Preserve(AllMembers = true)]

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckboxCustom : ContentView
    {
        public event EventHandler<bool> CheckChanged;

        private bool isChecked;
        //public bool IsChecked
        //{
        //    get { return isChecked; }
        //    set
        //    {
        //        isChecked = value;
        //        img.Source = !isChecked ? "unchecked_box" : "checked_box";
        //    }
        //}
        public bool IsChecked
        {
            get
            {
                return Convert.ToBoolean(base.GetValue(IsCheckedProperty));
            }
            set
            {
                base.SetValue(IsCheckedProperty, value);
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