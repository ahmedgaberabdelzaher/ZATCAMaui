using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class NotesPopUpPageViewModel : BaseViewModel
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
                if (!string.IsNullOrEmpty(_noteText))
                {
                    NoteString = _noteText;
                }
                else
                {
                    NoteString = string.Empty;
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
        public NotesPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
                if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote == true)
                {
                    if (string.IsNullOrEmpty(NoteText))
                    {

                    }
                    else
                    {
                        GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote = false;
                    }
                }
                if (String.Compare(PreviousNoteText, NoteText) != 0)
                {
                    NoteText = PreviousNoteText;
                }
                ClearNoteClicked = true;
                MessagingCenter.Send<Object, string>(this, "ClearNoteForVATDeclaration", "ClearNoteForVATDeclaration");
                PopupNavigation.Instance.PopAsync();
            });
            OnAddButtonClicked = new Command(() =>
            {
                if (GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote == true)
                {
                    if (string.IsNullOrEmpty(NoteText))
                    {

                    }
                    else
                    {
                        GAZTNewDesignVATReturnUpdatedUIPageViewModel.IsFirstTimeForNote = false;
                    }
                }
                MessagingCenter.Send<Object, string>(this, "AddNoteForVATDeclaration", "AddNoteForVATDeclaration");
                PopupNavigation.Instance.PopAsync();
            });
        }
        #endregion
        #region Method
        #endregion

        //public NotesPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        //{

        //}
    }
}
