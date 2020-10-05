using System;
using EGAZT.Models.AccountStatements;
using Xamarin.Forms;

namespace EGAZT.CustomControl
{
    public class AccountStatementsLVDataTemplateSelector: DataTemplateSelector
    {
		public DataTemplate TotalBalanceTemplate { get; set; }

		public DataTemplate OtherDataTemplate { get; set; }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return ((ASResult)item).IsTotalBalanceVisile == true ? TotalBalanceTemplate : OtherDataTemplate;
		}
	}
}
