using Mopups.Pages;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowVatInformationConfirmationPageView : PopupPage
    {
        public ShowVatInformationConfirmationPageViewModel viewModel;
        public ShowVatInformationConfirmationPageView(NewDesignPopUp newDesignPopData)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.ShowVatInformationConfirmationPageView;
                this.BindingContext = viewModel;
                this.CloseWhenBackgroundIsClicked = false;
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
            catch (Exception)
            {

            }
        }

        public void SetMargin()
        {
            if (viewModel.NewDesignPopUp != null && viewModel.NewDesignPopUp.HeaderWithInfos != null && viewModel.NewDesignPopUp.HeaderWithInfos.Count != 0)
            {
                if (viewModel.NewDesignPopUp.HeaderWithInfos.Count < 2)
                {
                    MainPanCakeView1.Margin = new Thickness(0, 300, 0, 0);
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
                        MessagingCenter.Send<object, string>(this, "YesReceived", AppResources.ZZZRefundEnableMessage);
                    }
                    var messageForVoid = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost).FirstOrDefault();
                    if (messageForVoid != null)
                    {
                        MessagingCenter.Send<object, string>(this, "YesReceived", AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost);
                    }
                    var messageForYesRefundConfirmation = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZZRefundYesMsgForFiteenPercent || x.Message == AppResources.ZZZRefundYesMsg).FirstOrDefault();
                    if (messageForYesRefundConfirmation != null)
                    {
                        MessagingCenter.Send<object, string>(this, "YesReceivedForRefundMsg", AppResources.ZZZRefundYesMsgForFiteenPercent);
                    }

                    var messageForNoRefundConfirmation = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.VATRefundNoMsg).FirstOrDefault();
                    if (messageForNoRefundConfirmation != null)
                    {
                        MessagingCenter.Send<object, string>(this, "ReceivedForYesRefundMsg", AppResources.VATRefundNoMsg);
                    }
                }
            }
            catch (Exception)
            {

            }
            MessagingCenter.Send<Object, string>(this, "YesReceived", "Yes");
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
                        MessagingCenter.Send<object, string>(this, "NoReceived", AppResources.ZZZRefundEnableMessage);
                    }
                    var messageForVoid = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost).FirstOrDefault();
                    if (messageForVoid != null)
                    {
                        MessagingCenter.Send<object, string>(this, "NoReceived", AppResources.ZZGeneralMessage_AllInfoFilledInTheFormWillBeLost);
                    }
                    var messageForYesRefundConfirmation = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.ZZZRefundYesMsgForFiteenPercent || x.Message == AppResources.ZZZRefundYesMsg).FirstOrDefault();
                    if (messageForYesRefundConfirmation != null)
                    {
                        MessagingCenter.Send<object, string>(this, "NoReceivedForRefundMsg", AppResources.ZZZRefundYesMsgForFiteenPercent);
                    }
                    var messageForNoRefundConfirmation = viewModel.HeaderWithInfoList.Where(x => x.Message == AppResources.VATRefundNoMsg).FirstOrDefault();
                    if (messageForNoRefundConfirmation != null)
                    {
                        MessagingCenter.Send<object, string>(this, "ReceivedForNoRefundMsg", AppResources.VATRefundNoMsg);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}