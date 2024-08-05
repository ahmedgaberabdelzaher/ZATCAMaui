

using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AddNotePage
{

    public class AddNotePageViewModel : BaseViewModel
    {
        #region Variable
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
                OnPropertyChanged("VATDeclarationData");
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
                OnPropertyChanged("NoteText");
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
                OnPropertyChanged("PreviousNoteText");
            }
        }
        #endregion
        #region Constructor
        public AddNotePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command( () =>
            {
                _navigationService.GoBack();
            });
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnClearButtonClicked = new Command(() =>
            {
                if (VATReturnsPageViewModelEX.IsFirstTimeForNote == true)
                {
                    if (string.IsNullOrEmpty(NoteText))
                    {

                    }
                    else
                    {
                        VATReturnsPageViewModelEX.IsFirstTimeForNote = false;
                    }
                }
                if (string.Compare(PreviousNoteText, NoteText) != 0)
                {
                    NoteText = PreviousNoteText;
                }
                ClearNoteClicked = true;
                _navigationService.GoBack();
            });
            OnAddButtonClicked = new Command(() =>
            {
                if (VATReturnsPageViewModelEX.IsFirstTimeForNote == true)
                {
                    if (string.IsNullOrEmpty(NoteText))
                    {

                    }
                    else
                    {
                        VATReturnsPageViewModelEX.IsFirstTimeForNote = false;
                    }
                }
                _navigationService.GoBack();
            });
        }
        #endregion
    }
}
