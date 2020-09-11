using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatObjection
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectionsSelectionPageView : ContentPage
    {

        private ObjectionViewModel _viewModel;

        public ObjectionsSelectionPageView()
        {
            InitializeComponent();

            ChangeAeroIcon();
            SetLTR();
        
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            _viewModel = App.Locator.ObjectionsSelectionPageView;

            this.BindingContext = _viewModel;

            _viewModel.AddSelectionOptions();

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

        async void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            ObjectionViewModel.SelectionModel selectedItem = e.AddedItems[0] as ObjectionViewModel.SelectionModel;

            await Task.Delay(1000);
            if (selectedItem.SelectionTitle == AppResources.VatReview)
            {
                _viewModel._navigationService.NavigateTo(App.VatReviewListPageView);

            }
            else if (selectedItem.SelectionTitle == AppResources.ZakatObjection)
            {
                _viewModel._navigationService.NavigateTo(App.ZakatObjectionsListPageView);
            }



        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                _viewModel.AddSelectionOptions();

            }
            catch (Exception ex)
            {
            }
        }
    }
}