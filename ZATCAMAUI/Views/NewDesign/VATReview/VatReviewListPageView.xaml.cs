

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATReviewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATReviewViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATReview
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class VatReviewListPageView : ContentPage
    {
        private VatReviewListViewModel _viewModel;

        public VatReviewListPageView()
        {
            InitializeComponent();

            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            _viewModel = App.Locator.VatReviewListView;

            BindingContext = _viewModel;

            _viewModel.ResetListData();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            _viewModel.isNewRequestCreated = false;
            await _viewModel.VATObjectionList();

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

        private async void Reviews_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {

            try
            {
                var item = e.DataItem as VATObjectionListModel.Result3;


                if (item.Fbust == "E0013")
                {

                    App.selectedVATItem = item.Fbnum;
                    App.selectedVATItemFbust = item.Fbust;
                    if (!_viewModel.isNewRequestCreated)
                    {
                        _viewModel.isNewRequestCreated = true;
                        _viewModel._navigationService.NavigateTo(App.VatReviewPageView);
                    }
                }
                else
                {

                    var index = _viewModel.VATobjListViewData.IndexOf(item);
                    _viewModel.EnableSummaryView();
                    await _viewModel.OnPageLoad1(index);

                }
            }
            catch (Exception)
            {


            }

        }

        private async void BankGuranAttachTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    _viewModel.IsLoading = true;
                });
                var attachment = e.DataItem as Attachment;
                //if (attachment.Filename.Contains(".")) ;
                string Extention = attachment.Filename.Split('.')[1];
                if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                {
                    if (attachment.DocUrl != null)
                    {
                        _viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                    }
                }
                else
                {


                    await GetVATReviewWebServiceManager.email(attachment.Doguid, attachment);
                }

                await Task.Run(() =>
                {
                    _viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


            }
        }

        private async void Attachments_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                await Task.Run(() =>
                {
                    _viewModel.IsLoading = true;
                });
                var attachment = e.DataItem as Attachment;

                //if (attachment.Filename.Contains(".")) ;
                string Extention = attachment.Filename.Split('.')[1];
                if (Extention.Equals("PDF") || Extention.Equals("pdf"))
                {
                    if (attachment.DocUrl != null)
                    {
                        _viewModel._navigationService.NavigateTo(App.PdfView, attachment.DocUrl);
                    }
                }
                else
                {

                    await GetVATReviewWebServiceManager.email(attachment.Doguid, attachment);
                }



                await Task.Run(() =>
                {
                    _viewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {


            }
        }

    }
}