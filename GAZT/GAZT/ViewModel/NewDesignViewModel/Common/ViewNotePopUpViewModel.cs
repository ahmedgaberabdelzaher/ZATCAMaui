using System;
using System.Collections.Generic;
using System.Windows.Input;
using EGAZT.Models.VATInstalationModels;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.Common
{
    [Preserve(AllMembers = true)]
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
        public ViewNotePopUpViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
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