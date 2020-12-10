using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class GenericPickerModel
    {
        public GenericPickerModel()
        {
        }

        public string PickerId { get; set; }
        public string PickerTitle { get; set; }
        public List<string> PickerData { get; set; }
        public string SelectedValue { get; set; }
    }
}
