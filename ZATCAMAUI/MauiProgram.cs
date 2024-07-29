using CommunityToolkit.Maui;
using Maui.PancakeView;
using Mopups.Hosting;
using Syncfusion.Maui.Core.Hosting;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Interfaces;

#if IOS
using ZATCAMAUI.Platforms.iOS.DependencyServices;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

#endif
#if ANDROID
using ZATCAMAUI.Platforms.Android.DependencyServices;
using ZATCAMAUI.Platforms.Android.CustomRenderer;
#endif

using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;
using ZXing.Net.Maui.Controls;
using IDeviceInfo = ZATCAMAUI.Core.Interfaces.IDeviceInfo;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Maui.GoogleMaps.Hosting;

namespace ZATCAMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        try
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .ConfigureMopups()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .UsePancakeViewCompat()
                .UseMauiCompatibility()
                .UseMauiMaps()
                //.UseMauiCommunityToolkitMaps("key")
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("GE_SS_Two_Bold.ttf", "GE_SS_Two_Bold");
                    fonts.AddFont("GE_SS_Two_Light.ttf", "GE_SS_Two_Light");
                    fonts.AddFont("GE_SS_Two_Medium.ttf", "GE_SS_Two_Medium");

                    fonts.AddFont("Mada-Regular.ttf", "Mada-Regular");

                    fonts.AddFont("MyMaterialIcon.ttf", "MyMaterialIcon");

                    fonts.AddFont("Somar-Bold.otf", "Somar-Bold");
                    fonts.AddFont("Somar-Light.otf", "Somar-Light");
                    fonts.AddFont("Somar-Regular.otf", "Somar-Regular");
                    fonts.AddFont("Somar-SemiBold.otf", "Somar-SemiBold");

                    fonts.AddFont("SSTArabic-Bold.ttf", "SSTArabic-Bold");
                    fonts.AddFont("SSTArabic-Light.ttf", "SSTArabic-Light");
                    fonts.AddFont("SSTArabic-Medium.ttf", "SSTArabic-Medium");
                    fonts.AddFont("SSTArabic-Roman.ttf", "SSTArabic-Roman");

                    fonts.AddFont("UIFontIcons.ttf", "UIFontIcons");

                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
#if ANDROID
                .UseGoogleMaps()
#elif IOS
                .UseGoogleMaps("AIzaSyCnIhK1NNzYNX-pZ1JjZpsLAXzHPgQOgSM")
#endif
             .ConfigureMauiHandlers((handlers) =>
              {
#if ANDROID
                  handlers.AddCompatibilityRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer));
                  handlers.AddCompatibilityRenderer(typeof(GAZTBorderlessEditor), typeof(GAZTBorderlessEditorRenderer));
                  handlers.AddCompatibilityRenderer(typeof(GAZTBorderlessEntry), typeof(GAZTBorderlessEntryRenderer));
                  handlers.AddCompatibilityRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer));
                  handlers.AddCompatibilityRenderer(typeof(MyWebView), typeof(CustomWebViewRenderer));
                  handlers.AddCompatibilityRenderer(typeof(RoundCornersEffect), typeof(RoundCornersEffectDroid));
#elif IOS
                  handlers.AddCompatibilityRenderer(typeof(HybridWebView), typeof(HybridCustomWebViewRenderer));
                  handlers.AddCompatibilityRenderer(typeof(MyWebView), typeof(CustomWebViewRenderer));
                  handlers.AddCompatibilityRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer));
                  handlers.AddCompatibilityRenderer(typeof(GAZTBorderlessEditor), typeof(GAZTBorderlessEditorRenderer));
                  handlers.AddCompatibilityRenderer(typeof(GAZTBorderlessEntry), typeof(GAZTBorderlessEntryRenderer));
#endif 
              });

#if ANDROID
            DependencyService.Register<IPrintService, AndroidDownloader>();
            DependencyService.Register<IBaseUrl, BaseUrl_Android>();
            DependencyService.Register<IForceUpdate, ForceUpdate>();
            DependencyService.Register<ILocalFileProvider, LocalFileProvider>();
            DependencyService.Register<IStatusBar, StatusBarImplementation>();
            DependencyService.Register<IDeviceInfo, ZATCADeviceInfo>();
#endif

#if IOS
            DependencyService.Register<IForceUpdate, ForceUpdate>();
            DependencyService.Register<IDeviceInfo, ZATCADeviceInfo>();
            DependencyService.Register<IApplePayAuthorizer, ApplePayAuthorizer>();
            DependencyService.Register<IBaseUrl, BaseUrl_iOS>();
            DependencyService.Register<IPrintService, IOSDownloader>();
            DependencyService.Register<IStatusBar, StatusBarImplementation>();

#endif

            return builder.Build();
        }
        catch (Exception)
        {
            return null;
        }

    }
}
