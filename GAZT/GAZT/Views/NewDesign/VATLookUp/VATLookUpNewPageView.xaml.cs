using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace EGAZT.Views.NewDesign.VATLookUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookUpNewPageView : ContentPage
    {
        bool isMandatoryDataEntered = true;
        VATLookUpNewPageViewModel viewModel;
        public VATLookUpNewPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATLookUpNewPageView;
            this.BindingContext = viewModel;

            ChangeAeroIcon();
            SetLTR();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            viewModel.LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;
        }
     
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
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
        
        void PPicker_btn_Clicked(System.Object sender, System.EventArgs e)
        {
            if (viewModel.IsNameVisible)
                return;

            PPicker.IsOpen = true;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (viewModel.SelectedParameterType != null)
            {
                //PopUp popUp = new PopUp();//SetPlaceholderText();
                //popUp.Message = viewModel.VATACCOrCRNOOrVATCER;
                //if (App.IsArabic)
                //{
                //    popUp.FlowDirections = "RightToLeft";
                //}
                //else
                //{
                //    popUp.FlowDirections = "LeftToRight";
                //}
                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(viewModel.VATACCOrCRNOOrVATCER));

            }
        }

        void PPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType = (VATParameterType)e.NewValue;
            PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
        }
 
    }
}
