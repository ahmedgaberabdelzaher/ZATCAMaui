using System;
using System.Windows.Input;
using Xamarin.Forms;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.Generic;
using System.Linq;
using EGAZT.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
using EGAZT.Converters;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace EGAZT.ViewModel.NewDesignViewModel.EDeclaration
{
    public class EDeclarationPaymentViewModel : BaseViewModel
    {
        PaymentCardModel paymentCard = new PaymentCardModel();
        public PaymentCardModel PaymentCard { get { return paymentCard; } set { paymentCard = value; } }

        TravelerDeclarationResponse _TravelerDeclarationResponse = new TravelerDeclarationResponse();
        public TravelerDeclarationResponse TravelerDeclarationResponse { get { return _TravelerDeclarationResponse; } set { _TravelerDeclarationResponse = value; RaisePropertyChanged(); } }

        public PaymentTypes SelctedPaymentType { get; set; }

        string priceText;
        public string PriceText { get { return priceText; } set { priceText = value; RaisePropertyChanged(); } }

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

                        PaymentCard.VisaCardTextColor = Color.White;
                        PaymentCard.SADADTextColor = Color.FromHex("#002447");
                    }

                    else if (selectedPaymentType == (int)PaymentTypes.SADAD)
                    {
                        PaymentCard.BackgroundVisaCardImage = "QUnselected.png";
                        PaymentCard.BackgroundSADADImage = "QSelected.png";

                        PaymentCard.VisaCardImage = "ic_iconpay.png";
                        PaymentCard.SADADImage = "WhiteSadad.png";

                        PaymentCard.VisaCardTextColor = Color.FromHex("#002447");
                        PaymentCard.SADADTextColor = Color.White;
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
                    if (SelctedPaymentType == PaymentTypes.SADAD)
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.EDEcelarationSADADFrstMsg + TravelerDeclarationResponse.sadadNumber + $"\n{AppResources.EDEcelarationSADADSecondMsg}";
                    }
                    else
                    {
                       _navigationService.NavigateTo("PaymentWebView", TravelerDeclarationResponse.paymentOrder);
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

