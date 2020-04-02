using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.SyncFusionEnabledViewModel.FAQPage
{
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



        private TERFFAQObject _tERFFAQRoot;
        public TERFFAQObject TERFFAQRoot
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

        public void OnPageLoad()
        {
            try
            {
                TERFFAQRoot = new TERFFAQObject();
                TERFFAQRoot = WebServiceManager.GAZTTESFAQRetrive();
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

               
            }
            catch(Exception ex)
            {

            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    _navigation.PopToRootAsync();
                });
            }
        }
        #endregion

    }
}
