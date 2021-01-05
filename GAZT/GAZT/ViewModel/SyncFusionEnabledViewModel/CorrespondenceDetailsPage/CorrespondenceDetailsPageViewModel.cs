using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.CorrespondenceDetailsPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class CorrespondenceDetailsPageViewModel : ViewModelBase
    {
        #region Properties
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
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
                RaisePropertyChanged("IsAttachmentEnable");
            }
        }
        #endregion
        #region Constructor
        public CorrespondenceDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        { 
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            OnAttachmentClick = new Xamarin.Forms.Command(() =>
            {
                if (CorrespondenceD != null)
                {
                    if(IsAttachmentEnabled)
                    {
                        string Url = Constants.GAZTGetCorrespondenceAttach + "'" + CorrespondenceD.Cokey + "',Cotyp='" + CorrespondenceD.Cotype + "')/$value?saml2=disabled";
                        ShowPdf(Url);
                    }
                }
            });
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
            OnFavClicked = new Xamarin.Forms.Command(async () =>
            {
               if(CorrespondenceD.IsFav==false)
                {
                    CorrespondenceFavoriteModel FavoriteM = new CorrespondenceFavoriteModel();
                    FavoriteM.Begdaz = CorrespondenceD.Begdaz   ;
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
        #endregion
        #region Methods
        public void ButtonEnable(string response)
        { if (response.Equals("X"))
                { IsAttachmentEnabled = true; }
            else { IsAttachmentEnabled = false; }
        }
        public async void ShowPdf(string pdfUrl)
        {
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    if (pdfUrl != null)
            //    {
            //        //Uri uri = new Uri(pdfUrl);
            //        //Device.OpenUri(uri);
            //        _navigationService.NavigateTo(App.PdfiOSView, pdfUrl);
            //    }
            //    else
            //    {
            //        //pop that certificate is not available
            //        Device.BeginInvokeOnMainThread(async () =>
            //        {
            //            await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
            //        }); 
            //    }
            //}
            //else
            //{
                if (pdfUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    //pop that certificate is not available
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                    });
                }
            //}
        }
        #endregion
    }
}
