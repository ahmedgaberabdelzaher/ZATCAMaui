using System;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.EstablishmentRegistrationPages
{
    public partial class OutletDetailsPageView : ContentPage
    {
        private OutletDetailsPageViewModel viewModel;
        //private OutletNavigationModels _outletNavigation;
        public OutletDetailsPageView(OutletNavigationModels outletNavigation)
        {
            InitializeComponent();
            //_outletNavigation = outletNavigation;
            viewModel = App.Locator.OutletDetailsPageView;
            viewModel.taxPayerDetails = outletNavigation.taxPayerDetails;
            viewModel.idItem = outletNavigation.idItem;
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
    }
}
