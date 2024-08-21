using System;
using System.ComponentModel;
using Newtonsoft.Json;
namespace ZATCAMAUI.Models.VATInstalmentModels
{
    public class VATRevokeUiListModel : INotifyPropertyChanged
    {
        //[JsonProperty("__metadata")]
        //public Metadata metadata { get; set; }
        [JsonProperty("UserTyp")]
        private string userTyp { get; set; }
        public string UserTyp
        {
            get { return userTyp; }
            set { userTyp = value; this.RaisedOnPropertyChanged("UserTyp"); }
        }
        [JsonProperty("Fbtyp")]
        private string fbtyp { get; set; }
        public string Fbtyp
        {
            get { return fbtyp; }
            set { fbtyp = value; this.RaisedOnPropertyChanged("Fbtyp"); }
        }
        [JsonProperty("Tin")]
        private string tin { get; set; }
        public string Tin
        {
            get { return tin; }
            set { tin = value; this.RaisedOnPropertyChanged("Tin"); }
        }
        [JsonProperty("Fbnum")]
        private string fbnum { get; set; }
        public string Fbnum
        {
            get { return fbnum; }
            set { fbnum = value; this.RaisedOnPropertyChanged("Fbnum"); }
        }
        [JsonProperty("SubmitDt")]
        private DateTime submitDt { get; set; }
        public DateTime SubmitDt
        {
            get { return submitDt; }
            set { submitDt = value; this.RaisedOnPropertyChanged("SubmitDt"); }
        }
        [JsonIgnore]
        private string submitDate { get; set; }
        public string SubmitDate
        {
            get { return submitDate; }
            set { submitDate = value; this.RaisedOnPropertyChanged("SubmitDate"); }
        }
        [JsonProperty("TotAmt")]
        private string totAmt { get; set; }
        public string TotAmt
        {
            get { return totAmt; }
            set { totAmt = value; this.RaisedOnPropertyChanged("TotAmt"); }
        }
        [JsonProperty("DpAmt")]
        private string dpAmt { get; set; }
        public string DpAmt
        {
            get { return dpAmt; }
            set { dpAmt = value; this.RaisedOnPropertyChanged("DpAmt"); }
        }
        [JsonProperty("DueAmt")]
        private string dueAmt { get; set; }
        public string DueAmt
        {
            get { return dueAmt; }
            set { dueAmt = value; this.RaisedOnPropertyChanged("DueAmt"); }
        }
        [JsonProperty("PymntFreq")]
        private string pymntFreq { get; set; }
        public string PymntFreq
        {
            get { return pymntFreq; }
            set { pymntFreq = value; this.RaisedOnPropertyChanged("PymntFreq"); }
        }
        [JsonProperty("PlanDur")]
        private string planDur { get; set; }
        public string PlanDur
        {
            get { return planDur; }
            set { planDur = value; this.RaisedOnPropertyChanged("PlanDur"); }
        }
        [JsonProperty("Waers")]
        private string waers { get; set; }
        public string Waers
        {
            get { return waers; }
            set { waers = value; this.RaisedOnPropertyChanged("Waers"); }
        }
        [JsonProperty("Fbsta")]
        private string fbsta { get; set; }
        public string Fbsta
        {
            get { return fbsta; }
            set { fbsta = value; this.RaisedOnPropertyChanged("Fbsta"); }
        }
        [JsonProperty("Fbust")]
        private string fbust { get; set; }
        public string Fbust
        {
            get { return fbust; }
            set { fbust = value; this.RaisedOnPropertyChanged("Fbust"); }
        }
        [JsonProperty("Status")]
        private string status { get; set; }
        public string Status
        {
            get { return status; }
            set { status = value; this.RaisedOnPropertyChanged("Status"); }
        }
        [JsonProperty("Revoke")]
        private bool revoke { get; set; }
        public bool Revoke
        {
            get { return revoke; }
            set { revoke = value; this.RaisedOnPropertyChanged("Revoke"); }
        }
        [JsonProperty("IptypeFg")]
        private string iptypeFg { get; set; }
        public string IptypeFg
        {
            get { return iptypeFg; }
            set { iptypeFg = value; this.RaisedOnPropertyChanged("IptypeFg"); }
        }
        public VATRevokeUiListModel()
        {
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
    }
}