using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EGAZT.Models.VATInstalationModels
{
    public class VATInstalmentPlanModel
    {
        public VATInstalmentPlanModel()
        {
        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }

    public class InstalmentAgreementFrequencyModel
    {
        public InstalmentAgreementFrequencyModel()
        {

        }

        public string FrequencyOptions { get; set; }
        public bool IsSelected { get; set; }
    }

    public class InstalmentAgreementAttachmentsModel
    {
        public InstalmentAgreementAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }

    public class ZakatSelectBillModel
    {
        public ZakatSelectBillModel()
        {

        }

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }
    }

    public class ZakatSummaryViewModel
    {
        public ZakatSummaryViewModel()
        {

        }

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }

    }

    public class InstalmentAgreementInstalmentPlansModel
    {
        public InstalmentAgreementInstalmentPlansModel()
        {
        }
        public string DueDate { get; set; }
        public string NumberOfMonths { get; set; }
        public string MonthlyInstalment { get; set; }
        public string TotalAmountPaid { get; set; }
        public string TotalAmountRemaining { get; set; }
    }

    //-----API Object will starts from herer-------

    public partial class VatInstalmentPlanResponse
    {
        public VATInstalment d { get; set; }
    }

    public partial class VATInstalment
    {
        public Metadata __metadata { get; set; }
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
        public VtadSet VTADSet { get; set; }
        public NotesSet NotesSet { get; set; }
        public VtisSet VTISSet { get; set; }
        public VtiaSet VTIASet { get; set; }
        public AttachmentSet AttachmentSet { get; set; }
    }

    public partial class AttachmentSet
    {
        public List<Attachment> results { get; set; }
    }

    public partial class Metadata
    {
        public Uri id { get; set; }
        public Uri uri { get; set; }
        public string type { get; set; }
    }

    public partial class NotesSet
    {
        public object[] results { get; set; }
    }

    public partial class VtadSet
    {
        public object[] results { get; set; }
    }

    public partial class VtiaSet
    {
        public VATResults4[] results { get; set; }
    }

    public partial class VATResults4
    {
        public Metadata __metadata { get; set; }
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

    public partial class VtisSet
    {
        public VATResults3[] results { get; set; }
    }

    public partial class VATResults3
    {
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Faedn { get; set; }
       // public string dueDate { get; set; }
        public string Monat { get; set; }
        public string Betrw { get; set; }
        public string Totpaidamt { get; set; }
        public string Totremainamt { get; set; }
        public string Waers { get; set; }
        public string ReturnId { get; set; }
    }

    public partial class VatInstalmentPlanRequest
    {
        public VATInstalmentRequest d { get; set; }
    }


    public partial class VATInstalmentRequest
    {
        public Metadata __metadata { get; set; }
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
        public object[] VTADSet { get; set; }
        public object[] NOTESSet { get; set; }
        public VATResults3[] VTISSet { get; set; }
        public VATResults4[] VTIASet { get; set; }
        public List<Attachment> ATTACHMENTSet { get; set; }
    }




}
