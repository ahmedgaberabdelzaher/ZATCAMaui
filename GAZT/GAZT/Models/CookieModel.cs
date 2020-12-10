using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class CookieModel
    {
        public string CName { get; set; }
        public string CValue { get; set; }
        public string Comment { get; set; }
        public string Domain { get; set; }
        public string HttpOnly { get; set; }
        public string Path { get; set; }
        public bool Secure { get; set; }
        public int Version { get; set; }
        public bool IsHttpOnly { get; set; }

        public CookieModel()
        {

        }
    }
    [Preserve(AllMembers = true)]
    public class CookieHeaderModel
    {
        [JsonProperty("sap-metadata-last-modified")]
        public string[] sap_metadata_last_modified { get; set; }

        [JsonProperty("content-type")]
        public string[] content_type { get; set; }

        [JsonProperty("cache-control")]
        public string[] cache_control { get; set; }

        [JsonProperty("sap-processing-info")]
        public string[] sap_processing_info { get; set; }

        [JsonProperty("set-cookie")]
        public string[] set_cookie { get; set; }

        [JsonProperty("content-length")]
        public string[] content_length { get; set; }

        [JsonProperty("sap-perf-fesrec")]
        public string[] sap_perf_fesrec { get; set; }

        [JsonProperty("dataserviceversion")]
        public string[] dataserviceversion { get; set; }
    }
}
