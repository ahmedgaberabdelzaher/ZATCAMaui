using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class ChangeMobileNumberModel
	{
        [JsonProperty("d")]
        public Data d;
    }
    [Preserve(AllMembers = true)]
    public class Data
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata;

        [JsonProperty("UserTypz")]
        public string UserTypz;

        [JsonProperty("Crname")]
        public string Crname;

        [JsonProperty("TxnTpz")]
        public string TxnTpz;

        [JsonProperty("Tintyp")]
        public string Tintyp;

        [JsonProperty("TimestampCr")]
        public object TimestampCr;

        [JsonProperty("TimestampCh")]
        public object TimestampCh;

        [JsonProperty("StepNumberz")]
        public string StepNumberz;

        [JsonProperty("Statusz")]
        public string Statusz;

        [JsonProperty("SrcAppz")]
        public string SrcAppz;

        [JsonProperty("ReturnIdz")]
        public string ReturnIdz;

        [JsonProperty("ReturnId")]
        public string ReturnId;

        [JsonProperty("PortalUsrz")]
        public string PortalUsrz;

        [JsonProperty("OtpGuid")]
        public string OtpGuid;

        [JsonProperty("Otp")]
        public string Otp;

        [JsonProperty("Operationz")]
        public string Operationz;

        [JsonProperty("OldTlnmbr")]
        public string OldTlnmbr;

        [JsonProperty("Officerz")]
        public string Officerz;

        [JsonProperty("OfficerTz")]
        public string OfficerTz;

        [JsonProperty("NewTlnmbr")]
        public string NewTlnmbr;

        [JsonProperty("Nafathguid")]
        public string Nafathguid;

        [JsonProperty("Nafathfg")]
        public string Nafathfg;

        [JsonProperty("Mgrnm")]
        public string Mgrnm;

        [JsonProperty("Mgrid")]
        public string Mgrid;

        [JsonProperty("McErrorFg")]
        public string McErrorFg;

        [JsonProperty("Link")]
        public string Link;

        [JsonProperty("Langz")]
        public string Langz;

        [JsonProperty("Inpchz")]
        public string Inpchz;

        [JsonProperty("Idtyp")]
        public string Idtyp;

        [JsonProperty("Gpartz")]
        public string Gpartz;

        [JsonProperty("Gpart")]
        public string Gpart;

        [JsonProperty("Formprocz")]
        public string Formprocz;

        [JsonProperty("Fbnumz")]
        public string Fbnumz;

        [JsonProperty("Fbnum")]
        public string Fbnum;

        [JsonProperty("Cmpnm")]
        public string Cmpnm;

        [JsonProperty("Captcha")]
        public string Captcha;

        [JsonProperty("Agrchk")]
        public string Agrchk;

        [JsonProperty("ATTACHSet")]
        public ATTACHSet1 ATTACHSet;

        [JsonProperty("NOTESSet")]
        public NOTESSet1 NOTESSet;

        [JsonProperty("IDTYPSet")]
        public IDTYPSet IDTYPSet;

        [JsonProperty("MC_ERRORSet")]
        public MCERRORSet MCERRORSet;
    }
    [Preserve(AllMembers = true)]
    public class ATTACHSet1
    {
        [JsonProperty("results")]
        public List<object> Results;
    }
    [Preserve(AllMembers = true)]
    public class IDTYPSet
    {
        [JsonProperty("results")]
        public List<IDTypes> Results;
    }
    [Preserve(AllMembers = true)]
    public class MCERRORSet
    {
        [JsonProperty("results")]
        public List<ErrorTypes> Results;
    }
    [Preserve(AllMembers = true)]
    public class NOTESSet1
    {
        [JsonProperty("results")]
        public List<object> Results;
    }
    [Preserve(AllMembers = true)]
    public class IDTypes
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata;

        [JsonProperty("Idtyp")]
        public string Idtyp;

        [JsonProperty("Text")]
        public string Text;
    }
    [Preserve(AllMembers = true)]
    public class ErrorTypes {
        [JsonProperty("__metadata")]
        public Metadata Metadata;

        [JsonProperty("Id")]
        public string Id;

        [JsonProperty("Number")]
        public string Number;

        [JsonProperty("Message")]
        public string Message;
    }
}

