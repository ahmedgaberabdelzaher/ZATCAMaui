using RGPopup.Maui.Pages;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdjustmentPopupPageView : PopupPage
    {
        public AdjustmentPopupPageView()
        {
            InitializeComponent();
            SetLTR();
        }
        private void SetLTR()
        {
            try
            {
                if (App.IsArabic)
                {
                    //this.FlowDirection = FlowDirection.RightToLeft;
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
                }
                else
                {
                    //this.FlowDirection = FlowDirection.LeftToRight;
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                    SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
                }
            }
            catch (Exception)
            {


            }
        }
    }
}