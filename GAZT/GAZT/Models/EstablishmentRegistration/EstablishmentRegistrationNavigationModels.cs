using System;
namespace EGAZT.Models
{
    public class OutletNavigationModels
    {
        public EstablishmentRegistrationOutletTabsEnum openedTab { get; set; } = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
    }

    public class ActivityNavigationModels
    {
        public EstablishmentOutletActivitiesTabsEnum openedTab { get; set; } = EstablishmentOutletActivitiesTabsEnum.ActivityList;
    }
}
