using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class PopUp
    {
        public string Message { get; set; }
        public bool IsLinkAvailable { get; set; }
        public string LinkMessage { get; set; }

        public string Link { get; set; }

    }

    public class ForPdfJs
    {
        public string Url { get; set; }
        public bool IsUrl { get; set; }

        public string Base64 { get; set; }
    }
}
