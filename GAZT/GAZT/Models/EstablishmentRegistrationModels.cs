using System;
namespace EGAZT.Models
{
    public enum EstablishmentRegistrationTabsEnum
    {
        RegistrationType = 1,
        TaxpayerDetail = 2,
        Outlets = 3,
        FinancialDetail = 4,
        Declaration = 5
    }

    public enum EstablishmentRegistrationNationalityEnum
    {
        StayMoreThanKSA = 1,
        RentOwnhouseMoreThanThirtyDays = 2,
        NoneOfTheAbove = 3
    }

    public enum EstablishmentRegistrationLegalEntityEnum
    {
        PermanentEstablishment = 1,
        OtherTaxIncomeFromSourceWithInTheSKA = 2
    }

    public enum EstablishmentRegistrationParmanentEstablishmentEnum
    {
        ABranchOfNonResidentCompanyPE = 1,
        ConstructionSitePE = 2,
        InstallationPE = 3,
        AFixedBasePE = 4,
        NonResidentPartnerPE = 5
    }

    public enum EstablishmentRegistrationTaxableIncomeSourceTypeEnum
    {
        ABranchOfNonResidentCompanyPE = 1,
        ConstructionSitePE = 2,
        InstallationPE = 3,
        AFixedBasePE = 4,
        NonResidentPartnerPE = 5
    }
}
