using Mopups.Pages;
using Mopups.Services;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Models.PaymentModel;

namespace ZATCAMAUI.Views.NewDesign.PaymentOptions
{

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
            this.isAlreadyPaid = isAlreadyPaid;
            this.isAmountLess = isAmountLess;
            amountMsg = messageText;
            InitializeComponent();
        }

        private void setUpListItems()
        {
            List<PaymentOptionsModel> paymentOptions = new List<PaymentOptionsModel>();
            if (isModaPaymentAvailable && !isAmountLess)
            {
                paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "Payment.png", UnSelectedCardIcon = "ic_iconpay_white.png", CardLabel = AppResources.PaymentMethodCardPayment});
            }
            paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_sadad.png", UnSelectedCardIcon = "ic_sadad_white.png", CardLabel = AppResources.Sadad });
            paymentItemsListView.ItemsSource = paymentOptions;

            if (paymentOptions.Count > 2)
            {
                paymentItemsListView.HeightRequest = 240;
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

            TextMadaPaymentText.Text = amountMsg;

            if (string.IsNullOrEmpty(TextMadaPaymentText.Text))
            {
                FrameMadaPaymentText.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            setUpListItems();

        }

        private async void paymentItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            PaymentOptionsModel selectedItem = e.AddedItems[0] as PaymentOptionsModel;
            if (selectedItem.CardLabel == AppResources.PaymentMethodCardPayment)
            {
                MessagingCenter.Send<object, string>(this, "Card_Payment", "Yes");
                OnSelect?.Invoke("Card_Payment");
                await MopupService.Instance.PopAsync();

            }
            else if (selectedItem.CardLabel == AppResources.Sadad)
            {
                MessagingCenter.Send<object, string>(this, "SADAD", "Yes");
                OnSelect?.Invoke("SADAD");
                await MopupService.Instance.PopAsync();
            }

        }

    }
}