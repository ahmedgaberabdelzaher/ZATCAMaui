using EGAZT.Views.NewDesign.Template;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.TemplateSelector
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
            //Application.Current.MainPage.DisplayAlert("", "", "cancel");
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
