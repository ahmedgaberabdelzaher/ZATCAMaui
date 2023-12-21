using System.Runtime.CompilerServices;

namespace ZATCAMAUI.Views.NewDesign.Common
{
    public partial class PageIndicatorView : StackLayout
    {
        #region BindableProperties
        public static readonly BindableProperty DotSizeProperty = BindableProperty.Create(propertyName: nameof(DotSize),
               returnType: typeof(int),
               declaringType: typeof(PageIndicatorView),
               defaultValue: 16);
        public int DotSize
        {
            get { return (int)GetValue(DotSizeProperty); }
            set { SetValue(DotSizeProperty, value); }
        }

        public static readonly BindableProperty MarkSizeProperty = BindableProperty.Create(propertyName: nameof(MarkSize),
               returnType: typeof(int),
               declaringType: typeof(PageIndicatorView),
               defaultValue: 24);
        public int MarkSize
        {
            get { return (int)GetValue(MarkSizeProperty); }
            set { SetValue(MarkSizeProperty, value); }
        }


        public static readonly BindableProperty MaxNumProperty = BindableProperty.Create(propertyName: nameof(MaxNum),
               returnType: typeof(int),
               declaringType: typeof(PageIndicatorView),
               defaultValue: 0);
        public int MaxNum
        {
            get { return (int)GetValue(MaxNumProperty); }
            set { SetValue(MaxNumProperty, value); }
        }

        public static readonly BindableProperty MinNumProperty = BindableProperty.CreateAttached(propertyName: nameof(MinNum),
               returnType: typeof(int),
               declaringType: typeof(PageIndicatorView),
               defaultValue: 0);
        public int MinNum
        {
            get { return (int)GetValue(MinNumProperty); }
            set { SetValue(MinNumProperty, value); }
        }

        public static readonly BindableProperty CompletedProperty = BindableProperty.Create(propertyName: nameof(Completed),
               returnType: typeof(bool),
               declaringType: typeof(PageIndicatorView),
               defaultValue: false);
        public bool Completed
        {
            get { return (bool)GetValue(CompletedProperty); }
            set { SetValue(CompletedProperty, value); }
        }

        #endregion
        public PageIndicatorView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                base.OnPropertyChanged(propertyName);
                if (propertyName == MarkSizeProperty.PropertyName)
                {
                    MinimumHeightRequest = MarkSize;
                }
                if (propertyName == MaxNumProperty.PropertyName)
                {
                    Children.Clear();
                    for (int i = 1; i <= MaxNum; i++)
                    {
                        Children.Add(new BoxView()
                        {
                            ClassId = i.ToString(),
                            AutomationId = i.ToString(),
                            BackgroundColor = (Color)Application.Current.Resources["NeutralLightGrey"],
                            HeightRequest = DotSize,
                            WidthRequest = DotSize,
                            CornerRadius = DotSize / 2,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center
                        });
                        Children.Add(new BoxView()
                        {
                            BackgroundColor = Colors.Transparent,
                            Margin = new Thickness(-DotSize / 2, 0),
                            HeightRequest = DotSize,
                            WidthRequest = DotSize + 5,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Center
                        });
                    }
                    Image completeMark = new Image()
                    {
                        Source = "ic_vat_check.png",
                        Margin = new Thickness(5, 0),
                        IsVisible = false,
                        HeightRequest = MarkSize,
                        WidthRequest = MarkSize,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    };
                    Children.Add(completeMark);
                }
                if (propertyName == MinNumProperty.PropertyName)
                {
                    int counter = 0, childCounter = 0;
                    Children.Where(c => c is BoxView).ToList().ForEach(box =>
                    {
                        //TODO
                        if (!string.IsNullOrEmpty(box.AutomationId))
                        {
                            if (counter < MinNum)
                            {
                                box.Background.BackgroundColor = (Color)Application.Current.Resources["Primary"];
                                if (childCounter - 1 > 0)
                                    Children[childCounter - 1].Background.BackgroundColor = (Color)Application.Current.Resources["Primary"];
                            }
                            else
                            {
                                box.Background.BackgroundColor = (Color)Application.Current.Resources["NeutralLightGrey"];
                                if (childCounter - 1 > 0)
                                    Children[childCounter - 1].Background.BackgroundColor = Colors.Transparent;
                            }
                            counter++;
                        }

                        childCounter++;
                    });
                }
                if (propertyName == CompletedProperty.PropertyName)
                {
                    //TODO
                    var view = (View)Children.LastOrDefault();
                    if (view != null)
                        view.IsVisible = Completed;
                    //Children.LastOrDefault().IsVisible = Completed;
                }
            }
            catch (Exception)
            {
            }
            
        }
    }
}
