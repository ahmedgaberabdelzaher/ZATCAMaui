using Syncfusion.Maui.Core;

namespace ZATCAMAUI.Core.CustomControls;

using Syncfusion.Maui.Core;
using Microsoft.Maui.Controls;

public partial class CustomSfTextInputView : SfTextInputLayout
{
    public CustomSfTextInputView()
    {
        InitializeComponent();
        UpdateErrorState();
    }

 
    public View TextInputView
    {
        get => MainContainer;
        set => MainContainer.Add(value);
    }

    public static new readonly BindableProperty HasErrorProperty =
        BindableProperty.Create(
            nameof(HasError),
            typeof(bool),
            typeof(CustomSfTextInputView),
            false,
            propertyChanged: OnHasErrorChanged);

    public new bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    private static void OnHasErrorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomSfTextInputView control && oldValue != newValue)
        {
            control.UpdateErrorState();
        }
    }


    private void UpdateErrorState()
    {
        this.Stroke = Color.FromArgb(HasError ? "#B00020" : "#999999");


        //refresh the layout manually
        this.Dispatcher.Dispatch(() =>
        {
            this.InvalidateMeasure();
            
        });
    }

}
