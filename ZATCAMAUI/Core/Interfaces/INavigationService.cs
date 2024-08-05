namespace ZATCAMAUI.Core.Interfaces
{
    public interface INavigationService
    {
        void PopToRootPage();
        void GoBack();
        void NavigateTo(string pageKey);
        void NavigateTo(string pageKey, object parameter);
        void NavigateToWithBack(string pageKey, object parameter);
    }
}