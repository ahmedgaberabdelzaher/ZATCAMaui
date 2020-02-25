using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class CorrespondanceModel
    {
        public string Title { get; set; }
        public string RefNumber { get; set; }
        public string DateAndTime { get; set; }

        public bool IsFav { get; set; }

        public string FavImg { get; set; } = "ic_save_Gray.png";
    }
}
