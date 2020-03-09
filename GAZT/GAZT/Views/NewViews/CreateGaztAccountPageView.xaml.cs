using GAZT.Manager;
using GAZT.Models;
using GAZT.ViewModel.NewViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateGaztAccountPageView : ContentPage
    {
        CreateGaztAccountPageViewModel viewModel;
       
        public CreateGaztAccountPageView(SignUpModelRootObject SignUpModelRootObjectModel)
        {
            InitializeComponent();
            viewModel = App.Locator.CreateGaztAccountPageView;
            this.BindingContext = viewModel;
            viewModel.SignUpModelRootObjectM = SignUpModelRootObjectModel;
            viewModel.TxtEmailAddress = SignUpModelRootObjectModel.d.AEmail;
            viewModel.TxtMobileNumber = SignUpModelRootObjectModel.d.AMobile;
            viewModel.OnPageLoad();
            SetLTR();
        }
        private void SetLTR()
        {


            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        private void EntryCfrmPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(EntryCfrmPass.Text))
            {
                if(EntryPass.Text != EntryCfrmPass.Text)
                {
                    frmCfrmPass.BorderColor = Color.Red;
                }
                else
                {
                    frmCfrmPass.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
        }

        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            StringBuilder PopMsg = new StringBuilder();
            bool IsAllValid = true;
            if (string.IsNullOrEmpty(viewModel.TxtEmailCode))
            {
                frmMobileCode.BorderColor = Color.Red;
                PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyouremailaddress);
                IsAllValid = false;
            }
            else
            {
                frmMobileCode.BorderColor = Color.FromHex("#B1B1B1");
            }

            if (string.IsNullOrEmpty(viewModel.TxtMobileNumberCode))
            {
                frmMobileCode.BorderColor = Color.Red;
                if (PopMsg.Length > 0)
                {
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyourmobilenumber);
                }
                else
                {
                    PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyourmobilenumber);
                }
                IsAllValid = false;
            }
            else
            {
                frmMobileCode.BorderColor = Color.FromHex("#B1B1B1");
            }
            if (string.IsNullOrEmpty(viewModel.TxtPassword))
            {
                frmPass.BorderColor = Color.Red;
                if (PopMsg.Length > 0)
                {
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                }
                else
                {
                    PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                }
                IsAllValid = false;
            }
            else
            {
                frmPass.BorderColor = Color.FromHex("#B1B1B1");
                bool IsValidPass = UtilityManager.IsPasswordValid(viewModel.TxtPassword);
                if (!IsValidPass)
                {
                    frmPass.BorderColor = Color.Red;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                    }
                    else
                    {
                        PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);

                    }
                    IsAllValid = false;
                }
                else
                {
                    frmPass.BorderColor = Color.FromHex("#B1B1B1");
                }
                if(viewModel.TxtPassword != viewModel.TxtConfirmPassword)
                {
                    frmCfrmPass.BorderColor = Color.Red;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.ZZNewpasswordfieldandconfirmPasswordfieldshouldmatchup);
                       
                    }
                    else
                    {
                        PopMsg.Append(AppResources.ZZNewpasswordfieldandconfirmPasswordfieldshouldmatchup);
                       
                    }
                    IsAllValid = false;
                }
                else
                {
                    frmCfrmPass.BorderColor = Color.FromHex("#B1B1B1");
                }
            }
            if (IsAllValid == true)
            {
                viewModel.CreateGaZTAccount();
            }
            else
            {
                if (PopMsg.Length > 0)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = PopMsg.ToString();
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                }
            }
            

        }


       
    }
}
