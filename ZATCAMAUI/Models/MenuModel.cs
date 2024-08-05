

using CommunityToolkit.Mvvm.ComponentModel;

namespace ZATCAMAUI.Models
{
    public class MenuModel : ObservableRecipient
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string ID { get; set; }
        bool isVerticalView;
        public bool IsVerticalView { get { return isVerticalView; } set { isVerticalView = value; OnPropertyChanged(); } }
        public int ColumnNo { get; set; }
        public int Row { get; set; }
        public string ServiceDesc { get; set; }
        public bool IsSelected { get; set; }
        public string SelectedImageSource { get; set; }
    }
}
