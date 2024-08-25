namespace ZATCAMAUI.Views.SyncFusionEnabledViews.StandardTemplate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StandardTemplateOfCardView : ContentPage
    {
        public StandardTemplateOfCardView()
        {
            InitializeComponent();
            App.IsArabic = false;
        }
       
    }
}