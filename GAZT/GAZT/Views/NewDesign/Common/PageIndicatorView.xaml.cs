using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace EGAZT.Views.NewDesign.Common
{
    public partial class PageIndicatorView : StackLayout
    {
        #region BindableProperties
        public static readonly BindableProperty MinNumProperty = BindableProperty.Create(propertyName: nameof(MinNum),
               returnType: typeof(int),
               declaringType: typeof(PageIndicatorView),
               defaultValue: 0,
               propertyChanged: UpdatePropertyChanged);
        public int MinNum
        {
            get { return (int)GetValue(MinNumProperty); }
            set { SetValue(MinNumProperty, value); }
        }

        public static readonly BindableProperty MaxNumProperty = BindableProperty.Create(propertyName: nameof(MaxNum),
               returnType: typeof(int),
               declaringType: typeof(PageIndicatorView),
               defaultValue: 0,
               propertyChanged: MaxNumPropertyChanged);
        public int MaxNum
        {
            get { return (int)GetValue(MaxNumProperty); }
            set { SetValue(MaxNumProperty, value); }
        }

        public static readonly BindableProperty CompletedProperty = BindableProperty.Create(propertyName: nameof(Completed),
               returnType: typeof(bool),
               declaringType: typeof(PageIndicatorView),
               defaultValue: false,
               propertyChanged: UpdatePropertyChanged);
        public bool Completed
        {
            get { return (bool)GetValue(CompletedProperty); }
            set { SetValue(CompletedProperty, value); }
        }


        private static void MaxNumPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var Controls = bindable as PageIndicatorView;
            //System.Diagnostics.Debug.WriteLine("MaxNumPropertyChanged with MinNum {0} MaxNum {1} Completed {2}", Controls.MinNum, Controls.MaxNum, Controls.Completed);
            for (int i = 1; i <= Controls.MaxNum; i++)
            {
                Controls.Children.Add(new BoxView()
                {
                    BackgroundColor = Color.WhiteSmoke,
                    HeightRequest = 10,
                    WidthRequest = 10,
                    CornerRadius = 5,
                });
            }
            Image completeMark = new Image()
            {
                Source = "ic_vat_check.png",
                IsVisible = false
            };
            Controls.Children.Add(completeMark);
        }

        private static void UpdatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var Controls = bindable as PageIndicatorView;
            System.Diagnostics.Debug.WriteLine("UpdatePropertyChanged with MinNum {0} MaxNum {1} Completed {2}", Controls.MinNum, Controls.MaxNum, Controls.Completed);
            var children = Controls?.Children;
            //System.Diagnostics.Debug.WriteLine("Children Counts {0} and boxes {1} ", children?.Count(), children?.Where(item => item is BoxView).Count());
            for (int index = 0; index < Controls.MinNum; index++)
            {
                if (children[index] is BoxView)
                {
                    (children[index] as BoxView).BackgroundColor = Color.Green;
                }
            }
            children[Controls.MaxNum].IsVisible = Controls.Completed;
        }
        #endregion
        public PageIndicatorView()
        {
            InitializeComponent();
        }
    }
}
