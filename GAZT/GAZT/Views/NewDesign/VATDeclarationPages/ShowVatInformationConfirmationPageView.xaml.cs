using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowVatInformationConfirmationPageView : PopupPage
    {
        public ShowVatInformationConfirmationPageViewModel viewModel;
        public ShowVatInformationConfirmationPageView(NewDesignPopUp newDesignPopData)
        {
            InitializeComponent();
            viewModel = App.Locator.ShowVatInformationConfirmationPageView;
            this.BindingContext = viewModel;
            SetLTR();
            if (newDesignPopData != null)
            {
                viewModel.NewDesignPopUp = newDesignPopData;
                viewModel.MainString = viewModel.NewDesignPopUp.MainHeader;
                if (viewModel.NewDesignPopUp.HeaderWithInfos != null && viewModel.NewDesignPopUp.HeaderWithInfos.Count != 0)
                {
                    viewModel.HeaderWithInfoList = viewModel.NewDesignPopUp.HeaderWithInfos;
                    //LoadLink();
                }

            }
            SetMargin();
        }

        private void SetLTR()
        {

            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        public void SetMargin()
        {
            if (viewModel.NewDesignPopUp != null && viewModel.NewDesignPopUp.HeaderWithInfos != null && viewModel.NewDesignPopUp.HeaderWithInfos.Count != 0)
            {
                if (viewModel.NewDesignPopUp.HeaderWithInfos.Count < 2)
                {
                    MainPanCakeView1.Margin = new Thickness(0, 380, 0, 0);
                }
                else
                {
                    MainPanCakeView1.Margin = new Thickness(0, 130, 0, 0);
                }
            }
            else
            {
                MainPanCakeView1.Margin = new Thickness(0, 130, 0, 0);
            }
        }
        private void YesButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.HeaderWithInfoList != null)
                {
                    var message = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZZRefundEnableMessage).FirstOrDefault();
                    if (message != null)
                    {
                        MessagingCenter.Send<Object, string>(this, "YesReceived", AppResources.ZZZRefundEnableMessage);
                    }
                    var messageForVoid = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost).FirstOrDefault();
                    if (messageForVoid != null)
                    {
                        MessagingCenter.Send<Object, string>(this, "YesReceived", AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost);
                    }
                }
            }
            catch(Exception ex)
            {

            }

        //            MessagingCenter.Send<Object, string>(this, "YesReceived", "Yes");
        }

        private void NoButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.HeaderWithInfoList != null)
                {
                    var message = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZZRefundEnableMessage).FirstOrDefault();
                    if (message != null)
                    {
                        MessagingCenter.Send<Object, string>(this, "NoReceived", AppResources.ZZZRefundEnableMessage);
                    }
                    var messageForVoid = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost).FirstOrDefault();
                    if (messageForVoid != null)
                    {
                        MessagingCenter.Send<Object, string>(this, "NoReceived", AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            //MessagingCenter.Send<Object, string>(this, "NoReceived", "No");
        }
    }
}