using System;
using EGAZT.Models.EstablishmentRegistration;

namespace EGAZT.Models
{
    public class OutletNavigationModels
    {
        public EstablishmentRegistrationOutletTabsEnum openedTab { get; set; } = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
    }

    public class ActivityNavigationModels
    {
        public EstablishmentOutletActivitiesTabsEnum openedTab { get; set; } = EstablishmentOutletActivitiesTabsEnum.ActivityList;
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        public OutletNumber nextNumber { get; set; } = null;
        public ValidateCR validateCR { get; set; } = null;
        public Nreg_ActivityItem cRActivityItem { get; set; } = null;
        public Action goBackAction { get; set; } = null;
    }
}
