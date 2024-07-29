using Mopups.Pages;
using Mopups.Services;
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

            viewModel.MultiplePayableBills = multiplePayableBills;
        }


        private async void ContinueButtonClicked(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync();
            MessagingCenter.Send<object, string>(this, "MultipleBillsContinue", "Yes");
        }
    }
}