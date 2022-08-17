using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EGAZT.Views.NewDesign
{
    public partial class BaseContentPage : ContentPage
    {
        public string titleText { get; set; }
        public bool HasBackButton { get; set; } = true;
        public BaseContentPage()
        {
            InitializeComponent();
            titel.TitleText = titleText;
            titel.BackButtonVisible = HasBackButton;
        }
        public View PancakeView
        {
            get => MainContainer;
            set => MainContainer.Content = value;
        }
    }
}
