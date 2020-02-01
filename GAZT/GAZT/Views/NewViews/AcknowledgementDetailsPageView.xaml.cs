using GAZT.ViewModel.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GAZT.Views.NewViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AcknowledgementDetailsPageView : ContentPage
	{

        #region Variable
        AcknowledgementDetailsPageViewModel viewModel;
        #endregion

        #region Property
        #endregion

        #region Constructor

        public AcknowledgementDetailsPageView()
        {
            InitializeComponent();
            try
            {
                viewModel = App.Locator.AcknowledgementDetailsPageView;
                this.BindingContext = viewModel;
                SetLTR();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Method
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        #endregion




    }
}