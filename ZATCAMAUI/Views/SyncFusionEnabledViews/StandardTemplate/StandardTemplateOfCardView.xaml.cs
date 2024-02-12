namespace ZATCAMAUI.Views.SyncFusionEnabledViews.StandardTemplate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardTemplateOfCardView : ContentPage
    {
        public StandardTemplateOfCardView()
        {
            InitializeComponent();
            App.IsArabic = false;
            ChangeAeroIcon();
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
    }
}