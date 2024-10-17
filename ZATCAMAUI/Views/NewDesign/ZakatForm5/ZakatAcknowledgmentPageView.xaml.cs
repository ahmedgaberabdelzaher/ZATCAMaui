
using ZATCAMAUI.Models.Form5Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.ZakatForm5
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatAcknowledgmentPageView : ContentPage
    {
        ZakatAcknowledgmentPageViewModel viewModel;

        public ZakatAcknowledgmentPageView(List<Result_9> AknowledgementList)
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatAcknowledgmentPageView;
            BindingContext = viewModel;
            viewModel.AknowledgementDataList = AknowledgementList;
            viewModel.LoadZakatForm5_ACK_Data();
        }
    }
}