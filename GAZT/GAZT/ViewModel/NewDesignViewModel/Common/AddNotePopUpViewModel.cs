using System;
using System.Collections.Generic;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.VATInstalationModels;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.VATReturnsPageEX;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    [Preserve(AllMembers = true)]
    public class AddNotePopUpViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnAddButtonClicked { get; set; }
        public ICommand OnClearButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        #endregion
        #region Property

        private string _noteText = String.Empty;
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
        public AddNotePopUpViewModel(INavigationService navigationService, IDialogService dialogService)
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
                NoteText = string.Empty;
                MessagingCenter.Send<Object, string>(this, "ClearNoteForVATDeclaration", "ClearNoteForVATDeclaration");
                PopupNavigation.Instance.PopAsync();
            });
            OnAddButtonClicked = new Command(() =>
            {

                MessagingCenter.Send<Object, string>(this, "AddNoteForVATDeclaration", "AddNoteForVATDeclaration");
                PopupNavigation.Instance.PopAsync();
            });
        }
        #endregion
    }
}