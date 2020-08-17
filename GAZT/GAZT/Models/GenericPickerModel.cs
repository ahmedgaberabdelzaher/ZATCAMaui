using System;
using System.Collections.ObjectModel;

namespace EGAZT.Models
{
    public class GenericPickerModel
    {
        public GenericPickerModel()
        {
        }

        public string PickerId { get; set; }
        public string PickerTitle { get; set; }
        public ObservableCollection<string> PickerData { get; set; }
        public string SelectedValue { get; set; }
    }
}
