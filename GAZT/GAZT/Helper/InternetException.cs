using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Helper
{
    [Preserve(AllMembers = true)]
    [Serializable]
    public class InternetException : Exception
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public InternetException()
        {
        }
        public InternetException(string ExceptionMessage)
            : base(ExceptionMessage)
        {
        }
    }
}
