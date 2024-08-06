
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
        public ICommand GoBackBtnTapped { get; set; }

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

        public NewYesorNoPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }

        public async void callSubmit()
        {
            IsLoading = true;
            try
            {

                VATFoodResults modelDetails = new VATFoodResults();
                modelDetails.Gpart = App.LoginDataRetrieved.TIN;
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


                //API Call for submitting the value;
                ProfitGoods response = await WebServiceManager.SaveVAtProfitGoodsAsync(modelDetails);
                if (response != null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsSuccessmessage));
                    MessagingCenter.Send<object>(this, "HideProfitGoods");

                    App.IsVAtProfitForGoods = false;

                    IsLoading = false;
                    _navigationService.GoBack();

                }
                else
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsfailuremessage));
                }
            }
            catch (Exception)
            {

            }


        }
    }
}

