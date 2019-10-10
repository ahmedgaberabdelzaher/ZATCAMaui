using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace GAZT.Views
{
    public partial class LogInView : ContentPage
    {
        public LogInView()
        {
            InitializeComponent();
            UserNameMobileNumber.HorizontalTextAlignment = TextAlignment.Start;
        }
        private void OnArClicked(object sender, EventArgs e)
        {
            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            // AppResources.ResourceManager.ReleaseAllResources();
            this.FlowDirection = FlowDirection.RightToLeft;
        }
        private void OnEnClicked(object sender, EventArgs e)
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            // AppResources.ResourceManager.ReleaseAllResources();
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
