
using ZATCAMAUI.Models.AccountStatements;

namespace ZATCAMAUI.Core.CustomControls
{

    public class AccountStatementsLVDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TotalBalanceTemplate { get; set; }

        public DataTemplate OtherDataTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((ASResult)item).IsTotalBalanceVisile == true ? TotalBalanceTemplate : OtherDataTemplate;
        }
    }
}
