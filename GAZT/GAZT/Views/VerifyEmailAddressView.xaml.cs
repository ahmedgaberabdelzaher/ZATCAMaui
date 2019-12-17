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
	public partial class VerifyEmailAddressView : ContentPage
	{
        VerifyEmailAddressViewModel viewModel;
		public VerifyEmailAddressView ()
		{
            viewModel = App.Locator.VerifyEmailAddressView;
            InitializeComponent();
            App.IsArabic = true;
            this.BindingContext = viewModel;
        }
	}
}