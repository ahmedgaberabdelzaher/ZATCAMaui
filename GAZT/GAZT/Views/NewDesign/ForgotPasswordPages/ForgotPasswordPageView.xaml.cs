using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System;

namespace EGAZT.Views.NewDesign.ForgotPasswordPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GAZTNewDesignForgotPasswordPageView : ContentPage
    {
        
        private string _LblCountDownTimer;

        GAZTNewDesignForgotPasswordPageViewModel viewModel;
        List<BorderlessEntry> labels;
        public GAZTNewDesignForgotPasswordPageView()
        {

            InitializeComponent();
            viewModel = App.Locator.GAZTNewDesignForgotPasswordPageView;
            this.BindingContext = viewModel;
            viewModel.ClearData();
            SetLTR();
            viewModel.StartPage = 1;

            
            labels = new List<BorderlessEntry>();

            labels.Add(OTPFirstDigit);
            labels.Add(OTPSecondDigit);
            labels.Add(OTPThirdDigit);
            labels.Add(OTPFourthDigit);

        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;

            Editor editor = sender as Editor;


            string editorStr = editor.Text;
            //if string.length lager than max length
            if (editorStr.Length > 4)
            {
                editor.Text = editorStr.Substring(0, 4);
            }

            //dismiss keyboard
            if (editorStr.Length >= 4)
            {
                editor.Unfocus();
            }

            for (int i = 0; i < labels.Count; i++)
            {
                Entry lb = labels[i];

                if (i < editorStr.Length)
                {
                    lb.Text = editorStr.Substring(i, 1);
                }
                else
                {
                    lb.Text = "";
                }
            }
        }


        private void SetLTR()
        {
            if (App.IsArabic)
            {
                //switch (Xamarin.Forms.Device.RuntimePlatform)
                //{

                //    case Xamarin.Forms.Device.iOS:
                //        LandingCreateAccountNote.LineHeight = .6;
                //        break;
                //    case Xamarin.Forms.Device.Android:
                //        LandingCreateAccountNote.LineHeight = 1.25;
                //        break;
                //}

                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void OnPasswordCardClicked(object sender, EventArgs e)
        {
            viewModel.SetPasswordCardLayoutVisibility();
        }


        private void OnUserNameCardClicked(object sender, EventArgs e)
        {
            viewModel.SetUserNameCardVisibility();
        }


    }
}