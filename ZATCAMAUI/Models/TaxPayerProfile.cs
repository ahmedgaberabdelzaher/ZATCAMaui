namespace ZATCAMAUI.Models
{

    //ideally it should be UserId, TIN etc. but based on API responsethe naming of variable has been matched 
    public class TaxPayerProfile
    {
        public string UserId { get; set; }
        public string Auditorz { get; set; }
        public string PrevEmail { get; set; }
        public string PreviousPwd { get; set; }
        public string MobileCountry { get; set; }
        public string Euser1 { get; set; }
        public string TpType { get; set; }
        public string VerifyEmail { get; set; }
        public string Euser { get; set; }
        public string Euser2 { get; set; }
        public string VerifyMobile { get; set; }
        public string Euser3 { get; set; }
        public string TypeChk { get; set; }
        public string Euser4 { get; set; }
        public string Fbguid { get; set; }
        public string Euser5 { get; set; }
        public string Taxpayerz { get; set; }
        public string RegIdz { get; set; }
        public string PeriodKeyz { get; set; }
        public string Submitz { get; set; }
        public string Savez { get; set; }
        public string Fbnumz { get; set; }
        public string Langz { get; set; }
        public string PortalUsrz { get; set; }
        public string Monthz { get; set; }
        public string OfficerUidz { get; set; }
        public string Approvez { get; set; }
        public string Rejectz { get; set; }
        public string CreateTxAssesz { get; set; }
        public string Xvoidz { get; set; }
        public string AmdRsnz { get; set; }
        public string ObjSubmitz { get; set; }
        public string Dmodez { get; set; }
        public string SkipBillingz { get; set; }
        public string Partner { get; set; }
        public string NameChk { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string NameOrg1 { get; set; }
        public string ActnmChk { get; set; }
        public string Actnm { get; set; }
        public string EmailChk { get; set; }
        public string Email { get; set; }
        public string EmailLoginCd { get; set; }
        public string MobileChk { get; set; }
        public string Mobile { get; set; }
        public string MobileLoginCd { get; set; }
        public string PasswordChk { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string Edit { get; set; }
        public string Cancel { get; set; }
        public string Conf { get; set; }
        public string Login { get; set; }
        public string VtpmFg { get; set; }
        // * For UI + Other API Calls
        public string Name
        {
            get
            {
                if (0 == string.Compare(TypeChk, "X"))
                    return NameFirst + NameLast;
                else
                    return NameOrg1;

            }
        }

        // Need to check
        private string _NewEmail = string.Empty;
        public string NewEmail
        {
            get { return _NewEmail; }
            set { _NewEmail = value; }
        }
        public string Tin
        {
            get { return Taxpayerz; }
            set { Taxpayerz = Partner = value; }
        }

        // Need to check
        private string _NewMobile = string.Empty;
        public string NewMobile
        {
            get { return _NewMobile; }
            set { _NewMobile = value; }
        }

        // Need to check
        private string _NewPassword = string.Empty;
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; }
        }

        // * Missmatch With Updated TPProfile Data
        public int Attempts { get; set; }
        public string Password { get; set; }
        public string Result { get; set; }
        public string Userid { get; set; }
    }
}
