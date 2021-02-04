using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class SignUpUsing
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]

        public string SUType { get; set; }
    }
}
