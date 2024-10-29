using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ChageFillingPeriodModel;

namespace ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodPageView : ContentPage, ChangeFillingInterface
    {
        #region Variable

        ChangeFillingPeriodViewModel viewModel;

        #endregion

        public ChangeFillingPeriodPageView()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.ChangeFillingPeriodPageView;
                this.BindingContext = viewModel;
                viewModel.cFInterface = this;
               

            }
            catch (Exception)
            {
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, string>(this, "YesReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "NoReceived");
            MessagingCenter.Unsubscribe<object, string>(this, "SaveCommandReceived");
            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, AttachmentsList>(this, "AttachmentReceived");

        }

       
        void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            ChangeFillingPeriodModel selectedItem = e.AddedItems[0] as ChangeFillingPeriodModel;
            viewModel.ShowAttachments = true;
            try
            {
                switch (viewModel.OutletDecisionOptions.IndexOf(selectedItem))
                {
                    case 0:
                        {
                            viewModel.IsTwoYearsAtachmentsVisible = true;
                            viewModel.IsMonthsAtachmentsVisible = false;
                            viewModel.IsOthersAtachmentsVisible = false;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;
                            return;
                        }
                    case 1:
                        {

                            viewModel.IsTwoYearsAtachmentsVisible = false;
                            viewModel.IsMonthsAtachmentsVisible = true;
                            viewModel.IsOthersAtachmentsVisible = false;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;
                            return;
                        }
                    case 2:
                        {
                            viewModel.IsTwoYearsAtachmentsVisible = false;
                            viewModel.IsMonthsAtachmentsVisible = false;
                            viewModel.IsOthersAtachmentsVisible = true;
                            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
                            viewModel.SelectedAttachmentText = AppResources.Attachment + " - " + selectedItem.ActiveOutletDecisionOptions;
                            return;
                        }
                }
            }
            catch (Exception)
            {
            }
        }

        private void IdNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel._idNumber = e.NewTextValue;
        }


        private void ContactPersonTextUnFocus(object sender, FocusEventArgs e)
        {
            viewModel.ContactPersonName = ContactPersonEntry.Text;
            viewModel.EnableDeclaration();
        }

        private void CheckBox_CheckedChanged(object sender, bool e)
        {
            viewModel.EnableDeclaration();
        }
        public void SelectDefaultAttachOption(int index)
        {
            AttachmentTypeOption.SelectedItem = viewModel.OutletDecisionOptions[index];
        }

        void OnFrequechCheckChanged(object sender, bool e)
        {
            viewModel.EnableFrequencyDetails();
        }
    }

    public interface ChangeFillingInterface
    {
        void SelectDefaultAttachOption(int index);
    }

}





