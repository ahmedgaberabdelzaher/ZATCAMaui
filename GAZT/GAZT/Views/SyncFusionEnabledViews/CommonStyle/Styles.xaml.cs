using GAZT;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
namespace GAZTeServicesApp.Views.LandingPage
{
    /// <summary>
    /// Class helps to reduce repetitive markup, and allows an apps appearance to be more easily changed.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Styles
    {
        public static string strGAZTFontBold = string.Empty;
        public static string strGAZTFontMedium = string.Empty;
        public static string strGAZTFontRegular = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="Styles" /> class.
        /// </summary>
        public Styles()
        {
            InitializeComponent();
        }
        public void OnToggleENAR()
        {
            if (App.IsArabic)
            {
                String langName = "ar-AE";//"en-US";// "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                String langName = "en-US";//"en-US";// "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            if (App.IsArabic)
            {
                Application.Current.Resources["GAZTFontBold"] = Application.Current.Resources["GAZTBoldArabic"];
            }
            else
            {
                Application.Current.Resources["GAZTFontBold"] = Application.Current.Resources["GAZTBoldEnglish"];
            }
        }
    }
}