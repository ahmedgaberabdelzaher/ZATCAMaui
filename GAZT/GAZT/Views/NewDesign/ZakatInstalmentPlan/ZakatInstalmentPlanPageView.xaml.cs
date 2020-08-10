using System;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentPlanViewModel;
using EGAZT.Views.NewDesign.VATDeclarationPages;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    public partial class ZakatInstalmentPlanPageView : ContentPage
    {



        #region Variable
        ZakatInstalmentPlanViewModel viewModel;
        #endregion

        public ZakatInstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();
                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ZakatInstalmentPlanPageView;
                this.BindingContext = viewModel;


            }
            catch (Exception ex)
            {

            }
            PopupNavigation.Instance.PushAsync(new ZakatInstalmentPlanBottomPopup());


        }




        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            ZakatInstalmentPlanModel selectedItem = e.AddedItems[0] as ZakatInstalmentPlanModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            //            viewModel.ReasonContinueBtnClicked();

            if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 0)
            {
                viewModel.IsZakatSelected = true;
                viewModel.IsIncomeTaxViewEnabled = true;
                viewModel.IsVATAmountVisible = false;
            }
            else
            {
                viewModel.IsZakatSelected = false;
                viewModel.IsIncomeTaxViewEnabled = false;
                viewModel.IsSubIncomeTaxViewEnabled = false;
                viewModel.IsVATAmountVisible = true;

            }

        }
        private void BPickerButtonZakat_Clicked(object sender, EventArgs e)
        {
            FZakatPicker.IsOpen = true;
        }

        private void FPickerZakat_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            CorrespondenceFiltersModel selectedZakat = (CorrespondenceFiltersModel)e.NewValue;
            FZakatPicker.SelectedItem = selectedZakat;
            viewModel.SelectedFilterZakat = selectedZakat;//selectedregion
            viewModel.SelectedFilterZakatPrev = selectedZakat;//selectedregion
            viewModel.TxtSelectedStatusZakat = selectedZakat.Filter;
            viewModel.IsSubIncomeTaxViewEnabled = true;
        }
        private void FPickerZakat_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                FZakatPicker.SelectedItem = viewModel.SelectedFilterZakatPrev;
                viewModel.SelectedFilterZakat = viewModel.SelectedFilterZakatPrev;//selectedregion
                if (viewModel.SelectedFilterZakatPrev == null)
                {
                    viewModel.TxtSelectedStatusZakat = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void SubBPickerButtonZakat_Clicked(object sender, EventArgs e)
        {
            SubFZakatPicker.IsOpen = true;
        }

        private void SubFPickerZakat_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            CorrespondenceFiltersModel selectedZakat = (CorrespondenceFiltersModel)e.NewValue;
            SubFZakatPicker.SelectedItem = selectedZakat;
            viewModel.SubSelectedFilterZakat = selectedZakat;//selectedregion
            viewModel.SubSelectedFilterZakatPrev = selectedZakat;//selectedregion
            viewModel.SubTxtSelectedStatusZakat = selectedZakat.Filter;
        }
        private void SubFPickerZakat_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                SubFZakatPicker.SelectedItem = viewModel.SubSelectedFilterZakatPrev;
                viewModel.SubSelectedFilterZakat = viewModel.SubSelectedFilterZakatPrev;//selectedregion
                if (viewModel.SelectedFilterZakatPrev == null)
                {
                    viewModel.SubTxtSelectedStatusZakat = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void DownPayment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
            viewModel.DownPaymentAmount = args.NewValue;
        }

        private void Installment_ValueChanged(object sender, ValueChangedEventArgs args)
        {
           var newVal = args.NewValue;
            viewModel.NoOfInstalments = Convert.ToInt32(newVal);
        }
        private void SearchItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void Bills_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {


            try
            {
                foreach (ZakatSelectBillModel zakatSelectBillModel in viewModel.SelectedBillsList)
                {
                    zakatSelectBillModel.isSelected = false;
                }

                var dataItem = e.ItemData as ZakatSelectBillModel;
                dataItem.isSelected = true;
            }
            catch (Exception ex)
            {

            }
        }



    }
}
