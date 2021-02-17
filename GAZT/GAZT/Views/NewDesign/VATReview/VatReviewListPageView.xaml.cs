using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Models.VatReviewModel;
using EGAZT.ViewModel.NewDesignViewModel.VatReviewViewModel;
using GAZT.Manager;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.VatReview
{
    [Preserve(AllMembers = true)]
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

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            _viewModel = App.Locator.VatReviewListView;

            this.BindingContext = _viewModel;

            _viewModel.ResetListData();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            _viewModel.isNewRequestCreated = false;
            await _viewModel.VATObjectionList();

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

        private async void Reviews_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //string formguid = "005056B1F8FB1EDABC99B9AFD7873DBB"; //string.Empty;
            //string euser = "00000010000008327086"; //string.Empty;

            var item = e.ItemData as VATObjectionListModel.Result3;


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

        private async void BankGuranAttachTapped(object sender, ItemTappedEventArgs e)
        {
            await Task.Run(() =>
            {
                _viewModel.IsLoading = true;
            });
            var attachment = e.ItemData as Attachment;
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

        private async void Attachments_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Task.Run(() =>
            {
                _viewModel.IsLoading = true;
            });
            var attachment = e.ItemData as Attachment;

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
       
    }
}