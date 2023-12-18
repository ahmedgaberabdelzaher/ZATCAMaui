namespace ZATCAMAUI.Models
{

    public class EnumForNavigation
    {
    }
    
    public enum ComingToOTPVerificationScreenFrom
    {
        IsLogin = 0,
        IsMobile = 1,
        IsEmail = 2,
        IsTes = 3
    }
    
    public enum ComingToOptionScreenFrom
    {
        IsDashboardPage = 0,
        IsAnonymousPage = 1
    }
    
    public enum NavigateToTaxationProfilePage
    {
        IsDefault = 0,
        IsMobile = 1,
        IsEmail = 2,
    }
    
    public enum BillStatus
    {
        P = 0,
        I = 1,
        O = 2
    }
    
    public class ComingToOTPVerificationScreenFromAndNavigatingTo
    {
        public ComingToOTPVerificationScreenFrom _ComingToOTPVerificationScreenFrom { get; set; }
        public string NavigateToThisService { get; set; }
        public string MobileNumber { get; set; }
        public string tes { get; set; }
        public string LoginKey { get; set; }

    }
}
