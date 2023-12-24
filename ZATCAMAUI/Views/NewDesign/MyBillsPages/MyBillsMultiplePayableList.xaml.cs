using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using System.Collections.ObjectModel;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.Views.NewDesign.MyBillsPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBillsMultiplePayableList : PopupPage
    {
        MyBillsMultiplePayableListViewModel viewModel;
        public MyBillsMultiplePayableList(ObservableCollection<MyBills> multiplePayableBills)
        {
            InitializeComponent();

            if (viewModel != null) return;
            viewModel = App.Locator.MyBillsMultiplePayableList;
            this.BindingContext = viewModel;

            //for(int i = 0; i < multiplePayableBills.Count; i++)
            //{
            //    multiplePayableBills[i].Abtypt = multiplePayableBills[i].Txt30;
            //}

            viewModel.MultiplePayableBills = multiplePayableBills;
            SetLTR();
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

        private async void ContinueButtonClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            MessagingCenter.Send<object, string>(this, "MultipleBillsContinue", "Yes");
        }
    }
}