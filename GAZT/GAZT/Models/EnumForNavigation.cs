using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class EnumForNavigation
    {
         
    }
    public enum NavigateToOtp
    {
        IsLogin=0,
        IsMobile=1,
        IsEmail=2
    }
    public enum NavigateToTaxationProfilePage
    {
        IsDefault=0,
        IsMobile = 1,
        IsEmail = 2
    }

    public enum BillStatus
    {
        P = 0,
        I = 1,
        O = 2
    }
}
