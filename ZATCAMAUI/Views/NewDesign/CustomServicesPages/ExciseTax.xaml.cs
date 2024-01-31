using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages
{

    public partial class ExciseTax : ContentPage
    {
        ExciseTaxViewModel viewModel;
        public ExciseTax()
        {
            viewModel = App.Locator.exciseTaxViewModel;
            BindingContext = viewModel;
            InitializeComponent();
            SetLTR();
            On<iOS>().SetUseSafeArea(true);
            App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

        }
        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;


            }
            else
            {
                FlowDirection = FlowDirection.RightToLeft;


                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
        }

    }
}
