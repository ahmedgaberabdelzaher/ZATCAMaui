using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models.PaymentModel
{
    public class PaymentOptionsModel
    {
        public PaymentOptionsModel()
        {
        }
        public string CardLabel { get; set; }
        public string UnSelectedCardIcon { get; set; }
        public string SelectedCardIcon { get; set; }
        public bool IsSelectedCardIconVisible { get => true; }
        public bool IsUnSelectedCardIconVisible { get => true; }

    }
}
