using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.VATInstalmentModels
{[Preserve(AllMembers = true)]
    public class VATInstalmentPlanListModel
    {
        public VATInstalmentPlanListModel()
        {
        }
    }


    [Preserve(AllMembers = true)]
    public class RequestToVATInstallmentPlan
    {
        public class __metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }

        }

        public class Results
        {
            public __metadata __metadata { get; set; }
            public string FbtText { get; set; }
            public string Fbtyp { get; set; }
            public string UserErrFg { get; set; }
            public string SysFlg { get; set; }
            public string Ldate { get; set; }

        }
        public class REQTYPSet
        {
            public IList<Results> results { get; set; }

        }

        public class STATUSSetResults
        {
            public __metadata __metadata { get; set; }
            public string Mandt { get; set; }
            public string Stsma { get; set; }
            public string Estat { get; set; }
            public string Spras { get; set; }
            public string Txt04 { get; set; }
            public string Txt30 { get; set; }
            public bool Ltext { get; set; }

        }
        public class STATUSSet
        {
            public List<STATUSSetResults> results { get; set; }

        }

        public class ASSLISTSetResults
        {
            public __metadata __metadata { get; set; }
            public string Selector { get; set; }
            public string Fbnum { get; set; }
            public string Fbtyp { get; set; }
            public string Gpart { get; set; }
            public string NameLast { get; set; }
            public string NameFirst { get; set; }
            public string NameOrg1 { get; set; }
            public string NameOrg2 { get; set; }
            public string FullNm { get; set; }
            public string GpartFullNm { get; set; }
            public string WfSub { get; set; }
            public string Fbust { get; set; }
            public string FbustTxt { get; set; }
            public string Receipt { get; set; }
            public string AssignUsr { get; set; }
            public string LoginUsr { get; set; }
            public string AssignMe { get; set; }
            public string NewUser { get; set; }
            public string TileInd { get; set; }
            public string FbtText { get; set; }
            public string TransactionType { get; set; }
            public string WiId { get; set; }
            public string WiPrio { get; set; }
            public string WiPrioDesc { get; set; }

        }
        public class ASSLISTSet
        {
            public List<ASSLISTSetResults> results { get; set; }

        }
        public class D
        {
            public __metadata __metadata { get; set; }
            public string UserTin { get; set; }
            public string AudTin { get; set; }
            public object Begda { get; set; }
            public string TaxType { get; set; }
            public object Endda { get; set; }
            public string Euser { get; set; }
            public string Fbnum { get; set; }
            public string Fbsta { get; set; }
            public string Fbtyp { get; set; }
            public string Fbust { get; set; }
            public string Formproc { get; set; }
            public string Gpart { get; set; }
            public string Lang { get; set; }
            public string Mandt { get; set; }
            public string Officer { get; set; }
            public string Operation { get; set; }
            public string Persl { get; set; }
            public string PortalUsr { get; set; }
            public string ReturnId { get; set; }
            public string Status { get; set; }
            public string StepNumber { get; set; }
            public string TransactionType { get; set; }
            public string TxnTp { get; set; }
            public string UserTyp { get; set; }
            public REQTYPSet REQTYPSet { get; set; }
            public STATUSSet STATUSSet { get; set; }
            public ASSLISTSet ASSLISTSet { get; set; }

        }
        public class Application
        {
            public D d { get; set; }

        }

    }

    [Preserve(AllMembers = true)]
    public class RequestToVATInstallmentPlanDetails
    {
        public D d { get; set; }


        public class __metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }

        }

        public class VTADSetResults
        {
            public __metadata __metadata { get; set; }
            public string Mandt { get; set; }
            public string FormGuid { get; set; }
            public string DataVersion { get; set; }
            public int LineNo { get; set; }
            public string RankingOrder { get; set; }
            public string AddrType { get; set; }
            public string Srcidentify { get; set; }
            public string Addrnumber { get; set; }
            public string City { get; set; }
            public string Quarter { get; set; }
            public string PostalCd { get; set; }
            public string Street { get; set; }
            public string AdditionalNo { get; set; }
            public string BuildingNo { get; set; }
            public string Region { get; set; }
            public string Longitude { get; set; }
            public string Latitude { get; set; }
            public string LenSiz { get; set; }
            public string WidSiz { get; set; }
            public string HeiSiz { get; set; }
            public string SizUn { get; set; }
            public string CubicSc { get; set; }
            public string CubicScUn { get; set; }
            public string SquarSc { get; set; }
            public string SquarScUn { get; set; }
            public string LongitudeC { get; set; }
            public string LatitudeC { get; set; }
            public string RegionDesc { get; set; }
            public string ReturnId { get; set; }

        }
        public class VTADSet
        {
            public IList<VTADSetResults> results { get; set; }

        }

        public class NOTESSetResults
        {
            public __metadata __metadata { get; set; }
            public string Notenoz { get; set; }
            public string Refnamez { get; set; }
            public string XInvoicez { get; set; }
            public string XObsoletez { get; set; }
            public string Rcodez { get; set; }
            public string Erfusrz { get; set; }
            public string Erfdtz { get; set; }
            public string Erftmz { get; set; }
            public string AttByz { get; set; }
            public string ByPusrz { get; set; }
            public string ByGpartz { get; set; }
            public string DataVersionz { get; set; }
            public string Namez { get; set; }
            public string Noteno { get; set; }
            public int Lineno { get; set; }
            public int ElemNo { get; set; }
            public string Tdformat { get; set; }
            public string Tdline { get; set; }
            public string Sect { get; set; }
            public string Strdt { get; set; }
            public string Strtime { get; set; }
            public string Strline { get; set; }

        }
        public class VATNOTESSet
        {
            public IList<NOTESSetResults> results { get; set; }

        }

        public class VTISSetResults
        {
            public __metadata __metadata { get; set; }
            public string Mandt { get; set; }
            public string FormGuid { get; set; }
            public string DataVersion { get; set; }
            public int LineNo { get; set; }
            public string RankingOrder { get; set; }
            public string Faedn { get; set; }
            public string Monat { get; set; }
            public string Betrw { get; set; }
            public string Totpaidamt { get; set; }
            public string Totremainamt { get; set; }
            public string Waers { get; set; }
            public string ReturnId { get; set; }

        }
        public class VTISSet
        {
            public IList<VTISSetResults> results { get; set; }

        }

        public class VTIASetResults
        {
            public __metadata __metadata { get; set; }
            public string Mandt { get; set; }
            public string SadadNo { get; set; }
            public string Xsele { get; set; }
            public string FormGuid { get; set; }
            public string DataVersion { get; set; }
            public int LineNo { get; set; }
            public string RankingOrder { get; set; }
            public string Vtre2 { get; set; }
            public string Abrzu { get; set; }
            public string Abrzo { get; set; }
            public string Betrh { get; set; }
            public string Waers { get; set; }
            public string Taxperioddsc { get; set; }
            public string ReturnId { get; set; }

        }
        public class VTIASet
        {
            public IList<VTIASetResults> results { get; set; }

        }

        public class ATTACHMENTSetResults
        {
            public __metadata __metadata { get; set; }
            public string AttBy { get; set; }
            public string ByPusr { get; set; }
            public string DataVersion { get; set; }
            public string DocUrl { get; set; }
            public string Doguid { get; set; }
            public string Dotyp { get; set; }
            public string Erfdt { get; set; }
            public string Erftm { get; set; }
            public string FileExtn { get; set; }
            public string Filename { get; set; }
            public string Mimetype { get; set; }
            public string OutletRef { get; set; }
            public string RetGuid { get; set; }
            public string SchGuid { get; set; }
            public string Seqno { get; set; }
            public int Srno { get; set; }

        }
        public class ATTACHMENTSet
        {
            public IList<ATTACHMENTSetResults> results { get; set; }

        }
        public class D
        {
            public __metadata __metadata { get; set; }
            public bool Appchkbox { get; set; }
            public string EvStatus { get; set; }
            public string Officer { get; set; }
            public string TotInvAmt { get; set; }
            public string Xstep1Conf { get; set; }
            public object Begdaz { get; set; }
            public string Xstep2Conf { get; set; }
            public string Betrw { get; set; }
            public string Decflg { get; set; }
            public string DataVersion { get; set; }
            public object Enddaz { get; set; }
            public string Euser { get; set; }
            public string Fbnumz { get; set; }
            public string FormGuid { get; set; }
            public string Formprocz { get; set; }
            public string Gpartz { get; set; }
            public string Langz { get; set; }
            public string Mandt { get; set; }
            public string Noofinstallment { get; set; }
            public string OfficerTz { get; set; }
            public string Officerz { get; set; }
            public string Operationz { get; set; }
            public string Partner { get; set; }
            public string Partnernm { get; set; }
            public string Peneltyamt { get; set; }
            public string Periodkeyz { get; set; }
            public string PortalUsrz { get; set; }
            public string ReturnId { get; set; }
            public string ReturnIdz { get; set; }
            public string SrcAppz { get; set; }
            public string Statusz { get; set; }
            public string StepNumberz { get; set; }
            public string Totdueamt { get; set; }
            public string Totliablityamt { get; set; }
            public string TxnTpz { get; set; }
            public string UserTypz { get; set; }
            public string Vtref { get; set; }
            public string Waers { get; set; }
            public VTADSet VTADSet { get; set; }
            public VATNOTESSet NOTESSet { get; set; }
            public VTISSet VTISSet { get; set; }
            public VTIASet VTIASet { get; set; }
            public ATTACHMENTSet ATTACHMENTSet { get; set; }

        }


        #region VATInstalmentList

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Result
        {
            public Metadata2 __metadata { get; set; }
            public string FbtText { get; set; }
            public string Fbtyp { get; set; }
            public string UserErrFg { get; set; }
            public string SysFlg { get; set; }
            public string Ldate { get; set; }
        }

        public class REQTYPSet
        {
            public List<Result> results { get; set; }
        }

        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Result21
        {
            public Metadata3 __metadata { get; set; }
            public string Mandt { get; set; }
            public string Stsma { get; set; }
            public string Estat { get; set; }
            public string Spras { get; set; }
            public string Txt04 { get; set; }
            public string Txt30 { get; set; }
            public bool Ltext { get; set; }
        }

        public class STATUSSet
        {
            public List<Result21> results { get; set; }
        }

        public class Metadata4
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Result31
        {
            public Metadata4 __metadata { get; set; }
            public string Selector { get; set; }
            public string Fbnum { get; set; }
            public string Fbtyp { get; set; }
            public string Gpart { get; set; }
            public string NameLast { get; set; }
            public string NameFirst { get; set; }
            public string NameOrg1 { get; set; }
            public string NameOrg2 { get; set; }
            public string FullNm { get; set; }
            public string GpartFullNm { get; set; }
            public string WfSub { get; set; }
            public string Fbust { get; set; }
            public string FbustTxt { get; set; }
            public string Receipt { get; set; }
            public string AssignUsr { get; set; }
            public string LoginUsr { get; set; }
            public string AssignMe { get; set; }
            public string NewUser { get; set; }
            public string TileInd { get; set; }
            public string FbtText { get; set; }
            public string TransactionType { get; set; }
            public string WiId { get; set; }
            public string WiPrio { get; set; }
            public string WiPrioDesc { get; set; }
        }

        public class ASSLISTSet
        {
            public List<Result31> results { get; set; }
        }

        public class ReqVatInstalmentPlan
        {
            public Metadata __metadata { get; set; }
            public string UserTin { get; set; }
            public string AudTin { get; set; }
            public object Begda { get; set; }
            public string TaxType { get; set; }
            public object Endda { get; set; }
            public string Euser { get; set; }
            public string Fbnum { get; set; }
            public string Fbsta { get; set; }
            public string Fbtyp { get; set; }
            public string Fbust { get; set; }
            public string Formproc { get; set; }
            public string Gpart { get; set; }
            public string Lang { get; set; }
            public string Mandt { get; set; }
            public string Officer { get; set; }
            public string Operation { get; set; }
            public string Persl { get; set; }
            public string PortalUsr { get; set; }
            public string ReturnId { get; set; }
            public string Status { get; set; }
            public string StepNumber { get; set; }
            public string TransactionType { get; set; }
            public string TxnTp { get; set; }
            public string UserTyp { get; set; }
            public REQTYPSet REQTYPSet { get; set; }
            public STATUSSet STATUSSet { get; set; }
            public ASSLISTSet ASSLISTSet { get; set; }
        }

        public class ReqVatInstalmentPlanResponse
        {
            public ReqVatInstalmentPlan d { get; set; }
        }

        #endregion

        #region Display Instalmenent Schedules 

        public class DisplayInstallmentAgreementSchedulePlan
        {

            [JsonProperty("d")]
            public D d { get; set; }


            public partial class D
            {
                [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("FormGuid")]
                public string FormGuid { get; set; }

                [JsonProperty("Langz")]
                public string Langz { get; set; }

                [JsonProperty("Noofmon")]
                public string Noofmon { get; set; }

                [JsonProperty("Euser")]
                public string Euser { get; set; }

                [JsonProperty("Opbel")]
                public string Opbel { get; set; }

                [JsonProperty("Gpart")]
                public string Gpart { get; set; }

                [JsonProperty("TotalInstall")]
                public string TotalInstall { get; set; }

                [JsonProperty("TotalAmntPaid")]
                public string TotalAmntPaid { get; set; }

                [JsonProperty("TotalRemAmnt")]
                public string TotalRemAmnt { get; set; }

                [JsonProperty("VTIA_IADTSet")]
                public VtiaIaSet VtiaIadtSet { get; set; }

                [JsonProperty("VTIA_IAHDSet")]
                public VtiaIaSet VtiaIahdSet { get; set; }
            }

            public partial class Metadata
            {
                [JsonProperty("id")]
                public Uri Id { get; set; }

                [JsonProperty("uri")]
                public Uri Uri { get; set; }

                [JsonProperty("type")]
                public string Type { get; set; }
            }

            public partial class VtiaIaSet
            {
                [JsonProperty("results")]
                public List<VtiaIaSetResult> Results { get; set; }
            }

            public partial class VtiaIaSetResult
            {
                [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("Opbel")]
                public string Opbel { get; set; }

                [JsonProperty("InstalmentAmt")]
                public string InstalmentAmt { get; set; }

                [JsonProperty("RemainingAmt")]
                public string RemainingAmt { get; set; }

                [JsonProperty("Waers")]
                public string Waers { get; set; }

                [JsonProperty("PeriodText")]
                public string PeriodText { get; set; }

                [JsonProperty("AgreementNo")]
                public string AgreementNo { get; set; }
            }
        }

        public class VATInstalmentDetailsInputModel
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class Metadata
            {
                public string id { get; set; }
                public string uri { get; set; }
                public string type { get; set; }
            }

            public class D
            {
                public Metadata __metadata { get; set; }
                public object Abrzo { get; set; }
                public object Abrzu { get; set; }
                public object Aedat { get; set; }
                public bool AudFlag { get; set; }
                public string Auditor { get; set; }
                public object Begda { get; set; }
                public string Branch { get; set; }
                public string CalendrTyp { get; set; }
                public string Comb { get; set; }
                public string Dispflag { get; set; }
                public string Due { get; set; }
                public object DueDt { get; set; }
                public string DueDtC { get; set; }
                public object Endda { get; set; }
                public string Euser { get; set; }
                public string Euser1 { get; set; }
                public string Euser2 { get; set; }
                public string Euser3 { get; set; }
                public string Euser4 { get; set; }
                public string Euser5 { get; set; }
                public string Fbguid { get; set; }
                public string Fbnum { get; set; }
                public string FbtText { get; set; }
                public string Fbtyp { get; set; }
                public string Flag { get; set; }
                public string Gpart { get; set; }
                public string Incotext { get; set; }
                public string Incotyp { get; set; }
                public string InfoMsg { get; set; }
                public string Lang { get; set; }
                public string Monthz { get; set; }
                public string Msg { get; set; }
                public bool ObjFiled { get; set; }
                public string ObligFlag { get; set; }
                public bool Open { get; set; }
                public string Period { get; set; }
                public string Persl { get; set; }
                public bool RefundFiled { get; set; }
                public string SadadDoc1 { get; set; }
                public string SadadDoc2 { get; set; }
                public string Stat { get; set; }
                public string Statflag { get; set; }
                public string Status { get; set; }
                public string TaxPeriod { get; set; }
                public string Vtref { get; set; }
            }


            public D d { get; set; }

        }

        public class VATInstalmentScheduleDetailsModel
        {

            [JsonProperty("d")]
            public D d { get; set; }


            public partial class D
            {
                [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("FormGuid")]
                public string FormGuid { get; set; }

                [JsonProperty("Langz")]
                public string Langz { get; set; }

                [JsonProperty("Noofmon")]
                public string Noofmon { get; set; }

                [JsonProperty("Euser")]
                public string Euser { get; set; }

                [JsonProperty("Opbel")]
                public string Opbel { get; set; }

                [JsonProperty("Gpart")]
                public string Gpart { get; set; }

                [JsonProperty("TotalInstall")]
                public string TotalInstall { get; set; }

                [JsonProperty("TotalAmntPaid")]
                public string TotalAmntPaid { get; set; }

                [JsonProperty("TotalRemAmnt")]
                public string TotalRemAmnt { get; set; }

                [JsonProperty("VTIA_IADTSet")]
                public VtiaIadtSet1 VtiaIadtSet { get; set; }

                [JsonProperty("VTIA_IAHDSet")]
                public VtiaIahdSet VtiaIahdSet { get; set; }
            }

            public partial class Metadata
            {
                [JsonProperty("id")]
                public Uri Id { get; set; }

                [JsonProperty("uri")]
                public Uri Uri { get; set; }

                [JsonProperty("type")]
                public string Type { get; set; }
            }

            public partial class VtiaIadtSet1
            {
                [JsonProperty("results")]
                public List<VtiaIadtSetResult> Results { get; set; }
            }

            public partial class VtiaIadtSetResult
            {
                [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("DueDate")]
                public string DueDate { get; set; }

                [JsonProperty("NoOfMonths")]
                public string NoOfMonths { get; set; }

                [JsonProperty("InstalmentAmt")]
                public string InstalmentAmt { get; set; }

                [JsonProperty("TotalPaidAmt")]
                public string TotalPaidAmt { get; set; }

                [JsonProperty("RemainingAmt")]
                public string RemainingAmt { get; set; }

                [JsonProperty("Status")]
                public string Status { get; set; }

                [JsonProperty("Waers")]
                public string Waers { get; set; }
            }

            public partial class VtiaIahdSet
            {
                [JsonProperty("results")]
                public List<VtiaIahdSetResult> Results { get; set; }
            }

            public partial class VtiaIahdSetResult
            {
                [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("Opbel")]
                public string Opbel { get; set; }

                [JsonProperty("InstalmentAmt")]
                public string InstalmentAmt { get; set; }

                [JsonProperty("RemainingAmt")]
                public string RemainingAmt { get; set; }

                [JsonProperty("Waers")]
                public string Waers { get; set; }

                [JsonProperty("PeriodText")]
                public string PeriodText { get; set; }

                [JsonProperty("AgreementNo")]
                public string AgreementNo { get; set; }
            }
        }

        #endregion

    }
}
