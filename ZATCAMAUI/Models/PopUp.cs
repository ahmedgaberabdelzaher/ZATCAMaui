namespace ZATCAMAUI.Models
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
        public string HeaderText { get; set; }
    }
    
    public class NewDesignPopUp
    {
        public string MainHeader { get; set; }
        public List<HeaderWithInfo> HeaderWithInfos { get; set; }
    }
    
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
        public bool IsHeaderAvailable { get => string.IsNullOrEmpty(HeaderText) ? false : true; }

    }
    
    public class ForPdfJs
    {
        public string Url { get; set; }
        public bool IsUrl { get; set; }
        public string Base64 { get; set; }
    }
}
