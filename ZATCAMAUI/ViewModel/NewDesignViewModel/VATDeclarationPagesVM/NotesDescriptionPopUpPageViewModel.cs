
using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.VATDeclarationPagesVM
{


    public class NotesDescriptionPopUpPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackClick { get; set; }
        // public ICommand OnSubmitClicked { get; set; }
        #endregion
        #region Property
        private List<Note> _noteList;
        public List<Note> NoteList
        {
            get
            {
                return _noteList;
            }
            set
            {
                if (_noteList == value) return;
                _noteList = value;
                OnPropertyChanged("NoteList");
            }
        }
        private bool _isNoDataLabelVisible;
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
                OnPropertyChanged("IsNoDataLabelVisible");
            }
        }
        private bool _isDisplayNoteVisible;
        public bool IsDisplayNoteVisible
        {
            get
            {
                return _isDisplayNoteVisible;
            }
            set
            {
                if (_isDisplayNoteVisible == value) return;

                _isDisplayNoteVisible = value;
                OnPropertyChanged("IsDisplayNoteVisible");
            }
        }
        #endregion
        #region Constructor
        public NotesDescriptionPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion
        #region Method
        #endregion
    }
}
