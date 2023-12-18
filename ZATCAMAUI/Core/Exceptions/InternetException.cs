using GalaSoft.MvvmLight.Views;

namespace ZATCAMAUI.Core.Exceptions
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
