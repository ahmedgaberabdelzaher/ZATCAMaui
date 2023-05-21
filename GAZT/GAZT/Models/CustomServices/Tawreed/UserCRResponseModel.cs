using System;
using EGAZT.Helper;

namespace EGAZT.Models.CustomServices.Tawreed
{
    public class UserCRResponseModel
    {
            public int id { get; set; }
            public int registeredUserID { get; set; }
            public string crName_En { get; set; }
            public string crName_Ar { get; set; }
            public string crNumber { get; set; }
            public string crType { get; set; }
        public string Name
        {
            get
            {
                return NameLocalization.GetLocalizedName(crName_Ar, crName_En);
            }
        }
    }
}

    

