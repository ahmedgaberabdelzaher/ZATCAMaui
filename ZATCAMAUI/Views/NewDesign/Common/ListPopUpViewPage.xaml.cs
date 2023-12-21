using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    public partial class ListPopUpViewPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(object item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;
        public ListPopUpViewPage(object data)
        {
            InitializeComponent();
            PopupList.ItemsSource = (System.Collections.IEnumerable)data;
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
        async void PopupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnItemSelect?.Invoke(e.CurrentSelection.FirstOrDefault());
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception)
            {


            }
        }
    }
}
