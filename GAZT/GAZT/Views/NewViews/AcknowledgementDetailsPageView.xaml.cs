using GAZT.Models;
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

        public AcknowledgementDetailsPageView(VATDeclaration vATDeclaration)
        {
            InitializeComponent();
            try
            {
                viewModel = App.Locator.AcknowledgementDetailsPageView;
                this.BindingContext = viewModel;

                if(vATDeclaration!=null)
                {
                    viewModel.VATDeclarationData = vATDeclaration;
                    viewModel.TPName = App.TP.Name;
                    viewModel.ReturnReferenceNumber = viewModel.VATDeclarationData.d.Fbnum;
                    viewModel.TaxablePeriod = viewModel.VATDeclarationData.d.Perslt;
                    viewModel.ReceiptDate = viewModel.VATDeclarationData.d.ReceiptDt;
                }

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