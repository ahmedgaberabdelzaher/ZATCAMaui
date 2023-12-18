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
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
