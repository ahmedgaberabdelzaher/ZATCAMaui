using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.DisplayNotesPage
{
 
    public class DisplayNotesPageViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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
                _noteList = value;
                RaisePropertyChanged("NoteList");
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
                RaisePropertyChanged("IsNoDataLabelVisible");
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
                RaisePropertyChanged("IsDisplayNoteVisible");
            }
        }
        #endregion
        #region Constructor
        public DisplayNotesPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion
    }
}
