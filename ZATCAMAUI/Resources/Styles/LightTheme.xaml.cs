using System.Globalization;
namespace ZATCAMAUI.Resources.Styles
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LightTheme
    {
        public static string strGAZTFontBold = string.Empty;
        public static string strGAZTFontMedium = string.Empty;
        public static string strGAZTFontRegular = string.Empty;
        public LightTheme()
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
                switch (DeviceInfo.Platform)
                {
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        strGAZTFontBold = "Somar-SemiBold";
                        strGAZTFontMedium = "Somar-SemiBold";
                        strGAZTFontRegular = "Somar-Light";
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        strGAZTFontBold = "Somar-SemiBold.otf#Somar-SemiBold";
                        strGAZTFontMedium = "Somar-SemiBold.otf#Somar-SemiBold";
                        strGAZTFontRegular = "Somar-Light.otf#Somar-Light";
                        break;
                }
            }
            else
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        strGAZTFontBold = "Somar-SemiBold";
                        strGAZTFontMedium = "Somar-SemiBold";
                        strGAZTFontRegular = "Somar-Light";
                        break;
                    case Device.Android:
                        strGAZTFontBold = "Somar-SemiBold.otf#Somar-SemiBold";
                        strGAZTFontMedium = "Somar-SemiBold.otf#SomarSemiBold";
                        strGAZTFontRegular = "Somar-Light.otf#Somar-Light";
                        break;
                }
            }
            //GAZTFontBold.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = strGAZTFontBold });
            //GAZTFontMedium.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = strGAZTFontMedium });
            //GAZTFontRegular.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = strGAZTFontRegular });
        }
    }
}