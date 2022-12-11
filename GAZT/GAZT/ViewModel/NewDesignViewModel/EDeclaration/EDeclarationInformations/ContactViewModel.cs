using System;
using System.Windows.Input;
using EGAZT.Models.EDeclerationsModel;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
	public partial class EDeclarationInformationsViewModel
    {

        ContactInfoModel contactInfo = new ContactInfoModel();
        public ContactInfoModel Contact { get { return contactInfo; } set { contactInfo = value; } }

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
                    if (Contact.IsTermsChecked)
                    {
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
                    Contact.IsTermsChecked = Contact.IsTermsChecked == true ? false : true;
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
    }
}

