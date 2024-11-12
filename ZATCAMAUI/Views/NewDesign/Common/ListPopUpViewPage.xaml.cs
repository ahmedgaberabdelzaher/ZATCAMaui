using Mopups.Pages;
using Mopups.Services;

namespace ZATCAMAUI.Views.NewDesign.Common
{

    public partial class ListPopUpViewPage : PopupPage
    {
        public delegate void OnItemSelectDelegate(object item);
        public OnItemSelectDelegate OnItemSelect { get; set; } = null;
        public event EventHandler Closed;
        public ListPopUpViewPage(object data)
        {
            InitializeComponent();
            PopupList.ItemsSource = (System.Collections.IEnumerable)data;
        }
        async void PopupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OnItemSelect?.Invoke(e.CurrentSelection.FirstOrDefault());
                await MopupService.Instance.PopAsync();
                Closed?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {


            }
        }
    }
}
