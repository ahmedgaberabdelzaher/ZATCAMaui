using System;
namespace EGAZT.Models
{
    public enum EstablishmentRegistrationTabsEnum
    {
        Unknown = -1,
        RegistrationType = 1,
        TaxpayerDetail = 2,
        PassportDetails = 3,
        Outlets = 4,
        FinancialDetail = 5,
        Declaration = 6
    }

    public enum OrgResidenceNationalityEstablishmentRegistrationEnum
    {
        StayMoreThanKSA = 1,
        RentOwnhouseMoreThanThirtyDays = 2,
        NoneOfTheAbove = 3
    }

    public enum OrgNonResidentEstablishmentRegistrationEnum
    {
        PermanentEstablishment = 1,
        OtherTaxIncomeFromSourceWithInTheSKA = 2
    }

    public enum OrgNonResidentOptionsEstablishmentEnum
    {
        ABranchOfNonResidentCompanyPE = 1,
        ConstructionSitePE = 2,
        InstallationPE = 3,
        AFixedBasePE = 4,
        NonResidentPartnerPE = 5
    }

    public enum EstablishmentRegistrationOutletTabsEnum
    {
        OutletDetail,
        ActivityDetails,
        AddressDetails ,
        NewOutlet 
    }

    public enum EstablishmentOutletActivitiesTabsEnum
    {
        CRDetails = 1,
        ActivityList = 2,
        LicenseDetails = 3
    }
}
