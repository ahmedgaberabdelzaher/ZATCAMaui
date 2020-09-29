using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
   public  class TaxpayerCorrespondanceDetailPageViewModel : BaseViewModel
    {
        public ICommand OnBackButtonClicked { get; set; }
        public ICommand OnAttachmentClick { get; set; }
        public ICommand OnFavClicked { get; set; }
        private string _correspondenceTitle = string.Empty;
        public string CorrespondenceTitle
        {
            get
            {
                return _correspondenceTitle;
            }
            set
            {
                _correspondenceTitle = value;
                RaisePropertyChanged("CorrespondenceTitle");
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
                _correspondenceDateTime = value;
                RaisePropertyChanged("CorrespondenceDateTime");
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
                _correspondenceTime = value;
                RaisePropertyChanged("CorrespondenceTime");
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
                _isFavoriteVisible = value;
                RaisePropertyChanged("IsFavoriteVisible");
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
                _correspondenceD = value;
                RaisePropertyChanged("CorrespondenceD");
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
                _favIcon = value;
                RaisePropertyChanged("FavIcon");
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
                _isAttachmentEnable = value;
                RaisePropertyChanged("IsAttachmentEnabled");
            }
        }
        public TaxpayerCorrespondanceDetailPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
            OnAttachmentClick = new Xamarin.Forms.Command(() =>
            {
                if (CorrespondenceD != null)
                {
                    if (IsAttachmentEnabled)
                    {
                        string Url = Constants.GAZTGetCorrespondenceAttach + "'" + CorrespondenceD.Cokey + "',Cotyp='" + CorrespondenceD.Cotype + "')/$value?saml2=disabled";
                        ShowPdf(Url);
                    }
                }
            });
            OnFavClicked = new Xamarin.Forms.Command(async () =>
            {
                if (CorrespondenceD.IsFav == false)
                {
                    CorrespondenceFavoriteModel FavoriteM = new CorrespondenceFavoriteModel();
                    FavoriteM.Begdaz = CorrespondenceD.Begdaz;
                    FavoriteM.Cokey = CorrespondenceD.Cokey;
                    FavoriteM.Cotyp = CorrespondenceD.Cotype;
                    FavoriteM.Enddaz = CorrespondenceD.Enddaz;
                    FavoriteM.Gpart = CorrespondenceD.Gpart;
                    FavoriteM.Zzfav = "1";
                    FavoriteM.Vkont = CorrespondenceD.Vkont;
                    string result = WebServiceManager.GAZTSetFavCorrespondence(FavoriteM);
                    CorrespondenceD.IsFav = true;
                    FavIcon = "ic_star.png";
                }
                else if (CorrespondenceD.IsFav == true)
                {
                    CorrespondenceFavoriteModel FavoriteM = new CorrespondenceFavoriteModel();
                    FavoriteM.Begdaz = CorrespondenceD.Begdaz;
                    FavoriteM.Cokey = CorrespondenceD.Cokey;
                    FavoriteM.Cotyp = CorrespondenceD.Cotype;
                    FavoriteM.Enddaz = CorrespondenceD.Enddaz;
                    FavoriteM.Gpart = CorrespondenceD.Gpart;
                    FavoriteM.Zzfav = "0";
                    FavoriteM.Vkont = CorrespondenceD.Vkont;
                    string result = WebServiceManager.GAZTSetFavCorrespondence(FavoriteM);
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
        public async void ShowPdf(string pdfUrl)
        {
            await Task.Run(() =>
            {
                IsLoading = false;
            });
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
                    //await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                });
            }
            await Task.Run(() =>
            {
               IsLoading = false;
            });
            //}
        }
    }
    
}
