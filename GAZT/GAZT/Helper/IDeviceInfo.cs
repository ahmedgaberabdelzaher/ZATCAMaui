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

        string GetAttachmentToDownloadsPath(string fileName, string fileContents);

    }
}
