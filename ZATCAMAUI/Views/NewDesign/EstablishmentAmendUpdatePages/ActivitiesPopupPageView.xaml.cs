
using Mopups.Pages;
using System.Collections.ObjectModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Models.EstablishmentRegistration;
using Syncfusion.Maui.Buttons;


namespace ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesPopupPageView : PopupPage
    {
        private ActivitiesPopUpViewModel viewModel;
        public ActivitiesPopupPageView(PopUpServiceModel popUpModel, bool isEnabled = true)
        {
            try
            {
                viewModel = App.Locator.ActivityPopUpPageView;
                InitializeComponent();
                this.BindingContext = viewModel;
                FlowDirection = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
                viewModel.AllActivitiesList.Clear();
                MessagingCenter.Subscribe<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", (sender, arg) =>
                {

                    viewModel.PickerModel = arg;
                });


                if (isEnabled == false)
                {
                    if (popUpModel?.existedActivities?.Count > 0)
                    {
                        viewModel.AllActivitiesList = new ObservableCollection<NregMulSet>(popUpModel?.existedActivities);
                    }

                }
                else
                {
                    viewModel.ActivitySetsList = popUpModel?.activitiesList;
                    if (popUpModel?.existedActivities?.Count > 0)
                    {
                        viewModel.AllActivitiesList = new ObservableCollection<NregMulSet>(popUpModel?.existedActivities);
                    }
                }

                if (popUpModel?.existedActivities?.Count > 0)
                {
                    viewModel.isListVisible = true;
                    viewModel.isNoData = false;
                }
                else
                {
                    viewModel.isListVisible = false;
                    viewModel.isNoData = true;
                }

                viewModel.isEditable = isEnabled;
            }
            catch (Exception )
            {
            }
        }

        private void onItemDelete_ItemTapped(object sender, EventArgs e)
        {
            var button = sender as SfButton;
            if (button != null)
            {
                var item = button.BindingContext as NregMulSet;
                var items = (BindingContext as ActivitiesPopUpViewModel)?.AllActivitiesList;
                if (item != null && items != null)
                {
                    var index = items.IndexOf(item);
                    viewModel.DeleteListItem(index);
                }
            }


        }
    }
}