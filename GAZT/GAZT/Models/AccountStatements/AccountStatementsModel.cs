using System;
using Xamarin.Forms;

namespace EGAZT.Models.AccountStatements
{
    public class AccountStatementsModel
    {
        public AccountStatementsModel()
        {
        }
    }

    public class ASReturnTypes
    {
        public string TaxType { get; set; }
        public string Id { get; set; }
    }

    public class ASChipModel
    {
        public string TemplateType { get; set; }
        public string Text { get; set; }
        public ImageSource ImageSource { get; set; }
    }
}
