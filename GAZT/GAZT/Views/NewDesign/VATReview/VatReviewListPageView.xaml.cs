using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.VatReview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class VatReviewListPageView : ContentPage
    {
        private VatReviewListViewModel _viewModel;
        
        public VatReviewListPageView()
        {
            InitializeComponent();
            
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            _viewModel = App.Locator.VatReviewListView;

            this.BindingContext = _viewModel;

            _viewModel.ResetListData();

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

        private void Reviews_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _viewModel.EnableSummaryView();
        }
    }
}