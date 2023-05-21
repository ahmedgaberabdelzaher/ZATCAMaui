using System;
namespace EGAZT.Models.TahqaqModels
{
    public class QrScanModel
    {
            public string ScanCode { get; set; }
            public string ScanCodeType { get; set; }
            public string ScanLocation { get; set; }
            public DateTime ScanDateTime { get; set; }
            public string ScanDeviceId { get; set; }
            public string ScanDeviceName { get; set; }
            public string ScanDeviceOS { get; set; }
            public string ScanDeviceOSVersion { get; set; }
            public string ScanDeviceLanguage { get; set; }
            public int ScanCustomerId { get; set; }
        
    }
}
