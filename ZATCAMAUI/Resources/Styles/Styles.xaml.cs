using System.Globalization;

namespace ZATCAMAUI.Resources.Styles
{
    /// <summary>
    /// Class helps to reduce repetitive markup, and allows an apps appearance to be more easily changed.
    /// </summary>

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
                string langName = "ar-AE";//"en-US";// "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                string langName = "en-US";//"en-US";// "ar-AE";
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