using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.VATInstalmentModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Common
{

    public class ViewNotePopUpViewModel : BaseViewModel
    {
        #region Variable
        public ICommand GoBackClick { get; set; }
        // public ICommand OnSubmitClicked { get; set; }
        #endregion
        #region Property
        private List<NotesSetResult> _noteList;
        public List<NotesSetResult> NoteList
        {
            get
            {
                return _noteList;
            }
            set
            {
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
                _isDisplayNoteVisible = value;
                OnPropertyChanged("IsDisplayNoteVisible");
            }
        }
        #endregion
        #region Constructor
        public ViewNotePopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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