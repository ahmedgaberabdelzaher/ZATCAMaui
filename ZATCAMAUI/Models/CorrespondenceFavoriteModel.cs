<<<<<<< HEAD:ZATCAMAUI/Models/CorrespondenceFavoriteModel.cs
﻿namespace ZATCAMAUI.Models
=======
﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
>>>>>>> c4bcf28b6 (CR6238 code merge to prod by chandu):GAZT/GAZT/Models/CorrespondenceFavoriteModel.cs
{

    public class CorrespondenceFavoriteModel
    {
        [JsonProperty("correspondenceKey")]
        public string Cokey { get; set; }
        [JsonProperty("correspondenceType")]
        public string Cotyp { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("contractAccount")]
        public string Vkont { get; set; }
        [JsonProperty("startDate")]
        public string Begdaz { get; set; }
        [JsonProperty("endDate")]
        public string Enddaz { get; set; }
        [JsonProperty("isFavourite")]
        public bool Zzfav { get; set; }
    }
}
