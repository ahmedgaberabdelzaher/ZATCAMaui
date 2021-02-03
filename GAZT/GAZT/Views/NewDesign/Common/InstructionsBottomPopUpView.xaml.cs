using System;
using EGAZT.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionsBottomPopUpView : PopupPage
    {
        private InstructionsBottomPopUpViewModel _viewModel;
        private bool isCheckboxchecked = false; 

        public InstructionsBottomPopUpView(string instructionString, string checkBoxString, string continueString,bool isEditable, InstructionsBottomPopUpViewModel.DialogType _dialogType)
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
            isCheckboxchecked = isEditable;
           



        }

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

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;

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
        protected override bool OnBackButtonPressed() => true;
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _viewModel.EnableCheckboxContinue();
        }

        private void CheckboxCustom_CheckChanged(object sender, bool e)
        {
            _viewModel.EnableCheckboxContinue();
        }
    }
}