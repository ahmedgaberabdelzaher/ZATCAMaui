using EGAZT;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.FAQPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class FAQPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets a collection of values to be displayed in the FAQ page.
        /// </summary>
        [DataMember(Name = "questions")]
        public ObservableCollection<FAQ> Questions { get; set; }
        public ObservableCollection<FAQ> DummyQuestions { get; set; }
        private TERFAQs _tERFFAQRoot;
        public TERFAQs TERFFAQRoot
        {
            get
            {
                return _tERFFAQRoot;
            }
            set
            {
                _tERFFAQRoot = value;
                RaisePropertyChanged("TERFFAQRoot");
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
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }



        private string _webUrl = string.Empty;
        public string WebUrl
        {
            get
            {
                return _webUrl;
            }
            set
            {
                _webUrl = value;
                RaisePropertyChanged("WebUrl");
            }
        }

        #endregion
        public FAQPageViewModel(INavigationService navigationService, IDialogService dialogService) 
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();
            });
        }
        #region Method
        public async Task OnPageLoad()
        {
            try
            {
                TERFFAQRoot = new TERFAQs();
                TERFFAQRoot = await WebServiceManager.GAZTTESFAQRetrive();
                PopToRootPage();
                if (TERFFAQRoot!=null && TERFFAQRoot.FAQList.Count!=0 && TERFFAQRoot.Success==true)
                {
                    FAQ fAQ = null;
                    List<string> AnswersList = null;
                    Questions = new ObservableCollection<FAQ>();
                    DummyQuestions = new ObservableCollection<FAQ>();
                    if (App.IsArabic)
                    {
                        foreach (var QAndAn in TERFFAQRoot.FAQList)
                        {
                            fAQ = new FAQ();
                            AnswersList = new List<string>();
                            fAQ.Question = QAndAn.QuestionAR;
                            AnswersList.Add(QAndAn.AnswerAR);
                            fAQ.Answer = AnswersList;
                            DummyQuestions.Add(fAQ);
                        }
                    }
                    else
                    {
                        foreach (var QAndAn in TERFFAQRoot.FAQList)
                        {
                            fAQ = new FAQ();
                            AnswersList = new List<string>();
                            fAQ.Question = QAndAn.QuestionEN;
                            AnswersList.Add(QAndAn.AnswerEN);
                            fAQ.Answer = AnswersList;
                            DummyQuestions.Add(fAQ);
                        }
                    }
                    Questions = DummyQuestions;
                }
                if (Questions==null)
                {
                    IsNoDataLabelVisible = true;
                }
                else
                {
                    if (Questions.Count == 0)
                    {
                        IsNoDataLabelVisible = true;
                    }
                    else
                    {
                        IsNoDataLabelVisible = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Device.BeginInvokeOnMainThread(async() =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.ZError);
                    _navigationService.GoBack();
                });
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFLoginPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFLoginPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        #endregion
    }
}
