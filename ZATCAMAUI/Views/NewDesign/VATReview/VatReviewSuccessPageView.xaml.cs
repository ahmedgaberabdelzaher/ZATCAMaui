
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.VATReviewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VatReviewSuccessPageView : ContentPage
    {
        public VatReviewViewModel viewModel;
        public VATObjectionSummaryModel modelclass3;
        public VatReviewSuccessPageView(VATObjectionSummaryModel modelclass)
        {
            InitializeComponent();

            ChangeAeroIcon();

            viewModel = App.Locator.VatReviewSuccessView;

            BindingContext = viewModel;
            VATApplicationID.Text = modelclass.d.Fbnumx;
            viewModel.VATReferanceNumber = modelclass.d.Fbnumx;
            modelclass3 = modelclass;

        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources[""];
            }
        }

        private async void Instalment_copy_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.VATReferanceNumber != null)
                {


                    await Clipboard.SetTextAsync(viewModel.VATReferanceNumber);
                    if (Clipboard.HasText)
                    {
                        var text = await Clipboard.GetTextAsync();
                        await viewModel._dialogService.ShowMessageBox(AppResources.NDReferenceNumber + " " + text, AppResources.Copied);

                    }

                }
            }
            catch (Exception)
            {




            }
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsLoading = true;
            });
            if (viewModel.VATReferanceNumber != null)
            {

                string downloadurl = ZATCAConstants.downloadFile + "'" + viewModel.VATReferanceNumber + "')/$value";
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
            MainThread.BeginInvokeOnMainThread(() =>
            {
                viewModel.IsLoading = false;
            });
        }

        private void VatReview_Tapped(object sender, EventArgs e)
        {
            var _navigation = Application.Current.MainPage.Navigation;
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewListPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.VatReviewSuccessPageView)
                {
                    _navigation.RemovePage(item);
                    break;
                }
            }

            viewModel._navigationService.NavigateTo(App.VatReviewListPageView);
        }
    }
}