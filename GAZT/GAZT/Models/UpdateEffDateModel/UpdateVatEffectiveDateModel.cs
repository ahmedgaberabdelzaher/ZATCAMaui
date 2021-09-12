using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.UpdateEffDateModel
{
    [Preserve(AllMembers = true)]
    public class UpdateVatEffectiveDateModel
    {
        public D d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class D
    {
        public Metadata Metadata { get; set; }
        public string GrpregFg { get; set; }
        public string Mandt { get; set; }
        public string Fbnum { get; set; }
        public string PortalUsr { get; set; }
        public string Lang { get; set; }
        public string Operation { get; set; }
        public string StepNumber { get; set; }
        public string ReturnId { get; set; }
        public string Officer { get; set; }
        public string Gpart { get; set; }
        public string Status { get; set; }
        public string UserTyp { get; set; }
        public string TxnTp { get; set; }
        public string Formproc { get; set; }
        public string OfficerT { get; set; }
        public string SrcApp { get; set; }
        public string Periodkey { get; set; }
        public object Begda { get; set; }
        public object Endda { get; set; }
        public string UserTin { get; set; }
        public string Inpch { get; set; }
        public string RegTp { get; set; }
        public ItemSet ItemSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public  class ItemSet
    {
        public List<ItemSetResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class ItemSetResult
    {
        public Metadata Metadata { get; set; }
        public long Mandt { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Tin { get; set; }
        public string Fbnum { get; set; }
        public DateTime? EffDtBefore { get; set; }
        public DateTime? EffDtAfter { get; set; }
        public string UpdatedBy { get; set; }
        public string ChangeReason { get; set; }
        public string AppStatus { get; set; }
        public string AppLatePen { get; set; }
        public string GenNewReturn { get; set; }
        public string UserType { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class Metadata
    {
        public Uri Id { get; set; }
        public Uri Uri { get; set; }
        public string Type { get; set; }
    }


}
