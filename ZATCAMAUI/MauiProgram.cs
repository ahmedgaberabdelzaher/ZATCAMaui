using CommunityToolkit.Maui;
using Maui.PancakeView;
using RGPopup.Maui.Extensions;
using Syncfusion.Maui.Core.Hosting;
using ZXing.Net.Maui.Controls;

namespace ZATCAMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseBarcodeReader()
            .UseMauiRGPopup()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .UsePancakeViewCompat()
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
			});

		return builder.Build();
	}
}
