using System;
using EGAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.Helper
{
	[Preserve(AllMembers = true)]
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
