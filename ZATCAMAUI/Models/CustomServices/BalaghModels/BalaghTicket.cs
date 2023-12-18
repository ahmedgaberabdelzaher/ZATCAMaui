using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices.BalaghModels
{

    public class Ticketfile
    {
        public string filename { get; set; }
        public string filecontent { get; set; }
        public double FileSize { get; set; }
        public string Id { get; set; }
    }

    public class BalaghTicket
    {
        public int balaghtypeid { get; set; }
        public string otherbalaghtype { get; set; }
        public int balaghplaceid { get; set; }
        public string otherbalaghplace { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string ticketcontent { get; set; }
        public ObservableCollection<Ticketfile> ticketfiles { get; set; }
    }
}
