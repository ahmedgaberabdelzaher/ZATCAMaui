using EGAZT.ViewModel.SyncFusionEnabledViewModel.ReturnsListCountsByStatus_ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.ReturnsPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReturnsPageView : ContentPage
    {
        ReturnsPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public ReturnsPageView(int Index)
        {
            try
            {
                InitializeComponent();
            //    On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
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
        protected override void OnSizeAllocated(double width, double height)
        {
            //base.OnSizeAllocated(width, height); //must be called
            //if (this.width != width || this.height != height)
            //{
            //    this.width = width;
            //    this.height = height;
            //    if (App.IsArabic)
            //    {
            //        if (width > height)
            //        {
            //            // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
            //            var safeInsets = On<iOS>().SafeAreaInsets();
            //            ICRList.Margin = new Thickness(0, 5, 60, 0);
            //            ZakatICRListSubmitted.Margin = new Thickness(0, 5, 60, 0);
            //            ICRListNon.Margin = new Thickness(0, 5, 60, 0);
            //            ZakatICRListOverDue.Margin = new Thickness(0, 5, 60, 0);
            //            ZakatICRListNonSubmitted.Margin = new Thickness(0, 5, 60, 0);
            //            ICRListOver.Margin = new Thickness(0, 5, 60, 0);
            //            //BPicker.Margin = new Thickness(20, 0, 60, 0);
            //            //FrmLicenseIssuedBy.Margin = new Thickness(20, 0, 80, 5);
            //            //safeInsets.Left = 80;
            //            //safeInsets.Right = 80;
            //           // Padding = safeInsets;
            //        }
            //        else
            //        {
            //            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            //            ICRList.Margin = new Thickness(0, 5, 60, 0);
            //            ZakatICRListSubmitted.Margin = new Thickness(0, 5, 0, 0);
            //            ICRListNon.Margin = new Thickness(0, 5, 0, 0);
            //            ZakatICRListOverDue.Margin = new Thickness(0, 5, 0, 0);
            //            ZakatICRListNonSubmitted.Margin = new Thickness(0, 5, 0, 0);
            //            ICRListOver.Margin = new Thickness(0, 5, 0, 0);
            //        }
            //    }
            //    //reconfigure layout
            //}
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