using GAZT.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
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
	public partial class TPProfileView : ContentPage
	{
        TPProfileViewModel viewModel;
        public TPProfileView ()
		{
            viewModel = App.Locator.TPProfileView;
            InitializeComponent();
            App.IsArabic = false;
            this.BindingContext = viewModel;
		}
     
    }
}