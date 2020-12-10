using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class EnumForNavigation
    {
    }
    [Preserve(AllMembers = true)]
    public enum ComingToOTPVerificationScreenFrom
    {
        IsLogin=0,
        IsMobile=1,
        IsEmail=2,
        IsTes=3
    }
    [Preserve(AllMembers = true)]
    public enum ComingToOptionScreenFrom
    {
        IsDashboardPage = 0,
        IsAnonymousPage = 1
    }
    [Preserve(AllMembers = true)]
    public enum NavigateToTaxationProfilePage
    {
        IsDefault=0,
        IsMobile = 1,
        IsEmail = 2,
    }
    [Preserve(AllMembers = true)]
    public enum BillStatus
    {
        P = 0,
        I = 1,
        O = 2
    }
    [Preserve(AllMembers = true)]
    public class ComingToOTPVerificationScreenFromAndNavigatingTo
    {
        public ComingToOTPVerificationScreenFrom _ComingToOTPVerificationScreenFrom { get; set; }
        public string NavigateToThisService {get;set;}
        public string MobileNumber { get; set; }
        public string tes { get; set; }
        public string LoginKey { get; set; }

    }
}
