using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignShowVatInformationPopUpPageView : PopupPage
    {
        public GAZTNewDesignShowVatInformationPopUpPageViewModel viewModel;
        public GAZTNewDesignShowVatInformationPopUpPageView(NewDesignPopUp newDesignPopData)
        {
            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignShowVatInformationPopUpPageView;
            this.BindingContext = viewModel;
            SetLTR();
            FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            ClearData();
            if (newDesignPopData != null)
            {
                viewModel.NewDesignPopUp = newDesignPopData;
                viewModel.MainString = viewModel.NewDesignPopUp.MainHeader;
                if (viewModel.NewDesignPopUp.HeaderWithInfos != null && viewModel.NewDesignPopUp.HeaderWithInfos.Count != 0)
                {
                    viewModel.HeaderWithInfoList = viewModel.NewDesignPopUp.HeaderWithInfos;
                    LoadLink();
                }

            }
            SetMargin();
        }
        public void SetMargin()
        {
            if (viewModel.NewDesignPopUp != null && viewModel.NewDesignPopUp.HeaderWithInfos != null && viewModel.NewDesignPopUp.HeaderWithInfos.Count != 0)
            {
                if (viewModel.NewDesignPopUp.HeaderWithInfos.Count < 2)
                {
                    MainPanCakeView.Margin = new Thickness(0, 300, 0, 0);
                }
                else
                {
                    MainPanCakeView.Margin = new Thickness(0, 130, 0, 0);
                }
            }
            else
            {
                MainPanCakeView.Margin = new Thickness(0, 130, 0, 0);
            }
        }

        public void ClearData()
        {
            viewModel.FirstLink = string.Empty;
            viewModel.FirstLinkText = string.Empty;
            viewModel.SecondLink = string.Empty;
            viewModel.SecondLinkText = string.Empty;
        }
        public void LoadLink()
        {
            int i = 0;
            foreach (var item in viewModel.HeaderWithInfoList)
            {
                if (item.IsLinkAvailable)
                {
                    if (i == 0)
                    {
                        viewModel.FirstLink = item.Link;
                        viewModel.FirstLinkText = item.LinkText;
                        i++;
                    }
                    else
                    {
                        viewModel.SecondLink = item.Link;
                        viewModel.SecondLinkText = item.LinkText;
                    }
                }
            }
        }
        private void SetLTR()
        {
            try
            {
                if (App.IsArabic)
                {
                    //this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
                }
                else
                {
                    //this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
                }
            }
            catch (Exception)
            {


            }
        }

        private void FirstLinkClicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri(viewModel.FirstLink));
        }

        private void SecondLinkClicked(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri(viewModel.SecondLink));
        }

        private void DoneButtonClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}