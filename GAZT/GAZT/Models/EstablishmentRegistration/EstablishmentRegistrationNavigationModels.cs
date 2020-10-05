using System;
using System.Collections.Generic;
using EGAZT.Models.EstablishmentRegistration;

namespace EGAZT.Models
{
    public delegate void ActicityListDelegate(List<Nreg_ActivityItem> list);
    public class OutletNavigationModels
    {
        public EstablishmentRegistrationOutletTabsEnum openedTab { get; set; } = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        public Nreg_IdItem idItem { get; set; } = null;
        public OutletItem selectedOutletItem { get; set; } = null;
        public bool IsEditingMode { get; set; }
    }

    public class ActivityNavigationModels
    {
        public EstablishmentOutletActivitiesTabsEnum openedTab { get; set; } = EstablishmentOutletActivitiesTabsEnum.ActivityList;
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        public OutletNumber nextNumber { get; set; } = null;
        public ValidateCR validateCR { get; set; } = null;
        public Nreg_ActivityItem validateLicense { get; set; } = null;
        public ActicityListDelegate goBackAction { get; set; } = null;
    }
}
