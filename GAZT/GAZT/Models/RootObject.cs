using EGAZT.Models;
using System;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class RootObject
    {
       public ZakatReturnDetails zakatReturnDetailsD { get; set; }
      public  SalesDetails salesDetails { get; set; }        
    }
}
