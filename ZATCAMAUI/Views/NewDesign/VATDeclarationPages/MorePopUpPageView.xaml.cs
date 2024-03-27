using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePopUpPageView : PopupPage
    {

        #region Variable
        public MorePopUpPageViewModel viewModel;
        #endregion

        public MorePopUpPageView(List<string> ListOfActionButtonsApplicable)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.MorePopUpPageView;
                this.BindingContext = viewModel;
                if (ListOfActionButtonsApplicable != null)
                {
                    viewModel.VatReturnUIButtons = ListOfActionButtonsApplicable;
                }
                btnList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
                {
                    // don't do anything if we just de-selected the row.
                    if (e.Item == null) return;

                    if (sender is ListView lv) lv.SelectedItem = null;
                };
            }
            catch (Exception)
            {
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

                    MessagingCenter.Send<object, string>(this, "CommandReceived", selected);


                }

            }
            catch (Exception)
            {


            }
        }
    }
}