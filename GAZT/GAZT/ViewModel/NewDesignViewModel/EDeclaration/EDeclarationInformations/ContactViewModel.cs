using System;
using System.Windows.Input;
using EGAZT.Models.EDeclerationsModel;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EDeclaration.PopUpPages;
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
                    AcknowledgePopUpPage poupWindow = new AcknowledgePopUpPage();
                    await PopupNavigation.Instance.PushAsync(poupWindow);
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
                    if (SubmitModel.travelerDeclaration.IsTermsChecked)
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
    }
}

