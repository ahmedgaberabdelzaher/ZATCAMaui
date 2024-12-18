using System.Windows.Input;

using Acr.UserDialogs;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.EDeclerationsModel;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclarationPaymentViewModel : BaseViewModel
    {
        PaymentCardModel paymentCard = new PaymentCardModel();
        public PaymentCardModel PaymentCard { get { return paymentCard; } set { paymentCard = value; } }

        TravelerDeclarationResponse _TravelerDeclarationResponse = new TravelerDeclarationResponse();
        public TravelerDeclarationResponse TravelerDeclarationResponse { get { return _TravelerDeclarationResponse; } set { _TravelerDeclarationResponse = value; OnPropertyChanged(); } }

        string sADADNewTXT { get; set; }

        public string SADADNewTXT
        {
            get { return sADADNewTXT; }

            set
            {
                sADADNewTXT = value;
                OnPropertyChanged();
            }
        }
        public PaymentTypes SelctedPaymentType { get; set; }

        string priceText;
        public string PriceText { get { return priceText; } set { priceText = value; OnPropertyChanged(); } }

        public ICommand PaymentCardCommand
        {
            get
            {
                return new Command<string>((e) =>
                {

                    SelctedPaymentType = (PaymentTypes)Enum.Parse(typeof(PaymentTypes), e);
                    var selectedPaymentType = int.Parse(e);

                    if (selectedPaymentType == (int)PaymentTypes.Visa)
                    {
                        PaymentCard.BackgroundVisaCardImage = "QSelected.png";
                        PaymentCard.BackgroundSADADImage = "QUnselected.png";

                        PaymentCard.VisaCardImage = "paywhiteCard.png";
                        PaymentCard.SADADImage = "ColorSadad.png";

                        PaymentCard.VisaCardTextColor = Colors.White;
                        PaymentCard.SADADTextColor = Color.FromArgb("#002447");
                    }

                    else if (selectedPaymentType == (int)PaymentTypes.SADAD)
                    {
                        PaymentCard.BackgroundVisaCardImage = "QUnselected.png";
                        PaymentCard.BackgroundSADADImage = "QSelected.png";

                        PaymentCard.VisaCardImage = "ic_iconpay.png";
                        PaymentCard.SADADImage = "WhiteSadad.png";

                        PaymentCard.VisaCardTextColor = Color.FromArgb("#002447");
                        PaymentCard.SADADTextColor = Colors.White;
                    }

                });
            }
        }

        public ICommand PaymentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (SelctedPaymentType == PaymentTypes.SADAD)
                        {

                            MessageTxt = $"{AppResources.EDEcelarationSADADFrstMsg} {TravelerDeclarationResponse.sadadNumber}";
                            SADADNewTXT = AppResources.EDEcelarationSADADSecondMsg;
                            IsShowMsgView = true;
                        }
                        else
                        {
                            SADADNewTXT = string.Empty;
                            var decreptedURlParam = EncryptionHelper.EncryptStringAES($"\"refCode={TravelerDeclarationResponse.ReferenceID}&travilID={TravelerDeclarationResponse.travelID}\"");

                            var paymentRedirectURL = $"{PageSettings.GetPaymentWebViewURl()}{decreptedURlParam}";
                            await Browser.OpenAsync(paymentRedirectURL, new BrowserLaunchOptions
                            {
                                LaunchMode = BrowserLaunchMode.SystemPreferred,
                                TitleMode = BrowserTitleMode.Show,
                                PreferredToolbarColor = Color.FromArgb("#002447"),
                                PreferredControlColor = Color.FromArgb("#0996d4")
                            });
                        }
                    }
                    catch (Exception)
                    {

                    }


                });
            }
        }

        public ICommand CopyCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Clipboard.SetTextAsync(TravelerDeclarationResponse.sadadNumber.ToString());
                    UserDialogs.Instance.Toast(AppResources.Copied, TimeSpan.FromSeconds(1));

                });
            }
        }

        public EDeclarationPaymentViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

        }
    }
}

