using Newtonsoft.Json;

namespace ZATCAMAUI.Models.TINOutletDeregister;
public class TinOutletPrevousRequestsModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    [JsonProperty("data")]
    public DeRegisterPrevousResponse D { get; set; }

    public class DeRegisterPrevousResponse
    {
        public Metadata Metadata { get; set; }

        [JsonProperty("systemCode")]
        public string Mandt { get; set; }

        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }

        [JsonProperty("portalUser")]
        public string PortalUsr { get; set; }

        [JsonProperty("language")]
        public string Lang { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }

        [JsonProperty("returnId")]
        public string ReturnId { get; set; }

        [JsonProperty("officer")]
        public string Officer { get; set; }

        [JsonProperty("TIN")]
        public string Gpart { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("userType")]
        public string UserTyp { get; set; }

        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }

        [JsonProperty("formProcess")]
        public string Formproc { get; set; }

        //[JsonProperty("officer")]
        public string OfficerT { get; set; }

        [JsonProperty("sourceApplication")]
        public string SrcApp { get; set; }

        [JsonProperty("periodKey")]
        public string Periodkey { get; set; }

        [JsonProperty("beginDate")]
        public object Begda { get; set; }

        [JsonProperty("endDate")]
        public object Endda { get; set; }

        [JsonProperty("userTIN")]
        public string UserTin { get; set; }

        [JsonProperty("channel")]
        public string Inpch { get; set; }

        [JsonProperty("requests")]
        public List<PreviousRequests> WIItemSet { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class PreviousRequests
    {
        public Metadata Metadata { get; set; }

        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("TIN")]
        public string Gpart { get; set; }

        private string _status1 { get; set; }

        [JsonProperty("status1")]
        public string Status1
        {
            get
            {
                return _status1;
            }

            set
            {
                _status1 = value;

                if (_status1.Equals("E0001") || _status1.Equals("E0018"))
                {
                    IsCancelVisible = true;
                }
            }

        }

        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }

        private string _status2 { get; set; }
        [JsonProperty("status2")]
        public string Status2
        {
            get
            {
                return _status2;
            }

            set
            {
                _status2 = value;

                if (_status2.Equals("E0018"))
                {
                    IsDeleteVisible = true;
                }
            }
        }

        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }

        [JsonProperty("deregistrationType")]
        public string Dtype { get; set; }

        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }

        private string _date;

        [JsonProperty("startDate")]
        public string Date
        {
            get { return _date; }
            set
            {
                DateTime originalDate = DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ss", null);
                _date = originalDate.ToString("yyyy/MM/dd");
            }
        }

        [JsonProperty("dregOldApplication")]
        public string DregOld { get; set; }
        public bool IsCancelVisible { get; set; }
        public bool IsDeleteVisible { get; set; }
    }

    public class WIItemSet
    {
        [JsonProperty("results")]
        public List<PreviousRequests> Results { get; set; }
    }
}

