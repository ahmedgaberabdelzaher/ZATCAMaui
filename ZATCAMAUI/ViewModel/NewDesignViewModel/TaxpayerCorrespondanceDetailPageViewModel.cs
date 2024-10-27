
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;
using static ZATCAMAUI.Models.correspdncAttchModel;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class TaxpayerCorrespondanceDetailPageViewModel : BaseViewModel
    {
        public ICommand OnBackButtonClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnFavClicked { get; set; }
        public ICommand OnAppearingTaxpayerCorrespondanceDetailCommand { get; set; }
        private string _correspondenceTitle = string.Empty;

        private ObservableCollection<CorrDetails> _AttChDtlsSet;
        public ObservableCollection<CorrDetails> attChDtlsSet
        {
            get
            {
                return _AttChDtlsSet;
            }
            set
            {
                if (_AttChDtlsSet == value) return;
                _AttChDtlsSet = value;
                OnPropertyChanged("attChDtlsSet");
            }

        }
        public correspdncAttchModel _attchModel;
        public correspdncAttchModel AttchModel
        {
            get
            {
                return _attchModel;
            }
            set
            {
                if (_attchModel == value) return;
                _attchModel = value;
                OnPropertyChanged("attchModel");
            }
        }
        public string CorrespondenceTitle
        {
            get
            {
                return _correspondenceTitle;
            }
            set
            {
                if (_correspondenceTitle == value) return;
                _correspondenceTitle = value;
                OnPropertyChanged("CorrespondenceTitle");
            }
        }
        private string _correspondenceDateTime = string.Empty;
        public string CorrespondenceDateTime
        {
            get
            {
                return _correspondenceDateTime;
            }
            set
            {
                if (_correspondenceDateTime == value) return;

                _correspondenceDateTime = value;
                OnPropertyChanged("CorrespondenceDateTime");
            }
        }
        private string _correspondenceTime = string.Empty;
        public string CorrespondenceTime
        {
            get
            {
                return _correspondenceTime;
            }
            set
            {
                if (_correspondenceTime == value) return;

                _correspondenceTime = value;
                OnPropertyChanged("CorrespondenceTime");
            }
        }

        private bool _isFavoriteVisible = false;
        public bool IsFavoriteVisible
        {
            get
            {
                return _isFavoriteVisible;
            }
            set
            {
                if (_isFavoriteVisible == value) return;

                _isFavoriteVisible = value;
                OnPropertyChanged("IsFavoriteVisible");
            }
        }
        private CorrespondanceModel _correspondenceD = null;
        public CorrespondanceModel CorrespondenceD
        {
            get
            {
                return _correspondenceD;
            }
            set
            {
                if (_correspondenceD == value) return;

                _correspondenceD = value;
                OnPropertyChanged("CorrespondenceD");
            }
        }

        private HtmlWebViewSource corWebViewSource;
        public HtmlWebViewSource CorWebViewSource
        {
            get
            {
                return corWebViewSource;
            }
            set
            {
                if (corWebViewSource == value) return;

                corWebViewSource = value;
                OnPropertyChanged("CorWebViewSource");
            }
        }

        private CorrespondanceModel corrModel;
        public CorrespondanceModel CorrModel
        {
            get
            {
                return corrModel;
            }
            set
            {
                if (corrModel == value) return;

                corrModel = value;
                OnPropertyChanged("CorrModel");
            }
        }
        private string _favIcon = string.Empty;
        public string FavIcon
        {
            get
            {
                return _favIcon;
            }
            set
            {
                if (_favIcon == value) return;

                _favIcon = value;
                OnPropertyChanged("FavIcon");
            }
        }
        private bool _isAttachmentEnable = false;
        public bool IsAttachmentEnabled
        {
            get
            {
                return _isAttachmentEnable;
            }
            set
            {
                if (_isAttachmentEnable == value) return;

                _isAttachmentEnable = value;
                OnPropertyChanged("IsAttachmentEnabled");
            }
        }
        public TaxpayerCorrespondanceDetailPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
            OnAttachmentClick = new Command(async () =>
            {
                if (CorrespondenceD != null)
                {
                    if (IsAttachmentEnabled)
                    {
                        string Url = ZATCAConstants.GAZTGetCorrespondenceAttach +  CorrespondenceD.Cokey + "&correspondenceType=" + CorrespondenceD.Cotype ;
                       await ShowPdf(Url);
                    }
                }
            });

            OnAppearingTaxpayerCorrespondanceDetailCommand = new Command(async () =>
            {
                IsFavoriteVisible = false;
                CorrespondenceDetailsRootObject CorrespondenceD = new CorrespondenceDetailsRootObject();
                if (CorrModel != null)
                {
                    CorrespondenceTitle = CorrModel.Title;
                    CorrespondenceDateTime = CorrModel.DateToDisplay;
                    CorrespondenceTime = CorrModel.TimeToDisplay;
                    if (string.IsNullOrEmpty(CorrModel.TaxtpFg))
                    {
                        IsFavoriteVisible = true;
                    }
                    else
                    {
                        IsFavoriteVisible = false;
                    }
                }
                try
                {
                    IsAttachmentEnabled = false;
                    CorrespondenceD = await WebServiceManager.GAZTGetCorrespondeceDetails(CorrModel);
                    if (CorrespondenceD != null && CorrespondenceD.d != null && CorrespondenceD.d.results != null)
                    {
                        string response = CorrespondenceD.d.results.LastOrDefault().Attfg;
                        if (response.Equals("X"))
                        {
                            IsAttachmentEnabled = true;
                        }
                        else
                        {
                        }
                    }
                    await PopToRootPage();

                }
                catch (InternetException ex)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                }
                string HTMLContent = string.Empty;
                string HTMLContentTest = string.Empty;
                if (CorrespondenceD != null && CorrespondenceD.d != null && CorrespondenceD.d.results != null)
                {
                    foreach (CorrespondenceDetailsResult ItemC in CorrespondenceD.d.results)
                    {
                        HTMLContent = HTMLContent + ItemC.Tdline;
                    }
                    string newHTMLContent = HTMLContent.Replace("<img ", "<img src='ic_GAZT_Logo_Text.png' width='40%' ");

                    newHTMLContent = newHTMLContent.Replace("FABB33", "0996d4");


                    newHTMLContent = newHTMLContent.Replace("</html>", "<head><style type='text/css'>@font-face {font-family: MyFont;src:url('Somar-Regular.otf') format('opentype');}body { font-family: MyFont }</style></head></html>");


                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        string newHTMLForFonts = newHTMLContent.Replace("<body>", "<body style='font-size:40px;margin:15;color:#042e66'>");
                        var htmlSource = new HtmlWebViewSource();
                        htmlSource.Html = newHTMLForFonts;
                        htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
                        CorWebViewSource = htmlSource;
                    }
                    else
                    {
                        string newHTMLForFonts = newHTMLContent.Replace("<body>", "<body style='font-size:16px;margin:10;color:#042e66'>");
                        var htmlSource = new HtmlWebViewSource();
                        htmlSource.Html = newHTMLForFonts;
                        htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
                        CorWebViewSource = htmlSource;
                    }
                }
                if (CorrModel != null)
                {
                    CorrespondenceTitle = CorrModel.Title;
                    this.CorrespondenceD = CorrModel;
                    if (CorrModel.IsFav == true)
                    {
                        FavIcon = "ic_star.png";
                    }
                    else
                    {
                        FavIcon = "ic_star_border.png";
                    }
                }
            });
            OnFavClicked = new Command(async () =>
            {
                if (CorrespondenceD.IsFav == false)
                {
                    CorrespondenceFavoriteModel FavoriteM = new CorrespondenceFavoriteModel();
                    DateTime formattedBegdaz = DateTime.Parse(CorrespondenceD.Begdaz);
                    FavoriteM.Begdaz = formattedBegdaz.ToString("yyyy-MM-ddTHH:mm:ss");
                    FavoriteM.Cokey = CorrespondenceD.Cokey;
                    FavoriteM.Cotyp = CorrespondenceD.Cotype;
                    DateTime formattedEnddaz = DateTime.Parse(CorrespondenceD.Enddaz);
                    FavoriteM.Enddaz = formattedEnddaz.ToString("yyyy-MM-ddTHH:mm:ss");
                    FavoriteM.Gpart = CorrespondenceD.Gpart;
                    FavoriteM.Zzfav = true;
                    FavoriteM.Vkont = CorrespondenceD.Vkont;
                    string result = await WebServiceManager.GAZTSetFavCorrespondence(FavoriteM);
                    CorrespondenceD.IsFav = true;
                    FavIcon = "ic_star.png";
                }
                else if (CorrespondenceD.IsFav == true)
                {
                    CorrespondenceFavoriteModel FavoriteM = new CorrespondenceFavoriteModel();
                    DateTime formattedBegdaz = DateTime.Parse(CorrespondenceD.Begdaz);
                    FavoriteM.Begdaz = formattedBegdaz.ToString("yyyy-MM-ddTHH:mm:ss");
                    FavoriteM.Cokey = CorrespondenceD.Cokey;
                    FavoriteM.Cotyp = CorrespondenceD.Cotype;
                    DateTime formattedEnddaz = DateTime.Parse(CorrespondenceD.Enddaz);
                    FavoriteM.Enddaz = formattedEnddaz.ToString("yyyy-MM-ddTHH:mm:ss");
                    FavoriteM.Gpart = CorrespondenceD.Gpart;
                    FavoriteM.Zzfav = false;
                    FavoriteM.Vkont = CorrespondenceD.Vkont;
                    string result = await WebServiceManager.GAZTSetFavCorrespondence(FavoriteM);
                    CorrespondenceD.IsFav = false;
                    FavIcon = "ic_star_border.png";
                }
            });
        }
        public void ButtonEnable(string response)
        {
            if (response.Equals("X"))
            { IsAttachmentEnabled = true; }
            else { IsAttachmentEnabled = false; }
        }
        public async Task ShowPdf(string pdfUrl)
        {
            IsLoading = false;
            if (pdfUrl != null)
            {
               await _navigationService.NavigateTo(App.PdfView, pdfUrl);
            }
            else
            {
                //pop that certificate is not available
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
            }
            IsLoading = false;
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }
    }

}
