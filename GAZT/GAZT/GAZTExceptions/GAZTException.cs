using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GAZTeServicesBusinessLibrary.GAZTExceptions
{
    public class GAZTException : Exception
    {
        public GAZTException()
        {

        }

        public GAZTException(string exceptionMessage) : base(exceptionMessage)
        {

        }
    }

    public class GAZTInternetException : GAZTException
    {
        public GAZTInternetException()
        {

        }

        public GAZTInternetException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTTokenExpiredException : GAZTException
    {
        public GAZTTokenExpiredException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTNetworkConnectivityIssueException : GAZTException
    {
        public GAZTNetworkConnectivityIssueException()
        {
        }

        public GAZTNetworkConnectivityIssueException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTUserDoesNotExistException : GAZTException
    {
        public GAZTUserDoesNotExistException()
        {
        }

        public GAZTUserDoesNotExistException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTUserAuthenticationFailedException : GAZTException
    {
        public GAZTUserAuthenticationFailedException()
        {
        }

        public GAZTUserAuthenticationFailedException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTPasswordLockedException : GAZTException
    {
        public GAZTPasswordLockedException()
        {
        }

        public GAZTPasswordLockedException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTUserNotValidException : GAZTException
    {
        public GAZTUserNotValidException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTUserAccountLockedException : GAZTException
    {
        public GAZTUserAccountLockedException()
        {
        }

        public GAZTUserAccountLockedException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTPasswordIsLockedDueToInvalidAttemptsException : GAZTException
    {
        public GAZTPasswordIsLockedDueToInvalidAttemptsException()
        {
        }

        public GAZTPasswordIsLockedDueToInvalidAttemptsException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTTaxpayersAccountNotActiveWithGAZTException : GAZTException
    {
        public GAZTTaxpayersAccountNotActiveWithGAZTException()
        {
        }

        public GAZTTaxpayersAccountNotActiveWithGAZTException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTWrongTINOrEmailException : GAZTException
    {
        public GAZTWrongTINOrEmailException()
        {
        }

        public GAZTWrongTINOrEmailException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTWrongPasswordException : GAZTException
    {
        public GAZTWrongPasswordException()
        {
        }

        public GAZTWrongPasswordException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTAccountLockedFor60MinutesAfterLastLoginAttemptException : GAZTException
    {
        public GAZTAccountLockedFor60MinutesAfterLastLoginAttemptException()
        {
        }

        public GAZTAccountLockedFor60MinutesAfterLastLoginAttemptException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTTaxpayersAccountInActiveWithGAZTException : GAZTException
    {
        public GAZTTaxpayersAccountInActiveWithGAZTException()
        {
        }

        public GAZTTaxpayersAccountInActiveWithGAZTException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTNoTINsAvailableException : GAZTException
    {
        public GAZTNoTINsAvailableException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTSessionExpiredException : GAZTException
    {
        public GAZTSessionExpiredException()
        {
        }

        public GAZTSessionExpiredException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTTaxPayerProfileDataException : GAZTException
    {
        public GAZTTaxPayerProfileDataException()
        {
        }

        public GAZTTaxPayerProfileDataException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTInvalidDataException : GAZTException
    {
        public GAZTInvalidDataException()
        { }

        public GAZTInvalidDataException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTUserCurrentlyInvalidException : GAZTException
    {
        public GAZTUserCurrentlyInvalidException()
        { }

        public GAZTUserCurrentlyInvalidException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTLoginDetailsException : GAZTException
    {
        public GAZTLoginDetailsException()
        { }

        public GAZTLoginDetailsException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTUserNameIncorrectException : GAZTException
    {
        public GAZTUserNameIncorrectException()
        { }

        public GAZTUserNameIncorrectException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTMobileNumberInProfileEmptyException : GAZTException
    {
        public GAZTMobileNumberInProfileEmptyException()
        { }

        public GAZTMobileNumberInProfileEmptyException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

}
