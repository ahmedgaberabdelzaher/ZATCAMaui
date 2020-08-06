using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    
    public class TaxpayersCertificatesPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBackButtonClicked { get; set; }
        bool isMendatoryDataEntered = true;

        #region proprety
        public AllCertificate allCertificate { get; set; }
        private TaxPayerProfile _TaxPayerProfile = App.TP;
        
        public List<ReturnTypes> _TaxTypeForFilter = null;
        public ReturnTypes _SelectedTaxTypeForFilter = null;
        public ReturnTypes SelectedTaxTypeForFilter
        {
            get
            {
                return _SelectedTaxTypeForFilter;
            }
            set
            {
                _SelectedTaxTypeForFilter = value;
                if (_SelectedTaxTypeForFilter != null)
                {
                    FilterLabelText = _SelectedTaxTypeForFilter.TaxType;
                    FilterCertificateOnBasisOfType();
                }
                RaisePropertyChanged("SelectedTaxTypeForFilter");
            }
        }
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        private Result _selectedCertificate;
        public Result SelectedCertificate
        {
            get
            {
                return _selectedCertificate;
            }
            set
            {
                _selectedCertificate = value;
                RaisePropertyChanged("SelectedCertificate");
                if (SelectedCertificate != null && SelectedCertificate.Pdfurl != null)
                {
                    ShowPdf(SelectedCertificate.Pdfurl);
                }
            }
        }
        public string _filterLabelText;
        public string FilterLabelText
        {
            get
            {
                return _filterLabelText;
            }
            set
            {
                _filterLabelText = value;

                RaisePropertyChanged("FilterLabelText");
            }
        }
        public List<ReturnTypes> TaxTypeForFilter
        {
            get
            {
                return _TaxTypeForFilter;
            }
            set
            {
                _TaxTypeForFilter = value;
                RaisePropertyChanged("TaxTypeForFilter");
            }
        }
        private ObservableCollection<Result> _certificateListToDisplay=null;
        public ObservableCollection<Result> CertificateListToDisplay
        {
            get
            {
                return _certificateListToDisplay;
            }
            set
            {
                _certificateListToDisplay = value;
                if (_certificateListToDisplay != null)
                {
                    if (_certificateListToDisplay.Count() > 0)
                    {
                        IsCertificateListVisible = true;
                        IsNoDataLabelVisible = false;
                    }
                    else
                    {
                        IsCertificateListVisible = false;
                        IsNoDataLabelVisible = true;
                    }

                }
                else
                {
                    IsCertificateListVisible = false;
                    IsNoDataLabelVisible = true;
                }
                RaisePropertyChanged("CertificateListToDisplay");
            }
        }
        private bool _isNoDataLabelVisible = false;
        public bool IsNoDataLabelVisible
        {
            get
            {
                return _isNoDataLabelVisible;
            }
            set
            {
                _isNoDataLabelVisible = value;
                RaisePropertyChanged("IsNoDataLabelVisible");
            }
        }
        private bool _isCertificateListVisible = true;
        public bool IsCertificateListVisible
        {
            get
            {
                return _isCertificateListVisible;
            }
            set
            {
                _isCertificateListVisible = value;
                RaisePropertyChanged("IsCertificateListVisible");
            }
        }
        private List<Result> _certificateListToAll;
        public List<Result> CertificateListToAll
        {
            get
            {
                return _certificateListToAll;
            }
            set
            {
                _certificateListToAll = value;
                RaisePropertyChanged("CertificateListToAll");
            }
        }
        private List<Result> _certificateListToZAKAT;
        public List<Result> CertificateListToZAKAT
        {
            get
            {
                return _certificateListToZAKAT;
            }
            set
            {
                _certificateListToZAKAT = value;
                RaisePropertyChanged("CertificateListToZAKAT");
            }
        }
        private List<Result> _certificateListToVAT;
        public List<Result> CertificateListToVAT
        {
            get
            {
                return _certificateListToVAT;
            }
            set
            {
                _certificateListToVAT = value;
                RaisePropertyChanged("_certificateListToVAT");
            }
        }
        private List<Result> _certificateListToET;
        public List<Result> CertificateListToET
        {
            get
            {
                return _certificateListToET;
            }
            set
            {
                _certificateListToET = value;
                RaisePropertyChanged("_certificateListToET");
            }
        }
        public TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return _TaxPayerProfile;
            }
            set
            {
                _TaxPayerProfile = value;
                RaisePropertyChanged("TaxPayerProfile");
            }
        }
        
        #endregion
        public TaxpayersCertificatesPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });

        }

        #region Methods

        public void OnPageLoad()
        {
            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                TaxPayerProfile = App.TP;
                CertificateListToDisplay = new ObservableCollection<Result>();
                CertificateListToZAKAT = new List<Result>();
                CertificateListToVAT = new List<Result>();
                CertificateListToET = new List<Result>();
                CertificateListToAll = new List<Result>();
                allCertificate = WebServiceManager.GAZTGetAllCertificate(lang, App.TP.Userid);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                bool Flag = false;
                if (allCertificate != null)
                {
                    if (allCertificate.ZakatSet != null && allCertificate.ZakatSet.results != null && allCertificate.ZakatSet.results.Count > 0)
                    {
                        CertificateListToZAKAT = allCertificate.ZakatSet.results;
                        //CertificateListZakat = allCertificate.ZakatSet.results;
                        //SelectedTab = 0;
                        //Flag = true;
                        //IsCertificateAvailableZakat = true;
                        //SetNoDataLabelVisibilityZakat = false;     foreach (var Item in CertificateListToZAKAT)
                        foreach (var Item in CertificateListToZAKAT)
                        {
                            CertificateListToAll.Add(Item);
                        }
                      
                    }
                    else
                    {
                        //IsCertificateAvailableZakat = false;
                        //SetNoDataLabelVisibilityZakat = true;
                    }
                    if (allCertificate.VATSet != null && allCertificate.VATSet.results != null && allCertificate.VATSet.results.Count > 0)
                    {
                        CertificateListToVAT = allCertificate.VATSet.results;
                        foreach (var Item in CertificateListToVAT)
                        {
                            CertificateListToAll.Add(Item);
                        }
                      
                        //  SetCertificateListViewVisibility();
                        //CertificateListVAT = allCertificate.VATSet.results;
                        //if (Flag == false)
                        //{
                        //    SelectedTab = 1;
                        //}
                        //IsCertificateAvailableVAT = true;
                        //SetNoDataLabelVisibilityVAT = false;
                    }
                    else
                    {
                        //IsCertificateAvailableVAT = false;
                        //SetNoDataLabelVisibilityVAT = true;
                    }
                    if (allCertificate.ExciseSet != null && allCertificate.ExciseSet.results != null && allCertificate.ExciseSet.results.Count > 0)
                    {
                        CertificateListToET = allCertificate.ExciseSet.results;
                        foreach (var Item in CertificateListToVAT)
                        {
                            CertificateListToAll.Add(Item);
                        }
                    }
                    else
                    {
                        //    IsCertificateAvailableET = false;
                        //    SetNoDataLabelVisibilityET = true;
                    }
                 SelectedTaxTypeForFilter = TaxTypeForFilter.Where(x => x.Id == "00").FirstOrDefault();
                }
                else
                {

                }
            }
            catch (InternetException ex)
            {
                _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception ex)
            { 
            
            }
            //CertificateType = AppResources.ExciseCertificates;
            //CertificateType = AppResources.ZakatCertificates;
            //CertificateType = AppResources.VATCertificates;
        }
        public void FilterCertificateOnBasisOfType()
        {
            try
            {
                if (SelectedTaxTypeForFilter != null)
                {
                    if (SelectedTaxTypeForFilter.Id != null)
                    {
                        if (SelectedTaxTypeForFilter.Id.Equals("00"))
                        {
                            //CertificateListToDisplay
                       
                            CertificateListToDisplay =new ObservableCollection<Result>(CertificateListToAll);
                        }
                        if (SelectedTaxTypeForFilter.Id.Equals("01"))
                        {
                           
                            CertificateListToDisplay = new ObservableCollection<Result>(CertificateListToZAKAT);
                        }
                        if (SelectedTaxTypeForFilter.Id.Equals("02"))
                        {

                            CertificateListToDisplay = new ObservableCollection<Result>(CertificateListToVAT);
                        }
                        if (SelectedTaxTypeForFilter.Id.Equals("03"))
                        {
                           
                            CertificateListToDisplay = new ObservableCollection<Result>(CertificateListToET); 
                        }
                    }


                }
             
            }
            catch (Exception ex)
            {

            }

        }
        public void PopulateCirtificateTypeList()
        {
            try
            {
                TaxTypeForFilter = new List<ReturnTypes>
                {
                        new ReturnTypes {Id = "00",TaxType = AppResources.All},
                        new ReturnTypes {Id = "01",TaxType = AppResources.ZakatCertificates},
                        new ReturnTypes {Id = "02",TaxType = AppResources.VATCertificates},
                        new ReturnTypes {Id = "03",TaxType = AppResources.ExciseCertificates},
                        //new ReturnTypes {Id = "04",TaxType = AppResources.ZZWithholding},
                };
               // SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();

            }
            catch (Exception ex)
            {
            }


        }
        public async void ShowPdf(string pdfUrl)
        {
           await  Task.Run( () =>
            {
                IsLoading = true;
            });

           
                if (Device.RuntimePlatform == Device.iOS)
                {
                    if (pdfUrl != null)
                    {
                        //Uri uri = new Uri(pdfUrl);
                        //Device.OpenUri(uri);
                        // _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
                        _navigationService.NavigateTo(App.PdfView, pdfUrl);
                    }
                    else
                    {
                        //pop that certificate is not available
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = false;
                            await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                        });
                    }
                }
                else
                {
                    if (pdfUrl != null)
                    {
                        _navigationService.NavigateTo(App.PdfView, pdfUrl);
                    }
                    else
                    {
                        //pop that certificate is not available
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = false;
                            await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                        });
                    }
                }
    

           
        }
        #endregion
    }
}
