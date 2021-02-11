

namespace EGAZT.Helper
{
    public interface IApplePayAuthorizer
    {
        bool AuthorizePayment(double Amount, string Title);
        bool IsPaymentFromDashboard (bool isDahboard);

    }
}

