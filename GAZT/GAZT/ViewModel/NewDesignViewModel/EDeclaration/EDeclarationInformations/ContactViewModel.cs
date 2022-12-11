using System;
using System.Windows.Input;
using EGAZT.Models.EDeclerationsModel;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
using System.Text.RegularExpressions;
using EGAZT.Models.BaseModels;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;
using Newtonsoft.Json;
using System.Threading.Tasks;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations
{
	public partial class EDeclarationInformationsViewModel
    {

        TravelerDeclarationResponse travelerDeclarationResponse;
        public TravelerDeclarationResponse TravelerDeclarationResponse { get { return travelerDeclarationResponse; } set { travelerDeclarationResponse = value; RaisePropertyChanged(); } }

        public ICommand GoToSuccessCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    if(IsValidateContactInfo())
                    {
                        AcknowledgePopUpPage poupWindow = new AcknowledgePopUpPage();
                        await PopupNavigation.Instance.PushAsync(poupWindow);
                    }
                   
                });
            }
        }
        private async Task SubmitDecleration()
        {
            try
            {
                IsLoading = true;
                var submitRes = await DeclerationServices.SubmitDecleration(SubmitModel);
                if (submitRes.IsSuccessStatusCode)
                {
                    var conent = await submitRes.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<EDeclerationSubmitResponseModel>(conent);
                    if (data.result != null)
                    {
                        TravelerDeclarationResponse = data.result.travelerDeclarationResponse;
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
          

        }


        public ICommand ApproveDeclarationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (SubmitModel.travelerDeclaration.IsTermsChecked )
                    {
                        await SubmitDecleration();
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

            Regex phoneRegex = new Regex(@"^5[0-9]{8}$");
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
            SubmitModel.travelerDeclaration.phoneNumber = "+966" + SubmitModel.travelerDeclaration.phoneNumber;
            return true;

        }
    }
}

