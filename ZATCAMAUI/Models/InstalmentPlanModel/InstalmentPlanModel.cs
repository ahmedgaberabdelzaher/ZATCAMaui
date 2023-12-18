namespace ZATCAMAUI.Models.InstalmentPlanModel
{

    public class InstalmentPlanModel
    {
        public InstalmentPlanModel()
        {
        }
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
        public string CardLabel { get => ActiveOutletDecisionOptions; }
        public string UnSelectedCardIcon { get; set; }
        public string SelectedCardIcon { get; set; }
        public bool IsSelectedCardIconVisible { get => !string.IsNullOrEmpty(SelectedCardIcon) && !string.IsNullOrWhiteSpace(SelectedCardIcon); }
        public bool IsUnSelectedCardIconVisible { get => !string.IsNullOrEmpty(UnSelectedCardIcon) && !string.IsNullOrWhiteSpace(UnSelectedCardIcon); }

    }
}
