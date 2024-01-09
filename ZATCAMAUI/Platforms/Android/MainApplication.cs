using Android.App;
using Android.Runtime;

namespace ZATCAMAUI.Platforms.Android;

[Application]
[MetaData("com.google.android.maps.v2.API_KEY",
            Value = "AIzaSyBPiNAyWZbs1gGcT3PolpRFvjNJCk-Xbdk")]
public class MainApplication : MauiApplication
{
    public MainApplication(nint handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
