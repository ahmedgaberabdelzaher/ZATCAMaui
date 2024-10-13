namespace ZATCAMAUI.Core.Interfaces
{
    public interface INavigationService
    {
        void PopToRootPage();
        void GoBack();
         Task NavigateTo(string pageKey);
         Task NavigateTo(string pageKey, object parameter);
        void NavigateToWithBack(string pageKey, object parameter);
    }
}