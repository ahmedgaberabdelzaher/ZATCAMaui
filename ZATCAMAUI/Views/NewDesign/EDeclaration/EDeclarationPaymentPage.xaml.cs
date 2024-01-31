using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration
{
    public partial class EDeclarationPaymentPage : ContentPage
    {
        EDeclarationPaymentViewModel viewModel;
        public EDeclarationPaymentPage(TravelerDeclarationResponse travelerDeclarationResponse)
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationPaymentViewModel;
            viewModel.TravelerDeclarationResponse = travelerDeclarationResponse;
            var moneyToWordConverter = new NumberToWord((decimal)travelerDeclarationResponse.totalFees, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            viewModel.PriceText = App.IsArabic ? moneyToWordConverter.ConvertToArabic() : moneyToWordConverter.ConvertToEnglish();
            BindingContext = viewModel;
        }
    }
}

