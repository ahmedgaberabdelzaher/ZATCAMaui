using EGAZT.Views.NewDesign.Template;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TemplateSelector
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
            //Application.Current.MainPage.DisplayAlert("", "", "cancel");
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
