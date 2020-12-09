using EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.TAXEvasionPages
{
    [Preserve(AllMembers = true)]
    public partial class TaxEvasionMyReportsListPageView : ContentPage
    {
        TaxEvasionMyReportsListPageViewModel viewModel;
        public TaxEvasionMyReportsListPageView(string mobno)
        {
            InitializeComponent();

            viewModel = App.Locator.TaxEvasionMyReportsListPageView;
            this.BindingContext = viewModel;

            viewModel.PopulateDataInChips();
            viewModel.SelectedChipFilterItemList = new List<ChipModel>();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            SetLTR();


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                ChangeAeroIcon();

                await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });
          
                await viewModel.OnPageLoad();
                viewModel.FilterOnbasisOfChipSelectedItem();

                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        //    try
        //    {
        //        var _navigation = Xamarin.Forms.Application.Current.MainPage.Navigation;
        //        foreach (var item in _navigation.NavigationStack)
        //        {
        //            if (item.GetType().Name == App.NewTaxEvasionFormPageView)
        //            {
        //                _navigation.RemovePage(item);
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    { 
            
        //    }
            
        }
        
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
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

        private async void ChipGroup_statusFilter_SelectionChanging(object sender, Syncfusion.Buttons.XForms.SfChip.SelectionChangingEventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            ChipModel addeditemtype = (ChipModel)e.AddedItem;
            ChipModel removedItem = (ChipModel)e.RemovedItem;

            if (addeditemtype != null)
            {
                viewModel.SelectedChipFilterItemList.Add(addeditemtype);
            }
            if (removedItem != null)
            {
                viewModel.SelectedChipFilterItemList.Remove(removedItem);
            }
            viewModel.FilterOnbasisOfChipSelectedItem();
            await Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private void ListView_TaxEvasionReportList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            return;
        }
        protected override bool OnBackButtonPressed() => true;
        private async  void AddReport_Tapped(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                viewModel.IsLoading = true;

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                viewModel._navigationService.NavigateTo(App.NewTaxEvasionFormPageView);

            });
           
        }
    }
}
