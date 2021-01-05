using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class InternationalMobileData
    {
        public __metadata __metadata { get; set; }
        public string Land1 { get; set; }
        public string Landx50 { get; set; }
        public string _telefto = String.Empty;
        public string Telefto
        {
            get
            {

                return _telefto;
            }
            set
            {
                _telefto = "+" + value;
            }
        }
  
        public string Spras { get; set; }
        public string Landx { get; set; }
    }

}
