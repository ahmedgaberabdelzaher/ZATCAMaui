using System;
namespace GAZT.Helper
{
    public interface IDeviceInfo
    {
        double GetDeviceHeight();
        double GetDeviceWidth();
        string[] GetAttachmentTypeString();

        string[] GetAttachmentTypeStringForZakat();

        string[] GetAttachmentTypeStringForAll();

        string[] GetAttachmentTypeStringForTaxEvasion();

        string GetDeviceUdid();

        bool IsJailBreakDetected();


       // bool IsJailBreakDetected { get; }

        //int ScreenHeight { get; }
        //int ScreenWidth { get; }

       // string DeviceId { get; }
      //  string Manufacturer { get; }
        string Model { get; }
        string OperatingSystem { get; }
        string OperatingSystemVersion { get; }
        bool IsSimulator { get; }
        bool IsTablet { get; }
    }
}
