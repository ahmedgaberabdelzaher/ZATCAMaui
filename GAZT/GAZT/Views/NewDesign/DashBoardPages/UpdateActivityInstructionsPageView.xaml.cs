using EGAZT.ViewModel.NewDesignViewModel.DashBoardPageViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.DashBoardPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateActivityInstructionsPageView : PopupPage
    {
       public  UpdateActivityInstructionsPageViewModel viewModel;
        public UpdateActivityInstructionsPageView(bool InstructionChecked,string respMessage)
        {
            InitializeComponent();
            viewModel = App.Locator.UpdateActivityInstructionsPage;
            this.BindingContext = viewModel;
            SetLTR();
            viewModel.IsInstructionChecked = InstructionChecked;
            viewModel.isInstructionCheckedEnable = !InstructionChecked;
            viewModel.responseMessage = respMessage;
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {

                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {

                this.FlowDirection = FlowDirection.RightToLeft;

            }
        }
    }
}