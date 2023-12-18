namespace ZATCAMAUI.Core.Interfaces
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

        string GetAttachmentToDownloadsPath(string fileName, string fileContents);

        bool IsJailBreakDetected();
        byte[] GetImagePathByteArray(string filePath);


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
