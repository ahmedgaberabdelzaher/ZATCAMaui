using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    [Preserve(AllMembers = true)]
    public class GeneralServicesViewModel : BaseViewModel
    {
        [Preserve(AllMembers = true)]
        public class GeneralServicesListModel
        {
            public string ZDTitle { get; set; }
            public string ZDImageSource { get; set; }
            public string ArrowImageSource { get; set; }
        }
        
        public ObservableCollection<GeneralServicesListModel> _generalServicesList { get; set; }
        public ObservableCollection<GeneralServicesListModel> GeneralServicesList
        {
            get
            {
                return _generalServicesList;
            }

            set
            {
                _generalServicesList = value;
                RaisePropertyChanged("GeneralServicesList");
            }
        }
        public ObservableCollection<GeneralServicesListModel> _refundRequestMenuList { get; set; }
        public ObservableCollection<GeneralServicesListModel> RefundRequestMenuList
        {
            get
            {
                return _refundRequestMenuList;
            }

            set
            {
                _refundRequestMenuList = value;
                RaisePropertyChanged("RefundRequestMenuList");
            }
        }

        
        public ObservableCollection<GeneralServicesListModel> _fillingFrquencyMenuList { get; set; }
        public ObservableCollection<GeneralServicesListModel> FillingFrquencyMenuList
        {
            get
            {
                return _fillingFrquencyMenuList;
            }

            set
            {
                _fillingFrquencyMenuList = value;
                RaisePropertyChanged("FillingFrquencyMenuList");
            }
        }
        
        public ICommand GoBackBtnTapped { get; set; }

        public GeneralServicesViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        
        public void PopulateGeneralServicesListData()
        {
            List<GeneralServicesListModel> generalServicesListData = new List<GeneralServicesListModel>();
            string fileImage = string.Empty;
            if(App.IsArabic)
            {
                fileImage = "arrowLeft.png";
            }
            else
            {
                fileImage = "arrowRight.png";

            }

            generalServicesListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.NDVATRegistrationVerification,
                    ZDImageSource = "request_verification.png",
                    ArrowImageSource = fileImage
                });
            
            generalServicesListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.NDTaxEvasionReport,
                    ZDImageSource = "tax_evasion_green.png",
                    ArrowImageSource = fileImage
                });

            if(App.LoginDataRetrieved.TpMpVip.Equals("X"))
            {
                generalServicesListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.NDRelationContact,
                    ZDImageSource = "tax_evasion_green.png",
                    ArrowImageSource = fileImage
                });
            }
           

                GeneralServicesList = new ObservableCollection<GeneralServicesListModel>(generalServicesListData);
        }
        
        public void PopulateRefundRequestMenuListData()
        {
            List<GeneralServicesListModel> refundRequestMenuListData = new List<GeneralServicesListModel>();
            string fileImage = string.Empty;
            if(App.IsArabic)
            {
                fileImage = "arrowLeft.png";
            }
            else
            {
                fileImage = "arrowRight.png";

            }

            if (App.LoginDataRetrieved.VtReg == "X")
            {
                refundRequestMenuListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.VATRefundsRequest,
                    ZDImageSource = "ic_RefundR.png",
                    ArrowImageSource = fileImage
                });
            }

            RefundRequestMenuList = new ObservableCollection<GeneralServicesListModel>(refundRequestMenuListData);
        }
        
        public void PopulateFillingFrequencyListData()
        {
            List<GeneralServicesListModel> fillingFrequencyListData = new List<GeneralServicesListModel>();
            string fileImage = string.Empty;
            if(App.IsArabic)
            {
                fileImage = "arrowLeft.png";
            }
            else
            {
                fileImage = "arrowRight.png";

            }

            if (App.LoginDataRetrieved.VtReg == "X")
            {
                fillingFrequencyListData.Add(new GeneralServicesListModel
                {
                    ZDTitle = AppResources.ChangeVatFillingFrequency,
                    ZDImageSource = "ic_vat_return.png",
                    ArrowImageSource = fileImage
                });
            }

            FillingFrquencyMenuList = new ObservableCollection<GeneralServicesListModel>(fillingFrequencyListData);
        }
    }
}