
using EGAZT.Models.NativeNafath;
using ZATCAMAUI.Models.BaseModels;

namespace ZATCAMAUI.Models.NativeNafath
{
    public class NativeNafathStatusModel
	{
        public Header header { get; set; }
        public ResultStatus result { get; set; }
        public MoreInformation moreInformation { get; set; }
    }
    public class ResultStatus
    {
        public string status { get; set; }
        public UserInfoModel userInfo { get; set; }
    }

  
}

