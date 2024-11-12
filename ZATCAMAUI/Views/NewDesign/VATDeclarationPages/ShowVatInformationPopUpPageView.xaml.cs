using System.Collections.ObjectModel;
using Mopups.Pages;
using Mopups.Services;
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
            try
            {
                InitializeComponent();
                viewModel = App.Locator.GAZTNewDesignShowVatInformationPopUpPageView;
                this.BindingContext = viewModel;
                ClearData();
                if (newDesignPopData != null)
                {
                    viewModel.NewDesignPopUp = newDesignPopData;
                    viewModel.MainString = viewModel.NewDesignPopUp.MainHeader;
                    if (viewModel.NewDesignPopUp.HeaderWithInfos != null && viewModel.NewDesignPopUp.HeaderWithInfos.Count != 0)
                    {
                        viewModel.HeaderWithInfoList = new ObservableCollection<HeaderWithInfo>(viewModel.NewDesignPopUp.HeaderWithInfos);
                        LoadLink();
                    }

                }
            }
            catch(Exception)
            {
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
            MopupService.Instance.PopAsync();
        }
    }
}