
using ZATCAMAUI.Models.Form5Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using Application = Microsoft.Maui.Controls.Application;
using Page = Microsoft.Maui.Controls.Page;

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
            IntialiseAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            Task.Run(() =>
            {
                LoadData();

            });
        }
        private void LoadData()
        {
            
            viewModel.LoadZakatForm5_ACK_Data();
            
        }
        public void IntialiseAsync()
        {
           
            viewModel.LoadZakatForm5_ACK_Data();
           
        }

      
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void OnFinishedTapped(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 0)
            {
                Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
            }
            viewModel._navigationService.GoBack();
        }
    }
}