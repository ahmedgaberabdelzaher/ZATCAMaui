using EGAZT.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class ZakatReturnDetails
    {
        public ZakatReturnDetailsD d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ZakatReturnDetailsReason
    {
        public Metadata2 __metadata { get; set; }
        public string Mandt { get; set; }
        public string Spras { get; set; }
        public string AmdSource { get; set; }
        public string Description { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class EstimateZakatAttachment
    {
      //  public Metadata3 __metadata { get; set; }
        public string RetGuid { get; set; }// Comp
        public string Seqno { get; set; }
        public string SchGuid { get; set; }
        public string Dotyp { get; set; }
        public int Srno { get; set; }
        public string Doguid { get; set; }// Comp
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public string Erfdt { get; set; }//Date
      //  public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
    }


    [Preserve(AllMembers = true)]
    public class ZakatAttachment
    {
        //  public Metadata3 __metadata { get; set; }
        public string RetGuid { get; set; }// Comp
        public string Seqno { get; set; }
        public string SchGuid { get; set; }
        public string Dotyp { get; set; }
        public int Srno { get; set; }
        public string Doguid { get; set; }// Comp
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileImage { get; set; }

        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public string Erfdt { get; set; }//Date
                                         //  public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
        public string UploadededDateToShow { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ReasonSet
    {
        public List<ZakatReturnDetailsReason> results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class AttachSet
    {
        public List<EstimateZakatAttachment> results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ZakatReturnDetailsInvoice
    {
        public Metadata3 __metadata { get; set; }
        public string Undisamt { get; set; }
        public string Disamt { get; set; }
        public string Totamt { get; set; }
        public string Sopbel { get; set; }
        public string Sadadid { get; set; }
        public string Sundisamt { get; set; }
        public string Sdisamt { get; set; }
        public string Stotamt { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class InvoiceSet
    {
        public List<ZakatReturnDetailsInvoice> results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ZakatReturnDetailsThresholdSet
    {
        public Metadata4 __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Chrnm { get; set; }
        public String Begda { get; set; }//Date
        public string Endda { get; set; }//Date
        public string Value { get; set; }
        public string Type { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ThresholdSet
    {
        public List<ZakatReturnDetailsThresholdSet> results { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ZakatReturnDetailsD
    {
        public Metadata __metadata { get; set; }
        public string RestFlg { get; set; }
        public string Fsource { get; set; }
        public string LabnoI { get; set; }
        public string Zkamt { get; set; }
        public string MadabutFg { get; set; }
        public string OpenliMsg { get; set; }
        public string LabnoE { get; set; }
        public string Zbamt { get; set; }
        public string Waers { get; set; }
        public string UserTypz { get; set; }
        public string UserTinz { get; set; }
        public string TxnTpz { get; set; }
        public string TvtslResn { get; set; }
        public string TvtslI { get; set; }
        public string TvtslE { get; set; }
        public string Tpstatus { get; set; }
        public string TimestampCr { get; set; }//date
        public string TimestampCh { get; set; }//Date
        public string Tcflg { get; set; }
        public string Sumcnt { get; set; }
        public string StepNumberz { get; set; }
        public string Statusz { get; set; }
        public string SrcAppz { get; set; }
        public string SadadFlg { get; set; }
        public string ReturnIdz { get; set; }
        public string ReturnId { get; set; }
        public string PtoslResn { get; set; }
        public string PtoslI { get; set; }
        public string PramtResn { get; set; }
        public string RestResn { get; set; }
        public string PramtI { get; set; }
        public string PramtE { get; set; }
        public string RestI { get; set; }
        public string RestE { get; set; }
        public string PortalUsrz { get; set; }
        public string Persl { get; set; }
        public string Periodkeyz { get; set; }
        public string Payamt { get; set; }
        public string Operationz { get; set; }
        public string Officerz { get; set; }
        public string OfficerTz { get; set; }
        public string Objst { get; set; }
        public string ObjFlag { get; set; }
        public string Notfg { get; set; }
        public string Mandtz { get; set; }
        public string Mandt { get; set; }
        public string Langz { get; set; }
        public string LabnoResn { get; set; }
        public string Invflg { get; set; }
        public string Incotyp { get; set; }
        public string ImpvalResn { get; set; }
        public string ImpvalI { get; set; }
        public string ImpvalE { get; set; }
        public string Gpartz { get; set; }
        public string Gpart { get; set; }
        public string Formprocz { get; set; }
        public string FormGuid { get; set; }
        public string Fbnumz { get; set; }
        public string Fbnum { get; set; }
        public string Fbguid { get; set; }
        public string ExamtResn { get; set; }
        public string ExamtI { get; set; }
        public string Euser { get; set; }
        public string EtimadResn { get; set; }
        public string EtimadI { get; set; }
        public string Estsl { get; set; }
        public object Enddaz { get; set; }
        public string Disamt { get; set; }
        public string DataVersion { get; set; }
        public string CpamtResn { get; set; }
        public string Cpamt { get; set; }
        public string Cotyp { get; set; }
        public string Cokey { get; set; }
        public object Begdaz { get; set; }
        public string AmdRsn { get; set; }
        public string Amdflg { get; set; }
        public string Abrzu { get; set; }//Date
        public string Abrzo { get; set; }// Date
        public ReasonSet ReasonSet { get; set; }
        public AttachSet AttachSet { get; set; }
        public InvoiceSet InvoiceSet { get; set; }
        public ThresholdSet ThresholdSet { get; set; }
    }
}
