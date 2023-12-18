using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices.TraiffSection
{
    public class SecNote
    {
        public int note_seq { get; set; }
        public string note_arbc_desc { get; set; }
        public string note_eng_desc { get; set; }
        public string Note
        {
            get
            {
                if (App.IsArabic)
                {
                    return note_arbc_desc;
                }
                else
                {
                    return note_eng_desc;
                }
            }
        }
    }

    public class Section
    {
        public string sect_code { get; set; }
        public string sect_arbc_desc { get; set; }
        public ObservableCollection<Note> notes { get; set; }
        public string sect_eng_desc { get; set; }
        public string Name
        {
            get
            {
                if (App.IsArabic)
                {
                    return sect_arbc_desc;
                }
                else
                {
                    return sect_eng_desc;
                }
            }
        }

    }

    public class SectionsModel
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<Section> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
