using System;
using System.Linq;
using EGAZT.Models;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    [Preserve(AllMembers = true)]
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
            ChangeAeroIcon();
            SetLTR();
        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            viewModel?.OnAppearing();
            if(!viewModel.PostalAsPhysical)
            {
                viewModel.PostalAddressVisibility = true;
            }
            else
            {
                viewModel.PostalAddressVisibility = false;

            }
        }

        void SfChipGroup_SelectionChanged(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                var index = OutletTabSfChipGroup.ItemsSource.IndexOf(e.AddedItem);
                OutletTabScrollView.ScrollToAsync(OutletTabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index), ScrollToPosition.MakeVisible, true);
            }
            catch (Exception) {

                
                
            }
        }

        void OutletTabSfChipGroup_SelectionChanging(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangingEventArgs e)
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
            catch (Exception) {

                
                
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

        void SfChipGroup_SelectionChanging(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
