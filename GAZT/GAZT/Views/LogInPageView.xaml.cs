using GAZT.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPageView : ContentPage
    {
        LogInPageViewModel viewModel;
        int LanguageToolBarCount = 0;
        public LogInPageView()
        {
            viewModel = App.Locator.LogInPageView;

            InitializeComponent();
            App.IsArabic = true;
            SetRTLDirection();
            this.BindingContext = viewModel;

            ToolbarItem toolbarItem1 = new ToolbarItem
            {
                Icon = "ic_language.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 1,
                Command = new Command(() =>
                {
                    if (App.IsArabic)
                    {
                        App.IsArabic = false;
                        SetLTRDirection();
                    }
                    else
                    {
                        App.IsArabic = true;
                        SetRTLDirection();
                    }
                })
            };
            if (LanguageToolBarCount == 0)
            {
                LanguageToolBarCount = 1;
                this.ToolbarItems.Add(toolbarItem1);
            }

        }

        public void SetRTLDirection()
        {
            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
           // InitializeComponent();

            this.FlowDirection = FlowDirection.RightToLeft;
        }
        public void SetLTRDirection()
        {
            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
         //   InitializeComponent();
            this.FlowDirection = FlowDirection.LeftToRight;
        }

    }
}