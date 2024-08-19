using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
namespace ZATCAMAUI.Models.TPProfile
{

    public class TPProfileAPIRequest
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; } = App.TP.userId;
        [JsonProperty("auditor")]
        public string Auditorz { get; set; } = App.TP.auditor;
        [JsonProperty("previousEmail")]
        public string PrevEmail { get; set; } = App.TP.PrevEmail;
        [JsonProperty("previousPassword")]
        public string PreviousPwd { get; set; } = App.TP.PreviousPwd;
        [JsonProperty("mobileCountry")]
        public string MobileCountry { get; set; } = App.TP.MobileCountry;
        [JsonProperty("authenticationUser1")]
        public string Euser1 { get; set; } = App.TP.authenticationUser1;
        [JsonProperty("taxpayerType")]
        public string TpType { get; set; } = App.TP.taxpayerType;
        [JsonProperty("verifyEmail")]
        public string VerifyEmail { get; set; } = string.Empty;
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; } = App.TP.authenticationUser;
        [JsonProperty("authenticationUser2")]
        public string Euser2 { get; set; } = App.TP.authenticationUser2;
        [JsonProperty("verifyMobile")]
        public string VerifyMobile { get; set; } = string.Empty;
        [JsonProperty("authenticationUser3")]
        public string Euser3 { get; set; } = App.TP.authenticationUser3;
        [JsonProperty("typeCheck")]
        public string TypeChk { get; set; } = App.TP.typeCheck;
        [JsonProperty("authenticationUser4")]
        public string Euser4 { get; set; } = App.TP.authenticationUser4;
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; } = App.TP.formBundleGUID;
        [JsonProperty("authenticationUser5")]
        public string Euser5 { get; set; } = App.TP.authenticationUser5;
        [JsonProperty("TIN")]
        public string Taxpayerz { get; set; } = App.TP.Taxpayerz;
        [JsonProperty("contractNumber")]
        public string RegIdz { get; set; } = App.TP.RegIdz;
        [JsonProperty("peroidKey")]
        public string PeriodKeyz { get; set; } = App.TP.periodKey;
        [JsonProperty("submit")]
        public string Submitz { get; set; } = App.TP.submit;
        [JsonProperty("save")]
        public string Savez { get; set; } = App.TP.save;
        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; } = App.TP.formBundleNumber;
        [JsonProperty("language")]
        public string Langz { get; set; } = App.TP.language;
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; } = App.TP.portalUser;
        [JsonProperty("month")]
        public string Monthz { get; set; } = App.TP.month;
        [JsonProperty("username")]
        public string OfficerUidz { get; set; } = App.TP.OfficerUidz;
        [JsonProperty("approve")]
        public string Approvez { get; set; } = App.TP.approve;
        [JsonProperty("reject")]
        public string Rejectz { get; set; } = App.TP.reject;
        [JsonProperty("createTaxAssessment")]
        public string CreateTxAssesz { get; set; } = App.TP.createTaxAssessment;
        [JsonProperty("void")]
        public string Xvoidz { get; set; } = App.TP.Void;
        [JsonProperty("amendmentReason")]
        public string AmdRsnz { get; set; } = App.TP.amendmentReason;
        [JsonProperty("objectSubmit")]
        public string ObjSubmitz { get; set; } = App.TP.objectSubmit;
        [JsonProperty("mode")]
        public string Dmodez { get; set; } = App.TP.mode;
        [JsonProperty("skipBilling")]
        public string SkipBillingz { get; set; } = App.TP.skipBilling;
        [JsonProperty("partner")]
        public string Partner { get; set; } = App.TP.partner;
        [JsonProperty("nameCheck")]
        public string NameChk { get; set; } = App.TP.nameCheck;
        [JsonProperty("firstName")]
        public string NameFirst { get; set; } = App.TP.firstName;
        [JsonProperty("lastName")]
        public string NameLast { get; set; } = App.TP.lastName;
        [JsonProperty("organizationName1")]
        public string NameOrg1 { get; set; } = App.TP.organizationName;
        [JsonProperty("activityNameCheck")]
        public string ActnmChk { get; set; } = App.TP.activityNameCheck;
        [JsonProperty("activityName")]
        public string Actnm { get; set; } = App.TP.activityName;
        [JsonProperty("emailCheck")]
        public string EmailChk { get; set; } = string.Empty;
        [JsonProperty("email")]
        public string Email { get; set; } = App.TP.email;
        [JsonProperty("emailLoginCode")]
        public string EmailLoginCd { get; set; } = string.Empty;
        [JsonProperty("mobileCheck")]
        public string MobileChk { get; set; } = string.Empty;
        [JsonProperty("mobile")]
        public string Mobile { get; set; } = App.TP.mobile;
        [JsonProperty("mobileLoginCode")]
        public string MobileLoginCd { get; set; } = string.Empty;
        [JsonProperty("passwordCheck")]
        public string PasswordChk { get; set; } = string.Empty;
        [JsonProperty("oldPassword")]
        public string PasswordOld { get; set; } = App.TP.oldPassword;
        [JsonProperty("newPassword")]
        public string PasswordNew { get; set; } = App.TP.newPassword;
        [JsonProperty("edit")]
        public string Edit { get; set; } = App.TP.edit;
        [JsonProperty("cancel")]
        public string Cancel { get; set; } = App.TP.cancel;
        [JsonProperty("confirm")]
        public string Conf { get; set; } = App.TP.confirm;

        // * Prepare POST API Request Data
        public static TPProfileAPIRequest PrepareRequestData(TPProfileAPIRequestDataModel APIRequestDataModel)
        {
            var metaData = new Metadata();
            metaData.id = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/TPFL_HEADERSet(Euser1='00000000000000000000',Euser='',Euser2='00000000000000000000',Euser3='00000000000000000000',Euser4='00000000000000000000',Fbguid='',Euser5='00000000000000000000',Taxpayerz='" + App.TP.TIN + "',Langz='E')";
            metaData.uri = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_TP_PROFILE_N_SRV/TPFL_HEADERSet(Euser1='00000000000000000000',Euser='',Euser2='00000000000000000000',Euser3='00000000000000000000',Euser4='00000000000000000000',Fbguid='',Euser5='00000000000000000000',Taxpayerz='" + App.TP.TIN + "',Langz='E')";
            metaData.type = "Z_TP_PROFILE_N_SRV.TPFL_HEADER";

            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

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
                    TPProfileAPIRequestData.MobileCountry = APIRequestDataModel.CountryCode;
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
                    TPProfileAPIRequestData.EmailLoginCd = "";
                    TPProfileAPIRequestData.EmailChk = "1";
                    TPProfileAPIRequestData.PasswordChk = "";
                    TPProfileAPIRequestData.PasswordNew = "";
                    TPProfileAPIRequestData.PasswordOld = "";
                    TPProfileAPIRequestData.PreviousPwd = APIRequestDataModel.OldPassword;
                    TPProfileAPIRequestData.MobileChk = "";
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

    public class TPProfileUpdatePasswordRequestModel
    {
        public string email { get; set; }
        public string TIN { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
