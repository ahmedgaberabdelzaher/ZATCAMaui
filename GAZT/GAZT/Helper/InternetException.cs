using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
namespace GAZT.Helper
{
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
