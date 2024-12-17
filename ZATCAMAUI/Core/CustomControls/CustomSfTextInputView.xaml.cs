using Syncfusion.Maui.Core;

namespace ZATCAMAUI.Core.CustomControls;

using Syncfusion.Maui.Core;
using Microsoft.Maui.Controls;



partial class CustomSfTextInputView : SfTextInputLayout
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

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == nameof(HasError))
        {
            UpdateErrorState();
        }
    }

    private void UpdateErrorState()
    {

        this.Stroke = HasError ? (Color)Application.Current.Resources["ErrorColor"] : (Color)Application.Current.Resources["NeutralGreay"];
        this.Dispatcher.Dispatch(() =>
        {
            this.InvalidateMeasure();
        });
    }
}
