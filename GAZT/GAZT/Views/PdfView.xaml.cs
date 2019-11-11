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
		public PdfView ()
		{
           
            viewModel = App.Locator.pdfView;
            
            InitializeComponent ();

            this.BindingContext = viewModel;
            viewModel.OnPageLoad();
        }
	}
}