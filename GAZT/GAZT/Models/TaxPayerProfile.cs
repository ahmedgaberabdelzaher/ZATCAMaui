using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    //ideally it should be UserId, TIN etc. but based on API responsethe naming of variable has been matched 
    public class TaxPayerProfile
    {
        public String FirstName{ get; set; }
        public String LastName { get; set; }
        public String Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public String Email { get; set; }

        private String _NewEmail = String.Empty;
        public string NewEmail
        {
            get
            {
                return _NewEmail;
            }
            set
            {
                _NewEmail = value;
            }
        }

        public String Userid { get; set; }
        public String Tin { get; set; }

        private String _Mobile = String.Empty;
        public string Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                _Mobile = value;
            }
        }

        private String _NewMobile = String.Empty;
        public string NewMobile
        {
            get
            {
                return _NewMobile;
            }
            set
            {
                _NewMobile = value;
            }
        }


        public String Password { get; set; }
        private String _NewPassword = String.Empty;
        public string NewPassword
        {
            get
            {
                return _NewPassword;
            }
            set
            {
                _NewPassword = value;
            }
        }

    }
}
