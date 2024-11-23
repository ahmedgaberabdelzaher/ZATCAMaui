
using Mopups.Services;
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using static ZATCAMAUI.Models.VATgoodsOnprofit.NewYesorNoPageModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATgoodsOnprofit
{

    public class NewYesorNoPageViewModel : BaseViewModel
    {
        public ICommand OnCancelButtonCommand { get; set; }
        public ICommand OnSubmitButtonCommand { get; set; }
        public ICommand OnAppearingNewYesorNoPageCommand { get; set; }

        private Color _RQ1 = (Color)Application.Current.Resources["DarkGrayTextColor"];
        public Color rq1
        {
            get
            {
                return _RQ1;
            }
            set
            {
                if (_RQ1 == value) return;
                _RQ1 = value;
                OnPropertyChanged("rq1");
            }
        }

        private Color _RQ2 = (Color)Application.Current.Resources["DarkGrayTextColor"];
        public Color rq2
        {
            get
            {
                return _RQ2;
            }
            set
            {
                if (_RQ2 == value) return;
                _RQ2 = value;
                OnPropertyChanged("rq2");
            }
        }

        private string _qa1 = "";
        public string QA1
        {
            get
            {
                return _qa1;
            }
            set
            {
                if (_qa1 == value) return;
                _qa1 = value;
                OnPropertyChanged("QA1");
            }
        }

        private string _qa2 = "";
        public string QA2
        {
            get
            {
                return _qa2;
            }
            set
            {
                if (_qa2 == value) return;
                _qa2 = value;
                OnPropertyChanged("QA2");
            }
        }



        private bool isYesQ1Checked = false;
        public bool IsYesQ1Checked
        {
            get { return isYesQ1Checked; }
            set
            {
                if (isYesQ1Checked == value) return;
                isYesQ1Checked = value;
                OnPropertyChanged("IsYesQ1Checked");
            }
        }
        private bool isYesQ2Checked = false;
        public bool IsYesQ2Checked
        {
            get { return isYesQ2Checked; }
            set
            {
                if (isYesQ2Checked == value) return;
                isYesQ2Checked = value;
                OnPropertyChanged("IsYesQ2Checked");
            }
        }
        private bool isNoQ1Checked = false;
        public bool IsNoQ1Checked
        {
            get { return isNoQ1Checked; }
            set
            {
                if (isNoQ1Checked == value) return;
                isNoQ1Checked = value;
                OnPropertyChanged("IsNoQ1Checked");
            }
        }
        private bool _BasedonQ1 = false;
        public bool basedonQ1
        {
            get { return _BasedonQ1; }
            set
            {
                if (_BasedonQ1 == value) return;
                _BasedonQ1 = value;
                OnPropertyChanged("basedonQ1");
            }
        }
        private bool isNoQ2Checked = false;
        public bool IsNoQ2Checked
        {
            get { return isNoQ2Checked; }
            set
            {
                if (isNoQ2Checked == value) return;
                isNoQ2Checked = value;
                OnPropertyChanged("IsNoQ2Checked");
            }
        }
        private bool _yesQ1Enable = false;
        public bool YesQ1Enable
        {
            get { return _yesQ1Enable; }
            set
            {
                if (_yesQ1Enable == value) return;
                _yesQ1Enable = value;
                OnPropertyChanged("YesQ1Enable");
            }
        }

        private bool _noQ1Enable = false;
        public bool NoQ1Enable
        {
            get { return _noQ1Enable; }
            set
            {
                if (_noQ1Enable == value) return;
                _noQ1Enable = value;
                OnPropertyChanged("NoQ1Enable");
            }
        }

        private bool isYesQ3Checked = false;
        public bool IsYesQ3Checked
        {
            get { return isYesQ3Checked; }
            set
            {
                if (isYesQ3Checked == value) return;
                isYesQ3Checked = value;
                OnPropertyChanged("IsYesQ3Checked");
            }
        }

        private bool isNoQ3Checked = false;
        public bool IsNoQ3Checked
        {
            get { return isNoQ3Checked; }
            set
            {
                if (isNoQ3Checked == value) return;
                isNoQ3Checked = value;
                OnPropertyChanged("IsNoQ3Checked");
            }
        }

        private bool showDeregQuestion = false;
        public bool ShowDeregQuestion
        {
            get { return showDeregQuestion; }
            set
            {
                if (showDeregQuestion == value) return;
                showDeregQuestion = value;
                OnPropertyChanged("ShowDeregQuestion");
            }
        }

        private bool showQ1 = false;
        public bool ShowQ1
        {
            get { return showQ1; }
            set
            {
                if (showQ1 == value) return;
                showQ1 = value;
                OnPropertyChanged("ShowQ1");
            }
        }



        private ProfitGoods profitGoodsModel = new ProfitGoods();
        public ProfitGoods ProfitGoodsModel
        {
            get { return profitGoodsModel; }
            set
            {
                if (profitGoodsModel == value) return;
                profitGoodsModel = value;
                OnPropertyChanged("ProfitGoodsModel");
            }
        }

        private VATFoodResults _previousRequest = new VATFoodResults();
        public VATFoodResults PreviousRequest
        {
            get { return _previousRequest; }
            set
            {
                if (_previousRequest == value) return;
                _previousRequest = value;
                OnPropertyChanged("PreviousRequest");
            }
        }
        public NewYesorNoPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            OnSubmitButtonCommand = new Command(async () =>
            {
                try
                {
                    if (ShowDeregQuestion == true)
                    {
                        if (IsYesQ3Checked == false)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsM02Vaidation));
                            return;
                        }

                    }
                    else
                    {
                        if (IsYesQ1Checked == false && IsNoQ1Checked == false)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsM02Vaidation));
                            rq1 = (Color)Application.Current.Resources["Red"];
                            return;
                        }


                        if (QA1 == "R" || QA1 == "r")
                        {
                            if (ProfitGoodsModel.registration.ToUpper().Equals("X"))
                            {
                                var result = await _dialogService.ShowMessage(AppResources.ZZZConfirmationMsg, AppResources.DeRegConfirmationmsg, AppResources.ZNo, AppResources.ZYes);
                                if (!result)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZProfitOnGoodsQ1M01Validation));
                                return;
                            }
                        }


                        if (basedonQ1 == true)
                        {
                            if (IsYesQ2Checked == false && IsNoQ2Checked == false)
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsM02Vaidation));
                                rq2 = (Color)Application.Current.Resources["Red"];
                                return;
                            }
                        }
                    }

                    await CallSubmit();
                }
                catch (Exception ex)
                {

                }
               
            });

            OnAppearingNewYesorNoPageCommand = new Command(async () =>
            {
                MakeFalse();
                await GetApplicationRequestAsync();
            });

            OnCancelButtonCommand = new Command(async () =>
            {
               await ShowAlertPopup(AppResources.ZProfitOnGoodsConfrimationMsg);
            });
        }

       private void MakeFalse()
        {
            IsYesQ1Checked = false;
            IsNoQ1Checked = false;
            IsYesQ2Checked = false;
            IsNoQ2Checked = false;
        }


        public async Task CallSubmit()
        {
            IsLoading = true;
            try
            {

                VATFoodResults modelDetails = new VATFoodResults();
                modelDetails.Gpart = App.LoginDataRetrieved.TIN;
                modelDetails.Fbnum = ProfitGoodsModel.Fbnum;
                modelDetails.TpName = ProfitGoodsModel.taxpayerName;
                if (showDeregQuestion)
                {
                    modelDetails.SaleUgmCb = false;
                    modelDetails.OthActyCb = false;
                }
                else
                {
                    if (QA1 == "X")
                    {
                        modelDetails.SaleUgmCb = true;
                    }
                    else
                    {
                        modelDetails.SaleUgmCb = false;
                    }
                    if (QA2 == "X")
                    {
                        modelDetails.OthActyCb = true;
                    }
                    else
                    {
                        modelDetails.OthActyCb = false;
                    }
                    if (PreviousRequest.SaleUgmCb == modelDetails.SaleUgmCb && PreviousRequest.OthActyCb == modelDetails.OthActyCb)
                    {
                        IsLoading = false;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NoChangesInTheApplication));
                        return;
                    }
                }

                ProfitGoods response = await WebServiceManager.SaveVAtProfitGoodsAsync(modelDetails);
                if (response != null)
                {
                    if (ShowDeregQuestion)//De reg
                    {
                        App.TP.VtpmFg = "";
                        string message = string.Format(AppResources.VATGoodsDeregisterSuccessMsg, response.taxpayerName, response.Gpart, response.Fbnum);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));

                    }
                    else
                    { // Reg
                        App.TP.VtpmFg = "X";
                        await _dialogService.ShowMessage(AppResources.ZprofitsOnGoodsSuccessmessage, AppResources.Information);
                    }
                    IsLoading = false;
                    App.HasToRefreshLoaderOnDashboard = true;
                    _navigationService.GoBack();
                    MessagingCenter.Send<object>(this, "HideProfitGoods");

                }
                else
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsfailuremessage));
                }
                IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;
            }


        }

        private async Task GetApplicationRequestAsync()
        {
            try
            {
                IsLoading = true;
                ProfitGoodsModel = await WebServiceManager.GetVATProfitGoodsAsync();
                IsLoading = false;
                if (!string.IsNullOrEmpty(ProfitGoodsModel.DregFbnum))
                {
                    string message = string.Format(AppResources.VATGoodsDeregisterMsg, ProfitGoodsModel.taxpayerName, ProfitGoodsModel.Gpart, ProfitGoodsModel.DregFbnum);

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                    _navigationService.GoBack();
                    return;
                }


                if (ProfitGoodsModel.registration.ToUpper().Equals("X"))
                {
                    ShowDeregQuestion = true;
                    ShowQ1 = false;

                    var result = await _dialogService.ShowMessage(AppResources.ZZZConfirmationMsg, AppResources.VATProfitDeregisterQuestion, AppResources.ZYes, AppResources.ZNo);
                    if (result)
                    {
                        await CallSubmit();
                    }
                    else
                    {
                        _navigationService.GoBack();
                    }
                }
                else
                {
                    ShowDeregQuestion = false;
                    ShowQ1 = true;
                    YesQ1Enable = true;
                    NoQ1Enable = true;


                }
            }
            catch (Exception)
            {
                IsLoading = false;
            }
        }

        private async Task ShowAlertPopup(string _message)
        {
             await _dialogService.ShowMessage(AppResources.Information, _message, AppResources.ZProfitOnGoodsConfrimationOk, AppResources.ZprofitsOnGoodscancel);
            MakeFalse();
            _navigationService.GoBack();
        }


    }
}

