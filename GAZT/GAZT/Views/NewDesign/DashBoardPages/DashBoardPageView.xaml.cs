using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoardPageView : ContentPage
    {
        public DashBoardPageView()
        {
            InitializeComponent();
            MenuView.IsVisible = false;
            HomeView.IsVisible = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            HomeView.IsVisible = false;
            MenuView.IsVisible = true;
            HomeIndicator.BackgroundColor = Color.White;
            MenuIndicator.BackgroundColor = Color.DarkGreen;
            Tabbar.BorderColor = Color.Transparent;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MenuView.IsVisible = false;
            HomeView.IsVisible = true;
        }
    }
}