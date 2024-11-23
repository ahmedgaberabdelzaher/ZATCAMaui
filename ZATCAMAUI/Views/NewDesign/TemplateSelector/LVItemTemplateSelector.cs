using Syncfusion.Maui.ListView;
using ZATCAMAUI.Views.NewDesign.Template;

namespace ZATCAMAUI.Views.NewDesign.TemplateSelector
{
    public class LVUnselectedItemTemplateSelector : DataTemplateSelector
    {
        DataTemplate selectedTemplate;
        DataTemplate unselectedTemplate;
        public LVUnselectedItemTemplateSelector()
        {
            selectedTemplate = new DataTemplate(typeof(SelectedItemTemplate));
            unselectedTemplate = new DataTemplate(typeof(UnSelectedItemTemplate));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return unselectedTemplate;
        }
    }
    public class LVSelectedItemTemplateSelector : DataTemplateSelector
    {
        DataTemplate selectedTemplate;
        DataTemplate unselectedTemplate;
        public LVSelectedItemTemplateSelector()
        {
            selectedTemplate = new DataTemplate(typeof(SelectedItemTemplate));
            unselectedTemplate = new DataTemplate(typeof(UnSelectedItemTemplate));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((SfListView)container).SelectedItem != null)
            {
                return selectedTemplate;
            }
            else
            {
                return unselectedTemplate;
            }
        }
    }
}
