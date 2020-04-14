using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.NewViewModel
{
    public class AddNotePageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static string NoteString = string.Empty;
        public static bool ClearNoteClicked = false;
        public static int NoteCount = 0;
        public static bool IsClearAndCloseForDraft = false;
        public static bool IsComingFromNotePage = false;
        public ICommand OnAddButtonClicked { get; set; }
        public ICommand OnClearButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        #endregion

        #region Property

        private VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                _vATDeclarationData = value;
                RaisePropertyChanged("VATDeclarationData");
            }
        }

        private string _noteText;
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;
                if(!string.IsNullOrEmpty(_noteText))
                {
                    NoteString = _noteText;
                }
                RaisePropertyChanged("NoteText");
            }
        }

        private string _previousNoteText;
        public string PreviousNoteText
        {
            get
            {
                return _previousNoteText;
            }
            set
            {
                _previousNoteText = value;
                RaisePropertyChanged("PreviousNoteText");
            }
        }


        #endregion

        #region Constructor

        public AddNotePageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;


            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();


            });
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }


            OnClearButtonClicked = new Command(() =>
            {
                //if (!string.IsNullOrEmpty(NoteText))
                //{
                //    NoteText = PreviousNoteText;
                //}
                //else
                //{
                //    NoteText = string.Empty;
                //}

                if (String.Compare(PreviousNoteText, NoteText) != 0)
                {
                    NoteText = PreviousNoteText;
                }

                ClearNoteClicked = true;
                _navigationService.GoBack();
            });

            OnAddButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });

        }


        #endregion

        #region Method
        #endregion
    }
}
