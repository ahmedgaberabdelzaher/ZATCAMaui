using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
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

            SetLTR();
        }

        private void setUpListItems()
        {
            List<PaymentOptionsModel> paymentOptions = new List<PaymentOptionsModel>();
            if (isModaPaymentAvailable && !isAmountLess)
            {
                paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_iconpay.png", UnSelectedCardIcon = "ic_iconpay_white.png", CardLabel = AppResources.PaymentMethodCardPayment, IconHeight = 40 });
            }
            if (Device.RuntimePlatform == Device.iOS)
            {

                if (!isAlreadyPaid && !isAmountLess)
                {
                    paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_icon_applelogo.png", UnSelectedCardIcon = "ic_icon_applelogo_white.png", CardLabel = AppResources.PaymentMethodApplePay, IconHeight = 25 });

                }
            }
            paymentOptions.Add(new PaymentOptionsModel() { SelectedCardIcon = "ic_sadad.png", UnSelectedCardIcon = "ic_sadad_white.png", CardLabel = AppResources.Sadad, IconHeight = 20 });
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

        private async void paymentItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            PaymentOptionsModel selectedItem = e.AddedItems[0] as PaymentOptionsModel;
            if (selectedItem.CardLabel == AppResources.PaymentMethodCardPayment)
            {
                MessagingCenter.Send<object, string>(this, "Card_Payment", "Yes");
                OnSelect?.Invoke("Card_Payment");
                await PopupNavigation.Instance.PopAsync();

            }
            else if (selectedItem.CardLabel == AppResources.Sadad)
            {
                MessagingCenter.Send<object, string>(this, "SADAD", "Yes");
                OnSelect?.Invoke("SADAD");
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                OnSelect?.Invoke("Apple Pay");

                await PopupNavigation.Instance.PopAsync();

                MessagingCenter.Send<object, string>(this, "Apple_Pay", "Yes");
            }

        }

    }
}