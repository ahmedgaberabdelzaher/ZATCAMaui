using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Manager;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePopUpPageView : PopupPage
    {

        #region Variable
        public MorePopUpPageViewModel viewModel;
        #endregion

        public MorePopUpPageView(List<String> ListOfActionButtonsApplicable)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.MorePopUpPageView;
                this.BindingContext = viewModel;
                SetLTR();
                if(ListOfActionButtonsApplicable!=null)
                {
                    viewModel.VatReturnUIButtons = ListOfActionButtonsApplicable;
                }
                btnList.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                    // don't do anything if we just de-selected the row.
                    if (e.Item == null) return;

                    if (sender is ListView lv) lv.SelectedItem = null;
                };
            }
            catch(Exception er)
            {
                Console.WriteLine(er.Message);
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

        private void OnAttachmentTapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            PopupNavigation.Instance.PushAsync(new FileAttachmentPopupPageView());

        }

        private void SelectButton(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selected = (string)e.SelectedItem;
                if (selected != null)
                {
                   
                    //NewAccountPopUpPageViewModel.ValidTypeIban = viewModel.IbanNumberText;
                    MessagingCenter.Send<Object, string>(this, "CommandReceived", selected);
                   

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }
    }
}