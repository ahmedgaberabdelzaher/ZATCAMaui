using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
namespace GAZT.Models
{
    public class CorrespondanceModel
    {
        public string Title { get; set; }
        public string RefNumber { get; set; }
        public string DateAndTime { get; set; }
        public bool IsFav { get; set; }
        public string FavImg { get; set; } = "ic_star_border.png";
        public string Cokey { get; set; }
        public DateTime Txtco { get; set; }
        public DateTime StartDate { get; set; }
        public string Cotype { get; set; }
        public string Coitm { get; set; }
        public string Gpart { get; set; }
        public string Vkont { get; set; }
        public string Begdaz { get; set; }
        public string Enddaz { get; set; }
        public string Ctime { get; set; }
        public DateTime Cdate { get; set; }
        public string TaxtpFg { get; set; }
    }
    public class CorrespondenceCollection : ObservableCollection<CorrespondanceModel>
    {
        public string MonthAndYear { get; set; }
    }
}
