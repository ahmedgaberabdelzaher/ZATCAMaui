using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriod
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeFillingPeriodSuccessPage : ContentPage
    {
        public ChangeFillingPeriodSuccessPage()
        {
            InitializeComponent();
        }

        private void btnDownloadConfirmation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangeFillingPeriodPageView());
        }


    }
}