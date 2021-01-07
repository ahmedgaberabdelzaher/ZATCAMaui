using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.EnumModels
{
    [Preserve(AllMembers = true)]
    public enum EstablishmentSignUPTabEnum
    {
        TermsAndConditions=1,
        IndividualInformation = 2,
        BusinessInformation = 3,
        ContactInformation = 4,
        MobileVerification = 5,
        EmailVerification = 6,
        Password = 7
    }
}
