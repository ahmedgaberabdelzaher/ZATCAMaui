using EGAZT.ViewModel.NewDesignViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ZakatForm5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatForm5PageView : ContentPage
    {
        ZakatForm5PageViewModel viewModel;

        public ZakatForm5PageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatForm5PageView;
            this.BindingContext = viewModel;
            //viewModel.z = true;
            //viewModel.IsNoDataLabelVisible = false;
            IntialiseAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.IsComingFromSleepMode = false;
            if (viewModel != null)
            {
                viewModel.IsLoading = false;
                
            }

            Task.Run(async () =>
            {
                await LoadData();

                if (viewModel != null)
                    viewModel.IsLoading = false;
            });
        }

        private async Task LoadData()
        {
            try
            {
                App.DisplayProgressView();
                await viewModel.LoadZakatForm5Data();
                Device.BeginInvokeOnMainThread(() => {
                   
                    App.HideProgressView();
                    });

               
            }
            catch (Exception ex)
            {
                App.HideProgressView();
            }
        }
        public async Task IntialiseAsync()
        {
            try
            {
                await viewModel.LoadZakatForm5Data();
                if (viewModel.ZakatForm5DataResult != null )
                {
                   // BPicker.SelectedIndex = 14;
                }
            }
            catch (Exception e)
            {
            }
        }

        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (!App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }
    }
}