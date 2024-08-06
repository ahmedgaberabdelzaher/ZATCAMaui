using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.Common
{

    public class AddNotePopUpViewModel : BaseViewModel
    {
        #region Variable
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

                OnPropertyChanged("NoteText");
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

                OnPropertyChanged("IsEdit");
            }
        }

        #endregion
        #region Constructor
        public AddNotePopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
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