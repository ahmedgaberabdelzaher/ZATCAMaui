using System.Collections.ObjectModel;

namespace ZATCAMAUI.Models.CustomServices.TraiffSection
{
    public class Note
    {
        public int note_seq { get; set; }
        public string note_arbc_desc { get; set; }
        public string note_eng_desc { get; set; }
        public string ChaptNote
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

    public class Chapter
    {
        public string sect_code { get; set; }
        public string chpt_code { get; set; }
        public string chpt_arbc_desc { get; set; }
        public string chpt_eng_desc { get; set; }

        public string Name
        {
            get
            {
                if (!App.IsArabic)
                {
                    return chpt_eng_desc;
                }
                return chpt_arbc_desc;
            }
        }
        public ObservableCollection<Note> notes { get; set; }
        public string Code { get { return chpt_code; } }
    }

    public class ChaptersModel
    {
        public bool issuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public ObservableCollection<Chapter> data { get; set; }
        public int count { get; set; }
        public string correlationid { get; set; }
    }
}
