using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.ESTOutletAddress
{
    public class ESTOutletAddressRequest
    {
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string taxpayerType { get; set; }
        public string TIN { get; set; }
    }
}
