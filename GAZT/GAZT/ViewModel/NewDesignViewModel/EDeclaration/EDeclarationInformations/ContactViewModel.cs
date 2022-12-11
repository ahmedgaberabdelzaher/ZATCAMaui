using System;
using System.Windows.Input;
using EGAZT.Models.EDeclerationsModel;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using System.Text.RegularExpressions;
namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
	public partial class EDeclarationInformationsViewModel
    {

        public ICommand GoToSuccessCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    AcknowledgePopUpPage poupWindow = new AcknowledgePopUpPage();
                    await PopupNavigation.Instance.PushAsync(poupWindow);
                });
            }
        }

        public ICommand ApproveDeclarationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsValidateContactInfo() && SubmitModel.travelerDeclaration.IsTermsChecked )
                    {
                        isSuccessPage = true;
                        await PopupNavigation.Instance.PopAsync(true);
                        _navigationService.NavigateTo("EDeclarationSuccessPage");
                    }

                });
            }
        }

        public ICommand CheckBoxCommand
        {
            get
            {
                return new Command(() =>
                {
                    SubmitModel.travelerDeclaration.IsTermsChecked = SubmitModel.travelerDeclaration.IsTermsChecked == true ? false : true;
                });
            }
        }

        public ICommand OpenTermsLinkCommand
        {
            get
            {
                return new Command(() =>
                {

                });
            }
        }
        private bool IsValidateContactInfo()
        {

            Regex phoneRegex = new Regex(@"^5[0-9]{9}$");
            Regex Email = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            if (string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.phoneNumber)
                    || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.address)
                    || string.IsNullOrWhiteSpace(SubmitModel.travelerDeclaration.email))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.RequiredData;
                return false;

            }
            else if (!Email.IsMatch(SubmitModel.travelerDeclaration.email))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.InvalidEmailFormat;
                return false;
            }
            else if (!phoneRegex.IsMatch(SubmitModel.travelerDeclaration.phoneNumber))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.ZZMobilenumberhastostartwithnumber5;
                return false;


            }
            return true;

        }
    }
}

