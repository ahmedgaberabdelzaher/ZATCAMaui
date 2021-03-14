using EGAZT.ViewModel.NewDesignViewModel;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using EGAZT.Models;
using System.Collections.Generic;

namespace EGAZT.Views.NewDesign.MyBillsPages
{
    [Preserve(AllMembers = true)]
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