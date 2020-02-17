using System;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace GAZT
{
    public class PdfiOSViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public string pdfUrl;
        #endregion

        #region Property

        private bool _isLoading;
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
        private string _localPath = null;
        public string LocalPath
        {
            get
            {
                return _localPath;
            }
            set
            {
                _localPath = value;
                RaisePropertyChanged("LocalPath");
            }
        }

        private Stream _StreamForDownloadURL = null;
        public Stream StreamForDownloadURL
        {
            get
            {
                return _StreamForDownloadURL;
            }
            set
            {
                _StreamForDownloadURL = value;
                RaisePropertyChanged("StreamForDownloadURL");
            }
        }

        #endregion

        #region Constructor

        public PdfiOSViewModel(INavigationService navigationService, IDialogService dialogService)
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




        }

        #endregion

    }
}
