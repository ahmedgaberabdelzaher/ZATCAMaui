using Syncfusion.Maui.ListView;
using ZATCAMAUI.Views.NewDesign.Template;

namespace ZATCAMAUI.Views.NewDesign.TemplateSelector
{
    public class LVUnselectedItemTemplateSelectorNoIcon : DataTemplateSelector
    {
        DataTemplate selectedTemplateNoIcon;
        DataTemplate unselectedTemplateNoIcon;
        public LVUnselectedItemTemplateSelectorNoIcon()
        {
            selectedTemplateNoIcon = new DataTemplate(typeof(SelectedItemTemplateNoIcon));
            unselectedTemplateNoIcon = new DataTemplate(typeof(UnSelectedItemTemplateNoIcon));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return unselectedTemplateNoIcon;
        }
    }
    public class LVSelectedItemTemplateSelectorNoIcon : DataTemplateSelector
    {
        DataTemplate selectedTemplateNoIcon;
        DataTemplate unselectedTemplateNoIcon;
        public LVSelectedItemTemplateSelectorNoIcon()
        {
            selectedTemplateNoIcon = new DataTemplate(typeof(SelectedItemTemplateNoIcon));
            unselectedTemplateNoIcon = new DataTemplate(typeof(UnSelectedItemTemplateNoIcon));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((SfListView)container).SelectedItem != null)
            {
                return selectedTemplateNoIcon;
            }
            else
            {
                return unselectedTemplateNoIcon;
            }
        }
    }
}
