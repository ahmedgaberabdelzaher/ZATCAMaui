using System.Collections.ObjectModel;
using System.Windows.Input;

using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EDeclerationsModel.SubmitModels;
using System.Linq;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration
{
    public class ReviewRequestViewModel : BaseViewModel
    {
        ObservableCollection<BottomSheetModel> _InquireList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> InquireList { get { return _InquireList; } set { _InquireList = value; OnPropertyChanged(); } }

        ObservableCollection<BottomSheetModel> _DetailsTotalFeesList = new ObservableCollection<BottomSheetModel>();
        public ObservableCollection<BottomSheetModel> DetailsTotalFeesList { get { return _DetailsTotalFeesList; } set { _DetailsTotalFeesList = value; OnPropertyChanged(); } }

        bool isNotEmptyDetailsTotalFeesList;
        public bool IsNotEmptyDetailsTotalFeesList { get { return isNotEmptyDetailsTotalFeesList; } set { isNotEmptyDetailsTotalFeesList = value; OnPropertyChanged(); } }

        TravelerDeclarationResponse _Inquire = new TravelerDeclarationResponse();
        public TravelerDeclarationResponse Inquire { get { return _Inquire; } set { _Inquire = value; OnPropertyChanged(); } }

        public ICommand GoToPaymentCommand
        {
            get
            {
                return new Command(_ =>
                {
                    if (Inquire.IsNotPaid)

                    {
                        var paymentRedirectURL = $"{PageSettings.GetPaymentWebViewURl()}{Inquire.ReferenceID}&travilID={Inquire.travelID}";
                        Browser.OpenAsync(paymentRedirectURL, new BrowserLaunchOptions
                        {
                            LaunchMode = BrowserLaunchMode.SystemPreferred,
                            TitleMode = BrowserTitleMode.Show,
                            PreferredToolbarColor = Color.FromHex("#002447"),
                            PreferredControlColor = Color.FromHex("#0996d4")
                        });
                        //_navigationService.NavigateTo("EDeclarationPaymentPage", Inquire);
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

