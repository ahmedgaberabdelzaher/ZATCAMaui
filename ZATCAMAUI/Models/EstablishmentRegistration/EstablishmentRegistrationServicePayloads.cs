using System.ComponentModel;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Exceptions;
using static System.Net.Mime.MediaTypeNames;

namespace ZATCAMAUI.Models.EstablishmentRegistration
{

    public class JsonFieldListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(NregIdSet) || objectType == typeof(NregOutletSet) || objectType == typeof(NregActivitySet) || objectType == typeof(NregAddressSet) || objectType == typeof(OffNotesSet);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is NregIdSet)
            {
                List<Nreg_IdItem> results = (value as NregIdSet)?.results;
                JArray jArray = JArray.FromObject(results, serializer);
                jArray.WriteTo(writer);
            }
            if (value is NregOutletSet)
            {
                List<Nreg_OutletItem> results = (value as NregOutletSet)?.results;
                JArray jArray = JArray.FromObject(results, serializer);
                jArray.WriteTo(writer);
            }
            if (value is NregActivitySet)
            {
                List<Nreg_ActivityItem> results = (value as NregActivitySet)?.results;
                JArray jArray = JArray.FromObject(results, serializer);
                jArray.WriteTo(writer);
            }
            if (value is NregAddressSet)
            {
                List<Nreg_AddressItem> results = (value as NregAddressSet)?.results;
                JArray jArray = JArray.FromObject(results, serializer);
                jArray.WriteTo(writer);
            }
            if (value is OffNotesSet)
            {

                List<OffNotes> results = (value as OffNotesSet)?.results;
                JArray jArray = JArray.FromObject(results, serializer);
                jArray.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    
    public class HTTPBadRequestException : GAZTException
    {
        public HTTPBadRequestException(string expception) : base(expception) { }
    }
    
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class BranchesDropDownModel
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        //public string Spras { get; set; }
        public string authorizationObject { get; set; }
        public string authorizationGroup { get; set; }
        public string branchDescription { get; set; }
        public string language { get; set; }
        public override string ToString()
        {
            return branchDescription = string.IsNullOrEmpty(branchDescription) ? "" : branchDescription;
        }
    }
    
    public class Nreg_CpersonItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("isContactOld")]
        public bool Cpoldfg { get; set; }
        public string Mandt { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        public bool Gmatt { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [JsonProperty("sourceIdentify")]
        public string Srcidentify { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        public DateTime? Enddt { get; set; }
        [JsonProperty("")]
        public string Contacttp { get; set; }
        [JsonProperty("isDefault")]
        public string Defaultfg { get; set; }
        [JsonProperty("outletName")]
        public string Outletnm { get; set; }
        public DateTime? Startdt { get; set; }
        [JsonProperty("firstName")]
        public string Firstnm { get; set; }
        [JsonProperty("lastName")]
        public string Lastnm { get; set; }
        public string Relationtp { get; set; }
        [JsonProperty("fatherName")]
        public string Fathernm { get; set; }
        [JsonProperty("grandfatherName")]
        public string Grandfathernm { get; set; }
        [JsonProperty("familyName")]
        public string Familynm { get; set; }
        public DateTime? Dobdt { get; set; }
        public string StartdtC { get; set; }
    }
    
    public class NregCpersonSet
    {
        public List<Nreg_CpersonItem> results { get; set; }
    }
    
    public class Nreg_IdItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; } = string.Empty;
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; } = string.Empty;
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; } = string.Empty;
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; } = string.Empty;
        [JsonProperty("sourceIdentifier")]
        public string Srcidentify { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; } = string.Empty;
        [JsonProperty("idType")]
        public string Type { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("validDateFrom")]
        public string ValidDateFrom { get; set; }
        [JsonProperty("institute")]
        public string Institute { get; set; } = string.Empty;
        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;
        [JsonProperty("entryDate")]
        public string EntryDate { get; set; }
        [JsonProperty("validDateTo")]
        public string ValidDateTo { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;
        [JsonProperty("activityNumber")]
        public string Actno { get; set; } = string.Empty;
        [JsonProperty("validDateFromCalendarType")]
        public string ValidDateFromC { get; set; } = string.Empty;
        [JsonProperty("validDateToCalendarType")]
        public string ValidDateToC { get; set; } = string.Empty;
        [JsonProperty("iqamaDescription")]
        public string IqamaDesc { get; set; } = string.Empty;
        [JsonProperty("iqamaFlag")]
        public string IqamaFg { get; set; } = string.Empty;
    }
    
    public class NregIdSet
    {
        public List<Nreg_IdItem> results { get; set; }
    }
    
    public class NregShareholderSet
    {
        public List<object> results { get; set; }
    }
    
    public class OutletItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("cityCode")]
        public string CityCode { get; set; }
        [JsonProperty("cityName")]
        public string City1 { get; set; }
        [JsonProperty("CRLicenseNumber")]
        public string Crlicenceno { get; set; }
        [JsonProperty("outletMasterdata")]
        public string Oldmst { get; set; }
        [JsonProperty("outletDocumentDeregistration")]
        public string Outdocdreg { get; set; }
        [JsonProperty("calendarType")]
        public string Caltp { get; set; }
        [JsonProperty("companyMemorandumAttachment")]
        public string Cmatt { get; set; }
        public string Mandtx { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnumx { get; set; }
        [JsonProperty("rentAttachment")]
        public string Rentatt { get; set; }
        [JsonProperty("contactAttachment")]
        public string Conatt { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrx { get; set; }
        [JsonProperty("language")]
        public string Langx { get; set; }
        [JsonProperty("operation")]
        public string Operationx { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumberx { get; set; }
        [JsonProperty("returnId")]
        public string ReturnIdx { get; set; }
        [JsonProperty("userName")]
        public string Officerx { get; set; }
        [JsonProperty("TIN")]
        public string Gpartx { get; set; }
        public string Mandt { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [JsonProperty("activityNumber")]
        public string Actno { get; set; }
        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("activityCategory")]
        public string Actcat { get; set; }
        [JsonProperty("activityName1")]
        public string Actnm { get; set; }
        [JsonProperty("activityName2")]
        public string Actnm2 { get; set; }
        [JsonProperty("change")]
        public string ChInd { get; set; }
        [JsonProperty("MCIEntry")]
        public string MciEntry { get; set; }
    }
    
    public class Nreg_OutletItem
    {
        [JsonProperty("acitivityName1")]
        public string Actnm { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("acitivityNumber")]
        public string Actno { get; set; }
        [JsonProperty("activityCategory")]
        public string Actcat { get; set; } = "S";
        [JsonProperty("calendarType")]
        public string Caltp { get; set; }
        [JsonProperty("outletMasterData")]
        public string Oldmst { get; set; } = string.Empty;
        [JsonProperty("outletDocumentDeregistration")]
        public string Outdocdreg { get; set; } = string.Empty;
    }
    
    public class NregOutletSet
    {
        public List<Nreg_OutletItem> results { get; set; }
    }
    
    public class Nreg_ActivityItem : INotifyPropertyChanged
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("cityCode")]
        public string CityCode { get; set; } = string.Empty;
        [JsonProperty("industry")]
        public string ActSgrp { get; set; } = string.Empty;
        [JsonProperty("activityStatus")]
        public string Crstat { get; set; } = string.Empty;
        [JsonProperty("mainCR")]
        public string Mncrfg { get; set; } = string.Empty;
        public DateTime? Crexpdt { get; set; }
        public string Hstfg { get; set; } = string.Empty;
        [JsonProperty("serialNumber")]
        public string Srno { get; set; }
        public string Mandt { get; set; } = string.Empty;
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; } = string.Empty;
        [JsonProperty("outletMasterdata")]
        public string Oldmst { get; set; } = string.Empty;
        [JsonProperty("activityDocumentRegistration")]
        public string Actdocdreg { get; set; } = string.Empty;
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; } = string.Empty;
        public string Idnm { get; set; } = string.Empty;
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; } = string.Empty;
        [JsonProperty("activityNumber")]
        public string Actno { get; set; } = string.Empty;
        [JsonProperty("idType")]
        public string Type { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; } = string.Empty;
        [JsonProperty("validDateFrom")]
        public string ValidDateFrom { get; set; }
        [JsonProperty("validDateTo")]
        public string ValidDateTo { get; set; }
        public string ValidDateType { get; set; } = string.Empty;
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;
        [JsonProperty("institute")]
        public string Institute { get; set; } = string.Empty;
        [JsonIgnore]
        public string ArrowImageSource { get; set; } = string.Empty;
        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;
        [JsonProperty("CRCopyTransfer")]
        public string Crclsattfg { get; set; } = string.Empty;
        [JsonProperty("CRAttachment")]
        public string Crattfg { get; set; } = string.Empty;
        [JsonProperty("companyRegistraionAttachment")]
        public string Crtrfattfg { get; set; } = string.Empty;
        [JsonProperty("activity")]
        public string Activity { get; set; } = string.Empty;
        private string _actcat = string.Empty;
        [JsonProperty("activityCategory")]
        public string Actcat
        {
            get => _actcat;
            set
            {
                _actcat = value;
                OnPropertyChanged(nameof(Actcat));
            }
        }
        [JsonProperty("activityGroup")]
        public string ActMgrp { get; set; } = string.Empty;
        [JsonProperty("CrType")]
        public string CrType { get; set; } = string.Empty;
        [JsonProperty("activityName")]
        public string ActName { get; set; } = string.Empty;
        [JsonProperty("MciEntry")]
        public string MciEntry { get; set; } = string.Empty;

        [JsonIgnore]
        public string ActMgrpDesc { get; set; } = string.Empty;
        [JsonIgnore]
        public string ActivityDesc { get; set; } = string.Empty;
        [JsonIgnore]
        public string ActSgrpDesc { get; set; } = string.Empty;
        [JsonIgnore]
        public string IssuedCity { get; set; } = string.Empty;
        [JsonIgnore]
        public string IssuedState { get; set; } = string.Empty;
        [JsonIgnore]
        public string IssuedCountry { get; set; } = string.Empty;
        [JsonIgnore]
        public string IssuedBy { get; set; } = string.Empty;
        [JsonIgnore]
        public string ValidFromUI { get; set; } = string.Empty;


        [JsonIgnore]
        public string CRTypeDesc { get; set; } = string.Empty;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    
    public class NregActivitySet
    {
        public List<Nreg_ActivityItem> results { get; set; } = new List<Nreg_ActivityItem>();
    }
    
    public class Nreg_AddressItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("cityCode")]
        public string CityCode { get; set; }
        [JsonProperty("houseNumber2")]
        public string HouseNum2 { get; set; }
        [JsonProperty("sameAsPhyiscal")]
        public string Sameasphy { get; set; }
        public string Mandt { get; set; } = string.Empty;
        [JsonProperty("buildingCode")]
        public string Building { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; } = string.Empty;
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; } = string.Empty;
        [JsonProperty("floor")]
        public string Floor { get; set; }
        [JsonProperty("district")]
        public string City2 { get; set; }
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; } = string.Empty;
        [JsonProperty("addressType")]
        public string AddrType { get; set; }
        [JsonProperty("sourceIdentifier")]
        public string Srcidentify { get; set; }
        [JsonProperty("startDate")]
        public string Begda { get; set; }
        [JsonProperty("endDate")]
        public string Endda { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; } = string.Empty;
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("houseNumber1")]
        public string HouseNum1 { get; set; }
        [JsonProperty("postalCode")]
        public string PostCode1 { get; set; }
        [JsonProperty("city")]
        public string City1 { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("street2")]
        public string StrSuppl1 { get; set; } = string.Empty;
        [JsonProperty("street3")]
        public string StrSuppl2 { get; set; } = string.Empty;
        [JsonProperty("streetAddressNumber")]
        public string StdAddrnumber { get; set; } = string.Empty;
        [JsonProperty("corporateAddressNumber")]
        public string CorAddrnumber { get; set; } = string.Empty;
    }
    
    public class NregAddressSet
    {
        public List<Nreg_AddressItem> results { get; set; }
    }

    
    public class NregContactSet
    {
        public List<object> results { get; set; }
    }
    
    public class AttDetItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("enableEdit")]
        public bool Enbedit { get; set; }
        [JsonProperty("documentId")]
        public string Doguid { get; set; }
        [JsonProperty("returnGUID")]
        public string RetGuid { get; set; }
        [JsonProperty("enableDelete")]
        public bool Enbdele { get; set; }
        [JsonProperty("sequenceNumber")]
        public string Seqno { get; set; }
        public string SchGuid { get; set; } = string.Empty;
        [JsonProperty("visibleEdit")]
        public bool Visedit { get; set; }
        [JsonProperty("documentCategory")]
        public string Dotyp { get; set; } // Missing
        [JsonProperty("visibleDelete")]
        public bool Visdel { get; set; }
        [JsonProperty("serialNumber")]
        public string Srno { get; set; }
        [JsonProperty("attachedByPerson")]
        public string AttBy { get; set; }
        [JsonProperty("fileName")]
        public string Filename { get; set; }
        [JsonProperty("fileExtension")]
        public string FileExtn { get; set; }
        [JsonProperty("MIMEType")]
        public string Mimetype { get; set; }
        [JsonProperty("portalUser")]
        public string ByPusr { get; set; }
        [JsonProperty("entryDate")]
        public string Erfdt { get; set; }
        [JsonProperty("erftm")]
        public string Erftm { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("documentURL")]
        public string DocUrl { get; set; }
        [JsonProperty("outletReference")]
        public string OutletRef { get; set; }
    }
    
    public class AttDetSet
    {
        public List<AttDetItem> results { get; set; }
    }
    
    public class Nreg_BtnItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string WfReason { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Fbust { get; set; }
        public string Bpkind { get; set; }
        public string Button { get; set; }
    }
    
    public class NregBtnSet
    {
        public List<object> results { get; set; } = new List<object>();
    }

    
    public class NregFormEdit
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Fbust { get; set; }
        public string Bpkind { get; set; }
        public bool EditFg { get; set; }
    }
    
    public class OffNotes
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("notesNumber")]
        public string Notenoz { get; set; } = "1";
        [JsonProperty("refrenceName")]
        public string Refnamez { get; set; } = string.Empty;
        [JsonProperty("dataVersion")]
        public string DataVersionz { get; set; } = string.Empty;//"00000"
        [JsonProperty("xInvoicez")]
        public string XInvoicez { get; set; } = string.Empty;
        public string XObsoletez { get; set; } = string.Empty;
        public string Rcodez { get; set; } = "REJ_NOTES";
        public string Erfusrz { get; set; } = string.Empty;
        public DateTime? Erfdtz { get; set; }
        [JsonProperty("portalUser")]
        public string Erftmz { get; set; }
        [JsonProperty("TIN")]
        public string ByGpartz { get; set; }
        [JsonProperty("attachmentBy")]
        public string AttByz { get; set; } = "TP";
        [JsonProperty("noteNumber")]
        public string Noteno { get; set; } = "1";
        [JsonProperty("lineNumber")]
        public int Lineno { get; set; } = 1;
        [JsonProperty("elementNumber")]
        public int ElemNo { get; set; }
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; } = string.Empty;
        [JsonProperty("notesLine")]
        public string Tdline { get; set; }
    }
    
    public class OffNotesSet
    {
        public List<OffNotes> results { get; set; }
    }


    
    public class NregMSGSet
    {
        public List<object> results { get; set; }
    }

    public class TaxPayerDetails
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("smartRegistration")]
        public string Smartregflg { get; set; }
        [JsonProperty("oldPartnerKind")]
        public string Abpkindold { get; set; }
        [JsonProperty("mainCR")]
        public string Maincr { get; set; }
        [JsonProperty("autoApproval")]
        public string Autoappfg { get; set; }
        [JsonProperty("CRExpiryDate")]
        public string Crexpdt { get; set; }
        [JsonProperty("shareholder")]
        public string Shldfg { get; set; }
        [JsonProperty("lastFilledReturnDate")]
        public string LastFilledRetdt { get; set; }
        [JsonProperty("year")]
        public string Zyear { get; set; }
        [JsonProperty("accountingMethod")]
        public string Accmethod { get; set; }
        [JsonProperty("estimateAccountingMethodCalendar")]
        public string Qsrvfg { get; set; }
        [JsonProperty("oldApplicationNumber")]
        public string Aoldappno { get; set; }
        [JsonProperty("isDraft")]
        public string Draftfg { get; set; }
        [JsonProperty("revenueType")]
        public string Abtyp { get; set; }
        [JsonProperty("shareholderGUID")]
        public string Shguid { get; set; }
        [JsonProperty("updateRequired")]
        public string Aupdaterequired { get; set; }
        [JsonProperty("withholdingContractAcccount")]
        public string Vkontwht { get; set; }
        [JsonProperty("confirm")]
        public string Fdconfirm { get; set; }
        [JsonProperty("withholdingContractObject")]
        public string Vtrefwht { get; set; }
        [JsonProperty("companyBusiness")]
        public string Acomcsbd { get; set; }
        [JsonProperty("companyFinancial")]
        public string Acomcsfd { get; set; }
        [JsonProperty("activity")]
        public string Acsactivitydet { get; set; }
        [JsonProperty("bank")]
        public string Acsbankdet { get; set; }
        [JsonProperty("contactPerson")]
        public string Acscontactper { get; set; }
        [JsonProperty("financial")]
        public string Acsfinancialdet { get; set; }
        [JsonProperty("id")]
        public string Acsiddet { get; set; }
        [JsonProperty("percentage")]
        public string Acsperdet { get; set; }
        [JsonProperty("residenceDetails")]
        public string Acsresidence { get; set; }
        [JsonProperty("companyResidence")]
        public string Acsresidensecomp { get; set; }
        [JsonProperty("share")]
        public string Acsshin { get; set; }
        [JsonProperty("addressAttachment1")]
        public string Addatt1 { get; set; }
        [JsonProperty("addressAttachment2")]
        public string Addatt2 { get; set; }
        [JsonProperty("isReactivationRegistration")]
        public bool Areact { get; set; }
        [JsonProperty("assignedToMe")]
        public string Assignme { get; set; }
        [JsonProperty("type")]
        public string Atype { get; set; }
        [JsonProperty("authorizationGroup")]
        public string Augrp { get; set; }
        [JsonProperty("birthCity")]
        public string Birthcity { get; set; }
        [JsonProperty("birthDate")]
        public string Birthdt { get; set; }
        [JsonProperty("birthDateCalendar")]
        public string Birthdtc { get; set; }
        [JsonProperty("birthLand")]
        public string Birthland { get; set; }
        [JsonProperty("birthRegion")]
        public string Birthregion { get; set; }
        [JsonProperty("partnerKind")]
        public string Bpkind { get; set; }
        [JsonProperty("branch")]
        public string Branchx { get; set; }
        [JsonProperty("calendarType")]
        public string Caltp { get; set; }
        [JsonProperty("capitalAmount")]
        public string Capamt { get; set; }
        [JsonProperty("capitalRegistrationDate")]
        public string Capregdt { get; set; }
        [JsonProperty("check")]
        public string Chkfg { get; set; }
        [JsonProperty("citizen")]
        public string Citizen { get; set; }
        [JsonProperty("toDate")]
        public string Commdt { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("declarationAttachment")]
        public string Decatt { get; set; }
        [JsonProperty("declarationContactNumber")]
        public string Decconno { get; set; }
        [JsonProperty("declarationDate")]
        public string Decdate { get; set; }
        [JsonProperty("declarationDesignation")]
        public string Decdesignation { get; set; }
        [JsonProperty("declaration")]
        public string Decfg { get; set; }
        [JsonProperty("declarationName")]
        public string Decname { get; set; }
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [JsonProperty("familyName")]
        public string FamilyName { get; set; }
        [JsonProperty("fatherName")]
        public string FatherName { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnumx { get; set; }
        [JsonProperty("formBundleStatus")]
        public string Fbsta { get; set; }
        public string Fbstax { get; set; }

        [JsonProperty("formBundleUserStatus")]
        public string Fbustx { get; set; }
        [JsonProperty("financialDetailsCalendar")]
        public string Fdcalender { get; set; }
        [JsonProperty("financialDetailsCalenderOld")]
        public string Fdcalold { get; set; }
        [JsonProperty("financialDetailsDay")]
        public string Fdday { get; set; }
        [JsonProperty("financialDetailsEndDate")]
        public string Fdenddt { get; set; }
        [JsonProperty("financialDetailsMonth")]
        public string Fdmonth { get; set; }
        [JsonProperty("financialDetailsNewTaxDate")]
        public string Fdnewtaxdt { get; set; }
        [JsonProperty("accountingMethodOld")]
        public string Fdtypold { get; set; }
        [JsonProperty("capitalEarn")]
        public string Forcapitalearn { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("partnerShare")]
        public string Forpartnershare { get; set; }
        [JsonProperty("profitShare")]
        public string Forprofitshare { get; set; }
        [JsonProperty("forUser")]
        public string ForUser { get; set; }
        [JsonProperty("forward")]
        public string Forward { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("financialPeriod")]
        public string FinPeriod { get; set; }
        [JsonProperty("fromDate")]
        public string FromDt { get; set; }
        [JsonProperty("governmentCompanyType")]
        public string Govttp { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("grandfatherName")]
        public string GrandfatherName { get; set; }
        [JsonProperty("idAttachment")]
        public string Idatt { get; set; }
        [JsonProperty("initials")]
        public string Initials { get; set; }
        [JsonProperty("language")]
        public string Langx { get; set; }
        [JsonProperty("client")]
        public string Mandt { get; set; }
        [JsonProperty("mobileNumber")]
        public string Mobno { get; set; }
        [JsonProperty("firstName")]
        public string NameFirst { get; set; }
        [JsonProperty("lastName")]
        public string NameLast { get; set; }
        [JsonProperty("organizationName1")]
        public string NameOrg1 { get; set; }
        [JsonProperty("organizationName2")]
        public string NameOrg2 { get; set; }
        [JsonProperty("nationality")]
        public string Natio { get; set; }
        [JsonProperty("nonProfitOrganization")]
        public string Ngotp { get; set; }
        [JsonProperty("userName")]
        public string Officerx { get; set; }
        [JsonProperty("operation")]
        public string Operationx { get; set; }
        [JsonProperty("organizationForResident")]
        public string Orgforresident { get; set; }
        [JsonProperty("organizationForResidentOptions")]
        public string Orgforresidentoptions { get; set; }
        [JsonProperty("organizationNonResident")]
        public string Orgnonresident { get; set; }
        [JsonProperty("organizationNonResidentActivity")]
        public string Orgnonresidentactivity { get; set; }
        [JsonProperty("organizationNonResidentOptions")]
        public string Orgnonresidentoptions { get; set; }
        [JsonProperty("organizationResidence")]
        public string Orgresidence { get; set; }
        [JsonProperty("organizationResidentProfessional")]
        public string Orgresidentprofessional { get; set; }
        [JsonProperty("passwordAttached")]
        public string Passatt { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrx { get; set; }
        [JsonProperty("registrationType")]
        public string Regtype { get; set; }
        [JsonProperty("rentAttachment")]
        public string Rentatt { get; set; }
        [JsonProperty("residence")]
        public string Residence { get; set; }
        [JsonProperty("returnId")]
        public string ReturnIdx { get; set; }
        [JsonProperty("SACaptialEarn")]
        public string Sacapitalearn { get; set; }
        [JsonProperty("SAPartnerShare")]
        public string Sapartnershare { get; set; }
        [JsonProperty("SAProfitShare")]
        public string Saprofitshare { get; set; }
        [JsonProperty("shareholderTIN")]
        public string Shgpartx { get; set; }
        [JsonProperty("sourceIdentifier")]
        public string Srcidentifyx { get; set; }
        [JsonProperty("statusCode")]
        public string Statusx { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumberx { get; set; }
        [JsonProperty("taxpayerDetermination")]
        public string Taxtpdetermination { get; set; }
        [JsonProperty("topAttachment")]
        public string Topmgatt { get; set; }
        [JsonProperty("taxpayerNationality")]
        public string Tpnationality { get; set; }
        [JsonProperty("taxpayerResidence")]
        public string Tpresidence { get; set; }
        [JsonProperty("userType")]
        public string UserTypx { get; set; }
        [JsonProperty("zakatContractAcccount")]
        public string Vkont { get; set; }
        [JsonProperty("zakatContractObject")]
        public string Vtref { get; set; }
        [JsonProperty("female")]
        public string Xsexf { get; set; }
        [JsonProperty("male")]
        public string Xsexm { get; set; }
        [JsonProperty("taxpayerTitle")]
        public string TpTitle { get; set; }
        [JsonProperty("persons")]
        public List<Nreg_CpersonItem> Nreg_CpersonSet { get; set; }
        [JsonProperty("identities")]
        public List<Nreg_IdItem> Nreg_IdSet { get; set; }
        public NregShareholderSet Nreg_ShareholderSet { get; set; }
        [JsonProperty("outlets")]
        public List<Nreg_OutletItem> Nreg_OutletSet { get; set; }
        [JsonProperty("activities")]
        public List<Nreg_ActivityItem> Nreg_ActivitySet { get; set; }
        [JsonProperty("addresses")]
        public List<Nreg_AddressItem> Nreg_AddressSet { get; set; }
        public NregContactSet Nreg_ContactSet { get; set; }
        [JsonProperty("attachments")]
        public List<AttDetItem> AttDetSet { get; set; }
        [JsonProperty("buttons")]
        public List<object> Nreg_BtnSet { get; set; } = new List<object>();
        [JsonProperty("offNotes")]
        public List<OffNotes> off_notesSet { get; set; }
        [JsonProperty("messages")]
        public List<object> Nreg_MSGSet { get; set; }
    }

    
    public class TaxpayerNationality : TaxpayerNationalityLandx50
    {
        public override string ToString() => string.IsNullOrEmpty(Landx50) ? "" : Landx50;
    }
    
    public class TaxpayerNationalityLandx50
    {
        [JsonIgnore]
        //public Metadata __metadata { get; set; }
        [JsonProperty("nationalityCode")]
        public string ANationality { get; set; }

        [JsonProperty("systeCode")]
        public string Mandt { get; set; }

        [JsonProperty("language")]
        public string Spras { get; set; }

        [JsonProperty("countryCode")]
        public string Land1 { get; set; }

        [JsonProperty("countryName")]
        public string Landx { get; set; }

        [JsonProperty("nationalityName")]
        public string Natio { get; set; }

        [JsonProperty("countryDescription")]
        public string Landx50 { get; set; }

        [JsonProperty("nationalityDescription")]
        public string Natio50 { get; set; }

        [JsonProperty("superRegion")]
        public string PrqSpregt { get; set; }
        public override string ToString() =>  string.IsNullOrEmpty(Landx50) ? "" : Landx50;
    }
    
    public class OutletNumber
    {
        [JsonIgnore]
        // public Metadata __metadata { get; set;
        [JsonProperty("fromBundleNumber")]
        public string Fbnum { get; set; }

        [JsonProperty("TIN")]
        public string Gpart { get; set; }

        [JsonProperty("activityNumber")]
        public string Actno { get; set; }
    }
    
    public class ActivityGroupSubGroup
    {
        [JsonIgnore]
        // public Metadata __metadata { get; set; }
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("industrySector")]
        public string IndSector { get; set; }
        [JsonProperty("industryType")]
        public string Istype { get; set; }
        [JsonProperty("industryDescription")]
        public string Text { get; set; }
        [JsonProperty("industryName")]
        public string TextShort { get; set; }

        public override string ToString() => string.IsNullOrEmpty(Text) ? "" : Text;
    }
    
  
    
    public class ActivitySetsList
    {
        [JsonIgnore]
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("industrySector")]
        public string IndSector { get; set; }
        [JsonProperty("industryType")]
        public string Istype { get; set; }
        [JsonProperty("industryDescription")]
        public string Text { get; set; }
        [JsonProperty("industryName")]
        public string TextShort { get; set; }
        [JsonProperty("activityGroups")]
        public List<ActGroupSet> act_groupSet { get; set; }
        [JsonProperty("activity")]
        public List<ActGroupSet> activitySet { get; set; }
        [JsonProperty("activitySubgroups")]
        public List<ActGroupSet> act_subgroupSet { get; set; }
    }

    public class ActGroupSet
    {
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("industrySector")]
        public string IndSector { get; set; }
        [JsonProperty("industryType")]
        public string Istype { get; set; }
        [JsonProperty("industryDescription")]
        public string Text { get; set; }
        [JsonProperty("industryName")]
        public string TextShort { get; set; }

        public override string ToString() => string.IsNullOrEmpty(Text) ? "" : Text;
    }

    public class ActivitySet
    {
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("industrySector")]
        public string IndSector { get; set; }
        [JsonProperty("industryType")]
        public string Istype { get; set; }
        [JsonProperty("industryDescription")]
        public string Text { get; set; }
        [JsonProperty("industryName")]
        public string TextShort { get; set; }

        public override string ToString() => string.IsNullOrEmpty(Text) ? "" : Text;
    }

    public class ActSubgroupSet
    {
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("industrySector")]
        public string IndSector { get; set; }
        [JsonProperty("industryType")]
        public string Istype { get; set; }
        [JsonProperty("industryDescription")]
        public string Text { get; set; }
        [JsonProperty("industryName")]
        public string TextShort { get; set; }

        public override string ToString() => string.IsNullOrEmpty(Text) ? "" : Text;
    }

    
    public class ValidateCR
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("CRNumber")]
        public string Crnum { get; set; }
        [JsonProperty("city")]
        public string CityAry { get; set; }
        public DateTime? Validfrm { get; set; }
        public string CountryAry { get; set; }
        public DateTime? Validto { get; set; }
        [JsonProperty("notFound")]
        public string NotFound { get; set; }
        [JsonProperty("issueDate")]
        public string Issuedt { get; set; }
        public DateTime? Expdt { get; set; }
        [JsonProperty("CRName")]
        public string Crname { get; set; }
        [JsonProperty("exception")]
        public string Excption { get; set; }
        [JsonProperty("telephoneNumber")]
        public string TelephoneNumbery { get; set; }
        [JsonProperty("faxNumber")]
        public string FaxNumbery { get; set; }
        [JsonProperty("email")]
        public string Emaily { get; set; }
        [JsonProperty("telex")]
        public string Telexy { get; set; }
        [JsonProperty("internetAddress")]
        public string InternetAddressy { get; set; }
        [JsonProperty("postalAddress")]
        public string AddressPostaly { get; set; }
        [JsonProperty("addressType")]
        public string Addresstypey { get; set; }
        [JsonProperty("physicalAddress")]
        public string AddressPhysicaly { get; set; }
        public string Z700Crnum { get; set; }
        [JsonProperty("activityCategory")]
        public string Actcat { get; set; }
        [JsonProperty("idType")]
        public string IdType { get; set; }
        [JsonProperty("activity")]
        public string Activity { get; set; }
        [JsonProperty("activitySGroup")]
        public string ActSgrp { get; set; }
        [JsonProperty("activityMGroup")]
        public string ActMgrp { get; set; }
        [JsonProperty("cityCode")]
        public string CityCode { get; set; }
    }
    
    public class CityDropdownItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("language")]
        public string Langu { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("cityCode")]
        public string CityCode { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("cityName")]
        public string CityName { get; set; }
        public override string ToString() => string.IsNullOrEmpty(CityName) ? "" : CityName;
    }

    
    public class CityDropdownSet
    {
        public List<CityDropdownItem> results { get; set; }
    }

    
    public class StateDropdownItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("country")]
        public string Land1 { get; set; }
        [JsonProperty("region")]
        public string Bland { get; set; }
        [JsonProperty("description")]
        public string Bezei { get; set; } //desc
        public override string ToString() =>  string.IsNullOrEmpty(Bezei) ? string.Empty : Bezei;
    }
    
    public class StateDropdownSet
    {
        public List<StateDropdownItem> results { get; set; }
    }
    
    public class CountryDropdownItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        [JsonProperty("country")]
        public string Land1 { get; set; }
        [JsonProperty("countryName")]
        public string Landx { get; set; }
        [JsonProperty("nationality")]
        public string Natio { get; set; }
        [JsonProperty("countryDescription")]
        public string Landx50 { get; set; }
        [JsonProperty("nationalityDescription")]
        public string Natio50 { get; set; }
        [JsonProperty("superRegion")]
        public string PrqSpregt { get; set; }
        public override string ToString() =>   string.IsNullOrEmpty(Landx50) ? "" : Landx50;
    }
    
    public class CountryDropdownSet
    {
        public List<CountryDropdownItem> results { get; set; }
    }
    
    public class OutletDropDowns
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        [JsonProperty("country")]
        public string Land1 { get; set; }
        [JsonProperty("region")]
        public string Bland { get; set; }
        [JsonProperty("cityCode")]
        public string Cityc { get; set; }
        [JsonProperty("cities")]
        public List<CityDropdownItem> city_dropdownSet { get; set; }
        [JsonProperty("states")]
        public List<StateDropdownItem> State_dropdownSet { get; set; }
        [JsonProperty("countries")]
        public List<CountryDropdownItem> country_dropdownSet { get; set; }
    }
    
    public class OutletAddress
    {
        [JsonIgnore]
        // public Metadata __metadata { get; set; }
        [JsonProperty("idType")]
        public string IdType { get; set; }

        [JsonProperty("TIN")]
        public string Tin { get; set; }

        [JsonProperty("taxpayerType")]
        public string TpType { get; set; }

        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNo { get; set; }

        [JsonProperty("additionalNumber")]
        public string AdditionalNo { get; set; }

        [JsonProperty("buildingNumber")]
        public string BuildingNo { get; set; }

        [JsonProperty("zipCode")]
        public string Zipcode { get; set; }

        [JsonProperty("unitNumber")]
        public string UnitNo { get; set; }

        [JsonProperty("districtName")]
        public string DistrictName { get; set; }

        [JsonProperty("streetName")]
        public string StreetName { get; set; }

        [JsonProperty("cityName")]
        public string CityName { get; set; }
    }

    
    public class DeleteOutletRequest
    {
        public string formBundleNumber { get; set; }
        public string activityNumber { get; set; }
        public string portalUser { get; set; }
        public string TIN { get; set; }
    }

    
    public class FinancialDetail
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        [JsonProperty("calendarType")]
        public string ACaltype { get; set; }
        [JsonProperty("month")]
        public string AMonth { get; set; }
        [JsonProperty("fiscalEndDate")]
        public string ACommDate { get; set; }
        public string ADateComm { get; set; }
        [JsonProperty("taxableDate")]
        public string EIsldate { get; set; }
        [JsonProperty("fiscalEndDays")]
        public string EIslmedate { get; set; }
        [JsonProperty("draft")]
        public string Draft { get; set; }
        [JsonProperty("financialPeriods")]
        public List<PeriodSetResult> PeriodSet { get; set; }


    }
    

    public class PeriodSetResult
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("fromDate")]
        public string FromDate { get; set; }
        [JsonProperty("toDate")]
        public string ToDate { get; set; }
        public string ConvretedFromDate { get; set; }
        public string ConvretedToDate { get; set; }
        public string FinPeriodText { get; set; }

        public string _finPeriod = String.Empty;
        [JsonProperty("financialPeriod")]
        public string FinPeriod
        {
            get
            {

                return _finPeriod;
            }
            set
            {
                _finPeriod = value;
                if (FinPeriod == "Normal")
                {
                    FinPeriodText = AppResources.FinacialPeriodNormal;
                }
                else if (FinPeriod == "Short")
                {
                    FinPeriodText = AppResources.FinacialPeriodSmall;
                }
                else if (FinPeriod == "Long")
                {
                    FinPeriodText = AppResources.FinacialPeriodLong;
                }

            }
        }
    }


    

    public class PeriodSet
    {
        public List<PeriodSetResult> results { get; set; }
    }
    
    public class FinancialDetailRequest
    {
        [JsonProperty("calendarType")]
        public string ACaltype { get; set; } = "Hijri";
        [JsonProperty("month")]
        public string AMonth { get; set; } = string.Empty;
        [JsonProperty("fiscalEndDate")]
        public string ADateComm { get; set; }
        [JsonProperty("fiscalEndDays")]
        public string EIslmedate { get; set; } = string.Empty;
    }
    public class FinancialDetailPeriodRequest
    {
        [JsonProperty("calendarType")]
        public string ACaltype { get; set; } = "Hijri";
        [JsonProperty("month")]
        public string AMonth { get; set; } = string.Empty;
        [JsonProperty("fiscalEndDate")]
        public string ADateComm { get; set; }
        [JsonProperty("fiscalEndDays")]
        public string EIslmedate { get; set; } = string.Empty;
        [JsonProperty("TIN")]
        public string Gpart { get; set; } = string.Empty;
        [JsonProperty("financialType")]
        public string Zfintype { get; set; } = string.Empty;
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; } = string.Empty;
        [JsonProperty("financialPeriods")]
        public List<PeriodSetResult> PeriodSet { get; set; }

    }
    public class TaxPayerTypeAvailability : INotifyPropertyChanged
    {
        private bool _reportingBranch;
        public bool ReportingBranch
        {
            get => _reportingBranch;
            set
            {
                _reportingBranch = value;
                OnPropertyChnaged(nameof(ReportingBranch));
            }
        }
        private bool _isReportingBranchVisible;
        public bool IsReportingBranchVisible
        {
            get => _isReportingBranchVisible;
            set
            {
                _isReportingBranchVisible = value;
                OnPropertyChnaged(nameof(IsReportingBranchVisible));
            }
        }
        private bool _entityType;
        public bool EntityType
        {
            get => _entityType;
            set
            {
                _entityType = value;
                OnPropertyChnaged(nameof(EntityType));
            }
        }
        private bool _taxPayerType;
        public bool TaxPayerType
        {
            get => _taxPayerType;
            set
            {
                _taxPayerType = value;
                OnPropertyChnaged(nameof(TaxPayerType));
            }
        }
        private bool _isTaxPayerTypeVisible;
        public bool IsTaxPayerTypeVisible
        {
            get => _isTaxPayerTypeVisible;
            set
            {
                _isTaxPayerTypeVisible = value;
                OnPropertyChnaged(nameof(IsTaxPayerTypeVisible));
            }
        }
        private bool _nationality;
        public bool Nationality
        {
            get => _nationality;
            set
            {
                _nationality = value;
                OnPropertyChnaged(nameof(Nationality));
            }
        }
        private bool _nationalityStatus;
        public bool NationalityStatus
        {
            get => _nationalityStatus;
            set
            {
                _nationalityStatus = value;
                OnPropertyChnaged(nameof(NationalityStatus));
            }
        }
        private bool _isNationalityStatusVisible;
        public bool IsNationalityStatusVisible
        {
            get => _isNationalityStatusVisible;
            set
            {
                _isNationalityStatusVisible = value;
                OnPropertyChnaged(nameof(IsNationalityStatusVisible));
            }
        }
        private bool _residencyStatus;
        public bool ResidencyStatus
        {
            get => _residencyStatus;
            set
            {
                _residencyStatus = value;
                OnPropertyChnaged(nameof(ResidencyStatus));
            }
        }
        private bool _isResidencyStatusVisible;
        public bool IsResidencyStatusVisible
        {
            get => _isResidencyStatusVisible;
            set
            {
                _isResidencyStatusVisible = value;
                OnPropertyChnaged(nameof(IsResidencyStatusVisible));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
    
    public class TaxPayerPersonalDetailsAvailability : INotifyPropertyChanged
    {
        private bool _dob;
        public bool DOB
        {
            get => _dob;
            set
            {
                _dob = value;
                OnPropertyChnaged(nameof(DOB));
            }
        }

        private bool _title;
        public bool Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChnaged(nameof(Title));
            }
        }

        private bool _firstName;
        public bool FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChnaged(nameof(FirstName));
            }
        }
        private bool _lastName;
        public bool LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChnaged(nameof(LastName));
            }
        }
        private bool _fathersName;
        public bool FathersName
        {
            get => _fathersName;
            set
            {
                _fathersName = value;
                OnPropertyChnaged(nameof(FathersName));
            }
        }
        private bool _grandFathersName;
        public bool GrandFathersName
        {
            get => _grandFathersName;
            set
            {
                _grandFathersName = value;
                OnPropertyChnaged(nameof(GrandFathersName));
            }
        }
        private bool _familyName;
        public bool FamilyName
        {
            get => _familyName;
            set
            {
                _familyName = value;
                OnPropertyChnaged(nameof(FamilyName));
            }
        }
        private bool _isFamilyNameVisible;
        public bool IsFamilyNameVisible
        {
            get => _isFamilyNameVisible;
            set
            {
                _isFamilyNameVisible = value;
                OnPropertyChnaged(nameof(IsFamilyNameVisible));
            }
        }
        private bool _initial;
        public bool Initial
        {
            get => _initial;
            set
            {
                _initial = value;
                OnPropertyChnaged(nameof(Initial));
            }
        }
        private bool _isInitialVisible;
        public bool IsInitialVisible
        {
            get => _isInitialVisible;
            set
            {
                _isInitialVisible = value;
                OnPropertyChnaged(nameof(IsInitialVisible));
            }
        }
        private bool _gender;
        public bool Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChnaged(nameof(Gender));
            }
        }
        private bool _isGenderVisible;
        public bool IsGenderVisible
        {
            get => _isGenderVisible;
            set
            {
                _isGenderVisible = value;
                OnPropertyChnaged(nameof(IsGenderVisible));
            }
        }
        private bool _nationality;
        public bool Nationality
        {
            get => _nationality;
            set
            {
                _nationality = value;
                OnPropertyChnaged(nameof(Nationality));
            }
        }
        private bool _isNationalityVisible;
        public bool IsNationalityVisible
        {
            get => _isNationalityVisible;
            set
            {
                _isNationalityVisible = value;
                OnPropertyChnaged(nameof(IsNationalityVisible));
            }
        }
        private bool _citizen;
        public bool Citizen
        {
            get => _citizen;
            set
            {
                _citizen = value;
                OnPropertyChnaged(nameof(Citizen));
            }
        }
        private bool _isCitizenVisible;
        public bool IsCitizenVisible
        {
            get => _isCitizenVisible;
            set
            {
                _isCitizenVisible = value;
                OnPropertyChnaged(nameof(IsCitizenVisible));
            }
        }
        private bool _residence;
        public bool Residence
        {
            get => _residence;
            set
            {
                _residence = value;
                OnPropertyChnaged(nameof(Residence));
            }
        }
        private bool _isResidenceVisible;
        public bool IsResidenceVisible
        {
            get => _isResidenceVisible;
            set
            {
                _isResidenceVisible = value;
                OnPropertyChnaged(nameof(IsResidenceVisible));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
    
    public class PassportDetails : INotifyPropertyChanged
    {
        private bool _passportNo;
        public bool PassportNo
        {
            get => _passportNo;
            set
            {
                _passportNo = value;
                OnPropertyChnaged(nameof(PassportNo));
            }
        }
        private bool _issueCountry;
        public bool IssueCountry
        {
            get => _issueCountry;
            set
            {
                _issueCountry = value;
                OnPropertyChnaged(nameof(IssueCountry));
            }
        }
        private bool _issueDate;
        public bool IssueDate
        {
            get => _issueDate;
            set
            {
                _issueDate = value;
                OnPropertyChnaged(nameof(IssueDate));
            }
        }
        private bool _expiryDate;
        public bool ExpiryDate
        {
            get => _expiryDate;
            set
            {
                _expiryDate = value;
                OnPropertyChnaged(nameof(ExpiryDate));
            }
        }
        private bool _attachment;
        public bool Attachment
        {
            get => _attachment;
            set
            {
                _attachment = value;
                OnPropertyChnaged(nameof(Attachment));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class OutletDetails : INotifyPropertyChanged
    {
        private bool _outletType;
        public bool OutletType
        {
            get => _outletType;
            set
            {
                _outletType = value;
                OnPropertyChnaged(nameof(OutletType));
            }
        }
        private bool _outletName;
        public bool OutletName
        {
            get => _outletName;
            set
            {
                _outletName = value;
                OnPropertyChnaged(nameof(OutletName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    
    public class ActivityDetails : INotifyPropertyChanged
    {
        private bool _issueCountry;
        public bool IssueCountry
        {
            get => _issueCountry;
            set
            {
                _issueCountry = value;
                OnPropertyChnaged(nameof(IssueCountry));
            }
        }
        private bool _issueBy;
        public bool IssueBy
        {
            get => _issueBy;
            set
            {
                _issueBy = value;
                OnPropertyChnaged(nameof(IssueBy));
            }
        }
        private bool _issueCity;
        public bool IssueCity
        {
            get => _issueCity;
            set
            {
                _issueCity = value;
                OnPropertyChnaged(nameof(IssueCity));
            }
        }
        private bool _crno;
        public bool CRNo
        {
            get => _crno;
            set
            {
                _crno = value;
                OnPropertyChnaged(nameof(CRNo));
            }
        }
        private bool _validFrom;
        public bool ValidFrom
        {
            get => _validFrom;
            set
            {
                _validFrom = value;
                OnPropertyChnaged(nameof(ValidFrom));
            }
        }
        private bool _mainActivity;
        public bool MainActivity
        {
            get => _mainActivity;
            set
            {
                _mainActivity = value;
                OnPropertyChnaged(nameof(MainActivity));
            }
        }
        private bool _isMainActivityVisible;
        public bool IsMainActivityVisible
        {
            get => _isMainActivityVisible;
            set
            {
                _isMainActivityVisible = value;
                OnPropertyChnaged(nameof(IsMainActivityVisible));
            }
        }
        private bool _crcopy;
        public bool CRCopy
        {
            get => _crcopy;
            set
            {
                _crcopy = value;
                OnPropertyChnaged(nameof(CRCopy));
            }
        }
        private bool _deleteCRcopy;
        public bool DeleteCRcopy
        {
            get => _deleteCRcopy;
            set
            {
                _deleteCRcopy = value;
                OnPropertyChnaged(nameof(DeleteCRcopy));
            }
        }
        private bool _transferCRCopy;
        public bool TransferCRCopy
        {
            get => _transferCRCopy;
            set
            {
                _transferCRCopy = value;
                OnPropertyChnaged(nameof(TransferCRCopy));
            }
        }
        private bool _deleteTransferCRCopy;
        public bool DeleteTransferCRCopy
        {
            get => _deleteTransferCRCopy;
            set
            {
                _deleteTransferCRCopy = value;
                OnPropertyChnaged(nameof(DeleteTransferCRCopy));
            }
        }
        private bool _isTransferCRCopyVisible;
        public bool IsTransferCRCopyVisible
        {
            get => _isTransferCRCopyVisible;
            set
            {
                _isTransferCRCopyVisible = value;
                OnPropertyChnaged(nameof(IsTransferCRCopyVisible));
            }
        }
        private bool _mainGroup;
        public bool MainGroup
        {
            get => _mainGroup;
            set
            {
                _mainGroup = value;
                OnPropertyChnaged(nameof(MainGroup));
            }
        }
        private bool _subGroup;
        public bool SubGroup
        {
            get => _subGroup;
            set
            {
                _subGroup = value;
                OnPropertyChnaged(nameof(SubGroup));
            }
        }
        private bool _activity;
        public bool Activity
        {
            get => _activity;
            set
            {
                _activity = value;
                OnPropertyChnaged(nameof(Activity));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    
    public class LicenseDetails : INotifyPropertyChanged
    {
        private bool _issueCountry;
        public bool IssueCountry
        {
            get => _issueCountry;
            set
            {
                _issueCountry = value;
                OnPropertyChnaged(nameof(IssueCountry));
            }
        }
        private bool _issueBy;
        public bool IssueBy
        {
            get => _issueBy;
            set
            {
                _issueBy = value;
                OnPropertyChnaged(nameof(IssueBy));
            }
        }
        private bool _issueCity;
        public bool IssueCity
        {
            get => _issueCity;
            set
            {
                _issueCity = value;
                OnPropertyChnaged(nameof(IssueCity));
            }
        }
        private bool _licenseNo;
        public bool LicenseNo
        {
            get => _licenseNo;
            set
            {
                _licenseNo = value;
                OnPropertyChnaged(nameof(LicenseNo));
            }
        }
        private bool _validFrom;
        public bool ValidFrom
        {
            get => _validFrom;
            set
            {
                _validFrom = value;
                OnPropertyChnaged(nameof(ValidFrom));
            }
        }
        private bool _mainActivity;
        public bool MainActivity
        {
            get => _mainActivity;
            set
            {
                _mainActivity = value;
                OnPropertyChnaged(nameof(MainActivity));
            }
        }
        private bool _isMainActivityVisible;
        public bool IsMainActivityVisible
        {
            get => _isMainActivityVisible;
            set
            {
                _isMainActivityVisible = value;
                OnPropertyChnaged(nameof(IsMainActivityVisible));
            }
        }
        private bool _licenseCopy;
        public bool LicenseCopy
        {
            get => _licenseCopy;
            set
            {
                _licenseCopy = value;
                OnPropertyChnaged(nameof(LicenseCopy));
            }
        }
        private bool _deleteLicenseCopy;
        public bool DeleteLicenseCopy
        {
            get => _deleteLicenseCopy;
            set
            {
                _deleteLicenseCopy = value;
                OnPropertyChnaged(nameof(DeleteLicenseCopy));
            }
        }
        private bool _transferLicenseCopy;
        public bool TransferLicenseCopy
        {
            get => _transferLicenseCopy;
            set
            {
                _transferLicenseCopy = value;
                OnPropertyChnaged(nameof(TransferLicenseCopy));
            }
        }
        private bool _mainGroup;
        public bool MainGroup
        {
            get => _mainGroup;
            set
            {
                _mainGroup = value;
                OnPropertyChnaged(nameof(MainGroup));
            }
        }
        private bool _subGroup;
        public bool SubGroup
        {
            get => _subGroup;
            set
            {
                _subGroup = value;
                OnPropertyChnaged(nameof(SubGroup));
            }
        }
        private bool _activity;
        public bool Activity
        {
            get => _activity;
            set
            {
                _activity = value;
                OnPropertyChnaged(nameof(Activity));
            }
        }

        private bool _licenseName;
        public bool LicenseName
        {
            get => _licenseName;
            set
            {
                _licenseName = value;
                OnPropertyChnaged(nameof(LicenseName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    
    public class AddressDetails : INotifyPropertyChanged
    {
        private bool _houseNo;
        public bool HouseNo
        {
            get => _houseNo;
            set
            {
                _houseNo = value;
                OnPropertyChnaged(nameof(HouseNo));
            }
        }
        private bool _buildinNo;
        public bool BuildinNo
        {
            get => _buildinNo;
            set
            {
                _buildinNo = value;
                OnPropertyChnaged(nameof(BuildinNo));
            }
        }
        private bool _floor;
        public bool Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                OnPropertyChnaged(nameof(Floor));
            }
        }
        private bool _street;
        public bool Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChnaged(nameof(Street));
            }
        }
        private bool _quarter;
        public bool Quarter
        {
            get => _quarter;
            set
            {
                _quarter = value;
                OnPropertyChnaged(nameof(Quarter));
            }
        }
        private bool _postelCode;
        public bool PostelCode
        {
            get => _postelCode;
            set
            {
                _postelCode = value;
                OnPropertyChnaged(nameof(PostelCode));
            }
        }
        private bool _addNo;
        public bool AddNo
        {
            get => _addNo;
            set
            {
                _addNo = value;
                OnPropertyChnaged(nameof(AddNo));
            }
        }
        private bool _country;
        public bool Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChnaged(nameof(Country));
            }
        }
        private bool _province;
        public bool Province
        {
            get => _province;
            set
            {
                _province = value;
                OnPropertyChnaged(nameof(Province));
            }
        }
        private bool _city;
        public bool City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChnaged(nameof(City));
            }
        }
        private bool _cbSameAsPhysical;
        public bool CBSameAsPhysical
        {
            get => _cbSameAsPhysical;
            set
            {
                _cbSameAsPhysical = value;
                OnPropertyChnaged(nameof(CBSameAsPhysical));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class UpdateActivityLicenseModel
    {
        public string Taxpayer { get; set; }
        public string Idtype { get; set; }
        public string Idnumber { get; set; }
        public string Activity { get; set; }
        public string MainGrp { get; set; }
        public string SubGrp { get; set; }
        public bool UpdFlg { get; set; }
    }
    public class FinancialDetails : INotifyPropertyChanged
    {
        private bool _financialRecords;
        public bool FinancialRecords
        {
            get => _financialRecords;
            set
            {
                _financialRecords = value;
                OnPropertyChnaged(nameof(FinancialRecords));
            }
        }
        private bool _calendarType;
        public bool CalendarType
        {
            get => _calendarType;
            set
            {
                _calendarType = value;
                OnPropertyChnaged(nameof(CalendarType));
            }
        }
        private bool _fiscalMonthEnd;
        public bool FiscalMonthEnd
        {
            get => _fiscalMonthEnd;
            set
            {
                _fiscalMonthEnd = value;
                OnPropertyChnaged(nameof(FiscalMonthEnd));
            }
        }
        private bool _fiscalDayEnd;
        public bool FiscalDayEnd
        {
            get => _fiscalDayEnd;
            set
            {
                _fiscalDayEnd = value;
                OnPropertyChnaged(nameof(FiscalDayEnd));
            }
        }
        private bool _commencementDate;
        public bool CommencementDate
        {
            get => _commencementDate;
            set
            {
                _commencementDate = value;
                OnPropertyChnaged(nameof(CommencementDate));
            }
        }
        private bool _taxableDate;

        public bool TaxableDate
        {
            get => _taxableDate;
            set
            {
                _taxableDate = value;
                OnPropertyChnaged(nameof(TaxableDate));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}
