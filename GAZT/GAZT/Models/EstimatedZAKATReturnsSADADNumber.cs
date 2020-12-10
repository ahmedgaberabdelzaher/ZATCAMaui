using EGAZT.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumber
    {
        public EstimatedZAKATReturnsSADADNumberD d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberDeferred
    {
        public string uri { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberDeferredReasonSet
    {
        public EstimatedZAKATReturnsSADADNumberDeferred __deferred { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberDeferred2
    {
        public string uri { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberAttachSet
    {
        public EstimatedZAKATReturnsSADADNumberDeferred2 __deferred { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberResult
    {
        public Metadata2 __metadata { get; set; }
        public string Undisamt { get; set; }
        public string Disamt { get; set; }
        public string Totamt { get; set; }
        public string Sopbel { get; set; }
        public string Sadadid { get; set; }
        public string Sundisamt { get; set; }
        public string Sdisamt { get; set; }
        public string Stotamt { get; set; }
        public bool ObjectionInvoiceVisibility { get; set; } = false;
        public bool AmendInvoiceVisibility { get; set; } = false;
        public bool InvoiceVisibility { get; set; } = false;
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberInvoiceSet
    {
        public List<EstimatedZAKATReturnsSADADNumberResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Deferred3
    {
        public string uri { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberThresholdSet
    {
        public Deferred3 __deferred { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class EstimatedZAKATReturnsSADADNumberD
    {
        public Metadata __metadata { get; set; }
        public string Fsource { get; set; }
        public string LabnoI { get; set; }
        public string Zkamt { get; set; }
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
        //public DateTime TimestampCr { get; set; }
       // public DateTime TimestampCh { get; set; }
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
        public string PramtI { get; set; }
        public string PramtE { get; set; }
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
        //public string Enddaz { get; set; }
        public string Disamt { get; set; }
        public string DataVersion { get; set; }
        public string CpamtResn { get; set; }
        public string Cpamt { get; set; }
        public string Cotyp { get; set; }
        public string Cokey { get; set; }
      //  public string Begdaz { get; set; }
        public string AmdRsn { get; set; }
        public string Amdflg { get; set; }
       // public DateTime Abrzu { get; set; }
        //public DateTime Abrzo { get; set; }
        public EstimatedZAKATReturnsSADADNumberDeferredReasonSet ReasonSet { get; set; }
        public EstimatedZAKATReturnsSADADNumberAttachSet AttachSet { get; set; }
        public EstimatedZAKATReturnsSADADNumberInvoiceSet InvoiceSet { get; set; }
        public EstimatedZAKATReturnsSADADNumberThresholdSet ThresholdSet { get; set; }
    }
  
    //public class RootObject
    //{
    //    public EstimatedZAKATReturnsSADADNumberD d { get; set; }
    //}
}
