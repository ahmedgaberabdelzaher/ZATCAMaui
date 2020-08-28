using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.Views.NewDesign.GenericPickers;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriodPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodPageView : ContentPage
    {
        #region Variable

        ChangeFillingPeriodViewModel viewModel;

        #endregion

        public ChangeFillingPeriodPageView()
        {
            try
            {
                InitializeComponent();

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ChangeFillingPeriodPageView;
                this.BindingContext = viewModel;
                viewModel.GetVATChangeFillingData();
                viewModel.ResetData();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem",
                (sender, arg) =>
                {

                    viewModel.PickedDate = Convert.ToDateTime(arg.SelectedValue);

                });

            MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) => {
                //viewModel.PickerModel = arg;
                //viewModel.updatePicker();
                Console.WriteLine(arg);
                OnAppearing();
            });

            Xamarin.Forms.MessagingCenter.Subscribe<object, Attachments>(this, "AttachmentReceived", (sender, arg) =>
            {
                if (arg != null)
                {
                    viewModel.PopulateAttachments(arg.results);
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem");
            MessagingCenter.Unsubscribe<CalendarPickerPageView, GenericDatePickerModel>(this, "DatePickerSelectedItem");
            MessagingCenter.Unsubscribe<object, Attachments>(this, "AttachmentReceived");

        }
        void outletDecisionOptionsListView_SelectionChanged(System.Object sender,
            Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            ChangeFillingPeriodModel selectedItem = e.AddedItems[0] as ChangeFillingPeriodModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);

            viewModel.ShowAttachments = true;

            try
            {
                switch (viewModel.OutletDecisionOptions.IndexOf(selectedItem))
                {
                    case 0:
                        {
                            viewModel.SelectedAttachmentText = "Attachment - 2 years monthly return";
                            viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
                            return;
                        }
                    case 1:
                        {
                            viewModel.SelectedAttachmentText = "Attachment - 12 Months Taxable Revenue";
                            viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
                            return;
                        }
                    case 2:
                        {
                            viewModel.SelectedAttachmentText = "Attachment - Other Document";
                            viewModel.AttachmentsListViewData = new ObservableCollection<Attachment>();
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SummarybtnContinue_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangeFillingPeriodSuccessPage());
        }

    }
}