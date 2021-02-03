using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatObjection
{
    [Preserve(AllMembers = true)]
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
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

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
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

        async void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            ObjectionViewModel.SelectionModel selectedItem = e.AddedItems[0] as ObjectionViewModel.SelectionModel;

            await Task.Delay(1000);
            if (selectedItem.SelectionTitle == AppResources.DBSMVATObjection)
            {
                _viewModel._navigationService.NavigateTo(App.VatReviewListPageView);

            }
            else if (selectedItem.SelectionTitle == AppResources.DBSMZAKATObjection)
            {
                _viewModel._navigationService.NavigateTo(App.ZakatObjectionsListPageView);
            }



        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;

                _viewModel.AddSelectionOptions();

            }
            catch (Exception)
            {
            }
        }
    }
}