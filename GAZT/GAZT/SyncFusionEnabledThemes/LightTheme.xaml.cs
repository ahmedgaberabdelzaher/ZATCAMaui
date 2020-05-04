using GAZT;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
namespace GAZTeServicesApp.Themes
{
    [Preserve(AllMembers = true)]
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
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        strGAZTFontBold = "GE_SS_Two_Bold";
                        strGAZTFontMedium = "GE_SS_Two_Medium";
                        strGAZTFontRegular = "GE_SS_Two_Light";
                        break;
                    case Device.Android:
                        strGAZTFontBold = "GE_SS_Two_Bold.ttf#GE_SS_Two_Bold";
                        strGAZTFontMedium = "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        strGAZTFontRegular = "GE_SS_Two_Light.ttf#GE_SS_Two_Light";
                        break;
                }
            }
            else
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        strGAZTFontBold = "SSTArabic-Bold";
                        strGAZTFontMedium = "SSTArabic-Medium";
                        strGAZTFontRegular = "SSTArabic-Light";
                        break;
                    case Device.Android:
                        strGAZTFontBold = "SSTArabic-Bold.ttf#SSTArabic-Bold";
                        strGAZTFontMedium = "SSTArabic-Medium.ttf#SSTArabic-Medium";
                        strGAZTFontRegular = "SSTArabic-Light.ttf#SSTArabic-Light";
                        break;
                }
            }
            //GAZTFontBold.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = strGAZTFontBold });
            //GAZTFontMedium.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = strGAZTFontMedium });
            //GAZTFontRegular.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = strGAZTFontRegular });
        }
    }
}