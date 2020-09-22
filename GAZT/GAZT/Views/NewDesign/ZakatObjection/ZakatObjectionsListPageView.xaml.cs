using EGAZT.Models.ZakatObjectionsModel;
using EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZAKATObjectionPages
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
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.ZakatObjectionListView;
            this.BindingContext = viewModel;
            viewModel.ResetData();
            viewModel.ZAKATObjectionList();
            

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

        public void Objection_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //string formguid = "005056B1F8FB1EDABC99B9AFD7873DBB"; //string.Empty;
            //string euser = "00000010000008327086"; //string.Empty;



            var item = e.ItemData as ZakatObjectionListModel.Result;
            Preferences.Set("ZakatObjectionSelectedValue", item.Fbnum);
            Preferences.Set("ZakatObjectionSelectedType", item.Fbtyp);

            
            if(item.Fbtyp == "TP09") {

                viewModel.GetWithdrawReviewReason(item.Fbnum);

            }
            else {
                viewModel.ReqInstalmentBtnClickedAsync();
            }



             



        }
    }


}