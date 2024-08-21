
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
            viewModel = App.Locator.ZakatObjectionListView;
            BindingContext = viewModel;
            viewModel.ResetData();
            _ = viewModel.ZAKATObjectionList();


        }
       

        public async void Objection_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
           

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