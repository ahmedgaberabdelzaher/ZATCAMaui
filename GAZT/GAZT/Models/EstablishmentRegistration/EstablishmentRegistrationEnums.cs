using System;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public enum OrgResidenceNationalityEstablishmentRegistrationEnum
    {
        StayMoreThanKSA = 1,
        RentOwnhouseMoreThanThirtyDays = 2,
        NoneOfTheAbove = 3
    }
    [Preserve(AllMembers = true)]
    public enum OrgNonResidentEstablishmentRegistrationEnum
    {
        PermanentEstablishment = 1,
        OtherTaxIncomeFromSourceWithInTheSKA = 2
    }
    [Preserve(AllMembers = true)]
    public enum OrgNonResidentOptionsEstablishmentEnum
    {
        ABranchOfNonResidentCompanyPE = 1,
        ConstructionSitePE = 2,
        InstallationPE = 3,
        AFixedBasePE = 4,
        NonResidentPartnerPE = 5
    }
    [Preserve(AllMembers = true)]
    public enum EstablishmentRegistrationOutletTabsEnum
    {
        OutletDetail,
        ActivityDetails,
        AddressDetails ,
        NewOutlet 
    }
    [Preserve(AllMembers = true)]
    public enum EstablishmentOutletActivitiesTabsEnum
    {
        CRDetails = 1,
        ActivityList = 2,
        LicenseDetails = 3
    }
}
