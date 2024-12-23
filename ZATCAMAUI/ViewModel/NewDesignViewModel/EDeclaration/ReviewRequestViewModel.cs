using System.Collections.ObjectModel;
using System.Windows.Input;

using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.EDeclerationsModel;
using Acr.UserDialogs;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ReviewRequestViewModel : BaseViewModel
    {
        PaymentCardModel paymentCard = new PaymentCardModel();
        public PaymentCardModel PaymentCard { get { return paymentCard; } set { paymentCard = value; } }

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
        public PaymentTypes SelctedPaymentType { get; set; } = PaymentTypes.Visa;

        ObservableCollection<BottomSheetModel> _InquireList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> InquireList { get { return _InquireList; } set { _InquireList = value; OnPropertyChanged(); } }

        ObservableCollection<BottomSheetModel> _DetailsTotalFeesList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> DetailsTotalFeesList { get { return _DetailsTotalFeesList; } set { _DetailsTotalFeesList = value; OnPropertyChanged(); } }

        bool isNotEmptyDetailsTotalFeesList;
        public bool IsNotEmptyDetailsTotalFeesList { get { return isNotEmptyDetailsTotalFeesList; } set { isNotEmptyDetailsTotalFeesList = value; OnPropertyChanged(); } }

        TravelerDeclarationResponse _Inquire = new TravelerDeclarationResponse();
        public TravelerDeclarationResponse Inquire { get { return _Inquire; } set { _Inquire = value; OnPropertyChanged(); } }


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

        public ICommand GoToPaymentCommand
        {
            get
            {
                return new Command(_ =>
                {
                    try
                    {
                        if (Inquire.IsNotPaid)
                        {
                            if (SelctedPaymentType == PaymentTypes.SADAD)
                            {

                                MessageTxt = $"{AppResources.EDEcelarationSADADFrstMsg} {Inquire.sadadNumber}";
                                SADADNewTXT = AppResources.EDEcelarationSADADSecondMsg;
                                IsShowMsgView = true;
                            }
                            else
                            {
                                var decreptedURlParam = EncryptionHelper.EncryptStringAES($"\"refCode={Inquire.ReferenceID}&travilID={Inquire.travelID}\"");

                                var paymentRedirectURL = $"{PageSettings.GetPaymentWebViewURl()}{decreptedURlParam}";
                                Browser.OpenAsync(paymentRedirectURL, new BrowserLaunchOptions
                                {
                                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                                    TitleMode = BrowserTitleMode.Show,
                                    PreferredToolbarColor = Color.FromArgb("#002447"),
                                    PreferredControlColor = Color.FromArgb("#0996d4")
                                });
                            }


                        }
                    }
                    catch (Exception)
                    {

                    }
                   
                });
            }
        }

        public ICommand OnAppearingCommand
        {
            get
            {
                return new Command(_ =>
                {
                    try
                    {
                        PaymentCardCommand.Execute("1");
                        var date = DateTime.Now;
                        Inquire = App.Locator.StateManager.GetItem("inquireDeclaration") as TravelerDeclarationResponse;
                        if (Inquire != null)
                        {
                            Inquire.TravelDateString = DateTimeHelper.DateTimeFormater(Inquire.travelDate);
                            Inquire.totalFees = Math.Round(Inquire.totalFees, 2);
                            Inquire.tobacco?.ForEach(t => { InquireList.Add(new BottomSheetModel { Name = t.Name, Id = $"(x {t.count.ToString()})" }); });
                            Inquire.product?.ForEach(p => { InquireList.Add(new BottomSheetModel { Name = p.Name, Id = $"(x {p.count.ToString()})" }); });
                            Inquire.currency?.ForEach(c => { InquireList.Add(new BottomSheetModel { Name = c.Name }); });
                            Inquire.restricted?.ForEach(r => { InquireList.Add(new BottomSheetModel { Name = r.Name, Id = $"(x {r.count.ToString()})" }); });
                            Inquire.fees?.ForEach(f => { DetailsTotalFeesList.Add(new BottomSheetModel { Name = f.Name, Id = Math.Round(f.value, 2).ToString() }); });

                            IsNotEmptyDetailsTotalFeesList = Inquire.fees != null && Inquire.fees.Count > 0 ? true : false;
                        }
                    }
                    catch (Exception)
                    {

                    }


                });
            }
        }

        private void ResetData()
        {
            App.Locator.StateManager.DeleteItem("inquireDeclaration");
            Inquire = new TravelerDeclarationResponse();
            InquireList = new ObservableCollection<BottomSheetModel>();
            DetailsTotalFeesList = new ObservableCollection<BottomSheetModel>();
        }
        public void BackMethod()
        {
            ResetData();
            _navigationService.GoBack();
        }

        public ICommand CopyCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Clipboard.SetTextAsync(Inquire.sadadNumber.ToString());
                    UserDialogs.Instance.Toast(AppResources.Copied, TimeSpan.FromSeconds(1));

                });
            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    BackMethod();

                });
            }
        }

        public ReviewRequestViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}

