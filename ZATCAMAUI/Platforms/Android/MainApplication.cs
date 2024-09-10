using Android.App;
using Android.Content.Res;
using Android.Runtime;
using Microsoft.Maui.Handlers;
using ZATCAMAUI.Core.CustomControls;
using Color = Android.Graphics;
namespace ZATCAMAUI.Platforms.Android;

[Application]
[MetaData("com.google.android.geo.API_KEY",
            Value = "AIzaSyBPiNAyWZbs1gGcT3PolpRFvjNJCk-Xbdk")]
public class MainApplication : MauiApplication
{
    public MainApplication(nint handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp()
    {
        EntryHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
        {
            if (view is GAZTBorderlessEntry)
            {
                handler.PlatformView.Background = null;
                handler.PlatformView.SetBackgroundColor(Color.Color.Transparent);
                // Remove underline
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Color.Color.Transparent);

            }
        });

        EditorHandler.Mapper.AppendToMapping("EditorBorderless", (handler, view) =>
        {
            if (view is GAZTBorderlessEditor)
            {
                handler.PlatformView.Background = null;
                handler.PlatformView.SetBackgroundColor(Color.Color.Transparent);
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Color.Color.Transparent);
            }
        });
        return MauiProgram.CreateMauiApp();
    }
}
