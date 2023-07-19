using System;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Manager;
using EGAZT.Models;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms.Internals;
using GAZT.Manager;
using EGAZT.Models.PaymentModel;
using Syncfusion.Licensing;
using static EGAZT.Models.NewYesorNoPageModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using Rg.Plugins.Popup.Services;
using System.Resources;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    [Preserve(AllMembers = true)]
    public class NewYesorNoPageViewModel : BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

        private Color _RQ1 = (Color)App.Current.Resources["DarkGrayTextColor"];
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
                RaisePropertyChanged("rq1");
            }
        }

        private Color _RQ2 = (Color)App.Current.Resources["DarkGrayTextColor"];
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
                RaisePropertyChanged("rq2");
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
                RaisePropertyChanged("QA1");
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
                RaisePropertyChanged("QA2");
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
                RaisePropertyChanged("IsYesQ1Checked");
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
                RaisePropertyChanged("IsYesQ2Checked");
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
                RaisePropertyChanged("IsNoQ1Checked");
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
                RaisePropertyChanged("basedonQ1");
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
                RaisePropertyChanged("IsNoQ2Checked");
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
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsSuccessmessage));
                    MessagingCenter.Send<Object>(this, "HideProfitGoods");

                    App.IsVAtProfitForGoods = false;
                    
                    IsLoading = false;
                    _navigationService.GoBack();

                }
                else
                {
                    IsLoading = false;
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZprofitsOnGoodsfailuremessage));
                }
            }
            catch(Exception ex)
            {

            }
            

        }
    }
}

