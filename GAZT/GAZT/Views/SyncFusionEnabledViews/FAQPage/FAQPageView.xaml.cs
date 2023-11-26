using EGAZT.ViewModel.SyncFusionEnabledViewModel.FAQPage_ViewModel;
using GAZT.Helper;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.FAQPage
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FAQPageView : ContentPage
    {
        FAQPageViewModel viewModel;
        public FAQPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.FAQPageView;
            //viewModel.IsLoading = true;
            // ParentContainer.RaiseChild(busyindicator);
            this.BindingContext = viewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            SetLTR();
            SetUrl();
          //  onPageLoad();
            //List<string> AnswerList = new List<string>();
            //AnswerList.Add("TestAnswer");
            //FAQ faq = new FAQ();
            //faq.Question = "Test";
            //faq.Answer = AnswerList;
            //viewModel.Questions = new System.Collections.ObjectModel.ObservableCollection<FAQ>();
            //viewModel.Questions.Add(faq);
        }

        public void SetUrl()
        {
            if (App.IsArabic)
            {
               // viewModel.WebUrl = "https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
                viewModel.WebUrl = Constants.GAZTFAQARUrl;
            }
            else
            {
                viewModel.WebUrl = Constants.GAZTFAQEnUrl;
            }

        }
        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);
        //    if (width > height)
        //    {
        //        if (Search.IsVisible)
        //        {
        //            Search.WidthRequest = width;
        //        }
        //    }
        //}
        //public async void onPageLoad()
        //{
        //    try
        //    {
        //        Task.Run(() =>
        //        {
        //            viewModel.IsLoading = true;
        //        });
        //        await Task.Run(async () =>
        //        {
        //            await viewModel.OnPageLoad();
        //            viewModel.IsLoading = false;
        //            Device.BeginInvokeOnMainThread(() =>
        //            {
        //                Questions.ItemsSource = viewModel.Questions;
        //            });
        //        });
        //        Task.Run(() =>
        //        {
        //            viewModel.IsLoading = false;
        //        });
        //    }
        //    catch(Exception)
        //    {
        //        Task.Run(() =>
        //        {
        //            viewModel.IsLoading = false;
        //            viewModel._navigationService.GoBack();
        //        });
        //    }
        //   // InitializeComponent();
        //}
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        /// <summary>
        /// Invoked when search button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        //private void SearchButton_Clicked(object sender, EventArgs e)
        //{
        //    this.Search.IsVisible = true;
        //    this.Title.IsVisible = false;
        //    this.SearchButton.IsVisible = false;
        //    if (this.TitleView != null)
        //    {
        //        double opacity;
        //        // Animating Width of the search box, from 0 to full width when it added to the view.
        //        var expandAnimation = new Animation(
        //            property =>
        //            {
        //                Search.WidthRequest = property;
        //                opacity = property / TitleView.Width;
        //                Search.Opacity = opacity;
        //            }, 0, TitleView.Width, Easing.Linear);
        //        expandAnimation.Commit(Search, "Expand", 16, 250, Easing.Linear);
        //    }
        //    SearchEntry.Focus();
        //}
        ///// <summary>
        ///// Invoked when back to title button is clicked.
        ///// </summary>
        ///// <param name="sender">The Sender</param>
        ///// <param name="e">Event Args</param>
        //private void BackToTitle_Clicked(object sender, EventArgs e)
        //{
        //    this.SearchButton.IsVisible = true;
        //    if (this.TitleView != null)
        //    {
        //        double opacity;
        //        // Animating Width of the search box, from full width to 0 before it removed from view.
        //        var shrinkAnimation = new Animation(property =>
        //        {
        //            Search.WidthRequest = property;
        //            opacity = property / TitleView.Width;
        //            Search.Opacity = opacity;
        //        },
        //        TitleView.Width, 0, Easing.Linear);
        //        shrinkAnimation.Commit(Search, "Shrink", 16, 250, Easing.Linear, (p, q) => this.SearchBoxAnimationCompleted());
        //    }
        //    SearchEntry.Text = string.Empty;
        //}
        ///// <summary>
        ///// Invokes when search box Animation completed.
        ///// </summary>
        //private void SearchBoxAnimationCompleted()
        //{
        //    this.Search.IsVisible = false;
        //    this.Title.IsVisible = true;
        //}
        private void ContactWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        private void ContactWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
    }
}