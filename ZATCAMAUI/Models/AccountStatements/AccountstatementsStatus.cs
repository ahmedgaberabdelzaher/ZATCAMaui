using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.AccountStatements
{
    public class AccountstatementsStatus
    {
        [JsonProperty("data")]
        public D d { get; set; }

        public AccountstatementsStatus()
        {

        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class D
    {
        [JsonProperty("paymentStatus")]
        public List<AccountStatus> results { get; set; }


    }

    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class AccountStatus
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("paymentStatusCode")]
        public string ZtpaccSts { get; set; }
        [JsonProperty("paymentStatusDescription")]
        public string PymtStatus { get; set; }
    }
}

