using Syncfusion.Maui.Core;

namespace ZATCAMAUI.Core.CustomControls;

public partial class CustomSfTextInputView : SfTextInputLayout
{
	public CustomSfTextInputView()
	{
		InitializeComponent();
	}

    public View TextInputView
    {
        get => MainContainer;
        set => MainContainer.Add(value);
    }
}
