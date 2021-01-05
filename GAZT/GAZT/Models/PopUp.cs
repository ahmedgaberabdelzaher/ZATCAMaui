using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
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
        public string HeaderText { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class NewDesignPopUp
    {
        public String MainHeader { get; set; }
        public List<HeaderWithInfo> HeaderWithInfos { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class HeaderWithInfo
    {
        public string HeaderText { get; set; }
        public string Message { get; set; }

        public string Link { get; set; }

        public string IsRed { get; set; } = string.Empty;

        public string IsBold { get; set; } = string.Empty;

        public string LinkText { get; set; }

        public bool IsLinkAvailable { get; set; }
        public string FlowDirections { get; set; }
        public bool isFontSet { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class ForPdfJs
    {
        public string Url { get; set; }
        public bool IsUrl { get; set; }
        public string Base64 { get; set; }
    }
}
