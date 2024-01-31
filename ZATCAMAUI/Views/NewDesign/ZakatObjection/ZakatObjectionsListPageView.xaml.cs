using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models.ZakatObjectionsModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatObjectionViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatObjection
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionsListPageView : ContentPage
    {
        ZakatObjectionsListViewModel viewModel;
        public ZakatObjectionsListPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
            viewModel = App.Locator.ZakatObjectionListView;
            BindingContext = viewModel;
            viewModel.ResetData();
            _ = viewModel.ZAKATObjectionList();


        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

            }
            catch (Exception)
            {


            }
        }

        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
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

        public async void Objection_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            //string formguid = "005056B1F8FB1EDABC99B9AFD7873DBB"; //string.Empty;
            //string euser = "00000010000008327086"; //string.Empty;

            try
            {
                var item = e.DataItem as ZakatObjectionListModel.Result;
                Preferences.Set("ZakatObjectionSelectedValue", item.Fbnum);
                Preferences.Set("ZakatObjectionSelectedType", item.Fbtyp);




                if (item.Fbtyp == "TP09")
                {
                    await viewModel.GetWithdrawReviewReason(item.Fbnum);
                }
                else if (item.Fbtyp == "TP10")
                {
                    await viewModel.GetWithdrawReviewReasonTP10(item.Fbnum);
                }
                else if (item.Fbtyp == "ZNOB" && (item.StatText == "Additional Info Requested" || item.StatText == "طلب معلومات اضافية"))
                {
                    await viewModel.showRejectPopup();
                }
                else
                {
                    viewModel.ReqInstalmentBtnClicked();
                }




            }
            catch (Exception)
            {

            }







        }
    }


}