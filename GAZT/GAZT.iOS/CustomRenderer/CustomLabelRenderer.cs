using System;
using GAZT;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using EGAZT;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
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


