using System;
using System.Collections.ObjectModel;

namespace EGAZT.Models
{
    public class GenericDatePickerModel
    {
        public GenericDatePickerModel()
        {
        }

        public string PickerId { get; set; }
        public string DatePickerTitle { get; set; }
        public ObservableCollection<string> PickerData { get; set; }
        public string SelectedValue { get; set; }
    }
}
