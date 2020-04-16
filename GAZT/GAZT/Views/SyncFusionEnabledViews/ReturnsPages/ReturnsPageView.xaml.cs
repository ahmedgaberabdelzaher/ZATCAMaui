using GAZT.ViewModel.SyncFusionEnabledViewModel.ReturnsPageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.SyncFusionEnabledViews.ReturnsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReturnsPageView : ContentPage
    {
        ReturnsPageViewModel viewModel;
        public ReturnsPageView(int Index)
        {
            try
            {
                InitializeComponent();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                viewModel = App.Locator.ReturnsPageView;
                this.BindingContext = viewModel;
                OnPageLoad();
                viewModel.TabIndexStatus = Index;
                SetLTR();
                ChangeAeroIcon();
                ICRList.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem == null)
                    {
                        return;
                    } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                };
                ICRListNon.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem == null)
                    {
                        return;
                    } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                };
                ICRListOver.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem == null)
                    {
                        return;
                    } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                };
                //ZakatICRListNonSubmitted.ItemSelected += (sender, e) =>
                //{
                //    if (e.SelectedItem == null)
                //    {
                //        return;
                //    } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                //};
                //ZakatICRListOverDue.ItemSelected += (sender, e) =>
                //{
                //    if (e.SelectedItem == null)
                //    {
                //        return;
                //    } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                //};
                //ZakatICRListSubmitted.ItemSelected += (sender, e) =>
                //{
                //    if (e.SelectedItem == null)
                //    {
                //        return;
                //    } ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                //};
            }
            catch (Exception ex)
            {

            }
        }
        public async Task OnPageLoad()
        {
            try
            {
               await Task.Run(() =>
                {
                    viewModel.IsLoading = true;
                });


                await Task.Run(async () =>
                {
                    await viewModel.onPageLoad();
                });
                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
            catch(Exception ex)
            {

            }
        }
        private void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    this.Content = null;
        //}
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                viewModel.FDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                viewModel.FDirection = FlowDirection.RightToLeft;
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
    }
}