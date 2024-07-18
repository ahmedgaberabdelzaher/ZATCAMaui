
using RGPopup.Maui.Pages;
using ZATCAMAUI.ViewModel.NewDesignViewModel.Instructions;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionsBottomPopUpView : PopupPage
    {
        private InstructionsBottomPopUpViewModel _viewModel;
        private bool isCheckboxchecked = false; 

        public InstructionsBottomPopUpView(string instructionString, string checkBoxString, string continueString,bool isEditable, InstructionsBottomPopUpViewModel.DialogType _dialogType)
        {
            InitializeComponent();
            _viewModel = App.Locator.InstructionsBottomPopUpView;
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
            isCheckboxchecked = isEditable;
           



        }

        public InstructionsBottomPopUpView(string instructionString, string checkBoxString, string continueString, InstructionsBottomPopUpViewModel.DialogType _dialogType)
        {
            InitializeComponent();
            _viewModel = App.Locator.InstructionsBottomPopUpView;
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

        public InstructionsBottomPopUpView(bool isWithCancelOption, string instructionString, string checkBoxString, string continueString, InstructionsBottomPopUpViewModel.DialogType _dialogType)
        {
            InitializeComponent();
            _viewModel = App.Locator.InstructionsBottomPopUpView;
            this.BindingContext = _viewModel;
            _viewModel.Description = instructionString;
            _viewModel.CheckBoxDescription = checkBoxString;
            _viewModel.ButtonTitle = continueString;

            if (_dialogType == InstructionsBottomPopUpViewModel.DialogType.Instructions)
            {
                _viewModel.IsInstructions = false;
                _viewModel.IsTerms = false;
            }
            else if (_dialogType == InstructionsBottomPopUpViewModel.DialogType.TermsConditions)
            {
                _viewModel.IsTerms = true;
                _viewModel.IsInstructions = false;
            }

            _viewModel.IsCancelButtonVisible = isWithCancelOption;



        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();


                if (isCheckboxchecked)
                {

                    _viewModel.IsInstuctionsChecked = true;
                    _viewModel.IsTermsChecked = true;
                    _viewModel.IsCheckboxEditable = false;
                }
                else {
                    _viewModel.IsCheckboxEditable = true;
                    _viewModel.IsInstuctionsChecked = false;
                    _viewModel.IsTermsChecked = false;

                }


               
                _viewModel.EnableCheckboxContinue();

            }
            catch (Exception) {

                
                
            }
        }

        protected override bool OnBackButtonPressed() => true;
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }

        private void CheckBox_CheckedChanged(object sender, Boolean e)
        {
            _viewModel.EnableCheckboxContinue();
        }

       


    }
}