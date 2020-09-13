using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.VATDeclarationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPopUpPageView : PopupPage
    {
        public NotesPopUpPageView()
        {
            InitializeComponent();
            SetLTR();
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
        protected override bool OnBackButtonPressed()
        {
            return true ;
        }
        protected override bool OnBackgroundClicked()
        {
            return true;
        }
        private void OnCloseTapped(object sender, EventArgs e)
        {
            try
            {
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {

            }
        }

        private void NoteDetail_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}