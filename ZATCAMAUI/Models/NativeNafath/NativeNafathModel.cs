using ZATCAMAUI.Models.BaseModels;

namespace ZATCAMAUI.Models.NativeNafath
{
	public class NativeNafathModel
	{
        public Header header { get; set; }
        public Result result { get; set; }
        public MoreInformation moreInformation { get; set; }
    }

    public class Result
    {
        public string transactionId { get; set; }
        public int randomNumber { get; set; }
    }
}

