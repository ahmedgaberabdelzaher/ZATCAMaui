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
            this.FlowDirection = FlowDirection.LeftToRight;
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
        protected override bool OnBackButtonPressed() => true;
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (_viewModel.IsInstructions)
            {
                if (_viewModel.IsInstuctionsChecked)
                {
                    MessagingCenter.Send<Object, Boolean>(this, "InstructionsContinue", true);
                }
                else
                {
                    MessagingCenter.Send<Object, Boolean>(this, "InstructionsContinue", false);
                }
            }
            else if (_viewModel.IsTerms)
            {
                if (_viewModel.IsTermsChecked)
                {
                    MessagingCenter.Send<Object, Boolean>(this, "TermsContinue", true);
                }
                else
                {
                    MessagingCenter.Send<Object, Boolean>(this, "TermsContinue", false);
                }
            }
        }
    }
}