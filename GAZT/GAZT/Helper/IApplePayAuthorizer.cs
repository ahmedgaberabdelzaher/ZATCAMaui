

namespace EGAZT.Helper
{
    public interface IApplePayAuthorizer
    {
        bool AuthorizePayment(string Amount, string Title);
    }
}

