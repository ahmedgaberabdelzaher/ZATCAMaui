using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPPageView : ContentPage
    {

        #region Variable
        OTPPageViewModel viewModel;
        #endregion

        #region Constructor
        public OTPPageView()
        {
            viewModel = App.Locator.OTPPageView;
            InitializeComponent();
            this.BindingContext = viewModel;
        }
        #endregion

        #region Method
        #endregion

        private void TextChangedForOne(object sender, TextChangedEventArgs e)
        {
            string OTPId = FirstEntry.Text;
            if(OTPId.Length==1)
            {
                SecondEntry.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(FirstEntry.Text))
                {
                    
                }
                else
                {
                    FirstEntry.Text = FirstEntry.Text.Substring(0, 1);
                }
            }
        }
        private void TextChangedForTwo(object sender, TextChangedEventArgs e)
        {
            string OTPId = SecondEntry.Text;
            if (OTPId.Length == 1)
            {
                ThirdEntry.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(SecondEntry.Text))
                {
                    FirstEntry.Focus();
                }
                else
                {
                    SecondEntry.Text = SecondEntry.Text.Substring(0, 1);
                }
            }
        }
        private void TextChangedForThree(object sender, TextChangedEventArgs e)
        {
            string OTPId = ThirdEntry.Text;
            if (OTPId.Length == 1)
            {
                FourthEntry.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(ThirdEntry.Text))
                {
                    SecondEntry.Focus();
                }
                else
                {
                    ThirdEntry.Text = ThirdEntry.Text.Substring(0, 1);
                }
            }
        }
        private void TextChangedForFour(object sender, TextChangedEventArgs e)
        {
            string OTPId = FourthEntry.Text;
            if (OTPId.Length == 1)
            {
               /// SecondEntry.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(FourthEntry.Text))
                {
                    ThirdEntry.Focus();
                }
                else
                {
                    FourthEntry.Text = FourthEntry.Text.Substring(0, 1);
                }
            }
        }
    }
}