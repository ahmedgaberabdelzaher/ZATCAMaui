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


    public class D
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string Link { get; set; }
        public string Srcid { get; set; }
        public string Guid { get; set; }
        public string Tin { get; set; }
    }

    public class ValidatePaymentResponse
    {
        public D d { get; set; }
    }
}
