
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

    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
        public MoreInformation moreInformation { get; set; }
    }

}

