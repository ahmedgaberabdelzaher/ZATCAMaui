namespace ZATCAMAUI.Core
{
    public class SfCardView : ContentView
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(propertyName: nameof(CornerRadius),
           returnType: typeof(double),
           declaringType: typeof(SfCardView),
           defaultValue: typeof(double));

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(propertyName: nameof(HasShadow),
          returnType: typeof(bool),
          declaringType: typeof(SfCardView),
          defaultValue: typeof(bool));

        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }


        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(propertyName: nameof(BorderColor),
          returnType: typeof(Color),
          declaringType: typeof(SfCardView),
          defaultValue: typeof(Color));

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(propertyName: nameof(IndicatorColor),
         returnType: typeof(Color),
         declaringType: typeof(SfCardView),
         defaultValue: typeof(Color));

        public Color IndicatorColor
        {
            get { return (Color)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }

        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(propertyName: nameof(ShadowColor),
          returnType: typeof(Color),
          declaringType: typeof(SfCardView),
          defaultValue: typeof(Color));

        public Color ShadowColor
        {
            get { return (Color)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(propertyName: nameof(BorderWidth),
          returnType: typeof(double),
          declaringType: typeof(SfCardView),
          defaultValue: typeof(double));

        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }


        public static readonly BindableProperty IndicatorThicknessProperty = BindableProperty.Create(propertyName: nameof(IndicatorThickness),
          returnType: typeof(double),
          declaringType: typeof(SfCardView),
          defaultValue: typeof(double));

        public double IndicatorThickness
        {
            get { return (double)GetValue(IndicatorThicknessProperty); }
            set { SetValue(IndicatorThicknessProperty, value); }
        }



        public static readonly BindableProperty IndicatorPositionProperty = BindableProperty.Create(propertyName: nameof(IndicatorPosition),
          returnType: typeof(IndicatorPositionEnum),
          declaringType: typeof(SfCardView),
          defaultValue: typeof(IndicatorPositionEnum));

        public IndicatorPositionEnum IndicatorPosition
        {
            get { return (IndicatorPositionEnum)GetValue(IndicatorPositionProperty); }
            set { SetValue(IndicatorPositionProperty, value); }
        }
    }

    public enum IndicatorPositionEnum
    {
        Left =0, Top =1, Right =2,
    }
    
    public class SfRotator : ListView
    {

    }
}
