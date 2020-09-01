using EGAZT;
using GAZT;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static GAZT.ErrorMessage;

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
            if (App.TP != null)
            {
                App.TP = null;
                App.IsSessionExpired = true;
                App.Token = String.Empty;
            }
        }
        public GAZTSessionExpiredException(string ExceptionMessage) : base(ExceptionMessage)
        {
            if (App.TP != null)
            {
                App.TP = null;
                App.IsSessionExpired = true;
                App.Token = String.Empty;
            }
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
    public class GAZTRegistrationPendingException : GAZTException
    {
        public GAZTRegistrationPendingException()
        { }
        public GAZTRegistrationPendingException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTVATRegistrationInProcessException : GAZTException
    {
        public GAZTVATRegistrationInProcessException()
        { }
        public GAZTVATRegistrationInProcessException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTUnlockAccountException : GAZTException
    {
        public GAZTUnlockAccountException()
        {
        }
        public GAZTUnlockAccountException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTErrorException : GAZTException
    {
        public GAZTErrorException()
        { }
        public GAZTErrorException(string ExceptionMessage) : base(ExceptionMessage)
        { }


    }

    public class GAZTVATInstalmentException : GAZTException
    {
        public GAZTVATInstalmentException()
        { }
        public GAZTVATInstalmentException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }
    public class GAZTVATChangeFillingPeriodException : GAZTException
    {
        public GAZTVATChangeFillingPeriodException()
        { }
        public GAZTVATChangeFillingPeriodException(string ExceptionMessage) : base(ExceptionMessage)
        {
        }
    }

    public class GAZTTinDeregistrationErrorException : GAZTException
    {
        public GAZTTinDeregistrationErrorException()
        { }
        public GAZTTinDeregistrationErrorException(string ExceptionMessage) : base(ExceptionMessage)
        { }
        public GAZTTinDeregistrationErrorException(ErrorObj errorObj)
        { }
    }
}