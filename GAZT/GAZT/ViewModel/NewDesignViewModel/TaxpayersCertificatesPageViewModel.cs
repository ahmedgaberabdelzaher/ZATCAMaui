using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
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
                if (_SelectedTaxTypeForFilter == value) return;
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
                if (_isLoading == value) return;

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
                if (_selectedCertificate == value) return;

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
                if (_filterLabelText == value) return;

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
                if (_TaxTypeForFilter == value) return;

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
                if (_certificateListToDisplay == value) return;

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
        private bool _isNoDataLabelVisible = true;
        public bool IsNoDataLabelVisible
        {
            get
            {
                return _isNoDataLabelVisible;
            }
            set
            {
                if (_isNoDataLabelVisible == value) return;

                _isNoDataLabelVisible = value;
                RaisePropertyChanged("IsNoDataLabelVisible");
            }
        }
        private bool _isCertificateListVisible = false;
        public bool IsCertificateListVisible
        {
            get
            {
                return _isCertificateListVisible;
            }
            set
            {
                if (_isCertificateListVisible == value) return;

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
                if (_certificateListToAll == value) return;

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
                if (_certificateListToZAKAT == value) return;

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
                if (_certificateListToVAT == value) return;

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
                if (_certificateListToET == value) return;

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
                if (_TaxPayerProfile == value) return;

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
                PopToRootPage();
                if (allCertificate != null)
                {
                    if (allCertificate.ZakatSet != null && allCertificate.ZakatSet.results != null && allCertificate.ZakatSet.results.Count > 0)
                    {
                        CertificateListToZAKAT = allCertificate.ZakatSet.results;
                        foreach (var Item in CertificateListToZAKAT)
                        {
                            CertificateListToAll.Add(Item);
                        }
                      
                    }
                    if (allCertificate.VATSet != null && allCertificate.VATSet.results != null && allCertificate.VATSet.results.Count > 0)
                    {
                        CertificateListToVAT = allCertificate.VATSet.results;
                        foreach (var Item in CertificateListToVAT)
                        {
                            CertificateListToAll.Add(Item);
                        }
                    }
                    if (allCertificate.ExciseSet != null && allCertificate.ExciseSet.results != null && allCertificate.ExciseSet.results.Count > 0)
                    {
                        CertificateListToET = allCertificate.ExciseSet.results;
                        foreach (var Item in CertificateListToET)
                        {
                            CertificateListToAll.Add(Item);
                        }
                    }
                 SelectedTaxTypeForFilter = TaxTypeForFilter.Where(x => x.Id == "00").FirstOrDefault();
                }
                else
                {
                    
                }
            }
            catch (InternetException ex)
            {
                //_dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                _navigationService.GoBack();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                //   _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
            }
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
        public void PopulateCirtificateTypeList()
        {
            try
            {
                TaxTypeForFilter = new List<ReturnTypes>
                {
                        new ReturnTypes {Id = "00",TaxType = AppResources.NDAllcertificates},
                        new ReturnTypes {Id = "01",TaxType = AppResources.ZakatCertificates},
                        new ReturnTypes {Id = "02",TaxType = AppResources.VATCertificates},
                        new ReturnTypes {Id = "03",TaxType = AppResources.ExciseCertificates},
                        //new ReturnTypes {Id = "04",TaxType = AppResources.ZZWithholding},
                };
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                        _navigationService.NavigateTo(App.PdfView, pdfUrl);
                    }
                    else
                    {
                     
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = false;
                            //await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
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
                       
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            IsLoading = false;
                       //     await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                        });
                    }
                }
    

           
        }
        #endregion
    }
}
