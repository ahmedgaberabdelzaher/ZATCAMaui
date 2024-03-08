using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.EstablishmentRegistration
{
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public class HTTPBadRequestException : GAZTException
    {
        public HTTPBadRequestException(string expception) : base(expception) { }
    }
    [Preserve(AllMembers = true)]
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class BranchesDropDownModel
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Auobj { get; set; }
        public string Augrp { get; set; }
        public string Bez50 { get; set; }
        public override string ToString()
        {
            return Bez50;
        }
    }
    [Preserve(AllMembers = true)]
    public class Nreg_CpersonItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public bool Cpoldfg { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public bool Gmatt { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Srcidentify { get; set; }
        public string Gpart { get; set; }
        public DateTime? Enddt { get; set; }
        public string Contacttp { get; set; }
        public string Defaultfg { get; set; }
        public string Outletnm { get; set; }
        public DateTime? Startdt { get; set; }
        public string Firstnm { get; set; }
        public string Lastnm { get; set; }
        public string Relationtp { get; set; }
        public string Fathernm { get; set; }
        public string Grandfathernm { get; set; }
        public string Familynm { get; set; }
        public DateTime? Dobdt { get; set; }
        public string StartdtC { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class NregCpersonSet
    {
        public List<Nreg_CpersonItem> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Nreg_IdItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; } = string.Empty;
        public string FormGuid { get; set; } = string.Empty;
        public string DataVersion { get; set; } = string.Empty;
        public int LineNo { get; set; }
        public string RankingOrder { get; set; } = string.Empty;
        public string Srcidentify { get; set; }
        public string Gpart { get; set; } = string.Empty;
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public DateTime? ValidDateFrom { get; set; }
        public string Institute { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime? EntryDate { get; set; }
        public DateTime? ValidDateTo { get; set; }
        public string Country { get; set; }
        public string Region { get; set; } = string.Empty;
        public string Actno { get; set; } = string.Empty;
        public string ValidDateFromC { get; set; } = string.Empty;
        public string ValidDateToC { get; set; } = string.Empty;
        //public string IqamaDesc { get; set; } = string.Empty;
        //public string IqamaFg { get; set; } = string.Empty;
    }
    [Preserve(AllMembers = true)]
    public class NregIdSet
    {
        public List<Nreg_IdItem> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class NregShareholderSet
    {
        public List<object> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class OutletItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string MciEntry { get; set; }
        public string CityCode { get; set; }
        public string City1 { get; set; }
        public string Crlicenceno { get; set; }
        public string Oldmst { get; set; }
        public string Outdocdreg { get; set; }
        public string Caltp { get; set; }
        public string Cmatt { get; set; }
        public string Mandtx { get; set; }
        public string Fbnumx { get; set; }
        public string Rentatt { get; set; }
        public string Conatt { get; set; }
        public string PortalUsrx { get; set; }
        public string Langx { get; set; }
        public string Operationx { get; set; }
        public string StepNumberx { get; set; }
        public string ReturnIdx { get; set; }
        public string Officerx { get; set; }
        public string Gpartx { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Actno { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Actcat { get; set; }
        public string Actnm { get; set; }
        public string Actnm2 { get; set; }
        public string ChInd { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Nreg_OutletItem
    {
        public string Actnm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Actno { get; set; }
        public string Actcat { get; set; } = "S";
        public string Caltp { get; set; }
        public string Oldmst { get; set; } = string.Empty;
        public string Outdocdreg { get; set; } = string.Empty;
    }
    [Preserve(AllMembers = true)]
    public class NregOutletSet
    {
        public List<Nreg_OutletItem> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Nreg_ActivityItem : INotifyPropertyChanged
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string CityCode { get; set; } = string.Empty;
        public string CrType { get; set; } = string.Empty;
        public string ActName { get; set; } = string.Empty;
        public string MciEntry { get; set; } = string.Empty;
        public string ActSgrp { get; set; } = string.Empty;
        public string Crstat { get; set; } = string.Empty;
        public string Mncrfg { get; set; } = string.Empty;
        public DateTime? Crexpdt { get; set; }
        public string Hstfg { get; set; } = string.Empty;
        public int Srno { get; set; }
        public string Mandt { get; set; } = string.Empty;
        public string FormGuid { get; set; } = string.Empty;
        public string Oldmst { get; set; } = string.Empty;
        public string Actdocdreg { get; set; } = string.Empty;
        public string DataVersion { get; set; } = string.Empty;
        public string Idnm { get; set; } = string.Empty;
        public int LineNo { get; set; }
        public string RankingOrder { get; set; } = string.Empty;
        public string Actno { get; set; } = string.Empty;
        public string Type { get; set; }
        public string Idnumber { get; set; } = string.Empty;
        public DateTime? ValidDateFrom { get; set; }
        public DateTime? ValidDateTo { get; set; }
        public string ValidDateType { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Institute { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Crclsattfg { get; set; } = string.Empty;
        public string Crattfg { get; set; } = string.Empty;
        public string Crtrfattfg { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        private string _actcat = string.Empty;
        public string Actcat
        {
            get => _actcat;
            set
            {
                _actcat = value;
                OnPropertyChanged(nameof(Actcat));
            }
        }
        public string ActMgrp { get; set; } = string.Empty;
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
    [Preserve(AllMembers = true)]
    public class NregActivitySet
    {
        public List<Nreg_ActivityItem> results { get; set; } = new List<Nreg_ActivityItem>();
    }
    [Preserve(AllMembers = true)]
    public class Nreg_AddressItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string CityCode { get; set; }
        public string HouseNum2 { get; set; }
        public string Sameasphy { get; set; }
        public string Mandt { get; set; } = string.Empty;
        public string Building { get; set; }
        public string FormGuid { get; set; } = string.Empty;
        public string DataVersion { get; set; } = string.Empty;
        public string Floor { get; set; }
        public string City2 { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; } = string.Empty;
        public string AddrType { get; set; }
        public string Srcidentify { get; set; }
        public DateTime? Begda { get; set; }
        public DateTime? Endda { get; set; }
        public string Gpart { get; set; } = string.Empty;
        public string Street { get; set; }
        public string HouseNum1 { get; set; }
        public string PostCode1 { get; set; }
        public string City1 { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string StrSuppl1 { get; set; } = string.Empty;
        public string StrSuppl2 { get; set; } = string.Empty;
        public string StdAddrnumber { get; set; } = string.Empty;
        public string CorAddrnumber { get; set; } = string.Empty;
    }
    [Preserve(AllMembers = true)]
    public class NregAddressSet
    {
        public List<Nreg_AddressItem> results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class NregContactSet
    {
        public List<object> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class AttDetItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public bool Enbedit { get; set; }
        public string Doguid { get; set; }
        public string RetGuid { get; set; }
        public bool Enbdele { get; set; }
        public string Seqno { get; set; }
        public string SchGuid { get; set; } = string.Empty;
        public bool Visedit { get; set; }
        public string Dotyp { get; set; }
        public bool Visdel { get; set; }
        public int Srno { get; set; }
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public DateTime? Erfdt { get; set; }
        public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class AttDetSet
    {
        public List<AttDetItem> results { get; set; }
    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public class NregBtnSet
    {
        public List<object> results { get; set; } = new List<object>();
    }

    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public class OffNotes
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Notenoz { get; set; } = "1";
        public string Refnamez { get; set; } = string.Empty;
        public string DataVersionz { get; set; } = string.Empty;//"00000"
        public string XInvoicez { get; set; } = string.Empty;
        public string XObsoletez { get; set; } = string.Empty;
        public string Rcodez { get; set; } = "REJ_NOTES";
        public string Erfusrz { get; set; } = string.Empty;
        public DateTime? Erfdtz { get; set; }
        public string Erftmz { get; set; }

        public string ByGpartz { get; set; }
        public string AttByz { get; set; } = "TP";
        public string Noteno { get; set; } = "1";
        public int Lineno { get; set; } = 1;
        public int ElemNo { get; set; }
        public string Tdformat { get; set; } = string.Empty;
        public string Tdline { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class OffNotesSet
    {
        public List<OffNotes> results { get; set; }
    }


    [Preserve(AllMembers = true)]
    public class NregMSGSet
    {
        public List<object> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class TaxPayerDetails
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Smartregflg { get; set; }
        public string Abpkindold { get; set; }
        public string Maincr { get; set; }
        public string Autoappfg { get; set; }
        public DateTime? Crexpdt { get; set; }
        public string Shldfg { get; set; }
        public DateTime? LastFilledRetdt { get; set; }
        public string Zyear { get; set; }
        public string Accmethod { get; set; }
        public string Qsrvfg { get; set; }
        public string Aoldappno { get; set; }
        public string Draftfg { get; set; }
        public string Abtyp { get; set; }
        public string Shguid { get; set; }
        public string Aupdaterequired { get; set; }
        public string Vkontwht { get; set; }
        public string Fdconfirm { get; set; }
        public string Vtrefwht { get; set; }
        public string Acomcsbd { get; set; }
        public string Acomcsfd { get; set; }
        public string Acsactivitydet { get; set; }
        public string Acsbankdet { get; set; }
        public string Acscontactper { get; set; }
        public string Acsfinancialdet { get; set; }
        public string Acsiddet { get; set; }
        public string Acsperdet { get; set; }
        public string Acsresidence { get; set; }
        public string Acsresidensecomp { get; set; }
        public string Acsshin { get; set; }
        public string Addatt1 { get; set; }
        public string Addatt2 { get; set; }
        public bool Areact { get; set; }
        public string Assignme { get; set; }
        public string Atype { get; set; }
        public string Augrp { get; set; }
        public string Birthcity { get; set; }
        public DateTime? Birthdt { get; set; }
        public string Birthdtc { get; set; }
        public string Birthland { get; set; }
        public string Birthregion { get; set; }
        public string Bpkind { get; set; }
        public string Branchx { get; set; }
        public string Caltp { get; set; }
        public string Capamt { get; set; }
        public DateTime? Capregdt { get; set; }
        public string Chkfg { get; set; }
        public string Citizen { get; set; }
        public DateTime? Commdt { get; set; }
        public string DataVersion { get; set; }
        public string Decatt { get; set; }
        public string Decconno { get; set; }
        public DateTime? Decdate { get; set; }
        public string Decdesignation { get; set; }
        public string Decfg { get; set; }
        public string Decname { get; set; }
        public string Euser { get; set; }
        public string FamilyName { get; set; }
        public string FatherName { get; set; }
        public string Fbguid { get; set; }
        public string Fbnumx { get; set; }
        public string Fbsta { get; set; }
        public string Fbstax { get; set; }
        public string Fbust { get; set; }
        public string Fbustx { get; set; }
        public string Fdcalender { get; set; }
        public string Fdcalold { get; set; }
        public string Fdday { get; set; }
        public DateTime? Fdenddt { get; set; }
        public string Fdmonth { get; set; }
        public DateTime? Fdnewtaxdt { get; set; }
        public string Fdtypold { get; set; }
        public string Forcapitalearn { get; set; }
        public string FormGuid { get; set; }
        public string Forpartnershare { get; set; }
        public string Forprofitshare { get; set; }
        public string ForUser { get; set; }
        public string Forward { get; set; }
        public string Forwardx { get; set; }
        public string FullName { get; set; }
        public string FinPeriod { get; set; }
        public DateTime? FromDt { get; set; }
        public string Govttp { get; set; }
        public string Gpart { get; set; }
        public string Gpartx { get; set; }
        public string GrandfatherName { get; set; }
        public string Idatt { get; set; }
        public string Initials { get; set; }
        public string Langx { get; set; }
        public string Mandt { get; set; }
        public string Mandtx { get; set; }
        public string Mobno { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string NameOrg1 { get; set; }
        public string NameOrg2 { get; set; }
        public string Natio { get; set; }
        public string Ngotp { get; set; }
        public string Officerx { get; set; }
        public string Operationx { get; set; }
        public string Orgforresident { get; set; }
        public string Orgforresidentoptions { get; set; }
        public string Orgnonresident { get; set; }
        public string Orgnonresidentactivity { get; set; }
        public string Orgnonresidentoptions { get; set; }
        public string Orgresidence { get; set; }
        public string Orgresidentprofessional { get; set; }
        public string Passatt { get; set; }
        public string PortalUsrx { get; set; }
        public string Regtype { get; set; }
        public string Rentatt { get; set; }
        public string Residence { get; set; }
        public string ReturnIdx { get; set; }
        public string Sacapitalearn { get; set; }
        public string Sapartnershare { get; set; }
        public string Saprofitshare { get; set; }
        public string Shgpartx { get; set; }
        public string Srcidentifyx { get; set; }
        public string Statusx { get; set; }
        public string StepNumberx { get; set; }
        public string Taxtpdetermination { get; set; }
        public string Topmgatt { get; set; }
        public string Tpnationality { get; set; }
        public string Tpresidence { get; set; }
        public string TpTitle { get; set; } = string.Empty;
        public string UserTypx { get; set; }
        public string Vkont { get; set; }
        public string Vtref { get; set; }
        public string Xsexf { get; set; }
        public string Xsexm { get; set; }
        public NregCpersonSet Nreg_CpersonSet { get; set; }
        public NregIdSet Nreg_IdSet { get; set; }
        public NregShareholderSet Nreg_ShareholderSet { get; set; }
        public NregOutletSet Nreg_OutletSet { get; set; }
        public NregActivitySet Nreg_ActivitySet { get; set; }
        public NregAddressSet Nreg_AddressSet { get; set; }
        public NregContactSet Nreg_ContactSet { get; set; }
        public AttDetSet AttDetSet { get; set; }
        public NregBtnSet Nreg_BtnSet { get; set; } = new NregBtnSet();

        public OffNotesSet off_notesSet { get; set; }
        public NregMSGSet Nreg_MSGSet { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class TaxpayerNationality : TaxpayerNationalityLandx50
    {
        public override string ToString() => Landx50;
    }
    [Preserve(AllMembers = true)]
    public class TaxpayerNationalityLandx50
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string ANationality { get; set; }
        public string Mandt { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Landx { get; set; }
        public string Natio { get; set; }
        public string Landx50 { get; set; }
        public string Natio50 { get; set; }
        public string PrqSpregt { get; set; }
        public override string ToString() => Landx50;
    }
    [Preserve(AllMembers = true)]
    public class OutletNumber
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string Gpart { get; set; }
        public string Actno { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ActivityGroupSubGroup
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string IndSector { get; set; }
        public string Istype { get; set; }
        public string Text { get; set; }
        public string TextShort { get; set; }
        public override string ToString() => Text;
    }
    [Preserve(AllMembers = true)]
    public class ActSubgroupSet
    {
        public List<ActivityGroupSubGroup> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ActivitySet
    {
        public List<ActivityGroupSubGroup> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ActGroupSet
    {
        public List<ActivityGroupSubGroup> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ActivitySetsList
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string IndSector { get; set; }
        public string Istype { get; set; }
        public string Text { get; set; }
        public string TextShort { get; set; }
        public ActGroupSet act_groupSet { get; set; }
        public ActivitySet activitySet { get; set; }
        public ActSubgroupSet act_subgroupSet { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ValidateCR
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Crnum { get; set; }
        public string CityAry { get; set; }
        public DateTime? Validfrm { get; set; }
        public string CountryAry { get; set; }
        public DateTime? Validto { get; set; }
        public string NotFound { get; set; }
        public DateTime? Issuedt { get; set; }
        public DateTime? Expdt { get; set; }
        public string Crname { get; set; }
        public string Excption { get; set; }
        public string TelephoneNumbery { get; set; }
        public string FaxNumbery { get; set; }
        public string Emaily { get; set; }
        public string Telexy { get; set; }
        public string InternetAddressy { get; set; }
        public string AddressPostaly { get; set; }
        public string Addresstypey { get; set; }
        public string AddressPhysicaly { get; set; }
        public string Z700Crnum { get; set; }

        public string Actcat { get; set; }
        public string IdType { get; set; }

        public string Activity { get; set; }
        public string ActSgrp { get; set; }
        public string ActMgrp { get; set; }

        public string CityCode { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class CityDropdownItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Langu { get; set; }
        public string Country { get; set; }
        public string CityCode { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
        public override string ToString() => CityName;
    }

    [Preserve(AllMembers = true)]
    public class CityDropdownSet
    {
        public List<CityDropdownItem> results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class StateDropdownItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Bland { get; set; }
        public string Bezei { get; set; }
        public override string ToString() => Bezei;
    }
    [Preserve(AllMembers = true)]
    public class StateDropdownSet
    {
        public List<StateDropdownItem> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CountryDropdownItem
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Landx { get; set; }
        public string Natio { get; set; }
        public string Landx50 { get; set; }
        public string Natio50 { get; set; }
        public string PrqSpregt { get; set; }
        public override string ToString() => Landx50;
    }
    [Preserve(AllMembers = true)]
    public class CountryDropdownSet
    {
        public List<CountryDropdownItem> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class OutletDropDowns
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Bland { get; set; }
        public string Cityc { get; set; }
        public CityDropdownSet city_dropdownSet { get; set; }
        public StateDropdownSet State_dropdownSet { get; set; }
        public CountryDropdownSet country_dropdownSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class OutletAddress
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string IdType { get; set; }
        public string Tin { get; set; }
        public string TpType { get; set; }
        public string IdNumber { get; set; }
        public string MobileNo { get; set; }
        public string AdditionalNo { get; set; }
        public string BuildingNo { get; set; }
        public string Zipcode { get; set; }
        public string UnitNo { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
        public string CityName { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class FinancialDetail
    {
        [JsonIgnore]
        public Metadata __metadata { get; set; }
        public string ACaltype { get; set; }
        public string AMonth { get; set; }
        public string ACommDate { get; set; }
        public DateTime? ADateComm { get; set; }
        public string IDatfm { get; set; }
        public string IGregdate { get; set; }
        public string EIsldate { get; set; }
        public string EIslmedate { get; set; }
        public string Draft { get; set; }
        public PeriodSet PeriodSet { get; set; }


    }
    [Preserve(AllMembers = true)]

    public class PeriodSetResult
    {
        public Metadata __metadata { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ConvretedFromDate { get; set; }
        public string ConvretedToDate { get; set; }
        public string FinPeriodText { get; set; }

        public string _finPeriod = String.Empty;
        public string FinPeriod
        {
            get
            {

                return _finPeriod;
            }
            set
            {
                _finPeriod = value;
                if (FinPeriod == "N")
                {
                    FinPeriodText = AppResources.FinacialPeriodNormal;
                }
                else if (FinPeriod == "S")
                {
                    FinPeriodText = AppResources.FinacialPeriodSmall;
                }
                else if (FinPeriod == "L")
                {
                    FinPeriodText = AppResources.FinacialPeriodLong;
                }

            }
        }
    }


    [Preserve(AllMembers = true)]

    public class PeriodSet
    {
        public List<PeriodSetResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class FinancialDetailRequest
    {
        public string ACaltype { get; set; } = "H";
        public string AMonth { get; set; } = string.Empty;
        public DateTime? ADateComm { get; set; }
        public string EIslmedate { get; set; } = string.Empty;
    }
    [Preserve(AllMembers = true)]
    public class FinancialDetailPeriodRequest
    {
        public string ACaltype { get; set; } = "H";
        public string AMonth { get; set; } = string.Empty;
        public DateTime? ADateComm { get; set; }
        public string EIslmedate { get; set; } = string.Empty;
        public string Gpart { get; set; } = string.Empty;
        public string Zfintype { get; set; } = string.Empty;
        public string Fbnum { get; set; } = string.Empty;
        public List<PeriodSetResult> PeriodSet { get; set; }

    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]

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
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
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

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChnaged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
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


    [Preserve(AllMembers = true)]
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
