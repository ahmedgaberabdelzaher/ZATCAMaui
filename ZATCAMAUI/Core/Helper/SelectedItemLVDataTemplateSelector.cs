using ZATCAMAUI.Models;

namespace ZATCAMAUI.Core.Helper
{

    public class SelectedItemLVDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedItemTemplated { get; set; }

        public DataTemplate NotSelectedItemTemplated { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((TINDeregistrationModel)item).ActiveOutletDecisionOptionsIsSelected == true ? SelectedItemTemplated : NotSelectedItemTemplated;
        }
    }
}
