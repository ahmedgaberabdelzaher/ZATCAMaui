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
    }

    public class ActivityNavigationModels
    {
        public EstablishmentOutletActivitiesTabsEnum openedTab { get; set; } = EstablishmentOutletActivitiesTabsEnum.ActivityList;
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        public OutletNumber nextNumber { get; set; } = null;
        public ValidateCR validateCR { get; set; } = null;
        public Nreg_ActivityItem validateLicense { get; set; } = null;
        //public List<Nreg_ActivityItem> newActivityItems { get; set; } = new List<Nreg_ActivityItem>();
        public ActicityListDelegate goBackAction { get; set; } = null;
        public bool EditEnabledMode { get; set; } = false;
    }
}
