
using ZATCAMAUI.Models.BaseModels;

namespace ZATCAMAUI.Models.CustomServices.Tawreed
{

    public class Result
    {
        public long referenceNumber { get; set; }
    }
    public class SubmitFormResponse
    {
        public Header header { get; set; }
        public Result result { get; set; }

    }

}

