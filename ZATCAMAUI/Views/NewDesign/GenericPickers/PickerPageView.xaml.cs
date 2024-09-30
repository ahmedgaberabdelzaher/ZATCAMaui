using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.GenericPickers;

namespace ZATCAMAUI.Views.NewDesign.GenericPickers
{

    public partial class PickerPageView : PopupPage
    {
        int _pageCode = 0;
        PickerPageViewModel viewModel;
        public PickerPageView(List<string> _pickerSource)
        {
            InitializeComponent();

            viewModel = App.Locator.PickerPageView;
            viewModel.PickerItemSource = _pickerSource;

            this.BindingContext = viewModel;

        }


       


        public PickerPageView(GenericPickerModel _pickerSource)
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.PickerPageView;
                viewModel.DataSource = _pickerSource;
                viewModel.PickerItemSource = viewModel.DataSource.PickerData;
                viewModel.PickerTitle = viewModel.DataSource.PickerTitle;
                _pageCode = _pickerSource.PageCode;
                if (viewModel.DataSource.SelectedValue != null)
                {
                    viewModel.SelectedItem = viewModel.DataSource.SelectedValue;
                }
                else
                {
                    viewModel.DataSource.SelectedValue = _pickerSource.PickerData.FirstOrDefault();
                }
                this.BindingContext = viewModel;
            }
            catch (Exception)
            {
            }

        }

        void genericPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            try
            {
                viewModel.DataSource.SelectedValue = viewModel.DataSource.PickerData[e.NewValue];

            }
            catch (Exception)
            {



            }
        }

        private void PopupClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                MopupService.Instance.PopAsync();
                if (viewModel.PickerItemSource.Count == 1)
                {
                    viewModel.DataSource.SelectedValue = viewModel.DataSource.PickerData[0];
                    MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);
                }
                else if (_pageCode == 1)
                {
                    MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelected", viewModel.DataSource);
                }
                else if (viewModel.DataSource.SelectedValue != null)
                    MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);

            }
            catch (Exception)
            {



            }
        }

        private void PopupCancel_Clicked(object sender, EventArgs e)
        {

            MopupService.Instance.PopAsync();

        }

        void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.PickerItemSource.Count == 1)
                {
                    viewModel.DataSource.SelectedValue = viewModel.DataSource.PickerData[0];
                    MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);
                }
                else if (_pageCode == 1)
                {
                    MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelected", viewModel.DataSource);
                }
                else if (viewModel.DataSource.SelectedValue != null)
                    MessagingCenter.Send<PickerPageView, GenericPickerModel>(this, "PickerSelectedItem", viewModel.DataSource);

              
            }
            catch (Exception)
            {



            }
        }
    }
}
