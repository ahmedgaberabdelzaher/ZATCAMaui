using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreMenuPopUpPageViewRTwo : PopupPage
    {
        #region Variable
        public MorePopUpViewModelRTwo viewModel;
        #endregion


        public MoreMenuPopUpPageViewRTwo(List<string> ListOfActionButtonsApplicable)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.MoreMenuPopUpPageViewRTwo;
                this.BindingContext = viewModel;
                if (ListOfActionButtonsApplicable != null)
                {
                    viewModel.VatReturnUIButtons = ListOfActionButtonsApplicable;
                }

            }
            catch (Exception)
            {


            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }


        private void SelectButton(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selected = (string)e.SelectedItem;
                if (selected != null)
                {

                    //NewAccountPopUpPageViewModel.ValidTypeIban = viewModel.IbanNumberText;
                    MessagingCenter.Send<object, string>(this, "SaveCommandReceived", selected);


                }

            }
            catch (Exception)
            {


            }
        }
    }
}