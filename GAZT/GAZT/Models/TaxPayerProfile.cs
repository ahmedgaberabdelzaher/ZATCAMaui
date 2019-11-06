using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    //ideally it should be UserId, TIN etc. but based on API responsethe naming of variable has been matched 
    public class TaxPayerProfile
    {
        public String Email { get; set; }
        public String Userid { get; set; }
        public String Tin { get; set; }
        public String Mobile { get; set; }        
    }
}
