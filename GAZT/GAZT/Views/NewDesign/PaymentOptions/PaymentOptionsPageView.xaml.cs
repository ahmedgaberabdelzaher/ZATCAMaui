using EGAZT.Models.PaymentModel;
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

namespace EGAZT.Views.NewDesign.PaymentOptions
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentOptionsPageView : PopupPage
    {
        bool isModaPaymentAvailable = false;
        bool isAlreadyPaid = false;
        bool isAmountLess = false;
        string amountMsg = "";


        public delegate void OnSelectDelegate(string item);
        public OnSelectDelegate OnSelect { get; set; } = null;
        public PaymentOptionsPageView(bool isModaPaymentAvailable, bool isAlreadyPaid, bool isAmountLess, string messageText)
        {
            this.isModaPaymentAvailable = isModaPaymentAvailable;
            this.isAlreadyPaid= isAlreadyPaid;
            this.isAmountLess = isAmountLess;
            this.amountMsg = messageText;
            InitializeComponent();

            SetLTR();
        }

        private void setUpListItems()
        {
            List<PaymentOptionsModel> paymentOptions = new List<PaymentOptionsModel>();
            if (isModaPaymentAvailable && !isAmountLess)
            {
                paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_iconpay", UnSelectedCardIcon = "ic_iconpay_white", CardLabel = AppResources.PaymentMethodCardPayment});
            }
            if (Device.RuntimePlatform == Device.iOS)
            {

                if (!isAlreadyPaid && !isAmountLess)
                {
                    paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_icon_applelogo", UnSelectedCardIcon = "ic_icon_applelogo_white", CardLabel = AppResources.PaymentMethodApplePay });

                }
            }
            paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_sadad", UnSelectedCardIcon = "ic_sadad_white", CardLabel =AppResources.Sadad});
            paymentItemsListView.ItemsSource = paymentOptions;

            if (paymentOptions.Count > 2)
            {
                paymentItemsListView.HeightRequest = 260;
            }
            else
            {
                paymentItemsListView.HeightRequest = 140;
            }

            if (isAmountLess || isAlreadyPaid)
            {
                FrameMadaPaymentText.IsVisible = true;
            }
            else
            {
                FrameMadaPaymentText.IsVisible = false;
            }

            TextMadaPaymentText.Text = this.amountMsg;

            //if (isAlreadyPaid)
            //{
            //    TextMadaPaymentText.Text = "There is no amount payable for the selected invoice";
            //}
            //else
            //{
            //    TextMadaPaymentText.Text = this.amountMsg;
            //}

            if (String.IsNullOrEmpty(TextMadaPaymentText.Text))
            {
                FrameMadaPaymentText.IsVisible = false;
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
            if (selectedItem.CardLabel == AppResources.PaymentMethodCardPayment)
            {
                MessagingCenter.Send<Object, string>(this, "Card_Payment", "Yes");
                OnSelect?.Invoke("Card_Payment");
                await PopupNavigation.Instance.PopAsync();

            }
            else if (selectedItem.CardLabel == AppResources.PaymentMethodApplePay)
            {
                OnSelect?.Invoke("Apple Pay");

                await PopupNavigation.Instance.PopAsync();

                MessagingCenter.Send<Object, string>(this, "Apple_Pay", "Yes");
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