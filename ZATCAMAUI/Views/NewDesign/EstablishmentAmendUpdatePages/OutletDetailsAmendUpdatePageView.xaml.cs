using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages
{
 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OutletDetailsAmendUpdatePageView : ContentPage
    {
        private OutletDetailsAmendUpdatePageViewModel viewModel;
        public OutletDetailsAmendUpdatePageView(OutletNavigationModels outletNavigation)
        {
            InitializeComponent();
            //_outletNavigation = outletNavigation;
            viewModel = App.Locator.OutletDetailsAmendUpdatePageView;
            viewModel.taxPayerDetails = outletNavigation.taxPayerDetails;
            viewModel.idItem = outletNavigation.idItem;
            viewModel.selectedOutletItem = outletNavigation.selectedOutletItem;
            viewModel.IsEditingMode = outletNavigation.IsEditingMode;
            viewModel.currentTab = outletNavigation.openedTab;

            BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel.SetUIAvailability();
            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
            {

                viewModel.PickerModel = arg;
            });
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
            Padding = safeInsets;
            viewModel?.OnAppearing();
            MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", (obj, res) =>
            {
                MopupService.Instance.PopAsync();
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse");
        }
        void SfChipGroup_SelectionChanged(object sender, Syncfusion.Maui.Core.Chips.SelectionChangingEventArgs e)
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

        private async void OutletType_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayActionSheet(AppResources.SelectOutletType, null, null, viewModel.ListOutletTypes.ToArray());

                if (result != null && result == AppResources.ESTMainOutlet && viewModel.ListOutlets != null && viewModel.ListOutlets.Count > 0 && viewModel.ListOutlets.Exists(x => x.Actcat == "M"))
                {
                    await MopupService.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZZZOkayText, AppResources.SelectOutletTypeError, string.Empty));
                    return;
                }

                else if (result != null && result != AppResources.ZZZOkayText)
                {
                    viewModel.SelectedOutletType = result;
                }
            }
            catch (Exception)
            {


            }
        }
    }
}
