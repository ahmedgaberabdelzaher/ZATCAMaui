using System;
namespace EGAZT.Models.TPProfile
{
    public class TPProfileAPIRequest
    {
        public Metadata __metadata { get; set; }
        public string UserId { get; set; } = App.TP.UserId;
        public string Auditorz { get; set; } = App.TP.Auditorz;
        public string PrevEmail { get; set; } = App.TP.PrevEmail;
        public string PreviousPwd { get; set; } = App.TP.PreviousPwd;
        public string MobileCountry { get; set; } = App.TP.MobileCountry;
        public string Euser1 { get; set; } = App.TP.Euser1;
        public string TpType { get; set; } = App.TP.TpType;
        public string VerifyEmail { get; set; } = string.Empty;
        public string Euser { get; set; } = App.TP.Euser;
        public string Euser2 { get; set; } = App.TP.Euser2;
        public string VerifyMobile { get; set; } = string.Empty;
        public string Euser3 { get; set; } = App.TP.Euser3;
        public string TypeChk { get; set; } = App.TP.TypeChk;
        public string Euser4 { get; set; } = App.TP.Euser4;
        public string Fbguid { get; set; } = App.TP.Fbguid;
        public string Euser5 { get; set; } = App.TP.Euser5;
        public string Taxpayerz { get; set; } = App.TP.Taxpayerz;
        public string RegIdz { get; set; } = App.TP.RegIdz;
        public string PeriodKeyz { get; set; } = App.TP.PeriodKeyz;
        public string Submitz { get; set; } = App.TP.Submitz;
        public string Savez { get; set; } = App.TP.Savez;
        public string Fbnumz { get; set; } = App.TP.Fbnumz;
        public string Langz { get; set; } = App.TP.Langz;
        public string PortalUsrz { get; set; } = App.TP.PortalUsrz;
        public string Monthz { get; set; } = App.TP.Monthz;
        public string OfficerUidz { get; set; } = App.TP.OfficerUidz;
        public string Approvez { get; set; } = App.TP.Approvez;
        public string Rejectz { get; set; } = App.TP.Rejectz;
        public string CreateTxAssesz { get; set; } = App.TP.CreateTxAssesz;
        public string Xvoidz { get; set; } = App.TP.Xvoidz;
        public string AmdRsnz { get; set; } = App.TP.AmdRsnz;
        public string ObjSubmitz { get; set; } = App.TP.ObjSubmitz;
        public string Dmodez { get; set; } = App.TP.Dmodez;
        public string SkipBillingz { get; set; } = App.TP.SkipBillingz;
        public string Partner { get; set; } = App.TP.Partner;
        public string NameChk { get; set; } = App.TP.NameChk;
        public string NameFirst { get; set; } = App.TP.NameFirst;
        public string NameLast { get; set; } = App.TP.NameLast;
        public string NameOrg1 { get; set; } = App.TP.NameOrg1;
        public string ActnmChk { get; set; } = App.TP.ActnmChk;
        public string Actnm { get; set; } = App.TP.Actnm;
        public string EmailChk { get; set; } = string.Empty;
        public string Email { get; set; } = App.TP.Email;
        public string EmailLoginCd { get; set; } = string.Empty;
        public string MobileChk { get; set; } = string.Empty;
        public string Mobile { get; set; } = App.TP.Mobile;
        public string MobileLoginCd { get; set; } = string.Empty;
        public string PasswordChk { get; set; } = string.Empty;
        public string PasswordOld { get; set; } = App.TP.PasswordOld;
        public string PasswordNew { get; set; } = App.TP.PasswordNew;
        public string Edit { get; set; } = App.TP.Edit;
        public string Cancel { get; set; } = App.TP.Cancel;
        public string Conf { get; set; } = string.Empty;

        // * Prepare POST API Request Data
        public static TPProfileAPIRequest PrepareRequestData(TPProfileAPIRequestDataModel APIRequestDataModel)
        {
            var metaData = new Metadata();
            metaData.id = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/TPFL_HEADERSet(Euser1='00000000000000000000',Euser='',Euser2='00000000000000000000',Euser3='00000000000000000000',Euser4='00000000000000000000',Fbguid='',Euser5='00000000000000000000',Taxpayerz='" + App.TP.Tin + "',Langz='E')";
            metaData.uri = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/TPFL_HEADERSet(Euser1='00000000000000000000',Euser='',Euser2='00000000000000000000',Euser3='00000000000000000000',Euser4='00000000000000000000',Fbguid='',Euser5='00000000000000000000',Taxpayerz='" + App.TP.Tin + "',Langz='E')";
            metaData.type = "Z_TP_PROFILE_N_SRV.TPFL_HEADER";

            string lang = "E";
            if (App.IsArabic == true) { lang = "A"; }

            var TPProfileAPIRequestData = new TPProfileAPIRequest();
            TPProfileAPIRequestData.__metadata = metaData;

            switch (APIRequestDataModel.RequestType)
            {
                case "GETOTPMOBILE":
                    TPProfileAPIRequestData.Mobile = APIRequestDataModel.NewMobile;
                    TPProfileAPIRequestData.MobileChk = "1";
                    TPProfileAPIRequestData.VerifyMobile = "X";
                    TPProfileAPIRequestData.MobileCountry = APIRequestDataModel.CountryCode;
                    break;

                case "VERIFYOTPMOBILE":
                    TPProfileAPIRequestData.Mobile = APIRequestDataModel.NewMobile;
                    TPProfileAPIRequestData.Conf = "X";
                    TPProfileAPIRequestData.MobileLoginCd = APIRequestDataModel.OTP;
                    TPProfileAPIRequestData.MobileChk = "1";
                    break;

                case "GETOTPEMAIL":
                    TPProfileAPIRequestData.Email = APIRequestDataModel.NewEmail;
                    TPProfileAPIRequestData.PrevEmail = APIRequestDataModel.OldEmail;
                    TPProfileAPIRequestData.EmailChk = "1";
                    TPProfileAPIRequestData.VerifyEmail = "X";
                    break;

                case "VERIFYOTPEMAIL":
                    TPProfileAPIRequestData.Conf = "X";
                    TPProfileAPIRequestData.PrevEmail = APIRequestDataModel.OldEmail;
                    TPProfileAPIRequestData.EmailLoginCd = APIRequestDataModel.OTP;
                    TPProfileAPIRequestData.EmailChk = "1";
                    TPProfileAPIRequestData.PasswordChk = "1";
                    TPProfileAPIRequestData.PasswordNew = APIRequestDataModel.NewPassword;
                    TPProfileAPIRequestData.PasswordOld = APIRequestDataModel.NewPassword;
                    TPProfileAPIRequestData.PreviousPwd = APIRequestDataModel.OldPassword;
                    TPProfileAPIRequestData.MobileChk = "0";
                    TPProfileAPIRequestData.Email = APIRequestDataModel.NewEmail;
                    break;

                default:
                    break;
            }
              
            TPProfileAPIRequestData.Langz = lang;
            return TPProfileAPIRequestData;
        }
    }

    public class TPProfileAPIRequestDataModel
    {
        public string NewMobile { get; set; } // Mobile
        public string CountryCode { get; set; } // - 
        public string RequestType { get; set; } // -
        public string OTP { get; set; } // MobileLoginCd + EmailLoginCd
        public string OldEmail { get; set; } // PrevEmail
        public string NewEmail { get; set; } // Email
        public string OldPassword { get; set; } // PreviousPwd
        public string NewPassword { get; set; } // PasswordOld + PasswordNew
    }
}
