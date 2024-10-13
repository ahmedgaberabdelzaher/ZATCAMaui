
using ZATCAMAUI.ViewModel.NewDesignViewModel.Common;

namespace ZATCAMAUI.Views.NewDesign.Common.NativeNafath
{
    public partial class NativeConfirmNafathPage : ContentPage
    {
        NativeConfirmNafathPageViewModel viewModel;
        public NativeConfirmNafathPage(string pageName, string NationalIqamaId ,string randomNumber, string TransactionId)
        {

            InitializeComponent();
            viewModel = App.Locator.NativeConfirmNafathPageViewModel;
            BindingContext = viewModel;
            viewModel.RandomNumber = randomNumber;
            viewModel.nationalIqamaId = NationalIqamaId;
            viewModel.transactionId = TransactionId;
            viewModel.pageName = pageName;

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.isCancel = false;
        }
    }
}

