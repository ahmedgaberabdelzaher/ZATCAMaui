using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionsBottomPopUpView : PopupPage
    {
        private InstructionsBottomPopUpViewModel _viewModel;
        public InstructionsBottomPopUpView(string instructionString, string checkBoxString, string continueString, InstructionsBottomPopUpViewModel.DialogType _dialogType)
        {
            InitializeComponent();
            _viewModel = App.Locator.InstructionsBottomPopUpView;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = _viewModel;
            _viewModel.Description = instructionString;
            _viewModel.CheckBoxDescription = checkBoxString;
            _viewModel.ButtonTitle = continueString;

            if (_dialogType == InstructionsBottomPopUpViewModel.DialogType.Instructions)
            {
                _viewModel.IsInstructions = true;
                _viewModel.IsTerms = false;
            }
            else if (_dialogType == InstructionsBottomPopUpViewModel.DialogType.TermsConditions)
            {
                _viewModel.IsTerms = true;
                _viewModel.IsInstructions = false;
            }

        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();



                SetLTR();



            }
            catch (Exception e) { }
        }




        private void SetLTR()



        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        //protected override bool OnBackButtonPressed() => true;
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           

        }
    }
}