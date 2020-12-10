using EGAZT;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class Dashboard
    {
        public List<DashboardResult> results { get; set; }
    }
    public class DashboardMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class DashboardResult
    {
        public DashboardMetadata __metadata { get; set; }
        public string Caltype { get; set; }
        public string EstimateZkat { get; set; }
        public string TpType { get; set; }
        public string Tin { get; set; }
        public string Vktyp { get; set; }
        public string RtnTot { get; set; }
        public string NrtnTot { get; set; }
        public string PrtnTot { get; set; }
        public string UprtnTot { get; set; }
        public string PprtnTot { get; set; }
        public string IcrTot { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string Text1 { get; set; }
        public string DueIcr { get; set; }
        public DateTime? Begda { get; set; }
        public DateTime? Endda { get; set; }
        public string Persl { get; set; }
        public string Waers { get; set; }
        public string PbillsTot { get; set; }
        public string PbillsBetrw { get; set; }
        public string UpbillsTot { get; set; }
        public string UpbillsBetrw { get; set; }
        public string PrbillsTot { get; set; }
        public string PrbillsBetrw { get; set; }
    }
}
