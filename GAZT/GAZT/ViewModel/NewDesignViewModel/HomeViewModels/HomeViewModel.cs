using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.HomeViewModels
{
    public enum Tabs
    {
        Home,
        Account,
        Menu
    }

    public class HomeViewModel:BaseViewModel
    {

        string title ;
        public string Title { get { return title; } set { title = value; RaisePropertyChanged(); } }

        int currentTab=1;
        public int CurrentTab { get { return currentTab; } set { currentTab = value; RaisePropertyChanged(); }  }
        public HomeViewModel(INavigationService navigationServices, IDialogService dialogService) : base(navigationServices, dialogService)
        {

        }
        public ICommand ChangeCurrentTabCommand
        {
            get
            {
                return new Command<string>((tab) =>
                {
                    if (tab!=currentTab.ToString())
                    {
                        if (tab=="1")
                        {
                           
                            _navigationService.NavigateTo($"../NavigationPage/{App.SFLoginPageView}", App.GAZTNewDesignDashBoardPageView);
                          
                        }
                        CurrentTab = int.Parse(tab);
                        Title = tab == "0" ? AppResources.Home : AppResources.ZZZMenu;
                    }
              
                });
            }
        }

    }
}
