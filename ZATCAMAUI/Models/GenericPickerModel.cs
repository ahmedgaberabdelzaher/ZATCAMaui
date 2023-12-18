namespace ZATCAMAUI.Models
{

    public class GenericPickerModel
    {
        public GenericPickerModel()
        {
        }

        public string PickerId { get; set; }
        public string PickerTitle { get; set; }
        public string PickerExtraData { get; set; }
        public List<string> PickerData { get; set; }
        public string SelectedValue { get; set; }
    }
}
