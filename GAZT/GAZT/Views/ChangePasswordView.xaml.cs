using GAZT.Models;
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
	public partial class ChangePasswordView : ContentPage
	{
        ChangePasswordViewModel viewModel;
		public ChangePasswordView ()
		{
            viewModel = App.Locator.ChangePasswordView;
            InitializeComponent();
            App.IsArabic = true;
            this.BindingContext = viewModel;
        }
	}
}