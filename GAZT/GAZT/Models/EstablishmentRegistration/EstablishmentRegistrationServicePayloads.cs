using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EGAZT.Models.EstablishmentRegistration
{
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class BranchesDropDownModel
    {
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

    public class Nreg_CpersonItem
    {
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
        public object Enddt { get; set; }
        public string Contacttp { get; set; }
        public string Defaultfg { get; set; }
        public string Outletnm { get; set; }
        public object Startdt { get; set; }
        public string Firstnm { get; set; }
        public string Lastnm { get; set; }
        public string Relationtp { get; set; }
        public string Fathernm { get; set; }
        public string Grandfathernm { get; set; }
        public string Familynm { get; set; }
        public object Dobdt { get; set; }
        public string StartdtC { get; set; }
    }

    public class NregCpersonSet
    {
        public List<Nreg_CpersonItem> results { get; set; }
    }

    public class Nreg_IdItem
    {
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Srcidentify { get; set; }
        public string Gpart { get; set; }
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public DateTime? ValidDateFrom { get; set; }
        public string Institute { get; set; }
        public string City { get; set; }
        public object EntryDate { get; set; }
        public DateTime? ValidDateTo { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Actno { get; set; }
        public string ValidDateFromC { get; set; }
        public string ValidDateToC { get; set; }
    }

    public class NregIdSet
    {
        public List<Nreg_IdItem> results { get; set; }
    }

    public class NregShareholderSet
    {
        public List<object> results { get; set; }
    }
    public class Nreg_OutletItem : INotifyPropertyChanged
    {
        public Metadata __metadata { get; set; }
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
    public class NregOutletSet
    {
        public List<Nreg_OutletItem> results { get; set; }
    }
    public class Nreg_ActivityItem
    {
        public Metadata __metadata { get; set; }
        public string CityCode { get; set; }
        public string ActSgrp { get; set; }
        public string Crstat { get; set; }
        public string Mncrfg { get; set; }
        public object Crexpdt { get; set; }
        public string Hstfg { get; set; }
        public int Srno { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string Oldmst { get; set; }
        public string Actdocdreg { get; set; }
        public string DataVersion { get; set; }
        public string Idnm { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Actno { get; set; }
        public string Type { get; set; }
        public string Idnumber { get; set; }
        public object ValidDateFrom { get; set; }
        public object ValidDateTo { get; set; }
        public string ValidDateType { get; set; }
        public string Country { get; set; }
        public string Institute { get; set; }
        public string City { get; set; }
        public string Crclsattfg { get; set; }
        public string Crattfg { get; set; }
        public string Crtrfattfg { get; set; }
        public string Activity { get; set; }
        public string Actcat { get; set; }
        public string ActMgrp { get; set; }
    }
    public class NregActivitySet
    {
        public List<Nreg_ActivityItem> results { get; set; }
    }
    public class Nreg_AddressItem
    {
        public Metadata __metadata { get; set; }
        public string CityCode { get; set; }
        public string HouseNum2 { get; set; }
        public string Sameasphy { get; set; }
        public string Mandt { get; set; }
        public string Building { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string Floor { get; set; }
        public string City2 { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string AddrType { get; set; }
        public string Srcidentify { get; set; }
        public DateTime Begda { get; set; }
        public DateTime Endda { get; set; }
        public string Gpart { get; set; }
        public string Street { get; set; }
        public string HouseNum1 { get; set; }
        public string PostCode1 { get; set; }
        public string City1 { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string StrSuppl1 { get; set; }
        public string StrSuppl2 { get; set; }
        public string StdAddrnumber { get; set; }
        public string CorAddrnumber { get; set; }
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
        public Metadata4 __metadata { get; set; }
        public bool Enbedit { get; set; }
        public string Doguid { get; set; }
        public string RetGuid { get; set; }
        public bool Enbdele { get; set; }
        public string Seqno { get; set; }
        public object SchGuid { get; set; }
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

    public class AttDetSet
    {
        public List<AttDetItem> results { get; set; }
    }

    public class Nreg_BtnItem
    {
        public Metadata5 __metadata { get; set; }
        public string WfReason { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Fbust { get; set; }
        public string Bpkind { get; set; }
        public string Button { get; set; }
    }

    public class NregBtnSet
    {
        public List<object> results { get; set; }
    }


    public class NregFormEdit
    {
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Fbust { get; set; }
        public string Bpkind { get; set; }
        public bool EditFg { get; set; }
    }

    public class OffNotesSet
    {
        public List<object> results { get; set; }
    }

    public class NregMSGSet
    {
        public List<object> results { get; set; }
    }

    public class TaxPayerDetails
    {
        public Metadata __metadata { get; set; }
        public string Smartregflg { get; set; }
        public string Abpkindold { get; set; }
        public string Maincr { get; set; }
        public string Autoappfg { get; set; }
        public object Crexpdt { get; set; }
        public string Shldfg { get; set; }
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
        public object Capregdt { get; set; }
        public string Chkfg { get; set; }
        public string Citizen { get; set; }
        public DateTime? Commdt { get; set; }
        public string DataVersion { get; set; }
        public string Decatt { get; set; }
        public string Decconno { get; set; }
        public object Decdate { get; set; }
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
        public object Fdenddt { get; set; }
        public string Fdmonth { get; set; }
        public object Fdnewtaxdt { get; set; }
        public string Fdtypold { get; set; }
        public string Forcapitalearn { get; set; }
        public string FormGuid { get; set; }
        public string Forpartnershare { get; set; }
        public string Forprofitshare { get; set; }
        public string ForUser { get; set; }
        public string Forward { get; set; }
        public string Forwardx { get; set; }
        public string FullName { get; set; }
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
        public NregBtnSet Nreg_BtnSet { get; set; }
        public NregFormEdit Nreg_FormEdit { get; set; }
        public OffNotesSet off_notesSet { get; set; }
        public NregMSGSet Nreg_MSGSet { get; set; }
    }


    public class TaxpayerNationality
    {
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
        public override string ToString() => Natio50;
    }

    public class OutletNumber
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string Gpart { get; set; }
        public string Actno { get; set; }
    }
    public class ActivityGroupSubGroup
    {
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string IndSector { get; set; }
        public string Istype { get; set; }
        public string Text { get; set; }
        public string TextShort { get; set; }
    }
    public class ActSubgroupSet
    {
        public List<ActivityGroupSubGroup> results { get; set; }
    }

    public class ActivitySet
    {
        public List<ActivityGroupSubGroup> results { get; set; }
    }
    public class ActGroupSet
    {
        public List<ActivityGroupSubGroup> results { get; set; }
    }
    public class ActivitySetsList
    {
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

    public class ValidateCR
    {
        public Metadata __metadata { get; set; }
        public string Crnum { get; set; }
        public string CityAry { get; set; }
        public object Validfrm { get; set; }
        public string CountryAry { get; set; }
        public object Validto { get; set; }
        public string NotFound { get; set; }
        public DateTime? Issuedt { get; set; }
        public object Expdt { get; set; }
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
    }

    public class CityDropdownItem
    {
        public Metadata __metadata { get; set; }
        public string Langu { get; set; }
        public string Country { get; set; }
        public string CityCode { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
    }

    public class CityDropdownSet
    {
        public List<CityDropdownItem> results { get; set; }
    }

    public class StateDropdownItem
    {
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Bland { get; set; }
        public string Bezei { get; set; }
    }

    public class StateDropdownSet
    {
        public List<StateDropdownItem> results { get; set; }
    }

    public class CountryDropdownItem
    {
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Landx { get; set; }
        public string Natio { get; set; }
        public string Landx50 { get; set; }
        public string Natio50 { get; set; }
        public string PrqSpregt { get; set; }
    }

    public class CountryDropdownSet
    {
        public List<CountryDropdownItem> results { get; set; }
    }
    public class OutletDropDowns
    {
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Bland { get; set; }
        public string Cityc { get; set; }
        public CityDropdownSet city_dropdownSet { get; set; }
        public StateDropdownSet State_dropdownSet { get; set; }
        public CountryDropdownSet country_dropdownSet { get; set; }
    }
}
