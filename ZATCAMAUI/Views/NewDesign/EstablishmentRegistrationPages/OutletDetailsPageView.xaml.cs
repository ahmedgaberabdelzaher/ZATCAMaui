
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages
{

    public partial class OutletDetailsPageView : ContentPage
    {
        private OutletDetailsPageViewModel viewModel;
        public OutletDetailsPageView(OutletNavigationModels outletNavigation)
        {
            InitializeComponent();
            viewModel = App.Locator.OutletDetailsPageView;
            viewModel.ClearData();
            viewModel.taxPayerDetails = outletNavigation.taxPayerDetails;
            viewModel.idItem = outletNavigation.idItem;
            viewModel.selectedOutletItem = outletNavigation.selectedOutletItem;
            viewModel.currentTab = outletNavigation.openedTab;
            BindingContext = viewModel;
            if (outletNavigation.selectedOutletItem.MciEntry == "X" || !string.IsNullOrEmpty(outletNavigation.selectedOutletItem.Actnm))
            {
                viewModel.isEditable = false;
            }
            else
            {
                viewModel.isEditable = true;
            }
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!viewModel.PostalAsPhysical)
            {
                viewModel.PostalAddressVisibility = true;
            }
            else
            {
                viewModel.PostalAddressVisibility = false;

            }
        }

        void SfChipGroup_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangedEventArgs e)
        {
            try
            {
                var index = OutletTabSfChipGroup.ItemsSource.IndexOf(e.AddedItem);
                //TODO
                var view = (Element)OutletTabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index);
                OutletTabScrollView.ScrollToAsync(view, ScrollToPosition.MakeVisible, true);
            }
            catch (Exception)
            {



            }
        }

        void OutletTabSfChipGroup_SelectionChanging(object sender, Syncfusion.Maui.Core.Chips.SelectionChangingEventArgs e)
        {
            try
            {
                var tt = e.AddedItem as string;
                EstablishmentRegistrationOutletTabsEnum newselectedTab = getEnumFromChipsLabel(tt);
                if (!string.IsNullOrWhiteSpace(tt) && (int)newselectedTab >= (int)viewModel?.currentTab)
                {
                    e.Cancel = true;
                }
                else
                {
                    viewModel.currentTab = newselectedTab;
                }
            }
            catch (Exception)
            {



            }
        }

        private EstablishmentRegistrationOutletTabsEnum getEnumFromChipsLabel(string label)
        {
            if (label.Equals(AppResources.ESTActivityDetails))
            {
                return EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
            }
            if (label.Equals(AppResources.ESTAddressDetails))
            {
                return EstablishmentRegistrationOutletTabsEnum.AddressDetails;
            }
            return EstablishmentRegistrationOutletTabsEnum.OutletDetail;
        }

        void SfChipGroup_SelectionChanging(object sender, Syncfusion.Maui.Core.Chips.SelectionChangingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
