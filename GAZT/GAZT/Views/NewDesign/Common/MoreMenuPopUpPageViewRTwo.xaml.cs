using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreMenuPopUpPageViewRTwo : PopupPage
    {
        #region Variable
        public MorePopUpViewModelRTwo viewModel;
        #endregion

       
        public MoreMenuPopUpPageViewRTwo(List<String> ListOfActionButtonsApplicable)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.MoreMenuPopUpPageViewRTwo;
                this.BindingContext = viewModel;
                SetLTR();
                if (ListOfActionButtonsApplicable != null)
                {
                    viewModel.VatReturnUIButtons = ListOfActionButtonsApplicable;
                }

            }
            catch (Exception ex)
            {

            }
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
        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }


        private async void SelectButton(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selected = (string)e.SelectedItem;
                if (selected != null)
                {

                    //NewAccountPopUpPageViewModel.ValidTypeIban = viewModel.IbanNumberText;
                    MessagingCenter.Send<Object, string>(this, "SaveCommandReceived", selected);


                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}