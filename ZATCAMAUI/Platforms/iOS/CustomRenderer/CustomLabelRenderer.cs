using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using UIKit;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    if (App.IsArabic)
                    {
                        Control.TextAlignment = UITextAlignment.Right;
                        if (e.NewElement != null)
                        {
                            string StyleId = e.NewElement.StyleId;

                            if (!string.IsNullOrEmpty(StyleId))
                            {
                                if (StyleId.Equals("ValueLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Left;
                                }
                                if (StyleId.Equals("SingleLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Right;
                                }
                            }
                        }
                    }
                    else
                    {
                        Control.TextAlignment = UITextAlignment.Left;
                        if (e.NewElement != null)
                        {
                            string StyleId = e.NewElement.StyleId;

                            if (!string.IsNullOrEmpty(StyleId))
                            {
                                if (StyleId.Equals("ValueLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Right;
                                }
                                if (StyleId.Equals("SingleLabel"))
                                {
                                    Control.TextAlignment = UITextAlignment.Left;
                                }
                            }
                        }
                    }

                    if (e.NewElement != null)
                    {
                        string StyleId = e.NewElement.StyleId;

                        if (!string.IsNullOrEmpty(StyleId))
                        {
                            if (StyleId.Equals("OnBoardText"))
                            {
                                Control.TextAlignment = UITextAlignment.Center;
                            }
                        }
                    }

                    if (e.NewElement != null)
                    {
                        string StyleId = e.NewElement.StyleId;

                        if (!string.IsNullOrEmpty(StyleId))
                        {
                            if (StyleId.Equals("LTRLabelText"))
                            {
                                Control.TextAlignment = UITextAlignment.Left;
                            }
                            if (StyleId.Equals("RTLLabelText"))
                            {
                                Control.TextAlignment = UITextAlignment.Right;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {


            }

        }
    }
}
