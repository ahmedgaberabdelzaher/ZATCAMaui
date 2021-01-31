using EGAZT.Models.PaymentModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.PaymentOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentOptionsPageView : PopupPage
    {
        bool isModaPaymentAvailable = false;
        bool isAlreadyPaid = false;

        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public PaymentOptionsPageView(bool isModaPaymentAvailable, bool isAlreadyPaid)
        {
            this.isModaPaymentAvailable = isModaPaymentAvailable;
            this.isAlreadyPaid= isAlreadyPaid;
            InitializeComponent();
            SetLTR();
        }

        private void setUpListItems()
        {
            List<PaymentOptionsModel> paymentOptions = new List<PaymentOptionsModel>();
            if (isModaPaymentAvailable)
            {
                paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_iconpay", UnSelectedCardIcon = "ic_iconpay", CardLabel = "Card Payment" });
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "email.png", UnSelectedCardIcon = "email", CardLabel = "Apple Pay" });
            }
            paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_add1.png", UnSelectedCardIcon = "ic_add1", CardLabel = "SADAD"});
            paymentItemsListView.ItemsSource = paymentOptions;

            if (paymentOptions.Count > 2)
            {
                paymentItemsListView.HeightRequest = 280;
            }
            else
            {
                paymentItemsListView.HeightRequest = 140;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            setUpListItems();

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

        private async void paymentItemSelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            PaymentOptionsModel selectedItem = e.AddedItems[0] as PaymentOptionsModel;
            if (selectedItem.CardLabel == "Card Payment")
            {
                MessagingCenter.Send<Object, string>(this, "Card_Payment", "Yes");
                OnSelect?.Invoke("Card_Payment");
                await PopupNavigation.Instance.PopAsync();

            }
            else if (selectedItem.CardLabel == "Apple Pay")
            {
                MessagingCenter.Send<Object, string>(this, "Apple_Pay", "Yes");
                OnSelect?.Invoke("Apple Pay");
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                MessagingCenter.Send<Object, string>(this, "SADAD", "Yes");
                OnSelect?.Invoke("SADAD");
                await PopupNavigation.Instance.PopAsync();
            }

        }

    }
}