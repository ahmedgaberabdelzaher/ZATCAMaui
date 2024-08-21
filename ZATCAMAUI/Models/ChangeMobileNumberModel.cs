using System.Collections.Generic;

using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class ChangeMobileNumberModel
	{
        [JsonProperty("data")]
        public Data d;
        
        [JsonProperty("result")]
        public Data result;
    }
    
    public class Data
    {
        //[JsonProperty("__metadata")]
        //public Metadata Metadata;

        [JsonProperty("userType")]
        public string UserTypz;

        [JsonProperty("CRName")]
        public string Crname;

        [JsonProperty("transactionType")]
        public string TxnTpz;

        [JsonProperty("TINType")]
        public string Tintyp;

        [JsonProperty("timeStampCreation")]
        public string TimestampCr;

        [JsonProperty("timeStampChange")]
        public string TimestampCh;

        [JsonProperty("stepNumber")]
        public string StepNumberz;

        [JsonProperty("statusCode")]
        public string Statusz;

        [JsonProperty("sourceApplication")]
        public string SrcAppz;

        //[JsonProperty("returnId")]
        //public string ReturnIdz;

        [JsonProperty("returnId")]
        public string ReturnId;

        [JsonProperty("portalUser")]
        public string PortalUsrz;

        [JsonProperty("OTPGUID")]
        public string OtpGuid;

        [JsonProperty("OTP")]
        public string Otp;

        [JsonProperty("operation")]
        public string Operationz;

        [JsonProperty("oldTelephoneNumber")]
        public string OldTlnmbr;

        [JsonProperty("userName")]
        public string Officerz;

        //[JsonProperty("userName")]
        //public string OfficerTz;

        [JsonProperty("newTelephoneNumber")]
        public string NewTlnmbr;

        [JsonProperty("nafathGUID")]
        public string Nafathguid;

        [JsonProperty("nafathAccount")]
        public string Nafathfg;

        [JsonProperty("mangerName")]
        public string Mgrnm;

        [JsonProperty("mangerId")]
        public string Mgrid;

        [JsonProperty("messageError")]
        public string McErrorFg;

        [JsonProperty("URL")]
        public string Link;

        [JsonProperty("language")]
        public string Langz;

        [JsonProperty("channel")]
        public string Inpchz;

        [JsonProperty("idType")]
        public string Idtyp;

        //[JsonProperty("TIN")]
        public string Gpartz;

        [JsonProperty("TIN")]
        public string Gpart;

        [JsonProperty("formProcess")]
        public string Formprocz;

        [JsonProperty("formBundleNumber")]
        public string Fbnumz;

       // [JsonProperty("formBundleNumber")]
        public string Fbnum;

        [JsonProperty("companyName")]
        public string Cmpnm;

        [JsonProperty("captchaCode")]
        public string Captcha;

        [JsonProperty("agreeCheckBox")]
        public string Agrchk;

        [JsonProperty("attachments")]
        public List<object> ATTACHSet;

        [JsonProperty("notes")]
        public List<object> NOTESSet;

        [JsonProperty("idTypes")]
        public List<IDTypes> IDTYPSet;

        [JsonProperty("errorMessages")]
        public List<ErrorTypes> MCERRORSet;
    }

    public class ATTACHSet1
    {
        [JsonProperty("results")]
        public List<object> Results;
    }
    
    
    
    public class IDTYPSet
    {
        [JsonProperty("results")]
        public List<IDTypes> Results;
    }

    
    public class MCERRORSet
    {
        [JsonProperty("results")]
        public List<ErrorTypes> Results;
    }
    
    public class NOTESSet1
    {
        [JsonProperty("results")]
        public List<object> Results;
    }
    
    public class IDTypes
    {
        [JsonProperty("idType")]
        public string Idtyp;

        [JsonProperty("description")]
        public string Text;
    }
    
    
    
    public class ErrorTypes {
        [JsonProperty("messageId")]
        public string Id;

        [JsonProperty("messageNumber")]
        public string Number;

        [JsonProperty("messageDescription")]
        public string Message;
    }
}


