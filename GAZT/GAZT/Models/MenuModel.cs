using System;
using GalaSoft.MvvmLight;

namespace EGAZT.Models
{
    public class MenuModel: ViewModelBase
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public string ID { get; set; }
        bool isVerticalView;
        public bool IsVerticalView { get { return isVerticalView; } set { isVerticalView = value; RaisePropertyChanged(); } }
    }
}
