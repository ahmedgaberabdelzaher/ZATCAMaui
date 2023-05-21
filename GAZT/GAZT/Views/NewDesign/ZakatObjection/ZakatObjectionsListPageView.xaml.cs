using EGAZT.Models.ZakatObjectionsModel;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZAKATObjectionPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatObjectionsListPageView : ContentPage
    {
        ZakatObjectionsListViewModel viewModel;
        public ZakatObjectionsListPageView()
        {
            InitializeComponent();
            ChangeAeroIcon();
            SetLTR();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;
            viewModel = App.Locator.ZakatObjectionListView;
            this.BindingContext = viewModel;
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
                this.Padding = safeInsets;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
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

        public async void Objection_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //string formguid = "005056B1F8FB1EDABC99B9AFD7873DBB"; //string.Empty;
            //string euser = "00000010000008327086"; //string.Empty;

            try
            {
                var item = e.ItemData as ZakatObjectionListModel.Result;
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
            catch (Exception )
            {

            }







        }
    }


}