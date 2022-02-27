using EGAZT;
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
                        strGAZTFontBold = "Somar-SemiBold";
                        strGAZTFontMedium = "Somar-SemiBold";
                        strGAZTFontRegular = "Somar-Light";
                        break;
                    case Device.Android:
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