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
        public string IsRed { get; set; } = string.Empty;
        public string IsBold { get; set; } = string.Empty;
        public string Link { get; set; }
        public string FlowDirections { get; set; }
        public bool isFontSet { get; set; }
    }

    public class ForPdfJs
    {
        public string Url { get; set; }
        public bool IsUrl { get; set; }

        public string Base64 { get; set; }
    }
}
