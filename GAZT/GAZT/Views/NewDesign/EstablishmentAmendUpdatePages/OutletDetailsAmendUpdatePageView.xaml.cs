using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using EGAZT.Views.NewDesign.Common;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.EstablishmentAmendUpdatePages
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
            SetLTR();
            viewModel.SetUIAvailability();
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
                //Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                //Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel?.OnAppearing();
            MessagingCenter.Subscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse", (obj, res) =>
            {
                PopupNavigation.Instance.PopAsync();
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SingleButtonPopupView, bool>(this, "SingleButtonPopupResponse");
        }
        void SfChipGroup_SelectionChanged(System.Object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangedEventArgs e)
        {
            try
            {
                var index = OutletTabSfChipGroup.ItemsSource.IndexOf(e.AddedItem);
                OutletTabScrollView.ScrollToAsync(OutletTabSfChipGroup.ChipLayout.Children.ElementAtOrDefault(index), ScrollToPosition.MakeVisible, true);
            }
            catch (Exception) { }
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
            catch (Exception) { }
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

        private async void OutletType_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet(AppResources.SelectOutletType, null, null, viewModel.ListOutletTypes.ToArray());

            if (result != null && result == AppResources.ESTMainOutlet && viewModel.ListOutlets != null && viewModel.ListOutlets.Count > 0 && viewModel.ListOutlets.Exists(x=>x.Actcat =="M"))
            {
                await PopupNavigation.PushAsync(new SingleButtonPopupView(AppResources.OKText, AppResources.SelectOutletTypeError));
                return;
            }

            else if (result != null && result != AppResources.OKText)
            {
                viewModel.SelectedOutletType = result;
            }
        }
    }
}
