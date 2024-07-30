using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Mopups.Services;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Common
{

    public class AddNotePopUpViewModel : BaseViewModel
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAddButtonClicked { get; set; }
        public ICommand OnClearButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        #endregion
        #region Property

        private string _noteText = string.Empty;
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;

                RaisePropertyChanged("NoteText");
            }
        }

        private bool isEdit = false;
        public bool IsEdit
        {
            get
            {
                return isEdit;
            }
            set
            {
                isEdit = value;

                RaisePropertyChanged("IsEdit");
            }
        }

        #endregion
        #region Constructor
        public AddNotePopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            GoBackClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnClearButtonClicked = new Command(() =>
            {
                NoteText = string.Empty;
                MessagingCenter.Send<object, string>(this, "ClearNoteForVATDeclaration", "ClearNoteForVATDeclaration");
                MopupService.Instance.PopAsync();
            });
            OnAddButtonClicked = new Command(() =>
            {

                MessagingCenter.Send<object, string>(this, "AddNoteForVATDeclaration", "AddNoteForVATDeclaration");
                MopupService.Instance.PopAsync();
            });
        }
        #endregion
    }
}