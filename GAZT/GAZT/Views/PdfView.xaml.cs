using GAZT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PdfView : ContentPage
	{
        PdfViewModel viewModel = null;
		public PdfView (string Pdfurl)
		{
           
            viewModel = App.Locator.pdfView;
             
            InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            viewModel.pdfUrl = Pdfurl;
            this.BindingContext = viewModel;
             
            // string str = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/corr_dataSet(Cokey='005056B1365C1EEA80F0BFC0C36DE462',Cotyp='ZVT3')/$value";
            

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.OnPageLoad();
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    string str = viewModel.DownloadUrl;
            //    Uri uri = new Uri(str);
            //    Device.OpenUri(uri);
            //}
        }
    }
}