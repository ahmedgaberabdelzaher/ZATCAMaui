using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriodPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodSuccessPage : ContentPage
    {
       
          //  public ChangeFillingPeriodViewModel viewModel;

            public ChangeFillingPeriodSuccessPage()
            {
                InitializeComponent();

                App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

               // viewModel = App.Locator.ChangeFillingPeriodSuccessPage;

               // this.BindingContext = viewModel;
            }

            private void SetLTR()
            {
                if (!App.IsArabic)
                {
                    this.FlowDirection = FlowDirection.LeftToRight;
                }
            }
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

        
        


    }
}