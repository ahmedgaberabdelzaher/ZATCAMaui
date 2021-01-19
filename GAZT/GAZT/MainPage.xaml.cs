using System;
using System.Globalization;
using Xamarin.Forms;
using EGAZT;

namespace GAZT
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
