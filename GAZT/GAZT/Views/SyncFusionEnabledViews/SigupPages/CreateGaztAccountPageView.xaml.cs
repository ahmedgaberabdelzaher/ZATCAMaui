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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
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
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.CreateGaztAccountPageView;
            this.BindingContext = viewModel;
            viewModel.SignUpModelRootObjectM = SignUpModelRootObjectModel;
            viewModel.TxtEmailAddress = SignUpModelRootObjectModel.d.AEmail;
            string mobileno = SignUpModelRootObjectModel.d.AMobile;
            viewModel.TxtMobileNumber="XXXXXXXXXX"+ mobileno.Substring(mobileno.Length - 4, 4);
            //viewModel.TxtMobileNumber = SignUpModelRootObjectModel.d.AMobile;//TxtMobileNumber string sub = mystring.Substring(mystring.Length - count, count);
            viewModel.OnPageLoad();
            SetLTR();
            ChangeAeroIcon();
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
                    frmCfrmPass.HasError = true;
                }
                else
                {
                    frmCfrmPass.HasError = false;
                }
            }
        }

        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            StringBuilder PopMsg = new StringBuilder();
            bool IsAllValid = true;
            if (string.IsNullOrEmpty(viewModel.TxtEmailCode))
            {
                frmMobileCode.HasError = true;
                PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyouremailaddress);
                IsAllValid = false;
            }
            else
            {
                frmMobileCode.HasError = false;
            }

            if (string.IsNullOrEmpty(viewModel.TxtMobileNumberCode))
            {
                frmMobileCode.HasError = true;
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
                frmMobileCode.HasError = false;
            }
            if (string.IsNullOrEmpty(viewModel.TxtPassword))
            {
                frmPass.HasError = true;
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
                frmPass.HasError = false;
                bool IsValidPass = UtilityManager.IsPasswordValid(viewModel.TxtPassword);
                if (!IsValidPass)
                {
                    frmPass.HasError = true;
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
                    frmPass.HasError = false;
                }
                if(viewModel.TxtPassword != viewModel.TxtConfirmPassword)
                {
                    frmPass.HasError = true;
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
                    frmCfrmPass.HasError = false;
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
