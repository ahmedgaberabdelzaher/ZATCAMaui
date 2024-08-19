
namespace ZATCAMAUI.Models.SignUP
{
    public class ImageCaptchaModel
    {
        public Header header { get; set; }
        public CaptchaData data { get; set; }
    }

    public class CaptchaData
    {
        public string cval { get; set; }
        public string GUID { get; set; }
    }
}

